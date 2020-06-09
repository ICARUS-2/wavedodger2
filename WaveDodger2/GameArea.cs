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
    /// <summary>
    /// GameArea class defines a working play area for a console game, complete with
    /// limit calculations and full cosmetic and dimensional customizability
    /// External Dependencies: Player.cs, Coin.cs
    /// </summary>
    class GameArea
    {
        public const char DEFAULT_SCREENGRASS_CHAR = '.';
        public const char DEFAULT_BORDER_CHAR = '█';
        private char _screengrassChar; //the character background of the level
        private char _borderChar; //the character border around the level
        
        public const ConsoleColor DEFAULT_SCREENGRASS_FORECOLOR = ConsoleColor.White;
        public const ConsoleColor DEFAULT_SCREENGRASS_BACKCOLOR = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_BORDER_FORECOLOR = ConsoleColor.White;
        public const ConsoleColor DEFAULT_BORDER_BACKCOLOR = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_MORE_LIVES_REMAINING_COLOR = ConsoleColor.Green;
        public const ConsoleColor DEFAULT_TWO_LIVES_REMAINING_COLOR = ConsoleColor.DarkYellow;
        public const ConsoleColor DEFAULT_ONE_LIFE_REMAINING_COLOR = ConsoleColor.Red;
        private ConsoleColor _screengrassForeColor; //foreground color of the screen background
        private ConsoleColor _screengrassBackColor; //background color of the screen background
        private ConsoleColor _borderForeColor; //foreground color of the border
        private ConsoleColor _borderBackColor; //background color of the border
        private ConsoleColor _moreLivesRemainingColor; //color of the display if lives remaining is three or more
        private ConsoleColor _twoLivesRemainingColor; //color of the display if player has two lives remaining
        private ConsoleColor _oneLifeRemainingColor; //color of the display if user is on their last life
       
        public const int DEFAULT_WIDTH = 50;
        public const int DEFAULT_HEIGHT = 30;
        public const int DEFAULT_BORDER_WIDTH = 15;
        private int _width; //width of the main play area NOT COUNTING THE BORDER SPACE
        private int _height; //height of the screen
        private int _borderWidth; //width of the blank areas flanking both sides of the level

        private int _upLimit; //lowest possible y coord the player can hit
        private int _downLimit; //highest possible y coord the player can hit
        private int _leftLimit; //lowest possible x coord the player can hit
        private int _rightLimit; //highest possible x coord the player can hit

        #region//CONSTRUCTORS
        /// <summary>
        /// ScreengrassChar, BorderChar, ScreengrassForeColor, ScreengrassBackColor, BorderForeColor, 
        /// BorderBackColor, Width, Height, BorderWidth, MoreLivesRemainingColor, TwoLivesRemainingColor, 
        /// OneLifeRemainingColor are all set to their public constant defaults. Limits are calculated (see CalculateLimits()).
        /// </summary>
        public GameArea() //default settings
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
            MoreLivesRemainingColor = DEFAULT_MORE_LIVES_REMAINING_COLOR;
            TwoLivesRemainingColor = DEFAULT_TWO_LIVES_REMAINING_COLOR;
            OneLifeRemainingColor = DEFAULT_ONE_LIFE_REMAINING_COLOR;
            CalculateLimits();
        }

        /// <summary>
        /// ScreengrassChar, BorderChar, ScreengrassForeColor, ScreengrassBackColor, BorderForeColor, BorderBackColor, 
        /// BorderWidth, MoreLivesRemainingColor, TwoLivesRemainingColor, OneLifeRemainingColor are all set to their public 
        /// constant defaults. Width and Height are set to their respective parameters. Limits are calculated (see CalculateLimits()).
        /// </summary>
        /// <param name="width_"></param>
        /// <param name="height_"></param>
        public GameArea(int width_, int height_) //simple editor
        {
            ScreengrassChar = DEFAULT_SCREENGRASS_CHAR;
            BorderChar = DEFAULT_BORDER_CHAR;
            ScreengrassForeColor = DEFAULT_SCREENGRASS_FORECOLOR;
            ScreengrassBackColor = DEFAULT_SCREENGRASS_BACKCOLOR;
            BorderForeColor = DEFAULT_BORDER_FORECOLOR;
            BorderBackColor = DEFAULT_BORDER_BACKCOLOR;
            Width = width_; //only thing that can be changed is the width and height in the simple editor
            Height = height_;
            BorderWidth = DEFAULT_BORDER_WIDTH;
            MoreLivesRemainingColor = DEFAULT_MORE_LIVES_REMAINING_COLOR;
            TwoLivesRemainingColor = DEFAULT_TWO_LIVES_REMAINING_COLOR;
            OneLifeRemainingColor = DEFAULT_ONE_LIFE_REMAINING_COLOR;
            CalculateLimits();
        }

        /// <summary>
        ///  ScreengrassChar, BorderChar, ScreengrassForeColor, ScreengrassBackColor, BorderForeColor, BorderBackColor, Width, 
        ///  Height, BorderWidth are all set to their respective parameter values. MoreLivesRemainingColor, TwoLivesRemainingColor, 
        ///  OneLifeRemainingColor are all set to their public constant defaults. Limits are calculated (see CalculateLimits()).
        /// </summary>
        /// <param name="screengrassChar_"></param>
        /// <param name="borderChar_"></param>
        /// <param name="screengrassForeColor_"></param>
        /// <param name="screengrassBackColor_"></param>
        /// <param name="borderForeColor_"></param>
        /// <param name="borderBackColor_"></param>
        /// <param name="width_"></param>
        /// <param name="height_"></param>
        /// <param name="borderWidth_"></param>
        public GameArea(char screengrassChar_, char borderChar_, ConsoleColor screengrassForeColor_, ConsoleColor screengrassBackColor_, ConsoleColor borderForeColor_, ConsoleColor borderBackColor_, int width_, int height_, int borderWidth_) //advanced editor
        {
            ScreengrassChar = screengrassChar_;
            BorderChar = borderChar_;
            ScreengrassForeColor = screengrassForeColor_;
            ScreengrassBackColor = screengrassBackColor_;
            BorderForeColor = borderForeColor_;
            BorderBackColor = borderBackColor_;
            Width = width_;
            Height = height_;
            BorderWidth = borderWidth_;
            MoreLivesRemainingColor = DEFAULT_MORE_LIVES_REMAINING_COLOR;
            TwoLivesRemainingColor = DEFAULT_TWO_LIVES_REMAINING_COLOR;
            OneLifeRemainingColor = DEFAULT_ONE_LIFE_REMAINING_COLOR;
            CalculateLimits();
        }
        #endregion

        #region//EXTERNAL METHODS

        /// <summary>
        /// Takes no arguments. Draws the play area in the console based on its dimensions, characters and colors. Returns void.
        /// </summary>
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

        /// <summary>
        /// Takes the index of the current level in the levels array (see Program.cs.levels), an instance of the player 
        /// object and an array of coins, and uses it to display the current round, number of coins collected, total number
        /// of coins, and the player’s lives remaining. Changes colors based on how many lives the Player instance’s backing 
        /// field has (see LIVES_REMAINING ConsoleColors). Returns void.
        /// </summary>
        /// <param name="currentLevelIndex"></param>
        /// <param name="player"></param>
        /// <param name="coins"></param>
        public void UpdateDisplay(int currentLevelIndex, Player player, Coin[] coins)
        {
            if (player.LivesRemaining >= Player.THREE_LIVES_REMAINING)
                ForegroundColor = MoreLivesRemainingColor;

            if (player.LivesRemaining == Player.TWO_LIVES_REMAINING)
                ForegroundColor = TwoLivesRemainingColor;

            if (player.LivesRemaining == Player.ONE_LIFE_REMAINING)
                ForegroundColor = OneLifeRemainingColor;

            int heightOffset = 3;
            int space = 2;
            string displayRound = String.Format("Round {0}", currentLevelIndex + 1);
            string displayCoins = String.Format("Coins Collected: {0} / {1}", player.CoinsCollected, coins.Length);
            string displayLives = String.Format("Lives Remaining: {0}", player.LivesRemaining);
            int displayX = (Width + 2*BorderWidth) / 2;
            int displayY = Height + heightOffset;

            SetCursorPosition(displayX - (displayRound.Length / 2), displayY);
            WriteLine(displayRound);
            displayY += space;

            SetCursorPosition(displayX - (displayCoins.Length / 2), displayY);
            WriteLine(displayCoins);
            displayY += space;

            SetCursorPosition(displayX - (displayLives.Length / 2), displayY);
            WriteLine(displayLives);

            ResetColor();
        }

        #endregion

        #region//INTERNAL METHODS
        /// <summary>
        /// Takes no arguments. Sets the UpLimit, DownLimit, LeftLimit, and RightLimit properties based on the dimensions
        /// of the Game Area itself, ensuring the player cannot clip outside of the game area itself. Returns void.
        /// </summary>
        private void CalculateLimits()
        {
            UpLimit = 1;
            DownLimit = Height - 2;
            LeftLimit = BorderWidth;
            RightLimit = BorderWidth + Width - 1;
        }

        #endregion

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
