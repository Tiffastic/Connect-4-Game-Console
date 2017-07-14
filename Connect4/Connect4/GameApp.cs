using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

/**
 * Connect 4 GameApp runs a simulation of the Connect 4 Game class.
 * Each player takes turn dropping their token down the game board columns.
 * Available columns are 1-7, any other number is invalid and results
 * in lost of turn.
 * 
 * Connect 4 of the same tokens horizontally, vertically, diagonally up/down
 * wins the game.
 * 
 * Thuy Nguyen
 * October 2, 2014
 * C# Class
 * 
 */
namespace Connect4
{
    class GameApp
    {

        // public properties
        public int Top
        {
            get
            {
                return top;
            }
            set
            {
                if (value >= 0 && value <= Console.WindowHeight - 100)
                {
                    top = value;
                }
            }
        }
        public int Left
        {
            get
            {
                return left;
            }
            set
            {
                if (value >= 5 && value <= Console.WindowWidth - 50)
                {
                    left = value;
                }
            }
        }

        // private fields
        private int top, left;
        private string title;
        private string stars;
        private Game connect4;
        private Player player1, player2;


        // Constructor 
        public GameApp(string player1 = "Player 1", string player2 = "Player 2")
        {
            this.player1 = new Player(player1, Game.TOKEN_RED);
            this.player2 = new Player(player2, Game.TOKEN_YELLOW);

            connect4 = new Game();
            this.player1.HasTurn = true;
            PrepareConsole();
        }


        // public method
        public void StartGame(int speed = 650)
        {
            ShowInstructions();

            Player player;

            do
            {
                RefreshScreen();
                connect4.PrintGameBoard(Left, Top + 5);

                player = TakeTurns();
                PrintTurn(player);
                MakeMove(player, speed);

            }

            while ((!connect4.HasWon()) && !connect4.HasTie());

            // winner or tie
            AnnounceResults(player);

        }


        // private methods
        private void PrepareConsole()
        {
            title = "Connect " + Game.CONNECTED_NUM;
            Console.Title = title + " in C# - by Thuy Nguyen";
            Top = 5;
            Left = Console.WindowWidth / 3;

            stars = "*".PadRight(39, '*');
        }

        private void ShowInstructions()
        {
            MessageBox.Show(string.Format("\t\tWelcome to Connect {0} by Thuy Nguyen!\nThere are {1} columns to drop your tokens into. Please select columns 1-{1}", Game.CONNECTED_NUM, connect4.Column), "Instructions");
        }

        private void CenterMessage(string message, int top)
        {
            int left = Left - 1 + (Game.BOARD_LENGTH - message.Length) / 2;
            Console.SetCursorPosition(left, top);
            Console.Write(message);

        }

        private void RefreshScreen()
        {
            Console.BackgroundColor = (player1.HasTurn) ? ConsoleColor.Green : ConsoleColor.DarkMagenta;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            CenterMessage(title, Top);

        }

        public Player TakeTurns()
        {
            if (player1.HasTurn)
            {
                player1.HasTurn = false;
                player2.HasTurn = true;
                return player1;
            }
            else
            {
                player2.HasTurn = false;
                player1.HasTurn = true;
                return player2;
            }
        }

        private void PrintTurn(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = (player.Token == Game.TOKEN_RED) ? ConsoleColor.Red : ConsoleColor.Yellow;

            CenterMessage(stars, Console.CursorTop + 2);
            CenterMessage(player.ToString(), Console.CursorTop + 2);

        }

        private void MakeMove(Player player, int speed)
        {
            int column;
            if (!int.TryParse(Console.ReadLine().Trim(), out column) || column > connect4.Column || column < 1)
            {
                LosesTurn();

            }
            else
            {
                column--;
                connect4.DropToken(column, player.Token, speed, Left, Top + 5);
            }
        }

        private void LosesTurn(int wait = 2000)
        {
            string invalid = "Invalid move, loses a turn";
            Console.CursorVisible = false;
            CenterMessage(invalid, Console.CursorTop + 1);

            Thread.Sleep(wait);
            Console.CursorVisible = true;
        }

        private void AnnounceResults(Player player)
        {
            string result = connect4.HasWon() ? string.Format("!!!! {0} is the Winner !!!!", player.Name) : string.Format("{0} and {1} has tied!", player1.Name, player2.Name);

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Cyan;

            CenterMessage(result, Top + 2);
        }

    }
}
