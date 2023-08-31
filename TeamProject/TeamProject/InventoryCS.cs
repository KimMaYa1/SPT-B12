﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TeamProject
{
    internal static class InventoryCS
    {
        static List<Item> Weapons = new List<Item>();      // player.Inventory.Where(item => item.Type == 1).ToList() 
        static List<Item> HPotions = new List<Item>();     // 포션류 전부 Type 2에 두었습니다. player.Inventory.Where(item => item.Type == 2 && item.EqMP != 0).ToList() ....?
        static List<Item> MPotions = new List<Item>();     // player.Inventory.Where(item => item.Type == 2 && item.EqHP != 0).ToList() ...?
        static List<Item> EtcItems = new List<Item>();    // 기타 아이템 추가한다면,. Type 3    player.Inventory.Where(item => item.Type == 3).ToList()? 기타아이템도 포션처럼 갯수 보이게 구현 가능할까요?

        static public bool DisplayInventory(Player player, Scene scene)
        {
            EtcItems.Clear();
            int page = 0;
            int x = 20;
            int y = 0;
            bool isEq = false;
            if(player.Inventory.Length > 0)
            {
                foreach (Item i in player.Inventory)
                {
                    if (i.Type == 0 || i.Type == 1)
                    {
                        Weapons.Add(i);
                    }
                    else if(i.Type == 2)
                    {
                        if (i.Name.Contains("HP"))
                        {
                            HPotions.Add(i);
                        }
                        else if (i.Name.Contains("MP"))
                        {
                            MPotions.Add(i);
                        }
                    }
                    else if(i.Type == 3)
                    {
                        EtcItems.Add(i);
                    }
                }
                isEq = true;
            }
            else
            {
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(x + 50, 2, "인벤토리", false);
                scene.SetCursorString(x + 20, 4, "이름\t| 공격력| 방어력| 체력\t| 마나\t| 수량\t| 설명\n", false);
                scene.SetCursorString(x + 40, 10, "아무것도 없습니다", false);
                Thread.Sleep(1000);
            }
            int temp = 0;
            if (player.Inventory.Length > 0)
            {
                temp = player.Inventory.Max(name => name.Name.Length);
            }
            while (isEq)
            {
                y = 6;
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(x + 50, 2, "인벤토리", false);
                scene.SetCursorString(x, 4, "이름", false);
                scene.SetCursorString(x + 23, 4, "|공격력", false);
                scene.SetCursorString(x + 33, 4, "|방어력\t|체력\t|마나\t|수량\t|설명\n", false);
                if(Weapons.Count > page * 10 + 10)
                {
                    for (int i = page * 10; i < page * 10 + 10; i++)
                    {
                        scene.SetCursorString(x, y, $"{Weapons[i].Name}", false);
                        scene.SetCursorString(x + 23, y, $"| {Weapons[i].EqAtk}", false);
                        scene.SetCursorString(x + 33, y++, $"| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}", false);
                        y++;
                    }
                }
                else
                {
                    if(page != 0)
                    {
                        for (int i = page * 10; i < Weapons.Count - ((page - 1) * 10); i++)
                        {
                            scene.SetCursorString(x, y, $"{Weapons[i].Name}", false);
                            scene.SetCursorString(x + 23, y, $"| {Weapons[i].EqAtk}", false);
                            scene.SetCursorString(x + 33, y++, $"| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}", false);
                            y++;
                        }
                    }
                    else
                    {
                        for (int i = page * 10; i < Weapons.Count - page * 10; i++)
                        {
                            scene.SetCursorString(x, y, $"{Weapons[i].Name}", false);
                            scene.SetCursorString(x + 23, y, $"| {Weapons[i].EqAtk}", false);
                            scene.SetCursorString(x + 33, y++, $"| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}", false);
                            y++;
                        }
                    }
                    if (HPotions.Count > 0)
                    {
                        scene.SetCursorString(x, y, $"{HPotions[0].Name}", false);
                        scene.SetCursorString(x + 23, y, $"| {HPotions[0].EqAtk}", false);
                        scene.SetCursorString(x + 33, y++, $"| {HPotions[0].EqDef}\t| {HPotions[0].EqHP}\t| {HPotions[0].EqMP}\t| {HPotions.Count}\t| {HPotions[0].Info}", false);
                        y++;
                    }
                    if (MPotions.Count > 0)
                    {
                        scene.SetCursorString(x, y, $"{MPotions[0].Name}", false);
                        scene.SetCursorString(x + 23, y, $"| {MPotions[0].EqAtk}", false);
                        scene.SetCursorString(x + 33, y++, $"| {MPotions[0].EqDef}\t| {MPotions[0].EqHP}\t| {MPotions[0].EqMP}\t| {MPotions.Count}\t| {MPotions[0].Info}", false);
                        y++;
                    }
                    if(EtcItems.Count > 0)
                    {
                        List<string> item0 = new List<string>();
                        for(int i = 0; i< EtcItems.Count; i++)
                        {
                            int count = 0;
                            foreach (Item item in EtcItems)
                            {
                                count = EtcItems.Where(name => name.Name == EtcItems[i].Name).Count();
                            }
                            if (!item0.Contains(EtcItems[i].Name))
                            {
                                item0.Add( EtcItems[i].Name);
                                scene.SetCursorString(x, y, $"{EtcItems[i].Name}", false);
                                scene.SetCursorString(x + 23, y, $"| {EtcItems[i].EqAtk}", false);
                                scene.SetCursorString(x + 33, y++, $"| {EtcItems[i].EqDef}\t| {EtcItems[i].EqHP}\t| {EtcItems[i].EqMP}\t| {count}\t| {EtcItems[i].Info}\n", false);
                                y++;
                            }
                        }
                    }
                }
                y++;
                int length = (Weapons.Count / 10) + 1;
                scene.SetCursorString(x + 50, y, $"{(page + 1)} / {length}", false);
                scene.SetCursorString(x + 20, --y, "1. 장착 관리", false);
                scene.SetCursorString(x + 20, ++y, "2. 소비 아이템 관리", false);
                scene.SetCursorString(x + 20, ++y, "3. 다음 페이지", false);
                scene.SetCursorString(x + 20, ++y, "4. 이전 페이지", false);
                scene.SetCursorString(x + 20, ++y, "0. 나가기", false);
                int key = scene.InputString(0, 5, 0, "행동 선택\n", x + 70, --y);
                if (key == 0)
                {
                    isEq = false;
                    Console.Clear();
                }
                else if (key == 1)
                {
                    DisplayEq(player, scene);
                }
                else if (key == 2)
                {
                    DisplayPotion(player, scene);
                }
                else if(key == 3)
                {
                    if(Weapons.Count - (page + 1) * 10 > 0)
                    {
                        page++;
                    }
                    else
                    {
                        scene.SetCursorString(x + 65, ++y - 4, "마지막 페이지 입니다", false);
                        Thread.Sleep(1000);
                    }
                }
                else if(key == 4)
                {
                    if(page != 0)
                    {
                        page--;
                    }
                    else
                    {
                        scene.SetCursorString(x + 65, ++y - 4, "처음 페이지 입니다", false);
                        Thread.Sleep(1000);
                    }
                }
                else if(key == 5)
                {
                    page = 0;
                }
            }
            Weapons.Clear();
            HPotions.Clear();
            MPotions.Clear();
            return isEq;
        }
        static void DisplayEq(Player player, Scene scene)
        {
            int x = 20;
            int page = 0;
            bool inf = true;
            while (inf)
            {
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(70, 2, "인벤토리 - 장착 관리", false);
                int y = 7;
                y = Info(page, scene,x, y, 31,"[E]");
                int length = (Weapons.Count / 10) + 1;
                scene.SetCursorString(x + 50, ++y, $"{(page + 1)} / {length}", false);
                scene.SetCursorString(x + 24, ++y + 3, "1. 다음 페이지", false);
                scene.SetCursorString(x + 24, ++y + 3, "2. 이전 페이지", false);
                scene.SetCursorString(x + 24, ++y + 3, "0. 나가기", false);
                int key = scene.InputString(0, Weapons.Count + 2, 0, "장착할 아이템을 선택해 주세요", x + 44, ++y + 4);
                if (key == 0)
                {
                    inf = false;
                    Console.Clear();
                }
                else if (key == 1)
                {
                    if (Weapons.Count - (page + 1) * 10 > 0)
                    {
                        page++;
                    }
                    else
                    {
                        scene.SetCursorString(x + 70, y, "마지막 페이지 입니다", false);
                        Thread.Sleep(1000);
                    }
                }
                else if (key == 2)
                {
                    if (page != 0)
                    {
                        page--;
                    }
                    else
                    {
                        scene.SetCursorString(x + 70, y, "처음 페이지 입니다", false);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    if (Weapons[key - 3].IsEquiped)
                    {
                        player.ItemUnEq(Weapons[key - 3]);
                        scene.SetCursorString(x + 70, y, "장비를 해제했습니다", false);
                    }
                    else
                    {
                        player.ItemEq(Weapons[key - 3]);
                        scene.SetCursorString(x + 70, y, "장비를 장착했습니다", false);
                    }
                    Thread.Sleep(1000);
                }
                
            }
        }
        static void DisplayPotion(Player player, Scene scene)
        {
            int x = 50;
            bool inf = true;
            while (inf)
            {
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(x + 10, 2, "인벤토리 - 소비 아이템 관리", false);
                if (HPotions.Count > 0)
                {
                    scene.SetCursorString(x, 8, $"1. {HPotions[0].Name}\t| {HPotions.Count}\t| {HPotions[0].Info}\n", false);
                }
                if (MPotions.Count > 0)
                {
                    scene.SetCursorString(x, 10, $"2. {MPotions[0].Name}\t| {MPotions.Count}\t| {MPotions[0].Info}\n", false);
                }
                scene.SetCursorString(x, 18, "0. 나가기", false);
                int key = scene.InputString(0, 2, 0, "포션을 선택해 주세요", x, 20);
                if (key == 0)
                {
                    inf = false;
                }
                else if(key == 1)
                {
                    if (HPotions.Count > 0)
                    {
                        if (player.Hp < player.MaxHp)
                        {
                            player.UseItem(HPotions[HPotions.Count - 1]);
                            HPotions.Remove(HPotions[HPotions.Count - 1]);
                            scene.SetCursorString(x, 15, "체력 회복에 성공했습니다.", false);
                        }
                        else
                        {
                            scene.SetCursorString(x, 15, "이미 체력이 가득 찼습니다.", false);
                        }

                    }
                    else
                    {
                        scene.SetCursorString(x, 15, "사용할 수 있는 포션이 없습니다.", false);
                    }
                }
                else if (key == 2)
                {
                    if (MPotions.Count > 0)
                    {
                        if (player.Mp < player.MaxMp)
                        {
                            player.UseItem(MPotions[MPotions.Count - 1]);
                            MPotions.Remove(MPotions[MPotions.Count - 1]);
                            scene.SetCursorString(x, 15, "마나 회복에 성공했습니다.", false);
                        }
                        else
                        {
                            scene.SetCursorString(x, 15, "이미 마나가 가득 찼습니다.", false);
                        }

                    }
                    else
                    {
                        scene.SetCursorString(x, 15, "사용할 수 있는 포션이 없습니다.", false);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        static int Info(int page, Scene scene,int x, int y,int temp, string eq)
        {
            string nick = "이름";
            scene.SetCursorString(x, 5, $"  이름", false);
            scene.SetCursorString(x + 28, 5, $"| 공격력| 방어력| 체력\t| 마나\t| 수량\t| 설명\n", false);
            if (Weapons.Count > page * 10 + 10)
            {
                for (int i = page * 10; i < page * 10 + 10; i++)
                {
                    if (Weapons[i].IsEquiped)
                    {
                        scene.SetCursorString(x, y, $"{eq}{Weapons[i].Name}", false);
                        scene.SetCursorString(x + 28, y++, $"| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                        y++;
                    }
                    else
                    {
                        scene.SetCursorString(x, y, $"{i + 3}.{Weapons[i].Name}", false);
                        scene.SetCursorString(x + 28, y++, $"| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                        y++;
                    }
                }
            }
            else
            {
                if(page != 0)
                {
                    for (int i = page * 10; i < Weapons.Count - ((page - 1) * 10); i++)
                    {
                        if (Weapons[i].IsEquiped)
                        {
                            scene.SetCursorString(x, y, $"{eq}{Weapons[i].Name}", false);
                            scene.SetCursorString(x + 28, y++, $"| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                            y++;
                        }
                        else
                        {
                            scene.SetCursorString(x, y, $"{i + 3}.{Weapons[i].Name}", false);
                            scene.SetCursorString(x + 28, y++, $"| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                            y++;
                        }
                    }
                }
                else
                {
                    for (int i = page * 10; i < Weapons.Count - (page * 10); i++)
                    {
                        if (Weapons[i].IsEquiped)
                        {
                            scene.SetCursorString(x, y, $"{eq}{Weapons[i].Name}", false);
                            scene.SetCursorString(x + 28, y++, $"| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                            y++;
                        }
                        else
                        {
                            scene.SetCursorString(x, y, $"{i + 3}.{Weapons[i].Name}", false);
                            scene.SetCursorString(x + 28, y++, $"| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                            y++;
                        }
                    }
                }
            }
            return y;
        }
    }
}
