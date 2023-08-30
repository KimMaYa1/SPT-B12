using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    public class Pathes
    {
        public static DirectoryInfo currentPath = new DirectoryInfo(Directory.GetCurrentDirectory());
        public static string localPath = currentPath.Parent.Parent.Parent.Parent.ToString();
        //public static string localPath = Directory.GetParent(Path.GetFullPath(@"..\Data\MonsterData.csv")).Parent.Parent.Parent.Parent.ToString();
        public static string MonsterDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"MonsterData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }

        public static string ItemDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"ItemData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }

        public static string BossSkillDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"BossSkill.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }

    }
}
