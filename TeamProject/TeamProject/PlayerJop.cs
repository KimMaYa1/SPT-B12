using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Warrior : Player
    {
        public Warrior(string _name, string _chrd) : base(_name, _chrd, 15, 10, 150) { }
    }
}
