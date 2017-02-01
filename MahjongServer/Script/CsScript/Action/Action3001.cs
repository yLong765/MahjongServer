using MahjongServer.Script.CsScript.Room;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 广播房间信息
    /// </summary>
    class Action3001 : BaseStruct
    {
        private int roomID;
        private string[] sessions;
        private string roomName;

        public Action3001(HttpGet httpGet) : base(3001, httpGet)
        {
        }

        public override void BuildPacket()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(sessions[i]);
                PushIntoStack(sessions[i]);
            }

            PushIntoStack(roomName);
 
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            return true;
        }

        public override bool TakeAction()
        {
            sessions = Room.getStringSessionsOfRoom(roomID);
            roomName = Room.getName(roomID);
            return true;
        }
    }
}
