using SFML.Graphics;
using SFML.System;

namespace MapGen
{
    public enum TileType
    {
        empty,
        wall,
        teleport
            
    }

    internal class Tile
    {
        public Sprite sprite { get; set; }
        public TileType type { get; set; }

        public Tile(TileType type)
        {
            this.type = type;
            this.sprite = new Sprite();
        }

        public void SetTile(TileType type, string texturePath = null)
        {
            if (texturePath != null)
                this.sprite = new Sprite(new Texture(texturePath));

            this.type = type;
        }

        public static void LoadBordersAndCorners(Tile[,] tileArray, string textureFolder)
        {
            for (int coordX = 0; coordX < tileArray.GetLength(0); coordX++)
            {
                for (int coordY = 0; coordY < tileArray.GetLength(1); coordY++)
                {
                    if (tileArray[coordX, coordY].type != TileType.wall)
                        continue;

                    Sprite sprite = tileArray[coordX, coordY].sprite;

                    switch (coordX % 3)
                    {
                        case 0:
                            sprite.Texture = new Texture(Path.Combine(textureFolder, "tile1.png"));
                            break;
                        case 1:
                            sprite.Texture = new Texture(Path.Combine(textureFolder, "tile2.png"));
                            break;
                        case 2:
                            sprite.Texture = new Texture(Path.Combine(textureFolder, "tile3.png"));
                            break;
                    }

                    if (coordY > 0 && tileArray[coordX, coordY - 1].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border1.png")), 0, 0);

                    if (coordX < tileArray.GetLength(0) -1 && tileArray[coordX + 1, coordY].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border2.png")), 12, 0);

                    if (coordY < tileArray.GetLength(1) - 1 && tileArray[coordX, coordY + 1].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border3.png")), 0, 12);

                    if (coordX > 0 && tileArray[coordX - 1, coordY].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border4.png")), 0, 0);



                    if (coordX > 0 && coordY > 0 && tileArray[coordX - 1, coordY - 1].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner1.png")), 0, 0);

                    if (coordX < tileArray.GetLength(0) - 1 && coordY > 0 && tileArray[coordX + 1, coordY - 1].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner2.png")), 12, 0);

                    if (coordX < tileArray.GetLength(0) - 1 && coordY < tileArray.GetLength(1) - 1 && tileArray[coordX + 1, coordY + 1].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner3.png")), 12, 12);

                    if (coordX > 0 && coordY < tileArray.GetLength(1) - 1 && tileArray[coordX - 1, coordY + 1].type != TileType.wall)
                        sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner4.png")), 0, 12);
                }
            }
        }
    }
}
