using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Shop
    {
        static Item[] items = Item.GetItemInfo();
        public static void BuyItem(int num , Player player)
        {
            player.Gold -= items[num-1].Price;
            player.ItemAdd(items[num-1]);
        }

        public static void SellItem(int num, Player player)
        {
            player.Gold += player.Inventory[num-1].Price / 2;    // 해당 아이템의 구매가격의 절반으로 돌려받음
            player.ItemDelete(items[num - 1]);      //인벤토리에서 해당 아티엠을 삭제

        }


        public void UpGradeItem(Player player, Item item)
        {
            if(item.Type == 0)
            {

            }
        }
        /*public void ItemUpGrade(Player player, Item item)
        {
            if (item.Type == 0)
            {
                if (item.Price > 1500)
                {
                    item.EqAtk += 5;
                }
                else
                {
                    item.EqAtk += 2;
                }
            }
            if (item.Type == 1)
            {
                if (item.Price > 1500)
                {
                    item.EqDef += 4;
                }
                else
                {
                    item.EqDef += 1;
                }
            }
        }*/
    }
}
