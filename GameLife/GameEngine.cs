using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GameEngine
    {
        public uint currentGeneration = 0;
        public bool[,] field { get; private set; }
        public int rows { get; private set; }
        public int cols { get; private set; }

        public GameEngine(int rows, int cols, int desity)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];
            Random random = new Random();
            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                    field[x, y] = random.Next(desity) == 0;

        }
        public void ValidationPassed(int cols, int rows, bool flag)
        {
            field[cols, rows] = flag;
        }
        public void NextGeneration()
        {
            var newField = new bool[cols, rows];
            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);
                    var hasLife = field[x, y];
                    if (!hasLife && neighboursCount == 3)
                    {
                        newField[x, y] = true;
                    }
                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                    {
                        newField[x, y] = false;
                    }
                    else
                    {
                        newField[x, y] = field[x, y];
                    }

                }
            field = newField;

        }
        public bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + j + cols) % cols;
                    var row = (y + i + rows) % rows;

                    var isSelfCheking = col == x && row == y;
                    var hasLife = field[col, row];
                    if (hasLife && !isSelfCheking)
                        count++;
                }

            return count;
        }
    }
}
