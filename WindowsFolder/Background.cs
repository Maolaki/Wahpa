using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowEngine;

namespace Wahpa.WindowsFolder
{
    internal class Background : GUIElement
    {
        public Background(float size, float coordX, float coordY, Texture texture, int location = 3) : base(size, coordX, coordY, texture, location)
        {
            base.Resize();
            base.Relocate();
        }

        public override void Resize()
        {
            Vector2u windowSize = MainWindow.window.Size;

            float scaleX = ((float)windowSize.X / 100 * Size) / this.Sprite.Texture.Size.X;
            float scaleY = ((float)windowSize.Y / 100 * Size) / this.Sprite.Texture.Size.Y;

            float maxScale = (scaleX > scaleY) ? scaleX : scaleY;

            this.Sprite.Scale = new Vector2f(maxScale, maxScale);
        }

        public override void Relocate()
        {
            Vector2u windowSize = MainWindow.window.Size;
            View view = MainWindow.window.GetView();
            Vector2f viewCenter = view.Center;
            Vector2f viewSize = view.Size;

            float coordX = (float)windowSize.X / 2 - this.Sprite.Texture.Size.X * this.Sprite.Scale.X / 2 + (float)windowSize.X / 100 * CoordinateX;
            float coordY = (float)windowSize.Y / 100 * CoordinateY;

            Vector2f topLeft = viewCenter - viewSize / 2f;

            this.Sprite.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
        }
    }
}
