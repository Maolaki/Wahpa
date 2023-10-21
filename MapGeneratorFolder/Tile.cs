using SFML.Graphics;
using SFML.System;

namespace MapGen
{
    public enum TileType
    {
        empty,
        wall,
        platform
            
    }

    public enum TileGenType
    {
        freeGen,
        wallGen

    }

    internal class Tile
    {
        public Sprite sprite { get; private set; } 
        public TileType type { get; set; }
        public TileGenType genType { get; set; }

        public Tile()
        {


        }

        public void SetTile(char type)
        {
            switch (type)
            {
                case '@':
                    sprite = new Sprite(Data.woodplankTexture);
                    this.type = TileType.platform;
                    break;
                case '#':
                    sprite = new Sprite(Data.grassTexture);
                    this.type = TileType.wall;
                    break;
                default:
                    sprite = new Sprite(Data.skyTexture);
                    this.type = TileType.empty;
                    break;
            }
        }
    }
}
