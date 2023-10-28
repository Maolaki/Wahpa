using MapGen;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EntityEngine
{
    public class Hero : EntityPhysics
    {
        public int speed { get; set; }

        public Hero(int coordX, int coordY) : base(coordX, coordY, 32, 48, 2, Data.heroStandingTexture)
        {
            this.speed = 1;

        }

        public bool IsJumpKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Space);
        }

        public bool IsLeftKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.A);
        }

        public bool IsRightKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.D);
        }

        public void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.N))
            {
                Level1Generator.GenerateChunks();
                MapGenerator.UpdateMap();
            }

            if (IsJumpKeyPressed() && (status == Status.stand || status == Status.run))
            {
                this.jumpSpeed = Data.JUMP_STRENGTH;
                this.status = Status.jump;
            }

            if (IsLeftKeyPressed())
            {
                this.MoveLeft(this.speed);
                this.direction = Direction.left;
                if (status == Status.stand)
                    status = Status.run;
            }
            else if (IsRightKeyPressed())
            {
                this.MoveRight(this.speed);
                this.direction = Direction.right;
                if (status == Status.stand)
                    status = Status.run;
            }
            else
            {
                if (status == Status.run)
                    status = Status.stand;
            }

        }

        //////////////////////////////////////////////////////////

        public void Update()
        {
            Move();
            UpdatePhysics();

            //////////////////////////////////////////////

            //Анимации
            if (status == Status.stand)
            {
                sprite.Texture = Data.heroStandingTexture;
            } 
            else if (status == Status.run)
            {
                sprite.Texture = Data.heroRunning1Texture;
            } 
            else if (status == Status.jump || status == Status.fall)
            {
                sprite.Texture = Data.heroJumpingTexture;
            }

            //Направления
            if (direction == Direction.right && prevDirection == Direction.left)
            {
                sprite.Scale = new Vector2f(1f, 1f);
                sprite.Origin -= new Vector2f(32, 0);
                prevDirection = Direction.right;
            } 
            else if (direction == Direction.left && prevDirection == Direction.right)
            {
                sprite.Scale = new Vector2f(-1f, 1f);
                sprite.Origin += new Vector2f(32, 0);
                prevDirection = Direction.left;
            }


        }
    }
}
