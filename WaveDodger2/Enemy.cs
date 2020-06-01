using System;
using static System.Console;

namespace WaveDodger2
{
    class Enemy
    {
        private int _enemyXPos;
        private int _enemyYPos;

        private const Sides DEFAULT_EMERGING_SIDE = Sides.Top;
        private Sides _emergingSide;

        private const char DEFAULT_ENEMY_CHAR = 'X';
        private char _enemyChar;

        private const ConsoleColor DEFAULT_ENEMY_FORECOLOR = ConsoleColor.DarkRed;
        private const ConsoleColor DEFAULT_ENEMY_BACKCOLOR = ConsoleColor.Black;
        private ConsoleColor _enemyForeColor;
        private ConsoleColor _enemyBackChar;
        public enum Sides
        {
            Top = 1,
            Right = 2,
            Bottom = 3,
            Left = 4,
        }

        public Enemy()
        {
            EnemyXPos = 0;
            EnemyYPos = 0;
            EmergingSide = DEFAULT_EMERGING_SIDE;
            EnemyChar = DEFAULT_ENEMY_CHAR;
            EnemyForeColor = DEFAULT_ENEMY_FORECOLOR;
            EnemyBackColor = DEFAULT_ENEMY_BACKCOLOR;
        }

        #region//EXTERNAL METHODS
        public static Enemy[] GetArrayOfEnemies(int numberOfEnemies, GameArea area)
        {
            Enemy[] enemies = new Enemy[numberOfEnemies];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy();
            }

            return enemies;
        }

        public static void ChangeSide(Enemy[] enemies, GameArea area, Random rnd)
        {
            //Random rnd = new Random();
            //enemies[0].EmergingSide = (Sides)rnd.Next((int)Sides.Top, (int)Sides.Left + 1); //Generates a number from 1-4 representing the side the enemies will scroll across the screen from
            enemies[0].EmergingSide = Sides.Left;
            for (int i = 0; i < enemies.Length; i++)
            {
                switch (enemies[0].EmergingSide)
                {
                    case Sides.Top: 
                        enemies[i].EnemyXPos = rnd.Next(area.BorderWidth + 1, area.Width + area.BorderWidth);
                        enemies[i].EnemyYPos = 1;
                        break;

                    case Sides.Right:
                        enemies[i].EnemyXPos = area.Width + area.BorderWidth - 1;
                        enemies[i].EnemyYPos = rnd.Next(1, area.Height - 1);
                        break;

                    case Sides.Bottom:
                        enemies[i].EnemyXPos = rnd.Next(area.BorderWidth + 1, area.Width + area.BorderWidth);
                        enemies[i].EnemyYPos = area.Height - 2;
                        break;

                    case Sides.Left:
                        enemies[i].EnemyXPos = area.BorderWidth;
                        enemies[i].EnemyYPos = rnd.Next(1, area.Height - 1);
                        break;
                }
            }
        }

        public static void Render(Enemy[] enemies, GameArea area, Coin[] coins, Random rnd)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ForegroundColor = area.ScreengrassForeColor;
                BackgroundColor = area.ScreengrassBackColor;

                switch (enemies[0].EmergingSide)
                {
                    case Sides.Top:
                        if (enemies[i].EnemyYPos != 1) //if the enemies were not just drawn, erase their previous instance before drawing them in their new position
                        {
                            SetCursorPosition(enemies[i].EnemyXPos, enemies[i].EnemyYPos - 1);
                            WriteLine(area.ScreengrassChar);
                        }
                        break;

                    case Sides.Right:
                        if (enemies[i].EnemyXPos != area.Width + area.BorderWidth - 1)
                        {
                            SetCursorPosition(enemies[i].EnemyXPos + 1, enemies[i].EnemyYPos);
                            WriteLine(area.ScreengrassChar);
                        }
                        break;

                    case Sides.Bottom:
                        if (enemies[i].EnemyYPos != area.Height - 2)
                        {
                            SetCursorPosition(enemies[i].EnemyXPos, enemies[i].EnemyYPos + 1);
                            WriteLine(area.ScreengrassChar);
                        }
                        break;

                    case Sides.Left:
                        if (enemies[i].EnemyXPos != area.BorderWidth)
                        {
                            SetCursorPosition(enemies[i].EnemyXPos - 1, enemies[i].EnemyYPos);
                            WriteLine(area.ScreengrassChar);
                        }
                        break;
                }
                /*
                for (int j = 0; j < coins.Length; j++) //if the enemy passed over a coin, redraw that coin after it passes
                {
                    if (enemies[i].EnemyXPos == coins[j].CoinXPos && enemies[i].EnemyYPos == coins[j].CoinYPos)
                    {
                        coins[j].WriteOnScreen();
                    }
                }*/

            }
            foreach (Coin c in coins)
            {
                c.WriteOnScreen();
            }
            foreach (Enemy e in enemies)
            {
                ForegroundColor = e.EnemyForeColor;
                BackgroundColor = e.EnemyBackColor;
                SetCursorPosition(e.EnemyXPos, e.EnemyYPos);
                WriteLine(e.EnemyChar);
            }


            ResetColor();
        }

        public static void RenderInitial(Enemy[] enemies)
        {
            foreach(Enemy e in enemies)
            {
                ForegroundColor = e.EnemyForeColor;
                BackgroundColor = e.EnemyBackColor;
                SetCursorPosition(e.EnemyXPos, e.EnemyYPos);
                WriteLine(e.EnemyChar);
            }
            ResetColor();
        }

        public static void MoveEnemies(Enemy[] enemies, GameArea area, Random rnd)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                switch (enemies[0].EmergingSide)
                {
                    case Sides.Top:
                        if (enemies[i].EnemyYPos == area.Height - 2)
                        {
                            ChangeSide(enemies, area, rnd);
                        }
                        else
                        {
                            enemies[i].EnemyYPos++;
                        }
                        break;

                    case Sides.Right:
                        if (enemies[i].EnemyXPos == area.BorderWidth)
                        {
                            ChangeSide(enemies, area, rnd);
                        }
                        else
                        {
                            enemies[i].EnemyXPos--;
                        }
                        break;

                    case Sides.Bottom:
                        if (enemies[i].EnemyYPos == 1)
                        {
                            ChangeSide(enemies, area, rnd);
                        }
                        else
                        {
                            enemies[i].EnemyYPos--;
                        }
                        break;

                    case Sides.Left:
                        if (enemies[i].EnemyXPos == area.Width + area.BorderWidth - 1)
                        {
                            ChangeSide(enemies, area, rnd);
                        }
                        else
                        {
                            enemies[i].EnemyXPos++;
                        }
                        break;
                }//end of switch
            }//end of loop
        }//end of method
        #endregion

        #region//INTERNAL METHODS

        #endregion
        #region//PROPERTIES
        public int EnemyXPos
        {
            get
            {
                return _enemyXPos;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("ENEMY X COORDINATE CANNOT BE NEGATIVE","_enemyXPos");

                _enemyXPos = value;
            }
        }

        public int EnemyYPos
        {
            get
            {
                return _enemyYPos;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("ENEMY Y COORDINATE CANNOT BE NEGATIVE","_enemyYPos");

                _enemyYPos = value;
            }
        }

        public Sides EmergingSide
        {
            get
            {
                return _emergingSide;
            }
            set
            {
                _emergingSide = value;
            }
        }

        public char EnemyChar
        {
            get
            {
                return _enemyChar;
            }
            set
            {
                _enemyChar = value;
            }
        }

        public ConsoleColor EnemyForeColor
        {
            get
            {
                return _enemyForeColor;
            }
            set
            {
                _enemyForeColor = value;
            }
        }

        public ConsoleColor EnemyBackColor
        {
            get
            {
                return _enemyBackChar;
            }
            set
            {
                _enemyBackChar = value;
            }
        }



        #endregion
    }
}
