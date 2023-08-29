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
        Player _player;
        Monster[] _monsters;
        int _beforeHp = 0;
        int _stage = 1;
        public Scene()
        {
            _player = new Player("조범준", "전사", 10, 5, 100, 30, 0);
        }

        public int InputString(int min, int max, bool isAttack, int num)
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
            if (num != 0)
            {
                while (!int.TryParse(input, out inputNum) || !(inputNum >= min && inputNum <= max) || _monsters[inputNum - 1].Hp >= 0)
                {
                    Console.WriteLine("=====================");
                    Console.WriteLine("  잘못된 대상입니다");
                    Console.WriteLine("=====================");
                    Console.WriteLine();
                    Console.WriteLine("다시 입력해주세요.");
                    Console.Write(">> ");
                    input = Console.ReadLine();
                }
            }
            else
            {
                while (!int.TryParse(input, out inputNum) || !(inputNum >= min && inputNum <= max))
                {
                    Console.WriteLine("=====================");
                    Console.WriteLine("  잘못된 입력입니다");
                    Console.WriteLine("=====================");
                    Console.WriteLine();
                    Console.WriteLine("다시 입력해주세요.");
                    Console.Write(">> ");
                    input = Console.ReadLine();
                }
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

            int inputNum = InputString(1, 2, false, 0);

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
            Console.WriteLine("이름   | {0}", _player.Name);
            Console.WriteLine("레벨   | {0}", _player.Level);
            Console.WriteLine("경험치 | {0}", _player.Exp);
            Console.WriteLine("직업   | {0} ", _player.Chrd);
            Console.WriteLine("공격력 | {0}", _player.Atk);
            Console.WriteLine("방어력 | {0}", _player.Def);
            Console.WriteLine("체력   | {0} / {1}", _player.Hp, _player.MaxHp);
            Console.WriteLine("돈     | {0} G", _player.Gold);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int inputNum = InputString(0, 0, false, 0);

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
            Console.WriteLine("Battle!! - stage {0}", _stage);
            Console.WriteLine();
            Console.WriteLine("[몬스터 정보]");
            int j = 4;
            if (!isBattle)
            {
                foreach (Monster mon in _monsters)
                {
                    Console.Write("레벨 | {0} \t이름 | {1}", mon.Level, mon.Name);
                    Console.SetCursorPosition(34, j++);
                    if (mon.Hp <= 0)
                    {
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.WriteLine("체력 | {0}", mon.Hp);
                    }
                }
            }
            else
            {
                int i = 1;
                foreach (Monster mon in _monsters)
                {
                    Console.Write("{0} 레벨 | {1} \t이름 | {2}", i++, mon.Level, mon.Name);
                    Console.SetCursorPosition(36, j++);
                    if (mon.Hp <= 0)
                    {
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.WriteLine("체력 | {0}", mon.Hp);
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine("레벨 | {0}\tChrd | {1}", _player.Level, _player.Chrd);
            Console.WriteLine("체력 | {0}/100", _player.Hp);
            Console.WriteLine();
        }

        public bool DisplayBattle()
        {
            _beforeHp = _player.Hp;
            Dungeon dungeon = new Dungeon(_stage);
            _monsters = dungeon.MonsterGen();

            BattleInfo(false);

            Console.WriteLine("1. 공격");
            Console.WriteLine();

            int inputNum = InputString(1, 1, false, 0);

            bool isAttack = true;
            if (inputNum == 1)
            {
                while (isAttack)
                {
                    isAttack = DisplayAttackSelect();
                }
                _stage++;
                return false;
            }
            return true;
        }

        public bool DisplayAttackSelect()
        {
            BattleInfo(true);

            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int isDeadMon = 0;
            foreach (Monster mon in _monsters)
            {
                if (mon.Hp <= 0)
                {
                    isDeadMon = 1;
                }
            }
            int inputNum = InputString(0, _monsters.Length, true, isDeadMon);

            if (inputNum == 0)
            {
                return false;
            }
            else if (inputNum >= 1 && inputNum <= _monsters.Length)
            {
                AttackInfo(_player, _monsters[inputNum - 1]);
                foreach (Monster mon in _monsters)
                {
                    if (mon.Hp > 0)
                    {
                        AttackInfo(mon, _player);
                    }
                }
            }

            if (_player.Hp <= 0 || IsDeadMonsters())
            {
                DisplayBattleClear();
                return false;
            }

            return true;
        }

        public void AttackInfo(Character aCharacter, Character tCharacter)
        {
            int damage = tCharacter.TakeDamage(aCharacter.Atk);
            Console.WriteLine("====================================");
            Console.WriteLine(" {0}이(가) {1}에게 {2}의 데미지를 입혔습니다", aCharacter.Name, tCharacter.Name, damage);
            Console.WriteLine("====================================");
            if (tCharacter.Hp < damage)
            {
                Console.WriteLine("{0}의 체력 {1} -> 0", tCharacter.Name, tCharacter.Hp);
                tCharacter.Hp = 0;
            }
            else
            {
                Console.WriteLine("{0}의 체력 {1} -> {2}", tCharacter.Name, tCharacter.Hp, tCharacter.Hp - damage);
                tCharacter.Hp -= damage;
            }
            Console.WriteLine();
            Thread.Sleep(1000);
        }

        public bool IsDeadMonsters()
        {
            foreach (Monster mon in _monsters)
            {
                if (mon.Hp > 0)
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
            if (_player.Hp > 0)
            {
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", _monsters.Length);

                int beforeExp = _player.Exp;
                Console.WriteLine();
                Console.WriteLine("레벨   | {0}", _player.Level);
                Console.WriteLine("경험치 | {0} / {1} -> {2} / {1}", beforeExp, _player.Level * 5, _player.Exp);
                Console.WriteLine("체력   | {0} / {1} -> {2} / {1}", _beforeHp, _player.MaxHp, _player.Hp);
            }
            else
            {
                Console.WriteLine("You Lose");
                Console.WriteLine("처음부터 다시 시작합니다");
                _player = new Player("조범준", "전사", 10, 5, 100, 30, 0);
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            InputString(0, 0, false, 0);

            Console.WriteLine();
            Console.WriteLine("=====================");
            Console.WriteLine("  시작창으로 이동중");
            Console.WriteLine("=====================");
            Thread.Sleep(1000);
        }

        public void DisplayInventory()
        {

        }

        public void ItemList()
        {

        }
    }
}