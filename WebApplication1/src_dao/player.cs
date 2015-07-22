using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace card
{
    /// <summary>
    /// 类Player。
    /// </summary>
    [Serializable]
    public partial class Player
    {
        public Player()
        { }
        #region Model
        private int _id;
        private int _account_id;
        private string _name;
        private int _level;
        private int _exp;
        private int _money;
        private int _gold;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int account_id
        {
            set { _account_id = value; }
            get { return _account_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int exp
        {
            set { _exp = value; }
            get { return _exp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int gold
        {
            set { _gold = value; }
            get { return _gold; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Player(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,account_id,name,level,exp,money,gold ");
            strSql.Append(" FROM Player");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
            parameters[0].Value = id;

            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    this.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["account_id"] != null && ds.Tables[0].Rows[0]["account_id"].ToString() != "")
                {
                    this.account_id = int.Parse(ds.Tables[0].Rows[0]["account_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null)
                {
                    this.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["level"] != null && ds.Tables[0].Rows[0]["level"].ToString() != "")
                {
                    this.level = int.Parse(ds.Tables[0].Rows[0]["level"].ToString());
                }
                if (ds.Tables[0].Rows[0]["exp"] != null && ds.Tables[0].Rows[0]["exp"].ToString() != "")
                {
                    this.exp = int.Parse(ds.Tables[0].Rows[0]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["money"] != null && ds.Tables[0].Rows[0]["money"].ToString() != "")
                {
                    this.money = int.Parse(ds.Tables[0].Rows[0]["money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gold"] != null && ds.Tables[0].Rows[0]["gold"].ToString() != "")
                {
                    this.gold = int.Parse(ds.Tables[0].Rows[0]["gold"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Player");
            strSql.Append(" where id=@id ");

            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Player(");
            strSql.Append("account_id,name,level,exp,money,gold)");
            strSql.Append(" values (");
            strSql.Append("@account_id,@name,@level,@exp,@money,@gold)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@account_id", MySqlDbType.Int32,11),
					new MySqlParameter("@name", MySqlDbType.VarChar,255),
					new MySqlParameter("@level", MySqlDbType.Int32,11),
					new MySqlParameter("@exp", MySqlDbType.Int32,11),
					new MySqlParameter("@money", MySqlDbType.Int32,11),
					new MySqlParameter("@gold", MySqlDbType.Int32,11)};
            parameters[0].Value = account_id;
            parameters[1].Value = name;
            parameters[2].Value = level;
            parameters[3].Value = exp;
            parameters[4].Value = money;
            parameters[5].Value = gold;
            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            getkey();
        }
        void getkey()
        {
            DataSet ds = DbHelperMySQL.Query("select @@IDENTITY as id");
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
            {
                this.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            }
            //getkey();
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Player set ");
            strSql.Append("account_id=@account_id,");
            strSql.Append("name=@name,");
            strSql.Append("level=@level,");
            strSql.Append("exp=@exp,");
            strSql.Append("money=@money,");
            strSql.Append("gold=@gold");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@account_id", MySqlDbType.Int32,11),
					new MySqlParameter("@name", MySqlDbType.VarChar,255),
					new MySqlParameter("@level", MySqlDbType.Int32,11),
					new MySqlParameter("@exp", MySqlDbType.Int32,11),
					new MySqlParameter("@money", MySqlDbType.Int32,11),
					new MySqlParameter("@gold", MySqlDbType.Int32,11),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = account_id;
            parameters[1].Value = name;
            parameters[2].Value = level;
            parameters[3].Value = exp;
            parameters[4].Value = money;
            parameters[5].Value = gold;
            parameters[6].Value = id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Player");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
            parameters[0].Value = id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public bool GetModel(int account_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,account_id,name,level,exp,money,gold ");
            strSql.Append(" FROM Player");
            strSql.Append(" where account_id=@account_id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@account_id", MySqlDbType.Int32)};
            parameters[0].Value = account_id;

            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    this.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["account_id"] != null && ds.Tables[0].Rows[0]["account_id"].ToString() != "")
                {
                    this.account_id = int.Parse(ds.Tables[0].Rows[0]["account_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null)
                {
                    this.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["level"] != null && ds.Tables[0].Rows[0]["level"].ToString() != "")
                {
                    this.level = int.Parse(ds.Tables[0].Rows[0]["level"].ToString());
                }
                if (ds.Tables[0].Rows[0]["exp"] != null && ds.Tables[0].Rows[0]["exp"].ToString() != "")
                {
                    this.exp = int.Parse(ds.Tables[0].Rows[0]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["money"] != null && ds.Tables[0].Rows[0]["money"].ToString() != "")
                {
                    this.money = int.Parse(ds.Tables[0].Rows[0]["money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gold"] != null && ds.Tables[0].Rows[0]["gold"].ToString() != "")
                {
                    this.gold = int.Parse(ds.Tables[0].Rows[0]["gold"].ToString());
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Player");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

