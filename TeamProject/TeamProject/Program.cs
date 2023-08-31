namespace TeamProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene();
            bool isSelectNameChrd = true;

            Console.SetWindowSize(161, 41);

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