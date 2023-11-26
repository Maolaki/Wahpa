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
        public static Time pDeltaTime { get; private set; }
        public static float deltaTime { get; private set; }

        public MainWindow()
        {
            window.SetVerticalSyncEnabled(true);

            window.Closed += _Closed;

            window.Resized += _Resized;

            Start();
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
            ViewHandler.TestStart();
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
