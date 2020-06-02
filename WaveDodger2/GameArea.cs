﻿using System;
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
        //consts labeled as DEFAULT are used in the default constructor
        private const char DEFAULT_SCREENGRASS_CHAR = '.';
        private const char DEFAULT_BORDER_CHAR = '█';
        private char _screengrassChar; //the character background of the level
        private char _borderChar; //the character border around the level and display
        
        private const ConsoleColor DEFAULT_SCREENGRASS_FORECOLOR = ConsoleColor.Blue;
        private const ConsoleColor DEFAULT_SCREENGRASS_BACKCOLOR = ConsoleColor.Black;
        private const ConsoleColor DEFAULT_BORDER_FORECOLOR = ConsoleColor.DarkBlue;
        private const ConsoleColor DEFAULT_BORDER_BACKCOLOR = ConsoleColor.Black;
        private const ConsoleColor DEFAULT_MORE_LIVES_REMAINING_COLOR = ConsoleColor.Green;
        private const ConsoleColor DEFAULT_TWO_LIVES_REMAINING_COLOR = ConsoleColor.DarkYellow;
        private const ConsoleColor DEFAULT_ONE_LIFE_REMAINING_COLOR = ConsoleColor.Red;
        private ConsoleColor _screengrassForeColor; //foreground color of the screen background
        private ConsoleColor _screengrassBackColor; //background color of the screen background
        private ConsoleColor _borderForeColor; //foreground color of the border
        private ConsoleColor _borderBackColor; //background color of the border
        private ConsoleColor _moreLivesRemainingColor; //color of the display if lives remaining is three or more
        private ConsoleColor _twoLivesRemainingColor; //color of the display if player has two lives remaining
        private ConsoleColor _oneLifeRemainingColor; //color of the display if user is on their last life
       
        private const int DEFAULT_WIDTH = 50;
        private const int DEFAULT_HEIGHT = 30;
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
            MoreLivesRemainingColor = DEFAULT_MORE_LIVES_REMAINING_COLOR;
            TwoLivesRemainingColor = DEFAULT_TWO_LIVES_REMAINING_COLOR;
            OneLifeRemainingColor = DEFAULT_ONE_LIFE_REMAINING_COLOR;
            Width = DEFAULT_WIDTH;
            Height = DEFAULT_HEIGHT;
            BorderWidth = DEFAULT_BORDER_WIDTH;
            CalculateLimits();
        }

        private void CalculateLimits()
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

        public void UpdateDisplay(Player player, Coin[] coins)
        {
            if (player.LivesRemaining >= Player.THREE_LIVES_REMAINING)
                ForegroundColor = MoreLivesRemainingColor;

            if (player.LivesRemaining == Player.TWO_LIVES_REMAINING)
                ForegroundColor = TwoLivesRemainingColor;

            if (player.LivesRemaining == Player.ONE_LIFE_REMAINING)
                ForegroundColor = OneLifeRemainingColor;

            int heightOffset = 3;
            string displayRound = String.Format("Round");
            string displayCoins = String.Format("Coins Collected: {0} / {1}", player.CoinsCollected, coins.Length);
            string displayLives = String.Format("Lives Remaining: {0}", player.LivesRemaining);
            int displayX = (Width + 2*BorderWidth) / 2;
            int displayY = Height + heightOffset;

            SetCursorPosition(displayX - (displayRound.Length / 2), displayY);
            WriteLine(displayRound);
            displayY += 2;

            SetCursorPosition(displayX - (displayCoins.Length / 2), displayY);
            WriteLine(displayCoins);
            displayY += 2;

            SetCursorPosition(displayX - (displayLives.Length / 2), displayY);
            WriteLine(displayLives);

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

        public ConsoleColor MoreLivesRemainingColor
        {
            get
            {
                return _moreLivesRemainingColor;
            }
            set
            {
                _moreLivesRemainingColor = value;
            }
        }

        public ConsoleColor TwoLivesRemainingColor
        {
            get
            {
                return _twoLivesRemainingColor;
            }
            set
            {
                _twoLivesRemainingColor = value;
            }
        }

        public ConsoleColor OneLifeRemainingColor
        {
            get
            {
                return _oneLifeRemainingColor;
            }
            set
            {
                _oneLifeRemainingColor = value;
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
