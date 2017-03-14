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
        private string playerName;

        public Action2008(HttpGet httpGet) : base(2008, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetString("playerName", ref playerName);

            return true;
        }

        public override bool TakeAction()
        {
            Radio(roomID, playerName);
            return true;
        }

        private void Radio(int id, string playerName)
        {
            var sessionList = Room.getSessionsOfRoom(id);

            var parameters = new Parameters();
            parameters["playerName"] = playerName;

            ActionFactory.SendAction(sessionList, 3003, parameters, (session, asyncResult) =>
            {
                Console.WriteLine(string.Format("Action 3003 send result:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail"));
            }, httpGet.OpCode, 0);
        }
    }
}
