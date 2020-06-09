using System;
using static System.Console;

namespace WaveDodger2
{
    /// <summary>
    /// Global Summary: Player class defines a user-controlled player in a console game that 
    /// can detect collision with enemies and moved using the assigned keyboard buttons
    /// External Dependencies: Enemy.cs, Coin.cs, GameArea.cs
    /// </summary>
    class Player
    {
        public const int THREE_LIVES_REMAINING = 3;
        public const int TWO_LIVES_REMAINING = 2;
        public const int ONE_LIFE_REMAINING = 1;

        public const char DEFAULT_PLAYER_CHAR = '0'; //Defines the ASCII character that the player is represented by on screen.
        private char _playerChar;

        private const int DEFAULT_SPEED = 1;
        public const int DEFAULT_STARTING_LIVES = 3;
        private const int DEFAULT_X_POS = 2;
        private const int DEFAULT_Y_POS = 2;
        private int _speed; //Defines the speed in console spaces the player moves at.
        private int _xPos; //Defines the horizontal position of the player on the screen.
        private int _yPos; //Defines the vertical position of the player on the screen.
        private int _oldXPos; //The horizontal position of the player that is recorded before the player moves so the previous player icon can be erased.
        private int _oldYPos; //The vertical position of the player that is recorded before the player moves so the previous player icon can be erased.
        private int _startingLives; //The amount of lives that the player starts with.
        private int _livesRemaining; //The amount of lives that the player has left.
        private int _coinsCollected; //The amount of coins the player has collected.

        public const ConsoleColor DEFAULT_FORE_COLOR = ConsoleColor.Green;
        public const ConsoleColor DEFAULT_BACK_COLOR = ConsoleColor.Magenta;
        private ConsoleColor _playerForeColor; //The foreground color of the ASCII character used to represent the player.
        private ConsoleColor _playerBackColor; //The background color of the ASCII character used to represent the player.

        public const ConsoleKey DEFAULT_KEY_UP = ConsoleKey.W;
        public const ConsoleKey DEFAULT_KEY_DOWN = ConsoleKey.S;
        public const ConsoleKey DEFAULT_KEY_LEFT = ConsoleKey.A;
        public const ConsoleKey DEFAULT_KEY_RIGHT = ConsoleKey.D;
        //The keys that are used to move the player around on the screen.
        private ConsoleKey _keyUp;
        private ConsoleKey _keyDown;
        private ConsoleKey _keyLeft;
        private ConsoleKey _keyRight;

        #region//CONSTRUCTORS
        /// <summary>
        /// Sets PlayerChar, Speed, XPos, YPos, StartingLives, ForeColor, BackColor, KeyUp, KeyDown, KeyLeft, 
        /// and KeyRight to their default public constants. LivesRemaining is set equal to StartingLives and 
        /// CoinsCollected is set to 0.
        /// </summary>
        public Player()
        {
            PlayerChar = DEFAULT_PLAYER_CHAR;
            Speed = DEFAULT_SPEED;
            XPos = DEFAULT_X_POS;
            YPos = DEFAULT_Y_POS;
            StartingLives = DEFAULT_STARTING_LIVES;
            LivesRemaining = StartingLives;
            CoinsCollected = 0;
            ForeColor = DEFAULT_FORE_COLOR;
            BackColor = DEFAULT_BACK_COLOR;
            KeyUp = DEFAULT_KEY_UP;
            KeyDown = DEFAULT_KEY_DOWN;
            KeyLeft = DEFAULT_KEY_LEFT;
            KeyRight = DEFAULT_KEY_RIGHT;
        }

        /// <summary>
        /// Speed, XPos, YPos, KeyUp, KeyDown, KeyLeft, and KeyRight are set to their default public constants. 
        /// PlayerChar, StartingLives, ForeColor and BackColor are set to their respective parameters. LivesRemaining
        /// is set equal to StartingLives and CoinsCollected is set to 0.
        /// </summary>
        /// <param name="playerChar_"></param>
        /// <param name="startingLives_"></param>
        /// <param name="playerForeColor_"></param>
        /// <param name="playerBackColor_"></param>
        public Player(char playerChar_, int startingLives_, ConsoleColor playerForeColor_, ConsoleColor playerBackColor_) //advanced editor
        {
            PlayerChar = playerChar_;
            Speed = DEFAULT_SPEED;
            XPos = DEFAULT_X_POS;
            YPos = DEFAULT_Y_POS;
            StartingLives = startingLives_;
            LivesRemaining = StartingLives;
            CoinsCollected = 0;
            ForeColor = playerForeColor_;
            BackColor = playerBackColor_;
            KeyUp = DEFAULT_KEY_UP;
            KeyDown = DEFAULT_KEY_DOWN;
            KeyLeft = DEFAULT_KEY_LEFT;
            KeyRight = DEFAULT_KEY_RIGHT;
        }
        #endregion

        #region//EXTERNAL METHODS
        /// <summary>
        /// Takes the user’s key press and the current game area as arguments, and moves the player by its 
        /// speed in the direction corresponding to the key press. Collision with the outer game area limits 
        /// is checked here too. Returns void.
        /// </summary>
        /// <param name="userKeyPress"></param>
        /// <param name="area"></param>
        public void Move(ConsoleKey userKeyPress, GameArea area)
        {
            OldXPos = XPos;
            OldYPos = YPos;

            //move player up
            if (userKeyPress == KeyUp && YPos != area.UpLimit) //up = decrease Y coord
            {
                YPos -= Speed;
            }

            //move player down
            if (userKeyPress == KeyDown && YPos != area.DownLimit) //down = increase Y coord
            {
                YPos += Speed;
            }

            //move player left
            if (userKeyPress == KeyLeft && XPos != area.LeftLimit) //left = decrease X coord
            {
                XPos -= Speed;
            }

            //move player right
            if (userKeyPress == KeyRight && XPos != area.RightLimit) //right = increase X coord
            {
                XPos += Speed;
            }
        }

        /// <summary>
        /// Takes the current game area as an argument. Erases the old player icon after moving and 
        /// draws the new one with its current X and Y positions. Returns void.
        /// </summary>
        /// <param name="area"></param>
        public void Draw(GameArea area)
        {
            if (OldXPos != XPos || OldYPos != YPos)
            {
                if (OldXPos != 0 && OldYPos != 0)
                {
                    SetCursorPosition(OldXPos, OldYPos);
                    ForegroundColor = area.ScreengrassForeColor;
                    BackgroundColor = area.ScreengrassBackColor;
                    WriteLine(area.ScreengrassChar);
                }

                SetCursorPosition(XPos, YPos);
                ForegroundColor = ForeColor;
                BackgroundColor = BackColor;
                WriteLine(PlayerChar);
                ResetColor();
            }
        }

        /// <summary>
        /// Takes no arguments. Draws the player in its current X and Y pos (no erasing any previous icons). Returns void.
        /// </summary>
        public void DrawDirectly()
        {
            ForegroundColor = ForeColor;
            BackgroundColor = BackColor;
            SetCursorPosition(XPos, YPos);
            WriteLine(PlayerChar);
            ResetColor();
        }

        /// <summary>
        /// Takes current game area as an argument and uses its dimensions to initialize the player’s 
        /// position in the center of the play area. Returns void.
        /// </summary>
        /// <param name="area"></param>
        public void InitializePosition(GameArea area)
        {
            XPos = (area.BorderWidth * 2 + area.Width) / 2;
            YPos = area.Height / 2;
            DrawDirectly();
        }

        /// <summary>
        /// Takes no arguments. Reduces player’s LivesRemaining by 1 and returns void.
        /// </summary>
        public void LoseLife()
        {
            LivesRemaining--;
        }

        /// <summary>
        /// Takes no arguments. Resets player’s LivesRemaining back to its StartingLives property and 
        /// resets its CoinsCollected property back to 0.
        /// </summary>
        public void ResetStats()
        {
            LivesRemaining = StartingLives;
            CoinsCollected = 0;
        }

        /// <summary>
        /// Takes an array of enemies, a Player class instance, and a cycleCollision boolean that is used 
        /// externally to check if the player has had an enemy collision for that run of the game loop. If 
        /// the player’s lives are greater than 0, draw the player directly (so it doesn’t get hidden by the 
        /// enemy), and check if the player’s coordinates match those of any enemies in the array. If so, the 
        /// player loses a life (see LoseLife()). Returns void as collision check is done internally with a 
        /// reference boolean (see CheckEnemyCollision(Enemy[] enemies)).
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="player"></param>
        /// <param name="cycleCollision"></param>
        public void HitTest(Enemy[] enemies, Player player, ref bool cycleCollision)
        {
            if (player.LivesRemaining > 0)
            {
                cycleCollision = CheckEnemyCollision(enemies);
                DrawDirectly();

                if (cycleCollision)
                    LoseLife();
            }
        }

        /// <summary>
        /// Checks if the player’s position matches that of any coins in the game. If so, that coin’s 
        /// Collect method is called (see Coin.Collect(Player player)). Returns void as coin stat modification 
        /// is done internally through the Coin.Collect(Player player) method.
        /// </summary>
        /// <param name="coins"></param>
        public void CheckCoinCollision(Coin[] coins)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                if ((XPos == coins[i].CoinXPos) && (YPos == coins[i].CoinYPos) && (!coins[i].Collected))
                    coins[i].Collect(this);
            }
        }

        #endregion

        #region//INTERNAL METHODS
        /// <summary>
        /// Takes an enemy array as an argument, and checks to see if the player’s position matches that of any
        /// enemies. If so, console beeps and returns true. If not, returns false.
        /// </summary>
        /// <param name="enemies"></param>
        /// <returns></returns>
        private bool CheckEnemyCollision(Enemy[] enemies)
        {
            foreach (Enemy e in enemies)
            {
                if ((XPos == e.EnemyXPos && YPos == e.EnemyYPos))
                {
                    Beep();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region//PROPERTIES

        public char PlayerChar
        {
            get
            {
                return _playerChar;
            }
            set
            {
                _playerChar = value;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("PLAYER SPEED INCREMENT CANNOT BE NEGATIVE", "_speed");
                _speed = value;
            }
        }

        public int XPos
        {
            get
            {
                return _xPos;
            }
            set
            {
                if (_xPos < 0)
                    throw new ArgumentException("PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_xPos");

                _xPos = value;
            }
        }

        public int YPos
        {
            get
            {
                return _yPos;
            }
            set
            {
                if (_yPos < 0)
                    throw new ArgumentException("PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_yPos");

                _yPos = value;
            }
        }


        public int OldXPos
        {
            get
            {
                return _oldXPos;
            }
            set
            {
                if (_oldXPos < 0)
                    throw new ArgumentException("PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_oldXPos");

                _oldXPos = value;
            }
        }

        public int OldYPos
        {
            get
            {
                return _oldYPos;
            }
            set
            {
                if (_oldYPos < 0)
                    throw new ArgumentException("PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_oldYPos");

                _oldYPos = value;
            }
        }

        public int StartingLives
        {
            get
            {
                return _startingLives;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("PLAYER STARTING LIVES CANNOT BE NEGATIVE", "_startingLives");

                _startingLives = value;
            }
        }

        public int LivesRemaining
        {
            get
            {
                return _livesRemaining;
            }
            set
            {
                if (LivesRemaining < 0)
                    throw new ArgumentException("PLAYER CANNOT HAVE NEGATIVE LIVES REMAINING","_livesRemaining");

                _livesRemaining = value;
            }
        }

        public int CoinsCollected
        {
            get
            {
                return _coinsCollected;
            }
            set
            {
                _coinsCollected = value;

                if (_coinsCollected < 0)
                    throw new ArgumentException("PLAYER CANNOT HAVE NEGATIVE COINS COLLECTED","_coinsCollected");
            }
        }

        public ConsoleColor ForeColor
        {
            get
            {
                return _playerForeColor;
            }
            set
            {
                _playerForeColor = value;
            }
        }

        public ConsoleColor BackColor
        {
            get
            {
                return _playerBackColor;
            }
            set
            {
                _playerBackColor = value;
            }
        }

        public ConsoleKey KeyUp
        {
            get
            {
                return _keyUp;
            }
            set
            {
                _keyUp = value;
            }
        }

        public ConsoleKey KeyDown
        {
            get
            {
                return _keyDown;
            }
            set
            {
                _keyDown = value;
            }
        }

        public ConsoleKey KeyLeft
        {
            get
            {
                return _keyLeft;
            }
            set
            {
                _keyLeft = value;
            }
        }

        public ConsoleKey KeyRight
        {
            get
            {
                return _keyRight;
            }
            set
            {
                _keyRight = value;
            }
        }


        #endregion
    }
}
