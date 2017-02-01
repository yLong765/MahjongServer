using MahjongServer.Script.CsScript.GameLogic;
using MahjongServer.Script.CsScript.Room;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 逻辑接口
    /// </summary>
    class Action2001 : BaseStruct
    {
        private int brand;
        private int roomID;

        public Action2001(HttpGet httpGet) : base(2001, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack(brand);
        }

        public override bool TakeAction()
        {
            brand = GameLogic.GetBrand(roomID);
            return true;
        }

    }

}
