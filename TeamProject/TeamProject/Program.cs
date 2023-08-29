namespace TeamProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene();
            bool isSelectNameChrd = true;
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