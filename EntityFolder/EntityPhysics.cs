using SFML.Graphics;
using SFML.System;
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
        public float mass { get; protected set; }
        private float fallAcceleration { get; set; }
        private float fallSpeed { get; set; }
        protected float jumpSpeed { get; set; }
        public Status status { get; protected set; }
        public Direction direction { get; protected set; }
        public Direction prevDirection { get; protected set; }

        protected EntityPhysics(int coordX, int coordY, int sizeX, int sizeY, int mass, Texture texture)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.mass = mass;
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

            for (coordY = coordinateY + sizeY; coordY <= coordinateY + sizeY + 1 + length; coordY += 1)
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
                for (coordY = coordinateY; coordY < coordinateY + sizeY; coordY += SettingFolder.tileSize)
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
                for (coordY = coordinateY; coordY < coordinateY + sizeY; coordY += SettingFolder.tileSize)
                {
                    Tile checkedTile = MapGenerator.mainLayer[(int)Math.Floor(coordX / (double)SettingFolder.tileSize), (int)Math.Floor(coordY / (double)SettingFolder.tileSize)];
                    if (checkedTile.status == SettingFolder.TileStatus.wall)
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

        public void Jump()
        {
            if (jumpSpeed <= 0)
            {
                status = Status.stand;
                return;
            }
            jumpSpeed -= 0.3f;
            coordinateY -= CheckMoveableUp((int)jumpSpeed);

        }

        public void Fall()
        {
            fallAcceleration *= SettingFolder.GRAVITATION_STRENGTH;
            fallSpeed = mass * fallAcceleration;
            coordinateY += CheckMoveableDown((int)fallSpeed);

        }

        ////////////////////////////////////////////////////////
        
        public void UpdatePhysics()
        {
            if (status == Status.jump)
                Jump();

            if (CheckMoveableDown(1) != 0 && (status == Status.stand || status == Status.fall || status == Status.run || CheckMoveableUp(1) == 0)) // Надо бы пофиксить
            {
                status = Status.fall;

                Fall();
                if (CheckMoveableDown(1) == 0)
                {
                    fallAcceleration = SettingFolder.GRAVITATION_STRENGTH;
                    status = Status.stand;
                }    
            }

            sprite.Position = new Vector2f(coordinateX, coordinateY);

        }


    }
}
