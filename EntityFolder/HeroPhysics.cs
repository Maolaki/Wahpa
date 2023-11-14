using SFML.Graphics;
using SFML.System;
using SFML.Window;
using WindowEngine;

namespace EntityEngine
{
    internal abstract class HeroPhysics : EntityPhysics
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

        public Status status { get; protected set; }
        public Direction direction { get; protected set; }
        public Direction prevDirection { get; protected set; }

        protected HeroPhysics(int coordinateX, int coordinateY, int sizeX, int sizeY, int mass, Texture texture) 
            : base(coordinateX, coordinateY, sizeX, sizeY, mass, texture)
        {
            direction = Direction.right;
            prevDirection = Direction.right;
        }

        /////////////////////////////////////////////////////////////////

        protected float jumpHoldTime = 0f;
        protected float maxJumpHoldTime = 0.6f;
        protected float fallSpeed = 0;
        protected float jumpSpeed = 0;

        protected void Jump()
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

        protected void Fall()
        {
            fallSpeed += Data.GRAVITATION_STRENGTH;
            coordinateY += CheckMoveableDown((int)fallSpeed);

            if (CheckMoveableDown(1) == 0)
            {
                status = Status.stand;
                fallSpeed = 0;
            }
        }

        protected void UpdatePhysics()
        {
            if (status == Status.jump)
                Jump();

            if (status == Status.fall || CheckMoveableDown(1) != 0 && (status == Status.stand || status == Status.run)) // Надо бы пофиксить
                Fall();


            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }
    }
}
