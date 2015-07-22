using System;
using card;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cache
{
    //玩家缓存层数据管理类
    public class PlayerCache
    {
        private  Dictionary<int, Player> playerMap = new Dictionary<int, Player>();//保存角色信息的字典

        //获取角色信息，通过玩家账号id；
        public Player getPlayer(int accountId)
        {
            if (!playerMap.ContainsKey(accountId))
            {
                Player player = new Player();
                if(player.GetModel(accountId))//如果数据库中有改玩家的角色~~~
                {
                    playerMap.Add(accountId, player);//将从数据库中获取的玩家信息添加到数据字典的缓存层！！；
                }
            }
            if(playerMap.ContainsKey(accountId))
            {
				playerMap[accountId].GetModel ( accountId );
                return playerMap[accountId];
            }
            return null;
        }

        //创建角色
        public Player create(int accountId, string name, int initCard)
        {
            Player player = getPlayer(accountId);
            if (player != null) return null;//存在玩家信息（有角色）；
            player = new Player();
            player.account_id = accountId;
            player.name = name;
            player.level = 1; 
            player.Add();//添加进数据库
            playerMap.Add(accountId, player);
            return player;
        }
    }
}