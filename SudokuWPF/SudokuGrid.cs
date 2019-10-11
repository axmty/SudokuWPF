using System;
using System.Linq;
using System.Text;

namespace SudokuWPF
{
    public class SudokuGrid
    {
        private readonly int[] _grid = new int[81];

        public static SudokuGrid BuildRandomFilledGrid()
        {
            var resultGrid = new SudokuGrid();
            var random = new Random();

            bool recursiveFill()
            {
                var index = Array.IndexOf(resultGrid._grid, 0);
                var line = index / 9;
                var column = index % 9;
                var distinctNumbers = Enumerable.Range(1, 9).OrderBy(x => random.Next());
                var validValues = distinctNumbers.Where(value => resultGrid.CanInsertValue(value, line, column));

                foreach (var value in validValues)
                {
                    resultGrid._grid[index] = value;

                    if (resultGrid.IsFull() || recursiveFill())
                    {
                        return true;
                    }
                }

                resultGrid._grid[index] = 0;

                return false;
            }

            recursiveFill();

            return resultGrid;
        }

        public int this[int i, int j]
        {
            get
            {
                return _grid[i * 9 + j];
            }
            set
            {
                if (value < 1 || value > 9)
                {
                    throw new InvalidOperationException("Grid value must be a number between 1 and 9.");
                }

                _grid[i * 9 + j] = value;
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
                return _grid.SequenceEqual(otherGrid._grid);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this._grid);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                {
                    builder.AppendLine(" ");
                }

                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                    {
                        builder.Append(" ");
                    }

                    builder.Append($"{_grid[i * 9 + j]} ");
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        //private bool CanSolve()
        //{
        //    var copy = new SudokuGrid();

        //    Array.Copy(_grid, copy._grid, 81);

        //    bool recursiveCanSolve()
        //    {
        //        var emptyIndexes = Enumerable.Range(0, 81).Where(i => resultGrid._grid[i] == 0);

        //        foreach (var index in emptyIndexes)
        //        {
        //            var distinctNumbers = Enumerable.Range(1, 9).OrderBy(x => random.Next());
        //            var line = index / 9;
        //            var column = index % 9;

        //            var validValues = distinctNumbers.Where(
        //                value =>
        //                    !resultGrid.GetLine(line).Contains(value) &&
        //                    !resultGrid.GetColumn(column).Contains(value) &&
        //                    !resultGrid.GetSquare(line, column).Contains(value));

        //            foreach (var value in validValues)
        //            {
        //                resultGrid._grid[index] = value;

        //                if (resultGrid.IsFull() || recursiveFill())
        //                {
        //                    return true;
        //                }
        //            }

        //            resultGrid._grid[index] = 0;
        //            break;
        //        }

        //        return false;
        //    }
        //}

        private bool CanInsertValue(int value, int line, int column)
        {
            if (column < 0 || column > 9 || line < 0 || line > 9)
            {
                throw new ArgumentException("Column and line must be in range [0, 8].");
            }

            if (value < 1 || value > 9)
            {
                throw new InvalidOperationException("Grid value must be a number between 1 and 9.");
            }

            return
                !this.GetLine(line).Contains(value) &&
                !this.GetColumn(column).Contains(value) &&
                !this.GetSquare(line, column).Contains(value);
        }

        private bool IsFull()
        {
            return !_grid.Contains(0);
        }

        private int[] GetSquare(int line, int column)
        {
            if (column < 0 || column > 9 || line < 0 || line > 9)
            {
                throw new ArgumentException("Column and line must be in range [0, 8].");
            }

            var result = new int[9];
            var firstSquareLine = (line / 3) * 3;
            var firstSquareColumn = (column / 3) * 3;

            for (int i = 0; i < 9; i++)
            {
                var currentLine = firstSquareLine + i / 3;
                var currentColumn = firstSquareColumn + i % 3;

                result[i] = _grid[currentLine * 9 + currentColumn];
            }

            return result;
        }

        private int[] GetColumn(int column)
        {
            if (column < 0 || column > 9)
            {
                throw new ArgumentException("Column must be in range [0, 8].", nameof(column));
            }

            var result = new int[9];

            for (int i = 0; i < 9; i++)
            {
                result[i] = _grid[column + i * 9];
            }

            return result;
        }

        private int[] GetLine(int line)
        {
            if (line < 0 || line > 9)
            {
                throw new ArgumentException("Line must be in range [0, 8].", nameof(line));
            }

            var result = new int[9];

            for (int i = 0; i < 9; i++)
            {
                result[i] = _grid[i + 9 * line];
            }

            return result;
        }
    }
}
