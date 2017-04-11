using System;

namespace MahjongServer.Script.CsScript.GameLogic
{
    using Room;
    using ZyGames.Framework.Game.Contract;

    /// <summary>
    /// 游戏逻辑
    /// </summary>
    static class GameLogic
    {
        /// <summary>
        /// 初始化游戏数据
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="BrandNum">牌数</param>
        public static bool InitGame(int id, int BrandNum)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                int r = rand.Next(0, 4);

                rand = new Random(Guid.NewGuid().GetHashCode());
                int num = rand.Next(2, 12);

                room.ModifyLocked(() =>
                {
                    room.target = r;
                    room.StartNum = num;
                });

                InitBrand(id, BrandNum);
                Console.WriteLine("游戏初始化成功");
                return true;
            }
            else
            {
                Console.WriteLine("游戏初始化失败 error:房间不存在");
                return false;
            }

            
        }

        /// <summary>
        /// 初始化牌谱
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="BrandNum">牌谱大小</param>
        public static void InitBrand(int id, int BrandNum)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                //牌谱生成
                int[] brands = new int[BrandNum];
                int z = 0;
                for (int i = 0; i < 4; i++)
                {
                    int p = 9;
                    if (i == 3) p = 7;
                    for (int j = 0; j < p; j++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            brands[z] = i * 10 + j;
                            z++;
                        }
                    }
                }

                //牌谱打乱
                for (int i = 0; i < z; i++)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    int r = rand.Next(i, z);
                    int t = brands[r];
                    brands[r] = brands[i];
                    brands[i] = t;
                }

                //牌谱存储
                room.ModifyLocked(() =>
                {
                    room.MaxBrandNum = z;
                    for (int i = 0; i < z; i++)
                        room.Brands[i] = brands[i];
                });
            }
        }

        /// <summary>
        /// 获取牌
        /// </summary>
        /// <param name = "id" > 房间ID </ param >
        /// < returns > 牌ID </ returns >
        public static int GetBrand(int id)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                int pos = room.BrandNum;

                if (pos + 1 >= room.MaxBrandNum)
                {
                    return -1;
                }

                room.ModifyLocked(() =>
                {
                    room.BrandNum++;
                });

                return room.Brands[pos + 1];
            }

            return -1;

        }

        /// <summary>
        /// 设置玩家操作等级
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerid"></param>
        /// <param name="level"></param>
        public static void SetLevel(int id, int playerid, int level)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                room.ModifyLocked(() =>
                {
                    if (level == 5)
                    {

                    }
                    room.Level[playerid] = level;
                });
            }

        }

        /// <summary>
        /// 获取玩家操作等级
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public static int GetLevel(int id, int playerid)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                int p = room.Level[playerid];

                room.ModifyLocked(() =>
                {
                    for (int i = 0; i < room.MaxPlayerLe; i++)
                    {
                        room.Level[i] = -1;
                    }
                });

                return p;

            }

            return -1;
        }

        /// <summary>
        /// 设置玩家操作牌
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerid"></param>
        /// <param name="num"></param>
        public static void SetNum(int id, int playerid, int num)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                room.ModifyLocked(() =>
                {
                    room.Num[playerid] = num;
                });
            }
        }

        /// <summary>
        /// 返回玩家操作牌
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public static int GetNum(int id, int playerid)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                int p = room.Num[playerid];

                room.ModifyLocked(() =>
                {
                    for (int i = 0; i < room.MaxPlayerLe; i++)
                    {
                        room.Num[i] = -1;
                    }
                });

                return p;

            }

            return -1;
        }

        /// <summary>
        /// 获取打牌玩家id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetPlayerId(int id)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                int target = room.target;

                int le = 0;
                int pos = (target + 1) % room.MaxPlayerLe;

                room.ModifyLocked(() =>
                {

                    for (int i = 0; i < room.MaxPlayerLe; i++)
                    {
                        target = (target + 1) % room.MaxPlayerLe;
                        if (room.Level[target] != -1)
                        {
                            if (le < room.Level[target])
                            {
                                le = room.Level[target];
                                pos = target;
                            }
                        }
                        else
                        {
                            pos = -1;
                            break;
                        }
                    }

                    if (pos != -1)
                    {
                        room.target = pos;
                    }

                });

                return pos;

            }

            return -1;
        }

        public static void GameInit(int id)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                InitGame(id, 136);
            }
        }

        public static void setWinName(int id, int winID)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                room.ModifyLocked(() =>
                {
                    room.WinName = room.players[winID].Name;
                });
            }
        }

        public static string getWinName(int id)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                return room.WinName;
            }
            else
            {
                return null;
            }
        }

    }
}
