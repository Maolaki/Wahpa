using WindowEngine;

class Program
{
    static void Main(string[] args)
    {
        SettingFolder.LoadData();
        WindowEngine.MainWindow mainWindow = new WindowEngine.MainWindow();

        while (mainWindow.window.IsOpen)
        {
            mainWindow.Update();

            mainWindow.Draw(mainWindow.window);
            mainWindow.window.Display();
        }

        return;
    }

}

