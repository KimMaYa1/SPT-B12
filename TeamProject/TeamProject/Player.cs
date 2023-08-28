using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Player : Character
    {
        public int level { get; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public string name { get; }
        public string chrd { get; }
        public int exp;
        public int gold;
        public Item[] inventory = new Item[0];
        public Item[] eqItem = new Item[2];
        public Player(string _name, string _chrd, int _atk, int _def, int _hp)
        {
            level = 1;
            name = _name;
            chrd = _chrd;
            exp = 0;
            gold = 1000;
            atk = _atk;
            def = _def;
            hp = _hp;
        }
        public virtual void Skill(int _atk)
        {

        }
        public void TakeDamage(Monster _mob , int _atk)
        {

        }
        public void ItemAdd(Item item)
        {
            Array.Resize(ref inventory, inventory.Length+1);    //inventory 한칸 늘려주고
            inventory[inventory.Length-1] = item;
        }

        public void ItemDelete(Item item)
        {
            inventory = inventory.Where(num => num != item).ToArray();      //인벤토리에서 한개 삭제
        }
    }
}
