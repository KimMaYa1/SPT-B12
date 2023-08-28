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

        public Monster(string _name, string _chrd, int _level)
        {
            level = _level;
            name = _name;
            chrd = _chrd;
            atk = level * 4;
            def = level * 2;
            hp = level * 20;
        }
    }
}
