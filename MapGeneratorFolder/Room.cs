using EntityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGen
{
    internal class Room
    {
        public List<Triggerable> heroSkills { get; init; } = new List<Triggerable> { };
        public List<Triggerable> monstersSkills { get; init; } = new List<Triggerable> { };
        public List<Triggerable> monsters { get; init; } = new List<Triggerable> { };
        public Hero hero { get; set; }
        public int sizeX;
        public int sizeY;
        public int startChunkX;
        public int startChunkY;
        
        //тут проверки остальные
    }
}
