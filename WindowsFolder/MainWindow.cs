using EntityEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace WindowEngine
{
    class MainWindow
    {
        public static RenderWindow window = new RenderWindow(new VideoMode(800, 600), "MainWidow");
        public static Clock clock = new Clock();
        private static Time pDeltaTime;
        public static float deltaTime;

        public MainWindow()
        {
            window.SetVerticalSyncEnabled(true);

            window.Closed += _Closed;

            window.Resized += _Resized;
        }

        private void _Resized(object? sender, SizeEventArgs e)
        {
            window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private void _Closed(object? sender, EventArgs e)
        {
            window.Close();
        }

        public void Start()
        {

        }

        public void Update()
        {
            pDeltaTime = clock.Restart();
            deltaTime = pDeltaTime.AsSeconds();

            window.DispatchEvents();

            window.Clear(Color.Black);

            ViewHandler.Update();
        }

        public void Draw()
        {
            ViewHandler.Draw();

        }
    }
}
