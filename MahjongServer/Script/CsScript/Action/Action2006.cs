using MahjongServer.Script.CsScript.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    class Action2006 : BaseStruct
    {
        private int roomID;
        private string roomName;
        private int size;

        public Action2006(HttpGet httpGet) : base(2006, httpGet)
        {
        }
        public override void BuildPacket()
        {
            PushIntoStack(roomName);
            PushIntoStack(size);
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            return true;
        }

        public override bool TakeAction()
        {
            roomName = Room.getName(roomID);
            size = Room.getSize(roomID);
            return true;
        }
    }
}
