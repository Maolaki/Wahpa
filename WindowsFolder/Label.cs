using SFML.Graphics;
using SFML.System;

namespace WindowEngine
{
    internal class Label : GUIElement
    {
        public Text text { get; set; }
        public float coordinateX { get; set; }
        public float coordinateY { get; set; }
        public float size { get; set; }
        public int location { get; set; }

        public Label(float size, float coordX, float coordY,int location, string text, Font font, uint charSize, Color color) : base(size, coordX, coordY, Data.GUIDict["Pane1"], location)
        {
            this.text = new Text
            {
                Font = font,
                DisplayedString = text,
                CharacterSize = charSize,
                FillColor = color
            };

            this.size = size;
            this.coordinateX = coordX;
            this.coordinateY = coordY;
            this.location = location;
        }
        public override void Resize()
        {
            Vector2u windowSize = MainWindow.window.Size;

            float scaleX = ((float)windowSize.X / 100 * size) / text.GetLocalBounds().Width;
            float scaleY = ((float)windowSize.Y / 100 * size) / text.GetLocalBounds().Height;

            float minScale = (scaleX < scaleY) ? scaleX : scaleY;

            text.Scale = new Vector2f(minScale, minScale);
        }

        public override void Relocate()
        {
            Vector2u windowSize = MainWindow.window.Size;
            View view = MainWindow.window.GetView();
            Vector2f viewCenter = view.Center;
            Vector2f viewSize = view.Size;

            switch (location)
            {
                case 1:
                    float coordX = (float)windowSize.X / 100 * coordinateX;
                    float coordY = (float)windowSize.Y / 100 * coordinateY;

                    Vector2f topLeft = viewCenter - viewSize / 2f;

                    this.text.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
                    break;
                case 2:
                    coordX = (float)windowSize.X / 100 * coordinateX;
                    coordY = (float)windowSize.Y / 100 * coordinateY;

                    Vector2f topRight = viewCenter + viewSize / 2f;

                    this.text.Position = new Vector2f(topRight.X - coordX, topRight.Y - coordY);
                    break;
                default:
                    coordX = (float)windowSize.X / 2 - text.GetLocalBounds().Width / 2 + (float)windowSize.X / 100 * coordinateX;
                    coordY = (float)windowSize.Y / 100 * coordinateY;

                    topLeft = viewCenter - viewSize / 2f;

                    this.text.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
                    break;
            }
        }

        public override void Draw()
        {
            MainWindow.window.Draw(this.text);
        }
    }
}
