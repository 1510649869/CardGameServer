using deployment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using card;
using WebApplication1.src_dao;

namespace cache
{
    public class DeploymentCache
    {
        private Dictionary<int, Deployment> userDeployments = new Dictionary<int, Deployment>();
        public Deployment GetDeployment(int userId)
        {
            InitCache(userId);
            return userDeployments[userId];
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userId"></param>
        public void InitCache(int userId)
        {
            if(!userDeployments.ContainsKey(userId))
            {
                Deployment d = new Deployment();
                d = d.GetModel(userId);
                if(d==null)
                {
                    //玩家第一次登陆，获得的出事数据
                    d = new Deployment();
                    d.user_id = userId;
                    d.grid1 = new DeploymentGridModel();
                    d.grid2 = new DeploymentGridModel();
                    d.grid3 = new DeploymentGridModel();
                    d.grid4 = new DeploymentGridModel();
                    d.grid5 = new DeploymentGridModel();
                    d.Add();
                }
                userDeployments.Add(userId, d);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <param name="level">玩家等级</param>
        /// <param name="grid">是哪一个英雄卡牌</param>
        /// <param name="id">卡牌或装备在数据中的id</param>
        /// <param name="type">类型0 卡牌 1-6装备</param>
        /// <returns></returns>
        public bool ChangeDeployment(int userId, string key,int level, int grid, int id, int type,Card c=null)
        {
            Deployment d = new Deployment();
            DeploymentGridModel model;
            switch(grid)
            {
                case 1:
                    model=d.grid1;
                    break;
                case 2:
                    if (level < 20) return false;
                    model = d.grid2;
                    break;
                case 3:
                    if (level < 30) return false;
                    model = d.grid3;
                    break;
                case 4:
                    if (level < 40) return false;
                    model = d.grid4;
                    break;
                case 5:
                    if (level < 45) return false;
                    model = d.grid5;
                    break;
                default:
                    return false;
            }
            switch(type)
            {
                case 0:
                    if (c == null) return false;
                    if (d.grid1.cardId == id || d.grid2.cardId == id || d.grid3.cardId == id || d.grid4.cardId == id || d.grid5.cardId == id)
                        return false;
                    model.cardId = id;
                    break;
                case 1:
                    model.swordId = id;
                    break;
                case 2:
                    model.hatId = id;
                    break;
                case 3:
                    model.clothId=id;
                    break;
                case 4:
                    model.bookId = id;
                    break;
                case 5:
                    model.shooeId = id;
                    break;
                case 6:
                    model.horseId = id;
                    break;
            }
            d.Update();
            userDeployments[userId] = d;
            return true;
        }
    }
}