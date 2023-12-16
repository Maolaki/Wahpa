using SFML.Graphics;
using SFML.System;
using SFML.Window;
using WindowEngine;
using static EntityEngine.Hero;

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

        public Status moveStatus { get; protected set; }
        public Status prevMoveStatus { get; protected set; }
        public HeroClass playerClass { get; set; }

        protected HeroPhysics(int coordinateX, int coordinateY, int sizeX, int sizeY, string animName, int health)
            : base(coordinateX, coordinateY, sizeX, sizeY, animName, health)
        {
            direction = Direction.right;
            prevDirection = Direction.right;
        }

        /////////////////////////////////////////////////////////////////

        public static string GetAnimationKey(int animPack, HeroClass heroClass)
        {
            switch (animPack)
            {
                case 1:
                    if (heroClass == HeroClass.FireMage)
                        return "FireShamanStand";
                    else
                        return "IceShamanStand";

                case 2:
                    if (heroClass == HeroClass.FireMage)
                        return "FireShamanRun";
                    else
                        return "IceShamanRun";

                default:
                    if (heroClass == HeroClass.FireMage)
                        return "FireShamanJump";
                    else
                        return "IceShamanJump";
            }
        }

        protected void UpdateSetAnimation()
        {
            if (moveStatus == Status.stand)
            {
                SetAnimation(GetAnimationKey(1, playerClass));
            }
            else if (moveStatus == Status.run)
            {
                SetAnimation(GetAnimationKey(2, playerClass));
            }
            else if (moveStatus == Status.jump || moveStatus == Status.fall)
            {
                SetAnimation(GetAnimationKey(3, playerClass));
            }
        }

        /////////////////////////////////////////////////////////////////

        protected float jumpHoldTime = 0f;
        protected float maxJumpHoldTime = 2f;
        protected float fallSpeed = 0;
        protected float jumpSpeed = 0;

        protected void Jump()
        {
            if ((Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Space)) && jumpHoldTime < maxJumpHoldTime)
            {
                jumpSpeed -= Data.GRAVITATION_STRENGTH * 0.2f;
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
                moveStatus = Status.fall;
                jumpHoldTime = 0f;
            }
        }

        protected void Fall()
        {
            fallSpeed += Data.GRAVITATION_STRENGTH;
            coordinateY += CheckMoveableDown((int)fallSpeed);

            if (CheckMoveableDown(1) == 0)
            {
                moveStatus = Status.stand;
                fallSpeed = 0;
            }
        }

        protected void UpdatePhysics()
        {
            if (moveStatus == Status.jump)
                Jump();

            if (moveStatus == Status.fall || CheckMoveableDown(1) != 0 && (moveStatus == Status.stand || moveStatus == Status.run)) // Надо бы пофиксить
                Fall();

            sprite.Position = new Vector2f(coordinateX, coordinateY);
        }
    }
}