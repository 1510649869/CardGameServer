using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace card
{
    /// <summary>
    /// 类card。
    /// </summary>
    [Serializable]
    public partial class Card
    {
        public Card()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private int _gd_code;
        private int _level;
        private int _exp;
        private int _additional;
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
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int gd_code
        {
            set { _gd_code = value; }
            get { return _gd_code; }
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
        public int additional
        {
            set { _additional = value; }
            get { return _additional; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Card(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,gd_code,level,exp,additional ");
            strSql.Append(" FROM card ");
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
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    this.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gd_code"] != null && ds.Tables[0].Rows[0]["gd_code"].ToString() != "")
                {
                    this.gd_code = int.Parse(ds.Tables[0].Rows[0]["gd_code"].ToString());
                }
                if (ds.Tables[0].Rows[0]["level"] != null && ds.Tables[0].Rows[0]["level"].ToString() != "")
                {
                    this.level = int.Parse(ds.Tables[0].Rows[0]["level"].ToString());
                }
                if (ds.Tables[0].Rows[0]["exp"] != null && ds.Tables[0].Rows[0]["exp"].ToString() != "")
                {
                    this.exp = int.Parse(ds.Tables[0].Rows[0]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["additional"] != null && ds.Tables[0].Rows[0]["additional"].ToString() != "")
                {
                    this.additional = int.Parse(ds.Tables[0].Rows[0]["additional"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from card");
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
            strSql.Append("insert into card (");
            strSql.Append("user_id,gd_code,level,exp,additional)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@gd_code,@level,@exp,@additional)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@gd_code", MySqlDbType.Int32,11),
					new MySqlParameter("@level", MySqlDbType.Int32,11),
					new MySqlParameter("@exp", MySqlDbType.Int32,11),
					new MySqlParameter("@additional", MySqlDbType.Int32,11)};
            parameters[0].Value = user_id;
            parameters[1].Value = gd_code;
            parameters[2].Value = level;
            parameters[3].Value = exp;
            parameters[4].Value = additional;

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
            strSql.Append("update card set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("gd_code=@gd_code,");
            strSql.Append("level=@level,");
            strSql.Append("exp=@exp,");
            strSql.Append("additional=@additional");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@gd_code", MySqlDbType.Int32,11),
					new MySqlParameter("@level", MySqlDbType.Int32,11),
					new MySqlParameter("@exp", MySqlDbType.Int32,11),
					new MySqlParameter("@additional", MySqlDbType.Int32,11),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = user_id;
            parameters[1].Value = gd_code;
            parameters[2].Value = level;
            parameters[3].Value = exp;
            parameters[4].Value = additional;
            parameters[5].Value = id;

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
            strSql.Append("delete from card ");
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
        public void GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,gd_code,level,exp,additional ");
            strSql.Append(" FROM card ");
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
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    this.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gd_code"] != null && ds.Tables[0].Rows[0]["gd_code"].ToString() != "")
                {
                    this.gd_code = int.Parse(ds.Tables[0].Rows[0]["gd_code"].ToString());
                }
                if (ds.Tables[0].Rows[0]["level"] != null && ds.Tables[0].Rows[0]["level"].ToString() != "")
                {
                    this.level = int.Parse(ds.Tables[0].Rows[0]["level"].ToString());
                }
                if (ds.Tables[0].Rows[0]["exp"] != null && ds.Tables[0].Rows[0]["exp"].ToString() != "")
                {
                    this.exp = int.Parse(ds.Tables[0].Rows[0]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["additional"] != null && ds.Tables[0].Rows[0]["additional"].ToString() != "")
                {
                    this.additional = int.Parse(ds.Tables[0].Rows[0]["additional"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static Card[] GetList(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM card ");
            strSql.Append(" where user_id=@userId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@userId", MySqlDbType.Int32)};
            parameters[0].Value = userId;
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(),parameters);
            Card[] cards = new Card[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cards[i] = new Card();
                if (ds.Tables[0].Rows[i]["id"] != null && ds.Tables[0].Rows[i]["id"].ToString() != "")
                {
                    cards[i].id = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                }
                if (ds.Tables[0].Rows[i]["user_id"] != null && ds.Tables[0].Rows[i]["user_id"].ToString() != "")
                {
                    cards[i].user_id = int.Parse(ds.Tables[0].Rows[i]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[i]["gd_code"] != null && ds.Tables[0].Rows[i]["gd_code"].ToString() != "")
                {
                    cards[i].gd_code = int.Parse(ds.Tables[0].Rows[i]["gd_code"].ToString());
                }
                if (ds.Tables[0].Rows[i]["level"] != null && ds.Tables[0].Rows[i]["level"].ToString() != "")
                {
                    cards[i].level = int.Parse(ds.Tables[0].Rows[i]["level"].ToString());
                }
                if (ds.Tables[0].Rows[i]["exp"] != null && ds.Tables[0].Rows[i]["exp"].ToString() != "")
                {
                    cards[i].exp = int.Parse(ds.Tables[0].Rows[i]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[i]["additional"] != null && ds.Tables[0].Rows[i]["additional"].ToString() != "")
                {
                    cards[i].additional = int.Parse(ds.Tables[0].Rows[i]["additional"].ToString());
                }
            }
            return cards;
        }

        #endregion  Method
    }
}

