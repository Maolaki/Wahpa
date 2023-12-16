using DatabaseEngine;
using SFML.Graphics;
using SFML.Window;
using WindowEngine;

class Program
{
    static void Main(string[] args)
    {
        MainWindow.Start();

        while (MainWindow.window.IsOpen)
        {
            MainWindow.Update();
            Render(MainWindow.window);
        }
    }

    static void Render(RenderWindow window)
    {

        MainWindow.Draw();

        window.Display();
    }

}

