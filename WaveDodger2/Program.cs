using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;


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
                Maximize();
                CursorVisible = false;
                //MainGameTesting();
                LevelGeneratorTesting();
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

        static void MainGameTesting()
        {
            int numberOfCoins = 1;
            int numberOfEnemies = 50;
            int difficultyCounter = 0;
            int difficulty = 100;
            int testLoopCounter = 0;
            bool cycleCollision = false;
            Random rnd = new Random();
            Player player1 = new Player();
            GameArea area = new GameArea();
            Enemy[] enemies = Enemy.GetArrayOfEnemies(numberOfEnemies, area);
            Coin[] coins = Coin.GenerateCoinArray(numberOfCoins, rnd, area, player1);
            area.Render();
            Coin.Render(coins);
            Enemy.ChangeSide(enemies, area, rnd);
            Enemy.RenderInitial(enemies);
            ConsoleKey userKey;
            player1.InitializePosition(area);
            while (player1.LivesRemaining > 0)
            {
                cycleCollision = false;
                testLoopCounter++;
                difficultyCounter++;
                Maximize();
                area.UpdateDisplay(player1, coins);
                while (KeyAvailable)
                {
                    player1.HitTest(enemies, player1, ref cycleCollision);
                    userKey = ReadKey(true).Key;
                    player1.Move(userKey, area);
                    player1.Draw(area);
                }//end of inner while
                player1.CheckCoinCollision(coins);
                if (difficultyCounter == difficulty)
                {
                    player1.HitTest(enemies, player1, ref cycleCollision);
                    Enemy.MoveEnemies(enemies, area, rnd);
                    Enemy.Render(enemies, player1, area, coins, rnd);
                    difficultyCounter = 0;
                }
                if (player1.CoinsCollected == numberOfCoins)
                    Environment.Exit(0);
            }//end of outer while
        }//end of method

        static void LevelGeneratorTesting()
        {
            Level[] levels = Level.GenerateLevels();
            NewGame(levels[1]);
        }

        static void PrepareLevel(Level current)
        {
            current.Area.Render();
            Coin.Render(current.Coins);
            Enemy.ChangeSide(current.Enemies, current.Area, current.Rnd);
            Enemy.RenderInitial(current.Enemies);
            current.Player1.InitializePosition(current.Area);
        }

        static void NewGame(Level current)
        {
            int currentLevel = 3;
            bool cycleCollision;
            int difficultyCounter = 0;
            ConsoleKey userKey;

            PrepareLevel(current);
            while (current.Player1.LivesRemaining != 0)
            {
                cycleCollision = false;
                difficultyCounter++;
                Maximize();
                current.Area.UpdateDisplay(current.Player1, current.Coins);
                while (KeyAvailable)
                {
                    current.Player1.HitTest(current.Enemies, current.Player1, ref cycleCollision);
                    userKey = ReadKey(true).Key;
                    current.Player1.Move(userKey, current.Area);
                    current.Player1.Draw(current.Area);
                }//end of inner while
                current.Player1.CheckCoinCollision(current.Coins);
                if (difficultyCounter == current.Difficulty)
                {
                    current.Player1.HitTest(current.Enemies, current.Player1, ref cycleCollision);
                    Enemy.MoveEnemies(current.Enemies, current.Area, current.Rnd);
                    Enemy.Render(current.Enemies, current.Player1, current.Area, current.Coins, current.Rnd);
                    difficultyCounter = 0;
                }
                if (current.Player1.CoinsCollected == current.Coins.Length)
                    Environment.Exit(0);
            }
        }
    }//end of class
}//end of namespace
