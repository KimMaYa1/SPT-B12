﻿using System;
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
            scene.DrawStar(100, 30);
            scene.SetCursorString(lineX + 20, lineY++, "상점", false);
            scene.SetCursorString(lineX, lineY++, "   \t 이름    \t| 공격력|방어력 | +HP \t| +MP \t| 가격\t| 설명", false);

            int count = 1;
            foreach (Item i in items)
            {
                scene.SetCursorString(lineX , lineY++, $"{count}.\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| {i.Price}\t| {i.Info}", false);
                count++;
            }
            lineY += 3;

            scene.SetCursorString(0, lineY++, "0.나가기", false);
            scene.SetCursorString(0, lineY++, "1.아이템 구매하기", false);
            scene.SetCursorString(0, lineY++, "2.아이템 판매하기", false);
            //Console.WriteLine("3. 아이템 강화하기");

            int num = CheckValidInput(0, 2, scene);

            if(num == 0)
            {
                return false;
            }else if( num == 1)
            {
                CheckBuyItem(scene, player);
            }
            else
            {
                CheckSellItem(scene, player);
            }
            return true;
        }

        public static void CheckBuyItem(Scene scene, Player player)
        {
            //lineY += 2;
            scene.SetCursorString(0, lineY++, "어떤 아이템을 구매하시겠습니까?", false);
            int num = CheckValidInput(1, items.Length, scene);

            bool result = BuyItem(num, player);         // 플레이어와 입력 번호 바이아이템 호출
            if (result)
            {
                scene.SetCursorString(0, lineY++, "구매에 성공하였습니다.", false);
                Thread.Sleep(2000);
            }
            else
            {
                scene.SetCursorString(0, lineY++, "구매에 실패하였습니다. 골드를 확인해 보세요.", false);
                Thread.Sleep(1000);
                ShowItemList(scene, player);
            }
        }
        public static bool BuyItem(int num , Player player)
        {
            if ( player.Gold < items[num - 1].Price)         //가지고 있는돈이 가격보다 적다면 구매 실패
            {
                return false;
            }
            else
            {
                player.Gold -= items[num - 1].Price;
                player.ItemAdd(items[num - 1]);
                return true;
            }
        }
        public static void CheckSellItem(Scene scene, Player player)
        {
            //lineY += 2;
            scene.SetCursorString(0, lineY++, "어떤 아이템을 판매하시겠습니까?", false);         //판매할 아이템 선택
            scene.SetCursorString(0, lineY++, "판매할 아이템의 인벤토리 번호를 입력해주세요.", false);  //판매할 아이템 선택
            int num = CheckValidInput(1, player.Inventory.Length, scene);
            SellItem(num, player);
            Console.WriteLine("판매에 성공하였습니다.");
            Thread.Sleep(1000);
            ShowItemList(scene, player);
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

        static int CheckValidInput(int min, int max, Scene scene)
        {
            while (true)
            {
                string input = Console.ReadLine();
                lineY++;
                bool parseSuccess = int.TryParse(input, out int ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }
                scene.SetCursorString(0, lineY++, "잘못된 입력입니다. 다시 입력하세요.", false);
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
