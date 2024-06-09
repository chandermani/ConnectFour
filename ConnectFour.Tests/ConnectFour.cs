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
            Assert.Equal(tokens.Select((t, i) => (t.Row, t.Column)), expectedMoves);
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

        // TODO: A lot many game play tests can be added for column
        [Theory]
        [InlineData(TokenType.BLUE, new int[] { 0, 1, 0, 1, 0, 1,0 }, TokenType.BLUE)]
        [InlineData(TokenType.RED, new int[] { 0, 1, 0, 1, 0, 1,0 }, TokenType.RED)]
        public void ShouldDeclareAWinnerIfFourInSequenceInColumn(TokenType firstMover, int[] nextMoves, TokenType expectedWinner)
        {
            // Arrange
            Board target = new Board(8, 10, firstMover);

            // Act
            var tokens = nextMoves.Select(c => target.DropToken(c)).ToList();

            // Assert
            Assert.True(target.GameWinner==expectedWinner);
            Assert.True(target.GameOver);
        }

        [Theory]
        [InlineData(TokenType.BLUE, new int[] { 0, 1, 0, 1, 0 })]
        [InlineData(TokenType.RED, new int[] { 0, 1, 0, })]
        public void ShouldNotDeclareWinnerUnlessFourInSequence(TokenType firstMover, int[] nextMoves)
        {
            // Arrange
            Board target = new Board(8, 10, firstMover);

            // Act
            var tokens = nextMoves.Select(c => target.DropToken(c)).ToList();

            // Assert
            Assert.Null(target.GameWinner);
            Assert.False(target.GameOver);
        }

        /**
         * TODO: Game play tests to be added
         * ShouldDeclareAWinnerIfFourInSequenceInRow
         * ShouldDeclareAWinnerIfFourInSequenceInDiagonal1
         * ShouldDeclareAWinnerIfFourInSequenceInDiagonal2
         */
    }
}