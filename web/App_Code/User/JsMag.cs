using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartFramework4v2.Web.WebExcutor;
using System.Data;

using System.Text;

using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Data.MySql;
using MySql.Data.MySqlClient;

/// <summary>
///JsMag 的摘要说明
/// </summary>
[CSClass("JsGlClass")]
public class JsMag
{
    public JsMag()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    [CSMethod("SaveJs")]
    public object SaveJs(JSReader jsr, string zt)
    {
        var user = SystemUser.CurrentUser;
        string userid = user.UserID;
        using (DBConnection dbc = new DBConnection())
        {
            string lbmc = jsr["JS_NAME"];
            if (lbmc == "")
            {
                throw new Exception("角色名称不能为空！");
            }

            string jsxh = jsr["JS_PX"];
            if (jsxh == "")
            {
                throw new Exception("角色序号不能为空！");
            }

            if (jsr["JS_ZT"] == "")
            {
                throw new Exception("角色状态出错！");
            }

            if (jsr["JS_Type"] == "")
            {
                throw new Exception("角色类别出错！");
            }

            int jszt = jsr["JS_ZT"].ToInteger();

            if (jsr["JS_ID"].ToString() == "")
            {
                //新增
                string jsid = Guid.NewGuid().ToString();

                string sqlStr = @"insert into tb_b_JS(JS_ID,JS_NAME,JS_PX,JS_Type,JS_ZT,status,updatetime,addtime,updateuser)
                                  values(@JS_ID,@JS_NAME,@JS_PX,@JS_Type,@JS_ZT,@status,@updatetime,@addtime,@updateuser)";
                MySqlCommand cmd = new MySqlCommand(sqlStr);
                cmd.Parameters.Add("@JS_ID", jsid);
                cmd.Parameters.Add("@JS_NAME", lbmc);
                cmd.Parameters.Add("@JS_PX", jsr["JS_PX"].ToInteger());
                cmd.Parameters.Add("@JS_Type", jsr["JS_Type"].ToInteger());
                cmd.Parameters.Add("@JS_ZT", jszt);
                cmd.Parameters.Add("@status", false);
                cmd.Parameters.Add("@updatetime", DateTime.Now);
                cmd.Parameters.Add("@addtime", DateTime.Now);
                cmd.Parameters.Add("@updateuser", userid);
                dbc.ExecuteNonQuery(cmd);
            }
            else
            {
                //修改
                string jsid = jsr["JS_ID"].ToString();

                //User.tb_b_JSDataTable hh = new User.tb_b_JSDataTable();
                //User.tb_b_JSRow sr = hh.Newtb_b_JSRow();
                //sr.JS_ID = new Guid(jsid);
                //sr.JS_NAME = lbmc;
                //sr.JS_PX = jsr["JS_PX"].ToInteger();
                //sr.JS_Type = jsr["JS_Type"].ToInteger();
                //sr.JS_ZT = jszt;
                //sr.updatetime = DateTime.Now;
                //sr.updateuser = userid;
                //dbc.UpdateTable(sr);

                string sqlStr = @"update tb_b_JS set JS_NAME=@JS_NAME,JS_PX=@JS_PX,JS_Type=@JS_Type,JS_ZT=@JS_ZT,updatetime=@updatetime,updateuser=@updateuser where JS_ID=@JS_ID";
                MySqlCommand cmd = new MySqlCommand(sqlStr);
                cmd.Parameters.Add("@JS_ID", jsid);
                cmd.Parameters.Add("@JS_NAME", lbmc);
                cmd.Parameters.Add("@JS_PX", jsr["JS_PX"].ToInteger());
                cmd.Parameters.Add("@JS_Type", jsr["JS_Type"].ToInteger());
                cmd.Parameters.Add("@JS_ZT", jszt);
                cmd.Parameters.Add("@updatetime", DateTime.Now);
                cmd.Parameters.Add("@updateuser", userid);
                dbc.ExecuteNonQuery(cmd);
            }

            return GetJs(zt);
        }
    }

    [CSMethod("GetJs")]
    public object GetJs(string zt)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string where = "";
                if (!string.IsNullOrEmpty(zt) && zt != "")
                {
                    where = " and JS_ZT=@JS_ZT ";
                }

                string sql = "select * from tb_b_JS where status=0 ";
                sql += where;
                sql += " order by JS_PX";

                MySqlCommand ocmd = new MySqlCommand(sql);
                if (!string.IsNullOrEmpty(zt) && zt != "")
                {
                    ocmd.Parameters.AddWithValue("@JS_ZT", zt);
                }

                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("DeleteJs")]
    public object DeleteJs(JSReader jsr, string zt)
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
                    //判断角色是否被用户关联就不能删除
                    string str = "select count(*) from tb_b_User_JS_Gl where JS_ID=@JS_ID and delflag=0";
                    MySqlCommand ocmd = new MySqlCommand(str);
                    ocmd.Parameters.AddWithValue("@JS_ID", jsr.ToArray()[i].ToString());
                    int num = Convert.ToInt32(dbc.ExecuteScalar(ocmd));

                    if (num > 0)
                    {
                        throw new Exception("你所要删除的角色已经被用户关联，请先删除用户再进行此操作！");
                    }
                    else
                    {

                        string delstr = "update tb_b_JS set status=1,updatetime=@updatetime,updateuser=@updateuser where JS_ID=@JS_ID";
                        ocmd = new MySqlCommand(delstr);
                        ocmd.Parameters.AddWithValue("updatetime", DateTime.Now);
                        ocmd.Parameters.AddWithValue("updateuser", userid);
                        ocmd.Parameters.AddWithValue("JS_ID", jsr.ToArray()[i].ToString());
                        dbc.ExecuteNonQuery(ocmd);

                    }

                }

                dbc.CommitTransaction();

                return GetJs(zt);
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }
}
