using SFML.Graphics;

namespace WindowEngine
{
    internal static class SettingFolder
    {
        public const int tileSize = 16;

        public static readonly Texture grassTexture = new Texture("..\\..\\..\\Texture\\grass.png");
        public static readonly Texture woodplankTexture = new Texture("..\\..\\..\\Texture\\woodplank.png");
        public static readonly Texture skyTexture = new Texture("..\\..\\..\\Texture\\sky.png");

        public enum TileStatus
        {
            empty,
            wall
        }
    }
}
