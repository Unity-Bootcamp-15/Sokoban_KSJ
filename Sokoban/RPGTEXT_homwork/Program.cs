using System;

namespace Sokoban
{
    // [유지] Point 구조체
    struct Point
    {
        public int X;
        public int Y;
    }

    internal class Program
    {

        public const int MAP_SIZE_MIN_X = 0;
        public const int MAP_SIZE_MIN_Y = 0;
        public const int MAP_SIZE_MAX_X = 20;
        public const int MAP_SIZE_MAX_Y = 10;
        public const int WALL_COUNT = 5;

        public const int PLAYER_START_X = 5;
        public const int PLAYER_START_Y = 5;
        public const int BOX1_START_X = 10;
        public const int BOX1_START_Y = 5;
        public const int GOAL1_START_X = 15;
        public const int GOAL1_START_Y = 5;
        public const int BOX2_START_X = 8;
        public const int BOX2_START_Y = 8;
        public const int GOAL2_START_X = 18;
        public const int GOAL2_START_Y = 8;

        public const ConsoleColor BOX1_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor BOX2_COLOR = ConsoleColor.Cyan;

        // [유지] Main 함수는 SokobanGame을 생성하고 실행하는 역할만 합니다.
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            while (true)
            {
                // [변경] 게임 실행 로직을 SokobanGame 인스턴스에 위임합니다.
                SokobanGame game = new SokobanGame();
                game.RunSession();
            }
        }
    }


    class SokobanGame
    {

        private int PlayerX;
        private int PlayerY;
        private int Box1X;
        private int Box1Y;
        private int Goal1X;
        private int Goal1Y;
        private int Box2X;
        private int Box2Y;
        private int Goal2X;
        private int Goal2Y;
        private Point[] walls = new Point[Program.WALL_COUNT];
        private bool IsBox1OnGoal = false;
        private bool IsBox2OnGoal = false;
        private int GobackX;
        private int GobackY;
        private bool shouldGobackPlayer = false;


        public SokobanGame()
        {

            PlayerX = Program.PLAYER_START_X;
            PlayerY = Program.PLAYER_START_Y;
            Box1X = Program.BOX1_START_X;
            Box1Y = Program.BOX1_START_Y;
            Goal1X = Program.GOAL1_START_X;
            Goal1Y = Program.GOAL1_START_Y;
            Box2X = Program.BOX2_START_X;
            Box2Y = Program.BOX2_START_Y;
            Goal2X = Program.GOAL2_START_X;
            Goal2Y = Program.GOAL2_START_Y;

            // [유지] 벽 위치 초기화
            walls[0] = new Point { X = 3, Y = 3 };
            walls[1] = new Point { X = 17, Y = 7 };
            walls[2] = new Point { X = 12, Y = 2 };
            walls[3] = new Point { X = 5, Y = 7 };
            walls[4] = new Point { X = 15, Y = 4 };
        }

        // [추가] RunSession 메서드: 게임 루프를 실행합니다.
        public void RunSession()
        {
            while (true)
            {
                Console.Clear();

                // [변경] 그리기 로직을 기존 코드의 순서대로 메서드 호출로 대체했습니다.
                DrawMapBoundary();
                UpdateGoalStatus();
                DrawGoals();
                DrawWalls();
                DrawBoxes();
                DrawPlayer();
                DrawControlMessage();

                GobackX = PlayerX;
                GobackY = PlayerY;
                shouldGobackPlayer = false;

                ConsoleKeyInfo KeyInfo = Console.ReadKey(true);

                if (KeyInfo.Key == ConsoleKey.F5)
                {
                    ClearAllMessages();
                    return;
                }

                // [변경] 이동 및 충돌 로직을 메서드 호출로 대체했습니다.
                HandleMovement(KeyInfo);

                // [변경] 되돌리기 및 메시지 처리 로직을 메서드 호출로 대체했습니다.
                HandlePlayerGoback();

                if (CheckWinCondition())
                {
                    HandleWin();
                    return;
                }
            }
        }



        private void DrawMapBoundary() // [변경] 맵 경계 그리기 로직 분리
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int x = Program.MAP_SIZE_MIN_X; x <= Program.MAP_SIZE_MAX_X; x++)
            {
                Console.SetCursorPosition(x, Program.MAP_SIZE_MIN_Y); Console.Write("#");
                Console.SetCursorPosition(x, Program.MAP_SIZE_MAX_Y); Console.Write("#");
            }
            for (int y = Program.MAP_SIZE_MIN_Y; y <= Program.MAP_SIZE_MAX_Y; y++)
            {
                Console.SetCursorPosition(Program.MAP_SIZE_MIN_X, y); Console.Write("#");
                Console.SetCursorPosition(Program.MAP_SIZE_MAX_X, y); Console.Write("#");
            }
        }

        private void UpdateGoalStatus() // 박스가 골인 지점에 들어왔는지
        {
            IsBox1OnGoal = (Box1X == Goal1X && Box1Y == Goal1Y);
            IsBox2OnGoal = (Box2X == Goal2X && Box2Y == Goal2Y);
        }

        private void DrawGoals() // 골인 지점 그리기 
        {
            // Goal1
            Console.SetCursorPosition(Goal1X, Goal1Y);
            if (IsBox1OnGoal)
            {
                Console.ForegroundColor = Program.BOX1_COLOR;
                Console.Write("★");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("O");
            }

            // Goal2
            Console.SetCursorPosition(Goal2X, Goal2Y);
            if (IsBox2OnGoal)
            {
                Console.ForegroundColor = Program.BOX2_COLOR;
                Console.Write("★");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("O");
            }
        }

        private void DrawWalls() // [변경] 벽 그리기 로직 분리
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < walls.Length; i++)
            {
                Point wall = walls[i];
                Console.SetCursorPosition(wall.X, wall.Y);
                Console.Write("■");
            }
        }

        private void DrawBoxes() // [변경] 박스 그리기 로직 분리
        {
            // Box1
            if (!((Box1X == Goal1X && Box1Y == Goal1Y) || (Box1X == Goal2X && Box1Y == Goal2Y)))
            {
                Console.SetCursorPosition(Box1X, Box1Y);
                Console.ForegroundColor = Program.BOX1_COLOR;
                Console.Write("■");
            }

            // Box2
            if (!((Box2X == Goal2X && Box2Y == Goal2Y) || (Box2X == Goal1X && Box2Y == Goal1Y)))
            {
                Console.SetCursorPosition(Box2X, Box2Y);
                Console.ForegroundColor = Program.BOX2_COLOR;
                Console.Write("■");
            }
        }

        private void DrawPlayer() // [변경] 플레이어 그리기 로직 분리
        {
            Console.SetCursorPosition(PlayerX, PlayerY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("P");
            Console.ResetColor();
        }

        private void DrawControlMessage() // [변경] 조작 메시지 출력 로직 분리
        {
            Console.SetCursorPosition(5, Program.MAP_SIZE_MAX_Y + 2);
            Console.Write("                                            ");
            Console.SetCursorPosition(5, Program.MAP_SIZE_MAX_Y + 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("조작: 화살표키 | 실수 시: F5 (재시작)");
            Console.ResetColor();
        }


        private void HandleMovement(ConsoleKeyInfo KeyInfo) // [변경] 이동/충돌 처리 로직을 묶는 상위 메서드
        {
            // [유지] 플레이어 위치 변경 switch문
            switch (KeyInfo.Key)
            {
                case ConsoleKey.UpArrow: PlayerY -= 1; break;
                case ConsoleKey.DownArrow: PlayerY += 1; break;
                case ConsoleKey.LeftArrow: PlayerX -= 1; break;
                case ConsoleKey.RightArrow: PlayerX += 1; break;
                default: return;
            }

            int newPlayerX = PlayerX;
            int newPlayerY = PlayerY;
            int MoveDirX = newPlayerX - GobackX;
            int MoveDirY = newPlayerY - GobackY;

            // 맵 경계/벽 충돌 검사
            bool isCollidedmap = newPlayerX <= Program.MAP_SIZE_MIN_X || newPlayerX >= Program.MAP_SIZE_MAX_X ||
                                 newPlayerY <= Program.MAP_SIZE_MIN_Y || newPlayerY >= Program.MAP_SIZE_MAX_Y;

            bool isCollidedWithWall = CheckWallCollision(newPlayerX, newPlayerY); // 벽 충돌 검사 함수 호출

            if (isCollidedmap || isCollidedWithWall)
            {
                shouldGobackPlayer = true;
            }
            // Box1 충돌 및 이동 처리
            else if (newPlayerX == Box1X && newPlayerY == Box1Y)
            {
                HandleBoxPush(ref Box1X, ref Box1Y, Box2X, Box2Y, MoveDirX, MoveDirY); // 박스 밀기 함수 
            }
            // Box2 충돌 및 이동 처리
            else if (newPlayerX == Box2X && newPlayerY == Box2Y)
            {
                HandleBoxPush(ref Box2X, ref Box2Y, Box1X, Box1Y, MoveDirX, MoveDirY); // 박스 밀기 함수 
            }
        }

        private bool CheckWallCollision(int x, int y) // 벽에 박는다라는 조건
        {
            for (int i = 0; i < walls.Length; i++)
            {
                if (x == walls[i].X && y == walls[i].Y)
                {
                    return true;
                }
            }
            return false;
        }

        private void HandleBoxPush(ref int CurrentBoxX, ref int CurrentBoxY, int OtherBoxX, int OtherBoxY, int MoveDirX, int MoveDirY) // 박스 미는 동작
        {
            int NextBoxX = CurrentBoxX + MoveDirX;
            int NextBoxY = CurrentBoxY + MoveDirY;

            bool isNextBoxCollidedWithOtherBox = (NextBoxX == OtherBoxX && NextBoxY == OtherBoxY);

            bool isBoxCollidedWithBoundary = NextBoxX <= Program.MAP_SIZE_MIN_X || NextBoxX >= Program.MAP_SIZE_MAX_X ||
                                             NextBoxY <= Program.MAP_SIZE_MIN_Y || NextBoxY >= Program.MAP_SIZE_MAX_Y;

            bool isNextBoxCollidedWithWall = CheckWallCollision(NextBoxX, NextBoxY);

            if (isBoxCollidedWithBoundary || isNextBoxCollidedWithWall || isNextBoxCollidedWithOtherBox)
            {
                shouldGobackPlayer = true;
            }
            else // 충돌하지 않았다면
            {
                CurrentBoxX = NextBoxX;
                CurrentBoxY = NextBoxY;
            }
        }

        private void HandlePlayerGoback() // 플레이어 충돌시 되돌리는 메시지
        {
            if (shouldGobackPlayer)
            {
                // 충돌 메시지 출력
                Console.SetCursorPosition(5, Program.MAP_SIZE_MAX_Y + 3);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("충돌함! (F5로 재시작)");

                PlayerX = GobackX;
                PlayerY = GobackY;
            }
            else
            {
                // 메시지 지우기
                Console.SetCursorPosition(5, Program.MAP_SIZE_MAX_Y + 3);
                Console.Write("                                       ");
            }
        }

        private bool CheckWinCondition() // 이겼다란 조건 확인 
        {
            return IsBox1OnGoal && IsBox2OnGoal;
        }

        private void HandleWin() // 승리한다는 조건만 쓰는걸로 변경
        {
            // 승리 메시지
            Console.SetCursorPosition(Program.MAP_SIZE_MIN_X, Program.MAP_SIZE_MAX_Y + 4);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("모든 박스 골인! 게임 클리어!");

            ConsoleKeyInfo endKeyInfo = Console.ReadKey(true);

            if (endKeyInfo.Key != ConsoleKey.F5)
            {
                // [유지] 게임 전체 종료
                Environment.Exit(0);
            }
        }

        private void ClearAllMessages() // [변경] F5 재시작 전 메시지 정리 로직 분리 (이거 필요한건가?)
        {
            Console.SetCursorPosition(5, Program.MAP_SIZE_MAX_Y + 3);
            Console.Write("                                            ");
            Console.SetCursorPosition(5, Program.MAP_SIZE_MAX_Y + 4);
            Console.Write("                                            ");
            Console.SetCursorPosition(5, Program.MAP_SIZE_MAX_Y + 5);
            Console.Write("                                            ");
        }
    }
}