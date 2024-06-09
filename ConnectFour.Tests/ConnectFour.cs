namespace ConnectFour.Tests
{
    public class ConnectFour
    {
        [Theory]
        [InlineData(4, 4, -1)]
        [InlineData(4, 4, -2)]
        [InlineData(4, 4, 4)]
        [InlineData(4, 4, 5)]
        public void ShouldThrowExceptionIfTryingToAddToOutOfBoundColumn(int boardRows, int boardColumns, int addToColumn)
        {
            //Arrange
            Board target = new Board(boardRows, boardColumns);

            // Act and assert
            // TODO: can be more precise with verification
            Assert.Throws<IndexOutOfRangeException>(() => target.DropToken(addToColumn));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void ShouldThrowExceptionIfTryingToAddToColumnAlreadyFilled(int prefillColumn)
        {
            // Arrange
            Board target = new Board();
            Enumerable.Repeat(1, target.Rows).Select(_ => target.DropToken(prefillColumn)).ToList();

            // Act and assert
            // TODO: can be more precise with verification
            Assert.Throws<IndexOutOfRangeException>(() => target.DropToken(prefillColumn));
        }

        [Theory]
        [InlineData(new int[] { 0, 2, 3, 4, 0, 5, 6, 4, 0 }, new int[] { 0, 0, 0, 0, 1, 0, 0, 1, 2 })]
        [InlineData(new int[] { 0, 2, 3, 4, 0, 1, 1, 1, 0 }, new int[] { 0, 0, 0, 0, 1, 0, 1, 2, 2 })]
        public void ShouldAddTokenCorrectRowColumnIfValidMove(int[] nextMoves, int[] expectedRows)
        {
            // TODO: Not asserting board position currently.
            // Arrange
            Board target = new Board();

            // Act
            var tokens = nextMoves.Select(c => target.DropToken(c));

            // Assert
            var expectedMoves = expectedRows.Zip(nextMoves);
            Assert.Equal(tokens.Select((t, i) => (t.X, t.Y)), expectedMoves);
        }

        [Theory]
        [InlineData(TokenType.BLUE, new int[] { 0, 2, 3, 4, 0 }, new TokenType[] { TokenType.BLUE, TokenType.RED, TokenType.BLUE, TokenType.RED, TokenType.BLUE })]
        [InlineData(TokenType.RED, new int[] { 0, 2, 3 }, new TokenType[] { TokenType.RED, TokenType.BLUE, TokenType.RED })]
        public void ShouldFlipPlayersIfValidMove(TokenType firstMover, int[] nextMoves, TokenType[] expectedPlayers)
        {
            // Arrange
            Board target = new Board(4, 5, firstMover);

            // Act
            var tokens = nextMoves.Select(c => target.DropToken(c));

            // Assert
            Assert.Equal(tokens.Select((t) => t.TokenType), expectedPlayers);
        }
    }
}