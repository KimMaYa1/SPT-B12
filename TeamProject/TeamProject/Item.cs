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



        public Item(int _eqAtk, int _eqDef, int _type, int _price, string _name, string _info, Player player)
        {
            EqAtk = _eqAtk;
            EqDef = _eqDef;
            Type = _type;
            Price = _price;
            Name = _name;
            Info = _info;
            IsEquiped = false;
            Player = player;

        }
        public virtual void Perfomence()
        {
        }


    }
    internal class Weapon : Item
    {
        public Weapon(int _eqAtk, int _eqDef, int _type, int _price, string _name, string _info, Player _player) : base(_eqAtk, _eqDef, _type, _price, _name, _info, _player) { }
        
        public override void Perfomence() 
        {
            if (IsEquiped)
            {
                Player.itemAtk += EqAtk;
            }
        }

    }
    internal class Defense : Item
    {
        public Defense(int _eqAtk, int _eqDef, int _type, int _price, string _name, string _info, Player _player) : base(_eqAtk, _eqDef, _type, _price, _name, _info, _player) { }


    }

    internal class Potion : Item
    {
        public Potion(int _eqAtk, int _eqDef, int _type, int _price, string _name, string _info, Player _player) : base(_eqAtk, _eqDef, _type, _price, _name, _info, _player) { }


    }
}