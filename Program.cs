using WindowEngine;

class Program
{
    static void Main(string[] args)
    {
        MainWindow mainWindow = new MainWindow();

        mainWindow.Start();

        while (MainWindow.window.IsOpen)
        {
            mainWindow.Update();

            mainWindow.Draw();

            MainWindow.window.Display();
        }

        return;
    }

}

