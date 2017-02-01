using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace MahjongServer.Script.CsScript.Action
{
    class Action3003 : BaseStruct
    {
        private int GameStart;

        public Action3003(HttpGet httpGet) : base(3003, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("GameStart", ref GameStart);

            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack(GameStart);
        }

        public override bool TakeAction()
        {
            return true;
        }
    }
}
