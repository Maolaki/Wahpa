using SFML.Graphics;
using SFML.System;
using WindowEngine;

namespace EntityEngine
{
    public class EntityPhysics
    {
        public int coordinateX { get; private set; }
        public int coordinateY { get; private set; }
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }
        public Sprite sprite { get; protected set; }

        protected EntityPhysics(int coordX, int coordY, int sizeX, int sizeY, Texture texture)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(0,0);
            sprite.Position = new Vector2f(coordX, coordY);
            coordinateX = (int)sprite.Position.X;
            coordinateY = (int)sprite.Position.Y;
        }

        public int CheckMoveableUp(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;
            
            for (coordY = coordinateY - 1; coordY >= coordinateY - 1 - length; coordY -= 1)
            {
                for (coordX = coordinateX; coordX <= coordinateX + sizeX; coordX += SettingFolder.tileSize)
                {
                    Tile checkedTile = MapGenerator.mainLayer[(int)Math.Floor(coordX / (double)SettingFolder.tileSize), (int)Math.Floor(coordY / (double)SettingFolder.tileSize)];
                    if (checkedTile.status == SettingFolder.TileStatus.wall)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        public int CheckMoveableDown(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;

            for (coordY = coordinateY + sizeY + 1; coordY <= coordinateY + sizeY + 1 + length; coordY += 1)
            {
                for (coordX = coordinateX; coordX <= coordinateX + sizeX; coordX += SettingFolder.tileSize)
                {
                    Tile checkedTile = MapGenerator.mainLayer[(int)Math.Floor(coordX / (double)SettingFolder.tileSize), (int)Math.Floor(coordY / (double)SettingFolder.tileSize)];
                    if (checkedTile.status == SettingFolder.TileStatus.wall)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        public int CheckMoveableLeft(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;

            for (coordX = coordinateX - 1; coordX >= coordinateX - 1 - length; coordX -= 1)
            {
                for (coordY = coordinateY; coordY <= coordinateY + sizeY; coordY += SettingFolder.tileSize)
                {
                    Tile checkedTile = MapGenerator.mainLayer[(int)Math.Floor(coordX / (double)SettingFolder.tileSize), (int)Math.Floor(coordY / (double)SettingFolder.tileSize)];
                    if (checkedTile.status == SettingFolder.TileStatus.wall)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        public int CheckMoveableRight(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;

            for (coordX = coordinateX + sizeX + 1; coordX <= coordinateX + sizeX + 1 + length; coordX += 1)
            {
                for (coordY = coordinateY; coordY <= coordinateY + sizeY; coordY += SettingFolder.tileSize)
                {
                    Tile checkedTile = MapGenerator.mainLayer[(int)Math.Floor(coordX / (double)SettingFolder.tileSize), (int)Math.Floor(coordY / (double)SettingFolder.tileSize)];
                    if (checkedTile.status == SettingFolder.TileStatus.wall)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        public void MoveUp(int length)
        {
            coordinateY -= CheckMoveableUp(length);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        public void MoveDown(int length)
        {
            coordinateY += CheckMoveableDown(length);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        public void MoveLeft(int length)
        {
            coordinateX -= CheckMoveableLeft(length);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        public void MoveRight(int length)
        {
            coordinateX += CheckMoveableRight(length);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        public void MoveUpRight(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY -= CheckMoveableUp(normalizeLength);
            coordinateX += CheckMoveableRight(normalizeLength);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        public void MoveUpLeft(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY -= CheckMoveableUp(normalizeLength);
            coordinateX -= CheckMoveableLeft(normalizeLength);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        public void MoveDownRight(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY += CheckMoveableDown(normalizeLength);
            coordinateX += CheckMoveableRight(normalizeLength);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        public void MoveDownLeft(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY += CheckMoveableDown(normalizeLength);
            coordinateX -= CheckMoveableLeft(normalizeLength);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }
    }
}
