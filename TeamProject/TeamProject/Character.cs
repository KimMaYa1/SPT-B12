using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal interface Character
    {
        int level { get; }
        int hp { get; set; }
        int atk { get; }
        int def { get; }
        string name { get; }
        string chrd { get; }
    }
}
