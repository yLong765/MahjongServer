using System;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    using MahjongServer.Script.CsScript.GameLogic;
    using MahjongServer.Script.CsScript.GameSetting;
    using MahjongServer.Script.CsScript.Room;
    using System.Collections.Generic;
    using ZyGames.Framework.Cache.Generic;
    using ZyGames.Framework.Net;
    using ZyGames.Framework.RPC.Sockets;

    /// <summary>
    /// 房间接口
    /// </summary>
    class Action2000 : BaseStruct
    {
        private int BackRoom;
        private int roomID;
        private int Ro;
        private string roomName;
        private string playerName;

        public Action2000(HttpGet httpGet) : base(2000, httpGet)
        {
        }

        public override void BuildPacket()
        {
            PushIntoStack(BackRoom);
            PushIntoStack(playerName);
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetString("roomName", ref roomName);
            httpGet.GetString("playerName", ref playerName);
            httpGet.GetInt("roomOperation", ref Ro);
            return true;
        }

        public override bool TakeAction()
        {
            switch (Ro)
            {
                case (int)roomOperation.Join:
                    if (roomID == -1) roomID = ++GameSetting.roomID;
                    playerName = Room.buildName();

                    if (Room.JoinRoom(roomID, Current.SessionId, roomName, playerName))
                    {
                        BackRoom = roomID;
                        Console.WriteLine(RadioSession(BackRoom));
                    }
                    else
                    {
                        BackRoom = 0;
                    }
                    break;

                case (int)roomOperation.Leave:
                    if (Room.LeaveRoom(roomID, playerName))
                    {
                        BackRoom = 1;
                        Console.WriteLine(RadioSession(roomID));
                    }
                    else
                    {
                        BackRoom = 0;
                    }
                    break;

                case (int)roomOperation.Delete:
                    Room.DelRoom(roomID);
                    break;

            }
            return true;
        }

        private string RadioSession(int id)
        {
            string str = "Error";

            List<GameSession> sessionList = Room.getSessionsOfRoom(id);
            var parameters = new Parameters();

            parameters["roomID"] = id;
            parameters["roomName"] = 

            ActionFactory.SendAction(sessionList, 3001, parameters, (session, asyncResult) =>
            {
                str = string.Format("Action 3001 send result:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
            }, httpGet.OpCode, 0);

            return str;
        }

        private enum roomOperation : int
        {

            /// <summary>
            /// 加入房间
            /// </summary>
            Join = 1,

            /// <summary>
            /// 离开房间
            /// </summary>
            Leave = 2,

            /// <summary>
            /// 删除房间
            /// </summary>
            Delete = 3,

        }
    }
}
