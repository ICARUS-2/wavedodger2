﻿using System;
using static System.Console;

namespace WaveDodger2
{
    class Player
    {
        public const int THREE_LIVES_REMAINING = 3;
        public const int TWO_LIVES_REMAINING = 2;
        public const int ONE_LIFE_REMAINING = 1;

        public const char DEFAULT_PLAYER_CHAR = '0';
        private char _playerChar;

        private const int DEFAULT_SPEED = 1;
        public const int DEFAULT_STARTING_LIVES = 3;
        private const int DEFAULT_X_POS = 2;
        private const int DEFAULT_Y_POS = 2;
        private int _speed;
        private int _xPos;
        private int _yPos;
        private int _oldXPos;
        private int _oldYPos;
        private int _startingLives;
        private int _livesRemaining;
        private int _coinsCollected;

        public const ConsoleColor DEFAULT_FORE_COLOR = ConsoleColor.Green;
        public const ConsoleColor DEFAULT_BACK_COLOR = ConsoleColor.Magenta;
        private ConsoleColor _playerForeColor;
        private ConsoleColor _playerBackColor;

        public const ConsoleKey DEFAULT_KEY_UP = ConsoleKey.W;
        public const ConsoleKey DEFAULT_KEY_DOWN = ConsoleKey.S;
        public const ConsoleKey DEFAULT_KEY_LEFT = ConsoleKey.A;
        public const ConsoleKey DEFAULT_KEY_RIGHT = ConsoleKey.D;
        private ConsoleKey _keyUp;
        private ConsoleKey _keyDown;
        private ConsoleKey _keyLeft;
        private ConsoleKey _keyRight;

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

        #region//EXTERNAL METHODS
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

        public void DrawDirectly()
        {
            ForegroundColor = ForeColor;
            BackgroundColor = BackColor;
            SetCursorPosition(XPos, YPos);
            WriteLine(PlayerChar);
            ResetColor();
        }

        public void InitializePosition(GameArea area)
        {
            XPos = (area.BorderWidth * 2 + area.Width) / 2;
            YPos = area.Height / 2;
            InitializeOnScreen();
        }


        public void LoseLife()
        {
            LivesRemaining--;
        }

        public void ResetLives()
        {
            LivesRemaining = StartingLives;
        }

        public void ResetPlayerCoinCount()
        {
            CoinsCollected = 0;
        }

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

        public bool CheckEnemyCollision(Enemy[] enemies)
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
        private void InitializeOnScreen()
        {
            SetCursorPosition(XPos, YPos);
            ForegroundColor = ForeColor;
            BackgroundColor = BackColor;
            Write(PlayerChar);
            ResetColor();
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
