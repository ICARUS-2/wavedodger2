using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace WaveDodger2
{
    class GameArea
    {
        private char screenGrass; //the character background of the level
        private char border; //the character border around the level and display

        private ConsoleColor sgForeColor; //foreground color of the screen background
        private ConsoleColor sgBackColor; //background color of the screen background
        private ConsoleColor borderForeColor; //foreground color of the border
        private ConsoleColor borderBackColor; //background color of the border

        private int width;
        private int height;


        public GameArea()
        {

        }
    }
}
