using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace cloth
{
    /// <summary>
    /// 类cloth。
    /// </summary>
    [Serializable]
    public partial class Cloth
    {
        public Cloth()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private int _type;
        private int _grid_id;
        private int _level;
        private int _defend;
        private int _magic;
        private int _ap;
        private int _hp;
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
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int grid_id
        {
            set { _grid_id = value; }
            get { return _grid_id; }
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
        public int defend
        {
            set { _defend = value; }
            get { return _defend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int magic
        {
            set { _magic = value; }
            get { return _magic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ap
        {
            set { _ap = value; }
            get { return _ap; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int hp
        {
            set { _hp = value; }
            get { return _hp; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
       public Cloth(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,user_id,type,grid_id,level,defend,magic,ap,hp ");
			strSql.Append(" FROM cloth ");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					this.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["user_id"]!=null && ds.Tables[0].Rows[0]["user_id"].ToString()!="")
				{
					this.user_id=int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["type"]!=null && ds.Tables[0].Rows[0]["type"].ToString()!="")
				{
					this.type=int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["grid_id"]!=null && ds.Tables[0].Rows[0]["grid_id"].ToString()!="")
				{
					this.grid_id=int.Parse(ds.Tables[0].Rows[0]["grid_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["level"]!=null && ds.Tables[0].Rows[0]["level"].ToString()!="")
				{
					this.level=int.Parse(ds.Tables[0].Rows[0]["level"].ToString());
				}
				if(ds.Tables[0].Rows[0]["defend"]!=null && ds.Tables[0].Rows[0]["defend"].ToString()!="")
				{
					this.defend=int.Parse(ds.Tables[0].Rows[0]["defend"].ToString());
				}
				if(ds.Tables[0].Rows[0]["magic"]!=null && ds.Tables[0].Rows[0]["magic"].ToString()!="")
				{
					this.magic=int.Parse(ds.Tables[0].Rows[0]["magic"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ap"]!=null && ds.Tables[0].Rows[0]["ap"].ToString()!="")
				{
					this.ap=int.Parse(ds.Tables[0].Rows[0]["ap"].ToString());
				}
				if(ds.Tables[0].Rows[0]["hp"]!=null && ds.Tables[0].Rows[0]["hp"].ToString()!="")
				{
					this.hp=int.Parse(ds.Tables[0].Rows[0]["hp"].ToString());
				}
			}
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
       public bool Exists(int id)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select count(1) from cloth");
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
            strSql.Append("insert into cloth (");
            strSql.Append("user_id,type,grid_id,level,defend,magic,ap,hp)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@type,@grid_id,@level,@defend,@magic,@ap,@hp)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@type", MySqlDbType.Int32,11),
					new MySqlParameter("@grid_id", MySqlDbType.Int32,11),
					new MySqlParameter("@level", MySqlDbType.Int32,11),
					new MySqlParameter("@defend", MySqlDbType.Int32,11),
					new MySqlParameter("@magic", MySqlDbType.Int32,11),
					new MySqlParameter("@ap", MySqlDbType.Int32,11),
					new MySqlParameter("@hp", MySqlDbType.Int32,11)};
            parameters[0].Value = user_id;
            parameters[1].Value = type;
            parameters[2].Value = grid_id;
            parameters[3].Value = level;
            parameters[4].Value = defend;
            parameters[5].Value = magic;
            parameters[6].Value = ap;
            parameters[7].Value = hp;

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
            strSql.Append("update cloth set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("type=@type,");
            strSql.Append("grid_id=@grid_id,");
            strSql.Append("level=@level,");
            strSql.Append("defend=@defend,");
            strSql.Append("magic=@magic,");
            strSql.Append("ap=@ap,");
            strSql.Append("hp=@hp");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@type", MySqlDbType.Int32,11),
					new MySqlParameter("@grid_id", MySqlDbType.Int32,11),
					new MySqlParameter("@level", MySqlDbType.Int32,11),
					new MySqlParameter("@defend", MySqlDbType.Int32,11),
					new MySqlParameter("@magic", MySqlDbType.Int32,11),
					new MySqlParameter("@ap", MySqlDbType.Int32,11),
					new MySqlParameter("@hp", MySqlDbType.Int32,11),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = user_id;
            parameters[1].Value = type;
            parameters[2].Value = grid_id;
            parameters[3].Value = level;
            parameters[4].Value = defend;
            parameters[5].Value = magic;
            parameters[6].Value = ap;
            parameters[7].Value = hp;
            parameters[8].Value = id;

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
            strSql.Append("delete from cloth ");
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
            strSql.Append("select id,user_id,type,grid_id,level,defend,magic,ap,hp ");
            strSql.Append(" FROM cloth ");
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
                if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    this.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["grid_id"] != null && ds.Tables[0].Rows[0]["grid_id"].ToString() != "")
                {
                    this.grid_id = int.Parse(ds.Tables[0].Rows[0]["grid_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["level"] != null && ds.Tables[0].Rows[0]["level"].ToString() != "")
                {
                    this.level = int.Parse(ds.Tables[0].Rows[0]["level"].ToString());
                }
                if (ds.Tables[0].Rows[0]["defend"] != null && ds.Tables[0].Rows[0]["defend"].ToString() != "")
                {
                    this.defend = int.Parse(ds.Tables[0].Rows[0]["defend"].ToString());
                }
                if (ds.Tables[0].Rows[0]["magic"] != null && ds.Tables[0].Rows[0]["magic"].ToString() != "")
                {
                    this.magic = int.Parse(ds.Tables[0].Rows[0]["magic"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ap"] != null && ds.Tables[0].Rows[0]["ap"].ToString() != "")
                {
                    this.ap = int.Parse(ds.Tables[0].Rows[0]["ap"].ToString());
                }
                if (ds.Tables[0].Rows[0]["hp"] != null && ds.Tables[0].Rows[0]["hp"].ToString() != "")
                {
                    this.hp = int.Parse(ds.Tables[0].Rows[0]["hp"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM cloth ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        public static Cloth[] GetList(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM cloth ");
            strSql.Append(" where user_id=@userId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@userId", MySqlDbType.Int32)};
            parameters[0].Value = userId;
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            Cloth[] cloths = new Cloth[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            { 
                cloths[i]=new Cloth();
                if (ds.Tables[0].Rows[i]["id"] != null && ds.Tables[0].Rows[i]["id"].ToString() != "")
                {
                    cloths[i].id = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                }
                if (ds.Tables[0].Rows[i]["user_id"] != null && ds.Tables[0].Rows[i]["user_id"].ToString() != "")
                {
                    cloths[i].user_id = int.Parse(ds.Tables[0].Rows[i]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[i]["type"] != null && ds.Tables[0].Rows[i]["type"].ToString() != "")
                {
                    cloths[i].type = int.Parse(ds.Tables[0].Rows[i]["type"].ToString());
                }
                if (ds.Tables[0].Rows[i]["grid_id"] != null && ds.Tables[0].Rows[i]["grid_id"].ToString() != "")
                {
                    cloths[i].grid_id = int.Parse(ds.Tables[0].Rows[i]["grid_id"].ToString());
                }
                //new add
                if (ds.Tables[0].Rows[i]["level"] != null && ds.Tables[0].Rows[i]["level"].ToString() != "")
                {
                    cloths[i].level = int.Parse(ds.Tables[0].Rows[i]["level"].ToString());
                }
                if (ds.Tables[0].Rows[i]["defend"] != null && ds.Tables[0].Rows[i]["defend"].ToString() != "")
                {
                    cloths[i].defend = int.Parse(ds.Tables[0].Rows[i]["defend"].ToString());
                }
                if (ds.Tables[0].Rows[i]["magic"] != null && ds.Tables[0].Rows[i]["magic"].ToString() != "")
                {
                    cloths[i].magic = int.Parse(ds.Tables[0].Rows[i]["magic"].ToString());
                }
                if (ds.Tables[0].Rows[i]["ap"] != null && ds.Tables[0].Rows[i]["ap"].ToString() != "")
                {
                    cloths[i].ap = int.Parse(ds.Tables[0].Rows[i]["ap"].ToString());
                }
                if (ds.Tables[0].Rows[i]["hp"] != null && ds.Tables[0].Rows[i]["hp"].ToString() != "")
                {
                    cloths[i].hp = int.Parse(ds.Tables[0].Rows[i]["hp"].ToString());
                }
            }
            return cloths;
        }

        #endregion  Method
    }
}

