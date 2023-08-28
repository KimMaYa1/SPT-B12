using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace TeamProject
{
    internal class Dungeon
    {
        //private int stage { get;  }
        private int stage;
        public int Stage
        {
            get { return stage; }
            set { stage = value; }
        }
        
        public Dungeon()
        {
            Stage = 1;                          // 시작 시에는 1 스테이지 부터.
        }

        public Dungeon(int stage)
        {
            Stage = stage;
        }

        public Monster[] MonsterGen()
        {
            Random random = new Random();
            int genMin = ((int)(stage * 0.5 / 1) > 0) ? (int)(stage * 0.5 / 1) : 1;
            int genMax = ((int)(stage * 1) < 6) ? (int)(stage * 1) : 5;
            int monsterNum = random.Next(genMin, genMax);

            MonsterDict[] monsterDict = MonsterDict.GetMonsterDict();    // 전체 몬스터 목록 호출.

            Monster[] getMonsterArray = new Monster[monsterNum];

            for (int randIdx = 0; randIdx < monsterNum; randIdx++)
            {
                int getMonsterIdx = random.Next(1, monsterDict.Length);
                int monsterLevel = random.Next(stage, stage + 4);
                Monster monster = new Monster(monsterDict[getMonsterIdx].Name, monsterDict[getMonsterIdx].Chrd, monsterLevel);
                getMonsterArray[randIdx] = monster;
            }


            return getMonsterArray;
        }

        public int GetExp(Monster[] getMonsterArray)
        {
            int resultExp = 0;
            MonsterDict[] monsterDict = MonsterDict.GetMonsterDict();
            foreach (MonsterDict monsterInfo in  monsterDict) 
            {
                foreach (Monster monster in getMonsterArray)
                {
                    if (monster.name == monsterInfo.Name)
                    {
                        resultExp += ((int)(monster.level * monsterInfo.Exp * 0.33) < 1) ? 1 : (int)(monster.level * monsterInfo.Exp * 0.33);
                    }
                }
            }
            return resultExp;
        }

    }

    public class MonsterDict
    {
        public string Name { get; set; }
        public string Chrd { get; set; }
        public int MonAtkCoeff { get; set; }
        public int MonDefCoeff { get; set; }
        public int Exp { get; set; }

        public MonsterDict(string name, string chrd, int monAtkCoeff, int monDefCoeff, int exp)
        {
            Name = name;
            Chrd = chrd;
            MonAtkCoeff = monAtkCoeff;
            MonDefCoeff = monDefCoeff;
            Exp = exp;
        }

        public static MonsterDict[] GetMonsterDict()
        {
            MonsterDict[] AllMonsters = new MonsterDict[] { };



            string fullPath = Pathes.NowPath();
            string[] monsterData = File.ReadAllLines(fullPath, Encoding.UTF8);
            string[] propertyNames = monsterData[0].Split(',');           // Gen 에 포함할 몬스터 속성

            for (int monIdx = 1; monIdx < monsterData.Length; monIdx++)
            {
                string[] monsterEach = monsterData[monIdx].Split(',');

                string name = monsterEach[Array.IndexOf(propertyNames, "Name")];
                string chrd = monsterEach[Array.IndexOf(propertyNames, "Chrd")];
                int monatkcoeff = int.Parse(monsterEach[Array.IndexOf(propertyNames, "MonAtkCoeff")]); // 공격계수
                int mondefcoeff = int.Parse(monsterEach[Array.IndexOf(propertyNames, "MonDefCoeff")]); // 방어계수
                int exp = int.Parse(monsterEach[Array.IndexOf(propertyNames, "Exp")]); // 경험치

                MonsterDict monsterDict = new MonsterDict(name, chrd, monatkcoeff, mondefcoeff, exp);

                Array.Resize(ref AllMonsters, AllMonsters.Length + 1);
                AllMonsters[monIdx] = monsterDict;
            }

            return AllMonsters;
        }
    }

    public static class Pathes
    {
        public static string NowPath()
        {
            var localPath = Directory.GetParent(Path.GetFullPath(@"..\Data\MonsterData.csv")).Parent.Parent.Parent.ToString();
            var dataPath = @"Data";
            var fileName = @"MonsterData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }
    }
}
