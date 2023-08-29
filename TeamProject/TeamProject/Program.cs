namespace TeamProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene();

            bool isNext = true;
            while (isNext)
            {
                isNext = scene.DisplaySelectName();
            }
            while (true)
            {
                scene.DisplayStart();
            }
        }
    }
}