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
    /// 广播牌
    /// </summary>
    class Action2003 : BaseStruct
    {
        private int brand;

        public Action2003(HttpGet httpGet) : base(2003, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetInt("brand", ref brand);
            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack(brand);
        }

        public override bool TakeAction()
        {
            return false;
        }
    }
}
