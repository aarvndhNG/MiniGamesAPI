using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniGamesAPI.Enum;

namespace MiniGamesAPI
{
    public interface IPlayer
    {
        int ID { get;  }
        string Name { get;  }
        bool IsReady { get; set; }
        int CurrentRoomID { get; set; }
        PlayerStatus Status { get; set; }
        void Ready();
      
    }
}
