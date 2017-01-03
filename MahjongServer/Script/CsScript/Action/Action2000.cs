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
        private int callback;
        private int roomID;
        private int Ro;
        private string roomName;
        private int size;

        public Action2000(HttpGet httpGet) : base(2000, httpGet)
        {
        }

        public override void BuildPacket()
        {
            if (Ro != (int)roomOperation.FindRoomData)
            {
                PushIntoStack(callback);
            }
            else
            {
                PushIntoStack(roomName);
                PushIntoStack(size);
            }
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetString("roomName", ref roomName);
            httpGet.GetInt("roomOperation", ref Ro);
            return true;
        }

        public override bool TakeAction()
        {
            switch (Ro)
            {
                case (int)roomOperation.Join:
                    if (roomID == -1) roomID = ++GameSetting.roomID;
                    if (Room.JoinRoom(roomID, Current.SessionId, roomName)) 
                    {
                        callback = roomID;
                        Console.WriteLine(RadioSession(roomID));
                    }
                    else
                    {
                        callback = 0;
                    }
                    break;

                case (int)roomOperation.Leave:
                    if (Room.LeaveRoom(roomID, Current.SessionId))
                    {
                        callback = 1;
                        Console.WriteLine(RadioSession(roomID));
                    }
                    else
                    {
                        callback = 0;
                    }
                    break;

                case (int)roomOperation.Delete:
                    Room.DelRoom(roomID);
                    break;

                case (int)roomOperation.FindRoomData:
                    roomName = Room.getName(roomID);
                    size = Room.getSize(roomID);
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

            ActionFactory.SendAction(sessionList, 2004, parameters, (session, asyncResult) =>
            {
                str = string.Format("Action 2003 send result:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
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

            /// <summary>
            /// 房间数据
            /// </summary>
            FindRoomData = 4,
        }
    }
}
