using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Warrior : Player
    {
        public Warrior(string name, string chrd) : base(name, "전사", 25, 10, 150, 40, 15) { }
        public override void SkillInfo()
        {
            Console.WriteLine("소드 스트라이크 - MP 15");
            Console.WriteLine("공격력의 3배의 피해를 입힙니다");
            Console.WriteLine("짐승에게는 공격력의 5배의 피해를 입힙니다");
        }
        public override void Skill(Monster mob, int atk)
        {
            if(mob.Chrd == "짐승")
            {
                mob.Hp -= Atk * 5;
            }
            else
            {
                mob.Hp -= Atk * 3;
            }
            Mp -= 15;
        }
        public override void LevelUp()
        {
            MaxHp += Level * 50;
            Hp = MaxHp;
            MaxMp += Level * 10;
            Mp = MaxMp;
            Atk += Level * 5;
            Def += Level * 3;
            Exp -= Level * 5;
            Level++;
        }
    }
    internal class Prist : Player
    {
        public Prist(string name, string chrd) : base(name, "사제", 20, 5, 100, 80, 20) { }
        public override void SkillInfo()
        {
            Console.WriteLine("홀리 엔젤 - MP 20");
            Console.WriteLine("공격력의 2배 만큼 체력을 회복합니다");
            Console.WriteLine("언데드에게는 추가로 공격력의 2배의 피해를 입힙니다");
        }
        public override void Skill(Monster mob, int atk)
        {
            if(mob.Chrd == "언데드")
            {
                mob.Hp -= Atk * 2;
            }
            Hp += Atk * 2;
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }
            Mp -= 20;
        }
        public override void LevelUp()
        {
            MaxHp += Level * 30;
            Hp = MaxHp;
            MaxMp += Level * 20;
            Mp = MaxMp;
            Atk += Level * 5;
            Def += Level * 2;
            Exp -= Level * 5;
            Level++;
        }
    }
    internal class Archer : Player
    {
        public Archer(string name, string chrd) : base(name, "궁수", 30, 3, 100, 50, 40) { }
        public override void SkillInfo()
        {
            Console.WriteLine("천공의 바람 - MP 25");
            Console.WriteLine("공격력의 4배 만큼 피해를 입힙니다");
            Console.WriteLine("악마에게는 공격력의 6배의 피해를 입힙니다");
        }
        public override void Skill(Monster mob, int atk)
        {
            if (mob.Chrd == "악마")
            {
                mob.Hp -= Atk * 6;
            }
            else
            {
                mob.Hp -= Atk * 4;
            }
            Mp -= 25;
        }
        public override void LevelUp()
        {
            MaxHp += Level * 15;
            Hp = MaxHp;
            MaxMp += Level * 10;
            Mp = MaxMp;
            Atk += Level * 7;
            Def += Level * 1;
            Exp -= Level * 5;
            Level++;
        }
    }
}
