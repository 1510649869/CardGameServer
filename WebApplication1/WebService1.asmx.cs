using card;
using cloth;
using deployment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplication1.tools;

namespace WebApplication1
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]0
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod (Description ="注册 需要传入参数 账号 密码")]
        public bool register(string account,string password) {
            return DataCache.register(account, password);
        }

        [WebMethod(Description = "登陆 需要传入参数 账号 密码，返回本次登陆密钥！")]
        public string login(string account, string password)
        {
            return DataCache.login(account,password);
        }
        [WebMethod(Description = "获取玩家信息，需要传入参数 玩家密钥")]
        public Player getPlayer(string key)
        {
            return DataCache.getPlayer(key);
        }
        [WebMethod(Description = "创建角色，需要传入参数 玩家密钥,名称，初始卡片")]
        public Player createPlayer(string key,string name,int initCard)
        {
            if(!getData().hasHero(initCard))
            {
                return null;
            }
            return DataCache.create(key, name, initCard);
        }
        [WebMethod(Description="获取英雄卡牌列表")]
        public Card[] getHeroList(int userId,string key)
        {
            return DataCache.getUserCardList(userId, key);
        }
        [WebMethod(Description="获取装备列表")]
        public Cloth[] getClothList(int userId,string key)
        {
            return DataCache.getUserClothList(userId, key);
        }
        [WebMethod(Description = "添加装备")]
        public Cloth AddCloth(int userId,string key,int typeCode,int gridCode)
        {
            getData();
            return DataCache.Add(userId, key, typeCode, gridCode);
        }
        [WebMethod(Description = "出售装备")]
        public int DeleteCloth(int userId,string key,int clothId)
        {
            getData();
			return DataCache.DeleteCloth ( userId, key, clothId );
        }
        [WebMethod(Description="添加卡牌")]
        public Card AddCard(int userId,string key,int cardCode)
        {
            getData();
            return DataCache.AddCard(userId, key, cardCode);
        }
        [WebMethod(Description="获取布阵信息")]
        public Deployment GetDeployment(int userId,string key)
        {
            return DataCache.GetDeployment(userId, key);
        }

         [WebMethod(Description = "更改布阵信息,传入参数用户ID,密钥,布阵ID,")]
        public bool SaveDeployment(int userId,string key,int grid,int id,int type)
        {
            return DataCache.SaveDeployment(userId, key, grid, id, type);
        }
        public FinalData getData()
        {
            return FinalData.Instance(Server.MapPath(""));
        }
    }
}


