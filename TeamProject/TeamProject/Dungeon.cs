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
            Monster[] MonsterArray = new Monster[] { };

            string fullPath = Pathes.NowPath();
            string[] monsterData = File.ReadAllLines(fullPath, Encoding.UTF8);
            string[] propertyNames = monsterData[0].Split(',');           // Gen 에 포함할 몬스터 속성

            for (int monIdx = 1; monIdx < monsterData.Length; monIdx++) 
            {
                string[] monsterEach = monsterData[monIdx].Split(',');
                int monsterLevel = random.Next(stage, stage + 3);

                string name = monsterEach[Array.IndexOf(propertyNames, "Name")];
                string chrd = monsterEach[Array.IndexOf(propertyNames, "Chrd")];
                int monatkcoeff = int.Parse(monsterEach[Array.IndexOf(propertyNames, "MonAtkCoeff")]); // 공격계수
                int mondefcoeff = int.Parse(monsterEach[Array.IndexOf(propertyNames, "MonDefCoeff")]); // 방어계수

                MonsterDict monsterDict = new MonsterDict(name, chrd, monatkcoeff, mondefcoeff);
                Monster monster = new Monster(name, chrd, monsterLevel); 

                Array.Resize(ref MonsterArray, MonsterArray.Length + 1);
                MonsterArray[monIdx] = monster;
            }

            Monster[] getMonsterArray = new Monster[monsterNum];

            for (int randIdx = 0; randIdx < monsterNum; randIdx++)
            {
                int getMonsterIdx = random.Next(1, monsterData.Length);
                getMonsterArray[randIdx] = MonsterArray[getMonsterIdx];
            }


            return getMonsterArray;
        }

    }

    public class MonsterDict
    {
        public string Name { get; set; }
        public string Chrd { get; set; }
        public int MonAtkCoeff { get; set; }
        public int MonDefCoeff { get; set; }

        public MonsterDict(string name, string chrd, int monAtkCoeff, int monDefCoeff)
        {
            Name = name;
            Chrd = chrd;
            MonAtkCoeff = monAtkCoeff;
            MonDefCoeff = monDefCoeff;
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
