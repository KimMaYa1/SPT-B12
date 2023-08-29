using Microsoft.VisualBasic;
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
        public int Level { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string Name { get; }
        public string Chrd { get; }
        public int Exp;
        public int Gold;
        protected int _critical;
        public Item[] Inventory = new Item[0];
        public Item[] EqItem = new Item[2];
        public Player(string name, string chrd, int atk, int def, int hp, int mp, int critical)
        {
            Level = 1;
            Name = name;
            Chrd = chrd;
            Exp = 0;
            Gold = 1000;
            Atk = atk;
            Def = def;
            Hp = hp;
            Mp = mp;
            _critical = critical;
            MaxHp = Hp;
            MaxMp = Mp;
        }
        public virtual void SkillInfo()
        {

        }
        public virtual void Skill(Monster _mob, int _atk)
        {
            
        }
        public int TakeDamage(int _atk)
        {
            int er = (int)(_atk * 0.1);
            int rand = new Random().Next(1, 101);
            int damage = new Random().Next(_atk - er, _atk + er);
            if(rand <= _critical)
            {
                return damage * 2;
            }
            else
            {
                if(damage > Def)
                {
                    return damage - Def;
                }
                else
                {
                    return 1;
                }
            }
        }
        public virtual void LevelUp()
        {

        }
        public void ItemAdd(Item item)
        {
            Array.Resize(ref Inventory, Inventory.Length+1);    //inventory 한칸 늘려주고
            Inventory[Inventory.Length-1] = item;
        }
        public Item ItemDelete(Item item)
        {
            foreach(Item i in Inventory)
            {
                if(i == item)
                {
                    if (i.IsEquiped)
                    {
                        ItemUnEq(i);
                    }
                    Inventory = Inventory.Where(num => num != i).ToArray();
                    return i;
                }
            }
            return null;
        }
        public void ItemEq(Item item)
        {
            if(item.type== 0)
            {
                WhereItemEq(0, item);
            }
            else if(item.type== 1)
            {
                WhereItemEq(1, item);
            }
            else
            {
                Console.WriteLine("장착 가능한 아이템이 아닙니다");
            }
        }
        void WhereItemEq(int index, Item item)
        {
            if (EqItem[index] == null)
            {
                WhatItemEq(index, item);
            }
            else
            {
                EqItem[index].IsEquiped = false;
                EqItem[index] = null;
                WhatItemEq(index, item);

            }

        }
        void WhatItemEq(int index, Item item)
        {
            EqItem[index] = item;
            item.IsEquiped = true;
            Atk += item.EqAtk;
            Def += item.EqDef;
        }
        public void ItemUnEq(Item item)
        {
            if (item.type == 0)
            {
                WhatItemUnEq(0, item);
            }
            else if (item.type == 1)
            {
                WhatItemUnEq(1, item);
            }
            else
            {
                Console.WriteLine("잘못된 접근입니다");
            }
            
        }
        void WhatItemUnEq(int index, Item item)
        {
            EqItem[index].IsEquiped = false;
            EqItem[index] = null;
            Atk -= item.EqAtk;
            Def -= item.EaDef;
        }
    }
}
