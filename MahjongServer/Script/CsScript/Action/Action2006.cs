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
    /// <summary>
    /// 返回房间数据
    /// </summary>
    class Action2006 : BaseStruct
    {
        private int roomID;
        private string Name;
        private int Size;

        public Action2006(HttpGet httpGet) : base(2006, httpGet)
        {
        }
        public override void BuildPacket()
        {
            PushIntoStack(Name);
            PushIntoStack(Size);
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            return true;
        }

        public override bool TakeAction()
        {
            Name = Room.getName(roomID);
            Size = Room.getSize(roomID);
            return true;
        }
    }
}
