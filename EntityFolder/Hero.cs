using SFML.System;
using SFML.Window;
using WindowEngine;

namespace EntityEngine
{
    internal class Hero : HeroPhysics
    {
        public enum HeroClass
        {
            FireMage = 1,
            IceMage = 2
        }
        private Clock skillClock { get; set; } = new Clock();
        private Time skillDeltaTime { get; set; }
        private float skillCooldown { get; set; } = 0;

        private static Clock heroClock = new Clock();
        private static Time attackedDeltaTime {  get; set; }
        public int speed { get; set; }
        public int coins { get; set; }
        public int crystals { get; set; }
        public float lastTimeDamage { get; set; } = 2;

        public Hero(int coordX, int coordY, HeroClass playerClass) : base(coordX, coordY, 16, 24, GetAnimationKey(1, playerClass), 6)
        {
            this.playerClass = playerClass;
            this.speed = 2;
        }

        ///////////////////////////////////////////
        // Механика получения урона

        public override void Attacked()
        {
            attackedDeltaTime = heroClock.Restart();

            if (lastTimeDamage < 1.5f)
                lastTimeDamage += attackedDeltaTime.AsSeconds();

            if (lastTimeDamage >= 1.5f)
            {
                lastTimeDamage = 0;
                health -= 1;

                if (health == 0)
                {
                    ViewHandler.LoadLoseScreen();
                }
            }
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

        public bool IsSkillKeyPressed()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Q);
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
            if (IsSkillKeyPressed())
            {
                
                if (skillCooldown > 2)
                {
                    ViewHandler.CastSkill(playerClass);
                    skillCooldown = 0;
                }
                else
                {
                    skillDeltaTime = skillClock.Restart();
                    skillCooldown += skillDeltaTime.AsSeconds();
                }
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