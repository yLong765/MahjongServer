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
    /// 登陆接口
    /// </summary>
    class Action2005 : BaseStruct
    {
        private string PlayerName;

        public Action2005(HttpGet httpGet) : base(2005, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetString("PlayerName", ref PlayerName);
            return true;
        }

        public override bool TakeAction()
        {
            return true;
        }
    }
}
