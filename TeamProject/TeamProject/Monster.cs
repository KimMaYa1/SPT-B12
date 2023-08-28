using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Monster : Character
    {
        public int level { get; }
        public int exp { get; }
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
        public int TakeDamage(int _atk)
        {
            int er = (int)(_atk * 0.1);
            int rand = new Random().Next(1, 101);
            int damage = new Random().Next(_atk - er, _atk + er);
            if (rand <= critical)
            {
                return (int)(damage * 1.5);
            }
            else
            {
                if (damage > def)
                {
                    return damage - def;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
