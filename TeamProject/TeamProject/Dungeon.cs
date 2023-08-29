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
        private int _round;
        public int Round
        {
            get { return _round; }
            set { _round = value; }
        }
        public MonsterInfo[] monsterInfo = MonsterInfo.GetMonsterDict();
        public Dungeon()
        {
            Stage = 1;                          // 시작 시에는 1 스테이지 부터.
            Round = 1;
        }

        public Dungeon(int stage, int round)
        {
            Stage = stage;
            Round = round;
        }

        public Monster[] MonsterGen()
        {
            Random random = new Random();
            int genMin = ((int)(Stage * 0.5 / 1) > 0) ? (int)(Stage * 0.5 / 1) : 1;
            genMin = (genMin > 3) ? 3 : genMin;                                     // 아무리 높은 스테이지여도 최소값 3으로 보정.
            int genMax = ((int)(Stage * 1) + 1 < 6) ? (int)(Stage * 1) + 1 : 5;
            genMin += random.Next(1, Round+1);
            genMax += random.Next(1, Round);
            int monsterNum = random.Next(genMin, genMax);

            MonsterInfo[] stageMonsterColl = MonsterInfo.GetMonsterDict().Where(mon => mon.StageRank <= Stage && mon.StageRank > 0).ToArray();    // 스테이지별 몬스터 목록 호출.

            Monster[] getMonsterArray = new Monster[monsterNum];

            for (int randIdx = 0; randIdx < monsterNum; randIdx++)
            {
                int getMonsterIdx = random.Next(1, stageMonsterColl.Length);
                int monsterLevel = random.Next(Stage, Stage + 4);
                Monster monster = new Monster(stageMonsterColl[getMonsterIdx].Name, stageMonsterColl[getMonsterIdx].Chrd, monsterLevel);
                getMonsterArray[randIdx] = monster;
            }
            return getMonsterArray;
        }

        public Monster[] BossMonsterGen()
        {
            Random random = new Random();
            int bossMonsterLevel = random.Next(Stage, Stage + 4);
            MonsterInfo bossMonsterInfo = MonsterInfo.GetMonsterDict().Where(mon => mon.StageRank < 0 && mon.StageRank > -Stage - 1).ToArray()[0];
            Monster bossMonster = new Monster(bossMonsterInfo.Name, bossMonsterInfo.Chrd, bossMonsterLevel);
            Monster[] getMonsterArray = new Monster[] { };
            Array.Resize(ref getMonsterArray, getMonsterArray.Length +1);
            getMonsterArray[0] = bossMonster;

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
                    if (monster.Name == monsterInfo.Name)
                    {
                        resultExp += ((int)(monster.Level * monsterInfo.Exp * 0.33) < 1) ? 1 : (int)(monster.Level * monsterInfo.Exp * 0.33);
                    }
                }
            }
            return resultExp;
        }

    }
    /*
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
    */
}
