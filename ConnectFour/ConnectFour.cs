public class Board {
    private const int WinningSequenceLength = 4;
    private Token[,] boardTokens;
    private int[] nextEmptySlotInColumns;

    public int Rows { get; }
    
    public int Columns { get; }
    
    public TokenType CurrentTurn { get; private set; }
    
    public TokenType? GameWinner { get; private set; }
    
    public bool GameOver => GameWinner != null;

    public bool ColumnFull(int column) => nextEmptySlotInColumns[column] >= this.Rows;

    public Board(int rows = 6, int columns = 7, TokenType firstMover = TokenType.BLUE) {
        this.Rows = rows;
        this.Columns = columns;
        boardTokens = new Token[rows, columns];
        nextEmptySlotInColumns = new int[columns];
        this.CurrentTurn = firstMover;
    }

    public Token DropToken(int toColumn)
    {
        // TODO: Handle game over case
        if(this.OutOfBoundOfBoard(0, toColumn))
        {
            throw new IndexOutOfRangeException($"Attempt to add token to column {toColumn} that does not exist. Valid values 0 - {this.Columns - 1}");
        }

        if (this.ColumnFull(toColumn))
        {
            throw new IndexOutOfRangeException($"Attempt to add token to column {toColumn} that has all rows filled.");
        }

        int toRow = this.nextEmptySlotInColumns[toColumn];

        var token = new Token(this.CurrentTurn, toRow, toColumn);
        boardTokens[toRow, toColumn] = token;

        if (IsWinningMove(token))
        {
            EndGame();
        }
        else
        {
            nextEmptySlotInColumns[toColumn] += 1;
            FlipTurn();
        }
        return token;
    }

    public Token[][] GetGameState()
    {
        Token[][] state = new Token[Rows][];
        for (int r = 0; r < state.Length; r++)
        {
            state[r] = Enumerable.Range(0, Columns).Select(c => this.boardTokens[r, c]).ToArray();
        }
        return state;
    }

    private void EndGame()
    {
        this.GameWinner = this.CurrentTurn;
    }

    private bool IsWinningMove(Token last)
    {
        // TODO: The winning logic can be exernalized instead of implementing this in the Board class.
        // From the last token added traverse in all direction to find if the is a sequence of the desired length
        // Subtracting one as double counting the last token due to two calls to MatchInDirection
        return
            // Check row
            (MatchInDirection(last, 0, -1) + MatchInDirection(last, 0, 1) - 1 == WinningSequenceLength)
                // Check Column
                || (MatchInDirection(last, -1, 0) + MatchInDirection(last, 1, 0) - 1 == WinningSequenceLength)
                // Diagonal 1
                || (MatchInDirection(last, -1, -1) + MatchInDirection(last, 1, 1) - 1 == WinningSequenceLength)
                // Diagonal 2
                || (MatchInDirection(last, 1, -1) + MatchInDirection(last, -1, 1) - 1 == WinningSequenceLength);
    }

    /// <summary>
    /// Return the number of contiguous token in a specific direction on the grid
    /// </summary>
    /// <param name="start">Start token position</param>
    /// <param name="dr">Direction increment for row</param>
    /// <param name="dc">Direction increment for column</param>
    /// <returns>Length of contiguous tokens on board</returns>
    private int MatchInDirection(Token start, int dr, int dc)
    {
        int matched = 1;
        int nextRow = start.Row + dr, nextColumn = start.Column + dc;
        while (!this.OutOfBoundOfBoard(nextRow, nextColumn) 
            && this.boardTokens[nextRow, nextColumn]?.TokenType == start.TokenType)
        {
            matched += 1;
            nextRow += dr;
            nextColumn+= dc;
        }

        return matched;
    }

    private bool OutOfBoundOfBoard(int row, int column) =>  
        row < 0 || column < 0
        || row >= this.Rows || column >= this.Columns;

    private void FlipTurn()
    {
        this.CurrentTurn = (this.CurrentTurn== TokenType.RED) ? TokenType.BLUE : TokenType.RED;
    }
}