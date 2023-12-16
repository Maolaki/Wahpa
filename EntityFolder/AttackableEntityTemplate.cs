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
        public int health { get; set; }

        protected AttackableEntityTemplate(bool killable, int health = 0)
        {
            this.killable = killable;
            this.health = health;
        }

        public abstract void Attacked();

    }
}