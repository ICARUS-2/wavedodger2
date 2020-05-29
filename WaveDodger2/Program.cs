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
            Player player1 = new Player();
            GameArea area = new GameArea();
            area.Render();
            ConsoleKey userKey;
            player1.InitializePosition(area);
            while (1 < 2)
            {
                while (KeyAvailable)
                {
                    userKey = ReadKey(true).Key;
                    player1.Move(userKey, area);
                    player1.Draw(area);
                }//end of inner while
            }//end of outer while
        }//end of method
    }//end of class
}//end of namespace
