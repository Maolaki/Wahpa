using SFML.Graphics;

public static class Data
{
    public static Random random = new Random();
    public const int tileSize = 16;
    public const float GRAVITATION_STRENGTH = 0.2f;
    public const float JUMP_STRENGTH = 6f;

    public const int ROOM_WALL_SIZE = 3;

    public const int LEVEL1_SIZEX = 100;
    public const int LEVEL1_SIZEY = 100;
    public const int LEVEL1_CHUNK_SIZE = 11;
    public static readonly String LEVEL1_ONE_ONE = "..\\..\\..\\MapTemplates\\Level1Templates\\one_one\\1.txt";
    public static readonly String LEVEL1_ONE_FIVE = "..\\..\\..\\MapTemplates\\Level1Templates\\one_five\\1.txt";
    public static readonly String LEVEL1_TWO_ONE = "..\\..\\..\\MapTemplates\\Level1Templates\\two_one\\1.txt";
    public static readonly String LEVEL1_TWO_TWO = "..\\..\\..\\MapTemplates\\Level1Templates\\two_two\\1.txt";
    public static readonly String LEVEL1_THREE_TWO = "..\\..\\..\\MapTemplates\\Level1Templates\\three_two\\1.txt";
    public static readonly String LEVEL1_FOUR_TWO = "..\\..\\..\\MapTemplates\\Level1Templates\\four_two\\1.txt";
    public static readonly String LEVEL1_FIVE_FIVE = "..\\..\\..\\MapTemplates\\Level1Templates\\five_five\\1.txt";

    public static readonly String LEVEL1_WALL_TEXTURES = "..\\..\\..\\Texture\\WoodPlanks";


    public static Dictionary<string, List<Texture>> EntityDictionary = new Dictionary<string, List<Texture>>
    {
        {"HeroBloodShamanStand",
        new List<Texture>(new Texture[] {
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroStand1.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroStand1.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroStand1.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroStand2.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroStand2.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroStand2.png")
        }) },
        {"HeroBloodShamanRun",
            new List<Texture>(new Texture[] {
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroRun1.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroRun2.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroRun3.png"),
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroRun2.png")
        }) },
        {"HeroBloodShamanJump",
            new List<Texture>(new Texture[] {
            new Texture("..\\..\\..\\Texture\\HeroBloodShaman\\heroJump.png")
        }) }

    };

}
