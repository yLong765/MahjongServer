using System;
using System.Collections.Generic;
using System.Threading;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;

namespace MahjongServer.Script.CsScript.Room
{
    /// <summary>
    /// 房间操作
    /// </summary>
    static class Room
    {

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="userID">玩家ID</param>
        /// <returns>是否成功加入信息</returns>
        public static bool JoinRoom(int id, string sid, string name, string playerName)
        {
            var cache = new ShareCacheStruct<RoomCache>();
            var room = cache.FindKey(id);

            //房间为空就创建房间
            if (room == null)
            {
                room = new RoomCache(id, RoomType.FourPeople, name);
                room.players.Insert(0, new playerData() { sessionid = sid,  Name = playerName });

                if (cache.AddOrUpdate(room))
                {
                    Console.WriteLine("Create Room " + id + " Ok");
                    return true;
                }
                else
                {
                    Console.WriteLine("Create Room " + id + " Fail");
                    return false;
                }
            }
            else //房间不为空则更新房间数据
            {
                //RoomPlayers.join(id, sid);
                room.ModifyLocked(() =>
                {
                    for (int i = 0; i < room.MaxPlayerLe; i++)
                    {
                        if (room.players[i] == null)
                        {
                            room.players.Insert(i, new playerData() { sessionid = sid, Name = playerName });
                            room.size++;
                            break;
                        }
                    }
                });
                Console.WriteLine(sid + " Join Room " + id + " Ok");
                return true;
            }
        }

        /// <summary>
        /// 离开房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="sid">玩家session</param>
        public static bool LeaveRoom(int id, string playerName)
        {
            var cache = new ShareCacheStruct<RoomCache>();
            var room = cache.FindKey(id);

            if (room != null)
            {
                room.ModifyLocked(() =>
                {
                    for (int i = 0; i < room.MaxPlayerLe; i++)
                    {
                        if (room.players[i] != null &&
                            room.players[i].Name == playerName)
                        {
                            room.players.RemoveAt(i);
                            room.size--;
                            break;
                        }
                    }
                });
                Console.WriteLine("Leave Room " + id + " Ok");
                return true;
            }
            else
            {
                Console.WriteLine("Leave Room " + id + " Fail");
                return false;
            }
            
        }

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns>是否成功删除房间</returns>
        public static void DelRoom(int id)
        {
            var cache = new ShareCacheStruct<RoomCache>();
            var room = cache.FindKey(id);

            if (room == null)
            {
                Console.WriteLine("Not Found Room " + id);
            }
            else
            {
                cache.Delete(room);
                Console.WriteLine("Delete Room " + id + " OK");
            }
        }

        /// <summary>
        /// 寻找房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns>寻找到则返回房间</returns>
        public static RoomCache FindRoom(int id)
        {
            var cache = new ShareCacheStruct<RoomCache>();
            var room = cache.FindKey(id);

            if (room == null)
            {
                Console.WriteLine("Not Found Room " + id);
                return null;
            }
            else
            {
                return room;
            }
        }

        /// <summary>
        /// 获取房间玩家sessionidList
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns>sessionidList</returns>
        public static List<GameSession> getSessionsOfRoom(int id)
        {
            var room = FindRoom(id);

            if (room != null)
            {

                List<GameSession> sessions = new List<GameSession>();
                for (int i = 0; i < room.MaxPlayerLe; i++)
                {
                    if (room.players[i] != null)
                        sessions.Add(GameSession.Get(room.players[i].sessionid));
                }
                return sessions;

            }
            else
            {
                Console.WriteLine("房间不存在，获取玩家ID错误");
                return null;
            }
        }

        /// <summary>
        /// 获取房间玩家sessionid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string[] getStringSessionsOfRoom(int id)
        {
            var room = FindRoom(id);

            if (room != null)
            {

                string[] str = new string[4] { "1", "1", "1", "1" };
                for (int i = 0; i < room.MaxPlayerLe; i++)
                {
                    if (room.players[i] != null)
                        str[i] = room.players[i].sessionid;
                }
                return str;

            }
            else
            {
                Console.WriteLine("房间不存在，获取玩家ID错误");
                return null;
            }
        }

        /// <summary>
        /// 获取房间名字
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns>名字</returns>
        public static string getName(int id)
        {
            var room = FindRoom(id);
            if (room != null)
            {
                return room.name;
            }
            else
            {
                Console.WriteLine("获取房间名字错误:不存在此房间");
                return null;
            }
            
        }

        /// <summary>
        /// 获取人数
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns>人数</returns>
        public static int getSize(int id)
        {
            var room = FindRoom(id);

            if (room != null)
            {
                return room.size;
            }
            else
            {
                Console.WriteLine("获取房间人数失败：房间不存在");
                return 0;
            }
        }

        /// <summary>
        /// 获取玩家id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int getId(int id, string playerName)
        {
            var room = FindRoom(id);

            if (room != null)
            {
                for (int i = 0; i < room.MaxPlayerLe; i++)
                {
                    if (playerName.Equals(room.players[i].Name))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public static int getTarget(int id)
        {
            var room = FindRoom(id);

            if (room != null)
            {
                return room.target;
            }

            return -1;
        }

        public static int getStartName(int id)
        {
            var room = FindRoom(id);

            if (room != null)
            {
                return room.StartNum;
            }
            return -1;
        }

        private static int playerBuildID = 0;

        public static string buildName()
        {
            string Sid = "Player";
            Sid += playerBuildID;
            playerBuildID++;
            return Sid;
        }

    }
}
