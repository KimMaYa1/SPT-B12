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

            Console.WriteLine("아이템을 구매하였습니다.");
        }

        public static void SellItem(int num, Player player)
        {
            player.Gold += player.Inventory[num-1].Price / 2;    // 해당 아이템의 구매가격의 절반으로 돌려받음
            player.Inventory = player.Inventory.Where(idx => idx != player.Inventory[num-1]).ToArray();       //인벤토리에서 해당 아티엠을 삭제
            Console.WriteLine("아이템을 판매하였습니다.");

        }
    }
}
