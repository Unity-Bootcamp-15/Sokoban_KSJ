


using System;

//  1. Character 클래스 구현 (기본 클래스) - Public Field 사용

public class Character
{
    // ■ 멤버 변수: 미션에 따라 public 필드로 직접 선언 (가장 간단한 형태)
    public string Name;
    public int Hp;
    public int Atk;

    // ■ 생성자
    public Character(string name, int hp, int atk)
    {
        Name = name;
        Hp = hp;
        Atk = atk;
    }


    public virtual void Attack(Character target)
    {

        target.Hp -= Atk;

        if (target.Hp < 0)
        {
            target.Hp = 0;
        }
    }


    public bool IsDead()
    {
        return Hp <= 0;
    }

    public override string ToString()
    {
        return $"{Name} (HP:{Hp}, ATK:{Atk})";
    }
}

// =================================================================================
//  2. Player 클래스 구현 (Character 상속)

public class Player : Character
{
    // 추가 변수: 경험치 (외부에서 읽기만 가능하게 private set;)
    public int Exp { get; private set; } = 0;

    // 생성자
    public Player(string name, int hp, int atk) : base(name, hp, atk)
    {

    }

    // 경험치 증가 함수
    public void GainExp(int amount)
    {
        Exp += amount;
    }
}

// =================================================================================
//  3. Monster 클래스 구현 (Character 상속)

public class Monster : Character
{
    private static Random random = new Random();

    // 생성자: 이름만 받고, Hp와 Atk는 랜덤으로 설정
    public Monster(string name) : base(name, 0, 0)
    {
        // Character 클래스의 public 필드(Hp, Atk)에 직접 값 할당
        this.Hp = random.Next(20, 51);
        this.Atk = random.Next(2, 7);
    }
}

// =================================================================================
//  4. Main()에서 게임 구현

class Program
{

    static string RandomName()
    {
        string[] names = { "슬라임", "고블린", "늑대", "박쥐" };
        return names[new Random().Next(names.Length)];
    }

    // 메인 함수
    static void Main(string[] args)
    {
        Console.WriteLine("=== 몬스터 사냥 게임 ===");

        Player player = new Player("용사", 40, 8);
        bool isPlaying = true;

        while (isPlaying)
        {
            Monster monster = new Monster(RandomName());
            Console.WriteLine($"\n몬스터 등장! {monster}");

            while (!player.IsDead() && !monster.IsDead())
            {
                // 플레이어 공격
                player.Attack(monster);
                Console.WriteLine($"{player.Name} 공격 → {monster.Name} HP:{monster.Hp}");

                if (monster.IsDead())

                    break;

                // 몬스터 반격
                monster.Attack(player);
                Console.WriteLine($"{monster.Name} 반격 → {player.Name} HP:{player.Hp}");

                if (player.IsDead())
                {
                    isPlaying = false;

                    break;
                }
            }

            // --- 전투 결과 처리 ---
            if (player.IsDead())
            {
                Console.WriteLine($"{player.Name}가 쓰러졌습니다. 게임 종료!");

                break;
            }
            else
            {
                player.GainExp(10);
                Console.WriteLine($"{monster.Name} 처치! 경험치 +10");
                Console.WriteLine($"현재 EXP: {player.Exp}");

                Console.Write("계속 싸우시겠습니까? (y/n): ");
                string input = Console.ReadLine()?.ToLower();

                if (input == "n")
                {
                    isPlaying = false;
                }
            }
        }


    }
}