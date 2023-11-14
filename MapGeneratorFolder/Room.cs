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
        public LinkedList<EntityTemplate> EntityList = new LinkedList<EntityTemplate> { };
        public int sizeX;
        public int sizeY;
        public int startChunkX;
        public int startChunkY;

        public Room()
        {

        }
        
    }
}
