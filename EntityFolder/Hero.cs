﻿using SFML.System;
using SFML.Window;
using WindowEngine;

namespace EntityEngine
{
    internal class Hero : HeroPhysics
    {
        public int speed { get; set; }

        public Hero(int coordX, int coordY) : base(coordX, coordY, 16, 24, "HeroBloodShamanStand", 100)
        {
            this.speed = 2;

        }

        ///////////////////////////////////////////
        // Механика навыков

        public override void Attacked()
        {
            
        }

        ///////////////////////////////////////////
        // Механика движения

        public bool IsJumpKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Space) || Keyboard.IsKeyPressed(Keyboard.Key.Up);
        }

        public bool IsLeftKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left);
        }

        public bool IsRightKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right);
        }

        public bool IsEscapeKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Escape);
        }

        public void Move()
        {
            prevMoveStatus = moveStatus;

            if (IsJumpKeyPressed() && (moveStatus == Status.stand || moveStatus == Status.run))
            {
                this.jumpSpeed = Data.JUMP_STRENGTH;
                this.moveStatus = Status.jump;
            }

            if (IsLeftKeyPressed())
            {
                this.MoveLeft(this.speed);
                this.direction = Direction.left;
                if (moveStatus == Status.stand)
                    moveStatus = Status.run;
            }
            else if (IsRightKeyPressed())
            {
                this.MoveRight(this.speed);
                this.direction = Direction.right;
                if (moveStatus == Status.stand)
                    moveStatus = Status.run;
            }
            else
            {
                if (moveStatus == Status.run)
                    moveStatus = Status.stand;
            }

            if (moveStatus != prevMoveStatus)
                UpdateSetAnimation();

        }

        //////////////////////////////////////////////////////////

        public override void Update()
        {
            if (IsEscapeKeyPressed())
            {
                MainWindow.window.Close();
            }

            Move();
            UpdatePhysics();

            //////////////////////////////////////////////

            //Анимации
            UpdateAnimation();

            //Направления
            if (direction == Direction.right && prevDirection == Direction.left)
            {
                sprite.Scale = new Vector2f(1f, 1f);
                sprite.Origin -= new Vector2f(16, 0);
                prevDirection = Direction.right;
            } 
            else if (direction == Direction.left && prevDirection == Direction.right)
            {
                sprite.Scale = new Vector2f(-1f, 1f);
                sprite.Origin += new Vector2f(16, 0);
                prevDirection = Direction.left;
            }


        }
    }
}
