public enum TokenType
{
    RED = 1,
    BLUE = 2
}

public class Token
{
    public TokenType TokenType { get; }

    public int Row { get; }
    public int Column { get; }
    public Token(TokenType tokenType, int row, int column)
    {
        this.Row = row;
        this.Column = column;
        this.TokenType = tokenType;
    }
}