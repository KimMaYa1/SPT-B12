using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TeamProject
{
    internal class Shop
    {
        static Item[] items = Item.GetItemInfo();
        static int lineX = 2;
        static int lineY = 2;

        public static void DisplayShop(Player player, Scene scene)
        {
            ShowItemList(scene, player);
        }
        public static bool ShowItemList(Scene scene, Player player)
        {
            Console.Clear();
            scene.DrawStar();

            lineX = 4;
            lineY = 2;

            Console.ForegroundColor = ConsoleColor.Green;
            scene.SetCursorString(77, lineY++, "상점", false);
            Console.ForegroundColor = ConsoleColor.Yellow;
            lineY++;

            scene.SetCursorString(33, lineY, "이름", false);
            scene.SetCursorString(58, lineY++, "|공격력|방어력 | +HP \t| +MP \t| 가격\t| 설명", false);
            scene.SetCursorString(30, lineY++, "-----------------------------------------------------------------------------------------------", false);
            lineY++;
            Console.ForegroundColor = ConsoleColor.White;

            int count = 1;
            foreach (Item i in items)
            {
                lineX = 30;
                scene.SetCursorString(lineX, lineY, String.Format("{0,-3}", $"{count}. {i.Name}"), false);
                lineX += 28;
                scene.SetCursorString(lineX , lineY++, $"| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| {i.Price}\t| {i.Info}", false);
                lineY++;
                count++;
            }

            lineX = 72;
            Console.ForegroundColor = ConsoleColor.Yellow;
            scene.SetCursorString(lineX, lineY++, "0.나가기", false);
            scene.SetCursorString(lineX, lineY++, "1.아이템 구매하기", false);
            scene.SetCursorString(lineX, lineY++, "2.아이템 판매하기", false);
            scene.SetCursorString(lineX, lineY++, "3.아이템 강화하기", false);

            lineX = 63;
            int num = scene.InputString(0, 3, 0, "원하시는 행동의 번호를 입력하세요.", lineX, lineY);


            if(num == 0)
            {
                return false;
            }else if( num == 1)
            {
                CheckBuyItem(scene, player);
            }
            else if( num == 2)
            {
                CheckSellItem(scene, player);
            }
            else if( num == 3)
            {
                CheckUpGradeItem(scene, player);
            }
            else
            {
                ShowItemList(scene, player);
            }
            return true;
        }

        public static void CheckBuyItem(Scene scene, Player player)
        {
            scene.SetCursorString(lineX, lineY+1 , "                                                  ", true);
            int num = scene.InputString(0, items.Length, 0, "구매할 아이템의 번호를 입력하세요. (취소는 0)", lineX, lineY);
            if( num == -1)
            {
                ShowItemList(scene, player);
            }

            if ( num == 0)
            {
                ShowItemList(scene, player);
            }
            else
            {
                scene.SetCursorString(lineX, lineY + 1, "                                                  ", true);
                int count = scene.InputString(1, 10, 0, "몇개 구입하시겠습니까? (최대 10개)", lineX, lineY);
                if (num == -1)
                {
                    ShowItemList(scene, player);
                }
                else
                {
                    bool result = false; 
                    result = BuyItem(num, count, player);         // 플레이어와 입력 번호 바이아이템 호출
                    if (result)
                    {
                        scene.SetCursorString(lineX, lineY++, "구매에 성공하였습니다.", false);
                        Thread.Sleep(1000);
                        ShowItemList(scene, player);
                    }
                    else
                    {
                        scene.SetCursorString(lineX, lineY++, "구매에 실패하였습니다. 골드가 부족합니다.", false);
                        Thread.Sleep(1000);
                        ShowItemList(scene, player);
                    }
                }
            }
        }
        public static bool BuyItem(int num , int count, Player player)
        {
            if ( player.Gold < items[num - 1].Price * count)         //가지고 있는돈이 가격보다 적다면 구매 실패
            {
                return false;
            }
            else
            {
                player.Gold -= items[num - 1].Price * count;
                player.ItemAdd(items[num - 1]);
                return true;
            }
        }

        public static void CheckSellItem(Scene scene, Player player)
        {
            scene.SetCursorString(lineX, lineY + 1, "                                                  ", true);
            int num = scene.InputString(0, player.Inventory.Length, 0, "판매할 아이템의 인벤토리 번호를 입력하세요. (취소는 0)", lineX, lineY);
            if (num == -1)
            {
                ShowItemList(scene, player);
            }

            if (num == 0)
            {
                ShowItemList(scene, player);
            }
            else
            {
                SellItem(num, player);
                scene.SetCursorString(lineX, lineY++, "판매에 성공하였습니다.", false);
                Thread.Sleep(1000);
                ShowItemList(scene, player);
            }
        }
        public static void SellItem(int num, Player player)
        {
            player.Gold += player.Inventory[num-1].Price / 2;    // 해당 아이템의 구매가격의 절반으로 돌려받음
            player.ItemDelete(player.Inventory[num - 1]);      //인벤토리에서 해당 아티엠을 삭제

        }

        public static void CheckUpGradeItem(Scene scene, Player player)
        {
            scene.SetCursorString(lineX, lineY + 1, "                                                  ", true);
            int num = scene.InputString(0, player.Inventory.Length, 0, "강화할 아이템의 인벤토리 번호를 입력하세요. (취소는 0)", lineX, lineY);
            if (num == -1)
            {
                ShowItemList(scene, player);
            }

            if (num == 0)
            {
                ShowItemList(scene, player);
            }
            else
            {
                int result = UpGradeItem(num, player);
                switch (result)
                {
                    case 1:
                        scene.SetCursorString(lineX, lineY++, "강화실패로 아이템이 파괴되었습니다.", false);
                        break;
                    case 2:
                        scene.SetCursorString(lineX, lineY++, "강화에 실패하였습니다.", false);
                        break;
                    case 3:
                        scene.SetCursorString(lineX, lineY++, "강화에 성공하였습니다.", false);
                        break;
                    case 4:
                        scene.SetCursorString(lineX, lineY++, "골드가 부족합니다.", false);
                        break;
                }

                Thread.Sleep(1000);
                ShowItemList(scene, player);
            }
        }
         public static int UpGradeItem(int num, Player player)
         {
            Random random = new Random();
            Item item = player.Inventory[num-1];

            if (player.Gold >= 500)              //강화 비용 확인
            {
                player.Gold -= 500;             //강화비용 차감
            }
            else
            {
                return 4;           //비용 부족으로 강화 실패
            }


            int success = random.Next(0, 100);
            if(success == 0){          // 1% 확률로 아이템 파괴
                player.ItemDelete(item);
                return 1;
            }
            else if (1 <= success && success <= 20 )
            {
                return 2;    // 20% 확률로 강화실패
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

        }
    }
}
