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
    public const String LEVEL1_ONE_ONE = "..\\..\\..\\MapTemplates\\Level1Templates\\one_one\\1.txt";
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

    public static readonly Texture platformSize3 = new Texture("..\\..\\..\\Backgrounds\\Forest.jpg");
    public static readonly Texture platformSize5 = new Texture("..\\..\\..\\Backgrounds\\Wood.jpg");
    public static readonly Texture platformSize9 = new Texture("..\\..\\..\\Backgrounds\\House.jpg");

    public static Dictionary<string, uint[]> SizesDict = new Dictionary<string, uint[]>
    {
        { "1920x1080", new uint[] { 1920, 1080 } },
        { "1024x768", new uint[] { 1024, 768 } },
        { "800x600", new uint[] { 800, 600 } }
    };

    public static Dictionary<string, Texture> GUIDict = new Dictionary<string, Texture>
    {
        { "Pane1", new Texture("..\\..\\..\\Texture\\GUI\\Pane.png") },
        { "Button1", new Texture("..\\..\\..\\Texture\\GUI\\Button.png") },
        { "Button2", new Texture("..\\..\\..\\Texture\\GUI\\WoodButton.png") },
        { "Button3", new Texture("..\\..\\..\\Texture\\GUI\\StoneButton.png") },
        { "Button4", new Texture("..\\..\\..\\Texture\\GUI\\FireButton.png") },
        { "Button5", new Texture("..\\..\\..\\Texture\\GUI\\IceButton.png") },
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
        { "TextField", new Texture("..\\..\\..\\Texture\\GUI\\TextField.png") },
        { "Empty", new Texture("..\\..\\..\\Texture\\GUI\\Empty.png") },

        { "FireShamanStand1", new Texture("..\\..\\..\\Texture\\FireShaman\\heroStand1.png") },
        { "FireShamanStand2", new Texture("..\\..\\..\\Texture\\FireShaman\\heroStand2.png") },
        { "FireShamanRun1", new Texture("..\\..\\..\\Texture\\FireShaman\\heroRun1.png") },
        { "FireShamanRun2", new Texture("..\\..\\..\\Texture\\FireShaman\\heroRun2.png") },
        { "FireShamanRun3", new Texture("..\\..\\..\\Texture\\FireShaman\\heroRun3.png") },
        { "FireShamanJump", new Texture("..\\..\\..\\Texture\\FireShaman\\heroJump.png") },
        { "FireShamanBullet", new Texture("..\\..\\..\\Texture\\FireShaman\\bullet.png") },

        { "IceShamanStand1", new Texture("..\\..\\..\\Texture\\IceShaman\\heroStand1.png") },
        { "IceShamanStand2", new Texture("..\\..\\..\\Texture\\IceShaman\\heroStand2.png") },
        { "IceShamanRun1", new Texture("..\\..\\..\\Texture\\IceShaman\\heroRun1.png") },
        { "IceShamanRun2", new Texture("..\\..\\..\\Texture\\IceShaman\\heroRun2.png") },
        { "IceShamanRun3", new Texture("..\\..\\..\\Texture\\IceShaman\\heroRun3.png") },
        { "IceShamanJump", new Texture("..\\..\\..\\Texture\\IceShaman\\heroJump.png") },
        { "IceShamanBullet", new Texture("..\\..\\..\\Texture\\IceShaman\\bullet.png") },

        { "SkeletonRun1", new Texture("..\\..\\..\\Texture\\Skeleton\\skeletonRun1.png") },
        { "SkeletonRun2", new Texture("..\\..\\..\\Texture\\Skeleton\\skeletonRun2.png") },
        { "SkeletonRun3", new Texture("..\\..\\..\\Texture\\Skeleton\\skeletonRun3.png") },

        { "GolemRun1", new Texture("..\\..\\..\\Texture\\Golem\\golemRun1.png") },
        { "GolemRun2", new Texture("..\\..\\..\\Texture\\Golem\\golemRun2.png") },
        { "GolemRun3", new Texture("..\\..\\..\\Texture\\Golem\\golemRun3.png") },

        { "Platform3", new Texture("..\\..\\..\\Texture\\GUI\\Platform3.png") },
        { "Platform5", new Texture("..\\..\\..\\Texture\\GUI\\Platform5.png") },
        { "Platform9", new Texture("..\\..\\..\\Texture\\GUI\\Platform9.png") },
        { "Coin", new Texture("..\\..\\..\\Texture\\GUI\\Coin.png") },
        { "Crystal", new Texture("..\\..\\..\\Texture\\GUI\\Crystal.png") },
        { "HeartFull", new Texture("..\\..\\..\\Texture\\GUI\\HeartFull.png") },
        { "HeartHalf", new Texture("..\\..\\..\\Texture\\GUI\\HeartHalf.png") },
        { "HeartEmpty", new Texture("..\\..\\..\\Texture\\GUI\\HeartEmpty.png") }
    };

    public static Dictionary<string, List<Texture>> EntityDictionary = new Dictionary<string, List<Texture>>
    {
        {"FireShamanStand",
        new List<Texture>(new Texture[] {
            GUIDict["FireShamanStand1"],
            GUIDict["FireShamanStand1"],
            GUIDict["FireShamanStand1"],
            GUIDict["FireShamanStand2"],
            GUIDict["FireShamanStand2"],
            GUIDict["FireShamanStand2"]
        }) },
        {"FireShamanRun",
            new List<Texture>(new Texture[] {
            GUIDict["FireShamanRun1"],
            GUIDict["FireShamanRun2"],
            GUIDict["FireShamanRun3"],
            GUIDict["FireShamanRun2"]
        }) },
        {"FireShamanJump",
            new List<Texture>(new Texture[] {
            GUIDict["FireShamanJump"]
        }) },

        {"IceShamanStand",
        new List<Texture>(new Texture[] {
            GUIDict["IceShamanStand1"],
            GUIDict["IceShamanStand1"],
            GUIDict["IceShamanStand1"],
            GUIDict["IceShamanStand2"],
            GUIDict["IceShamanStand2"],
            GUIDict["IceShamanStand2"]
        }) },
        {"IceShamanRun",
            new List<Texture>(new Texture[] {
            GUIDict["IceShamanRun1"],
            GUIDict["IceShamanRun2"],
            GUIDict["IceShamanRun3"],
            GUIDict["IceShamanRun2"]
        }) },
        {"IceShamanJump",
            new List<Texture>(new Texture[] {
            GUIDict["IceShamanJump"]
        }) },

        {"SkeletonRun",
            new List<Texture>(new Texture[] {
            GUIDict["SkeletonRun1"],
            GUIDict["SkeletonRun2"],
            GUIDict["SkeletonRun3"],
            GUIDict["SkeletonRun2"]
        }) },

        {"GolemRun",
            new List<Texture>(new Texture[] {
            GUIDict["GolemRun1"],
            GUIDict["GolemRun2"],
            GUIDict["GolemRun3"],
            GUIDict["GolemRun2"]
        }) }
    };

}
