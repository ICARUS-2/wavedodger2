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
            Collected = DEFAULT_COLLECTED_VALUE;
        }

        #region//EXTERNAL METHODS
       
        public void WriteOnScreen()
        {
            if (!Collected)
            {
                SetCursorPosition(CoinXPos, CoinYPos);
                ForegroundColor = CoinForeColor;
                BackgroundColor = CoinBackColor;
                WriteLine(CoinChar);
                ResetColor();
            }
        }

        public void Collect(Player player)
        {
            Collected = true;
            player.CoinsCollected++;
        }

        public static Coin[] GenerateCoinArray(int numberOfCoins, Random rnd, GameArea area)
        {
            Coin[] coins = new Coin[numberOfCoins];
            int tempX;
            int tempY;

            for (int i = 0; i < coins.Length; i++)
                coins[i] = new Coin();

            coins[0].CoinXPos = rnd.Next(area.LeftLimit, area.RightLimit + 1); 
            coins[0].CoinYPos = rnd.Next(area.UpLimit, area.DownLimit + 1);

            for (int i = 0; i < coins.Length; i++)
            {
                tempX = rnd.Next(area.LeftLimit, area.RightLimit + 1);
                tempY = rnd.Next(area.UpLimit, area.DownLimit + 1);

                for(int j = 0; j < i; j++)
                {
                    if (tempX != coins[j].CoinXPos && tempY != coins[j].CoinYPos)
                    {
                        coins[i].CoinXPos = tempX;
                        coins[i].CoinYPos = tempY;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            return coins;
        }

        public static void Render(Coin[] coins)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                coins[i].WriteOnScreen();
            }
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
