using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    internal abstract class AttackableEntityTemplate : Triggerable
    {
        public bool killable { get; set; }
        public float health { get; set; }

        protected AttackableEntityTemplate(bool killable, float health = 0)
        {
            this.killable = killable;
            this.health = health;
        }

        public abstract void Attacked();

        public abstract void Update();
    }
}
