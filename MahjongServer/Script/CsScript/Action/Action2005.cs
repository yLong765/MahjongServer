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
        private string userName;
        private string passWord;

        public Action2005(HttpGet httpGet) : base(2005, httpGet)
        {
        }

        public override bool GetUrlElement()
        {
            httpGet.GetString("UserName", ref userName);
            httpGet.GetString("PassWord", ref passWord);

            return true;
        }

        public override void BuildPacket()
        {
            if (userName.Equals("yang") && passWord.Equals("yang"))
            {
                PushIntoStack(1);
            }
            else
            {
                PushIntoStack(1);
            }
        }

        public override bool TakeAction()
        {
            return true;
        }
    }
}
