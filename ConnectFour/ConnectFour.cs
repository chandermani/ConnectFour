public enum TokenType
{
    RED = 1,
    BLUE = 2
}

public class Token
{
    public TokenType TokenType {get; private set; }

    public int X { get; private set; }
    public int Y { get; private set; }
    public Token(TokenType tokenType, int x, int y) {
        this.X= x;
        this.Y= y;
        this.TokenType = tokenType;
    }
}

public class Board {
    private Token[,] boardTokens;
    private int[] nextEmptySlotInColumns;

    public int Rows { get; }
    public int Columns { get; }
    public TokenType CurrentTurn { get; private set; }

    public Board(int rows = 6, int columns = 7, TokenType firstMover = TokenType.BLUE) {
        this.Rows = rows;
        this.Columns = columns;
        boardTokens = new Token[rows, columns];
        nextEmptySlotInColumns = new int[columns];
        this.CurrentTurn = firstMover;
    }

    public bool ColumnFull(int toColumn) => nextEmptySlotInColumns[toColumn] >= this.Rows;

    public Token DropToken(int toColumn)
    {
        if(this.OutOfBounds(toColumn))
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
        nextEmptySlotInColumns[toColumn] += 1;
        FlipTurn();
        return token;
    }

    private bool OutOfBounds(int column) => column < 0 || column >= this.Columns;

    private void FlipTurn()
    {
        this.CurrentTurn = (this.CurrentTurn== TokenType.RED) ? TokenType.BLUE : TokenType.RED;
    }
}