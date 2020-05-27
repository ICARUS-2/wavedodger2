using System;
using static System.Console;

namespace WaveDodger2
{
    class Player
    {
        private const char DEFAULT_PLAYER_CHAR = 'X';
        private char _playerChar;

        private const int DEFAULT_SPEED = 1;
        private const int DEFAULT_STARTING_LIVES = 3;
        private const int DEFAULT_X_POS = 2;
        private const int DEFAULT_Y_POS = 2;
        private int _speed;
        private int _xPos;
        private int _yPos;
        private int _oldXPos;
        private int _oldYPos;
        private int _startingLives;
        private int _livesRemaining;

        private const ConsoleColor DEFAULT_FORE_COLOR = ConsoleColor.Green;
        private const ConsoleColor DEFAULT_BACK_COLOR = ConsoleColor.Magenta;
        private ConsoleColor _foreColor;
        private ConsoleColor _backColor;

        private const ConsoleKey DEFAULT_KEY_UP = ConsoleKey.W;
        private const ConsoleKey DEFAULT_KEY_DOWN = ConsoleKey.S;
        private const ConsoleKey DEFAULT_KEY_LEFT = ConsoleKey.A;
        private const ConsoleKey DEFAULT_KEY_RIGHT = ConsoleKey.D;
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
            ForeColor = DEFAULT_FORE_COLOR;
            BackColor = DEFAULT_BACK_COLOR;
            KeyUp = DEFAULT_KEY_UP;
            KeyDown = DEFAULT_KEY_DOWN;
            KeyLeft = DEFAULT_KEY_LEFT;
            KeyRight = DEFAULT_KEY_RIGHT;
        }

        #region//METHODS
        public void Move(ConsoleKey userKeyPress)
        {
            OldXPos = XPos;
            OldYPos = YPos;

            if (userKeyPress == KeyUp) //decrease Y coord
            {
                YPos -= Speed;
            }

            if (userKeyPress == KeyDown) //increase Y coord
            {
                YPos += Speed;
            }

            if (userKeyPress == KeyLeft) //decrease X coord
            {
                XPos -= Speed;
            }

            if (userKeyPress == KeyRight) //increase X coord
            {
                XPos += Speed;
            }
        }

        public void Draw()
        {
            if (OldXPos != XPos || OldYPos != YPos)
            {
                SetCursorPosition(OldXPos, OldYPos);
                WriteLine(" ");
                SetCursorPosition(XPos, YPos);
                WriteLine(PlayerChar);
            }
        }

        public void LoseLife()
        {
            LivesRemaining--;
        }

        public void ResetLives()
        {
            LivesRemaining = StartingLives;
        }

        public bool Collide(Enemy[] enemies)
        {
            //if players position is equal to the position of an enemy return true
            return true;
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
                    throw new ArgumentException("ERROR: PLAYER SPEED INCREMENT CANNOT BE NEGATIVE", "_speed");
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
                _xPos = value;

                if (_xPos < 0)
                    throw new ArgumentException("ERROR: PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_xPos");
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
                _yPos = value;

                if (_yPos < 0)
                    throw new ArgumentException("ERROR: PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_yPos");
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
                _oldXPos = value;

                if (_oldXPos < 0)
                    throw new ArgumentException("ERROR: PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_oldXPos");
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
                _oldYPos = value;

                if (_oldYPos < 0)
                    throw new ArgumentException("ERROR: PLAYER CANNOT EXIST OUTSIDE OF THE CONSOLE", "_oldYPos");
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
                    throw new ArgumentException("ERROR: PLAYER STARTING LIVES CANNOT BE NEGATIVE", "_startingLives");

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

        public ConsoleColor ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
            }
        }

        public ConsoleColor BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
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
