using System;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    using MahjongServer.Script.CsScript.GameSetting;
    using MahjongServer.Script.CsScript.Room;
    using System.Collections.Generic;
    using ZyGames.Framework.Net;
    using ZyGames.Framework.RPC.Sockets;

    /// <summary>
    /// 加入房间
    /// </summary>
    class Action2000 : BaseStruct
    {
        private int BackRoom;
        private int roomID;
        private int id;
        private string roomName;
        private string playerName;

        public Action2000(HttpGet httpGet) : base(2000, httpGet)
        {
        }

        public override void BuildPacket()
        {
            PushIntoStack(BackRoom);
            PushIntoStack(id);
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetString("roomName", ref roomName);
            httpGet.GetString("playerName", ref playerName);
            return true;
        }

        public override bool TakeAction()
        {
            if (roomID == -1) roomID = ++GameSetting.roomID;

            if (Room.JoinRoom(roomID, Current.SessionId, roomName, playerName))
            {
                BackRoom = roomID;
                id = Room.getId(roomID);
                Console.WriteLine(RadioSession(BackRoom));
            }
            else
            {
                BackRoom = -1;
            }

            return true;
        }

        private string RadioSession(int id)
        {
            string str = "Error";

            List<GameSession> sessionList = Room.getSessionsOfRoom(id);
            var parameters = new Parameters();

            parameters["roomID"] = id;

            ActionFactory.SendAction(sessionList, 3001, parameters, (session, asyncResult) =>
            {
                str = string.Format("Action 3001 send result:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
            }, httpGet.OpCode, 0);

            return str;
        }
    }
}
