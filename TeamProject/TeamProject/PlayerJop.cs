using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Warrior : Player
    {
        public Warrior(string name) : base(name, "전사", 25, 10, 150, 40, 15) { }
        public override float[][] SkillInfo(Scene scene, int lineX, int lineY)
        {
            scene.SetCursorString(lineX, lineY++, "1. 알파 스트라이크 - 마나 15", false);
            scene.SetCursorString(lineX, lineY++, "   공격력 * 2 로 하나의 적을 공격합니다.", false);
            scene.SetCursorString(lineX, lineY++, "2. 더블 스트라이크 - 마나 20", false);
            scene.SetCursorString(lineX, lineY++, "   공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", false);

            skills[0] = new float[] { 2f,  1.5f};
            skills[1] = new float[] { 15f, 20f };
            
            return skills;
        }
    }
    internal class Prist : Player
    {
        public Prist(string name) : base(name, "사제", 20, 5, 100, 80, 20) { }
        public override float[][] SkillInfo(Scene scene, int lineX, int lineY)
        {
            scene.SetCursorString(lineX, lineY++, "1. 홀리 엔젤 - 마나 20", false);
            scene.SetCursorString(lineX, lineY++, "   공격력 * 3 로 하나의 적을 공격합니다.", false);
            scene.SetCursorString(lineX, lineY++, "2. 샤이닝 로우 - 마나 30", false);
            scene.SetCursorString(lineX, lineY++, "   공격력 * 3 로 2명의 적을 랜덤으로 공격합니다.", false);

            skills[0] = new float[] { 3f, 3f };
            skills[1] = new float[] { 20f, 30f };

            return skills;
        }
    }
    internal class Archer : Player
    {
        public Archer(string name) : base(name, "궁수", 30, 3, 100, 50, 40) { }
        public override float[][] SkillInfo(Scene scene, int lineX, int lineY)
        {
            scene.SetCursorString(lineX, lineY++, "1. 천공의 바람 - 마나 25", false);
            scene.SetCursorString(lineX, lineY++, "   공격력 * 4 로 하나의 적을 공격합니다.", false);
            scene.SetCursorString(lineX, lineY++, "2. 섬멸 - 마나 30", false);
            scene.SetCursorString(lineX, lineY++, "   공격력 * 3 로 2명의 적을 랜덤으로 공격합니다.", false);

            skills[0] = new float[] {4f, 3f };
            skills[1] = new float[] { 25f, 30f };

            return skills;
        }
    }
    
}
