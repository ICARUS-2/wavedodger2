using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace WaveDodger2
{
    class Player
    {
        private const char _DEFAULT_PLAYER_CHAR = 'X';
        private char _playerChar;

        private const int _DEFAULT_SPEED = 1;
        private const int _DEFAULT_STARTING_LIVES = 3;
        private const int _DEFAULT_X_POS = 2;
        private const int _DEFAULY_Y_POS = 2;
        private int _speed;
        private int _xPos;
        private int _yPos;
        private int _oldXPos;
        private int _oldYPos;
        private int _startingLives;
        private int _livesRemaining;

        private const ConsoleColor _DEFAULT_FORE_COLOR = ConsoleColor.Green;
        private const ConsoleColor _DEFAULT_BACK_COLOR = ConsoleColor.Magenta;
        private ConsoleColor _foreColor;
        private ConsoleColor _backColor;

        private const ConsoleKey _DEFAULT_KEY_UP = ConsoleKey.W;
        private const ConsoleKey _DEFAULT_KEY_DOWN = ConsoleKey.S;
        private const ConsoleKey _DEFAULT_KEY_LEFT = ConsoleKey.A;
        private const ConsoleKey _DEFAULT_KEY_RIGHT = ConsoleKey.D;
        private ConsoleKey _keyUp;
        private ConsoleKey _keyDown;
        private ConsoleKey _keyLeft;
        private ConsoleKey _keyRight;

        public Player()
        {
            PlayerChar = _DEFAULT_PLAYER_CHAR;
            Speed = _DEFAULT_SPEED;
            XPos = _DEFAULT_X_POS;
            YPos = _DEFAULY_Y_POS;
            StartingLives = _DEFAULT_STARTING_LIVES;
            LivesRemaining = StartingLives;
            ForeColor = _DEFAULT_FORE_COLOR;
            BackColor = _DEFAULT_BACK_COLOR;
            KeyUp = _DEFAULT_KEY_UP;
            KeyDown = _DEFAULT_KEY_DOWN;
            KeyLeft = _DEFAULT_KEY_LEFT;
            KeyRight = _DEFAULT_KEY_RIGHT;
        }


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
                if (value < 0)
                    throw new ArgumentException("ERROR: PLAYER LIVES REMAINING CANNOT BE NEGATIVE", "_livesRemaining");
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

        #region//METHODS
        public void Move(ConsoleKey userKeyPress)
        {

        }

        public void Draw()
        {

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
    }
}
