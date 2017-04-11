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
    class Action2008 : BaseStruct
    {
        private int roomID;
        private string winName;

        public Action2008(HttpGet httpGet) : base(2008, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack(winName);
        }

        public override bool TakeAction()
        {
            winName = GameLogic.getWinName(roomID);
            return true;
        }
    }
}
