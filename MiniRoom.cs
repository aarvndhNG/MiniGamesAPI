using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamesAPI
{
    public class MiniRoom
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public MiniRoom(int id,string name) {
            ID = id;
            Name = name;
        }
        public MiniRoom() { }
    }
}
