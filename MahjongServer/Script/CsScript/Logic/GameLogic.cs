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
                int r = rand.Next(0, 5);

                room.ModifyLocked(() =>
                {
                    room.target = r;
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
        /// 玩家准备
        /// </summary>
        /// <param name = "id" > 房间ID </ param >
        /// < param name="UserID">用户ID</param>
        public static void PlayerReady(int id, string session)
        {
            var room = Room.FindRoom(id);

            if (room != null)
            {
                
                if (Room.PlayerReady(id, session))
                {
                    Console.WriteLine("游戏开始初始化");
                    InitGame(id, 136);
                }

            }
        }

    }
}
