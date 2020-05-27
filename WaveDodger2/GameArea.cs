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
        private const char DEFAULT_SCREENGRASS_CHAR = '.';
        private const char DEFAULT_BORDER_CHAR = '#';
        private char _screengrassChar; //the character background of the level
        private char _borderChar; //the character border around the level and display

        private const ConsoleColor DEFAULT_SCREENGRASS_FORECOLOR = ConsoleColor.White;
        private const ConsoleColor DEFAULT_SCREENGRASS_BACKCOLOR = ConsoleColor.Black;
        private const ConsoleColor DEFAULT_BORDER_FORECOLOR = ConsoleColor.White;
        private const ConsoleColor DEFAULT_BORDER_BACKCOLOR = ConsoleColor.Black;
        private ConsoleColor _screengrassForeColor; //foreground color of the screen background
        private ConsoleColor _screengrassBackColor; //background color of the screen background
        private ConsoleColor _borderForeColor; //foreground color of the border
        private ConsoleColor _borderBackColor; //background color of the border

        private const int DEFAULT_WIDTH = 100;
        private const int DEFAULT_HEIGHT = 30;
        private int _width;
        private int _height;


        public GameArea()
        {
            ScreengrassChar = DEFAULT_SCREENGRASS_CHAR;
            BorderChar = DEFAULT_BORDER_CHAR;
            ScreengrassForeColor = DEFAULT_SCREENGRASS_FORECOLOR;
            ScreengrassBackColor = DEFAULT_SCREENGRASS_BACKCOLOR;
            BorderForeColor = DEFAULT_BORDER_FORECOLOR;
            BorderBackColor = DEFAULT_BORDER_BACKCOLOR;
            Width = DEFAULT_WIDTH;
            Height = DEFAULT_HEIGHT;
        }

        public void Render()
        {
            SetCursorPosition(0,0);

            //draws top border line
            ForegroundColor = BorderForeColor;
            BackgroundColor = BorderBackColor;
            for (int i = 0; i < Width; i++)
            {
                Write(BorderChar);
            }

            //draws main area
            for (int i = 0; i < Height - 2; i++)
            {
                ForegroundColor = BorderForeColor;
                BackgroundColor = BorderBackColor;
                WriteLine();
                Write(BorderChar);
                for (int j = 0; j < Width - 2; j++)
                {
                    ForegroundColor = ScreengrassForeColor;
                    BackgroundColor = ScreengrassBackColor;
                    Write(ScreengrassChar);
                }
                ForegroundColor = BorderForeColor;
                BackgroundColor = BorderBackColor;
                Write(BorderChar);
            }

            //draws bottom border line
            WriteLine();
            ForegroundColor = BorderForeColor;
            BackgroundColor = BorderBackColor;
            for (int i = 0; i < Width; i++)
            {
                Write(BorderChar);
            }
        }

        #region//PROPERTIES
        public char ScreengrassChar
        {
            get
            {
                return _screengrassChar;
            }
            set
            {
                _screengrassChar = value;
            }
        }

        public char BorderChar
        {
            get
            {
                return _borderChar;
            }
            set
            {
                _borderChar = value;
            }
        }

        public ConsoleColor ScreengrassForeColor
        {
            get
            {
                return _screengrassForeColor;
            }
            set
            {
                _screengrassForeColor = value;
            }
        }

        public ConsoleColor ScreengrassBackColor
        {
            get
            {
                return _screengrassBackColor;
            }
            set
            {
                _screengrassBackColor = value;
            }
        }

        public ConsoleColor BorderForeColor
        {
            get
            {
                return _borderForeColor;
            }
            set
            {
                _borderForeColor = value;
            }
        }

        public ConsoleColor BorderBackColor
        {
            get
            {
                return _borderBackColor;
            }
            set
            {
                _borderBackColor = value;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }    
            set
            {
                if (value < 0)
                    throw new ArgumentException("ERROR: GAME AREA WIDTH CANNOT BE NEGATIVE","_width");

                _width = value;
            }               

        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("ERROR: GAME AREA HEIGHT CANNOT BE NEGATIVE","_height");

                _height = value;
            }
              
        }


        #endregion
    }
}
