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
        private int bO;
        private int roomID;

        public Action2001(HttpGet httpGet) : base(2001, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);
            httpGet.GetInt("BrandOperation", ref bO);
            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack(brand);
        }

        public override bool TakeAction()
        {
            switch (bO)
            {
                case (int)OperationCode.GetBrand:
                    brand = GameLogic.GetBrand(roomID);
                    break;

                case (int)OperationCode.Ready:
                    GameLogic.PlayerReady(roomID, Current.SessionId);
                    break;

                case (int)OperationCode.Test:

                    break;
            }
            return true;
        }

    }

    enum OperationCode
    {
        GetBrand = 1,
        Ready = 2,
        Test = 3,
    }
}
