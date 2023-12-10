using SFML.Graphics;
using SFML.Window;
using WindowEngine;

class Program
{
    static void Main(string[] args)
    {
        MainWindow.Start();

      //  MainWindow.window.SetActive(false);

        while (MainWindow.window.IsOpen)
        {
            MainWindow.Update();
            Render(MainWindow.window);
        }
    }

    static void Render(RenderWindow window)
    {
      //  window.SetActive(true);

        MainWindow.Draw();

        window.Display();
    }

}

