using SFML.Graphics;
using SFML.System;

namespace WindowEngine
{
    public class Tile
    {
        public Sprite sprite { get; private set; } 

        public Tile(char textureKey = ' ')
        {
            switch (textureKey)
            {
                case '@':
                    sprite = new Sprite(SettingFolder.woodplankTexture);
                    break;
                case '#':
                    sprite = new Sprite(SettingFolder.grassTexture);
                    break;
                default:
                    sprite = new Sprite(SettingFolder.skyTexture);
                    break;
            }

        }
    }
}
