using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WaveDodger2
{
    class GameArea
    {

        private const char DEFAULT_SCREENGRASS_CHAR = '█';
        private const char DEFAULT_BORDER_CHAR = '█';
        private char _screengrassChar; //the character background of the level
        private char _borderChar; //the character border around the level and display
        
        private const ConsoleColor DEFAULT_SCREENGRASS_FORECOLOR = ConsoleColor.Blue;
        private const ConsoleColor DEFAULT_SCREENGRASS_BACKCOLOR = ConsoleColor.Green;
        private const ConsoleColor DEFAULT_BORDER_FORECOLOR = ConsoleColor.White;
        private const ConsoleColor DEFAULT_BORDER_BACKCOLOR = ConsoleColor.Black;
        private ConsoleColor _screengrassForeColor; //foreground color of the screen background
        private ConsoleColor _screengrassBackColor; //background color of the screen background
        private ConsoleColor _borderForeColor; //foreground color of the border
        private ConsoleColor _borderBackColor; //background color of the border

        private const int DEFAULT_WIDTH = 100;
        private const int DEFAULT_HEIGHT = 35;
        private const int DEFAULT_BORDER_WIDTH = 15;
        private int _width; //width of the main play area NOT COUNTING THE BORDER SPACE
        private int _height; //height of the screen
        private int _borderWidth; //width of the blank areas flanking both sides of the level

        private int _upLimit; //lowest possible y coord the player can hit
        private int _downLimit; //highest possible y coord the player can hit
        private int _leftLimit; //lowest possible x coord the player can hit
        private int _rightLimit; //highest possible x coord the player can hit


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
            BorderWidth = DEFAULT_BORDER_WIDTH;
            SetLimits();
        }

        private void SetLimits()
        {
            UpLimit = 1;
            DownLimit = Height - 2;
            LeftLimit = BorderWidth;
            RightLimit = BorderWidth + Width - 1;
        }

        public void Render()
        {
            SetCursorPosition(0, 0);
            //draws top border line
            ForegroundColor = BorderForeColor;
            BackgroundColor = BorderBackColor;
            for (int i = 0; i < (2 * BorderWidth + Width); i++)
            {
                Write(BorderChar);
            }

            //draws the area in between the top and bottom border lines
            for (int i = 0; i < Height - 2; i++)
            {
                WriteLine();
                //draws the left border
                ForegroundColor = BorderForeColor;
                BackgroundColor = BorderBackColor;
                Write(BorderChar);
                BackgroundColor = DEFAULT_BORDER_BACKCOLOR;
                for (int j = 0; j < BorderWidth - 2; j++)
                {
                    Write(" ");
                }
                ForegroundColor = BorderForeColor;
                BackgroundColor = BorderBackColor;
                Write(BorderChar);

                ForegroundColor = ScreengrassForeColor;
                BackgroundColor = ScreengrassBackColor;
                //draws the main game area
                for (int j = 0; j < Width; j++)
                {
                    Write(ScreengrassChar);
                }
                //draws the right border
                ForegroundColor = BorderForeColor;
                BackgroundColor = BorderBackColor;
                Write(BorderChar);
                BackgroundColor = DEFAULT_BORDER_BACKCOLOR;
                for (int j = 0; j < BorderWidth - 2; j++)
                {
                    Write(" ");
                }
                ForegroundColor = BorderForeColor;
                BackgroundColor = BorderBackColor;
                Write(BorderChar);
                ResetColor();
            }

            WriteLine();
            //draws the bottom border line
            ForegroundColor = BorderForeColor;
            BackgroundColor = BorderBackColor;
            for (int i = 0; i < (2 * BorderWidth + Width); i++)
            {
                Write(BorderChar);
            }
            ResetColor();
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
                    throw new ArgumentException("GAME AREA WIDTH CANNOT BE NEGATIVE","_width");

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
                    throw new ArgumentException("GAME AREA HEIGHT CANNOT BE NEGATIVE", "_height");

                _height = value;
            }

        }

        public int BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("BORDER WIDTH CANNOT BE NEGATIVE","_borderWidth");

                _borderWidth = value;
            }
        }

        public int UpLimit
        {
            get
            {
                return _upLimit;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("TOP COLLISION LIMIT CANNOT BE NEGATIVE","_upLimit");

                _upLimit = value;
            }
        }

        public int DownLimit
        {
            get
            {
                return _downLimit;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("BOTTOM COLLISION LIMIT CANNOT BE NEGATIVE","_downLimit");

                _downLimit = value;
            }
        }

        public int LeftLimit
        {
            get
            {
                return _leftLimit;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("LEFT COLLISION LIMIT CANNOT BE NEGATIVE","_leftLimit");

                _leftLimit = value;
            }
        }

        public int RightLimit
        {
            get
            {
                return _rightLimit;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("RIGHT COLLISION LIMIT CANNOT BE NEGATIVE","_rightLimit");

                _rightLimit = value;
            }
        }



        #endregion
    }
}
