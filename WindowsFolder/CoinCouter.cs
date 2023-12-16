using EntityEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowEngine
{
    internal class CoinCouter : GUIElement, IUpdate
    {
        public CoinCouter(float size, float coordX, float coordY, Texture texture, int location) : base(size, coordX, coordY, texture, location)
        {
            this.Text = new Text("0", Data.font2);
        }

        public void Update()
        {
            int coins = ViewHandler.hero.coins;

            Text.DisplayedString = coins.ToString();
        }

        public override void Relocate()
        {
            Vector2u windowSize = MainWindow.window.Size;
            View view = MainWindow.window.GetView();
            Vector2f viewCenter = view.Center;
            Vector2f viewSize = view.Size;

            float coordX = (float)windowSize.X / 100 * CoordinateX;
            float coordY = (float)windowSize.Y / 100 * CoordinateY;
            float coordCoinX = (float)windowSize.X / 100 * (CoordinateX - 0.6f);
            float coordCoinY = (float)windowSize.Y / 100 * (CoordinateY + 0.1f);

            Vector2f topLeft = viewCenter - viewSize / 2f;

            this.Sprite.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
            this.Text.Position = new Vector2f(topLeft.X + coordCoinX, topLeft.Y + coordCoinY);

        }
    }
}
