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
        public int Level { get; }
        public int Exp { get; }
        public int Hp { get; set; }
        public int Atk { get; }
        public int Def { get; }
        public int Gold { get; }
        public string Name { get; }
        public string Chrd { get; }
        int _critical;
        public MonsterInfo[] MonsterInfo = TeamProject.MonsterInfo.GetMonsterDict();
        public static Dictionary<string, List<SkillInfoStruct>> bossMonsterInfo = GetBossMonsterInfo();

        public string[][] skills = new string[2][];
        public Monster(string name, string chrd, int level)
        {
            Level = level;
            Name = name;
            Chrd = chrd;
            MonsterInfo[] monsterInfo = MonsterInfo.Where(mon => mon.Name == name).ToArray(); // 데이터마다 고유 키값을 가지고 처리함... private key를 사용하여,,,, 보통은 인덱스를 사용한다. 키 : private key 값 : 이름 이런식으로....
            Atk = (int)(Level * monsterInfo[0].MonAtkCoeff);
            Def = (int)(Level * monsterInfo[0].MonDefCoeff);
            Hp = Level * monsterInfo[0].MonHPCoeff;
            Exp = Level * monsterInfo[0].Exp;
            Gold = Level * monsterInfo[0].DropGold;
            _critical = 20;
        }

        public readonly struct SkillInfoStruct
        {
            public string SkillName { get; }
            public string SkillInfo { get; }
            public int MP { get; }
            public int SkillCoeff { get; }

            public SkillInfoStruct(string skillName, string skillInfo, int mp, int skillCoeff)
            {
                SkillName = skillName;
                SkillInfo = skillInfo;
                MP = mp;
                SkillCoeff = skillCoeff;
            }
        }
        public bool Evasion()
        {
            int rand = new Random().Next(1, 11);
            if (rand == 10)
            {
                return true;
            }
            return false;
        }
        public int TakeDamage(int _atk)
        {
            int er = (int)(_atk * 0.1);
            int rand = new Random().Next(1, 101);
            int damage = new Random().Next(_atk - er, _atk + er);
            int realDef = (Def / 100) * damage;
            if (rand <= _critical)
            {
                return (int)(damage * (1.5)) - realDef;
            }
            else
            {
                if (damage > realDef)
                {
                    return damage - realDef;
                }
                else
                {
                    return 1;
                }
            }
        }

        public static Dictionary<string, List<SkillInfoStruct>> GetBossMonsterInfo()
        {
            var bossMonsterInfo = new Dictionary<string, List<SkillInfoStruct>>();

            string fullPath = Pathes.BossSkillDataPath();
            string[] bossSkillData = File.ReadAllLines(fullPath, Encoding.UTF8);
            string[] propertyNames = bossSkillData[0].Split(',');
            string[] bossSkills = bossSkillData.Skip(1).ToArray();

            for (int bossIdx = 0; bossIdx < bossSkills.Length; bossIdx++)
            {
                string[] skillData = bossSkills[bossIdx].Split(",");

                string name = skillData[Array.IndexOf(propertyNames, "Name")];
                string skillName = skillData[Array.IndexOf(propertyNames, "SkillName")];
                string skillInfo = skillData[Array.IndexOf(propertyNames, "SkillInfo")];
                int mp = int.Parse(skillData[Array.IndexOf(propertyNames, "MP")]);
                int skillCoeff = int.Parse(skillData[Array.IndexOf(propertyNames, "SkillCoeff")]);


                var skillStruct = new SkillInfoStruct(skillName, skillInfo, mp, skillCoeff);

                if (bossMonsterInfo.ContainsKey(name))
                {
                    bossMonsterInfo[name].Add(skillStruct);
                }
                else
                {
                    bossMonsterInfo[name] = new List<SkillInfoStruct> { skillStruct };
                }
            }
            return bossMonsterInfo;
        }
        public string[][] SkillInfo()
        {
            var skillList = bossMonsterInfo[Name];
            var skillNamesList = new List<string>();
            var skillInfoList = new List<string>();
            var skillCoeffList = new List<string>();
            foreach (var singleStruct in skillList)
            {
                skillInfoList.Add(singleStruct.SkillInfo);
                skillCoeffList.Add((singleStruct.SkillCoeff).ToString());
            }

            string[] skillSpecific = skillInfoList.Zip(skillCoeffList, (skillInfo, coeff) => $"{Name}이(가) {skillInfo} {coeff}배만큼의 피해를 입힙니다.").ToArray();
 
            skills[0] = skillCoeffList.ToArray();
            skills[1] = skillSpecific.ToArray();

            return skills;
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
        public int StageRank { get; set; }
        public int DropGold { get; set; }
        public MonsterInfo(string name, string chrd, float monAtkCoeff, float monDefCoeff, int monHPCoeff, int exp, int dropGold, int stageRank)
        {
            Name = name;
            Chrd = chrd;
            MonAtkCoeff = monAtkCoeff;
            MonDefCoeff = monDefCoeff;
            MonHPCoeff = monHPCoeff;
            Exp = exp;
            DropGold = dropGold;
            StageRank = stageRank;

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
                int dropGold = int.Parse(monsterEach[Array.IndexOf(propertyNames, "DropGold")]); // 골드 계수
                int stageRank = int.Parse(monsterEach[Array.IndexOf(propertyNames, "StageRank")]); // 몬스터 스테이지 조정

                MonsterInfo monsterDict = new MonsterInfo(name, chrd, monatkcoeff, mondefcoeff, monHPcoeff, exp, dropGold, stageRank);

                Array.Resize(ref AllMonsters, AllMonsters.Length + 1);
                AllMonsters[monIdx - 1] = monsterDict;
            }

            return AllMonsters;
        }
    }
}
