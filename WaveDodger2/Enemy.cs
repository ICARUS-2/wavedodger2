using System;
using static System.Console;

namespace WaveDodger2
{
    /// <summary>
    /// Enemy class defines an enemy in a console game that moves from side to side of the play area randomly.
    /// External Dependencies: Player.cs, Coin.cs, GameArea.cs
    /// </summary>
    class Enemy
    {
        private int _enemyXPos; //The horizontal position of the enemy on screen.
        private int _enemyYPos; //The vertical position of the enemy on screen.

        private const Sides DEFAULT_EMERGING_SIDE = Sides.Top;
        private Sides _emergingSide; //The side that the wave of the enemies will come out from.

        public const char DEFAULT_ENEMY_CHAR = 'X';
        private char _enemyChar; //The ASCII character that represents the enemy.

        public const ConsoleColor DEFAULT_ENEMY_FORECOLOR = ConsoleColor.DarkRed;
        public const ConsoleColor DEFAULT_ENEMY_BACKCOLOR = ConsoleColor.Black;
        private ConsoleColor _enemyForeColor; //The foreground color of the character representing the enemy.
        private ConsoleColor _enemyBackChar; //The background color of the character representing the enemy.
        public enum Sides //Makes it easier to set the emerging side of the enemy wave.
        {
            Top = 1,
            Right = 2,
            Bottom = 3,
            Left = 4,
        }

        #region//CONSTRUCTORS
        /// <summary>
        /// Sets EnemyXPos and EnemyYPos to zero, and sets the EmergingSide, EnemyChar, EnemyForeColor
        /// and EnemyBackColor to their public constant defaults.
        /// </summary>
        public Enemy() //default
        {
            EnemyXPos = 0;
            EnemyYPos = 0;
            EmergingSide = DEFAULT_EMERGING_SIDE;
            EnemyChar = DEFAULT_ENEMY_CHAR;
            EnemyForeColor = DEFAULT_ENEMY_FORECOLOR;
            EnemyBackColor = DEFAULT_ENEMY_BACKCOLOR;
        }

        /// <summary>
        /// Sets EnemyXPos and EnemyYPos to zero, sets the EmergingSide to its default value, and sets the 
        /// EnemyChar, EnemyForeColor and EnemyBackColor to their respective parameters.
        /// </summary>
        /// <param name="enemyChar_"></param>
        /// <param name="enemyForeColor_"></param>
        /// <param name="enemyBackColor_"></param>
        public Enemy(char enemyChar_, ConsoleColor enemyForeColor_, ConsoleColor enemyBackColor_) //advanced editor
        {
            EnemyXPos = 0;
            EnemyYPos = 0;
            EmergingSide = DEFAULT_EMERGING_SIDE;
            EnemyChar = enemyChar_;
            EnemyForeColor = enemyForeColor_;
            EnemyBackColor = enemyBackColor_;
        }
        #endregion

        #region//EXTERNAL METHODS
        /// <summary>
        /// Overload takes an integer representing the amount of enemies to be generated and the GameArea they will be used on. Creates a new Enemy 
        /// array and initializes each element with its default constructor. Returns the array.
        /// </summary>
        /// <param name="numberOfEnemies"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public static Enemy[] GetArrayOfEnemies(int numberOfEnemies, GameArea area)
        {
            Enemy[] enemies = new Enemy[numberOfEnemies];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy();
            }

            return enemies;
        }

        /// <summary>
        /// Overload takes an integer representing the amount of enemies to be generated, the game area, an enemy character, and enemy 
        /// foreground/background colors. Creates a new Enemy array and initializes each element with its Advanced Editor Constructor. 
        /// Returns the array.
        /// </summary>
        /// <param name="numberOfEnemies"></param>
        /// <param name="area"></param>
        /// <param name="enemyChar"></param>
        /// <param name="enemyForeColor"></param>
        /// <param name="enemyBackColor"></param>
        /// <returns></returns>
        public static Enemy[] GetArrayOfEnemies(int numberOfEnemies, GameArea area, char enemyChar, ConsoleColor enemyForeColor, ConsoleColor enemyBackColor)
        {
            Enemy[] enemies = new Enemy[numberOfEnemies];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy(enemyChar, enemyForeColor, enemyBackColor);
            }

            return enemies;
        }

        /// <summary>
        /// Takes an array of enemies, a game area and a Random class instance as arguments, and changes the emerging side of the
        /// enemies once its wave is complete by setting its initial position to a randomly generated side. Returns void.
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="area"></param>
        /// <param name="rnd"></param>
        public static void ChangeSide(Enemy[] enemies, GameArea area, Random rnd)
        {

            enemies[0].EmergingSide = (Sides)rnd.Next((int)Sides.Top, (int)Sides.Left + 1); //Generates a number from 1-4 representing the side the enemies will scroll across the screen from

            for (int i = 0; i < enemies.Length; i++)
            {
                switch (enemies[0].EmergingSide)
                {
                    case Sides.Top: 
                        enemies[i].EnemyXPos = rnd.Next(area.BorderWidth, area.Width + area.BorderWidth);
                        enemies[i].EnemyYPos = 1;
                        break;

                    case Sides.Right:
                        enemies[i].EnemyXPos = area.Width + area.BorderWidth - 1;
                        enemies[i].EnemyYPos = rnd.Next(1, area.Height - 1);
                        break;

                    case Sides.Bottom:
                        enemies[i].EnemyXPos = rnd.Next(area.BorderWidth, area.Width + area.BorderWidth);
                        enemies[i].EnemyYPos = area.Height - 2;
                        break;

                    case Sides.Left:
                        enemies[i].EnemyXPos = area.BorderWidth;
                        enemies[i].EnemyYPos = rnd.Next(1, area.Height - 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Takes an array of enemies, a game area and a Random class instance as arguments, and moves the enemies’ coordinates 
        /// depending on their EmergingSide. Returns void.
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="area"></param>
        /// <param name="rnd"></param>
        public static void MoveEnemies(Enemy[] enemies, GameArea area, Random rnd)
        {
            if (CheckIfAtEnd(enemies, area))
            {
                DeleteWaveAtEnd(enemies, area);
                ChangeSide(enemies, area, rnd);          
            }
            else
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    switch (enemies[0].EmergingSide)
                    {
                        case Sides.Top:
                            enemies[i].EnemyYPos++;
                            break;

                        case Sides.Right:
                            enemies[i].EnemyXPos--;
                            break;

                        case Sides.Bottom:
                            enemies[i].EnemyYPos--;
                            break;

                        case Sides.Left:
                            enemies[i].EnemyXPos++;
                            break;

                    }//end of switch
                }//end of loop
            }
        }//end of method

        /// <summary>
        /// Takes an array of enemies, a Player object instance, a GameArea instance, a Coin array and a Random 
        /// class instance as arguments. Erases previous enemy icons after the enemies have moved and re-draws them as well as
        /// the coins so their icons do not disappear. Returns void.
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="player"></param>
        /// <param name="area"></param>
        /// <param name="coins"></param>
        /// <param name="rnd"></param>
        public static void Render(Enemy[] enemies, Player player, GameArea area, Coin[] coins, Random rnd)
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
            player.Draw(area);

            ResetColor();
        }

        /// <summary>
        /// Takes an array of enemies as an argument and draws every instance based on their X and Y coordinates. Is essentially
        /// a stripped back version of the Render method that only draws the enemies and does not erase any previous enemy icons or 
        /// re-draw any coins. Returns void.
        /// </summary>
        /// <param name="enemies"></param>
        public static void RenderInitial(Enemy[] enemies)
        {
            foreach (Enemy e in enemies)
            {
                ForegroundColor = e.EnemyForeColor;
                BackgroundColor = e.EnemyBackColor;
                SetCursorPosition(e.EnemyXPos, e.EnemyYPos);
                WriteLine(e.EnemyChar);
            }
            ResetColor();
        }
        #endregion

        #region//INTERNAL METHODS
        /// <summary>
        /// Takes an array of enemies and a GameArea instance as arguments, and utilizes a checksum to see if all of the 
        /// enemies have reached their ending position for that specific wave. Returns true if the enemies are at the end of 
        /// the wave, returns false if not.
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private static bool CheckIfAtEnd(Enemy[] enemies, GameArea area)
        {
            int checksum = 0;
            for (int i = 0; i < enemies.Length; i++)
            {
                switch (enemies[0].EmergingSide)
                {
                    case Sides.Top:
                        if (enemies[i].EnemyYPos == area.Height - 2)
                        {
                            checksum++;
                        }
                        break;

                    case Sides.Right:
                        if (enemies[i].EnemyXPos == area.BorderWidth)
                        {
                            checksum++;
                        }
                        break;

                    case Sides.Bottom:
                        if (enemies[i].EnemyYPos == 1)
                        {
                            checksum++;
                        }
                        break;

                    case Sides.Left:
                        if (enemies[i].EnemyXPos == area.Width + area.BorderWidth - 1)
                        {
                            checksum++;
                        }
                        break;
                }
            }
            return (checksum == enemies.Length);
        }

        /// <summary>
        /// Takes an array of enemies and a GameArea instance as arguments, and erases all of the enemies once they’ve reached
        /// the end of their wave and replaces their icons with the screengrass of the GameArea instance passed to it. Returns void.
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="area"></param>
        private static void DeleteWaveAtEnd(Enemy[] enemies, GameArea area)
        {
            foreach(Enemy e in enemies)
            {
                SetCursorPosition(e.EnemyXPos, e.EnemyYPos);
                ForegroundColor = area.ScreengrassForeColor;
                BackgroundColor = area.ScreengrassBackColor;
                WriteLine(area.ScreengrassChar);
            }
        }
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
