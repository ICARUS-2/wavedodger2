using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace WaveDodger2
{
    class Program
    {
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
                while(!exitGame)
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
            bool validKey = false;
            ConsoleKey userKey = ConsoleKey.NoName;
            WriteLine("Press 1 to start new game");
            WriteLine("Press 2 to start current round: {0}", currentLevelIndex + 1);
            WriteLine("Press 3 to exit game");

            while (!validKey)
            {
                while (KeyAvailable)
                {
                    userKey = ReadKey(true).Key;
                    if (userKey == ConsoleKey.D1 || userKey == ConsoleKey.D2 || userKey == ConsoleKey.D3)
                        validKey = true;
                }
            }
            gameInProgress = true;
            switch (userKey)
            {
                case ConsoleKey.D1:
                    currentLevelIndex = 0;
                    break;

                case ConsoleKey.D3:
                    Environment.Exit(0);
                    break;
                    
            }

        }

        static void WinScreen(int currentLevelIndex, int numberOfLevels)
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
                WriteLine("                                                                                   ");
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
        }

        static void NewGame(Level current, ref int currentLevelIndex, int numberOfLevels, ref bool gameInProgress)
        {
            bool cycleCollision;
            bool win = false;
            int difficultyCounter = 0;
            ConsoleKey userKey;
            ConsoleKey pauseMenuKey;

            PrepareLevel(current);
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
                            case ConsoleKey.D1:
                                Clear();
                                current.Player1.ResetStats();
                                Coin.Reset(current.Coins);
                                PrepareLevel(current);
                                break;

                            case ConsoleKey.D2:
                                Clear();
                                current.Player1.ResetStats();
                                Coin.Reset(current.Coins);
                                gameInProgress = false;
                                return;

                            case ConsoleKey.D3:
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

            current.Player1.ResetStats();
            Coin.Reset(current.Coins);
            Clear();
            if (win)
            {
                if (currentLevelIndex != numberOfLevels - 1)
                {
                    currentLevelIndex++;
                    WinScreen(currentLevelIndex, numberOfLevels);
                }
                else
                {
                    gameInProgress = false;
                }
            }
            else
            {
                gameInProgress = false;
                Clear();
            }
        }
    }//end of class
}//end of namespace
