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


        public bool UpGradeItem(Player player, Item item)
        {
            Random random = new Random();

            if( player.Gold >= 500)              //강화 비용 확인
            {
                player.Gold -= 500;             //강화비용 차감
            }
            else
            {
                return false;           //비용 부족으로 강화 실패
            }
                                   

            int success = random.Next(10);

            if(success == 0) 
            {
                return false;    // 10% 확률로 강화실패
            }
            else
            {
                if (item.Type == 0)
                {
                    item.EqAtk += random.Next(1, 6);    //램덤값으로 강화

                }
                else if (item.Type == 1)
                {
                    item.EqDef += random.Next(1, 6);
                }
                return true;    //강화 성공
            }
           
        }

        /*  아이템 파괴기능 추가시 
         public int UpGradeItem(Player player, Item item)
        {
            Random random = new Random();

            if (player.Gold >= 500)              //강화 비용 확인
            {
                player.Gold -= 500;             //강화비용 차감
            }
            else
            {
                return false;           //비용 부족으로 강화 실패
            }


            int success = random.Next(0, 100);
            if(success == 0){          // 1% 확률로 아이템 파괴
                player.ItemDelete(item);
                return 1;
            }
            else if (1 <= success <= 10 )
            {
                return 2;    // 10% 확률로 강화실패
            }
            else
            {
                if (item.Type == 0)
                {
                    item.EqAtk += random.Next(1, 6);    //램덤값으로 강화

                }
                else if (item.Type == 1)
                {
                    item.EqDef += random.Next(1, 6);
                }
                return 3;    //강화 성공
            }

        } */
    }
}
