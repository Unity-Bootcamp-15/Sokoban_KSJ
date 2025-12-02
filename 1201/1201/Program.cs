namespace _1201
{
    internal class Program
    {
        
        
        
            public enum Season
        {
            Spring =1,
            Summer,
            Autumn,
            Winter
        }

        public class EnumConversionExample
        {
            public static void Main()
            {
                Season a = Season.Autumn;
                Console.WriteLine($"Integral value of {a} is {(int)a}");

                Season b = (Season)1;
                Console.WriteLine(b);

                Season c = 0; // 암시적변환
                Console.WriteLine(c);
            }
        }
        
    }
}
