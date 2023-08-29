using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal interface Character
    {
        int Level { get; }
        int Hp { get; set; }
        int Atk { get; }
        int Def { get; }
        string Name { get; }
        string Chrd { get; }

        int TakeDamage(int _atk);
        bool Evasion();
    }
}
