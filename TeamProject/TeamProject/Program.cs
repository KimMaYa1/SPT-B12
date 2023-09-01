namespace TeamProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene();
            bool isSelectNameChrd = false;
            Console.SetWindowSize(161, 41);
            if (DataSave.HowChoose(scene))
            {
                scene._player = DataSave.LoadPlayer();
                scene._stage = scene._player.Stage;
                scene._round = scene._player.Round;
            }
            else
            {
                isSelectNameChrd = true;
            }

            while (isSelectNameChrd)
            {
                isSelectNameChrd = scene.DisplaySelectName();
            }

            while (true)
            {
                scene.DisplayStart();
                
            }
        }
    }
}