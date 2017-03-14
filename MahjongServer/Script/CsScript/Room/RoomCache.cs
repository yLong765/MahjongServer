using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Model;

namespace MahjongServer.Script.CsScript.Room
{
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Rank, "ConnData")]
    class RoomCache : ShareEntity
    {
        public RoomCache() : base(false)
        {
            Brands = new CacheList<int>();
            players = new CacheList<playerData>();
        }

        public RoomCache(int roomID,RoomType rt, string rName) : base(false)
        {
            this.roomID = roomID;
            target = 0;
            Brands = new CacheList<int>();
            players = new CacheList<playerData>();
            Level = new CacheList<int>() { -1, -1, -1, -1 };
            Num = new CacheList<int>();
            BrandNum = 0;
            MaxBrandNum = 0;
            size = 1;
            name = rName;
            if (rt == RoomType.FourPeople) MaxPlayerLe = 4;
        }

        /// <summary>
        /// 房间ID，主键
        /// </summary>
        [ProtoMember(1)]
        [EntityField(true)]
        public int roomID { get; set; }

        /// <summary>
        /// 最大玩家数
        /// </summary>
        [ProtoMember(3)]
        [EntityField]
        public int MaxPlayerLe { get; set; }

        /// <summary>
        /// 牌谱
        /// </summary>
        [ProtoMember(4)]
        [EntityField]
        public CacheList<int> Brands { get; set; }

        /// <summary>
        /// 牌谱位置
        /// </summary>
        [ProtoMember(5)]
        [EntityField]
        public int BrandNum { get; set; }

        /// <summary>
        /// 最大牌数
        /// </summary>
        [ProtoMember(6)]
        [EntityField]
        public int MaxBrandNum { get; set; }

        /// <summary>
        /// 目标，默认玩家0
        /// </summary>
        [ProtoMember(7)]
        [EntityField]
        public int target { get; set; }

        /// <summary>
        /// 房间玩家数据List
        /// </summary>
        [ProtoMember(8)]
        [EntityField]
        public CacheList<playerData> players { get; set; }

        /// <summary>
        /// 房间人数
        /// </summary>
        [ProtoMember(9)]
        [EntityField]
        public int size { get; set; }

        /// <summary>
        /// 房间名字
        /// </summary>
        [ProtoMember(10)]
        [EntityField]
        public string name { get; set; }

        [ProtoMember(11)]
        [EntityField]
        public CacheList<int> Level { get; set; }

        [ProtoMember(12)]
        [EntityField]
        public CacheList<int> Num { get; set; }

        [ProtoMember(13)]
        [EntityField]
        public int StartNum { get; set; }

    }

    [ProtoContract]
    public class playerData : EntityChangeEvent
    {
        [ProtoMember(1)]
        public string sessionid { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }
    }

    
}
