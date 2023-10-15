using SFML.Graphics;

public static class Data
{
    public const int tileSize = 16;
    public const float GRAVITATION_STRENGTH = 0.2f;
    public const float JUMP_STRENGTH = 6f;

    public const int LEVEL1_CHUNK_SIZE = 9;

    public static readonly Texture grassTexture = new Texture("..\\..\\..\\Texture\\grass.png");
    public static readonly Texture woodplankTexture = new Texture("..\\..\\..\\Texture\\woodplank.png");
    public static readonly Texture skyTexture = new Texture("..\\..\\..\\Texture\\sky.png");

    public static readonly Texture heroStandingTexture = new Texture("..\\..\\..\\Texture\\heroStanding.png");
    public static readonly Texture heroJumpingTexture = new Texture("..\\..\\..\\Texture\\heroJumping.png");
    public static readonly Texture heroRunning1Texture = new Texture("..\\..\\..\\Texture\\heroRunning1.png");

}
