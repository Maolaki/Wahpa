using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Data;
using WindowEngine;

namespace EntityEngine
{
    internal abstract class EntityPhysics : AttackableEntityTemplate
    {
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }
        private List<Texture> animList { get; set; }
        private float lastCallTime = 0;
        private int animNumber = 0;

        protected EntityPhysics(int coordinateX, int coordinateY, int sizeX, int sizeY, string animName, float health) : base(true, health)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.triggerZoneSizeX = sizeX;
            this.triggerZoneSizeY = sizeY;
            this.coordinateX = coordinateX;
            this.coordinateY = coordinateY;
            this.SetAnimation(animName);
            this.sprite.Origin = new Vector2f(0,0);
            this.sprite.Position = new Vector2f(coordinateX, coordinateY);
        }

        //////////////////////////////////////////////////////////////

        public void SetAnimation(string animName)
        {
            this.animList = Data.EntityDictionary[animName];
            this.sprite.Texture = animList[0];
            animNumber = 0;
        }

        protected void UpdateAnimation()
        {
            lastCallTime += MainWindow.pDeltaTime.AsSeconds();

            if (lastCallTime >= 0.3f)
            {
                lastCallTime -= 0.3f;
                this.sprite.Texture = animList[animNumber];

                if (animNumber < animList.Count - 1)
                    animNumber++;
                else
                    animNumber = 0;
            }
        }

        //////////////////////////////////////////////////////////////
        protected int CheckMoveableUp(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;
            
            for (coordY = coordinateY - 1; coordY > coordinateY - length; coordY -= 1)
            {
                for (coordX = coordinateX; coordX < coordinateX + sizeX; coordX += 4)
                {
                    MapGen.Tile checkedTile = ViewHandler.tileViewMap[coordX / Data.tileSize, coordY / Data.tileSize];
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

            for (coordY = coordinateY + sizeY; coordY < coordinateY + sizeY + length; coordY += 1)
            {
                for (coordX = coordinateX; coordX < coordinateX + sizeX; coordX += 4)
                {
                    MapGen.Tile checkedTile = ViewHandler.tileViewMap[coordX / Data.tileSize, coordY / Data.tileSize];
                    if (checkedTile.type == MapGen.TileType.wall)
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

            for (coordX = coordinateX - 1; coordX > coordinateX - length; coordX -= 1)
            {
                for (coordY = coordinateY; coordY < coordinateY + sizeY; coordY += 4)
                {
                    MapGen.Tile checkedTile = ViewHandler.tileViewMap[coordX / Data.tileSize, coordY / Data.tileSize];
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

            for (coordX = coordinateX + sizeX; coordX < coordinateX + sizeX + length; coordX += 1)
            {
                for (coordY = coordinateY; coordY < coordinateY + sizeY; coordY += 4)
                {
                    MapGen.Tile checkedTile = ViewHandler.tileViewMap[coordX / Data.tileSize, coordY / Data.tileSize];
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
            coordinateY += CheckMoveableDown(length);

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
