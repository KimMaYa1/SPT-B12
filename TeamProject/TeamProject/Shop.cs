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
            scene.DrawStar();

            lineX = 4;
            lineY = 2;
            
            scene.SetCursorString(77, lineY++, "상점", false);
            lineY++;
            scene.SetCursorString(33, lineY, "이름", false);
            scene.SetCursorString(58, lineY++, "|공격력|방어력 | +HP \t| +MP \t| 가격\t| 설명", false);
            scene.SetCursorString(30, lineY++, "-----------------------------------------------------------------------------------------------", false);
            lineY++;

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
            //lineY ++;

            scene.SetCursorString(72, lineY++, "0.나가기", false);
            scene.SetCursorString(72, lineY++, "1.아이템 구매하기", false);
            scene.SetCursorString(72, lineY++, "2.아이템 판매하기", false);
            scene.SetCursorString(72, lineY++, "3.아이템 강화하기", false);

            int num = scene.InputString(0, 3, 0, "원하시는 행동의 번호를 입력하세요.", 63, lineY);
            lineY += 2;


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
            else
            {
                CheckUpGradeItem(scene, player);
            }
            return true;
        }

        public static void CheckBuyItem(Scene scene, Player player)
        {
            int num = scene.InputString(0, items.Length, 0, "구매할 아이템의 번호를 입력하세요. (취소는 0)", 63, lineY);
            lineY += 2;

            if( num == 0)
            {
                DisplayShop(player, scene);
            }


            bool result = BuyItem(num, player);         // 플레이어와 입력 번호 바이아이템 호출
            if (result)
            {
                scene.SetCursorString(63, lineY++, "구매에 성공하였습니다.", false);
                Thread.Sleep(2000);
                DisplayShop(player, scene);
                //ShowItemList(scene, player);
            }
            else
            {
                scene.SetCursorString(63, lineY++, "구매에 실패하였습니다. 골드를 확인해 보세요.", false);
                Thread.Sleep(1000);
                DisplayShop(player, scene);
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
                Item item = new Item(items[num-1].Name);
                player.ItemAdd(item);
                return true;
            }
        }
        public static void CheckSellItem(Scene scene, Player player)
        {
            int num = scene.InputString(0, player.Inventory.Length, 0, "판매할 아이템의 인벤토리 번호를 입력하세요. (취소는 0)", 63, lineY);
            lineY += 2;

            if (num == 0)
            {
                DisplayShop(player, scene);
            }

            SellItem(num, player);
            scene.SetCursorString(63, lineY++, "판매에 성공하였습니다.", false);
            Thread.Sleep(1000);
            DisplayShop(player, scene);
        }
        public static void SellItem(int num, Player player)
        {
            player.Gold += player.Inventory[num-1].Price / 2;    // 해당 아이템의 구매가격의 절반으로 돌려받음
            player.ItemDelete(player.Inventory[num - 1]);      //인벤토리에서 해당 아티엠을 삭제

        }

        public static void CheckUpGradeItem(Scene scene, Player player)
        {
            int num = scene.InputString(0, player.Inventory.Length, 0, "강화할 아이템의 인벤토리 번호를 입력하세요. (취소는 0)", 63, lineY);
            lineY += 2;

            if (num == 0)
            {
                DisplayShop(player, scene);
            }

            bool result = UpGradeItem(num, player);
            if (result)
            {
                scene.SetCursorString(63, lineY++, "강화에 성공하였습니다.", false);
            }
            else
            {
                scene.SetCursorString(63, lineY++, "강화에 실패하였습니다.", false);
            }
            Thread.Sleep(1000);
            DisplayShop(player, scene);
        }

        public static bool UpGradeItem(int num, Player player)
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
                if (player.Inventory[num-1].Type == 0)
                {
                    player.Inventory[num-1].EqAtk += random.Next(1, 6);    //램덤값으로 강화

                }
                else if (player.Inventory[num-1].Type == 1)
                {
                    player.Inventory[num-1].EqDef += random.Next(1, 6);
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
