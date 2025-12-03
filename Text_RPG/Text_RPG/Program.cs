namespace Program
{
    class Program
    {
        enum ClassType
        {
            None,
            Knight,
            Mage,
            Rogue
        }

        static ClassType ClassChoice()
        {
            Console.WriteLine("직업을 선택하세요!");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 마법사");
            Console.WriteLine("[3] 도둑");

            ClassType choice = ClassType.None;
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    choice = ClassType.Knight;
                    break;
                case "2":
                    choice = ClassType.Mage;
                    break;
                case "3":
                    choice = ClassType.Rogue;
                    break;
            }

            return choice;
        }

        static void CreatePlayer(ClassType choice, out int hp , out int atk)
        {
            // 기사(100/10), 마법사(50/15), 도둑(75/12)
           
            switch (choice)
            {
                case ClassType.Knight:
                    hp = 100;
                    atk = 0;
                    break;
                case ClassType.Mage:
                    hp = 50;
                    atk = 15;
                    break;
                case ClassType.Rogue:
                    hp = 75;
                    atk = 12;
                    break;
                    default:
                       hp = 0; atk = 0; 
                    
                    break;

            }
        }

        static void Main(string[] args)
        {
            ClassType choice = ClassType.None;

            while (true)
            {
                choice = ClassChoice();
                if (choice != ClassType.None)
                {
                    // 캐릭터 생성
                    int hp;
                    int atk;
                    CreatePlayer(choice, out hp, out atk);

                    Console.WriteLine($"HP {hp}, ATK {atk}");
                }
            }
        }
    }
}