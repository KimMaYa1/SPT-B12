using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Monster : Character
    {
        public int level { get; }
        public int hp { get; set; }
        public int atk { get; }
        public int def { get; }
        public string name { get; }
        public string chrd { get; }
        int critical;
        public Monster(string _name, string _chrd, int _level)
        {
            level = _level;
            name = _name;
            chrd = _chrd;
            atk = level * 4;
            def = level * 2;
            hp = level * 20;
            critical = 20;
        }
        public void GiveDamage(Player _player, int _atk)
        {
            int rand = new Random().Next(1, 101);
            if (rand <= critical)
            {
                _player.hp -= _atk;
            }
            else
            {
                _player.hp -= _atk - (int)(_atk * def / 15);
            }
            if (_player.hp <= 0)
            {
                Console.WriteLine($"플레이어 : {_player.name}님께서 사망하였습니다.");
            }
        }
    }
}
