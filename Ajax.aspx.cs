using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
public partial class Ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string w=YYCMS.Request.GetFormString("w");
            switch (w)
            {
                case "GetPageContent": GetPageContent(); break;
            }
            
        }
    }
    /// <summary>
    /// 获取记录
    /// </summary>
    private void GetPageContent()
    {
        int recordcount = 0;
        int _pageindex = YYCMS.Request.GetFormInt("_pageindex", 1);//当前页
        int _PageSize = YYCMS.Request.GetFormInt("_PageSize", 10);//每页条数
        int _PageCount = 0;//总共页数 
        if (_pageindex < 1)
        {
            _pageindex = 1;
        }
        YYCMS.students studentsDal = new YYCMS.students();
        DataSet ds = studentsDal.Pager(_pageindex,10, " 1=1 ", out recordcount);
        //下面计算总共页数
        if (recordcount % _PageSize == 0)
        {
            _PageCount = recordcount / _PageSize;
        }
        else
        {
            _PageCount = recordcount / _PageSize + 1;
        }

        StringBuilder sb = new StringBuilder();
        sb.Remove(0,sb.Length);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", dr["id"].ToString(), dr["StuNum"].ToString(), dr["name"].ToString(), dr["age"].ToString(), dr["Tel"].ToString(), dr["ctime"].ToString());//记录列表
        }
        sb.Append("|");
        sb.Append(_pageindex);//当前页
        sb.Append("|");
        sb.Append(_PageCount);//总页数
        Response.Write(sb.ToString());
    }
   
}
