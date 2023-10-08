using SFML.System;

namespace WindowEngine
{
    internal static class MapGenerator
    {
        public static Tile[,] mainLayer { get; set; }
        
        public static void CreateMainLayer(string[] map)
        {
            char[,] charArray = new char[map[0].Length, map.Length];
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    charArray[j, i] = map[i][j];
                }
            }

            mainLayer = new Tile[charArray.GetLength(0), charArray.GetLength(1)];

            for (int x = 0; x < mainLayer.GetLength(0); x++)
            {
                for (int y = 0; y < mainLayer.GetLength(1); y++)
                {
                    mainLayer[x, y] = new Tile(charArray[x, y]);
                    mainLayer[x, y].sprite.Position = new Vector2f(x * SettingFolder.tileSize, y * SettingFolder.tileSize);
                }
            }
        }


    }
}
