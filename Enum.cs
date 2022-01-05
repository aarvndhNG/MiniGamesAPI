using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamesAPI
{
    public static class Enum
    {
        public enum RoomStatus{
        Waiting,
        Gaming,
        Concluding,
        Restoring,
        Stoped
        }
        public enum PlayerStatus { 
        Wating,
        Selecting,
        Gaming    
        }
    }
}
