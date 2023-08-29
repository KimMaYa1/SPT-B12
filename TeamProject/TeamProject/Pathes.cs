using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
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

        public static string ItemDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"ItemData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }
    }
}
