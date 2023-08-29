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

        public int eqAtk;           // 장착 무기
        public int eqDef;           // 장착 방어력
        public string name;         // 아이템 이름
        public string info;         // 아이템 정보
        public int type;            // 0 무기 1 방어구
        public bool isEquiped;   //착용유무
        public int price;           //가격
        public Player player;// { get; set; }



        public Item(int _eqAtk, int _eqDef, int _type, int _price, string _name, string _info, Player _player)
        {
            eqAtk = _eqAtk;
            eqDef = _eqDef;
            type = _type;
            price = _price;
            name = _name;
            info = _info;
            isEquiped = false;
            player = _player;

        }
        public virtual void Perfomence()
        {
        }


    }
    internal class Weapon : Item
    {
        public Weapon(int _eqAtk, int _eqDef, int _type, int _price, string _name, string _info, Player _player) : base(_eqAtk, _eqDef, _type, _price, _name, _info, _player) { }
        

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