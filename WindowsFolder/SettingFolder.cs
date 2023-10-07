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
        grassTexture = new Texture("E:\\Курсачь\\TestFPAv1\\Texture\\grass.png");
        woodplankTexture = new Texture("E:\\Курсачь\\TestFPAv1\\Texture\\woodplank.png");
        skyTexture = new Texture("E:\\Курсачь\\TestFPAv1\\Texture\\sky.png");
        }


    }
}
