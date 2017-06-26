using MySql.Data.MySqlClient;
using SmartFramework4v2.Data.MySql;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// News 的摘要说明
/// </summary>

[CSClass("News")]
public class News
{
    public News()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    [CSMethod("GetNewsList")]
    public object GetNewsList(int p, string title, string time1, string time2)
    {

        using (DBConnection dbc = new DBConnection())
        {

            int cp = p;
            int ac = 0;



            string where = "   ";
            if (time1 != null && time1 != "")
            {
                where += " and ADDTIME>=@ADDTIME";
            }
            else if (time2 != null && time2 != "")
            {
                where += " and ADDTIME<=@ADDTIME2";
            }

            if (title != null && title != "")
            {
                where += " and  XW_TITLE like @XW_TITLE";
            }

            string str = "select a.*,b.user_xm people from tb_b_News a left join tb_b_users b on a.user_id=b.user_id where a.delflag=0 " + where + " order by a.ADDTIME desc";
            MySqlCommand cmd = new MySqlCommand(str);
            if (time1 != null && time1 != "")
            {
                cmd.Parameters.AddWithValue("@ADDTIME", time1);
            }
            else if (time2 != null && time2 != "")
            {
                cmd.Parameters.AddWithValue("@ADDTIME2", time2);
            }

            if (title != null && title != "")
            {
                cmd.Parameters.AddWithValue("@XW_TITLE", "%" + title + "%");
            }

            System.Data.DataTable dtPage = new System.Data.DataTable();
            dtPage = dbc.GetPagedDataTable(cmd, 20, ref cp, out ac);
            dtPage.Columns.Add("nADDTIME");
            if (dtPage.Rows.Count > 0)
            {
                for (int i = 0; i < dtPage.Rows.Count; i++)
                {
                    dtPage.Rows[i]["nADDTIME"] = Convert.ToDateTime(dtPage.Rows[i]["ADDTIME"].ToString()).ToString("yyyy-MM-dd");
                }
            }

            return new { dt = dtPage, cp = cp, ac = ac };
        }

    }


    [CSMethod("DeleteXW")]
    public object DeleteXW(JSReader jsr)
    {
        var user = SystemUser.CurrentUser;
        string userid = user.UserID;

        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                for (int i = 0; i < jsr.ToArray().Length; i++)
                {
                    string str = "update tb_b_news set delflag=1,UPDATETIME=@UPDATETIME where XW_ID=@XW_ID";
                    MySqlCommand ocmd = new MySqlCommand(str);
                    ocmd.Parameters.AddWithValue("@UPDATETIME", DateTime.Now);
                    ocmd.Parameters.AddWithValue("@XW_ID", jsr.ToArray()[i].ToString());
                    dbc.ExecuteNonQuery(ocmd);
                }

                dbc.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    //---保存新闻
    [CSMethod("SaveNews")]
    public object SaveNews(JSReader jsr)
    {

        var s = SystemUser.CurrentUser;
        DateTime datetime = DateTime.Now;

        string title = jsr["title"];
        string userid = s.UserID;//用户
        string contxt = jsr["contxt"];

        string XW_TITLE_Color = jsr["XW_TITLE_Color"];
        string addtime = jsr["ADDTIME"];
        string XW_CONTEXT_SMALL = jsr["XW_CONTEXT_SMALL"];

        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                dbc.BeginTransaction();
                if (jsr["newsid"].IsEmpty)
                {
                    string sqlStr = @"insert into tb_b_news(XW_TITLE,XW_TITLE_SMALL,XW_CONTEXT_SMALL,XW_CONTEXT,ADDTIME,UPDATETIME,User_Id,delflag)
                                  values(@XW_TITLE,@XW_TITLE_SMALL,@XW_CONTEXT_SMALL,@XW_CONTEXT,@ADDTIME,@UPDATETIME,@User_Id,0)";
                    MySqlCommand cmd = new MySqlCommand(sqlStr);
                    cmd.Parameters.Add("@XW_TITLE", title);
                    cmd.Parameters.Add("@XW_TITLE_SMALL", jsr["XW_TITLE_SMALL"].ToString());
                    cmd.Parameters.Add("@XW_CONTEXT_SMALL", jsr["XW_CONTEXT_SMALL"].ToString());
                    cmd.Parameters.Add("@XW_CONTEXT", contxt);
                    cmd.Parameters.Add("@ADDTIME", DateTime.Now);
                    cmd.Parameters.Add("@UPDATETIME", DateTime.Now);
                    cmd.Parameters.Add("@User_Id", userid);
                    dbc.ExecuteNonQuery(cmd);
                }
                else
                {
                    string sqlStr = @"update tb_b_news set XW_TITLE=@XW_TITLE,XW_TITLE_SMALL=@XW_TITLE_SMALL,XW_CONTEXT_SMALL=@XW_CONTEXT_SMALL,XW_CONTEXT=@XW_CONTEXT,UPDATETIME=@UPDATETIME,User_Id=@User_Id where XW_ID=@XW_ID";
                    MySqlCommand cmd = new MySqlCommand(sqlStr);
                    cmd.Parameters.Add("@XW_ID", jsr["newsid"].ToString());
                    cmd.Parameters.Add("@XW_TITLE", title);
                    cmd.Parameters.Add("@XW_TITLE_SMALL", jsr["XW_TITLE_SMALL"].ToString());
                    cmd.Parameters.Add("@XW_CONTEXT_SMALL", jsr["XW_CONTEXT_SMALL"].ToString());
                    cmd.Parameters.Add("@XW_CONTEXT", contxt);
                    cmd.Parameters.Add("@UPDATETIME", DateTime.Now);
                    cmd.Parameters.Add("@User_Id", userid);
                    dbc.ExecuteNonQuery(cmd);
                }


                dbc.CommitTransaction();

                return null;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    [CSMethod("GetNews")]
    public object GetNews(string newsid)// 查询详情
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                DataTable dt = new DataTable();
                string str = "select  * from tb_b_news where XW_ID = " + dbc.ToSqlValue(newsid) + " and delflag=0";
                dt = dbc.ExecuteDataTable(str);
                dt.Columns.Add("nADDTIME");

                Hashtable dtRet = new Hashtable();
                dtRet.Add("dt", dt);

                return dtRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("SaveRules")]
    public object SaveRules(JSReader jsr)
    {

        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                dbc.BeginTransaction();
                dbc.ExecuteNonQuery("delete from tb_b_rules");
                string sqlStr = @"insert into tb_b_rules(YJSX,YHXX,JSZQSX,JSZQXX,JFJRSX,TSCLSX,TCJSSX,TCJSXX,YYEZSJQ,TSCLLJQ,JFCLLJQ,XZFWSJQ,JSJQ)
                                  values(@YJSX,@YHXX,@JSZQSX,@JSZQXX,@JFJRSX,@TSCLSX,@TCJSSX,@TCJSXX,@YYEZSJQ,@TSCLLJQ,@JFCLLJQ,@XZFWSJQ,@JSJQ)";
                MySqlCommand cmd = new MySqlCommand(sqlStr);
                cmd.Parameters.Add("@YJSX", jsr["YJSX"].ToString());
                cmd.Parameters.Add("@YHXX", jsr["YHXX"].ToString());
                cmd.Parameters.Add("@JSZQSX", jsr["JSZQSX"].ToString());
                cmd.Parameters.Add("@JSZQXX", jsr["JSZQXX"].ToString());
                cmd.Parameters.Add("@JFJRSX", jsr["JFJRSX"].ToString());
                cmd.Parameters.Add("@TSCLSX", jsr["TSCLSX"].ToString());
                cmd.Parameters.Add("@TCJSSX", jsr["TCJSSX"].ToString());
                cmd.Parameters.Add("@TCJSXX", jsr["TCJSXX"].ToString());
                cmd.Parameters.Add("@YYEZSJQ", jsr["YYEZSJQ"].ToString());
                cmd.Parameters.Add("@TSCLLJQ", jsr["TSCLLJQ"].ToString());
                cmd.Parameters.Add("@JFCLLJQ", jsr["JFCLLJQ"].ToString());
                cmd.Parameters.Add("@XZFWSJQ", jsr["XZFWSJQ"].ToString());
                cmd.Parameters.Add("@JSJQ", jsr["JSJQ"].ToString());
                dbc.ExecuteNonQuery(cmd);
                dbc.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    [CSMethod("GetRules")]
    public object GetRules()
    {

        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                return dbc.ExecuteDataTable("select * from tb_b_rules");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}