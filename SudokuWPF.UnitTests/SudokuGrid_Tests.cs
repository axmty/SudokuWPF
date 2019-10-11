using FluentAssertions;
using Xunit;

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

        [Fact]
        public void BuildRandomFilledGrid_ShouldReturnValidGrid()
        {
            var randomGrid = SudokuGrid.BuildRandomFilledGrid();

            randomGrid.IsValid().Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Remove_ShouldEmptyTheSpecifiedNumberOfCells(int numberToRemove)
        {
            var randomGrid = SudokuGrid.BuildRandomFilledGrid();

            randomGrid.Remove(numberToRemove);

            randomGrid.CountEmptyCells().Should().Be(numberToRemove);
        }
    }
}
