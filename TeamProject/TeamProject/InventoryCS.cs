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
            Console.Clear();
            Console.WriteLine("인벤토리\n");
            Console.WriteLine("이름\t\t| 공격력| 방어력| 체력\t| 마나\t| 수량\t| 설명\n");
            foreach(Item i in player.Inventory)
            {
                if(i.Type != 2)
                {
                    Console.WriteLine($"{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n");
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
                Console.WriteLine($"{HPotions[0].Name}\t| {HPotions[0].EqAtk}\t| {HPotions[0].EqDef}\t| {HPotions[0].EqHP}\t| {HPotions[0].EqMP}\t| {HPotions.Count}\t| {HPotions[0].Info}\n");
            }
            if (MPotions.Count > 0)
            {
                Console.WriteLine($"{MPotions[0].Name}\t| {MPotions[0].EqAtk}\t| {MPotions[0].EqDef}\t| {MPotions[0].EqHP}\t| {MPotions[0].EqMP}\t| {MPotions.Count}\t| {MPotions[0].Info}\n");
            }
            bool isEq = true;
            while (isEq )
            {
                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("1. 장착 관리");
                int key = scene.InputString(0, 1, 0, "행동 선택");
                if (key == 0)
                {
                    isEq = false;
                }
                else if (key == 1)
                {
                    DisplayEq(player, scene);
                }
            }
            return isEq;
        }
        static void DisplayEq(Player player, Scene scene)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리\n");
            int count = 0;
            Dictionary<int, Item> items= new Dictionary<int, Item>();
            foreach(Item i in player.Inventory)
            {
                if (i.Type != 2)
                {
                    count++;
                    Console.WriteLine($"{count}.\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n");
                    items.Add(count, i);
                }
                /*else if (i == HPotions[0])
                {
                    count++;
                    Console.WriteLine($"{count}.\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| {HPotions.Count}\t| {i.Info}\n");
                    items.Add(count, i);
                }
                else if (i == MPotions[0])
                {
                    count++;
                    Console.WriteLine($"{count}.\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| {MPotions.Count}\t| {i.Info}\n");
                    items.Add(count, i);
                }*/
            }
            bool inf = true;
            Console.WriteLine("0. 나가기");
            while(inf)
            {
                int key = scene.InputString(0, items.Count, 0, "장착할 아이템을 선택해 주세요");
                if (key == 0)
                {
                    inf = false;
                }
                else
                {
                    player.ItemEq(items[key]);
                    Console.Clear();
                    Console.WriteLine("인벤토리 - 장착 관리\n");
                    count = 0;
                    foreach (Item i in items.Values)
                    {
                        if (i.Type != 2)
                        {
                            count++;
                            if (i.IsEquiped)
                            {
                                Console.WriteLine($"{count}.[E]\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n");
                            }
                            else
                            {
                                Console.WriteLine($"{count}.\t{i.Name}\t| {i.EqAtk}\t| {i.EqDef}\t| {i.EqHP}\t| {i.EqMP}\t| 1\t| {i.Info}\n");
                            }
                        }
                    }
                    Console.WriteLine("0. 나가기");
                }
            }
        }
    }
}
