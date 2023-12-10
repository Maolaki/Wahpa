using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace WindowEngine
{
    internal abstract class GUIElement
    {
        public enum LocationEnum
        {
            TopLeft = 1,
            TopRight = 2,
            Center = 3
        }

        // Использовал автоматические свойства с защищенными сеттерами, чтобы предотвратить нежелательные изменения извне
        public Sprite Sprite { get; set; }
        public Text Text { get; set; }
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public float Size { get; set; }
        public int Location { get; set; }

        public GUIElement(float size, float coordX, float coordY, Texture texture, int location)
        {
            this.Sprite = new Sprite(texture); // Упростил создание спрайта
            this.Size = size;
            this.CoordinateX = coordX;
            this.CoordinateY = coordY;
            this.Location = location;
        }

        public virtual void Resize()
        {
            Vector2u windowSize = MainWindow.window.Size;

            // Использовал Math.Min для нахождения минимального масштаба
            float minScale = Math.Min((float)windowSize.X / 100 * Size / Sprite.Texture.Size.X, (float)windowSize.Y / 100 * Size / Sprite.Texture.Size.Y);

            Sprite.Scale = new Vector2f(minScale, minScale);

            if (Text != null && Sprite != null)
            {
                float maxLetterHeight = Text.Font.GetLineSpacing(Text.CharacterSize);

                minScale = Math.Min(Sprite.GetGlobalBounds().Height * 0.7f / maxLetterHeight, Sprite.GetGlobalBounds().Width * 0.7f / Text.GetLocalBounds().Width);

                Text.Scale = new Vector2f(minScale, minScale);
            }
        }

        public virtual void Relocate()
        {
            Vector2u windowSize = MainWindow.window.Size;
            View view = MainWindow.window.GetView();
            Vector2f viewCenter = view.Center;
            Vector2f viewSize = view.Size;

            // Использовал enum для обозначения положения элемента
            switch ((LocationEnum)Location)
            {
                case LocationEnum.TopLeft:
                    float coordX = (float)windowSize.X / 100 * CoordinateX;
                    float coordY = (float)windowSize.Y / 100 * CoordinateY;

                    Vector2f topLeft = viewCenter - viewSize / 2f;

                    Sprite.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
                    break;
                case LocationEnum.TopRight:
                    coordX = (float)windowSize.X / 100 * CoordinateX;
                    coordY = (float)windowSize.Y / 100 * CoordinateY;

                    Vector2f topRight = viewCenter + viewSize / 2f;

                    Sprite.Position = new Vector2f(topRight.X - coordX, topRight.Y - coordY);
                    break;
                case LocationEnum.Center:
                    coordX = (float)windowSize.X / 2 - Sprite.Texture.Size.X * Sprite.Scale.X / 2 + (float)windowSize.X / 100 * CoordinateX;
                    coordY = (float)windowSize.Y / 100 * CoordinateY;

                    topLeft = viewCenter - viewSize / 2f;

                    Sprite.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
                    break;
            }

            if (Text != null)
            {
                float maxLetterHeight = Text.Font.GetLineSpacing(Text.CharacterSize);

                float offsetX = Sprite.GetGlobalBounds().Width / 2 - Text.GetLocalBounds().Width * Text.Scale.X / 2;
                float offsetY = (Sprite.GetGlobalBounds().Height - maxLetterHeight * Text.Scale.Y) / 2;

                Text.Position = new Vector2f(Sprite.Position.X + offsetX, Sprite.Position.Y + offsetY);
            }

        }

        public virtual void Draw()
        {
            MainWindow.window.Draw(Sprite);

            if (Text != null)
                MainWindow.window.Draw(Text);
        }

        public void SetPosition(float coordX, float coordY)
        {
            this.CoordinateX = coordX;
            this.CoordinateY = coordY;
        }

        public void SetText(string text, Font font, uint charSize, Color color)
        {
            this.Text = new Text
            {
                Font = font,
                DisplayedString = text,
                CharacterSize = charSize,
                FillColor = color
            };
        }
    }
}
