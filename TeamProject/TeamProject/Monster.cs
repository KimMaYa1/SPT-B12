using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Monster : Character
    {
        public int level { get; }
        public int exp { get; }
        public int hp { get; set; }
        public int atk { get; }
        public int def { get; }
        public string name { get; }
        public string chrd { get; }
        int critical;
        public MonsterInfo[] MonsterInfo = TeamProject.MonsterInfo.GetMonsterDict();
        public Monster(string _name, string _chrd, int _level)
        {
            level = _level;
            name = _name;
            chrd = _chrd;
            MonsterInfo[] monsterInfo = MonsterInfo.Where(mon => mon.Name == _name).ToArray(); // 데이터마다 고유 키값을 가지고 처리함... private key를 사용하여,,,, 보통은 인덱스를 사용한다. 키 : private key 값 : 이름 이런식으로....
            atk = (int)(level * monsterInfo[0].MonAtkCoeff);
            def = (int)(level * monsterInfo[0].MonDefCoeff);
            hp = level * monsterInfo[0].MonHPCoeff;
            exp = level * monsterInfo[0].Exp;
            critical = 20;
        } 
        public int TakeDamage(int _atk)
        {
            int er = (int)(_atk * 0.1);
            int rand = new Random().Next(1, 101);
            int damage = new Random().Next(_atk - er, _atk + er);
            if (rand <= critical)
            {
                return (int)(damage * 1.5);
            }
            else
            {
                if (damage > def)
                {
                    return damage - def;
                }
                else
                {
                    return 1;
                }
            }
        }
    }

    public class MonsterInfo
    {
        public string Name { get; set; }
        public string Chrd { get; set; }
        public float MonAtkCoeff { get; set; }
        public float MonDefCoeff { get; set; }
        public int MonHPCoeff { get; set; }
        public int Exp { get; set; }

        public MonsterInfo(string name, string chrd, float monAtkCoeff, float monDefCoeff, int monHPCoeff, int exp)
        {
            Name = name;
            Chrd = chrd;
            MonAtkCoeff = monAtkCoeff;
            MonDefCoeff = monDefCoeff;
            MonHPCoeff = monHPCoeff;
            Exp = exp;
        }

        public static MonsterInfo[] GetMonsterDict()
        {
            MonsterInfo[] AllMonsters = new MonsterInfo[] { };

            string fullPath = Pathes.MonsterDataPath();
            string[] monsterData = File.ReadAllLines(fullPath, Encoding.UTF8);
            string[] propertyNames = monsterData[0].Split(',');           // Gen 에 포함할 몬스터 속성

            for (int monIdx = 1; monIdx < monsterData.Length; monIdx++)
            {
                string[] monsterEach = monsterData[monIdx].Split(',');

                string name = monsterEach[Array.IndexOf(propertyNames, "Name")];
                string chrd = monsterEach[Array.IndexOf(propertyNames, "Chrd")];
                float monatkcoeff = float.Parse(monsterEach[Array.IndexOf(propertyNames, "MonAtkCoeff")]); // 공격계수
                float mondefcoeff = float.Parse(monsterEach[Array.IndexOf(propertyNames, "MonDefCoeff")]); // 방어계수
                int monHPcoeff = int.Parse(monsterEach[Array.IndexOf(propertyNames, "MonHPCoeff")]); // HP 계수
                int exp = int.Parse(monsterEach[Array.IndexOf(propertyNames, "Exp")]); // 경험치

                MonsterInfo monsterDict = new MonsterInfo(name, chrd, monatkcoeff, mondefcoeff, monHPcoeff, exp);

                Array.Resize(ref AllMonsters, AllMonsters.Length + 1);
                AllMonsters[monIdx - 1] = monsterDict;
            }

            return AllMonsters;
        }

    }
}
