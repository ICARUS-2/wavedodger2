using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace WaveDodger2
{
    class Coin
    {
        private const char DEFAULT_COIN_CHAR = 'o';
        private char _coinChar;

        private const ConsoleColor DEFAULT_COIN_FORECOLOR = ConsoleColor.Yellow;
        private const ConsoleColor DEFAULT_COIN_BACKCOLOR = ConsoleColor.DarkYellow;
        private ConsoleColor _coinForeColor;
        private ConsoleColor _coinBackColor;
        private int _coinXPos;
        private int _coinYPos;

        private const bool DEFAULT_COLLECTED_VALUE = false;
        private bool _collected;

        public Coin()
        {
            CoinChar = DEFAULT_COIN_CHAR;
            CoinForeColor = DEFAULT_COIN_FORECOLOR;
            CoinBackColor = DEFAULT_COIN_BACKCOLOR;
            CoinXPos = 0;
            CoinYPos = 0;
        }

        #region//EXTERNAL METHODS
        public void Initialize(GameArea area)
        {
            Random rnd = new Random();
            CoinXPos = rnd.Next(area.LeftLimit, area.RightLimit + 1);
            CoinYPos = rnd.Next(area.UpLimit, area.DownLimit + 1);
        }
        #endregion

        #region//PROPERTIES
        public char CoinChar
        {
            get
            {
                return _coinChar;
            }
            set
            {
                _coinChar = value;
            }
        }

        public ConsoleColor CoinForeColor
        {
            get
            {
                return _coinForeColor;
            }
            set
            {
                _coinForeColor = value;
            }
        }

        public ConsoleColor CoinBackColor
        {
            get
            {
                return _coinBackColor;
            }
            set
            {
                _coinBackColor = value;
            }
        }

        public int CoinXPos
        {
            get
            {
                return _coinXPos;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("COIN X POSITION CANNOT BE NEGATIVE","_coinXPos");

                _coinXPos = value;
            }
        }

        public int CoinYPos
        {
            get
            {
                return _coinYPos;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("COIN Y POSITION CANNOT BE NEGATIVE", "_coinYPos");

                _coinYPos = value;
            }
        }

        public bool Collected
        {
            get
            {
                return _collected;
            }
            set
            {
                _collected = value;
            }
        }


        #endregion
    }
}
