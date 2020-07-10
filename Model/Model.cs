using System;
using System.Runtime.ExceptionServices;
using System.Security.Principal;

namespace Game2048
{
    public class Model
    {
        Map map;

        static Random random = new Random();

        public int size { get { return map.size; } }
        bool isGameOver;
        bool moved;
         
        public bool IsGameOver ()
        {
            return isGameOver;
        }

        public void Start ()
        {
            for (int y = 0; y < map.size; y++)
                for (int x = 0; x < map.size; x++)
                    map.Set(x, y, 0);
            AddRandomNumbers();
            AddRandomNumbers();
        }

        private void AddRandomNumbers()
        {
            if (isGameOver) return;
            for (int i = 0; i <= 100; i++)
            {
                int x = random.Next(0, map.size);
                int y = random.Next(0, map.size);
                if (map.Get(x, y) == 0)
                {
                    map.Set(x, y, random.Next(1, 3) * 2);
                    return;
                }
            }
        }

        public Model(int dimension)
        {
            map = new Map(dimension);
        }

        void Move(int x, int y, int sx, int sy)
        {
            if (map.Get(x, y) > 0)
                while (map.Get(x + sx, y + sy) == 0)
                {
                    map.Set(x + sx, y + sy, map.Get(x, y));
                    map.Set(x, y, 0);
                    x += sx;
                    y += sy;
                    moved = true;
                }
        }

        void Merge(int x, int y, int sx, int sy)
        {
            if (map.Get(x, y) > 0)
                if (map.Get(x + sx, y + sy) == map.Get(x, y))
                {
                    map.Set(x + sx, y + sy, map.Get(x, y) * 2);
                    while (map.Get(x - sx, y - sy) > 0)
                    {
                        map.Set(x, y, map.Get(x - sx, y - sy));
                        x -= sx;
                        y -= sy;
                    }
                    map.Set(x, y, 0);
                    moved = true;
                }
        }

        public void Up()
        {
            moved = false;
            for (int i = 0; i < map.size; i++)
                for (int j = 1; j < map.size; j++)
                {
                    Move(i, j, 0, -1);
                }
            for (int i = 0; i < map.size; i++)
                for (int j = 1; j < map.size; j++)
                {
                    Merge(i, j, 0, -1);
                }
            if (moved) AddRandomNumbers();
        }

        public void Down()
        {
            moved = false;
            for (int i = 0; i < map.size; i++)
                for (int j = map.size - 2; j >= 0; j--)
                    Move(i, j, 0, +1);
            for (int i = 0; i < map.size; i++)
                for (int j = map.size - 2; j >= 0; j--)
                    Merge(i, j, 0, +1);
            if (moved) AddRandomNumbers();
        }

        public void Left()
        {
            moved = false;
            for (int i = 0; i < map.size; i++)
                for (int j = 1; j < map.size; j++)
                {
                    Move(j, i, -1, 0);
                }
            for (int i = 0; i < map.size; i++)
                for (int j = 1; j < map.size; j++)
                {
                    Merge(j, i, -1, 0);
                }
            if (moved) AddRandomNumbers();

        }

        public void Right()
        {
            moved = false;
            for (int i = 0; i < map.size; i++)
                for (int j = map.size - 2; j >= 0; j--)
                {
                    Move(j, i, +1, 0);
                }
            for (int i = 0; i < map.size; i++)
                for (int j = map.size - 2; j >= 0; j--)
                {
                    Merge(j, i, +1, 0);
                }
            if (moved) AddRandomNumbers();
        }

        public int GetMap(int x, int y)
        {
            return map.Get(x, y);
        }
    }
}
