using cache;
using card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cloth;
using WebApplication1.tools;
using deployment;
/* 
 缓存层数据管理类
 */
    public static class DataCache
    {
        /// <summary>
        /// 账号与与账号对象之间的关系
        /// </summary>
        public static Dictionary<string , Account> accountMap = new Dictionary<string , Account>();

        /// <summary>
        /// 登录密钥值与用户ID的映射关系
        /// </summary>
        private static Dictionary<string, int> session = new Dictionary<string, int>();
        private static Dictionary<int, string> session1 = new Dictionary<int, string>();

        private static PlayerCache playerCache;
        private static CardCache cardCache;
        private static ClothCache clothCache;
        private static DeploymentCache deploymentCache;


        static DataCache()
        {
            playerCache = new PlayerCache();
            cardCache = new CardCache();
            clothCache = new ClothCache();
            deploymentCache = new DeploymentCache();
        }
        /// <summary>
        /// 返回当前的账号对象
        /// </summary>
        /// <param name="account">代表的是当前用户的账号</param>
        /// <returns></returns>
        public static Account getAccount(string account) 
        {
            //缓存层中不存在该账号
            if (!accountMap.ContainsKey(account)) {
                Account acc = new Account();
                if (acc.GetModel(account)) {
                    accountMap.Add(account, acc);
                }
            }
            if (accountMap.ContainsKey(account)) {
                return accountMap[account];
            }
            return null;
        }

        //注册账号
        public static bool register(string account,string password)
        {
            //判断账号是否存在
            Account acc = getAccount(account);
            if (acc != null) return false;
            acc = new Account();
            acc.account = account;
            acc.password = password;
            acc.Add();
            accountMap.Add(account, acc);
            return true;
        }
        /*
         * 账号重复登陆，后登陆的覆盖先登录的,返回密钥
         */
        public static string login(string account, string password)
        {
            //获取玩家对象（id account password）
            Account acc = getAccount(account);
            if (acc == null) return string.Empty;//账号不存在
            if (!acc.password.Equals(password)) return "-1";//密码不正确

            if(session1.ContainsKey(acc.id))
            {
                session.Remove(session1[acc.id]);
                session1.Remove(acc.id);
            }
            string key = Guid.NewGuid().ToString();//每一次登录生成新的密钥
            session.Add(key, acc.id);
            session1.Add(acc.id, key);
            return key;
        }

        public static Player getPlayer(string key)
        {
            if(session.ContainsKey(key))
            {
                return playerCache.getPlayer(session[key]);
            }
            return null;
        }

        /*
         * 用于创建玩家信息的数据管理方法，
         */
        public static Player create(string key, string name, int initCard) 
        {
            if(!session.ContainsKey(key))
            {
                return null;
            }
            Player player = playerCache.create(session[key], name, initCard);
            if(player!=null)//角色创建成功
            {
                //给予玩家 初始卡牌
                cardCache.add(player.id, initCard);
            }
            return player;
        }
        /// <summary>
        /// 获取当前用户所有的卡牌列表。
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Card[] getUserCardList(int userId, string key)
        {
            if(CheckSession(userId,key))
            {
                return cardCache.getList(userId);
            }
            return null;
        }
        public static bool CheckSession(int userId,string key)
        {
            if (DataCache.session1.ContainsKey(userId))
            {
                string value = "";
                DataCache.session1.TryGetValue(userId, out value);//尝试获取字典中的值
                if (key.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
        public static Cloth[] getUserClothList(int userId,string key)
        {
            if (CheckSession(userId, key))
            {
                return clothCache.getList(userId);
            }
            return null;
        }

        /// <summary>
        /// 玩家得到装备就添加到数据库
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <param name="typeCode"></param>
        /// <param name="gridCode"></param>
        /// <returns></returns>
        public static Cloth Add(int userId, string key, int typeCode, int gridCode)
        {
            if(CheckSession(userId,key))
            {
                if(!FinalData.Instance().hasCloth(typeCode))
                {
                    return null;//
                }
                ClothData cd = FinalData.Instance().GetCloth(typeCode);//获取对应的装备数据；
                Player cp = DataCache.getPlayer(key);//获取当前玩家的信息；
                int price =(cd.ap + cd.hp + cd.magic + cd.defend) / 5 * Math.Max(1, cd.star / 2);
                if(cp.money<price)
                {
                    return null;//金钱不够
                }
                cp.money -= price;
                cp.Update();
                return clothCache.add(userId, typeCode, gridCode);
                
            }
            return null;
        }
        /// <summary>
        /// 实现向数据库中存入装备
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public static Card AddCard(int userId, string key, int cardCode)
        {
             if(CheckSession(userId,key))//检查是否为当前玩家登录
             {
                 if(!FinalData.Instance().hasHero(cardCode))
                 {
                     return null;//缓存层中不存在该数据
                 }
                 HeroData hd = FinalData.Instance().GetCard(cardCode);//得到卡牌对象
                 Player cp = DataCache.getPlayer(key);//得到当前玩家；
             
                 //非免费阶段；
                 if (cp.gold < hd.price)
                 {
                   return null;//元宝不足；
                 }
                 cp.gold-=hd.price;
                 cp.Update();
                 return cardCache.add(userId,cardCode);
              }
              return null;    
       }
        /// <summary>
        /// 商店出售装备
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <param name="typeCode"></param>
        /// <returns></returns>
       public static int DeleteCloth(int userId, string key, int clothId)
       {
           if(!CheckSession(userId,key))return -1;
		   Cloth cloth = clothCache.getCloth ( userId, clothId );//获得Cloth的对象
		   int typeCode = cloth.type;
           if(!FinalData.Instance().hasCloth(typeCode))return -2;
           if (cloth == null) return -3;
           ClothData cd = FinalData.Instance().GetCloth(typeCode);//获取对应的装备数据；
           Player cp = DataCache.getPlayer(key);//获取当前玩家的信息；
           int price = (cd.ap + cd.hp + cd.magic + cd.defend) / 5 * Math.Max(1, cd.star / 2);
           cp.money += price;
           cp.Update();
           cloth.Delete(cloth.id);//cloth为符合要求的唯一一条数据
           clothCache.DeleteCloth(userId, clothId);
           return 1;
       }

       public static Deployment GetDeployment(int userId, string key)
       {
           if (!CheckSession(userId, key)) return null;
           return  deploymentCache.GetDeployment(userId);
       }

       internal static bool SaveDeployment(int userId, string key, int grid, int id, int type)
       {
           if (!CheckSession(userId, key)) return false;
           Player p = DataCache.getPlayer(key);//获取当前玩家
           Card c = null;
           if(type==0)
           {
               c = cardCache.getCard(userId,id);
           }
           int level = p.level;
           return deploymentCache.ChangeDeployment(userId, key,level, grid, id, type,c);
       }
    }
