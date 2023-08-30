using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Warrior : Player
    {
        public Warrior(string name) : base(name, "전사", 25, 10, 150, 40, 15) { }
        public override float[][] SkillInfo()
        {
            Console.WriteLine("1. 알파 스트라이크 - MP 15");
            Console.WriteLine("   공격력 * 2의 피해를 입힙니다");
            Console.WriteLine("2. 더블 스트라이크 - MP 20");
            Console.WriteLine("   공격력 * 1.5의 피해를 랜덤한 적 2명에게 입힙니다");

            skills[0][0] = Atk * 2f;
            skills[0][1] = Atk * 1.5f;
            skills[1][0] = 15f;
            skills[1][1] = 20f;
            
            return skills;
        }
    }
    internal class Prist : Player
    {
        public Prist(string name) : base(name, "사제", 20, 5, 100, 80, 20) { }
        public override float[][] SkillInfo()
        {
            Console.WriteLine("1. 홀리 엔젤 - MP 20");
            Console.WriteLine("   공격력 * 3의 만큼 피해를 입힙니다");
            Console.WriteLine("2. 샤이닝 로우 - MP 30");
            Console.WriteLine("   2명의 적에게 공격력 * 3의 피해를 입힙니다");

            skills[0][0] = Atk * 3f;
            skills[0][1] = Atk * 3f;
            skills[1][0] = 20f;
            skills[1][1] = 30f;

            return skills;
        }
    }
    internal class Archer : Player
    {
        public Archer(string name) : base(name, "궁수", 30, 3, 100, 50, 40) { }
        public override float[][] SkillInfo()
        {
            Console.WriteLine("1. 천공의 바람 - MP 25");
            Console.WriteLine("   공격력 * 4 만큼 피해를 입힙니다");
            Console.WriteLine("2. 천공의 바람 - MP 40");
            Console.WriteLine("   공격력 * 7 만큼 피해를 입힙니다");

            skills[0][0] = Atk * 4f;
            skills[0][1] = Atk * 7f;
            skills[1][0] = 25f;
            skills[1][1] = 40f;

            return skills;
        }
    }
    
}
