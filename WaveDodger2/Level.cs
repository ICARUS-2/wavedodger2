using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace WaveDodger2
{
    class Level
    {
        private const int DEFAULT_NUMBER_OF_COINS = 5;
        private const int DEFAULT_NUMBER_OF_ENEMIES = 20;
        private const int DEFAULT_DIFFICULTY = 250;
        private const string DEFAULT_SOUNDLOCATION = @"..\..\sound\l1-love4eva.wav";
        private SoundPlayer _music = new SoundPlayer(); //Instructions screen: 365 8-bit l1: Love4Eva 8-bit l2:Ddu Du Ddu Du 8-bit  l3: Unravel 8 bit l4: Godzilla 8 bit l5:Black Swan 8-bit
        private Random _rnd;
        private Player _player1;
        private GameArea _area;
        private Coin[] _coins;
        private Enemy[] _enemies;
        private int _difficulty;

        public Level() //default settings
        {
            Rnd = new Random();
            Player1 = new Player();
            Area = new GameArea();
            Coins = Coin.GenerateCoinArray(DEFAULT_NUMBER_OF_COINS, Rnd, Area, Player1);
            Enemies = Enemy.GetArrayOfEnemies(DEFAULT_NUMBER_OF_ENEMIES, Area);
            Difficulty = DEFAULT_DIFFICULTY;
            Music.SoundLocation = DEFAULT_SOUNDLOCATION;
        }

        public Level(int width_, int height_, int numberOfCoins_, int numberOfEnemies_, int difficulty_) //Simple editor
        {
            Rnd = new Random();
            Player1 = new Player();
            Area = new GameArea(width_, height_);
            Coins = Coin.GenerateCoinArray(numberOfCoins_, Rnd, Area, Player1);
            Enemies = Enemy.GetArrayOfEnemies(numberOfEnemies_, Area);
            Difficulty = difficulty_;
        }

        public Level(char playerChar_, int playerStartingLives_, ConsoleColor playerForeColor_, ConsoleColor playerBackColor_, //Advanced editor //Player parameters
                    char screengrassChar_, char borderChar_, ConsoleColor screengrassForeColor_, ConsoleColor screengrassBackColor_, ConsoleColor borderForeColor_, ConsoleColor borderBackColor_, int width_, int height_, int borderWidth_, //Game area parameters
                    char coinChar_, ConsoleColor coinForeColor_, ConsoleColor coinBackColor_, int numberOfCoins, //Coin parameters
                    int numberOfEnemies_, char enemyChar_, ConsoleColor enemyForeColor_, ConsoleColor enemyBackColor_, //enemy parameters
                    int difficulty_, string soundLocation_) 
        {
            Rnd = new Random();
            Player1 = new Player(playerChar_, playerStartingLives_, playerForeColor_, playerBackColor_);
            Area = new GameArea(screengrassChar_, borderChar_, screengrassForeColor_, screengrassBackColor_, borderForeColor_, borderBackColor_, width_, height_, borderWidth_);
            Coins = Coin.GenerateCoinArray(numberOfCoins, Rnd, Area, Player1, coinChar_, coinForeColor_, coinBackColor_);
            Enemies = Enemy.GetArrayOfEnemies(numberOfEnemies_, Area, enemyChar_, enemyForeColor_, enemyBackColor_);
            Difficulty = difficulty_;
            Music.SoundLocation = soundLocation_;
        }

        public static Level[] GenerateLevels()
        {
            Level[] levels = new Level[]
            {
                //level 1
                new Level(LevelBuilder.LEVEL_1_PLAYER_CHAR, LevelBuilder.LEVEL_1_STARTING_LIVES, LevelBuilder.LEVEL_1_PLAYER_FORE_COLOR, LevelBuilder.LEVEL_1_PLAYER_BACK_COLOR,
                         LevelBuilder.LEVEL_1_SCREENGRASS_CHAR, LevelBuilder.LEVEL_1_BORDER_CHAR, LevelBuilder.LEVEL_1_SCREENGRASS_FORE_COLOR, LevelBuilder.LEVEL_1_SCREENGRASS_BACK_COLOR, LevelBuilder.LEVEL_1_BORDER_FORE_COLOR, LevelBuilder.LEVEL_1_BORDER_BACK_COLOR, LevelBuilder.LEVEL_1_AREA_WIDTH, LevelBuilder.LEVEL_1_AREA_HEIGHT, LevelBuilder.LEVEL_1_BORDER_WIDTH,
                         LevelBuilder.LEVEL_1_COIN_CHAR, LevelBuilder.LEVEL_1_COIN_FORE_COLOR, LevelBuilder.LEVEL_1_COIN_BACK_COLOR, LevelBuilder.LEVEL_1_NUMBER_OF_COINS,
                         LevelBuilder.LEVEL_1_NUMBER_OF_ENEMIES, LevelBuilder.LEVEL_1_ENEMY_CHAR, LevelBuilder.LEVEL_1_ENEMY_FORE_COLOR, LevelBuilder.LEVEL_1_ENEMY_BACK_COLOR,
                         LevelBuilder.LEVEL_1_DIFFICULTY, LevelBuilder.LEVEL_1_SOUNDLOCATION
                ),
                //level 2
                new Level(LevelBuilder.LEVEL_2_PLAYER_CHAR, LevelBuilder.LEVEL_2_STARTING_LIVES, LevelBuilder.LEVEL_2_PLAYER_FORE_COLOR, LevelBuilder.LEVEL_2_PLAYER_BACK_COLOR,
                         LevelBuilder.LEVEL_2_SCREENGRASS_CHAR, LevelBuilder.LEVEL_2_BORDER_CHAR, LevelBuilder.LEVEL_2_SCREENGRASS_FORE_COLOR, LevelBuilder.LEVEL_2_SCREENGRASS_BACK_COLOR, LevelBuilder.LEVEL_2_BORDER_FORE_COLOR, LevelBuilder.LEVEL_2_BORDER_BACK_COLOR, LevelBuilder.LEVEL_2_AREA_WIDTH, LevelBuilder.LEVEL_2_AREA_HEIGHT, LevelBuilder.LEVEL_2_BORDER_WIDTH,
                         LevelBuilder.LEVEL_2_COIN_CHAR, LevelBuilder.LEVEL_2_COIN_FORE_COLOR, LevelBuilder.LEVEL_2_COIN_BACK_COLOR, LevelBuilder.LEVEL_2_NUMBER_OF_COINS,
                         LevelBuilder.LEVEL_2_NUMBER_OF_ENEMIES, LevelBuilder.LEVEL_2_ENEMY_CHAR, LevelBuilder.LEVEL_2_ENEMY_FORE_COLOR, LevelBuilder.LEVEL_2_ENEMY_BACK_COLOR,
                         LevelBuilder.LEVEL_2_DIFFICULTY, LevelBuilder.LEVEL_2_SOUNDLOCATION
                ),
                //level 3
                new Level(LevelBuilder.LEVEL_3_PLAYER_CHAR, LevelBuilder.LEVEL_3_STARTING_LIVES, LevelBuilder.LEVEL_3_PLAYER_FORE_COLOR, LevelBuilder.LEVEL_3_PLAYER_BACK_COLOR,
                         LevelBuilder.LEVEL_3_SCREENGRASS_CHAR, LevelBuilder.LEVEL_3_BORDER_CHAR, LevelBuilder.LEVEL_3_SCREENGRASS_FORE_COLOR, LevelBuilder.LEVEL_3_SCREENGRASS_BACK_COLOR, LevelBuilder.LEVEL_3_BORDER_FORE_COLOR, LevelBuilder.LEVEL_3_BORDER_BACK_COLOR, LevelBuilder.LEVEL_3_AREA_WIDTH, LevelBuilder.LEVEL_3_AREA_HEIGHT, LevelBuilder.LEVEL_3_BORDER_WIDTH,
                         LevelBuilder.LEVEL_3_COIN_CHAR, LevelBuilder.LEVEL_3_COIN_FORE_COLOR, LevelBuilder.LEVEL_3_COIN_BACK_COLOR, LevelBuilder.LEVEL_3_NUMBER_OF_COINS,
                         LevelBuilder.LEVEL_3_NUMBER_OF_ENEMIES, LevelBuilder.LEVEL_3_ENEMY_CHAR, LevelBuilder.LEVEL_3_ENEMY_FORE_COLOR, LevelBuilder.LEVEL_3_ENEMY_BACK_COLOR,
                         LevelBuilder.LEVEL_3_DIFFICULTY, LevelBuilder.LEVEL_3_SOUNDLOCATION
                ),
                
                //level 4
                new Level(LevelBuilder.LEVEL_4_PLAYER_CHAR, LevelBuilder.LEVEL_4_STARTING_LIVES, LevelBuilder.LEVEL_4_PLAYER_FORE_COLOR, LevelBuilder.LEVEL_4_PLAYER_BACK_COLOR,
                         LevelBuilder.LEVEL_4_SCREENGRASS_CHAR, LevelBuilder.LEVEL_4_BORDER_CHAR, LevelBuilder.LEVEL_4_SCREENGRASS_FORE_COLOR, LevelBuilder.LEVEL_4_SCREENGRASS_BACK_COLOR, LevelBuilder.LEVEL_4_BORDER_FORE_COLOR, LevelBuilder.LEVEL_4_BORDER_BACK_COLOR, LevelBuilder.LEVEL_4_AREA_WIDTH, LevelBuilder.LEVEL_4_AREA_HEIGHT, LevelBuilder.LEVEL_4_BORDER_WIDTH,
                         LevelBuilder.LEVEL_4_COIN_CHAR, LevelBuilder.LEVEL_4_COIN_FORE_COLOR, LevelBuilder.LEVEL_4_COIN_BACK_COLOR, LevelBuilder.LEVEL_4_NUMBER_OF_COINS,
                         LevelBuilder.LEVEL_4_NUMBER_OF_ENEMIES, LevelBuilder.LEVEL_4_ENEMY_CHAR, LevelBuilder.LEVEL_4_ENEMY_FORE_COLOR, LevelBuilder.LEVEL_4_ENEMY_BACK_COLOR,
                         LevelBuilder.LEVEL_4_DIFFICULTY, LevelBuilder.LEVEL_4_SOUNDLOCATION
                ),
                
                //level 5
                new Level(LevelBuilder.LEVEL_5_PLAYER_CHAR, LevelBuilder.LEVEL_5_STARTING_LIVES, LevelBuilder.LEVEL_5_PLAYER_FORE_COLOR, LevelBuilder.LEVEL_5_PLAYER_BACK_COLOR,
                         LevelBuilder.LEVEL_5_SCREENGRASS_CHAR, LevelBuilder.LEVEL_5_BORDER_CHAR, LevelBuilder.LEVEL_5_SCREENGRASS_FORE_COLOR, LevelBuilder.LEVEL_5_SCREENGRASS_BACK_COLOR, LevelBuilder.LEVEL_5_BORDER_FORE_COLOR, LevelBuilder.LEVEL_5_BORDER_BACK_COLOR, LevelBuilder.LEVEL_5_AREA_WIDTH, LevelBuilder.LEVEL_5_AREA_HEIGHT, LevelBuilder.LEVEL_5_BORDER_WIDTH,
                         LevelBuilder.LEVEL_5_COIN_CHAR, LevelBuilder.LEVEL_5_COIN_FORE_COLOR, LevelBuilder.LEVEL_5_COIN_BACK_COLOR, LevelBuilder.LEVEL_5_NUMBER_OF_COINS,
                         LevelBuilder.LEVEL_5_NUMBER_OF_ENEMIES, LevelBuilder.LEVEL_5_ENEMY_CHAR, LevelBuilder.LEVEL_5_ENEMY_FORE_COLOR, LevelBuilder.LEVEL_5_ENEMY_BACK_COLOR,
                         LevelBuilder.LEVEL_5_DIFFICULTY, LevelBuilder.LEVEL_5_SOUNDLOCATION
                ),
            };

            return levels;
        }
            
        


        #region//PROPERTIES

        public SoundPlayer Music
        {
            get
            {
                return _music;
            }
            set
            {
                _music = value;
            }
        }

        public Random Rnd
        {
            get
            {
                return _rnd;
            }
            set
            {
                _rnd = value;
            }
        }

        
        public Player Player1
        {
            get
            {
                return _player1;
            }
            set
            {
                _player1 = value;
            }
        }

        public GameArea Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
            }
        }

        public Coin[] Coins
        {
            get
            {
                return _coins;
            }
            set
            {
                _coins = value;
            }
        }

        public Enemy[] Enemies
        {
            get
            {
                return _enemies;
            }
            set
            {
                _enemies = value;
            }
        }

        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value;
            }
        }


        #endregion
    }
}
