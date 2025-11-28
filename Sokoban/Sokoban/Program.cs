using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Sokoban
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            int y = 10;
            {

                Console.Clear();
                //색깔 조정
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                //제목 수정
                Console.Title = "Console SOKOBAN";
                //커서 숨김
                Console.CursorVisible = false;
                //플레이어 "P"를 생성함
                //------------------------------------------------------------------------------------------------
                while (true)
                {
                    Console.Clear();

                    Console.SetCursorPosition(x, y);
                    Console.Write("P");

                    ConsoleKeyInfo KeyInfo = Console.ReadKey();

                    if (KeyInfo.Key == ConsoleKey.DownArrow)
                    {
                        y += 1;
                    }
                    else if (KeyInfo.Key == ConsoleKey.UpArrow)
                    {
                        y -= 1;
                    }
                    else if (KeyInfo.Key == ConsoleKey.RightArrow)
                    {
                        x += 1;
                    }
                    else if (KeyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        x -= 1;
                    }
                    //-------------------------------------------------------------------------------------------------

                    

                }

            }

        }
    }
}
