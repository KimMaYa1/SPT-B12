using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Warrior : Player
    {
        public Warrior(string _name, string _chrd) : base(_name, _chrd, 15, 10, 150, 40, 15) { }
        public override void SkillInfo()
        {
            Console.WriteLine("소드 스트라이크 - MP 15");
            Console.WriteLine("공격력의 3배의 피해를 입힙니다");
            Console.WriteLine("짐승에게는 공격력의 5배의 피해를 입힙니다");
        }
        public override void Skill(Monster _mob, int _atk)
        {
            if(_mob.chrd == "짐승")
            {
                _mob.hp -= _atk * 5;
            }
            else
            {
                _mob.hp -= _atk * 3;
            }
            mp -= 15;
        }
        public override void LevelUp()
        {
            maxHp += level * 50;
            hp = maxHp;
            maxMp += level * 10;
            mp = maxMp;
            atk += level * 3;
            def += level * 3;
            exp -= level * 5;
            level++;
        }
    }
    internal class Prist : Player
    {
        public Prist(string _name, string _chrd) : base(_name, _chrd, 10, 5, 100, 80, 20) { }
        public override void SkillInfo()
        {
            Console.WriteLine("홀리 엔젤 - MP 20");
            Console.WriteLine("공격력의 2배 만큼 체력을 회복합니다");
            Console.WriteLine("언데드에게는 추가로 공격력의 2배의 피해를 입힙니다");
        }
        public override void Skill(Monster _mob, int _atk)
        {
            if(_mob.chrd == "언데드")
            {
                _mob.hp -= _atk * 2;
            }
            hp += _atk * 2;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
        public override void LevelUp()
        {
            maxHp += level * 30;
            hp = maxHp;
            maxMp += level * 20;
            mp = maxMp;
            atk += level * 5;
            def += level * 2;
            exp -= level * 5;
            level++;
        }
    }
    internal class Archer : Player
    {
        public Archer(string _name, string _chrd) : base(_name, _chrd, 20, 3, 100, 50, 40) { }
        public override void SkillInfo()
        {
            Console.WriteLine("천공의 바람 - MP 15");
            Console.WriteLine("공격력의 4배 만큼 피해를 입힙니다");
            Console.WriteLine("악마에게는 공격력의 6배의 피해를 입힙니다");
        }
        public override void Skill(Monster _mob, int _atk)
        {
            if (_mob.chrd == "악마")
            {
                _mob.hp -= _atk * 6;
            }
            else
            {
                _mob.hp -= _atk * 4;
            }
        }
        public override void LevelUp()
        {
            maxHp += level * 15;
            hp = maxHp;
            maxMp += level * 10;
            mp = maxMp;
            atk += level * 7;
            def += level * 1;
            exp -= level * 5;
            level++;
        }
    }
}
