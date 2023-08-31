using System;
using System.Collections.Generic;
using System.Linq;
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
            int page = 0;
            bool isEq = false;
            if(player.Inventory.Length > 0)
            {
                foreach (Item i in player.Inventory)
                {
                    if (i.Type == 0 || i.Type == 1)
                    {
                        Weapons.Add(i);
                    }
                    else
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
                }
                isEq = true;
            }
            else
            {
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(4, 1, "인벤토리", false);
                scene.SetCursorString(4, 3, "이름\t| 공격력| 방어력| 체력\t| 마나\t| 수량\t| 설명\n", false);
                scene.SetCursorString(4, 5, "아무것도 없습니다", false);
                Thread.Sleep(1000);
            }
            int temp = 0;
            string nick = "이름";
            if (player.Inventory.Length > 0)
            {
                temp = player.Inventory.Max(name => name.Name.Length);
            }
            while (isEq)
            {
                int y = 4;
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(4, 1, "인벤토리", false);
                scene.SetCursorString(4, 3, $"{nick.PadRight(25, ' ')}\t\t| 공격력| 방어력| 체력\t| 마나\t| 수량\t| 설명\n", false);
                if(Weapons.Count > page * 10 + 10)
                {
                    for (int i = page * 10; i < page * 10 + 10; i++)
                    {
                        scene.SetCursorString(4, ++y, $"{Weapons[i].Name.PadRight(25, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t | 1\t| {Weapons[i].Info}\n", false);
                    }
                }
                else
                {
                    if(page != 0)
                    {
                        for (int i = page * 10; i < Weapons.Count - ((page - 1) * 10); i++)
                        {
                            scene.SetCursorString(4, ++y, $"{Weapons[i].Name.PadRight(25,' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                        }
                    }
                    else
                    {
                        for (int i = page * 10; i < Weapons.Count - page * 10; i++)
                        {
                            scene.SetCursorString(4, ++y, $"{Weapons[i].Name.PadRight(25, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                        }
                    }
                    if (HPotions.Count > 0)
                    {
                        scene.SetCursorString(4, ++y, $"{HPotions[0].Name.PadRight(25, ' ')}\t| {HPotions[0].EqAtk}\t| {HPotions[0].EqDef}\t| {HPotions[0].EqHP}\t| {HPotions[0].EqMP}\t| {HPotions.Count}\t| {HPotions[0].Info}\n", false);
                    }
                    if (MPotions.Count > 0)
                    {
                        scene.SetCursorString(4, ++y, $"{MPotions[0].Name.PadRight(25, ' ')}\t| {MPotions[0].EqAtk}\t| {MPotions[0].EqDef}\t| {MPotions[0].EqHP}\t| {MPotions[0].EqMP}\t| {MPotions.Count}\t| {MPotions[0].Info}\n", false);
                    }
                }
                y++;
                int length = (Weapons.Count / 10) + 1;
                scene.SetCursorString(40, ++y + 2, $"{(page + 1)} / {length}", false);
                scene.SetCursorString(4, ++y + 2, "1. 장착 관리", false);
                scene.SetCursorString(4, ++y + 2, "2. 소비 아이템 관리", false);
                scene.SetCursorString(4, ++y + 2, "3. 다음 페이지", false);
                scene.SetCursorString(4, ++y + 2, "4. 이전 페이지", false);
                scene.SetCursorString(4, ++y + 2, "0. 나가기", false);
                int key = scene.InputString(0, 5, 0, "행동 선택\n", 40, ++y + 3);
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
                        scene.SetCursorString(4, ++y + 2, "마지막 페이지 입니다", false);
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
                        scene.SetCursorString(4, ++y + 2, "처음 페이지 입니다", false);
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
            int page = 0;
            bool inf = true;
            while (inf)
            {
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(2, 2, "인벤토리 - 장착 관리", false);
                int y = 4;
                Info(page, scene, y, 31,"[E]");
                y = 11;
                int length = (Weapons.Count / 10) + 1;
                scene.SetCursorString(40, ++y + 5, $"{(page + 1)} / {length}", false);
                scene.SetCursorString(2, ++y + 5, "1. 다음 페이지", false);
                scene.SetCursorString(2, ++y + 5, "2. 이전 페이지", false);
                scene.SetCursorString(2, ++y + 5, "0. 나가기", false);
                int key = scene.InputString(0, Weapons.Count + 2, 0, "장착할 아이템을 선택해 주세요", 40, 22);
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
                        scene.SetCursorString(40, ++y + 5, "마지막 페이지 입니다", false);
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
                        scene.SetCursorString(40, ++y + 5, "처음 페이지 입니다", false);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    player.ItemEq(Weapons[key - 3]);
                }
            }
        }
        static void DisplayPotion(Player player, Scene scene)
        {
            bool inf = true;
            while (inf)
            {
                Console.Clear();
                scene.DrawStar();
                scene.SetCursorString(4, 2, "인벤토리 - 소비 아이템 관리", false);
                if (HPotions.Count > 0)
                {
                    scene.SetCursorString(4, 4, $"1. {HPotions[0].Name}\t| {HPotions[0].EqAtk}\t| {HPotions[0].EqDef}\t| {HPotions[0].EqHP}\t| {HPotions[0].EqMP}\t| {HPotions.Count}\t| {HPotions[0].Info}\n", false);
                }
                if (MPotions.Count > 0)
                {
                    scene.SetCursorString(4, 6, $"2. {MPotions[0].Name}\t| {MPotions[0].EqAtk}\t| {MPotions[0].EqDef}\t| {MPotions[0].EqHP}\t| {MPotions[0].EqMP}\t| {MPotions.Count}\t| {MPotions[0].Info}\n", false);
                }
                scene.SetCursorString(4, 11, "0. 나가기", false);
                int key = scene.InputString(0, 2, 0, "포션을 선택해 주세요", 40, 15);
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
                            scene.SetCursorString(4, 9, "체력 회복에 성공했습니다.", false);
                        }
                        else
                        {
                            scene.SetCursorString(4, 9, "이미 체력이 가득 찼습니다.", false);
                        }

                    }
                    else
                    {
                        scene.SetCursorString(4, 9, "사용할 수 있는 포션이 없습니다.", false);
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
                            scene.SetCursorString(4, 9, "마나 회복에 성공했습니다.", false);
                        }
                        else
                        {
                            scene.SetCursorString(4, 9, "이미 마나가 가득 찼습니다.", false);
                        }

                    }
                    else
                    {
                        scene.SetCursorString(4, 9, "사용할 수 있는 포션이 없습니다.", false);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        static void Info(int page, Scene scene, int y,int temp, string eq)
        {
            string nick = "이름";
            scene.SetCursorString(4, 3, $"   {nick.PadRight(temp, ' ')}\t\t| 공격력| 방어력| 체력\t| 마나\t| 수량\t| 설명\n", false);
            if (Weapons.Count > page * 10 + 10)
            {
                for (int i = page * 10; i < page * 10 + 10; i++)
                {
                    if (Weapons[i].IsEquiped)
                    {
                        scene.SetCursorString(4, ++y, $"{i + 3}.{eq}{Weapons[i].Name.PadRight(temp, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                    }
                    else
                    {
                        scene.SetCursorString(4, ++y, $"{i + 3}.{Weapons[i].Name.PadRight(temp, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
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
                            scene.SetCursorString(4, ++y, $"{eq}{Weapons[i].Name.PadRight(temp, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                        }
                        else
                        {
                            scene.SetCursorString(4, ++y, $"{i + 3}.{Weapons[i].Name.PadRight(temp, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                        }
                    }
                }
                else
                {
                    for (int i = page * 10; i < Weapons.Count - (page * 10); i++)
                    {
                        if (Weapons[i].IsEquiped)
                        {
                            scene.SetCursorString(4, ++y, $"{eq}{Weapons[i].Name.PadRight(temp, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t | 1\t| {Weapons[i].Info}\n", false);
                        }
                        else
                        {
                            scene.SetCursorString(4, ++y, $"{i + 3}.{Weapons[i].Name.PadRight(temp, ' ')}\t| {Weapons[i].EqAtk}\t| {Weapons[i].EqDef}\t| {Weapons[i].EqHP}\t| {Weapons[i].EqMP}\t| 1\t| {Weapons[i].Info}\n", false);
                        }
                    }
                }
            }
        }
    }
}
