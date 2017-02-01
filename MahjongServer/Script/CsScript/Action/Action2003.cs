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
    class Action2003 : BaseStruct
    {
        private int roomID;
        private int playerId;
        private int num;
        private int level;

        public Action2003(HttpGet httpGet) : base(2003, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetInt("playerId", ref playerId);
            httpGet.GetInt("num", ref num);
            httpGet.GetInt("level", ref level);
            return true;
        }

        public override bool TakeAction()
        {
            Console.WriteLine("playerId : " + playerId);

            GameLogic.SetLevel(roomID, playerId, level);
            GameLogic.SetNum(roomID, playerId, num);

            int playerID = -1;
            if ((playerID = GameLogic.GetPlayerId(roomID)) != -1)
            {
                Console.WriteLine("Target : " + playerID);
                Radio(roomID, playerID);
            }
            return true;
        }

        private void Radio(int id, int playerID)
        {
            List<GameSession> sessionList = Room.getSessionsOfRoom(id);
            var parameters = new Parameters();
            parameters["num"] = GameLogic.GetNum(id, playerID);
            parameters["level"] = GameLogic.GetLevel(id, playerID);
            parameters["playerID"] = playerID;

            ActionFactory.SendAction(sessionList, 3002, parameters, (session, asyncResult) =>
            {
                Console.WriteLine(string.Format("Action 3002 send result:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail"));
            }, httpGet.OpCode, 0);
        }

    }
}
