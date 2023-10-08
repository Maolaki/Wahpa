using SFML.Graphics;

public static class SettingFolder
{
    public const int tileSize = 16;
    public const float GRAVITATION_STRENGTH = 1.2f;

    public static readonly Texture grassTexture = new Texture("..\\..\\..\\Texture\\grass.png");
    public static readonly Texture woodplankTexture = new Texture("..\\..\\..\\Texture\\woodplank.png");
    public static readonly Texture skyTexture = new Texture("..\\..\\..\\Texture\\sky.png");

    public static readonly Texture heroStandingTexture = new Texture("..\\..\\..\\Texture\\heroStanding.png");
    public static readonly Texture heroJumpingTexture = new Texture("..\\..\\..\\Texture\\heroJumping.png");
    public static readonly Texture heroRunning1Texture = new Texture("..\\..\\..\\Texture\\heroRunning1.png");

    public enum TileStatus
    {
        empty,
        wall
    }
}
