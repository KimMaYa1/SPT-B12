using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Item
    {

        public int EqAtk;           // 장착 무기
        public int EqDef;           // 장착 방어력
        public string Name;         // 아이템 이름
        public string Info;         // 아이템 정보
        public int Type;            // 0 무기 1 방어구
        public bool IsEquiped;   //착용유무
        public int Price;           //가격
        public Player Player;// { get; set; }



        public Item(int eqAtk, int eqDef, int type, int price, string name, string info, Player player)
        {
            EqAtk = eqAtk;
            EqDef = eqDef;
            Type = type;
            Price = price;
            Name = name;
            Info = info;
            IsEquiped = false;
            Player = player;

        }
        public virtual void Perfomence()
        {
        }


    }
    internal class Weapon : Item
    {
        public Weapon(int eqAtk, int eqDef, int type, int price, string name, string info, Player player) : base(eqAtk, eqDef, type, price, name, info, player) { }
        

    }
    internal class Defense : Item
    {
        public Defense(int eqAtk, int eqDef, int type, int price, string name, string info, Player player) : base(eqAtk, eqDef, type, price, name, info, player) { }


    }

    internal class Potion : Item
    {
        public Potion(int eqAtk, int eqDef, int type, int price, string name, string info, Player player) : base(eqAtk, eqDef, type, price, name, info, player) { }


    }
}