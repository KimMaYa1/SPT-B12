using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject
{
    internal class Scene
    {
        Player player;
        Monster[] monster;
        public Scene()
        {
            player = new Player("조범준", "전사", 0, 0, 0);
        }

        public int InputString(int min, int max)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();
            int inputNum;

            while (!int.TryParse(input, out inputNum) || !(inputNum >= min && inputNum <= max))
            {
                Console.WriteLine("=====================");
                Console.WriteLine("  잘못된 입력입니다");
                Console.WriteLine("=====================");
                Console.WriteLine();
                Console.WriteLine("다시 입력해주세요.");
                Console.WriteLine(">>");
                input = Console.ReadLine();
            }
            return inputNum;
        }

        public void DisplayStart()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine();
            
            int inputNum = InputString(1, 2);

            bool isStat = true;
            if (inputNum == 1)
            {
                while (isStat)
                {
                    isStat = DisplayStat();
                }
            }
            else if(inputNum == 2)
            {
                while (isStat)
                {
                    isStat = DisplayBattle();
                }
            }
        }

        public bool DisplayStat()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("레벨   | {0}",player.level);
            Console.WriteLine("경험치 | {0}",player.exp);
            Console.WriteLine("직업   | {0} ",player.chrd);
            Console.WriteLine("공격력 | {0}",player.atk);
            Console.WriteLine("방어력 | {0}", player.def);
            Console.WriteLine("체력   | {0}",player.hp);
            Console.WriteLine("돈     | {0} G",player.gold);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int inputNum = InputString(0,0);

            if(inputNum == 0)
                return false;

            return true;
        }

        public bool DisplayBattle()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine("");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();

            int inputNum = InputString(1, 1);

            if (inputNum == 0)
                return false;

            return true;
        }

        public void DisplayInventory()
        {
            ItemList();
        }

        public void DisplayClear()
        {

        }

        public void ItemList()
        {

        }

        public void RanMonster()
        {
            
        }

        public void MonsterInfo(Monster monster)
        {

        }

        public void PlayerInfo(Player player)
        {

        }
    }
}
