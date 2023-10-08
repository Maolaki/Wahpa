﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EntityEngine
{
    public class Hero : EntityPhysics
    {
        public int speed { get; set; }

        public Hero(int coordX, int coordY) : base(coordX, coordY, 32, 48, 3, SettingFolder.heroStandingTexture)
        {
            this.speed = 1;

        }

        public void Move()
        {
            if ((Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Space)) && this.CheckMoveableDown(1) == 0)
            {
                this.jumpSpeed = 8;
                this.status = Status.jump;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                this.MoveLeft(this.speed);
                this.direction = Direction.left;
                if (status == Status.stand)
                    status = Status.run;
            } 
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
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
                sprite.Texture = SettingFolder.heroStandingTexture;
            } 
            else if (status == Status.run)
            {
                sprite.Texture = SettingFolder.heroRunning1Texture;
            } 
            else if (status == Status.jump || status == Status.fall)
            {
                sprite.Texture = SettingFolder.heroJumpingTexture;
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
