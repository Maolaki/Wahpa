using EntityEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace WindowEngine
{
    class MainWindow
    {
        public RenderWindow window;
        public static Clock clock = new Clock();
        private static Time pDeltaTime;
        public static float deltaTime;

        private View view;
        private Hero hero;

        public MainWindow()
        {
            window = new RenderWindow(new VideoMode(800, 600), "MainWidow");
            view = new View(new Vector2f(0, 0), new Vector2f(0, 0));

            window.SetVerticalSyncEnabled(true);

            window.Closed += _Closed;

            window.Resized += _Resized;

            ////////////////////////////////////////

            hero = new Hero(150, 120);

            MapGen.MapGenerator.LoadHub(File.ReadAllLines("..\\..\\..\\Maps\\Hub.txt"));
           // MapGen.MapGenerator.LoadLevel1();
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

           hero.Update();

           window.SetView(new View(hero.sprite.Position, new Vector2f(96 * (window.Size.X / 192), 54 * (window.Size.Y / 108))));
        }

        public void Draw()
        {
            for (int x = 0; x < MapGen.MapGenerator.mainLayer.GetLength(0); x++)
            {
                for (int y = 0; y < MapGen.MapGenerator.mainLayer.GetLength(1); y++)
                {
                    window.Draw(MapGen.MapGenerator.mainLayer[x, y].sprite);
                }
            }

            window.Draw(hero.sprite);
        }
    }
}
