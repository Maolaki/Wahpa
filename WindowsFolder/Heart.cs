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
    internal class Heart : GUIElement, IUpdate
    {
        private int healthSet {  get; set; }

        public Heart(float size, float coordX, float coordY, Texture texture, int location, int healthMin) : base(size, coordX, coordY, texture, location)
        {
            this.healthSet = healthMin;
        }

        public void Update()
        {
            int health = ViewHandler.hero.health;

            if (health > healthSet)
            {
                this.Sprite.Texture = Data.GUIDict["HeartFull"];
            }
            else if (health == healthSet)
            {
                this.Sprite.Texture = Data.GUIDict["HeartHalf"];
            }
            else
            {
                this.Sprite.Texture = Data.GUIDict["HeartEmpty"];
            }

        }

        public override void Relocate()
        {
            Vector2u windowSize = MainWindow.window.Size;
            View view = MainWindow.window.GetView();
            Vector2f viewCenter = view.Center;
            Vector2f viewSize = view.Size;

            float coordX = (float)windowSize.X / 100 * CoordinateX;
            float coordY = (float)windowSize.Y / 100 * CoordinateY;

            Vector2f topLeft = viewCenter - viewSize / 2f;

            this.Sprite.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);

        }
    }
}
