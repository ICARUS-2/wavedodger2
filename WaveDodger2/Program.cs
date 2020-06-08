using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;
namespace WaveDodger2
{
    class Program
    {
        const ConsoleKey HOW_TO_PLAY = ConsoleKey.D0;
        const ConsoleKey NEW_GAME = ConsoleKey.D1;
        const ConsoleKey CURRENT_ROUND = ConsoleKey.D2;
        const ConsoleKey EDITOR_MODE = ConsoleKey.D3;
        const ConsoleKey EXIT_TO_DESKTOP = ConsoleKey.D4;

        const ConsoleKey SIMPLE_EDITOR = ConsoleKey.D1;
        const ConsoleKey ADVANCED_EDITOR = ConsoleKey.D2;
        const ConsoleKey EXIT_EDITOR = ConsoleKey.D3;

        const ConsoleKey CHOICE_1 = ConsoleKey.D1;
        const ConsoleKey CHOICE_2 = ConsoleKey.D2;

        const ConsoleKey PAUSE_RESTART = ConsoleKey.D1;
        const ConsoleKey PAUSE_QUIT_TO_MENU = ConsoleKey.D2;
        const ConsoleKey PAUSE_QUIT_TO_DESKTOP = ConsoleKey.D3;

        const ConsoleKey WINSCREEN_EXIT = ConsoleKey.Enter;

        const ConsoleColor EDITOR_COLOR = ConsoleColor.Cyan;

        const int EDITOR_MIN_WIDTH = 20;
        const int EDITOR_MAX_WIDTH = 150;
        const int EDITOR_MIN_HEIGHT = 15;
        const int EDITOR_MAX_HEIGHT = 50;
        const int EDITOR_MIN_COINS = 1;
        const int EDITOR_MAX_COINS = 50;
        const int EDITOR_MIN_ENEMIES = 1;
        const int EDITOR_MAX_ENEMIES = 200;
        const int EDITOR_MIN_DIFFICULTY = 100;
        const int EDITOR_MAX_DIFFICULTY = 4000;
        const int HEIGHT_OFFSET = 3;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
        static void Main(string[] args)
        {
            try
            {
                bool exitGame = false;
                bool gameInProgress = true;
                int currentLevelIndex = 0;
                Level[] levels = Level.GenerateLevels();
                CursorVisible = false;
                Maximize();
                while (!exitGame)
                {
                    TitleScreen(ref currentLevelIndex, ref gameInProgress);
                    while (gameInProgress)
                    {
                        NewGame(levels[currentLevelIndex], ref currentLevelIndex, levels.Length, ref gameInProgress);
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayCrashInfo(ex);
            }
        }

        private static void Maximize()
        {
            //Sourced from https://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp/22053200
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        static void TitleScreen(ref int currentLevelIndex, ref bool gameInProgress)
        {
            SoundPlayer titleMusic = new SoundPlayer();
            titleMusic.SoundLocation = @"..\..\sound\l0-365.wav";
            titleMusic.PlayLooping();
            int titleY = 10;
            int menuX = 85;
            int menuY = 25;
            int changeColorValue = 1000;
            int changeColorCounter = changeColorValue - 1;
            int rgbCycleCounter = 1;
            bool validKey = false;
            ConsoleKey userKey = ConsoleKey.NoName;

            while (!validKey)
            {
                changeColorCounter++;
                SetCursorPosition(0, titleY);
                CursorVisible = false;
                switch (rgbCycleCounter) //switch statement controls the RGB
                {
                    case 1:
                        ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case 2:
                        ForegroundColor = ConsoleColor.Red;
                        break;
                    case 3:
                        ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 4:
                        ForegroundColor = ConsoleColor.Green;
                        break;
                    case 5:
                        ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case 6:
                        ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    case 7:
                        ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case 8:
                        ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case 9:
                        ForegroundColor = ConsoleColor.Magenta;
                        rgbCycleCounter = 0;
                        break;
                }
                if (changeColorCounter == changeColorValue)
                {
                    WriteLine("                                   ___       __   ________  ___      ___ _______   ________  ________  ________  ________  _______   ________          ___  ___    ");
                    WriteLine("                                  |\\  \\     |\\  \\|\\   __  \\|\\  \\    /  /|\\  ___ \\ |\\   ___ \\|\\   __  \\|\\   ___ \\|\\   ____\\|\\  ___ \\ |\\   __  \\        |\\  \\|\\  \\ ");
                    WriteLine("                                  \\ \\  \\    \\ \\  \\ \\  \\|\\  \\ \\  \\  /  / | \\   __/|\\ \\  \\_|\\ \\ \\  \\|\\  \\ \\  \\_|\\ \\ \\  \\___|\\ \\   __/|\\ \\  \\|\\  \\       \\ \\  \\ \\  \\ ");
                    WriteLine("                                   \\ \\  \\  __\\ \\  \\ \\   __  \\ \\  \\/  / / \\ \\  \\_|/_\\ \\  \\ \\\\ \\ \\  \\\\\\  \\ \\  \\ \\\\ \\ \\  \\  __\\ \\  \\_|/_\\ \\   _  _\\       \\ \\  \\ \\  \\");
                    WriteLine("                                    \\ \\  \\|\\__\\_\\  \\ \\  \\ \\  \\ \\    / /   \\ \\  \\_|\\ \\ \\  \\_\\\\ \\ \\  \\\\\\  \\ \\  \\_\\\\ \\ \\  \\|\\  \\ \\  \\_|\\ \\ \\  \\\\  \\|       \\ \\  \\ \\  \\");
                    WriteLine("                                     \\ \\____________\\ \\__\\ \\__\\ \\__/ /     \\ \\_______\\ \\_______\\ \\_______\\ \\_______\\ \\_______\\ \\_______\\ \\__\\\\ _\\        \\ \\__\\ \\__\\");
                    WriteLine("                                      \\|____________|\\|__|\\|__|\\|__|/       \\|_______|\\|_______|\\|_______|\\|_______|\\|_______|\\|_______|\\|__|\\|__|        \\|__|\\|__|");
                    changeColorCounter = 0;
                    rgbCycleCounter++;
                }
                ForegroundColor = ConsoleColor.White;
                SetCursorPosition(menuX, menuY);
                WriteLine("Press 0 for How to Play");

                SetCursorPosition(menuX, menuY + 2);
                WriteLine("Press 1 to start new game");

                SetCursorPosition(menuX, menuY + 4);
                WriteLine("Press 2 to start current round: {0}", currentLevelIndex + 1);

                SetCursorPosition(menuX, menuY + 6);
                WriteLine("Press 3 for Custom Level Editor");

                SetCursorPosition(menuX, menuY + 8);
                WriteLine("Press 4 to Exit to Desktop");

                SetCursorPosition(menuX, menuY + 14);
                WriteLine("Developed by Ethan Briffett");

                SetCursorPosition(menuX, menuY + 16);
                WriteLine("Soundtrack by Darnu-Pop, Studio Megaane, 8 Bit Universe, JHN Studio, Nintendo");
                while (KeyAvailable)
                {
                    userKey = ReadKey(true).Key;
                    if (userKey == NEW_GAME || userKey == CURRENT_ROUND || userKey == EXIT_TO_DESKTOP)
                        validKey = true;

                    if (userKey == HOW_TO_PLAY)
                    {
                        Instructions(menuX, menuY);
                    }

                    if (userKey == EDITOR_MODE)
                    {
                        LevelEditorMode();
                    }
                }
            }
            gameInProgress = true;
            switch (userKey)
            {
                case NEW_GAME:
                    currentLevelIndex = 0;
                    break;

                case EXIT_TO_DESKTOP:
                    Environment.Exit(0);
                    break;

            }
            titleMusic.Stop();
            Clear();
        }

        static void Instructions(int menuX, int menuY)
        {
            CursorVisible = false;
            ForegroundColor = ConsoleColor.White;
            int space = 2;
            menuY = 30;
            Clear();
            SetCursorPosition(menuX, menuY);
            WriteLine("INSTRUCTIONS");

            menuY += space;
            SetCursorPosition(menuX, menuY);

            WriteLine("Your player is: {0}", Player.DEFAULT_PLAYER_CHAR);

            menuY += space;
            SetCursorPosition(menuX, menuY);

            WriteLine("Dodge the waves of enemies ({0})", Enemy.DEFAULT_ENEMY_CHAR);

            menuY += space;
            SetCursorPosition(menuX, menuY);

            WriteLine("Collect all the coins ({0})", Coin.DEFAULT_COIN_CHAR);

            menuY += space;
            SetCursorPosition(menuX, menuY);

            WriteLine("and most importantly... DONT FUCKING DIE... have fun :)");

            menuY += space;
            SetCursorPosition(menuX, menuY);

            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("Press enter to return to menu");
            ForegroundColor = ConsoleColor.Black;
            ReadLine();
            ResetColor();
            Clear();
        }

        static void MoveToNextRoundScreen(int currentLevelIndex, int numberOfLevels)
        {
            int delay = 1000;
            int countdown = 3;
            int cursorX = 50;
            int cursorY = 25;
            Clear();
            if (currentLevelIndex != numberOfLevels)
            {
                ForegroundColor = ConsoleColor.Green;
                while (countdown != 0)
                {
                    SetCursorPosition(cursorX, cursorY);
                    Write("\t\t\t\t\t\tROUND COMPLETE, MOVING TO ROUND {0} IN {1}", currentLevelIndex + 1, countdown);
                    Beep();
                    Thread.Sleep(delay);
                    countdown--;
                    Clear();
                }//end of while loop
                while(KeyAvailable)
                {
                    ReadKey(false);
                }
            }//end of if statement
        }

        static void WinScreen()
        {
            SoundPlayer victoryMusic = new SoundPlayer();
            victoryMusic.SoundLocation = @"..\..\sound\victorytheme.wav";
            victoryMusic.Play();

            int titleY = 10;
            int menuX = 85;
            int menuY = 25;
            int changeColorValue = 1000;
            int changeColorCounter = changeColorValue - 1;
            int rgbCycleCounter = 1;
            bool validKey = false;
            ConsoleKey userKey = ConsoleKey.NoName;

            while (!validKey)
            {
                changeColorCounter++;
                SetCursorPosition(0, titleY);
                CursorVisible = false;
                switch (rgbCycleCounter) //switch statement controls the RGB
                {
                    case 1:
                        ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case 2:
                        ForegroundColor = ConsoleColor.Red;
                        break;
                    case 3:
                        ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 4:
                        ForegroundColor = ConsoleColor.Green;
                        break;
                    case 5:
                        ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case 6:
                        ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    case 7:
                        ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case 8:
                        ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case 9:
                        ForegroundColor = ConsoleColor.Magenta;
                        rgbCycleCounter = 0;
                        break;
                }
                if (changeColorCounter == changeColorValue)
                {
                    WriteLine("                                                              oooooo     oooo ooooo   .oooooo.   ooooooooooooo   .oooooo.   ooooooooo.   oooooo   oooo   ");
                    WriteLine("                                                               `888.     .8'  `888'  d8P'  `Y8b  8'   888   `8  d8P'  `Y8b  `888   `Y88.  `888.   .8' ");
                    WriteLine("                                                                `888.   .8'    888  888               888      888      888  888   .d88'   `888. .8'  ");
                    WriteLine("                                                                 `888. .8'     888  888               888      888      888  888ooo88P'     `888.8'  ");
                    WriteLine("                                                                  `888.8'      888  888               888      888      888  888`88b.        `888' ");
                    WriteLine("                                                                   `888'       888  `88b    ooo       888      `88b    d88'  888  `88b.       888   ");
                    WriteLine("                                                                    `8'       o888o  `Y8bood8P'      o888o      `Y8bood8P'  o888o  o888o     o888o   ");
                    changeColorCounter = 0;
                    rgbCycleCounter++;
                }
                ForegroundColor = ConsoleColor.White;
                SetCursorPosition(menuX, menuY);
                WriteLine(" Press enter to return to return to main menu");
                while (KeyAvailable)
                {
                    userKey = ReadKey(true).Key;
                    if (userKey == WINSCREEN_EXIT)
                        validKey = true;
                }
            }
            victoryMusic.Stop();
            Clear();
        }

        static void DeathScreen(ref bool gameInProgress)
        {
            int menuYPos = 20;
            int youFuckingDiedYPos = 10;
            bool validKey = false;
            int colorCounter = 0;
            int blinkCounter = 0;
            int blinkCounterMax = 600;
            ConsoleKey userNumPress = ConsoleKey.NoName;

            ForegroundColor = ConsoleColor.White;
            Clear();
            SetCursorPosition(0, menuYPos);
            WriteLine("                                                 PRESS 1 TO RESTART CURRENT ROUND");
            WriteLine("                                                 PRESS 2 TO EXIT TO MAIN MENU");
            WriteLine("                                                 PRESS 3 TO EXIT TO DESKTOP");
            ForegroundColor = ConsoleColor.DarkRed;
            do
            {
                SetCursorPosition(0, youFuckingDiedYPos);
                WriteLine("                     ██╗   ██╗ ██████╗ ██╗   ██╗    ███████╗██╗   ██╗ ██████╗██╗  ██╗██╗███╗   ██╗ ██████╗     ██████╗ ██╗███████╗██████╗");
                WriteLine("                     ╚██╗ ██╔╝██╔═══██╗██║   ██║    ██╔════╝██║   ██║██╔════╝██║ ██╔╝██║████╗  ██║██╔════╝     ██╔══██╗██║██╔════╝██╔══██╗");
                WriteLine("                      ╚████╔╝ ██║   ██║██║   ██║    █████╗  ██║   ██║██║     █████╔╝ ██║██╔██╗ ██║██║  ███╗    ██║  ██║██║█████╗  ██║  ██║");
                WriteLine("                       ╚██╔╝  ██║   ██║██║   ██║    ██╔══╝  ██║   ██║██║     ██╔═██╗ ██║██║╚██╗██║██║   ██║    ██║  ██║██║██╔══╝  ██║  ██║");
                WriteLine("                        ██║   ╚██████╔╝╚██████╔╝    ██║     ╚██████╔╝╚██████╗██║  ██╗██║██║ ╚████║╚██████╔╝    ██████╔╝██║███████╗██████╔╝");
                WriteLine("                        ╚═╝    ╚═════╝  ╚═════╝     ╚═╝      ╚═════╝  ╚═════╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝     ╚═════╝ ╚═╝╚══════╝╚═════╝");

                if (blinkCounter == blinkCounterMax)
                {
                    blinkCounter = 0;
                    switch (colorCounter)
                    {
                        case 0:
                            ForegroundColor = ConsoleColor.DarkRed;
                            colorCounter++;
                            break;
                        case 1:
                            ForegroundColor = ConsoleColor.Black;
                            colorCounter = 0;
                            break;
                    }//end of switch statement
                }//end of if statement
                if (KeyAvailable)
                {
                    userNumPress = ReadKey(true).Key;

                    switch (userNumPress)
                    {
                        case ConsoleKey.D1:
                            gameInProgress = true;
                            validKey = true;
                            break;

                        case ConsoleKey.D2:
                            gameInProgress = false;
                            validKey = true;
                            break;

                        case ConsoleKey.D3:
                            Environment.Exit(0);
                            break;
                    }


                }//end of if statement
                blinkCounter++;
            } while (!validKey); //end of loop
            ResetColor();
            Clear();
        }

        static ConsoleKey Pause(GameArea area)
        {
            ConsoleKey menuKeyPress;
            string pauseMessage = "GAME PAUSED - PRESS ESCAPE TO RETURN TO GAME";
            string menu1 = "PRESS 1 TO RESTART CURRENT ROUND";
            string menu2 = "PRESS 2 TO QUIT TO MENU";
            string menu3 = "PRESS 3 TO QUIT TO DESKTOP";

            int space = 2; 
            int heightOffset = 10;
            int numberOfMessasges = 4;
            int cursorX = (area.Width + 2 * area.BorderWidth) / 2;
            int initialCursorY = area.Height + heightOffset;
            int cursorY = initialCursorY;

            ForegroundColor = ConsoleColor.White;

            SetCursorPosition(cursorX - (pauseMessage.Length / 2), cursorY);
            WriteLine(pauseMessage);
            cursorY += space;

            SetCursorPosition(cursorX - (menu1.Length / 2), cursorY);
            WriteLine(menu1);
            cursorY += space;

            SetCursorPosition(cursorX - (menu2.Length / 2), cursorY);
            WriteLine(menu2);
            cursorY += space;

            SetCursorPosition(cursorX - (menu3.Length / 2), cursorY);
            WriteLine(menu3);

            menuKeyPress = ReadKey(true).Key;

            SetCursorPosition(0, initialCursorY);
            for(int i = 0; i < numberOfMessasges*2; i++)
            {
                WriteLine("                                                                                                                                                                                                                                                                      ");
            }
            ResetColor();

            return menuKeyPress;
        }

        static void DisplayCrashInfo(Exception ex)
        {
            ResetColor();
            Clear();
            SetCursorPosition(0, 0);
            ForegroundColor = ConsoleColor.DarkRed;
            Write("ERROR: EXCEPTION THROWN");
            Write(ex.StackTrace);
            WriteLine("\n\nDETAILS: {0}", ex.Message);
            ReadKey();
        }

        static void PrepareLevel(Level current)
        {
            current.Area.Render();
            Coin.Render(current.Coins);
            Enemy.ChangeSide(current.Enemies, current.Area, current.Rnd);
            Enemy.RenderInitial(current.Enemies);
            current.Player1.InitializePosition(current.Area);
            current.Music.PlayLooping();
        }

        static void NewGame(Level current, ref int currentLevelIndex, int numberOfLevels, ref bool gameInProgress)
        {
            bool cycleCollision;
            bool win = false;
            int difficultyCounter = 0;
            ConsoleKey userKey;
            ConsoleKey pauseMenuKey;
            string startMessage = " Press any key to start";
            PrepareLevel(current);
            SetCursorPosition((2 * current.Area.BorderWidth + current.Area.Width) / 2 - startMessage.Length/2, current.Area.DownLimit + HEIGHT_OFFSET);
            WriteLine(startMessage);
            ReadKey();
            SetCursorPosition((2 * current.Area.BorderWidth + current.Area.Width) / 2 - startMessage.Length / 2, current.Area.DownLimit + 3);
            WriteLine("                                                                                                                                                                                         ");
            WriteLine(" ");
            while (current.Player1.LivesRemaining != 0 && current.Player1.CoinsCollected != current.Coins.Length)
            {
                cycleCollision = false;
                difficultyCounter++;
                Maximize();
                CursorVisible = false;
                current.Area.UpdateDisplay(currentLevelIndex, current.Player1, current.Coins);
                while (KeyAvailable) //Check collision with enemies before moving, get the user's key press and move player based on it
                {
                    current.Player1.HitTest(current.Enemies, current.Player1, ref cycleCollision);
                    userKey = ReadKey(true).Key;
                    if (userKey == ConsoleKey.Escape)
                    {
                        pauseMenuKey = Pause(current.Area);
                        switch (pauseMenuKey)
                        {
                            case PAUSE_RESTART:
                                Clear();
                                current.Player1.ResetStats();
                                Coin.Reset(current.Coins);
                                PrepareLevel(current);
                                break;

                            case PAUSE_QUIT_TO_MENU:
                                Clear();
                                current.Player1.ResetStats();
                                Coin.Reset(current.Coins);
                                gameInProgress = false;
                                return;

                            case PAUSE_QUIT_TO_DESKTOP:
                                Environment.Exit(0);
                                break;
                        }
                    }
                    current.Player1.Move(userKey, current.Area);
                    current.Player1.Draw(current.Area);
                }//end of inner while
                current.Player1.CheckCoinCollision(current.Coins);
                if (difficultyCounter == current.Difficulty) //When it is time to move enemies, check their collision and move them
                {
                    current.Player1.HitTest(current.Enemies, current.Player1, ref cycleCollision);
                    Enemy.MoveEnemies(current.Enemies, current.Area, current.Rnd);
                    Enemy.Render(current.Enemies, current.Player1, current.Area, current.Coins, current.Rnd);
                    difficultyCounter = 0;
                }
                if (current.Player1.CoinsCollected == current.Coins.Length)
                    win = true;
            }
            Clear();
            if (win)
            {
                if (currentLevelIndex != numberOfLevels - 1)
                {
                    currentLevelIndex++;
                    MoveToNextRoundScreen(currentLevelIndex, numberOfLevels); //if the user is proceeding to the next level
                }
                else //user completes last level of the game
                {
                    gameInProgress = false;
                    WinScreen();
                }
            }
            else
            {
                gameInProgress = false;
                Clear();
                if (current.Player1.LivesRemaining == 0)
                    DeathScreen(ref gameInProgress);
            }
            current.Music.Stop();
            current.Player1.ResetStats();
            Coin.Reset(current.Coins);
        }//end of method

        #region//DEDICATED LEVEL EDITOR METHODS
        static void LevelEditorMode()
        {
            bool exitEditor = false;
            bool editorRunning = true;
            ConsoleKey userKey;
            Level custom = new Level();
            while (!exitEditor)
            {
                userKey = GetEditorMode();
                switch (userKey)
                {
                    case SIMPLE_EDITOR:
                        custom = SimpleEditor();
                        break;

                    case ADVANCED_EDITOR:
                        custom = AdvancedEditor();
                        break;

                    case EXIT_EDITOR:
                        return;
                }

                editorRunning = true;

                while (editorRunning)
                {
                    NewCustomGame(custom, ref editorRunning);
                }
            }
        }

        static ConsoleKey GetEditorMode()
        {
            Clear();
            ForegroundColor = EDITOR_COLOR;
            WriteLine("===============LEVEL EDITOR===============");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\nSELECT EDITOR MODE:");
            WriteLine("\n1 - SIMPLE EDITOR");
            WriteLine("\n2 - ADVANCED EDITOR");
            WriteLine("\n3 - EXIT TO MENU");
            ConsoleKey userKey = ConsoleKey.NoName;
            bool validKey = false;
            while (!validKey)
            {
                userKey = ReadKey(true).Key;

                if (userKey == SIMPLE_EDITOR || userKey == ADVANCED_EDITOR || userKey == EXIT_EDITOR)
                    validKey = true;
            }
            ResetColor();
            Clear();
            return userKey;
        }

        static Level SimpleEditor()
        {
            int width;
            int height;
            int numberOfCoins;
            int numberOfEnemies;
            int difficulty;

            //get level dimensions
            Clear();
            ForegroundColor = EDITOR_COLOR;
            WriteLine("PLAY AREA INFO:");
            ForegroundColor = ConsoleColor.White;

            WriteLine("\nEnter the width of the play area ({0} - {1})", EDITOR_MIN_WIDTH, EDITOR_MAX_WIDTH);
            width = ValInt(EDITOR_MIN_WIDTH, EDITOR_MAX_WIDTH, String.Format("ERROR: WIDTH CAN ONLY BE AN INTEGER BETWEEN {0} AND {1}", EDITOR_MIN_WIDTH, EDITOR_MAX_WIDTH)) ;

            WriteLine("\nEnter the height of the play area ({0} - {1})", EDITOR_MIN_HEIGHT, EDITOR_MAX_HEIGHT);
            height = ValInt(EDITOR_MIN_HEIGHT, EDITOR_MAX_HEIGHT, String.Format("ERROR: HEIGHT CAN ONLY BE AN INTEGER BETWEEN {0} AND {1}", EDITOR_MIN_HEIGHT, EDITOR_MAX_HEIGHT));

            Clear();

            //get coin amount
            ForegroundColor = EDITOR_COLOR;
            WriteLine("COIN INFO");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\nEnter the number of coins in the game ({0} - {1})", EDITOR_MIN_COINS, EDITOR_MAX_COINS);

            numberOfCoins = ValInt(EDITOR_MIN_COINS, EDITOR_MAX_COINS, String.Format("ERROR: NUMBER OF COINS CAN ONLY BE AN INTEGER BETWEEN {0} AND {1}", EDITOR_MIN_COINS, EDITOR_MAX_COINS));

            //get enemy info
            Clear();
            ForegroundColor = EDITOR_COLOR;
            WriteLine("ENEMY INFO");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\nEnter the number of enemies in the game ({0} - {1})", EDITOR_MIN_ENEMIES, EDITOR_MAX_ENEMIES);

            numberOfEnemies = ValInt(EDITOR_MIN_ENEMIES, EDITOR_MAX_ENEMIES, String.Format("ERROR: NUMBER OF ENEMIES CAN ONLY BE AN INTEGER BETWEEN {0} AND {1}", EDITOR_MIN_ENEMIES, EDITOR_MAX_ENEMIES));

            Clear();

            ForegroundColor = EDITOR_COLOR;
            WriteLine("DIFFICULTY");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\nEnter the difficulty setting (lower = slower enemies) ({0} - {1})", EDITOR_MIN_DIFFICULTY, EDITOR_MAX_DIFFICULTY);

            difficulty = ValInt(EDITOR_MIN_DIFFICULTY, EDITOR_MAX_DIFFICULTY, String.Format("ERROR: DIFFICULTY SETTING CAN ONLY BE BETWEEN {0} AND {1}", EDITOR_MIN_ENEMIES, EDITOR_MAX_ENEMIES));

            ResetColor();
            Clear();
            return new Level(width, height, numberOfCoins, numberOfEnemies, difficulty);
        }

        static Level AdvancedEditor()
        {
            throw new NotImplementedException();
        }

        static void NewCustomGame(Level custom, ref bool editorRunning)
        {
            bool cycleCollision;
            bool win = false;
            int difficultyCounter = 0;
            ConsoleKey userKey;
            ConsoleKey pauseMenuKey;
            string startMessage =  "Press any key to start";
            PrepareLevel(custom);
            SetCursorPosition((2 * custom.Area.BorderWidth + custom.Area.Width) / 2 - startMessage.Length / 2, custom.Area.DownLimit + HEIGHT_OFFSET);
            WriteLine(startMessage);
            ReadKey();
            SetCursorPosition((2 * custom.Area.BorderWidth + custom.Area.Width) / 2 - startMessage.Length / 2, custom.Area.DownLimit + HEIGHT_OFFSET);
            WriteLine("                                                                                                                                                                                         ");
            WriteLine(" ");
            while (custom.Player1.LivesRemaining != 0 && custom.Player1.CoinsCollected != custom.Coins.Length)
            {
                cycleCollision = false;
                difficultyCounter++;
                Maximize();
                CursorVisible = false;
                custom.Area.UpdateDisplay(1, custom.Player1, custom.Coins);
                while (KeyAvailable) //Check collision with enemies before moving, get the user's key press and move player based on it
                {
                    custom.Player1.HitTest(custom.Enemies, custom.Player1, ref cycleCollision);
                    userKey = ReadKey(true).Key;
                    if (userKey == ConsoleKey.Escape)
                    {
                        pauseMenuKey = Pause(custom.Area);
                        switch (pauseMenuKey)
                        {
                            case PAUSE_RESTART:
                                Clear();
                                custom.Player1.ResetStats();
                                Coin.Reset(custom.Coins);
                                PrepareLevel(custom);
                                break;

                            case PAUSE_QUIT_TO_MENU:
                                Clear();
                                custom.Player1.ResetStats();
                                Coin.Reset(custom.Coins);
                                custom.Music.Stop();
                                editorRunning = false;
                                return;

                            case PAUSE_QUIT_TO_DESKTOP:
                                Environment.Exit(0);
                                break;
                        }
                    }
                    custom.Player1.Move(userKey, custom.Area);
                    custom.Player1.Draw(custom.Area);
                }//end of inner while
                custom.Player1.CheckCoinCollision(custom.Coins);
                if (difficultyCounter == custom.Difficulty) //When it is time to move enemies, check their collision and move them
                {
                    custom.Player1.HitTest(custom.Enemies, custom.Player1, ref cycleCollision);
                    Enemy.MoveEnemies(custom.Enemies, custom.Area, custom.Rnd);
                    Enemy.Render(custom.Enemies, custom.Player1, custom.Area, custom.Coins, custom.Rnd);
                    difficultyCounter = 0;
                }
                if (custom.Player1.CoinsCollected == custom.Coins.Length)
                    win = true;
            }

            Clear();
            if (win)
            {
                editorRunning = EditorWinOption(custom.Area);
                //display the win screen
            }
            else
            {
                editorRunning = false;
                Clear();
                if (custom.Player1.LivesRemaining == 0)
                    DeathScreen(ref editorRunning);
            }
            custom.Music.Stop();
            custom.Player1.ResetStats();
            Coin.Reset(custom.Coins);
        }

        static bool EditorWinOption(GameArea area)
        {
            Clear();
            string message1 = "CUSTOM LEVEL COMPLETE";
            string message2 = "Press 1 to restart level";
            string message3 = "Press 2 to exit to menu";
            ConsoleKey userKey = ConsoleKey.NoName;
            bool validKey = false;

            WriteLine(message1);
            WriteLine(message2);
            WriteLine(message3);

            while (!validKey)
            {
                userKey = ReadKey(true).Key;

                if (userKey == CHOICE_1 || userKey == CHOICE_2)
                    validKey = true;
            }

            return (userKey == CHOICE_1);
        }
        #endregion

        #region//INPUT VALIDATORS
        static ConsoleKey TwoKeyChoices()
        {
            ConsoleKey userKey = ConsoleKey.NoName;
            bool validKey = false;

            while (!validKey)
            {
                userKey = ReadKey(true).Key;
                if (userKey == CHOICE_1 || userKey == CHOICE_2)
                    validKey = true;
            }

            return userKey;
        }

        static int ValInt(int minValue, int maxValue, string errorMessage)
        {
            bool isInt;
            int userNumPress;
            do
            {
                isInt = int.TryParse(Console.ReadLine(), out userNumPress);
                if (!isInt | userNumPress < minValue | userNumPress > maxValue)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("{0}", errorMessage);
                    Console.ResetColor();
                }
            } while (!isInt | userNumPress < minValue | userNumPress > maxValue);
            return userNumPress;
        }//end of function
        #endregion 
    }//end of class
}//end of namespace

/*
 * CREDITS:
 DEVELOPED BY ETHAN BRIFFETT
 PROJ3CT DA3DALU5 SOFTWARES

 SOUNDTRACK CREDITS:
 Darnu-Pop 
 Studio Megaane
 8 Bit Universe
 JHN Studio
 * */
