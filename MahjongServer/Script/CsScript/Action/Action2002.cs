using MahjongServer.Script.CsScript.Room;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 广播接口
    /// </summary>
    class Action2002 : BaseStruct
    {
        private int roomID;
        private int rO;
        private int brand;

        public Action2002(HttpGet httpGet) : base(2002, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetInt("RadioOperation", ref rO);
            httpGet.GetInt("brand", ref brand);
            return true;
        }

        public override bool TakeAction()
        {

            switch (rO)
            {
                case (int)RadioOperation.rBrand:
                    Console.WriteLine(RadioSession(roomID));
                    break;
            }

            return true;
        }

        private string RadioSession(int id)
        {
            string str = "Error";

            List<GameSession> sessionList = Room.getSessionsOfRoom(id);
            var parameters = new Parameters();
            parameters["brand"] = brand;

            ActionFactory.SendAction(sessionList, 2003, parameters, (session, asyncResult) =>
            {
                str = string.Format("Action 2003 send result:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
            }, httpGet.OpCode, 0);

            return str;
        }

        enum RadioOperation
        {
            rBrand = 1,
        }

    }
}
