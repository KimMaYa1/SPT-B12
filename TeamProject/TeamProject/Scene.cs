﻿using System;
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
        Monster[] monsters;
        int beforeHp = 0;
        public Scene()
        {
            player = new Player("조범준", "전사", 10, 5, 100, 30, 0);
        }

        public int InputString(int min, int max, bool isAttack)
        {
            if (!isAttack)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
            }
            else
            {
                Console.WriteLine("대상을 선택해주세요.");
            }
            Console.Write(">> ");
            string input = Console.ReadLine();
            int inputNum;

            while (!int.TryParse(input, out inputNum) || !(inputNum >= min && inputNum <= max))
            {
                Console.WriteLine("=====================");
                Console.WriteLine("  잘못된 입력입니다");
                Console.WriteLine("=====================");
                Console.WriteLine();
                Console.WriteLine("다시 입력해주세요.");
                Console.WriteLine(">> ");
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

            int inputNum = InputString(1, 2, false);

            Console.WriteLine();

            bool isStat = true;
            if (inputNum == 1)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("  상태 보기 창으로 이동중");
                Console.WriteLine("===========================");
                Thread.Sleep(1000);

                while (isStat)
                {
                    isStat = DisplayStat();
                }
            }
            else if (inputNum == 2)
            {
                Console.WriteLine("=====================");
                Console.WriteLine("  전투창으로 이동중");
                Console.WriteLine("=====================");
                Thread.Sleep(1000);

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
            Console.WriteLine("레벨   | {0}", player.level);
            Console.WriteLine("경험치 | {0}", player.exp);
            Console.WriteLine("직업   | {0} ", player.chrd);
            Console.WriteLine("공격력 | {0}", player.atk);
            Console.WriteLine("방어력 | {0}", player.def);
            Console.WriteLine("체력   | {0}", player.hp);
            Console.WriteLine("돈     | {0} G", player.gold);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int inputNum = InputString(0, 0, false);

            if (inputNum == 0)
            {
                Console.WriteLine("=====================");
                Console.WriteLine("  시작창으로 이동중");
                Console.WriteLine("=====================");
                Thread.Sleep(1000);

                return false;
            }

            return true;
        }

        public void BattleInfo(bool isBattle)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine("[몬스터 정보]");
            int j = 4;
            if (!isBattle)
            {
                foreach (Monster mon in monsters)
                {
                    Console.Write("레벨 | {0} \t이름 | {1}", mon.level, mon.name);
                    Console.SetCursorPosition(34, j++);
                    if (mon.hp <= 0)
                    {
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.WriteLine("체력 | {0}", mon.hp);
                    }
                }
            }
            else
            {
                int i = 1;
                foreach (Monster mon in monsters)
                {
                    Console.Write("{0} 레벨 | {1} \t이름 | {2}", i++, mon.level, mon.name);
                    Console.SetCursorPosition(36, j++);
                    if (mon.hp <= 0)
                    {
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.WriteLine("체력 | {0}", mon.hp);
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine("레벨 | {0}\tChrd | {1}", player.level, player.chrd);
            Console.WriteLine("체력 | {0}/100", player.hp);
            Console.WriteLine();
        }

        public bool DisplayBattle()
        {
            beforeHp = player.hp;
            Dungeon dungeon = new Dungeon();
            Monster mon1 = new Monster("공허충", "몬스터", 1);
            Monster mon2 = new Monster("미니언즈", "몬스터", 2);
            monsters = new Monster[2];
            monsters[0] = mon1;
            monsters[1] = mon2;

            //monsters = dungeon.MonsterGen();

            BattleInfo(false);

            Console.WriteLine("1. 공격");
            Console.WriteLine();

            int inputNum = InputString(1, 1, false);

            bool isAttack = true;
            if (inputNum == 1)
            {
                while (isAttack)
                {
                    isAttack = DisplayAttackSelect();
                }
                return false;
            }

            return true;
        }

        public bool DisplayAttackSelect()
        {
            BattleInfo(true);

            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int inputNum = InputString(0, monsters.Length, true);

            if (inputNum == 0)
            {
                return false;
            }
            else if (inputNum >= 1 && inputNum <= monsters.Length)
            {
                //몬스터 데미지
                Console.WriteLine("====================================");
                Console.WriteLine(" {0}에게 {1}의 데미지를 입혔습니다", monsters[inputNum].name, player.atk);
                Console.WriteLine("====================================");
                Console.WriteLine("{0}의 체력 {1} -> {2}", monsters[inputNum].name, monsters[inputNum].hp, monsters[inputNum].hp);
                Thread.Sleep(1000);
                
                Console.WriteLine();
                Console.WriteLine("====================================");
                foreach(Monster monster in monsters)
                {
                    Console.WriteLine(" {0}에게 {1}의 데미지를 입었습니다", monster.name, monster.atk);
                }
                Console.WriteLine("====================================");
                Console.WriteLine("{0}의 체력 {1} -> {2}", player.name, player.hp, player.hp);
                Thread.Sleep(1000);
            }

            if (player.hp <= 0 || IsDeadMonsters())
            {
                DisplayBattleClear();
                return false;
            }

            return true;
        }

        public bool IsDeadMonsters()
        {
            foreach (Monster mon in monsters)
            {
                if (mon.hp > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void DisplayBattleClear()
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Battle!! - Result");
            Console.WriteLine();
            if (player.hp > 0)
            {
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", monsters.Length);

                int beforeExp = player.exp;
                Console.WriteLine();
                Console.WriteLine("레벨   | {0}", player.level);
                Console.WriteLine("경험치 | {0} / {1} -> {2} / {1}", beforeExp, player.exp, player.level * 5);
                Console.WriteLine("체력   | {0} / {1} -> {2} / {1}", beforeHp, player.maxHp, player.hp);
            }
            else
            {
                Console.WriteLine("You Lose");
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            InputString(0, 0, false);

            Console.WriteLine();
            Console.WriteLine("=====================");
            Console.WriteLine("  시작창으로 이동중");
            Console.WriteLine("=====================");
            Thread.Sleep(1000);
        }

        public void DisplayInventory()
        {
            ItemList();
        }

        public void ItemList()
        {
            foreach (Item item in player.inventory)
            {
                Console.WriteLine("이름 | {0}\t공격력 | {1}\t방어력 | {2}\t정보 | {3}", item.name, item.eqAtk, item.eaDef, item.info);
            }
        }
    }
}