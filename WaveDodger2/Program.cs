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
                Test();
            }
            catch (Exception ex)
            {
                Clear();
                SetCursorPosition(0,0);
                ForegroundColor = ConsoleColor.DarkRed;
                Write("ERROR: EXCEPTION THROWN");
                Write(ex.StackTrace);
                WriteLine("\n\nDETAILS: {0}", ex.Message);
                ReadKey();
            }
        }
        private static void Maximize()
        {
            //Sourced from https://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp/22053200
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        static void Test()
        {
            int numberOfCoins = 1;
            int numberOfEnemies = 2;
            int difficultyCounter = 0;
            int difficulty = 100;
            int testLoopCounter = 0;
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
            while (1 < 2)
            {
                testLoopCounter++;
                difficultyCounter++;
                Maximize();
                area.UpdateDisplay(player1, coins);
                while (KeyAvailable)
                {
                    userKey = ReadKey(true).Key;
                    player1.Move(userKey, area);
                }//end of inner while
                player1.Draw(area);
                player1.CheckCollision(enemies, coins);

                if (difficultyCounter == difficulty)
                {
                    Enemy.MoveEnemies(enemies, area, rnd);
                    Enemy.Render(enemies, area, coins, rnd);
                    difficultyCounter = 0;
                }

                if (player1.CoinsCollected == numberOfCoins)
                    Environment.Exit(0);
            }//end of outer while
        }//end of method
    }//end of class
}//end of namespace
