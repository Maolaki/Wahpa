using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFML.Window.Mouse;

namespace WindowEngine
{
    internal class Button : GUIElement, IUpdate
    {
        private static DateTime lastClickTime;
        float timeSinceLastScale = 0;
        float scale = 1;
        float scaleChanger = 0.02f;
        bool scaling = false;

        public delegate void doFunc();
        
        public Button(float size, float coordX, float coordY, Texture texture, int location) : base(size, coordX, coordY, texture, location)
        {
            base.Resize();
            base.Relocate();
        }

        public doFunc DoClickFunc;

        public override void Resize()
        {
            if (!scaling)
                base.Resize(); 
        }

        public void Update()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(MainWindow.window);
                Vector2f worldPos = MainWindow.window.MapPixelToCoords(mousePos);

                if (this.Sprite.GetGlobalBounds().Contains(worldPos.X, worldPos.Y))
                {
                    DateTime currentTime = DateTime.Now;

                    if ((currentTime - lastClickTime).TotalSeconds >= 0.3)
                    {
                        lastClickTime = currentTime;

                        DoClickFunc();
                    }
                }
            }
            else
            {
                Vector2i mousePosition = Mouse.GetPosition(MainWindow.window);
                Vector2f worldPosition = MainWindow.window.MapPixelToCoords(mousePosition);
                Transform transform = this.Sprite.Transform;
                FloatRect localBounds = (FloatRect)this.Sprite.TextureRect;
                FloatRect globalBounds = transform.TransformRect(localBounds);

                if (globalBounds.Contains(worldPosition.X, worldPosition.Y))
                {
                    scaling = true;
                    timeSinceLastScale += MainWindow.deltaTime;

                    if (timeSinceLastScale >= 0.02f)
                    {
                        timeSinceLastScale = 0;

                        scale += scaleChanger;

                        if (scale >= 1.5 || scale <= 0.8)
                        {
                            scaleChanger = -scaleChanger;
                        }

                        this.Sprite.Scale = new Vector2f(Sprite.Scale.X + scaleChanger, Sprite.Scale.Y + scaleChanger);

                        if (Text != null)
                        {
                            float maxLetterHeight = Text.Font.GetLineSpacing(Text.CharacterSize);

                            float minScale = Math.Min(Sprite.GetGlobalBounds().Height * 0.7f / maxLetterHeight, Sprite.GetGlobalBounds().Width * 0.7f / Text.GetLocalBounds().Width);

                            Text.Scale = new Vector2f(minScale, minScale);
                        }

                        base.Relocate();
                    }
                }
                else
                {
                    scaling = false;
                    base.Resize();
                    base.Relocate();
                }
            }

        }
    }
}
