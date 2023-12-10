using EntityEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wahpa.WindowsFolder;

namespace WindowEngine
{
    internal class LevelBackground
    {
        public Sprite sprite { get; set; } // спрайт для фона
        public float size { get; set; }

        public LevelBackground(Texture texture, float size)
        {
            this.sprite = new Sprite(texture); // создаем спрайт из текстуры
            this.size = size;
            this.Resize();
        }

        public void Resize()
        {
            float pixelSizeX = ViewHandler.pixelSizeX; // получаем размер окна
            float pixelSizeY = ViewHandler.pixelSizeY; // получаем размер окна

            // вычисляем масштаб спрайта по оси X и Y в зависимости от размера окна и фона
            float scaleX = (pixelSizeX / 100 * size) / this.sprite.Texture.Size.X;
            float scaleY = (pixelSizeY / 100 * size) / this.sprite.Texture.Size.Y;

            // выбираем максимальный масштаб, чтобы спрайт заполнял весь экран
            float maxScale = (scaleX > scaleY) ? scaleX : scaleY;

            // устанавливаем масштаб спрайта
            this.sprite.Scale = new Vector2f(maxScale, maxScale);
        }

        public void Relocate()
        {
            View view = MainWindow.window.GetView(); // получаем текущий вид камеры
            Vector2f edge = view.Center - view.Size / 2f;

            float offsetX = edge.X / 100 * size;
            float offsetY = edge.Y / 100 * size;

            this.sprite.Position = new Vector2f(edge.X - offsetX, edge.Y - offsetY);
        }

        public void Draw()
        {
            MainWindow.window.Draw(this.sprite);
        }
    }
}
