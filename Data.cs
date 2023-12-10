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

    public static readonly Font font1 = new Font("..\\..\\..\\Fonts\\MinecraftBold.otf");
    public static readonly Font font2 = new Font("..\\..\\..\\Fonts\\MinecraftRegular.otf");
    public static readonly Font font3 = new Font("..\\..\\..\\Fonts\\Mintsoda.ttf");

    public static readonly Texture background1 = new Texture("..\\..\\..\\Backgrounds\\Menu.jpg");
    public static readonly Texture background2 = new Texture("..\\..\\..\\Backgrounds\\Forest.jpg");
    public static readonly Texture background3 = new Texture("..\\..\\..\\Backgrounds\\Wood.jpg");
    public static readonly Texture background4 = new Texture("..\\..\\..\\Backgrounds\\House.jpg");

    public static Dictionary<string, int[]> SizesDict = new Dictionary<string, int[]>
    {
        { "1280x1280", new int[] { 1920, 1080 } },
        { "1024x768", new int[] { 1024, 768 } },
        { "800x600", new int[] { 800, 600 } }
    };

    public static Dictionary<string, Texture> GUIDict = new Dictionary<string, Texture>
    {
        { "Pane1", new Texture("..\\..\\..\\Texture\\GUI\\Pane.png") },
        { "Button1", new Texture("..\\..\\..\\Texture\\GUI\\Button.png") },
        { "LogOut", new Texture("..\\..\\..\\Texture\\GUI\\LogOut.png") },
        { "ArrowRight", new Texture("..\\..\\..\\Texture\\GUI\\ArrowRight.png") },
        { "ArrowLeft", new Texture("..\\..\\..\\Texture\\GUI\\ArrowLeft.png") },
        { "ComboBox", new Texture("..\\..\\..\\Texture\\GUI\\ComboBox.png") },
        { "ComboBoxMid", new Texture("..\\..\\..\\Texture\\GUI\\ComboBoxMid.png") },
        { "ComboBoxEdge", new Texture("..\\..\\..\\Texture\\GUI\\ComboBoxEdge.png") },
        { "ComboBoxEdge2", new Texture("..\\..\\..\\Texture\\GUI\\ComboBoxEdge2.png") },
        { "CheckBoxFalse", new Texture("..\\..\\..\\Texture\\GUI\\CheckBoxFalse.png") },
        { "CheckBoxTrue", new Texture("..\\..\\..\\Texture\\GUI\\CheckBoxTrue.png") },
        { "Couter", new Texture("..\\..\\..\\Texture\\GUI\\Couter.png") },
        { "TextField", new Texture("..\\..\\..\\Texture\\GUI\\TextField.png") }
    };

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
