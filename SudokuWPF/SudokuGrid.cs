using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuWPF
{
    public class SudokuGrid
    {
        private readonly int[] _grid = new int[81];

        public int this[int i, int j]
        {
            get
            {
                return _grid[i * 9 + j];
            }
            set
            {
                this.CheckValue(value);
                _grid[i * 9 + j] = value;
            }
        }

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

        public void Remove(int maxCellsToRemove)
        {
            var random = new Random();
            var removeCount = 0;

            while (removeCount < maxCellsToRemove)
            {
                var nonEmptyCellIndexes = _grid
                    .Where(value => value != 0)
                    .Select((_, index) => index)
                    .OrderBy(x => random.Next());

                foreach (var index in nonEmptyCellIndexes)
                {
                    var backupValue = _grid[index];
                    var copy = new SudokuGrid();

                    _grid[index] = 0;
                    Array.Copy(_grid, copy._grid, 81);

                    if (!copy.HasUniqueSolution())
                    {
                        _grid[index] = backupValue;
                    }
                    else
                    {
                        removeCount++;
                        break;
                    }
                }

                break;
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

        private bool HasUniqueSolution()
        {
            var copy = new SudokuGrid();
            var count = 0;

            Array.Copy(_grid, copy._grid, 81);

            bool recursiveSolve()
            {
                var index = Array.IndexOf(_grid, 0);
                var line = index / 9;
                var column = index % 9;
                var distinctNumbers = Enumerable.Range(1, 9);
                var validValues = distinctNumbers.Where(value => copy.CanInsertValue(value, line, column));

                foreach (var value in validValues)
                {
                    copy._grid[index] = value;

                    if (copy.IsFull())
                    {
                        count++;
                        break;
                    }
                    else if (recursiveSolve())
                    {
                        return true;
                    }
                }

                _grid[index] = 0;

                return false;
            }

            recursiveSolve();

            return count == 1;
        }

        private bool CanInsertValue(int value, int line, int column)
        {
            this.CheckLineOrColumn(line);
            this.CheckLineOrColumn(column);
            this.CheckValue(value);

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
            this.CheckLineOrColumn(line);
            this.CheckLineOrColumn(column);

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
            this.CheckLineOrColumn(column);

            var result = new int[9];

            for (int i = 0; i < 9; i++)
            {
                result[i] = _grid[column + i * 9];
            }

            return result;
        }

        private int[] GetLine(int line)
        {
            this.CheckLineOrColumn(line);

            var result = new int[9];

            for (int i = 0; i < 9; i++)
            {
                result[i] = _grid[i + 9 * line];
            }

            return result;
        }

        private void CheckLineOrColumn(int lineOrColumn)
        {
            if (lineOrColumn < 0 || lineOrColumn >= 9)
            {
                throw new ArgumentException("Lines and columns must be numbers between 0 and 8.");
            }
        }

        private void CheckValue(int value)
        {
            if (value < 0 || value > 0)
            {
                throw new InvalidOperationException("Grid value must be a number between 0 and 9.");
            }
        }
    }
}
