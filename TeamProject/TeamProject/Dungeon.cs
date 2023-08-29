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
        private int _stage;
        public int Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }
        public MonsterInfo[] monsterInfo = MonsterInfo.GetMonsterDict();
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
            int genMin = ((int)(Stage * 0.5 / 1) > 0) ? (int)(Stage * 0.5 / 1) : 1;
            int genMax = ((int)(Stage * 1) < 6) ? (int)(Stage * 1) : 5;
            int monsterNum = random.Next(genMin, genMax);

            //MonsterDict[] monsterDict = MonsterDict.GetMonsterDict();    // 전체 몬스터 목록 호출.

            Monster[] getMonsterArray = new Monster[monsterNum];

            for (int randIdx = 0; randIdx < monsterNum; randIdx++)
            {
                int getMonsterIdx = random.Next(1, monsterInfo.Length);
                int monsterLevel = random.Next(Stage, Stage + 4);
                Monster monster = new Monster(monsterInfo[getMonsterIdx].Name, monsterInfo[getMonsterIdx].Chrd, monsterLevel);
                getMonsterArray[randIdx] = monster;
            }


            return getMonsterArray;
        }

        public int GetExp(Monster[] getMonsterArray)
        {
            int resultExp = 0;
            //MonsterDict[] monsterDict = MonsterDict.GetMonsterDict();
            foreach (MonsterInfo monsterInfo in  monsterInfo) 
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

    public static class Pathes
    {
        public static string localPath = Directory.GetParent(Path.GetFullPath(@"..\Data\MonsterData.csv")).Parent.Parent.Parent.Parent.ToString();
        public static string MonsterDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"MonsterData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }
    }
}
