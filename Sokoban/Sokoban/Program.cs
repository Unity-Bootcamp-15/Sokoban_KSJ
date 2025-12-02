using System;

namespace Sokoban
{

    struct Point
    {
        public int X;
        public int Y;
    }

    internal class Program
    {
        // 상수로 변환 (수업때 대문자 _ 이걸 쓰는게 상수값 변환의 예의라고 적혀있었음)
        const int MAP_SIZE_MIN_X = 0;
        const int MAP_SIZE_MIN_Y = 0;
        const int MAP_SIZE_MAX_X = 20;
        const int MAP_SIZE_MAX_Y = 10;
        const int WALL_COUNT = 5;

        const int PLAYER_START_X = 5;
        const int PLAYER_START_Y = 5;
        const int BOX1_START_X = 10;
        const int BOX1_START_Y = 5;
        const int GOAL1_START_X = 15;
        const int GOAL1_START_Y = 5;
        const int BOX2_START_X = 8;
        const int BOX2_START_Y = 8;
        const int GOAL2_START_X = 18;
        const int GOAL2_START_Y = 8;

        // 박스 색깔 상수 (박스 색깔까지는.. 굳이? 싶지만 일단 변하지 않는것이기에..)
        const ConsoleColor BOX1_COLOR = ConsoleColor.Yellow;
        const ConsoleColor BOX2_COLOR = ConsoleColor.Cyan;

        // 메인 함수: 게임 세션을 무한 반복하며 실행 (반복문 while 사용)
        static void Main(string[] args)
        {
            // 콘솔 색 변경
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            // 게임 세션을 무한히 반복합니다. RestartGame()이 끝날 때마다 새로운 게임이 시작됩니다.
            while (true)
            {
                // 한 번의 게임 세션이 이 함수 안에서 실행됩니다.
                RestartGame();
            }
        }

        // 하나의 게임 세션을 실행하는 함수 (초기화 및 게임 루프 포함)
        static void RestartGame()
        {

            int PlayerX = PLAYER_START_X;
            int PlayerY = PLAYER_START_Y;

            int Box1X = BOX1_START_X;
            int Box1Y = BOX1_START_Y;

            int Goal1X = GOAL1_START_X;
            int Goal1Y = GOAL1_START_Y;

            int Box2X = BOX2_START_X;
            int Box2Y = BOX2_START_Y;

            int Goal2X = GOAL2_START_X;
            int Goal2Y = GOAL2_START_Y;

            // Point 구조체 타입의 배열 선언 (배열 개념 사용)
            Point[] walls = new Point[WALL_COUNT];
            // bool 타입 변수 선언 (선택문, 반복문 탈출 조건 등에 사용)
            bool IsBox1OnGoal = false;
            bool IsBox2OnGoal = false;


            // =========================================================================================
            walls[0] = new Point { X = 3, Y = 3 };
            walls[1] = new Point { X = 17, Y = 7 };
            walls[2] = new Point { X = 12, Y = 2 };
            walls[3] = new Point { X = 5, Y = 7 };
            walls[4] = new Point { X = 15, Y = 4 };

            int GobackX;
            int GobackY;
            bool shouldGobackPlayer = false;

            // =========================================================================================

            while (true)
            {
                Console.Clear();

                // 맵의 경계 그리기 (반복문 for 사용)
                Console.ForegroundColor = ConsoleColor.DarkRed;
                for (int x = MAP_SIZE_MIN_X; x <= MAP_SIZE_MAX_X; x++)
                {
                    Console.SetCursorPosition(x, MAP_SIZE_MIN_Y); Console.Write("#"); // 문자열 출력
                    Console.SetCursorPosition(x, MAP_SIZE_MAX_Y); Console.Write("#");
                }
                for (int y = MAP_SIZE_MIN_Y; y <= MAP_SIZE_MAX_Y; y++)
                {
                    Console.SetCursorPosition(MAP_SIZE_MIN_X, y); Console.Write("#");
                    Console.SetCursorPosition(MAP_SIZE_MAX_X, y); Console.Write("#");
                }

                // 두 박스의 골인 상태 업데이트 (bool 변수 사용)
                IsBox1OnGoal = (Box1X == Goal1X && Box1Y == Goal1Y);
                IsBox2OnGoal = (Box2X == Goal2X && Box2Y == Goal2Y);

                // Goal1 위치 지정
                Console.SetCursorPosition(Goal1X, Goal1Y);
                if (IsBox1OnGoal) // 선택문 if/else로 변경
                {
                    Console.ForegroundColor = BOX1_COLOR;
                    Console.Write("★");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("O");
                }

                // Goal2 그리기 (삼항 연산자를 if/else 선택문으로 변경)
                Console.SetCursorPosition(Goal2X, Goal2Y);
                if (IsBox2OnGoal) // 선택문 if/else로 변경
                {
                    Console.ForegroundColor = BOX2_COLOR;
                    Console.Write("★");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("O");
                }


                // 벽 그리기 (Point 타입 배열을 순회하는 반복문 for로 변경)
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int i = 0; i < walls.Length; i++) // for 반복문으로 변경
                {
                    Point wall = walls[i]; // 배열의 요소를 가져옴
                    Console.SetCursorPosition(wall.X, wall.Y);
                    Console.Write("※");
                }


                // Box1 그리기 (선택문 if 사용)
                // 만약 박스가 골 지점에 있다면 그리지 않습니다.
                // Box1이 Box1 Goal에 있거나, 실수로 Box2 Goal에 있다면 그리지 않습니다.
                if (!((Box1X == Goal1X && Box1Y == Goal1Y) || (Box1X == Goal2X && Box1Y == Goal2Y)))
                {
                    Console.SetCursorPosition(Box1X, Box1Y);
                    Console.ForegroundColor = BOX1_COLOR;
                    Console.Write("■");
                }

                // Box2 그리기
                // Box2가 Box2 Goal에 있거나, 실수로 Box1 Goal에 있다면 그리지 않습니다.
                if (!((Box2X == Goal2X && Box2Y == Goal2Y) || (Box2X == Goal1X && Box2Y == Goal1Y)))
                {
                    Console.SetCursorPosition(Box2X, Box2Y);
                    Console.ForegroundColor = BOX2_COLOR;
                    Console.Write("■");
                }


                // 플레이어 그리기
                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("P"); // 문자열 출력


                Console.ResetColor();

                Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 2);
                Console.Write("                                     "); // 공백으로 해두면 기존 줄이 지워짐
                Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("조작: 화살표키 | 실수 시: F5 (재시작)");
                Console.ResetColor();


                // 되돌아갈 위치 저장 (int 변수 사용)
                GobackX = PlayerX;
                GobackY = PlayerY;

                shouldGobackPlayer = false;

                ConsoleKeyInfo KeyInfo = Console.ReadKey(true);

                // F5 키 입력 처리 (선택문 if 사용)
                if (KeyInfo.Key == ConsoleKey.F5)
                {
                    // 재시작 전에 승리/충돌 메시지 줄 정리
                    Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 3);
                    Console.Write(" ");
                    Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 4);
                    Console.Write(" ");
                    Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 5);
                    Console.Write(" ");
                    return; // 함수 종료 -> Main에서 새 세션 시작
                }

                // 플레이어 이동 처리 (선택문 switch 사용)
                switch (KeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        PlayerY -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        PlayerY += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        PlayerX -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        PlayerX += 1;
                        break;

                    default:

                        continue;
                }

                // 다음 플레이어 위치 계산 (int 변수 사용)
                int newPlayerX = PlayerX;
                int newPlayerY = PlayerY;
                int MoveDirX = newPlayerX - GobackX;
                int MoveDirY = newPlayerY - GobackY;


                // 1. 맵 경계 충돌 확인 (bool 변수와 논리 연산자 사용)
                bool isCollidedmap = newPlayerX <= MAP_SIZE_MIN_X || newPlayerX >= MAP_SIZE_MAX_X ||
                  newPlayerY <= MAP_SIZE_MIN_Y || newPlayerY >= MAP_SIZE_MAX_Y;

                // 2. 고정된 벽들과 충돌 확인 (bool 변수 사용)
                bool isCollidedWithWall = false;

                // Point 타입 배열을 순회하는 반복문 for로 변경
                for (int i = 0; i < walls.Length; i++) // for 반복문으로 변경
                {
                    if (newPlayerX == walls[i].X && newPlayerY == walls[i].Y)
                    {
                        isCollidedWithWall = true;
                        break;
                    }
                }

                // 선택문 if/else if 사용
                if (isCollidedmap || isCollidedWithWall)
                {
                    shouldGobackPlayer = true;
                }

                // Box1 충돌 및 이동 처리
                else if (newPlayerX == Box1X && newPlayerY == Box1Y)
                {
                    int NextBoxX = Box1X + MoveDirX;
                    int NextBoxY = Box1Y + MoveDirY;

                    // 다음 박스 위치 충돌 검사
                    bool isNextBoxCollidedWithOtherBox = (NextBoxX == Box2X && NextBoxY == Box2Y);

                    bool isBoxCollidedWithBoundary = NextBoxX <= MAP_SIZE_MIN_X || NextBoxX >= MAP_SIZE_MAX_X ||
                                                     NextBoxY <= MAP_SIZE_MIN_Y || NextBoxY >= MAP_SIZE_MAX_Y;

                    // 다음 박스 위치가 벽과 충돌하는지 확인
                    bool isNextBoxCollidedWithWall = false;
                    // Point 타입 배열을 순회하는 반복문 for로 변경
                    for (int i = 0; i < walls.Length; i++) // for 반복문으로 변경
                    {
                        if (NextBoxX == walls[i].X && NextBoxY == walls[i].Y)
                        {
                            isNextBoxCollidedWithWall = true;
                            break;
                        }
                    }

                    // if 조건문의 변수명도 변경된 isNextBoxCollidedWithOtherBox로 수정
                    if (isBoxCollidedWithBoundary || isNextBoxCollidedWithWall || isNextBoxCollidedWithOtherBox)
                    {
                        shouldGobackPlayer = true;
                    }
                    else // 충돌하지 않았다면
                    {
                        Box1X = NextBoxX;
                        Box1Y = NextBoxY;
                    }
                }

                // Box2 충돌 및 이동 처리
                else if (newPlayerX == Box2X && newPlayerY == Box2Y)
                {
                    int NextBoxX = Box2X + MoveDirX;
                    int NextBoxY = Box2Y + MoveDirY;

                    // 다음 박스 위치 충돌 검사
                    // 변수명 통일성을 위해 isNextBoxCollidedWithOtherBox로 변경
                    bool isNextBoxCollidedWithOtherBox = (NextBoxX == Box1X && NextBoxY == Box1Y);


                    bool isBoxCollidedWithBoundary = NextBoxX <= MAP_SIZE_MIN_X || NextBoxX >= MAP_SIZE_MAX_X ||
                                                     NextBoxY <= MAP_SIZE_MIN_Y || NextBoxY >= MAP_SIZE_MAX_Y;

                    // 다음 박스 위치가 벽과 충돌하는지 확인
                    bool isNextBoxCollidedWithWall = false;
                    // Point 타입 배열을 순회하는 반복문 for로 변경
                    for (int i = 0; i < walls.Length; i++) // for 반복문으로 변경
                    {
                        if (NextBoxX == walls[i].X && NextBoxY == walls[i].Y)
                        {
                            isNextBoxCollidedWithWall = true;
                            break;
                        }
                    }

                    // if 조건문의 변수명도 변경된 isNextBoxCollidedWithOtherBox로 수정
                    if (isBoxCollidedWithBoundary || isNextBoxCollidedWithWall || isNextBoxCollidedWithOtherBox)
                    {
                        shouldGobackPlayer = true;
                    }
                    else // 충돌하지 않았다면
                    {
                        Box2X = NextBoxX;
                        Box2Y = NextBoxY;
                    }
                }


                // 플레이어 되돌리기 (선택문 if 사용)
                if (shouldGobackPlayer)
                {
                    // 충돌 메시지 출력 (문자열 사용)
                    Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 3);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("충돌함! (F5로 재시작)");


                    PlayerX = GobackX; // 되돌아갈 위치로 PlayerX, PlayerY 값 변경
                    PlayerY = GobackY;
                }
                else
                {

                    // 메시지 지우기 
                    Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 3);
                    Console.Write(" ");
                }


                // 승리 조건 확인 (선택문 if 사용)
                if (IsBox1OnGoal && IsBox2OnGoal)
                {
                    // 승리 메시지 출력 (문자열 사용)
                    Console.SetCursorPosition(MAP_SIZE_MIN_X, MAP_SIZE_MAX_Y + 4);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("모든 박스 골인! 게임 클리어!");

                    // 승리 후 키 입력 대기
                    ConsoleKeyInfo endKeyInfo = Console.ReadKey(true);

                    if (endKeyInfo.Key == ConsoleKey.F5)
                    {
                        // 재시작 전에 승리,충돌 메시지 줄 정리
                        Console.SetCursorPosition(5, MAP_SIZE_MAX_Y + 4);
                        Console.Write(" ");
                        return; // Main에서 새 세션 시작
                    }
                    else
                    {
                        // 게임 전체 종료 기능단어
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}