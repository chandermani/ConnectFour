using System;
using System.Linq;

namespace ConnectFour
{
    internal class GamePlay
    {
        private Board board;
        public GamePlay()
        {
            board = new Board();
        }

        public void Start()
        {
            while (true)
            {
                this.RenderBoard();
                if (board.GameOver)
                {
                    Console.WriteLine($"PLAYER {board.GameWinner} WON!!!!!!");
                    Console.ReadLine();
                    break;
                }
                Console.WriteLine($"{board.CurrentTurn} turn. Input column number to drop a token. Enter 'q' to quit");
                var input = Console.ReadLine();
                if (input?.ToLower() == "q")
                {
                    break;
                }
                if(string.IsNullOrEmpty( input ) )
                {
                    continue;
                }
                var column = int.Parse(input ?? "-1");
                var token = board.DropToken(column);
            }
        }

        private void RenderBoard()
        {
            Console.Clear();
            var gameState = this.board.GetGameState();
            for (int i = gameState.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(string.Join(" ", gameState[i].Select(c => c?.TokenType.ToString() ?? "Empty")));
            }
            Console.WriteLine();
        }
    }
}