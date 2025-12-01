using System;
using System.Text;

namespace ConsoleSokoban
{
    internal class Program
    {
        // 맵 요소 정의 (문자 상수)
        const char WALL = 'ㅁ';     // 벽
        const char PLAYER = 'P';   // 플레이어
        const char BOX = '$';      // 상자
        const char GOAL = 'O';     // 목표 지점 (단순화)
        const char EMPTY = ' ';    // 빈 공간

        // 맵 설계도 (char 배열로 정의하여 개별 문자 수정 가능하도록 함)
        static char[,] map = new char[,]
        {
            {'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ'}, // 0
            {'ㅁ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'ㅁ'}, // 1
            {'ㅁ', ' ', '$', ' ', 'ㅁ', 'O', ' ', ' ', ' ', 'ㅁ'}, // 2 (상자 1개, 목표 1개)
            {'ㅁ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'ㅁ'}, // 3
            {'ㅁ', 'P', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'ㅁ'}, // 4 (플레이어 시작 위치)
            {'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ', 'ㅁ'}  // 5
        };

        static int playerX = 1; // 플레이어 시작 X 좌표
        static int playerY = 4; // 플레이어 시작 Y 좌표
        static int mapWidth = map.GetLength(1); // 맵의 가로 길이 (10)
        static int mapHeight = map.GetLength(0); // 맵의 세로 길이 (6)

        static void Main(string[] args)
        {
            // 콘솔 환경 설정
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Console SOKOBAN (상자 밀기)";
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            // 메인 게임 루프 시작
            while (true)
            {
                // 1. 화면 출력 (Draw)
                DrawMap();

                // 2. 키 입력 및 이동 (Input & Update)
                if (ProcessInput())
                {
                    // 3. 맵 상태 업데이트 (플레이어 위치를 맵에 반영)
                    UpdateMap();
                }
            }
        }

        static void DrawMap()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            // char[,] 배열 전체를 반복하며 출력 (가장 효율적인 맵 출력)
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    // 문자 종류에 따라 색상 변경 (가독성 향상)
                    switch (map[y, x])
                    {
                        case WALL:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case PLAYER:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case BOX:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case GOAL:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write(map[y, x]);
                }
                Console.WriteLine();
            }
            // 플레이어의 실제 위치를 맵 상에 다시 한 번 정확히 그림
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(PLAYER);
        }

        static void UpdateMap()
        {
            // 맵 업데이트 로직: 플레이어의 이전 위치는 빈 공간으로 만들어줍니다.
            // (이 코드는 이동 로직 안에서 이미 처리되므로 여기서는 생략)
        }

        static bool ProcessInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // 다음 위치를 미리 계산
            int nextX = playerX;
            int nextY = playerY;

            // 다다음 위치를 미리 계산 (상자를 밀 때 필요)
            int nextNextX = playerX;
            int nextNextY = playerY;

            // 1. 키 입력에 따른 다음 좌표 계산
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    nextY--;
                    nextNextY -= 2;
                    break;
                case ConsoleKey.DownArrow:
                    nextY++;
                    nextNextY += 2;
                    break;
                case ConsoleKey.LeftArrow:
                    nextX--;
                    nextNextX -= 2;
                    break;
                case ConsoleKey.RightArrow:
                    nextX++;
                    nextNextX += 2;
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0); // ESC로 프로그램 종료
                    return false;
                default:
                    return false; // 다른 키는 무시
            }

            // 2. 벽 충돌 체크 및 이동/상자 밀기 로직 (소코반 핵심)

            // ⭐️ 다음 타일 확인
            char nextTile = map[nextY, nextX];

            if (nextTile == WALL)
            {
                // 벽이라면 이동을 막고 false 반환
                return false;
            }
            else if (nextTile == BOX)
            {
                // 상자라면 다다음 타일 확인
                char nextNextTile = map[nextNextY, nextNextX];

                // ⭐️ 상자를 밀 수 있는 조건 (다다음 칸이 빈 공간이거나 목표 지점 'O'일 때)
                if (nextNextTile == EMPTY || nextNextTile == GOAL)
                {
                    // 맵 업데이트: 
                    // 1. 다다음 칸에 상자를 놓습니다.
                    map[nextNextY, nextNextX] = BOX;

                    // 2. 다음 칸(상자가 있던 자리)은 빈 공간으로 만듭니다.
                    map[nextY, nextX] = EMPTY;

                    // 3. 플레이어 좌표를 다음 칸으로 이동시킵니다.
                    map[playerY, playerX] = EMPTY; // 이전 위치를 빈 공간으로
                    playerX = nextX;
                    playerY = nextY;
                    map[playerY, playerX] = PLAYER;

                    return true; // 이동 성공
                }
                else
                {
                    // 상자 뒤에 벽이나 다른 상자가 있다면 이동을 막음
                    return false;
                }
            }
            else // 다음 타일이 빈 공간(EMPTY)이거나 목표(GOAL)일 때 (일반 이동)
            {
                // 플레이어의 이전 위치를 빈 공간으로 만듭니다.
                map[playerY, playerX] = EMPTY;

                // 플레이어 좌표 업데이트
                playerX = nextX;
                playerY = nextY;

                // 새로운 위치에 플레이어를 표시합니다.
                map[playerY, playerX] = PLAYER;

                return true; // 이동 성공
            }
        }
    }
}