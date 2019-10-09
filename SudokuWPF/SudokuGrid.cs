using System;

namespace SudokuWPF
{
    public class SudokuGrid
    {
        private readonly int[,] _grid = new int[9, 9];

        public int this[int i, int j]
        {
            get
            {
                return _grid[i, j];
            }
            set
            {
                _grid[i, j] = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (!(obj is SudokuGrid otherGrid))
            {
                throw new ArgumentException(nameof(obj));
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (_grid[i, j] != otherGrid._grid[i, j])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this._grid);
        }
    }
}
