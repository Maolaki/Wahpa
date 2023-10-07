using SFML.System;

namespace WindowEngine
{
    public static class MapGenerator
    {
        public static int mapLength { get; set; }
        public static int mapHeight { get; set; }
        public static Tile[,] mapGrid { get; set; }

        public static void CreateGrid(int length, int height)
        {
            mapLength = length;
            mapHeight = height;

            mapGrid = new Tile[length, height];

            for (int x = 0; x < mapGrid.GetLength(0); x++)
            {
                for (int y = 0; y < mapGrid.GetLength(1); y++)
                {
                    mapGrid[x, y] = new Tile('#');

                    mapGrid[x, y].sprite.Position = new Vector2f(x * SettingFolder.tileSize, y * SettingFolder.tileSize);
                }
            }
        }


    }
}
