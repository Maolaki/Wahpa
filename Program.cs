

using WindowEngine;

class Program
{
    static void Main(string[] args)
    {
        WindowEngine.MainWindow mainWindow = new WindowEngine.MainWindow();
        SettingFolder.LoadData();

        while (mainWindow.window.IsOpen)
        {
            mainWindow.Update();

            mainWindow.Draw(mainWindow.window);
            mainWindow.window.Display();
        }

        return;
    }

}

