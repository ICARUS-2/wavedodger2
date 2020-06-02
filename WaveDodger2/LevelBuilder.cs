using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace WaveDodger2
{
    struct LevelBuilder
    {
        //LEVEL 1 PARAMETERS

        //level 1 player parameters
        public const char LEVEL_1_PLAYER_CHAR = Player.DEFAULT_PLAYER_CHAR;
        public const int LEVEL_1_STARTING_LIVES = Player.DEFAULT_STARTING_LIVES;
        public const ConsoleColor LEVEL_1_PLAYER_FORE_COLOR = Player.DEFAULT_FORE_COLOR;
        public const ConsoleColor LEVEL_1_PLAYER_BACK_COLOR = Player.DEFAULT_BACK_COLOR;
        //level 1 game area parameters
        public const char LEVEL_1_SCREENGRASS_CHAR = GameArea.DEFAULT_SCREENGRASS_CHAR;
        public const char LEVEL_1_BORDER_CHAR = GameArea.DEFAULT_BORDER_CHAR;
        public const ConsoleColor LEVEL_1_SCREENGRASS_FORE_COLOR = ConsoleColor.Cyan;
        public const ConsoleColor LEVEL_1_SCREENGRASS_BACK_COLOR = ConsoleColor.Black;
        public const ConsoleColor LEVEL_1_BORDER_FORE_COLOR = ConsoleColor.Blue;
        public const ConsoleColor LEVEL_1_BORDER_BACK_COLOR = ConsoleColor.Black;
        public const int LEVEL_1_AREA_WIDTH = 60;
        public const int LEVEL_1_AREA_HEIGHT = 25;
        public const int LEVEL_1_BORDER_WIDTH = 12;
        //level 1 coin parameters
        public const char LEVEL_1_COIN_CHAR = Coin.DEFAULT_COIN_CHAR;
        public const ConsoleColor LEVEL_1_COIN_FORE_COLOR = Coin.DEFAULT_COIN_FORECOLOR;
        public const ConsoleColor LEVEL_1_COIN_BACK_COLOR = Coin.DEFAULT_COIN_BACKCOLOR;
        public const int LEVEL_1_NUMBER_OF_COINS = 5;
        //level 1 enemy parameters
        public const int LEVEL_1_NUMBER_OF_ENEMIES = 30;
        public const char LEVEL_1_ENEMY_CHAR = Enemy.DEFAULT_ENEMY_CHAR;
        public const ConsoleColor LEVEL_1_ENEMY_FORE_COLOR = Enemy.DEFAULT_ENEMY_FORECOLOR;
        public const ConsoleColor LEVEL_1_ENEMY_BACK_COLOR = Enemy.DEFAULT_ENEMY_BACKCOLOR;
        //level 1 difficulty parameter
        public const int LEVEL_1_DIFFICULTY = 400;



        //LEVEL 2 PARAMETERS

        //level 2 player parameters
        public const char LEVEL_2_PLAYER_CHAR = Player.DEFAULT_PLAYER_CHAR;
        public const int LEVEL_2_STARTING_LIVES = Player.DEFAULT_STARTING_LIVES;
        public const ConsoleColor LEVEL_2_PLAYER_FORE_COLOR = Player.DEFAULT_FORE_COLOR;
        public const ConsoleColor LEVEL_2_PLAYER_BACK_COLOR = Player.DEFAULT_BACK_COLOR;
        //level 2 game area parameters
        public const char LEVEL_2_SCREENGRASS_CHAR = GameArea.DEFAULT_SCREENGRASS_CHAR;
        public const char LEVEL_2_BORDER_CHAR = GameArea.DEFAULT_BORDER_CHAR;
        public const ConsoleColor LEVEL_2_SCREENGRASS_FORE_COLOR = ConsoleColor.Green;
        public const ConsoleColor LEVEL_2_SCREENGRASS_BACK_COLOR = ConsoleColor.Black;
        public const ConsoleColor LEVEL_2_BORDER_FORE_COLOR = ConsoleColor.DarkGreen;
        public const ConsoleColor LEVEL_2_BORDER_BACK_COLOR = ConsoleColor.Black;
        public const int LEVEL_2_AREA_WIDTH = 75;
        public const int LEVEL_2_AREA_HEIGHT = 30;
        public const int LEVEL_2_BORDER_WIDTH = 15;
        //level 2 coin parameters
        public const char LEVEL_2_COIN_CHAR = Coin.DEFAULT_COIN_CHAR;
        public const ConsoleColor LEVEL_2_COIN_FORE_COLOR = Coin.DEFAULT_COIN_FORECOLOR;
        public const ConsoleColor LEVEL_2_COIN_BACK_COLOR = Coin.DEFAULT_COIN_BACKCOLOR;
        public const int LEVEL_2_NUMBER_OF_COINS = 15;
        //level 2 enemy parameters
        public const int LEVEL_2_NUMBER_OF_ENEMIES = 35;
        public const char LEVEL_2_ENEMY_CHAR = Enemy.DEFAULT_ENEMY_CHAR;
        public const ConsoleColor LEVEL_2_ENEMY_FORE_COLOR = Enemy.DEFAULT_ENEMY_FORECOLOR;
        public const ConsoleColor LEVEL_2_ENEMY_BACK_COLOR = Enemy.DEFAULT_ENEMY_BACKCOLOR;
        //level 2 difficulty parameter
        public const int LEVEL_2_DIFFICULTY = 350;


        //LEVEL 3 PARAMETERS

        //level 3 player parameters
        public const char LEVEL_3_PLAYER_CHAR = Player.DEFAULT_PLAYER_CHAR;
        public const int LEVEL_3_STARTING_LIVES = Player.DEFAULT_STARTING_LIVES;
        public const ConsoleColor LEVEL_3_PLAYER_FORE_COLOR = Player.DEFAULT_FORE_COLOR;
        public const ConsoleColor LEVEL_3_PLAYER_BACK_COLOR = Player.DEFAULT_BACK_COLOR;
        //level 3 game area parameters
        public const char LEVEL_3_SCREENGRASS_CHAR = GameArea.DEFAULT_SCREENGRASS_CHAR;
        public const char LEVEL_3_BORDER_CHAR = GameArea.DEFAULT_BORDER_CHAR;
        public const ConsoleColor LEVEL_3_SCREENGRASS_FORE_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor LEVEL_3_SCREENGRASS_BACK_COLOR = ConsoleColor.Black;
        public const ConsoleColor LEVEL_3_BORDER_FORE_COLOR = ConsoleColor.DarkYellow;
        public const ConsoleColor LEVEL_3_BORDER_BACK_COLOR = ConsoleColor.Black;
        public const int LEVEL_3_AREA_WIDTH = 90;
        public const int LEVEL_3_AREA_HEIGHT = 30;
        public const int LEVEL_3_BORDER_WIDTH = 15;
        //level 3 coin parameters
        public const char LEVEL_3_COIN_CHAR = Coin.DEFAULT_COIN_CHAR;
        public const ConsoleColor LEVEL_3_COIN_FORE_COLOR = Coin.DEFAULT_COIN_FORECOLOR;
        public const ConsoleColor LEVEL_3_COIN_BACK_COLOR = Coin.DEFAULT_COIN_BACKCOLOR;
        public const int LEVEL_3_NUMBER_OF_COINS = 20;
        //level 3 enemy parameters
        public const int LEVEL_3_NUMBER_OF_ENEMIES = 40;
        public const char LEVEL_3_ENEMY_CHAR = Enemy.DEFAULT_ENEMY_CHAR;
        public const ConsoleColor LEVEL_3_ENEMY_FORE_COLOR = Enemy.DEFAULT_ENEMY_FORECOLOR;
        public const ConsoleColor LEVEL_3_ENEMY_BACK_COLOR = Enemy.DEFAULT_ENEMY_BACKCOLOR;
        //level 3 difficulty parameter
        public const int LEVEL_3_DIFFICULTY = 300;



        //LEVEL 4 PARAMETERS

        //level 4 player parameters
        public const char LEVEL_4_PLAYER_CHAR = Player.DEFAULT_PLAYER_CHAR;
        public const int LEVEL_4_STARTING_LIVES = Player.DEFAULT_STARTING_LIVES;
        public const ConsoleColor LEVEL_4_PLAYER_FORE_COLOR = Player.DEFAULT_FORE_COLOR;
        public const ConsoleColor LEVEL_4_PLAYER_BACK_COLOR = Player.DEFAULT_BACK_COLOR;
        //level 4 game area parameters
        public const char LEVEL_4_SCREENGRASS_CHAR = GameArea.DEFAULT_SCREENGRASS_CHAR;
        public const char LEVEL_4_BORDER_CHAR = GameArea.DEFAULT_BORDER_CHAR;
        public const ConsoleColor LEVEL_4_SCREENGRASS_FORE_COLOR = ConsoleColor.Red;
        public const ConsoleColor LEVEL_4_SCREENGRASS_BACK_COLOR = ConsoleColor.Black;
        public const ConsoleColor LEVEL_4_BORDER_FORE_COLOR = ConsoleColor.DarkRed;
        public const ConsoleColor LEVEL_4_BORDER_BACK_COLOR = ConsoleColor.Black;
        public const int LEVEL_4_AREA_WIDTH = 100;
        public const int LEVEL_4_AREA_HEIGHT = 35;
        public const int LEVEL_4_BORDER_WIDTH = 15;
        //level 4 coin parameters
        public const char LEVEL_4_COIN_CHAR = Coin.DEFAULT_COIN_CHAR;
        public const ConsoleColor LEVEL_4_COIN_FORE_COLOR = Coin.DEFAULT_COIN_FORECOLOR;
        public const ConsoleColor LEVEL_4_COIN_BACK_COLOR = Coin.DEFAULT_COIN_BACKCOLOR;
        public const int LEVEL_4_NUMBER_OF_COINS = 25;
        //level 4 enemy parameters
        public const int LEVEL_4_NUMBER_OF_ENEMIES = 45;
        public const char LEVEL_4_ENEMY_CHAR = Enemy.DEFAULT_ENEMY_CHAR;
        public const ConsoleColor LEVEL_4_ENEMY_FORE_COLOR = Enemy.DEFAULT_ENEMY_FORECOLOR;
        public const ConsoleColor LEVEL_4_ENEMY_BACK_COLOR = Enemy.DEFAULT_ENEMY_BACKCOLOR;
        //level 4 difficulty parameter
        public const int LEVEL_4_DIFFICULTY = 250;


        
        //LEVEL 5 PARAMETERS

        //level 5 player parameters
        public const char LEVEL_5_PLAYER_CHAR = Player.DEFAULT_PLAYER_CHAR;
        public const int LEVEL_5_STARTING_LIVES = Player.DEFAULT_STARTING_LIVES;
        public const ConsoleColor LEVEL_5_PLAYER_FORE_COLOR = Player.DEFAULT_FORE_COLOR;
        public const ConsoleColor LEVEL_5_PLAYER_BACK_COLOR = Player.DEFAULT_BACK_COLOR;
        //level 5 game area parameters
        public const char LEVEL_5_SCREENGRASS_CHAR = GameArea.DEFAULT_SCREENGRASS_CHAR;
        public const char LEVEL_5_BORDER_CHAR = GameArea.DEFAULT_BORDER_CHAR;
        public const ConsoleColor LEVEL_5_SCREENGRASS_FORE_COLOR = ConsoleColor.Black;
        public const ConsoleColor LEVEL_5_SCREENGRASS_BACK_COLOR = ConsoleColor.Black;
        public const ConsoleColor LEVEL_5_BORDER_FORE_COLOR = ConsoleColor.DarkMagenta;
        public const ConsoleColor LEVEL_5_BORDER_BACK_COLOR = ConsoleColor.Black;
        public const int LEVEL_5_AREA_WIDTH = 110;
        public const int LEVEL_5_AREA_HEIGHT = 40;
        public const int LEVEL_5_BORDER_WIDTH = 20;
        //level 5 coin parameters
        public const char LEVEL_5_COIN_CHAR = Coin.DEFAULT_COIN_CHAR;
        public const ConsoleColor LEVEL_5_COIN_FORE_COLOR = Coin.DEFAULT_COIN_FORECOLOR;
        public const ConsoleColor LEVEL_5_COIN_BACK_COLOR = Coin.DEFAULT_COIN_BACKCOLOR;
        public const int LEVEL_5_NUMBER_OF_COINS = 25;
        //level 5 enemy parameters
        public const int LEVEL_5_NUMBER_OF_ENEMIES = 55;
        public const char LEVEL_5_ENEMY_CHAR = Enemy.DEFAULT_ENEMY_CHAR;
        public const ConsoleColor LEVEL_5_ENEMY_FORE_COLOR = Enemy.DEFAULT_ENEMY_FORECOLOR;
        public const ConsoleColor LEVEL_5_ENEMY_BACK_COLOR = Enemy.DEFAULT_ENEMY_BACKCOLOR;
        //level 5 difficulty parameter
        public const int LEVEL_5_DIFFICULTY = 250;


    }
}
