using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal static class InventoryCS
    {
        static List<Item> Weapons = new List<Item>();
        static List<Item> HPotions = new List<Item>();
        static List<Item> MPotions = new List<Item>();
        static public bool DisplayInventory(Player player, Scene scene)
        {
            bool isEq = true;
            while (isEq)
            {
                int y = 4;
                Console.Clear();
                scene.DrawStar(90, 25);
                scene.SetCursorString(2, 1, "인벤토리", false);
                scene.SetCursorString(2, 3, "이름\t\t| 공격력| 방어력| 체력\t| 마나\t| 수량\t| 설명\n", false);
                foreach(Item i in player.Inventory)
                {
                    if(i.Type != 2)
                    {
                        scene.SetCursorString(2, ++y, $"{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n", false);
                        Weapons.Add(i);
                    }
                    if (i.Name.Contains("HP"))
                    {
                        HPotions.Add(i);
                    }
                    else if (i.Name.Contains("MP"))
                    {
                        MPotions.Add(i);
                    }
                }
                if(HPotions.Count > 0)
                {
                    scene.SetCursorString(2, ++y, $"{HPotions[0].Name}\t| {HPotions[0].EqAtk}\t| {HPotions[0].EqDef}\t| {HPotions[0].EqHP}\t| {HPotions[0].EqMP}\t| {HPotions.Count}\t| {HPotions[0].Info}\n", false);
                }
                if (MPotions.Count > 0)
                {
                    scene.SetCursorString(2, ++y, $"{MPotions[0].Name}\t| {MPotions[0].EqAtk}\t| {MPotions[0].EqDef}\t| {MPotions[0].EqHP}\t| {MPotions[0].EqMP}\t| {MPotions.Count}\t| {MPotions[0].Info}\n", false);
                }
                scene.SetCursorString(2, ++y + 2, "0. 나가기", false);
                scene.SetCursorString(2, ++y + 2, "1. 장착 관리", false);
                scene.SetCursorString(2, ++y + 2, "2. 소비 아이템 관리", false);
                int key = scene.InputString(0, 1, 0, "행동 선택\n", 2, 20);
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
                    DisplayPotion(scene);
                }
            }
            return isEq;
        }
        static void DisplayEq(Player player, Scene scene)
        {
            Console.Clear();
            scene.DrawStar(95, 25);
            scene.SetCursorString(2, 2, "인벤토리 - 장착 관리", false);
            int count = 0;
            int y = 4;
            Dictionary<int, Item> items= new Dictionary<int, Item>();
            foreach(Item i in player.Inventory)
            {
                if (i.Type != 2)
                {
                    count++;
                    scene.SetCursorString(2, ++y, $"{count}.\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n", false);
                    items.Add(count, i);
                }
            }
            bool inf = true;
            scene.SetCursorString(2, ++y, "0. 나가기", false);
            while(inf)
            {
                int key = scene.InputString(0, items.Count, 0, "장착할 아이템을 선택해 주세요", 2, 22);
                if (key == 0)
                {
                    inf = false;
                    Console.Clear();
                }
                else
                {
                    player.ItemEq(items[key]);
                    Console.Clear();
                    scene.DrawStar(95, 20);
                    scene.SetCursorString(2, 2, "인벤토리 - 장착 관리", false);
                    count = 0;
                    y = 4;
                    foreach (Item i in items.Values)
                    {
                        if (i.Type != 2)
                        {
                            count++;
                            if (i.IsEquiped)
                            {
                                scene.SetCursorString(2, ++y, $"{count}.[E]\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n", false);
                            }
                            else
                            {
                                scene.SetCursorString(2, ++y, $"{count}.\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n", false);
                            }
                        }
                    }
                    scene.SetCursorString(2, ++y + 2, "0. 나가기", false);
                }
            }
        }
        static void DisplayPotion(Scene scene)
        {
            Console.Clear();
            scene.SetCursorString(2, 2, "인벤토리 - 소비 아이템 관리", false);
            if (HPotions.Count > 0)
            {
                scene.SetCursorString(2, 4, $"{HPotions[0].Name}\t| {HPotions[0].EqAtk}\t| {HPotions[0].EqDef}\t| {HPotions[0].EqHP}\t| {HPotions[0].EqMP}\t| {HPotions.Count}\t| {HPotions[0].Info}\n", false);
            }
            if (MPotions.Count > 0)
            {
                scene.SetCursorString(2, 6, $"{MPotions[0].Name}\t| {MPotions[0].EqAtk}\t| {MPotions[0].EqDef}\t| {MPotions[0].EqHP}\t| {MPotions[0].EqMP}\t| {MPotions.Count}\t| {MPotions[0].Info}\n", false);
            }
            bool inf = true;
            while(inf)
            {
                int key = scene.InputString(0, 2, 0, "포션을 선택해 주세요", 0, 15);
                if (key == 0)
                {
                    inf = false;
                }
                else if(key == 1)
                {
                    //HPotions[]
                }
            }
            
        }
    }
}
