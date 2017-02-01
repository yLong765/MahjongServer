using MahjongServer.Script.CsScript.GameLogic;
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
    class Action2007 : BaseStruct
    {
        private int roomID;

        public Action2007(HttpGet httpGet) : base(2007, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("roomID", ref roomID);

            return true;
        }

        public override bool TakeAction()
        {
            GameLogic.GameInit(roomID);
            return true;
        }
    }
}
