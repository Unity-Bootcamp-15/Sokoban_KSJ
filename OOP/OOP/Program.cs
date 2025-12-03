namespace OOP
{
    internal class Program // 설계도
    {
        static void Main()
        {
          
        }

        static void Test()
        {

        }
 
    }

    // 객체는 클래스를 바탕으로 실존하는 무언가 만든거
    class Car // class는 설계도 (청사진)   기능과 속성을 정의하는것
              //객체 == 모든것의 엔진, 베터리, 운전 , 소리
              // 객체끼리 상호작용을 통해서 문제 해결을 하는 거
    {
        public ExecutionEngineException engine = new ExecutionEngineException();
        public battery battery = new battery();

    }
}
// 구조체 => 변수의 묶음
struct Player // 설계도
{

}