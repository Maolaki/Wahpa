using DatabaseEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace WindowEngine
{
    class MainWindow
    {
        private static ContextSettings settings;
        public static Clock clock = new Clock();
        public static RenderWindow window;
        public static Time pDeltaTime { get; private set; }
        public static float deltaTime { get; private set; }

        private static void _Resized(object? sender, SizeEventArgs e)
        {
            window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
            ViewHandler.screenSizeX = e.Width;
            ViewHandler.screenSizeY = e.Height;

            ViewHandler.UpdateGUIElements();
        }

        private static void _Closed(object? sender, EventArgs e)
        {
            window.Close();
        }

        public static void Start()
        {
            settings.AntialiasingLevel = 8;
            window = new RenderWindow(new VideoMode(1920, 1080), "Wahpa", Styles.Fullscreen, settings);

            window.Closed += _Closed;

            window.Resized += _Resized;

            ViewHandler.Start();
        }

        public static void StartChanged()
        {
            settings.AntialiasingLevel = 8;
            window = new RenderWindow(new VideoMode(1920, 1080), "Wahpa", Styles.Fullscreen, settings);

            window.Closed += _Closed;

            window.Resized += _Resized;
        }

        public static void StartChanged(uint sizeX, uint sizeY)
        {
            settings.AntialiasingLevel = 8;
            window = new RenderWindow(new VideoMode(sizeX, sizeY), "Wahpa", Styles.None, settings);

            window.Closed += _Closed;

            window.Resized += _Resized;
        }

        public static void Update()
        {
            pDeltaTime = clock.Restart();
            deltaTime = pDeltaTime.AsSeconds();

            window.DispatchEvents();

            ViewHandler.Update?.Invoke();
        }

        public static void Draw()
        {
            window.Clear(Color.Blue);

            ViewHandler.Draw?.Invoke();
        }
    }
}
