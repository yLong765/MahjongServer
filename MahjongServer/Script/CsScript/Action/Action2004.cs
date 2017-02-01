using MahjongServer.Script.CsScript.GameLogic;
using MahjongServer.Script.CsScript.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{
    class Action2004 : BaseStruct
    {
        private int roomID;
        private string playerName;
        private int id;
        private int target;
        private int StartNum;

        public Action2004(HttpGet httpGet) : base(2004, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetString("playerName", ref playerName);

            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack(id);
            PushIntoStack(target);
            PushIntoStack(StartNum);
        }

        public override bool TakeAction()
        {
            id = Room.getId(roomID, playerName);
            target = Room.getTarget(roomID);
            StartNum = Room.getStartName(roomID);

            return true;
        }

    }
}
