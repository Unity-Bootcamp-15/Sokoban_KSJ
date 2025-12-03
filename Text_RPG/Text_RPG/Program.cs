using System.Threading;

namespace Program
{
    class Program
    {
        //enum ClassType
        //{
        //    None,
        //    Knight,
        //    Mage,
        //    Rogue,


        //}

        //enum MonsterType
        //{
        //    None,
        //    Slime,
        //    Orc,
        //    Skeleton
        //}
        //// 몬스터 클래스 이넘을 만들어주세요(None, Slime, Orc, Skeleton)

        //struct Player
        //{
        //    public int hp;
        //    public int atk;
        //}

        //// 몬스터 구조체를 만들어주세요(hp, atk) 
        //struct Monster
        //{
        //    public int hp;
        //    public int atk; 
        //}
        //static ClassType ClassChoice()
        //{
        //    Console.WriteLine("직업을 선택하세요!");
        //    Console.WriteLine("[1] 기사");
        //    Console.WriteLine("[2] 마법사");
        //    Console.WriteLine("[3] 도둑");

        //    ClassType choice = ClassType.None;
        //    string input = Console.ReadLine();

        //    switch (input)
        //    {
        //        case "1":
        //            choice = ClassType.Knight;
        //            break;
        //        case "2":
        //            choice = ClassType.Mage;
        //            break;
        //        case "3":
        //            choice = ClassType.Rogue;
        //            break;
        //    }

        //    return choice;
        //}

        //static void CreatePlayer(ClassType choice, out Player player)
        //{
        //    // 기사(100/10), 마법사(50/15), 도둑(75/12)
        //    switch (choice)
        //    {
        //        case ClassType.Knight:
        //            player.hp = 100;
        //            player.atk = 10;
        //            break;
        //        case ClassType.Mage:
        //            player.hp = 50;
        //            player.atk = 15;
        //            break;
        //        case ClassType.Rogue:
        //            player.hp = 75;
        //            player.atk = 12;
        //            break;
        //        default:
        //            player.hp = 0;
        //            player.atk = 0;
        //            break;
        //    }
        //}

        //static void CreateRandomMonster(out Monster monster)
        //{
        //    // out 매개변수는 함수 내에서 반드시 초기화해야 합니다.
        //    Random rand = new Random();

        //    // None (0)을 제외하고 1 (Slime)부터 3 (Skeleton) 중 랜덤하게 선택
        //    int randValue = rand.Next(1, (int)MonsterType.Skeleton + 1);
        //    MonsterType monsterType = (MonsterType)randValue; // 숫자를 Enum 타입으로 변환

        //    // 몬스터의 스탯 설정
        //    switch (monsterType)
        //    {
        //        case MonsterType.Slime:
        //            monster.hp = 20;
        //            monster.atk = 2;
        //            Console.WriteLine("-> 슬라임이 등장했습니다!");
        //            break;
        //        case MonsterType.Orc:
        //            monster.hp = 40;
        //            monster.atk = 4;
        //            Console.WriteLine("-> 오크가 등장했습니다!");
        //            break;
        //        case MonsterType.Skeleton:
        //            monster.hp = 30;
        //            monster.atk = 3;
        //            Console.WriteLine("-> 스켈레톤이 등장했습니다!");
        //            break;
        //        default:
        //            monster.hp = 0;
        //            monster.atk = 0;
        //            break;
        //    }
        //}



        //// 랜덤한 몬스터 생성
        //// Slime, Orc, Skeleton
        //// Slime(20/2), Orc(40/4), Skeleton(30/3)


        //static void Main(string[] args)
        //{
        //    ClassType choice = ClassType.None;

        //    Player player;

        //    while (true)
        //    {
        //        choice = ClassChoice();
        //        if (choice != ClassType.None)
        //        {
        //            CreatePlayer(choice, out player);

        //            Console.WriteLine($"HP {player.hp}, ATK {player.atk}");

        //            Monster monster;
        //            CreateRandomMonster(out monster);
        //        }
        //    }
        //}
        // =======================================
        // 1. Wizard 클래스를 만들어 다음 조건을 만족하세요:
        // 
        // - 멤버 변수: mp, intelligence (둘 다 int형)
        // - 생성자에서 각각 50, 20으로 초기화
        // - Main()에서 Wizard 객체를 생성하고 두 값을 출력
        //
        // [실행 결과]
        // 마법사의 MP: 50, 지능: 20
        // =======================================
        class Wizard
        {
            public int mp;          
            public int intelligence; 
            
            public Wizard()
            {
                this.mp = 50;
                this.intelligence = 20;
            }
        }

        static void Main(string[] args)
        {
            Wizard wizard = new Wizard();

            Console.WriteLine($"마법사의 MP: {wizard.mp}, 지능: {wizard.intelligence}");
        }
    }

}