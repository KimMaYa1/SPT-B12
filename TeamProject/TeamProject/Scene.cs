using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject
{
    internal class Scene
    {
        Player _player;
        Dungeon _dungeon;
        Monster[] _monsters;
        int _beforeHp = 0;
        int _stage = 1;
        int _round = 1;

        public int InputString(int min, int max, int num, string str)
        {
            Console.WriteLine("{0}", str);
            Console.Write(">> ");
            string input = Console.ReadLine();
            int inputNum;
            if (num != 0 )
            {
                bool isSelect = int.TryParse(input, out inputNum);
                
                bool ismon = false;
                int monnum = inputNum - 1;
                if(monnum >= 0 && monnum < _monsters.Length)
                {
                    if (_monsters[monnum].Hp <= 0)
                        ismon = true;
                }

                while (!isSelect || !(inputNum >= min && inputNum <= max) || ismon)
                {
                    Console.WriteLine("=====================");
                    Console.WriteLine("  잘못된 대상입니다");
                    Console.WriteLine("=====================");
                    Console.WriteLine();
                    Console.WriteLine("다시 입력해주세요.");
                    Console.Write(">> ");
                    input = Console.ReadLine();
                    isSelect = int.TryParse(input, out inputNum);
                    monnum = inputNum - 1;
                    ismon = false;
                    if (monnum >= 0 && monnum < _monsters.Length)
                    {
                        if (_monsters[monnum].Hp <= 0)
                            ismon = true;
                    }
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

        public bool DisplaySelectName()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.(최대 5글자)");
            Console.Write(">> ");
            string name = Console.ReadLine();

            Console.WriteLine();
            if (name.Length > 6)
            {
                Console.WriteLine("=======================");
                Console.WriteLine("  이름이 너무 깁니다.");
                Console.WriteLine("  다시 설정해 주세요.");
                Console.WriteLine("=======================");
            }
            else if (name == "")
            {
                Console.WriteLine("=======================");
                Console.WriteLine("   잘못된 입력입니다.");
                Console.WriteLine("   다시 입력해주세요.");
                Console.WriteLine("=======================");
            }
            else
            {
                Console.WriteLine("=============================");
                Console.WriteLine("      이름 : {0}", name);
                Console.WriteLine("  확정 하시겠습니까 ?(Y/N)");
                Console.Write("        >> ");
                string str = Console.ReadLine();
                if (str == "Y")
                {
                    Console.WriteLine("      확정 하셨습니다.");
                    Console.WriteLine(" 직업선택 창으로 이동합니다.");
                    Console.WriteLine("=============================");
                    Thread.Sleep(1000);
                    bool isNext = true;
                    while (isNext)
                    {
                        isNext = DisplaySelectChrd(name);
                    }
                    return false;
                }
                else if (str == "N")
                {
                    Console.WriteLine("      취소 하셨습니다.");
                    Console.WriteLine("     다시 입력해주세요.");
                    Console.WriteLine("=============================");
                }
                else
                {
                    Console.WriteLine("     잘못된 입력입니다.");
                    Console.WriteLine("   초기 단계로 돌아갑니다.");
                    Console.WriteLine("=============================");
                }
            }
            Thread.Sleep(1000);
            return true;
        }

        public bool DisplaySelectChrd(string name)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("직업 선택");
            Console.WriteLine();
            Console.WriteLine("============");
            Console.WriteLine("  1. 전사");
            Console.WriteLine(" 2. 성직자");
            Console.WriteLine("  3. 궁수");
            Console.WriteLine("============");
            Console.WriteLine();
            int input = InputString(1, 3, 0, "원하는 직업을 선택해주세요.");
            Console.WriteLine();

            string chrd = "";
            if (input == 1)
            {
                chrd = "전사";
            }
            else if (input == 2)
            {
                chrd = "성직자";
            }
            else if (input == 3)
            {
                chrd = "궁수";
            }

            Console.WriteLine("=============================");
            Console.WriteLine("      직업 : {0}", chrd);
            Console.WriteLine("  확정 하시겠습니까 ?(Y/N)");
            Console.Write("        >> ");
            string str = Console.ReadLine();

            if (str == "Y")
            {
                Console.WriteLine("      확정 하셨습니다.");
                Console.WriteLine("    마을로 이동하겠습니다.");
                Console.WriteLine("=============================");
                if (input == 1)
                {
                    _player = new Warrior(name);
                }
                else if (input == 2)
                {
                    _player = new Prist(name);
                }
                else if (input == 3)
                {
                    _player = new Archer(name);
                }

                Thread.Sleep(1000);
                return false;
            }
            else if (str == "N")
            {
                Console.WriteLine("      취소 하셨습니다.");
                Console.WriteLine("     다시 입력해주세요.");
                Console.WriteLine("=============================");
            }
            else
            {
                Console.WriteLine("     잘못된 입력입니다.");
                Console.WriteLine("   초기 단계로 돌아갑니다.");
                Console.WriteLine("=============================");
            }
            Thread.Sleep(1000);
            return true;
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
            Console.WriteLine("경험치 | {0} / {1}", _player.Exp, _player.Level * 5);
            Console.WriteLine("직업   | {0} ", _player.Chrd);
            Console.WriteLine("공격력 | {0}", _player.Atk);
            Console.WriteLine("방어력 | {0}", _player.Def);
            Console.WriteLine("체력   | {0} / {1}", _player.Hp, _player.MaxHp);
            Console.WriteLine("돈     | {0} G", _player.Gold);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int inputNum = InputString(0, 0, 0, "원하시는 행동을 입력해주세요.");

            if (inputNum == 0)
            {
                Console.WriteLine();
                Console.WriteLine("=====================");
                Console.WriteLine("  시작창으로 이동중");
                Console.WriteLine("=====================");
                Thread.Sleep(1000);

                return false;
            }

            return true;
        }

        public void DisplayStart()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.Write("2. 전투 시작 - 현재 스테이지 {0}-", _stage);
            if (_round == 4)
            {
                Console.WriteLine("보스");
            }
            else
            {
                Console.WriteLine("{0}", _round);
            }
            Console.WriteLine();

            int inputNum = InputString(1, 2, 0, "원하시는 행동을 입력해주세요.");

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
                int input = _round;
                if(_round == 4)
                {
                    Console.WriteLine("============================");
                    Console.WriteLine("  라운드 선택창으로 이동중");
                    Console.WriteLine("============================");
                    Thread.Sleep(1000);

                    input = DisplaySelectRound();
                }

                if (input == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("=====================");
                    Console.WriteLine("  시작창으로 이동중");
                    Console.WriteLine("=====================");
                    Thread.Sleep(1000);
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("=====================");
                Console.WriteLine("  전투창으로 이동중");
                Console.WriteLine("=====================");
                Thread.Sleep(1000);

                _dungeon = new Dungeon(_stage, input);
                if (input < 4)
                {
                    _monsters = _dungeon.MonsterGen();
                }
                else
                {
                    _monsters = _dungeon.BossMonsterGen();
                }

                while (isStat)
                {
                    isStat = DisplayBattle(input);
                }
            }
        }

        public void ReStart()
        {
            Console.WriteLine("0. 다시시작");
            Console.WriteLine("1. 나가기");
            Console.WriteLine();
            int input = InputString(0, 1, 0, "다시 시작하시겟습니까?");
            Console.WriteLine();
            if (input == 0)
            {
                RestartApplication();
            }
            else if(input == 1)
            {
                Environment.Exit(0);
            }
        }

        public void RestartApplication()
        {
            string fileName = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(fileName);
            Environment.Exit(0);
        }

        public int DisplaySelectRound()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("라운드 선택");
            Console.WriteLine("원하시는 라운드로 입장하실수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. {0}-1",_stage);
            Console.WriteLine("2. {0}-2",_stage);
            Console.WriteLine("3. {0}-3",_stage);
            Console.WriteLine("4. {0}-보스",_stage);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = InputString(0, 4, 0, "원하시는 던전을 입력해주세요.");

            if (input == 0)
            {
                return 0;
            }
            else
            {
                return input;
            }
        }

        public bool DisplayBattle(int round)
        {
            _beforeHp = _player.Hp;

            BattleInfo(false, round);

            Console.WriteLine("1. 공격");
            Console.WriteLine();

            int inputNum = InputString(1, 1, 0, "원하시는 행동을 입력해주세요.");

            bool isAttack = true;
            if (inputNum == 1)
            {
                while (isAttack)
                {
                    isAttack = DisplayAttackSelect(round);
                }
                if (_player.Hp > 0 && !IsDeadMonsters())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public bool DisplayAttackSelect(int round)
        {
            BattleInfo(true, round);

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
            int inputNum = InputString(0, _monsters.Length, isDeadMon, "대상을 입력해주세요.");

            if (inputNum == 0)
            {
                return false;
            }
            else if (inputNum >= 1 && inputNum <= _monsters.Length)
            {
                AttackInfo(_player, _monsters[inputNum - 1]);
                foreach (Monster mon in _monsters)
                {
                    if(_player.Hp > 0)
                    {
                        if (mon.Hp > 0)
                        {
                            AttackInfo(mon, _player);
                        }
                    }
                }
            }

            if (_player.Hp <= 0 || IsDeadMonsters())
            {
                DisplayBattleClear(round);
                return false;
            }

            return true;
        }

        public void BattleInfo(bool isBattle, int round)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Battle!! - 현재 스테이지 {0}-", _stage);
            if(round == 4)
            {
                Console.WriteLine("보스");
            }
            else
            {
                Console.WriteLine("{0}", round);
            }
            Console.WriteLine();
            Console.WriteLine("[몬스터 정보]");
            int j = 4;
            if (!isBattle)
            {
                foreach (Monster mon in _monsters)
                {
                    Console.Write("레벨 | {0} \t이름 | {1}", mon.Level, mon.Name);
                    Console.SetCursorPosition(37, j++);
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
                    Console.Write("{0}| 레벨 | {1} \t이름 | {2}", i++, mon.Level, mon.Name);
                    Console.SetCursorPosition(37, j++);
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
            Console.WriteLine("체력 | {0}/{1}", _player.Hp, _player.MaxHp);
            Console.WriteLine();
        }

        public void AttackInfo(Character aCharacter, Character tCharacter)
        {
            Console.WriteLine();
            if (tCharacter.Evasion())
            {
                Console.WriteLine("========================================");
                Console.WriteLine(" {0}이(가) {1}의 공격을 회피하였습니다.", tCharacter.Name, aCharacter.Name);
                Console.WriteLine("========================================");
            }
            else
            {
                int damage = tCharacter.TakeDamage(aCharacter.Atk);
                Console.WriteLine("==============================================");
                Console.WriteLine(" {0}이(가) {1}에게 {2}의 데미지를 입혔습니다", aCharacter.Name, tCharacter.Name, damage);
                Console.WriteLine("==============================================");
                Console.WriteLine();
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

        public void DisplayBattleClear(int round)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Battle!! - Result");
            Console.WriteLine();
            if (_player.Hp > 0)
            {
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", _monsters.Length);

                int beforeExp = _player.Exp;
                int exp = _dungeon.GetExp(_monsters);
                _player.Exp += exp;
                int beforeMaxExp = _player.Level * 5;


                if (_player.Exp >= beforeMaxExp)
                {
                    _player.Exp -= _player.Level * 5;
                    _player.Level += 1;
                    _player.Atk += 1;
                    _player.Def += 1;
                    Console.WriteLine();
                    Console.WriteLine("========================");
                    Console.WriteLine("  ☆★☆ 레벨업 ★☆★");
                    Console.WriteLine("========================");
                }

                Console.WriteLine();
                Console.WriteLine("레벨   | {0}", _player.Level);
                Console.WriteLine();
                Console.WriteLine("경험치 | {0} / {1} -> {2} / {3}\t(+{4})", beforeExp, beforeMaxExp, _player.Exp, _player.Level * 5, exp);
                Console.WriteLine();
                Console.WriteLine("체력   | {0} / {1} -> {2} / {1}", _beforeHp, _player.MaxHp, _player.Hp);
                if (_round == 4)
                {
                    if (round == 4)
                    {
                        _stage++;
                        _round = 1;
                    }
                }
                else
                {
                    _round++;
                }
            }
            else
            {
                Console.WriteLine("You Lose");
                if (_player.Hp <= 0)
                {
                    ReStart();
                }
                _stage = 1;
                _round = 1;
                return;
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            InputString(0, 0, 0, "원하시는 행동을 입력해주세요.");

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