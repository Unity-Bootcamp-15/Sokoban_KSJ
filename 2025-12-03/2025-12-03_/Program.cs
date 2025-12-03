namespace _2025_12_03_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ref 짝꿍 => out (out도 참조 전달은 맞는데 
            //                             [ref] [out]
            // 주소 전달 방식?               O     O
            // 함수 안에서 읽기 가능?        O     X
            // 함수 호출전 반드시 초기화?    O     X
            // 함수 내에서 반드시 값쓰기?    X     O



            // Out은 타입이 다른 변수를 한꺼번에 받고 싶을때 자주 쓴다.

            // Start is called once before the first execution of Update after the MonoBehaviour is created
            //void Swap(ref int a, ref int b)  // ref를 넣으면 참조전달을 해서 아예 값을 다르게한다
            //{
            //    int temp = a;  // 빈공간에 a를 일단 집어 넣음 temp = 10;
            //    a = b; // a = 20, b = 20
            //    b = temp; // b = 10, a = 20, temp = 10
            //}

            //// Update is called once per frame
            //static void Main()
            //{
            //    int x = 10;
            //    int y = 20;
            //    Swap(x, y);
            //    Console.WriteLine($"{x}, {y}");
            //}

            // 오버로딩 : 함수 이름 재사용 (면접 문제 필수 제미나이 필요)


            //static void Add(int a)
            //{
            //    a++;
            //}

            //static void Add(int b)
            //{
            //    int temp = a + b;
            //}

            // 선택적 매개변수 : 미리 기본값 지정 (이것도 제미나이 예시 필요) 장점, 예시 , 예시코드 물어볼것


            // 재귀함수 : 내가 나 자신을 호출하는것(??)

            // 반복문 써서 1~6까지 더하기 누적합 알고리즘을
            // 재귀함수 써서도 가능

            //static void Test()
            //{
            //    Console.WriteLine(); // 4를 넣게되면 Test에도 4가 들어가고 다시 되돌아가서 오버플로우 (무한반복)
            //    Test();
            //}
            static int Sum(int strat, int end)
            {
                if (strat == end)
                {
                    return end;
                }
                else
                {
                    return strat + Sum(strat + 1, end);
                }
            }



        }
    }
}
