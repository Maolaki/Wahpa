﻿using SFML.Graphics;

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

    public static readonly Texture grassTexture = new Texture("..\\..\\..\\Texture\\grass.png");
    public static readonly Texture woodplankTexture = new Texture("..\\..\\..\\Texture\\woodplank.png");
    public static readonly Texture skyTexture = new Texture("..\\..\\..\\Texture\\sky.png");

    public static readonly Texture heroStandingTexture = new Texture("..\\..\\..\\Texture\\heroStanding.png");
    public static readonly Texture heroJumpingTexture = new Texture("..\\..\\..\\Texture\\heroJumping.png");
    public static readonly Texture heroRunning1Texture = new Texture("..\\..\\..\\Texture\\heroRunning1.png");

    public struct EntityAnimation
    {
        string anim1;
        string anim2;
        string anim3;
        string anim4;

        public EntityAnimation(string anim1, string anim2, string anim3, string anim4)
        {
            this.anim1 = anim1;
            this.anim2 = anim2;
            this.anim3 = anim3;
            this.anim4 = anim4;
        }
    }

    public static Dictionary<string, EntityAnimation> EntityDictionary = new Dictionary<string, EntityAnimation>
    {
        // придумать тут навыки всякие
        {"BloodExposion", new("///", "///", "///", "///") },
        {"BloodArrow", new("///", "///", "///", "///") },
        {"BloodSpike", new("///", "///", "///", "///") }
    };

}
