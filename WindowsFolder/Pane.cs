using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowEngine;

namespace WindowEngine
{
    internal class Pane : GUIElement
    {
        public List<GUIElement> nodes = new List<GUIElement>();

        // Конструктор, который принимает высоту и ширину окна
        public Pane(float size, float coordX, float coordY, Texture texture, int location) : base (size, coordX, coordY, texture, location)
        {
            base.Resize();
            base.Relocate();
        }

        public override void Relocate()
        {
            foreach (GUIElement element in nodes)
            {
                element.Relocate();
            }

            base.Relocate();
        }

        public override void Resize()
        {
            Vector2u windowSize = MainWindow.window.Size;

            float scaleX = ((float)windowSize.X / 100 * Size) / this.Sprite.Texture.Size.X;
            float scaleY = ((float)windowSize.Y / 100 * Size) / this.Sprite.Texture.Size.Y;

            float minScale = (scaleX < scaleY) ? scaleX : scaleY;

            this.Sprite.Scale = new Vector2f(minScale, minScale);

            foreach (GUIElement element in nodes)
            {
                float scaleXE = ((float)windowSize.X / 100 * Size) / element.Sprite.Texture.Size.X;
                float scaleYE = ((float)windowSize.Y / 100 * Size) / element.Sprite.Texture.Size.Y;

                float minScaleE = (scaleX < scaleY) ? scaleX : scaleY;

                element.Sprite.Scale = new Vector2f(minScaleE, minScaleE);
            }
        }

        public override void Draw()
        {
            foreach (GUIElement element in nodes)
            {
                element.Draw();
            }

            base.Draw();
        }
    }
}
