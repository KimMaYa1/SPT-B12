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
        public int level { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public int mp { get; set; }
        public int maxMp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public string name { get; }
        public string chrd { get; }
        public int exp;
        public int gold;
        protected int critical;
        public Item[] inventory = new Item[0];
        public Item[] eqItem = new Item[2];
        public Player(string _name, string _chrd, int _atk, int _def, int _hp, int _mp, int _critical)
        {
            level = 1;
            name = _name;
            chrd = _chrd;
            exp = 0;
            gold = 1000;
            atk = _atk;
            def = _def;
            hp = _hp;
            mp = _mp;
            critical = _critical;
            maxHp = hp;
            maxMp = mp;
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
            if(rand <= critical)
            {
                return damage * 2;
            }
            else
            {
                if(damage > def)
                {
                    return damage - def;
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
            Array.Resize(ref inventory, inventory.Length+1);    //inventory 한칸 늘려주고
            inventory[inventory.Length-1] = item;
        }
        public Item ItemDelete(Item item)
        {
            foreach(Item i in inventory)
            {
                if(i == item)
                {
                    if (i.IsEquiped)
                    {
                        ItemUnEq(i);
                    }
                    inventory = inventory.Where(num => num != i).ToArray();
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
            if (eqItem[index] == null)
            {
                WhatItemEq(index, item);
            }
            else
            {
                eqItem[index].IsEquiped = false;
                eqItem[index] = null;
                WhatItemEq(index, item);

            }

        }
        void WhatItemEq(int index, Item item)
        {
            eqItem[index] = item;
            item.IsEquiped = true;
            atk += item.eqAtk;
            def += item.eaDef;
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
            eqItem[index].IsEquiped = false;
            eqItem[index] = null;
            atk -= item.eqAtk;
            def -= item.eaDef;
        }
    }
}
