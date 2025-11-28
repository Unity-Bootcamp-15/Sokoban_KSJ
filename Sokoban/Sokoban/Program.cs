namespace Sokoban
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //색깔 조정
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            //제목 수정
            Console.Title = "콘솔로 만든 소코반";
            //커서 숨김
            Console.CursorVisible = false;
            Console.Clear();
        }
    }
}
