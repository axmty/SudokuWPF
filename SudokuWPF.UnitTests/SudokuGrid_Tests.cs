using System;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace SudokuWPF.UnitTests
{
    public class SudokuGrid_Tests
    {
        [Fact]
        public void CanInsertValue_WhenValueAlreadyExistsInLine_ShouldReturnFalse()
        {
            var randomGrid = SudokuGrid.BuildRandomFilledGrid();

            randomGrid.CanInsertValue(randomGrid[0, 0], 0, 8).Should().BeFalse();
        }

        [Fact]
        public void CanInsertValue_WhenValueAlreadyExistsInColumn_ShouldReturnFalse()
        {
            var randomGrid = SudokuGrid.BuildRandomFilledGrid();

            randomGrid.CanInsertValue(randomGrid[0, 0], 8, 0).Should().BeFalse();
        }

        [Fact]
        public void CanInsertValue_WhenValueAlreadyExistsInSquare_ShouldReturnFalse()
        {
            var randomGrid = SudokuGrid.BuildRandomFilledGrid();

            randomGrid.CanInsertValue(randomGrid[0, 0], 1, 1).Should().BeFalse();
        }

        [Fact]
        public void BuildRandomFilledGrid_ShouldReturnFullGrid()
        {
            var randomGrid = SudokuGrid.BuildRandomFilledGrid();

            randomGrid.IsFull().Should().BeTrue();
        }
    }
}
