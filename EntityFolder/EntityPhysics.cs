using SFML.Graphics;
using SFML.System;
using SFML.Window;
using WindowEngine;

namespace EntityEngine
{
    public class EntityPhysics
    {
        public enum Status
        {
            jump,
            stand,
            fall,
            run
        }

        public enum Direction
        {
            right,
            left
        }

        public int coordinateX { get; private set; }
        public int coordinateY { get; private set; }
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }
        public Sprite sprite { get; protected set; }
        public Status status { get; protected set; }
        public Direction direction { get; protected set; }
        public Direction prevDirection { get; protected set; }

        protected EntityPhysics(int coordX, int coordY, int sizeX, int sizeY, int mass, Texture texture)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(0,0);
            sprite.Position = new Vector2f(coordX, coordY);
            coordinateX = (int)sprite.Position.X;
            coordinateY = (int)sprite.Position.Y;
            direction = Direction.right;
            prevDirection = Direction.right;
        }

        /////////////////////////////////////////////////////////////

        public int CheckMoveableUp(int length)
        {
            int coordX;
            int coordY;
            int moveableLength = 0;
            
            for (coordY = coordinateY - 1; coordY >= coordinateY - 1 - length; coordY -= 1)
            {
                for (coordX = coordinateX; coordX <= coordinateX + sizeX; coordX += Data.tileSize)
                {
                    MapGen.Tile checkedTile = MainWindow.viewHandler.tileViewMap[(int)Math.Floor(coordX / (double)Data.tileSize), (int)Math.Floor(coordY / (double)Data.tileSize)];
                    if (checkedTile.type == MapGen.TileType.wall)
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

            for (coordY = coordinateY + sizeY; coordY <= coordinateY + sizeY + 1 + length; coordY += 1)
            {
                for (coordX = coordinateX; coordX <= coordinateX + sizeX; coordX += Data.tileSize)
                {
                    MapGen.Tile checkedTile = MainWindow.viewHandler.tileViewMap[(int)Math.Floor(coordX / (double)Data.tileSize), (int)Math.Floor(coordY / (double)Data.tileSize)];
                    if (checkedTile.type == MapGen.TileType.wall || checkedTile.type == MapGen.TileType.platform)
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

        public int CheckMoveableRight(int length)
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

        //////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////

        private float jumpHoldTime = 0f;
        private float maxJumpHoldTime = 0.6f;
        private float fallSpeed = 0;
        protected float jumpSpeed = 0;

        public void Jump()
        {
            if ((Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Space)) && jumpHoldTime < maxJumpHoldTime)
            {
                jumpSpeed -= Data.GRAVITATION_STRENGTH * 0.5f;
                jumpHoldTime += MainWindow.deltaTime;
            }
            else
            {
                jumpSpeed -= Data.GRAVITATION_STRENGTH;
                jumpHoldTime = maxJumpHoldTime;
            }

            coordinateY -= CheckMoveableUp((int)jumpSpeed);

            if (jumpSpeed <= 0)
            {
                status = Status.fall;
                jumpHoldTime = 0f;
            }
        }

        public void Fall()
        {
            fallSpeed += Data.GRAVITATION_STRENGTH;
            coordinateY += CheckMoveableDown((int)fallSpeed);

            if (CheckMoveableDown(1) == 0)
            {
                status = Status.stand;
                fallSpeed = 0;
            }
        }

        ////////////////////////////////////////////////////////

        public void UpdatePhysics()
        {
            if (status == Status.jump)
                Jump();

            if (status == Status.fall || CheckMoveableDown(1) != 0 && (status == Status.stand || status == Status.run)) // Надо бы пофиксить
                Fall();


            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }


    }
}
