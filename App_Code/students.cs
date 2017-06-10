using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;
namespace YYCMS
{
    /// <summary>
    /// 类students。
    /// </summary>
    public class students
    {
        public students()
        { }
        #region Model
        private int _id;
        private string _name;
        private int? _age;
        private string _stunum;
        private string _tel;
        private DateTime? _ctime;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        public int? age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StuNum
        {
            set { _stunum = value; }
            get { return _stunum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ctime
        {
            set { _ctime = value; }
            get { return _ctime; }
        }
        #endregion Model


        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from students");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into students(");
            strSql.Append("name,age,StuNum,Tel,ctime)");
            strSql.Append(" values (");
            strSql.Append("@name,@age,@StuNum,@Tel,@ctime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@age", SqlDbType.Int,4),
					new SqlParameter("@StuNum", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@ctime", SqlDbType.DateTime)};
            parameters[0].Value = name;
            parameters[1].Value = age;
            parameters[2].Value = StuNum;
            parameters[3].Value = Tel;
            parameters[4].Value = ctime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update students set ");
            strSql.Append("name=@name,");
            strSql.Append("age=@age,");
            strSql.Append("StuNum=@StuNum,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("ctime=@ctime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@age", SqlDbType.Int,4),
					new SqlParameter("@StuNum", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@ctime", SqlDbType.DateTime)};
            parameters[0].Value = ID;
            parameters[1].Value = name;
            parameters[2].Value = age;
            parameters[3].Value = StuNum;
            parameters[4].Value = Tel;
            parameters[5].Value = ctime;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from students ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM students ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex">当前第几页</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="Recordcount">记录总条数</param>
        /// <returns></returns>
        public DataSet Pager(int PageIndex, int PageSize, string strWhere, out int Recordcount)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                strWhere = " 1=1 ";
            }
            string strSql = string.Format("select top {0} * from students where id not in (select top {1} id from students where {2} order by id desc) and ({2}) order by id desc", PageSize, PageSize * (PageIndex - 1), strWhere);
            DataSet ds = DbHelperSQL.Query(strSql);

            string strSql2 = string.Format("select id from students where {0}", strWhere);
            DataSet dsCount = DbHelperSQL.Query(strSql2);
            try
            {
                Recordcount = dsCount.Tables[0].Rows.Count;
            }
            catch
            {
                Recordcount = 0;
            }
            return ds;
        }

        #endregion  成员方法
    }
}


