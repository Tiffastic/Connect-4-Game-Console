using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/**
 * The Connect 4 Game displays a game board of 6 rows x 7 columns,
 * where tokens are dropped into a selected column via animated motion.
 * 
 * The Game evaluates whether there are four of the same token connected
 * horizontally, vertically, or diagonally up/down.  Or evalutes a tie 
 * if there are no more spaces in the game board and nobody has won.
 * 
 * Thuy Nguyen
 * October 2, 2014
 * C# Class
 * 
 */
namespace Connect4
{
    class Game
    {

        // public properties
        public int Row
        {
            get;
            private set;
        }
        public int Column
        {
            get;
            private set;

        }


        // public constants
        public const char TOKEN_RED = '@';
        public const char TOKEN_YELLOW = '#';
        public const int BOARD_LENGTH = 31;
        public const int CONNECTED_NUM = 4;

        // private fields
        private char[,] board;
        private char space;

        // constructor
        public Game()
        {
            Row = 6;
            Column = 7;
            board = new char[Row, Column];
            space = ' ';

            PrepareBoard();
        }


        // public methods
        public void PrintGameBoard(int cursorLeft = 25, int cursorTop = 10)
        {
            PrintBorder(cursorLeft, cursorTop);

            for (int i = 0; i < Row; i++)
            {
                cursorTop++;
                Console.SetCursorPosition(cursorLeft, cursorTop);

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(" ");

                for (int j = 0; j < Column; j++)
                {

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    char player = board[i, j];

                    if (player == TOKEN_RED)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (player == TOKEN_YELLOW)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }


                    Console.Write("({0})", board[i, j]);

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(" ");

                }

                cursorTop++;
                PrintBorder(cursorLeft, cursorTop);

            }

            cursorTop++;
            PrintBorder(cursorLeft, cursorTop);
        }

        private static void PrintBorder(int cursorLeft, int cursorTop)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ".PadRight(29));
        }

        public void DropToken(int colJ, char player, int speed, int cursorLeft = 25, int cursorTop = 10)
        {

            int row = 0;

            while (HasSpace(row, colJ))
            {
                board[row, colJ] = player;
                PrintGameBoard(cursorLeft, cursorTop);
                Thread.Sleep(speed);

                if (HasSpace(row + 1, colJ))
                {
                    board[row, colJ] = space;
                }

                row++;
            }

        }

        public bool HasWon()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    if (board[i, j] != space)
                    {

                        if (Connected4Horizontally(i, j, board[i, j]))
                        {
                            return true;
                        }

                        if (Connected4Vertically(i, j, board[i, j]))
                        {
                            return true;
                        }

                        if (Connected4DiagonallyUp(i, j, board[i, j]))
                        {
                            return true;
                        }

                        if (Connected4DiagonallyDown(i, j, board[i, j]))
                        {
                            return true;
                        }

                    }
                }
            }

            return false;
        }

        public bool HasTie()
        {
            for (int j = 0; j < Column; j++)
            {
                if (board[0, j] == space)
                {
                    return false;
                }
            }

            return true;
        }


        // private methods
        private void PrepareBoard()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    board[i, j] = space;
                }
            }
        }

        private bool Connected(int rowI, int colJ, char player)
        {
            if (rowI >= board.GetLength(0) || colJ >= board.GetLength(1) || rowI < 0)
            {
                return false;
            }

            return board[rowI, colJ] == player;
        }

        private bool Connected4Horizontally(int rowI, int colJ, char player)
        {
            for (int j = colJ + 1; j < colJ + CONNECTED_NUM; j++)
            {
                if (!Connected(rowI, j, player))
                {
                    return false;
                }
            }

            return true;
        }

        private bool Connected4Vertically(int rowI, int colJ, char player)
        {
            for (int i = rowI + 1; i < rowI + CONNECTED_NUM; i++)
            {
                if (!Connected(i, colJ, player))
                {
                    return false;
                }
            }

            return true;

        }

        private bool Connected4DiagonallyDown(int rowI, int colJ, char player)
        {
            for (int i = rowI + 1, j = colJ + 1; i < rowI + CONNECTED_NUM; i++, j++)
            {
                if (!Connected(i, j, player))
                {
                    return false;
                }
            }

            return true;
        }

        private bool Connected4DiagonallyUp(int rowI, int colJ, char player)
        {
            for (int i = rowI - 1, j = colJ + 1; i > rowI - CONNECTED_NUM; i--, j++)
            {
                if (!Connected(i, j, player))
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasSpace(int rowI, int colJ)
        {
            if (rowI >= Row || rowI < 0 || colJ < 0 || colJ >= Column)
            {
                return false;
            }

            return board[rowI, colJ] == space;
        }

    }
}
