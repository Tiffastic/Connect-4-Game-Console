using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * The Player struct encapsulates the player's name and token, and keeps
 * track of the player's turn for the Connect 4 Game.
 * 
 * Thuy Nguyen
 * October 2, 2014
 * C# Class
 * 
 */
namespace Connect4
{
    struct Player
    {

        // public properties
        public char Token
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public bool HasTurn
        {
            get;
            set;
        }

        // constructor
        public Player(string name, char token)
            : this()
        {
            Name = name;
            Token = token;
            HasTurn = false;

        }

        // public methods
        public override string ToString()
        {
            return string.Format("{0} {1} move: ", Name, Token);
        }

    }
}
