using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cloth;
using WebApplication1.tools;
public class ClothCache
{
    /// <summary>
    /// 第一层字典映射关系为 用户ID，和用户下所有的装备数据
    /// 第二层字典映射关系  装备数据自身的映射关系,int为id
    /// </summary>
    private Dictionary<int, Dictionary<int, Cloth>> userCloth = new Dictionary<int, Dictionary<int, Cloth>>();

    /// <summary>
    /// 向数据库中添加装备数据
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="clothType"></param>
    /// <param name="gridId"></param>
    public Cloth add(int userId,int clothType,int gridId)
    {
        Cloth cloth = new Cloth();
        ClothData data = FinalData.Instance().GetCloth(clothType);
        cloth.user_id = userId;
        cloth.type = clothType;
        cloth.grid_id = gridId;
        cloth.defend = data.defend;
        cloth.magic = data.magic;
        cloth.hp = data.hp;
        cloth.ap = data.ap;
        cloth.Add();
		userCloth[userId].Add ( cloth.id, cloth );
        return cloth;
    }
    public Cloth[] getList(int userID)
    {
        initCache(userID);
        return userCloth[userID].Values.ToArray<Cloth>();
    }
    private void initCache(int userId)
    {
        //我这里是向缓存层里添加的是user_id对应的信息
        if (userCloth.ContainsKey(userId)) return;
        Cloth[] cloths = Cloth.GetList(userId);///从数据库中得到列表
        Dictionary<int, Cloth> map = new Dictionary<int, Cloth>();
        foreach(Cloth cloth in cloths)
        {
            map.Add(cloth.id, cloth);
        }
        userCloth.Add(userId, map);
    }

   public Cloth getCloth(int userId, int clothId)
   {
       initCache(userId);
	   return userCloth[userId][clothId];//返回cloth对象
   }

   public void DeleteCloth(int userId, int clothId)
   {
       userCloth[userId].Remove(clothId);
   }
}
