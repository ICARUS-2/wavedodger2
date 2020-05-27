using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace WaveDodger2
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player();
            GameArea area = new GameArea();
            area.Render();
            ConsoleKey userKey;
            while (1 < 2)
            {
                while (KeyAvailable)
                {
                    userKey = ReadKey(true).Key;
                    player1.Move(userKey);
                    player1.Draw();
                }
            }
        }
    }
}
