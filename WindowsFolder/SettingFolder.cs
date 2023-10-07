using SFML.Graphics;

namespace WindowEngine
{
    internal static class SettingFolder
    {
        public const int tileSize = 16;

        public static Texture grassTexture;
        public static Texture woodplankTexture;
        public static Texture skyTexture;

        public static void LoadData()
        {
        grassTexture = new Texture("..\\..\\..\\Texture\\grass.png");
        woodplankTexture = new Texture("..\\..\\..\\Texture\\woodplank.png");
        skyTexture = new Texture("..\\..\\..\\Texture\\sky.png");
        }


    }
}
