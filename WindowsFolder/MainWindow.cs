using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace WindowEngine
{
    class MainWindow
    {
        public RenderWindow window;
        public View view;
        Event lookEvent;
        CircleShape circle;
        CircleShape circle2;

        public MainWindow()
        {
            window = new RenderWindow(new VideoMode(800, 600), "MainWidow");
            view = new View(new Vector2f(0, 0), new Vector2f(0, 0));

            window.SetVerticalSyncEnabled(true);

            window.Closed += _Closed;

            window.Resized += _Resized;

            ////////////////////////////////////////

            circle = new CircleShape(20);
            circle2 = new CircleShape(28);
            circle.Position = new Vector2f(30, 30);
            circle.FillColor = Color.Yellow;
            circle2.Position = new Vector2f(10, 30);
            circle2.FillColor = Color.Red;

            MapGenerator.CreateMainLayer(File.ReadAllLines("..\\..\\..\\Maps\\Hub.txt"));
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
            window.DispatchEvents();

            window.Clear(Color.Black);

            for (int x = 0; x < MapGenerator.mainLayer.GetLength(0); x++)
            {
                for (int y = 0; y < MapGenerator.mainLayer.GetLength(1); y++)
                {
                    window.Draw(MapGenerator.mainLayer[x,y].sprite);
                }
            }

            circle.Position += new Vector2f(0.5f, 0);

            circle2.Position += new Vector2f(0.8f, 0);
            window.Draw(circle);
            window.Draw(circle2);



           //window.SetView(new View(circle.Position, new Vector2f(96 * (window.Size.X / 192), 54 * (window.Size.Y / 108))));
        }

        public void Draw(RenderTarget target)
        {

        }
    }
}
