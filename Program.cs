﻿using WindowEngine;

class Program
{
    static void Main(string[] args)
    {
        MainWindow mainWindow = new WindowEngine.MainWindow();

        while (WindowEngine.MainWindow.window.IsOpen)
        {
            mainWindow.Update();

            mainWindow.Draw();
            WindowEngine.MainWindow.window.Display();
        }

        return;
    }

}

