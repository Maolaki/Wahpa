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
        public Sprite sprite { get; private set; } = new Sprite();
        public TileType type { get; set; }

        public Tile()
        {
            this.type = TileType.empty;
        }

        public Tile(int coordX, int coordY, Chunk chunk, string textureFolder)
        {
            this.type = TileType.wall;
            GetSprite(coordX, coordY, chunk, textureFolder);
        }

        public void SetTile(TileType type, string texturePath = null)
        {
            if (texturePath != null)
                this.sprite = new Sprite(new Texture(texturePath));

            this.type = type;
        }

        public Sprite GetSprite(int coordX, int coordY, Chunk chunk, string textureFolder)
        {
            Sprite sprite = new Sprite();

            switch (coordY % 3)
            {
                case 0:
                    sprite = new Sprite(new Texture(Path.Combine(textureFolder, "tile1.png")));
                    break;
                case 1:
                    sprite = new Sprite(new Texture(Path.Combine(textureFolder, "tile2.png")));
                    break;
                case 2:
                    sprite = new Sprite(new Texture(Path.Combine(textureFolder, "tile3.png")));
                    break;
            }

            if (chunk.charTileArray[coordX, coordY - 1] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border1.png")), 0, 0);

            if (chunk.charTileArray[coordX + 1, coordY] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border2.png")), 12, 0);

            if (chunk.charTileArray[coordX, coordY - 1] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border3.png")), 0, 11);

            if (chunk.charTileArray[coordX - 1, coordY] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "border4.png")), 0, 0);



            if (chunk.charTileArray[coordX - 1, coordY - 1] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner1.png")), 0, 0);

            if (chunk.charTileArray[coordX + 1, coordY - 1] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner2.png")), 12, 0);

            if (chunk.charTileArray[coordX + 1, coordY + 1] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner3.png")), 12, 12);

            if (chunk.charTileArray[coordX - 1, coordY + 1] == '#')
                sprite.Texture.Update(new Texture(Path.Combine(textureFolder, "corner4.png")), 0, 12);

            return sprite;
        }
    }
}
