using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * This Program starts a new GameApp with the names of the players
 * initialized in the constructor.  The Program runs the GameApp
 * with a chosen speed of x milliseconds.
 * 
 */
namespace Connect4
{
    class Program
    {

        static void Main(string[] args)
        {
            GameApp app = new GameApp("Thuy", "Friend");
            app.Top = 0;
            app.Left = 0;
            app.StartGame(speed: 510); 
           
            Console.ReadLine();

        }

    }
}
