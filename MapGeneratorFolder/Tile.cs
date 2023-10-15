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

        public Tile(char textureKey = ' ')
        {
            switch (textureKey)
            {
                case '@':
                    sprite = new Sprite(Data.woodplankTexture);
                    type = TileType.platform;
                    break;
                case '#':
                    sprite = new Sprite(Data.grassTexture);
                    type = TileType.wall;
                    break;
                default:
                    sprite = new Sprite(Data.skyTexture);
                    type = TileType.empty;
                    break;
            }

        }
    }
}
