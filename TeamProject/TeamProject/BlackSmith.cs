using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace TeamProject
{
    internal class BlackSmith
    {
        public Item SmithItem;
        public string? Req1Name;
        public string? Req2Name;
        public string? Req3Name;
        public string? Req4Name;

        public int? Req1Num;
        public int? Req2Num;
        public int? Req3Num;
        public int? Req4Num;
        public int?[] ReqNums = new int?[4];
        public string?[] ReqNames = new string?[4];

        public static ItemTable[] CombinationTable = GetItemCombinationTable();
        public BlackSmith() { }
        public BlackSmith(Item item)
        {
            ItemTable matchingTable = CombinationTable.Where(it => it.CombItem.Name == item.Name).ToArray()[0];         // 파라미터로 주어진 아이템이름과 조합표 아이템 이름이 같은가?
            SmithItem = matchingTable.CombItem;
            string[] tableArray = matchingTable.Requirements;                                   // Req1Name, Req1Num.. 부분을 배열로 한 것
            Req1Name = tableArray.Length > 0 ? tableArray[0] : null;                            // 배열의 갯수에 따라, 배열상에 존재하지 않으면 null. 존재하면 배열의 값
            Req2Name = tableArray.Length > 2 ? tableArray[2] : null;
            Req3Name = tableArray.Length > 4 ? tableArray[4] : null;
            Req4Name = tableArray.Length > 6 ? tableArray[6] : null;
            Req1Num = tableArray.Length > 1 ? int.Parse(tableArray[1]) : null;                   // 문자열 배열로 받았으므로, int로 Parse
            Req2Num = tableArray.Length > 3 ? int.Parse(tableArray[3]) : null;
            Req3Num = tableArray.Length > 5 ? int.Parse(tableArray[5]) : null;
            Req4Num = tableArray.Length > 7 ? int.Parse(tableArray[7]) : null;
            ReqNums = new int?[] { Req1Num, Req2Num, Req3Num, Req4Num };
            ReqNames = new string?[] { Req1Name, Req2Name, Req3Name, Req4Name };
        }

        public struct ItemTable
        {
            public Item CombItem;
            public string[] Requirements;

            public ItemTable(Item item, string[] requirements)
            {
                CombItem = item;
                Requirements = requirements;
            }
        }

        public static ItemTable[] GetItemCombinationTable()
        {
            ItemTable[] ItemCombinationTable = new ItemTable[] { };                   // Return할 구조체를 요소로 하는 배열 (각 아이템마다, 조합에 필요한 아이템의 종류가 달라서 채택함,)

            string fullPath = Pathes.ItemCombPath();
            string[] ItemCombTable = File.ReadAllLines(fullPath, Encoding.UTF8);     // ItemCombTable.csv의 각 줄을 요소로 하는 배열
            string[] propertyNames = ItemCombTable[0].Split(',');                    // ItemCombTable.csv의 컬럼을 배열로
            string[] ItemCombinations = ItemCombTable.Skip(1).ToArray();             // 본문(컬럼 아래의 내용)의 각 줄을 요소로 하는 배열

            for (int itemIdx = 0; itemIdx < ItemCombinations.Length; itemIdx++)
            {
                string[] EachItemComb = ItemCombinations[itemIdx].Split(",");         // ItemCombTable.csv의 ItemIdx 번째 줄을 , 로 split하여 배열로 만듦.
                string[] EachCombination = new string[] { };                          // 공백을 제외한 재료아이템 이름과 재료 아이템 수를 가진 string배열
                for (int reqIdx = 1; reqIdx < EachItemComb.Length - 7; reqIdx++)
                {
                    if (EachItemComb[reqIdx] != "")       // csv 상, 재료와 개수가 적혀져 있지 않은 부분은 Pass
                    {
                        Array.Resize(ref EachCombination, EachCombination.Length + 1);
                        EachCombination[EachCombination.Length - 1] = EachItemComb[reqIdx];
                    }
                }

                string name = EachItemComb[Array.IndexOf(propertyNames, "Name")];           // 컬럼배열의 idx 를 ItemCombTable.csv의 ItemIdx 번째 줄을 , 로 split하여 배열의 idx에 대입
                string info = EachItemComb[Array.IndexOf(propertyNames, "Info")];
                int type = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "Type")]);
                int eqAtk = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqAtk")]);
                int eqDef = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqDef")]);
                int eqHP = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqHP")]);
                int eqMP = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqMP")]);
                int price = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "Price")]);

                Item tempItem = new Item(eqAtk, eqDef, eqHP, eqMP, type, price, name, info);  // csv 상의 컬럼과 값으로 Item 선언

                ItemTable tempTable = new ItemTable(tempItem, EachCombination);
                Array.Resize(ref ItemCombinationTable, ItemCombinationTable.Length + 1);
                ItemCombinationTable[itemIdx] = tempTable;
            }
            return ItemCombinationTable;
        }

        public bool DisplaySmith(Player player, Scene scene)                    // Scene에서 한다면, Scene scene은 빠질듯요?
        {
            Console.Clear();
            scene.DrawStar();                                            // scene에서 가져온 메서드도 지우셔야할듯

            int lineX = 60;
            int lineY = 3;

            CombItemList(player, scene, lineX, ref lineY, false);
            
            lineY += 3;
            lineX = 60;
            scene.SetCursorString(lineX, lineY++, "        1. 제작", false);
            scene.SetCursorString(lineX, lineY++, "       0. 나가기", false);

            int input = scene.InputString(0, 1, 0, "원하시는 행동을 입력하세요.", lineX, lineY);
            lineY += 3;
            lineX -= 6;
            if (input == 0)
            {
                scene.SetCursorString(lineX, lineY++, "=====================================", false);
                scene.SetCursorString(lineX, lineY++, "            시작창으로 이동중", false);
                scene.SetCursorString(lineX, lineY++, "=====================================", false);
                Thread.Sleep(1000);
                return false;
            }
            else if (input == 1)
            {
                scene.SetCursorString(lineX, lineY++, "=====================================", false);
                scene.SetCursorString(lineX, lineY++, "            제작창으로 이동중", false);
                scene.SetCursorString(lineX, lineY++, "=====================================", false);
                Thread.Sleep(1000);
                bool isEndCreate = true;
                while (isEndCreate)
                {
                    isEndCreate = DisplayCreate(player, scene);
                }
            }
            return true;
        }

        public bool DisplayCreate(Player player, Scene scene)                    // Scene에서 한다면, Scene scene은 빠질듯요?
        {
            Console.Clear();
            scene.DrawStar();                                            // scene에서 가져온 메서드도 지우셔야할듯

            int lineX = 60;
            int lineY = 10;

            int length = CombItemList(player, scene, lineX, ref lineY, true);

            lineY += 3;
            lineX = 60;
            scene.SetCursorString(lineX, lineY++, "       0. 나가기", false);

            int input = scene.InputString(0, length-1, 0, "원하시는 행동을 입력하세요.", lineX, lineY);
            lineY += 3;
            lineX -= 6;
            if (input == 0)
            {
                scene.SetCursorString(lineX, lineY++, "=====================================", false);
                scene.SetCursorString(lineX, lineY++, "           대장간창으로 이동중", false);
                scene.SetCursorString(lineX, lineY++, "=====================================", false);
                Thread.Sleep(1000);
                return false;
            }
            else if (input >= 1 && input <= length-1)
            {
                Item targetItem = CombinationTable[input - 1].CombItem;
                BlackSmith targetSmith = new BlackSmith(targetItem);
                bool isCreat = false;
                for (int i = 0; i < 4 && targetSmith.ReqNums[i] > 0; i++)
                {
                    if (!(targetSmith.ReqNums[i] <= player.Inventory.Where(item => item.Name == targetSmith.ReqNames[i]).ToArray().Length))
                    {
                        scene.SetCursorString(lineX, lineY++, "=====================================", false);
                        scene.SetCursorString(lineX, lineY++, "            재료가 부족합니다", false);
                        scene.SetCursorString(lineX, lineY++, "=====================================", false);
                        isCreat = false;
                        break;
                    }
                    isCreat = true;
                }
                if (isCreat)
                {
                    for (int i = 0; i < 4 && targetSmith.ReqNums[i] > 0; i++)
                    {
                        int count = 0;
                        for (int j = 0; j < player.Inventory.Length; j++)
                        {
                            if (player.Inventory[j].Name == targetSmith.ReqNames[i])
                            {
                                player.ItemDelete(player.Inventory[j]);
                                count++;
                            }
                            if(count>= targetSmith.ReqNums[i])
                            {
                                break;
                            }
                        }
                    }
                    player.ItemAdd(targetItem);
                    scene.SetCursorString(lineX, lineY++, "=====================================", false);
                    scene.SetCursorString(lineX, lineY++, $"   {targetItem.Name}을 제작하였습니다.", false);
                    scene.SetCursorString(lineX, lineY++, "=====================================", false);
                }
                Thread.Sleep(1000);
            }
            return true;
        }

        public int CombItemList(Player player, Scene scene, int lineX, ref int lineY , bool isCreate)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            scene.SetCursorString(lineX, lineY++, "      [대장간]", false); //false 가 줄넘김
            scene.SetCursorString(lineX, lineY++, "무엇이든 만들어드립니다...", false);
            lineY +=2;
            lineX = 7;
            Console.ForegroundColor = ConsoleColor.White;
            scene.SetCursorString(lineX, lineY++, "----------------------------------------------------------------------------------------------------------------------------------", false);
            scene.SetCursorString(lineX, lineY++, "제작 아이템         ||   재료이름    ||필요숫자||   재료이름    ||필요숫자||   재료이름    ||필요숫자||   재료이름    ||필요숫자||", false);
            scene.SetCursorString(lineX, lineY++, "----------------------------------------------------------------------------------------------------------------------------------", false);
            int maxItemLength = 0;
            var stallArray = new List<string>();
            int num = 1;

            foreach (BlackSmith.ItemTable table in BlackSmith.GetItemCombinationTable())
            {
                lineY++;
                var stallElementList = new List<string>();
                BlackSmith itemSmith = new BlackSmith(table.CombItem);
                maxItemLength = (maxItemLength > itemSmith.SmithItem.Name.Length) ? maxItemLength : itemSmith.SmithItem.Name.Length;
                stallElementList.Add(itemSmith.SmithItem.Name);                    // 출력하기 위한 리스트에 아이템 이름 추가

                var blackSmithFields = typeof(BlackSmith).GetFields();
                foreach (var field in blackSmithFields)
                {
                    var value = field.GetValue(itemSmith);

                    if (value != null && field.Name.Contains("Req"))                // Req1Name 과 같은 필드 중 값이 있는 것만 그 값을 리스트에 추가
                    {
                        stallElementList.Add(value.ToString());
                    }
                }
                
                lineX = 7;
                if (isCreate)
                {
                    scene.SetCursorString(4, lineY, $"{num++}.", false);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                scene.SetCursorString(lineX, lineY, $"{stallElementList[0]}", false);
                Console.ForegroundColor = ConsoleColor.White;
                lineX += 20;
                string showString = "||";
                int a = 0;
                int length = 0;
                while (!int.TryParse(stallElementList[length + 1], out a))
                {
                    length++;
                }
                for (int i = 1; i <= length; i++)
                {
                    try
                    {
                        int j = i;
                        while (!int.TryParse(stallElementList[j + 1], out a))
                        {
                            j++;
                        }
                        showString += $" {Utils.LimitString(stallElementList[i], 6)} ||  {player.Inventory.Where(item => item.Name == stallElementList[i]).ToArray().Length} / {a.ToString()} ||";
                    }
                    catch
                    {
                        continue;
                    }
                }
                scene.SetCursorString(lineX, lineY++, showString, false);
            }
            return num;
        }
    }

    class Utils
    {
        public static float CountString(string str)                         // 숫자, 띄어쓰기, /는 반칸, 한글은 두칸 써먹음. 이를 반영하여, 전체 글자 수 계산
        {                                                                   // ex, 토끼의 분노 => 5.5
            char space = ' ';
            int spaceFreq = str.Count(f => f == space);
            int stringLength = str.Length;
            string result = Regex.Replace(str, @"[^0-9\/]", "");
            return ((float)stringLength - ((float)spaceFreq + result.Length) * 0.5f);
        }

        public static string LimitString(string str, int limitNum)         // 아이템 이름이 제한 글자수 (칸당 글자수)를 넘으면 적당히 잘라서.. 으로 보이게 하는 메서드
        {
            string resultString = (str.Length > limitNum) ? str.Substring(0, 6) + ".." : str;
            string copy = resultString;
            for (int i = 0; i < ((float)limitNum - CountString(copy)) * 2; i++)
            {
                resultString += " ";
            }
            if (str.Length <= limitNum)
            {
                resultString += " ";
            }
            return resultString;
        }
    }
}
