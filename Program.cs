using WindowEngine;

class Program
{
    static void Main(string[] args)
    {
        MainWindow mainWindow = new WindowEngine.MainWindow();

        while (mainWindow.window.IsOpen)
        {
            mainWindow.Update();

            mainWindow.Draw();
            mainWindow.window.Display();
        }

        return;
    }

}

