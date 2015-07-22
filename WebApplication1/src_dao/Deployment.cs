using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;
using WebApplication1.src_dao;//Please add references
using WebApplication1.src_dao.SeriallizeTools;
namespace deployment
{
    /// <summary>
    /// 类Deployment。
    /// </summary>
    [Serializable]
    public partial class Deployment
    {
        public Deployment()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private DeploymentGridModel _grid1;
        private DeploymentGridModel _grid2;
        private DeploymentGridModel _grid3;
        private DeploymentGridModel _grid4;
        private DeploymentGridModel _grid5;
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
        public DeploymentGridModel grid1
        {
            set { _grid1 = value; }
            get { return _grid1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DeploymentGridModel grid2
        {
            set { _grid2 = value; }
            get { return _grid2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DeploymentGridModel grid3
        {
            set { _grid3 = value; }
            get { return _grid3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DeploymentGridModel grid4
        {
            set { _grid4 = value; }
            get { return _grid4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DeploymentGridModel grid5
        {
            set { _grid5 = value; }
            get { return _grid5; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Deployment(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,grid1,grid2,grid3,grid4,grid5 ");
            strSql.Append(" FROM deployment ");
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
                if (ds.Tables[0].Rows[0]["grid1"] != null && ds.Tables[0].Rows[0]["grid1"].ToString() != "")
                {
                    this.grid1 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid1"]);
                }
                if (ds.Tables[0].Rows[0]["grid2"] != null && ds.Tables[0].Rows[0]["grid2"].ToString() != "")
                {
                    this.grid2 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid2"]);
                }
                if (ds.Tables[0].Rows[0]["grid3"] != null && ds.Tables[0].Rows[0]["grid3"].ToString() != "")
                {
                    this.grid3 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid3"]);
                }
                if (ds.Tables[0].Rows[0]["grid4"] != null && ds.Tables[0].Rows[0]["grid4"].ToString() != "")
                {
                    this.grid4 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid4"]);
                }
                if (ds.Tables[0].Rows[0]["grid5"] != null && ds.Tables[0].Rows[0]["grid5"].ToString() != "")
                {
                    this.grid5 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid5"]);
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from deployment");
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
            strSql.Append("insert into deployment (");
            strSql.Append("user_id,grid1,grid2,grid3,grid4,grid5)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@grid1,@grid2,@grid3,@grid4,@grid5)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@grid1", MySqlDbType.VarBinary),
					new MySqlParameter("@grid2", MySqlDbType.VarBinary),
					new MySqlParameter("@grid3", MySqlDbType.VarBinary),
					new MySqlParameter("@grid4", MySqlDbType.VarBinary),
					new MySqlParameter("@grid5", MySqlDbType.VarBinary)};
            parameters[0].Value = user_id;
            parameters[1].Value = SeriallizeUtil.SeriallizeBinary(grid1);
            parameters[2].Value = SeriallizeUtil.SeriallizeBinary(grid2);
            parameters[3].Value = SeriallizeUtil.SeriallizeBinary(grid3);
            parameters[4].Value = SeriallizeUtil.SeriallizeBinary(grid4);
            parameters[5].Value = SeriallizeUtil.SeriallizeBinary(grid5);
            getkey();
            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }
        void getkey()
        {
            DataSet ds = DbHelperMySQL.Query("select @@IDENTITY as id");
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
            {
                this.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update deployment set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("grid1=@grid1,");
            strSql.Append("grid2=@grid2,");
            strSql.Append("grid3=@grid3,");
            strSql.Append("grid4=@grid4,");
            strSql.Append("grid5=@grid5");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@grid1", MySqlDbType.VarBinary),
					new MySqlParameter("@grid2", MySqlDbType.VarBinary),
					new MySqlParameter("@grid3", MySqlDbType.VarBinary),
					new MySqlParameter("@grid4", MySqlDbType.VarBinary),
					new MySqlParameter("@grid5", MySqlDbType.VarBinary),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = user_id;
            parameters[1].Value = SeriallizeUtil.SeriallizeBinary(grid1);
            parameters[2].Value = SeriallizeUtil.SeriallizeBinary(grid2);
            parameters[3].Value = SeriallizeUtil.SeriallizeBinary(grid3);
            parameters[4].Value = SeriallizeUtil.SeriallizeBinary(grid4);
            parameters[5].Value = SeriallizeUtil.SeriallizeBinary(grid5);
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
            strSql.Append("delete from deployment ");
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
        public Deployment GetModel(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,grid1,grid2,grid3,grid4,grid5 ");
            strSql.Append(" FROM deployment ");
            strSql.Append(" where user_id=@user_id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32)};
            parameters[0].Value = userId;

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
                 if (ds.Tables[0].Rows[0]["grid1"] != null && ds.Tables[0].Rows[0]["grid1"].ToString() != "")
                {
                    this.grid1 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid1"]);
                }
                if (ds.Tables[0].Rows[0]["grid2"] != null && ds.Tables[0].Rows[0]["grid2"].ToString() != "")
                {
                    this.grid2 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid2"]);
                }
                if (ds.Tables[0].Rows[0]["grid3"] != null && ds.Tables[0].Rows[0]["grid3"].ToString() != "")
                {
                    this.grid3 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid3"]);
                }
                if (ds.Tables[0].Rows[0]["grid4"] != null && ds.Tables[0].Rows[0]["grid4"].ToString() != "")
                {
                    this.grid4 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid4"]);
                }
                if (ds.Tables[0].Rows[0]["grid5"] != null && ds.Tables[0].Rows[0]["grid5"].ToString() != "")
                {
                    this.grid5 = SeriallizeUtil.DeseriallizeBinary<DeploymentGridModel>((byte[])ds.Tables[0].Rows[0]["grid5"]);
                }
                return this;
            }
            return null;
        }
        #endregion  Method
    }
}

