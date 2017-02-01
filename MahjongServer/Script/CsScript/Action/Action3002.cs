using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    class Action3002 : BaseStruct
    {
        private int num;
        private int level;
        private int playerID;

        public Action3002(HttpGet httpGet) : base(3002, httpGet)
        {
        }

        public override void BuildPacket()
        {
            PushIntoStack(num);
            PushIntoStack(level);
            PushIntoStack(playerID);
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("num", ref num);
            httpGet.GetInt("level", ref level);
            httpGet.GetInt("playerID", ref playerID);
            return true;
        }

        public override bool TakeAction()
        {
            return true;
        }
    }
}
