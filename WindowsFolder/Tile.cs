using SFML.Graphics;
using SFML.System;

namespace WindowEngine
{
    internal class Tile
    {
        public Sprite sprite { get; private set; } 
        public SettingFolder.TileStatus status { get; private set; }

        public Tile(char textureKey = ' ')
        {
            switch (textureKey)
            {
                case '@':
                    sprite = new Sprite(SettingFolder.woodplankTexture);
                    status = SettingFolder.TileStatus.wall;
                    break;
                case '#':
                    sprite = new Sprite(SettingFolder.grassTexture);
                    status = SettingFolder.TileStatus.wall;
                    break;
                default:
                    sprite = new Sprite(SettingFolder.skyTexture);
                    status = SettingFolder.TileStatus.empty;
                    break;
            }

        }
    }
}
