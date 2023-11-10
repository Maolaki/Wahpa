using SFML.Graphics;
using SFML.System;
using SFML.Window;
using WindowEngine;

namespace EntityEngine
{
    public class EntityPhysics
    {
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }
        public Sprite sprite { get; protected set; }


        protected EntityPhysics(int coordinateX, int coordinateY, int sizeX, int sizeY, int mass, Texture texture)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(0,0);
            sprite.Position = new Vector2f(coordinateX, coordinateY);
            coordinateX = (int)sprite.Position.X;
            coordinateY = (int)sprite.Position.Y;
        }

        /////////////////////////////////////////////////////////////

        protected int CheckMoveableUp(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;
            
            for (coordY = coordinateY - 1; coordY >= coordinateY - 1 - length; coordY -= 1)
            {
                for (coordX = coordinateX; coordX <= coordinateX + sizeX; coordX += Data.tileSize)
                {
                    MapGen.Tile checkedTile = MainWindow.viewHandler.tileViewMap[coordX / Data.tileSize, coordY / Data.tileSize];
                    if (checkedTile.type == MapGen.TileType.wall)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        protected int CheckMoveableDown(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;

            for (coordY = coordinateY + sizeY; coordY <= coordinateY + sizeY + 1 + length; coordY += 1)
            {
                for (coordX = coordinateX; coordX <= coordinateX + sizeX; coordX += Data.tileSize)
                {
                    MapGen.Tile checkedTile = MainWindow.viewHandler.tileViewMap[coordX / Data.tileSize, coordY / Data.tileSize];
                    if (checkedTile.type == MapGen.TileType.wall || checkedTile.type == MapGen.TileType.platform)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        protected int CheckMoveableLeft(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;

            for (coordX = coordinateX - 1 - 15; coordX >= coordinateX - 1 - length - 15; coordX -= 1)
            {
                for (coordY = coordinateY; coordY < coordinateY + sizeY; coordY += Data.tileSize)
                {
                    MapGen.Tile checkedTile = MainWindow.viewHandler.tileViewMap[(int)Math.Ceiling(coordX / (double)Data.tileSize), (int)Math.Ceiling(coordY / (double)Data.tileSize)];
                    if (checkedTile.type == MapGen.TileType.wall)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        protected int CheckMoveableRight(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;

            for (coordX = coordinateX + sizeX + 1 - 15; coordX <= coordinateX + sizeX + 1 + length - 15; coordX += 1)
            {
                for (coordY = coordinateY; coordY < coordinateY + sizeY; coordY += Data.tileSize)
                {
                    MapGen.Tile checkedTile = MainWindow.viewHandler.tileViewMap[(int)Math.Ceiling(coordX / (double)Data.tileSize), (int)Math.Ceiling(coordY / (double)Data.tileSize)];
                    if (checkedTile.type == MapGen.TileType.wall)
                        return moveableLength;
                }
                moveableLength++;
            }

            return moveableLength;
        }

        public void MoveUp(int length)
        {
            coordinateY -= CheckMoveableUp(length);

        }

        public void MoveDown(int length)
        {
            coordinateY += CheckMoveableUp(length);

        }

        public void MoveLeft(int length)
        {
            coordinateX -= CheckMoveableLeft(length);

        }

        public void MoveRight(int length)
        {
            coordinateX += CheckMoveableRight(length);

        }

        public void MoveUpRight(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY -= CheckMoveableUp(normalizeLength);
            coordinateX += CheckMoveableRight(normalizeLength);

        }

        public void MoveUpLeft(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY -= CheckMoveableUp(normalizeLength);
            coordinateX -= CheckMoveableLeft(normalizeLength);

        }

        public void MoveDownRight(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY += CheckMoveableUp(normalizeLength);
            coordinateX += CheckMoveableRight(normalizeLength);

        }

        public void MoveDownLeft(int length)
        {
            int normalizeLength = (int)Math.Floor(Math.Sqrt(length));
            coordinateY += CheckMoveableUp(normalizeLength);
            coordinateX -= CheckMoveableLeft(normalizeLength);

        }



    }
}
