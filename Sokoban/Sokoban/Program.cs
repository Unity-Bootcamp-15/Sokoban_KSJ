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
            int mapSizeMinX = 0;
            int mapSizeMinY = 0;
            int mapSizeMaxX = 20;
            int mapSizeMaxY = 10;

            int PlayerX = 5;
            int PlayerY = 10;

            int WallX = 3;
            int WallY = 3;
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



                    Console.SetCursorPosition(PlayerX, PlayerY);
                    Console.Write("P");

                    Console.SetCursorPosition(WallX, WallY);
                    Console.WriteLine("ㅁ");

                    ConsoleKeyInfo KeyInfo = Console.ReadKey();

                    switch (KeyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:

                            if (PlayerY > 0)
                            {
                                PlayerY -= 1;
                            }
                            break;

                        case ConsoleKey.DownArrow:

                            PlayerY += 1;
                            break;

                        case ConsoleKey.LeftArrow:

                            if (PlayerX > 0)
                            {
                                PlayerX -= 1;
                            }
                            break;

                        case ConsoleKey.RightArrow:

                            PlayerX += 1;
                            break;
                    }
                    //-------------------------------------------------------------------------------------------------
                    //플레이어랑 벽이 충돌했는가? => (플레이어 좌표) == (벽 좌표)
                    bool isPlayerXAndSameWallx = PlayerX == WallX;
                    bool isPlayerYAndSameWallY = PlayerY == WallY;
                    bool isCollidedPlayerWithWall = isPlayerXAndSameWallx && isPlayerYAndSameWallY;

                    if (isCollidedPlayerWithWall)
                    {
                        Console.SetCursorPosition(20, 20);
                        Console.WriteLine("충돌함");

                        PlayerX = PlayerX + 1;
                        PlayerX = PlayerX - 1;

                        //콘솔의 맵 밖을 벗어나지 못하게
                        //플레이어가 박스를 밀면 밀린다 (벽에 가로막힘)
                        //플레이어가 박스를 밀어서 골인지점에 넣으면 끝남
                        //플레이어가 박스를 통과할수없음 (충돌)
                        
                    }

                }

            }

        }
    }
}
