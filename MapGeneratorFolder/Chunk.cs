using EntityEngine;
using SFML.Graphics;

namespace MapGen
{
    internal class Chunk
    {
        public bool borderUp { get; set; } = false;
        public bool borderDown { get; set; } = false;
        public bool borderLeft { get; set; } = false;
        public bool borderRight { get; set; } = false;
        public bool exitUp { get; set; } = false;
        public bool exitDown { get; set; } = false;
        public bool exitLeft { get; set; } = false;
        public bool exitRight { get; set; } = false;
        public char[,] ?charTileArray { get; set; } = null;
        public CircleShape shape { get; set; } = new CircleShape();
        public Room ?room = null;
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }

        public Chunk(int coordX, int coordY)
        {
            // Квадратики для пустых чанков
            shape.FillColor = Color.Black;
            this.coordinateX = coordX;
            this.coordinateY = coordY;
            

        }
    }
}
