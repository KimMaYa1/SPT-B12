using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
        int _beforeMp = 0;
        int _stage = 1;
        int _round = 1;

        public void DrawStar(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < x; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("*");
            }
            for (int i = 0; i < y; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("*");
                Console.SetCursorPosition(x - 1, i);
                Console.Write("*");
            }
            for (int i = 0; i < x; i++)
            {
                Console.SetCursorPosition(i, y);
                Console.Write("*");
            }
        }
        
        public void SetCursorString(int lineX, int lineY, string str, bool isNextLine)
        {
            if (isNextLine)
            {
                Console.SetCursorPosition(lineX, lineY);
                Console.Write(str);
            }
            else
            {
                Console.SetCursorPosition(lineX, lineY);
                Console.WriteLine(str);
            }
        }

        public int InputString(int min, int max, int num, string str,int lineX, int lineY)
        {
            SetCursorString(lineX, lineY++,str,false);
            SetCursorString(lineX, lineY++, "      >> ",true);
            string input = Console.ReadLine();
            int inputNum;
            lineY++;
            if (num != 0)
            {
                bool isSelect = int.TryParse(input, out inputNum);

                bool ismon = false;
                int monnum = inputNum - 1;
                if (monnum >= 0 && monnum < _monsters.Length)
                {
                    if (_monsters[monnum].Hp <= 0)
                        ismon = true;
                }

                if (!isSelect || !(inputNum >= min && inputNum <= max) || ismon)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    SetCursorString(lineX, lineY++, "=====================",false);
                    SetCursorString(lineX, lineY++, "  잘못된 대상입니다",false);
                    SetCursorString(lineX, lineY++, "=====================",false);
                    Thread.Sleep(1000);
                    return -1;
                }
            }
            else
            {
                if (!int.TryParse(input, out inputNum) || !(inputNum >= min && inputNum <= max))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    SetCursorString(lineX, lineY++, "=====================", false);
                    SetCursorString(lineX, lineY++, "  잘못된 입력입니다", false);
                    SetCursorString(lineX, lineY++, "=====================", false);
                    Thread.Sleep(1000);
                    return -1;
                }
            }
            return inputNum;
        }

        public bool DisplaySelectName()
        {
            Console.Clear();

            DrawStar(60,15);


            int lineX = 12;
            int lineY = 2;
            Console.ForegroundColor = ConsoleColor.Red;
            SetCursorString(8, lineY++, "스파르타 던전에 오신 여러분 환영합니다.", false);
            SetCursorString(7, lineY++, "원하시는 이름을 설정해주세요.(최대 5글자)", false);
            lineY = 5;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(15, lineY++, "이름 : ", true);
            string name = Console.ReadLine();

            lineY = 7;
            if (name.Length >= 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                SetCursorString(lineX, lineY++, "=======================", false);
                SetCursorString(lineX, lineY++, "  이름이 너무 깁니다.", false);
                SetCursorString(lineX, lineY++, "  다시 설정해 주세요.", false);
                SetCursorString(lineX, lineY++, "=======================", false);
            }
            else if (name == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                SetCursorString(lineX, lineY++, "=======================", false);
                SetCursorString(lineX, lineY++, "   잘못된 입력입니다.", false);
                SetCursorString(lineX, lineY++, "   다시 입력해주세요.", false);
                SetCursorString(lineX, lineY++, "=======================", false);
            }
            else
            {
                SetCursorString(lineX, lineY++,"=============================", false);
                SetCursorString(lineX, lineY++, $"      이름 : {name}", false);
                SetCursorString(lineX, lineY++, "  확정 하시겠습니까 ?(Y/N)", false);
                SetCursorString(lineX, lineY++, "        >> ", true);
                string str = Console.ReadLine();
                if (str == "Y")
                {
                    SetCursorString(lineX, lineY++, "      확정 하셨습니다.", false);
                    SetCursorString(lineX, lineY++, " 직업선택 창으로 이동합니다.", false);
                    SetCursorString(lineX, lineY++, "=============================", false);
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
                    SetCursorString(lineX, lineY++, "      취소 하셨습니다.", false);
                    SetCursorString(lineX, lineY++, "     다시 입력해주세요.", false);
                    SetCursorString(lineX, lineY++, "=============================", false);
                }
                else
                {
                    SetCursorString(lineX, lineY++, "     잘못된 입력입니다.", false);
                    SetCursorString(lineX, lineY++, "   초기 단계로 돌아갑니다.", false);
                    SetCursorString(lineX, lineY++, "=============================" , false);
                }
            }
            Thread.Sleep(1000);
            return true;
        }

        public bool DisplaySelectChrd(string name)
        {
            Console.Clear();

            DrawStar(60, 21);

            int lineY = 2;
            int lineX = 15;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "    직업 선택",false);
            lineY++;
            SetCursorString(lineX, lineY++, "   ============",false);
            Console.ForegroundColor = ConsoleColor.Red;
            SetCursorString(lineX, lineY++, "     1. 전사", false);
            Console.ForegroundColor = ConsoleColor.Yellow;
            SetCursorString(lineX, lineY++, "    2. 성직자", false);
            Console.ForegroundColor = ConsoleColor.Green;
            SetCursorString(lineX, lineY++, "     3. 궁수", false);
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "   ============",false);
            lineY++;
            int input = InputString(1, 3, 0, "원하는 직업을 선택해주세요.", lineX-3, lineY);
            if(input == -1)
            {
                return true;
            }
            Console.WriteLine();

            lineY = 13;
            lineX = 12;

            SetCursorString(lineX - 1, lineY++, "=============================", false);
            SetCursorString(lineX, lineY, $"      직업 : ", true);

            string chrd = "";
            if (input == 1)
            {
                chrd = "전사";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (input == 2)
            {
                chrd = "성직자";
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (input == 3)
            {
                chrd = "궁수";
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine(chrd);
            lineY++;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "  확정 하시겠습니까 ?(Y/N)",false);
            SetCursorString(lineX, lineY++, "        >> ", true);
            string str = Console.ReadLine();

            if (str == "Y")
            {
                SetCursorString(lineX, lineY++, "      확정 하셨습니다.", false);
                SetCursorString(lineX, lineY++, "    마을로 이동하겠습니다.", false);
                SetCursorString(lineX-1, lineY++, "=============================", false);
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
                SetCursorString(lineX, lineY++, "      취소 하셨습니다.", false);
                SetCursorString(lineX, lineY++, "   초기 단계로 돌아갑니다.", false);
                SetCursorString(lineX - 1, lineY++, "=============================", false);
            }
            else
            {
                SetCursorString(lineX, lineY++, "     잘못된 입력입니다.", false);
                SetCursorString(lineX, lineY++, "   초기 단계로 돌아갑니다.", false);
                SetCursorString(lineX - 1, lineY++, "=============================", false);
            }
            Thread.Sleep(1000);
            return true;
        }

        public bool DisplayStat()
        {
            Console.Clear();
            DrawStar(40, 26);
            int lineX = 8;
            int lineY = 2;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "상태 보기", false);
            SetCursorString(lineX, lineY++, "캐릭터의 정보가 표시됩니다.", false);
            lineY++;
            SetCursorString(lineX, lineY++, $"이름   | {_player.Name}", false);
            SetCursorString(lineX, lineY++, $"레벨   | {_player.Level}", false);
            SetCursorString(lineX, lineY++, $"경험치 | {_player.Exp} / {_player.Level * 5}", false);
            SetCursorString(lineX, lineY++, $"직업   | {_player.Chrd} ", false);
            SetCursorString(lineX, lineY++, $"공격력 | {_player.Atk}", false);
            SetCursorString(lineX, lineY++, $"방어력 | {_player.Def}", false);
            SetCursorString(lineX, lineY++, $"체력   | ", true);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{_player.Hp} / {_player.MaxHp}");
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, $"마나   | ", true);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{_player.Mp} / {_player.MaxMp}");
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, $"돈     | ", true);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{_player.Gold} G");

            lineY++;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "0. 나가기", true);
            lineY++;

            int inputNum = InputString(0, 0, 0, "원하시는 행동을 입력해주세요.", lineX-3,lineY);

            lineY = 20;
            if (inputNum == 0)
            {
                SetCursorString(lineX, lineY++, "=====================", false);
                SetCursorString(lineX, lineY++, "  시작창으로 이동중", false);
                SetCursorString(lineX, lineY++, "=====================", false);
                Thread.Sleep(1000);

                return false;
            }

            return true;
        }

        public void DisplayStart()
        {
            _beforeHp = _player.Hp;
            _beforeMp = _player.Mp;
            Console.Clear();

            DrawStar(60, 20);

            int lineX = 10;
            int lineY = 2;

            Console.ForegroundColor = ConsoleColor.Red;
            SetCursorString(lineX, lineY++, "스파르타 던전에 오신 여러분 환영합니다.", false);
            SetCursorString(lineX, lineY++, "  이제 전투를 시작할 수 있습니다.", false);
            lineY++;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "           1. 상태 보기", false);
            if (_round == 4)
            {
                SetCursorString(lineX, lineY++, $"  2. 전투 시작  ( 현재 스테이지 {_stage}-보스 )", false);
            }
            else
            {
                SetCursorString(lineX, lineY++, $"  2. 전투 시작 - ( 현재 스테이지 {_stage}-{_round} )", false);
            }
            SetCursorString(lineX, lineY++, "           3. 인벤토리", false);
            SetCursorString(lineX, lineY++, "             4. 상점", false);
            lineY++;
            lineX = 15;
            int inputNum = InputString(1, 3, 0, "원하시는 행동을 입력해주세요.", lineX-3, lineY);

            lineY = 11;
            bool isStat = true;
            if (inputNum == 1)
            {
                SetCursorString(lineX, lineY++, "===========================", false);
                SetCursorString(lineX, lineY++, "  상태 보기 창으로 이동중", false);
                SetCursorString(lineX, lineY++, "===========================", false);
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
                    SetCursorString(lineX, lineY++, "============================", false);
                    SetCursorString(lineX, lineY++, "  라운드 선택창으로 이동중", false);
                    SetCursorString(lineX, lineY++, "============================", false);
                    Thread.Sleep(1000);

                    input = DisplaySelectRound();
                }

                if (input == 0)
                {
                    SetCursorString(lineX, lineY++, "=====================", false);
                    SetCursorString(lineX, lineY++, "  시작창으로 이동중", false);
                    SetCursorString(lineX, lineY++, "=====================", false);
                    Thread.Sleep(1000);
                    return;
                }
                
                SetCursorString(lineX, lineY++, "=====================", false);
                SetCursorString(lineX, lineY++, "  전투창으로 이동중", false);
                SetCursorString(lineX, lineY++, "=====================", false);
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
            else if (inputNum == 3)
            {
                InventoryCS.DisplayInventory(_player, this);
            }
            else if (inputNum == 4)
            {
                Shop.DisplayShop(_player, this);
            }
        }

        public bool ReStart(int lineX, int lineY)
        {
            SetCursorString(lineX, 10, "                     ", false);
            SetCursorString(lineX, 11, "                     ", false);
            SetCursorString(lineX, 12, "                     ", false);
            SetCursorString(lineX, lineY++, "0. 다시시작", false);
            SetCursorString(lineX, lineY++, "1. 나가기", false);
            lineY++;
            int input = InputString(0, 1, 0, "다시 시작하시겟습니까?", lineX, lineY) ;

            Console.WriteLine();
            if (input == 0)
            {
                RestartApplication();
            }
            else if(input == 1)
            {
                Environment.Exit(0);
            }
            return true;
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
            DrawStar(45,15);
            int lineX = 10;
            int lineY = 2;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "라운드 선택", false);
            SetCursorString(3, lineY++, "원하시는 라운드로 입장하실수 있습니다.", false);
            lineY++;
            SetCursorString(lineX, lineY++, $"3. {_stage}-1", false);
            SetCursorString(lineX, lineY++, $"3. {_stage}-2", false);
            SetCursorString(lineX, lineY++, $"3. {_stage}-3", false);
            SetCursorString(lineX, lineY++, $"4. {_stage}-보스", false);
            lineY++;
            SetCursorString(lineX, lineY++, "0. 나가기", false);
            lineY++;

            int input = InputString(0, 4, 0, "원하시는 던전을 입력해주세요.",lineX ,lineY);

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
            int lineY = BattleInfo(false, round);
            int lineX = 10;
            lineY++;
            DrawStar(60, 25);

            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "1. 공격", false);
            SetCursorString(lineX, lineY++, "2. 스킬", false);
            lineY++;

            int inputNum = InputString(1, 2, 0, "원하시는 행동을 입력해주세요.", lineX, lineY);

            bool isAttack = true;
            if (inputNum == 1)
            {
                DisplayAttackSelect(round, 1, 0);
                if (_player.Hp > 0 && !IsDeadMonsters())
                {
                    return true;
                }
                return false;
            }
            if (inputNum == 2)
            {
                while (isAttack)
                {
                    isAttack = DisplaySkillSelect(round);
                }
                if (_player.Hp > 0 && !IsDeadMonsters())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public bool DisplaySkillSelect(int round)
        {
            int lineY = BattleInfo(false, round);
            int lineX = 8;
            lineY++;
            DrawStar(60, 30);

            Console.ForegroundColor = ConsoleColor.White;
            float[][] skill= _player.SkillInfo(this, lineX, lineY);
            lineY += 4;
            SetCursorString(lineX, lineY++, "0. 취소", false);
            lineY++;

            int input = InputString(0, 2, 0, "원하시는 스킬을 입력해주세요.", lineX, lineY) ;

            if (input == 1)
            {
                if (_player.Mp >= skill[1][0])
                {
                    DisplayAttackSelect(round, skill[0][0], (int)skill[1][0]);
                }
                else
                {
                    lineY += 3;
                    SetCursorString(lineX, lineY++, "마나가 부족합니다.", false);
                    Thread.Sleep(1000);
                    return true;
                }
            }
            else if (input == 2)
            {
                if (_player.Mp >= skill[1][1])
                {
                    Random ran = new Random();

                    bool isSkill = true;
                    while (isSkill)
                    {
                        int num1 = ran.Next(0, _monsters.Length);
                        int num2 = ran.Next(0, _monsters.Length);
                        int i = 0;
                        foreach (Monster mon in _monsters)
                        {
                            if (mon.Hp > 0)
                            {
                                i++;
                            }
                        }

                        if (i < 2)
                        {
                            SetCursorString(lineX, lineY++, "살아있는적이 2마리 이하입니다.", false);
                            Thread.Sleep(1000);
                            return true;
                        }

                        if (num1 != num2)
                        {
                            if (_monsters[num1].Hp > 0 && _monsters[num2].Hp > 0)
                            {
                                Console.Clear();
                                _player.Mp -= (int)skill[1][1];
                                lineY = 2;
                                AttackInfo(_player, _monsters[num1], skill[0][1], ref lineY);
                                lineY++;
                                AttackInfo(_player, _monsters[num2], skill[0][1], ref lineY);
                                lineY++;
                                isSkill = false;
                            }
                        }
                    }

                    foreach (Monster mon in _monsters)
                    {
                        if (_player.Hp > 0)
                        {
                            if (mon.Hp > 0)
                            {
                                AttackInfo(mon, _player, 1, ref lineY);
                                lineY++;
                            }
                        }
                    }

                    if (_player.Hp <= 0 || IsDeadMonsters())
                    {
                        DisplayBattleClear(round);
                    }
                    else
                    {
                        SetCursorString(15, lineY++, "아무키나 입력하여 다음턴으로 넘어갑니다", false);
                        SetCursorString(15, lineY++, ">> ", true);
                        string str = Console.ReadLine();
                    }
                }
                else
                {
                    lineY += 3;
                    SetCursorString(lineX, lineY++, "마나가 부족합니다.", false);
                    Thread.Sleep(1000);
                    return true;
                }
            }
            
            return false;
        }

        public void DisplayAttackSelect(int round, float skillDamage, float skillMp)
        {
            int lineY = BattleInfo(true, round);
            int lineX = 10;
            lineY++;
            DrawStar(60, 30);

            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "0. 취소", false);

            lineY++;

            int isDeadMon = 0;
            foreach (Monster mon in _monsters)
            {
                if (mon.Hp <= 0)
                {
                    isDeadMon = 1;
                }
            }
            int inputNum = InputString(0, _monsters.Length, isDeadMon, "대상을 입력해주세요.",lineX,lineY);

            if (inputNum >= 1 && inputNum <= _monsters.Length)
            {
                Console.Clear();
                lineY = 2;
                AttackInfo(_player, _monsters[inputNum - 1], skillDamage, ref lineY);
                lineY++;
                foreach (Monster mon in _monsters)
                {
                    if(_player.Hp > 0)
                    {
                        if (mon.Hp > 0)
                        {
                            AttackInfo(mon, _player, 1, ref lineY);
                            lineY++;
                        }
                    }
                }
                _player.Mp -= (int)skillMp;

                if (_player.Hp <= 0 || IsDeadMonsters())
                {
                    DisplayBattleClear(round);
                }
                else
                {
                    SetCursorString(15, lineY++, "아무키나 입력하여 다음턴으로 넘어갑니다", false);
                    SetCursorString(15, lineY++, ">> ", true);
                    string str = Console.ReadLine();
                }
            }
        }

        public int BattleInfo(bool isBattle, int round)
        {
            Console.Clear();
            int lineX = 10;
            int lineY = 2;
            Console.ForegroundColor = ConsoleColor.Red;
            if (round == 4)
            {
                SetCursorString(lineX, lineY++, $"Battle!! - 현재 스테이지 {_stage}-보스", false);
            }
            else
            {
                SetCursorString(lineX, lineY++, $"Battle!! - 현재 스테이지 {_stage}-{round}", false);
            }
            lineY++;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "[몬스터 정보]", false);
            int j = 5;
            if (!isBattle)
            {
                foreach (Monster mon in _monsters)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    SetCursorString(lineX, lineY++, $"레벨 | {mon.Level} \t이름 | {mon.Name}", true);
                    Console.SetCursorPosition(lineX+37, j++);
                    if (mon.Hp <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.Write("체력 | ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0}", mon.Hp);
                    }
                }
            }
            else
            {
                int i = 1;
                foreach (Monster mon in _monsters)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    SetCursorString(lineX, lineY++, $"{i++}| 레벨 | {mon.Level} \t이름 | {mon.Name}", true);
                    Console.SetCursorPosition(lineX+37, j++);
                    if (mon.Hp <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.Write("체력 | ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0}", mon.Hp);
                    }
                }
            }
            lineY++;
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, "[내 정보]", false);
            SetCursorString(lineX, lineY++, $"레벨   | {_player.Level}\tChrd | {_player.Chrd}", false);
            SetCursorString(lineX, lineY++, "체력   | ", true);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}/{1}", _player.Hp, _player.MaxHp);
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, $"마나   | ", true);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("{0}/{1}", _player.Mp, _player.MaxMp);
            Console.ForegroundColor = ConsoleColor.White;
            SetCursorString(lineX, lineY++, $"공격력 | {_player.Atk}", false);
            SetCursorString(lineX, lineY++, $"방어력 | {_player.Def}", false);

            return lineY;
        }

        public void AttackInfo(Character aCharacter, Character tCharacter, float skillDamage, ref int lineY)
        {
            Console.WriteLine();
            int lineX = 10;

            Console.ForegroundColor = ConsoleColor.White;
            if (tCharacter.Evasion())
            {
                SetCursorString(lineX, lineY++, "========================================", false);
                SetCursorString(lineX, lineY++, $" {tCharacter.Name}이(가) {aCharacter.Name}의 공격을 회피하였습니다.", false);
                SetCursorString(lineX, lineY++, "========================================", false);
            }
            else
            {
                float damage = tCharacter.TakeDamage(aCharacter.Atk) * skillDamage;
                SetCursorString(lineX, lineY++, "==============================================", false);
                SetCursorString(lineX, lineY++, $" {aCharacter.Name}이(가) {tCharacter.Name}에게 {(int)damage}의 데미지를 입혔습니다", false);
                SetCursorString(lineX, lineY++, "==============================================", false);
                lineY++;

                if (tCharacter.Hp < damage)
                {
                    SetCursorString(lineX, lineY++, $"{tCharacter.Name}의 체력 {tCharacter.Hp} -> 0", false);
                    tCharacter.Hp = 0;
                }
                else
                {
                    SetCursorString(lineX, lineY++, $"{tCharacter.Name}의 체력 {tCharacter.Hp} -> {tCharacter.Hp - (int)damage}", false);
                    tCharacter.Hp -= (int)damage;
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
            DrawStar(60, 25);
            int lineX = 10;
            int lineY = 2;

            SetCursorString(lineX, lineY, "Battle!! - Result",false);
            lineY++;
            Console.ForegroundColor = ConsoleColor.White;
            if (_player.Hp > 0)
            {
                SetCursorString(lineX, lineY, $"던전에서 몬스터 {_monsters.Length}마리를 잡았습니다.", false);
                lineY++;

                int beforeExp = _player.Exp;
                int beforeGold = _player.Gold;
                int exp = _dungeon.GetExp(_monsters);
                int gold = _dungeon.GetGold(_monsters);
                _player.Exp += exp;
                _player.Gold += gold;
                int beforeMaxExp = _player.Level * 5;


                if (_player.Exp >= beforeMaxExp)
                {
                    _player.Exp -= _player.Level * 5;
                    _player.Level += 1;
                    _player.Atk += 1;
                    _player.Def += 1;
                    Console.WriteLine();
                    SetCursorString(lineX, lineY++, "========================", false);
                    SetCursorString(lineX, lineY++, "  ☆★☆ 레벨업 ★☆★", false);
                    SetCursorString(lineX, lineY++, "========================", false);
                }

                lineY++;
                SetCursorString(lineX, lineY++, $"레벨   | {_player.Level}", false);
                lineY++;
                SetCursorString(lineX, lineY++, $"경험치 | {beforeExp} / {beforeMaxExp} -> {_player.Exp} / {_player.Level * 5}\t(+{exp})", false);
                lineY++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                SetCursorString(lineX, lineY++, $"골드   | {beforeGold} -> {_player.Gold} \t(+{gold})", false);
                lineY++;
                Console.ForegroundColor = ConsoleColor.Red;
                SetCursorString(lineX, lineY++, $"체력   | {_beforeHp} / {_player.MaxHp} -> {_player.Hp} / {_player.MaxHp}", false);
                lineY++;
                Console.ForegroundColor = ConsoleColor.Blue;
                SetCursorString(lineX, lineY++, $"마나   | {_beforeMp} / {_player.MaxMp} -> {_player.Mp} / {_player.MaxMp}", false);

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
                Console.ForegroundColor = ConsoleColor.Red;
                SetCursorString(lineX, lineY++, "You Lose", false);
                if (_player.Hp <= 0)
                {
                    bool isSelectEnd = true;
                    while (isSelectEnd)
                    {
                        isSelectEnd = ReStart(lineX, lineY);
                    }
                }
                _stage = 1;
                _round = 1;
            }
            lineY++;
            SetCursorString(lineX, lineY++, "아무키나 입력하면 시작창으로 돌아갑니다", false);
            SetCursorString(lineX, lineY++, ">> ", true);
            Console.ReadLine();

            lineY ++;
            SetCursorString(lineX, lineY++, "=====================", false);
            SetCursorString(lineX, lineY++, "  시작창으로 이동중", false);
            SetCursorString(lineX, lineY++, "=====================", false);
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