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
        public static ViewHandler viewHandler = new ViewHandler();
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

            viewHandler.Update();

            //window.SetView(new View(hero.sprite.Position, new Vector2f(96 * (window.Size.X / 192), 54 * (window.Size.Y / 108))));
            //window.SetView(new View(new Vector2f(5000,5200), new Vector2f(16*96 * (window.Size.X / 192), 16*54 * (window.Size.Y / 108))));
            //window.SetView(new View(new Vector2f(0, 0), new Vector2f(10 * 96 * (window.Size.X / 192), 10 * 54 * (window.Size.Y / 108))));
        }

        public void Draw()
        {
            viewHandler.Draw();

        }
    }
}
