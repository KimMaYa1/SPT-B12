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
        public int EqHP;            // Hp (임시)
        public int EqMP;            // MP (임시)
        public string Name;         // 아이템 이름
        public string Info;         // 아이템 정보
        public int Type;            // 0 무기 1 방어구 2 포션
        public bool IsEquiped;   //착용유무
        public int Price;           //가격

        public static Item[] ItemInfo = GetItemInfo();

        public Item(string name)
        {
            Item item = ItemInfo.Where(it => it.Name == name).ToArray()[0];
            EqAtk = item.EqAtk;
            EqDef = item.EqDef;
            EqHP = item.EqHP;
            EqMP = item.EqMP;
            Type = item.Type;
            Price = item.Price;
            Name = item.Name;
            Info = item.Info;
            IsEquiped = false;
        }

        public Item(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info)
        {
            //Item item = (Item)ItemInfo.Where(it => it.Name == name);
            EqAtk = eqAtk;
            EqDef = eqDef;
            EqHP = eqHp;
            EqMP = eqMp;
            Type = type;
            Price = price;
            Name = name;
            Info = info;
            IsEquiped = false;
        }
        public virtual void Perfomence()
        {
        }

        public static Item[] GetItemInfo()
        {
            Item[] allItems = new Item[] { };

            string itemPath = Pathes.ItemDataPath();
            string[] itemData = File.ReadAllLines(itemPath, Encoding.UTF8);
            string[] propertyNames = itemData[0].Split(',');

            for (int itemIdx = 1; itemIdx < itemData.Length; itemIdx++)
            {
                string[] itemEach = itemData[itemIdx].Split(",");

                string name = itemEach[Array.IndexOf(propertyNames, "Name")];
                string info = itemEach[Array.IndexOf(propertyNames, "Info")];
                int type = int.Parse(itemEach[Array.IndexOf(propertyNames, "Type")]);
                int eqAtk = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqAtk")]);
                int eqDef = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqDef")]);
                int eqHP = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqHp")]);
                int eqMP = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqMp")]);
                int price = int.Parse(itemEach[Array.IndexOf(propertyNames, "Price")]);

                Item item = new Item(eqAtk, eqDef, eqHP, eqMP, type, price, name, info);

                Array.Resize(ref allItems, allItems.Length + 1);
                allItems[itemIdx - 1] = item;
            }

            return allItems;
        }
    }
    internal class Weapon : Item
    {
        public Weapon(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info) : base(eqAtk, eqDef, eqHp, eqMp, type, price, name, info) { }
        

    }
    internal class Defense : Item
    {
        public Defense(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info) : base(eqAtk, eqDef, eqHp, eqMp, type, price, name, info) { }


    }

    internal class Potion : Item
    {
        public Potion(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info) : base(eqAtk, eqDef, eqHp, eqMp, type, price, name, info) { }

    }
}