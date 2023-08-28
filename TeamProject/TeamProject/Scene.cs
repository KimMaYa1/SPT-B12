using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject
{
    internal class Scene
    {
        Player player;
        Monster[] monster;
        public Scene()
        {
            new Player("조범준", "전사", 0, 0, 0);
        }

        public void DisplayStart()
        {

        }

        public void DisplayBattle()
        {
            RanMonster();
            MonsterInfo();
            PlayerInfo();
        }

        public void DisplayStat()
        {

        }

        public void DisplayInventory()
        {
            ItemList();
        }

        public void DisplayClear()
        {

        }

        public void ItemList()
        {

        }

        public void RanMonster()
        {

        }

        public void MonsterInfo(Monster monster)
        {

        }

        public void PlayerInfo(Player player)
        {

        }
    }
}
