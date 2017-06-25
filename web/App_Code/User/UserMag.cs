using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartFramework4v2.Web.WebExcutor;
using System.Data;

using System.Text;

using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Data;
using SmartFramework4v2.Data.MySql;
using MySql.Data.MySqlClient;
using System.IO;
/// <summary>
///UserMag 的摘要说明
/// </summary>

[CSClass("YHGLClass")]
public class UserMag
{
    [CSMethod("GetUserListTotal")]
    public object GetUserListTotal(string jsid, string xm)
    {
        if (!string.IsNullOrEmpty(jsid))
        {
            try
            {
                Guid guid = new Guid(jsid);
            }
            catch
            {
                throw new Exception("角色ID出错！");
            }
        }

        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string where = "";
                if (!string.IsNullOrEmpty(jsid))
                {
                    where += " and User_ID in (SELECT User_ID FROM tb_b_User_JS_Gl where JS_ID=" + jsid + " and delflag=0 )";
                }

                if (!string.IsNullOrEmpty(xm.Trim()))
                {
                    where += " and " + dbc.C_Like("User_XM", xm.Trim(), LikeStyle.LeftAndRightLike);
                }

                string str = "select * from tb_b_Users where User_DelFlag=0  ";
                str += where;
                str += " order by LoginName,User_XM";

                DataTable dt = dbc.ExecuteDataTable(str);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    public static bool getJs_cj()
    {
        var user = SystemUser.CurrentUser;
        string userid = user.UserID;

        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                string str = "SELECT * FROM tb_b_User_JS_Gl where delflag=0 AND JS_ID='F6613AFB-06E2-454A-881F-8C51483976F3' and USER_ID=" + userid + "";
                DataTable dt = dbc.ExecuteDataTable(str);
                if (dt.Rows.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    [CSMethod("GetUserList")]
    public object GetUserList(int pagnum, int pagesize, string xm, string gh, string sdate, string edate, string qy, string zt)
    {
        //if (!string.IsNullOrEmpty(jsid))
        //{
        //    try
        //    {
        //        Guid guid = new Guid(jsid);
        //    }
        //    catch
        //    {
        //        throw new Exception("角色ID出错！");
        //    }
        //}

        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";
                //if (!string.IsNullOrEmpty(jsid))
                //{
                //    where += " and User_ID in (SELECT User_ID FROM tb_b_User_JS_Gl where JS_ID=" + jsid + " and delflag=0 )";
                //}

                if (!string.IsNullOrEmpty(xm.Trim()))
                {
                    where += " and " + dbc.C_Like("User_XM", xm.Trim(), LikeStyle.LeftAndRightLike);
                }
                if (!string.IsNullOrEmpty(gh.Trim()))
                {
                    where += " and " + dbc.C_Like("User_JobNo", gh.Trim(), LikeStyle.LeftAndRightLike);
                }
                if (!string.IsNullOrEmpty(sdate))
                {
                    where += " and StartDate>=" + dbc.ToSqlValue(sdate);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    where += " and EndDate<=" + dbc.ToSqlValue(edate);
                }
                if (!string.IsNullOrEmpty(qy))
                {
                    where += " and QY_ID=" + qy;
                }
                if (!string.IsNullOrEmpty(zt))
                {
                    where += " and User_Enable=" + zt;
                }

                string str = "select a.*,b.QY_NAME from tb_b_Users a left join tb_b_qy b on a.QY_ID=b.QY_ID where User_DelFlag=0  ";
                str += where;

                //开始取分页数据
                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(str + " order by LoginName,User_XM", pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    //[CSMethod("GetUserList")]
    //public object GetUserList(int cp, string userName, string deptType, string dept)
    //{
    //    string condition = "";
    //    if (deptType.Trim() != "")
    //    {
    //        condition += " and t2.dw_lx = '" + deptType.Replace("'", "''") + "'";
    //    }
    //    if (userName.Trim() != "")
    //    {
    //        condition += " and t1.yh_dlm like '%" + userName.Replace("'", "''") + "%'";
    //    }
    //    if (dept.Trim() != "")
    //    {
    //        condition += " and t1.dw_id = '" + dept.Replace("'", "''") + "'";
    //    }
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("select t1.yh_id, ");
    //    sb.Append("t1.yh_dlm, ");
    //    sb.Append("t1.addtime, ");
    //    sb.Append("t2.dw_mc, ");
    //    sb.Append("case t2.dw_lx ");
    //    sb.Append("when 0 then ");
    //    sb.Append("'物价局' ");
    //    sb.Append("when 1 then ");
    //    sb.Append("'配供中心' ");
    //    sb.Append("when 2 then ");
    //    sb.Append("'平价商店' ");
    //    sb.Append("when 3 then ");
    //    sb.Append("'蔬菜基地' ");
    //    sb.Append("end as dw_lx ,case t1.yh_enable when 0 then '启用' else '禁用' end as YH_ENABLE ");
    //    sb.Append("from TZCLZ_T_YH t1, TZCLZ_T_DW t2 ");
    //    sb.AppendFormat("where t1.dw_id = t2.dw_id and t1.status = 0 {0} order by t1.addtime", condition);
    //    int allCount = 0;
    //    int currentPage = cp;
    //    currentPage = cp;
    //    using (DBConnection dbc = new DBConnection())
    //    {
    //        DataTable dt = dbc.ExecuteDataTable(sb.ToString());
    //        return new { dtUser = dt, currentPage = cp, allCount = allCount };
    //    }
    //}

    [CSMethod("GetUserAndJs")]
    public object GetUserAndJs(string UserId)
    {

        using (DBConnection dbc = new DBConnection())
        {
            string sqlStrUser = "select * from tb_b_Users where User_ID=" + UserId + "";
            DataTable dtuser = dbc.ExecuteDataTable(sqlStrUser);
            string sqlStrJs = "select distinct JS_ID from tb_b_User_JS_Gl where delflag=0 and User_ID=" + UserId + "";
            DataTable dtjs = dbc.ExecuteDataTable(sqlStrJs);

            return new { dtuser = dtuser, dtjs = dtjs };
        }
    }


    [CSMethod("GetUser")]
    public DataTable GetUser(string UserId)
    {
        string sqlStr = "select * from tb_b_Users where User_ID=" + UserId + "";
        using (DBConnection dbc = new DBConnection())
        {
            return dbc.ExecuteDataTable(sqlStr);
        }
    }

    [CSMethod("GetUserJs")]
    public DataTable GetUserJs(string UserId)
    {
        string sqlStr = "select distinct JS_ID from tb_b_User_JS_Gl where delflag=0 and User_ID=" + UserId + "";
        using (DBConnection dbc = new DBConnection())
        {
            return dbc.ExecuteDataTable(sqlStr);
        }
    }

    [CSMethod("GetJs")]
    public DataTable GetJs()
    {
        string sqlStr = "select JS_ID,JS_NAME from tb_b_JS where status=0 order by JS_Type,JS_PX";
        using (DBConnection dbc = new DBConnection())
        {
            return dbc.ExecuteDataTable(sqlStr);
        }
    }

    [CSMethod("GetDWByJsid")]
    public DataTable GetDWByJsid(string UserId, string jsid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            DataTable dt = new DataTable();
            string sqlStr = "select * from tb_b_JS where status=0 and JS_ID=" + jsid + "";
            DataTable dtjs = dbc.ExecuteDataTable(sqlStr);
            if (dtjs != null && dtjs.Rows.Count > 0)
            {
                int jstype = Convert.ToInt32(dtjs.Rows[0]["JS_Type"]);
                if (jstype == 0)
                {
                    string str = "select distinct DW_ID from tb_b_User_Dw_Gl where delflag=0 and DW_ID in(SELECT DW_ID  FROM tb_b_Department where STATUS=0) and User_ID=" + UserId + "";
                    dt = dbc.ExecuteDataTable(str);
                }
                else
                {
                    switch (jsid.ToUpper())
                    {
                        case "F6613AFB-06E2-454A-881F-8C51483976F3":
                            dt = dbc.ExecuteDataTable("select distinct DW_ID from tb_b_User_Dw_Gl where delflag=0 and DW_ID in( select DW_ID from tb_b_DW where DW_LX=4 and STATUS=0) and User_ID=" + UserId + "");
                            break;
                        case "7E53492E-CF66-411F-83C4-7923467F59B4":
                            dt = dbc.ExecuteDataTable("select distinct DW_ID from tb_b_User_Dw_Gl where delflag=0 and DW_ID in( select PJSD_ID from tb_b_PJSD where delflag=0) and User_ID=" + UserId + "");
                            break;
                    }
                }
            }
            return dt;
        }
    }


    [CSMethod("GetDW")]
    public DataTable GetDW(string jsid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            DataTable dt = new DataTable();
            string sqlStr = "select * from tb_b_JS where status=0 and JS_ID=" + jsid + "";
            DataTable dtjs = dbc.ExecuteDataTable(sqlStr);
            if (dtjs != null && dtjs.Rows.Count > 0)
            {
                int jstype = Convert.ToInt32(dtjs.Rows[0]["JS_Type"]);
                if (jstype == 0)
                {
                    string str1 = "SELECT DW_ID ID,DW_MC MC FROM tb_b_Department where STATUS=0";
                    dt = dbc.ExecuteDataTable(str1);
                }
                else
                {
                    switch (jsid.ToUpper())
                    {
                        case "F6613AFB-06E2-454A-881F-8C51483976F3":
                            dt = dbc.ExecuteDataTable("select DW_ID ID,DW_MC MC from tb_b_DW where DW_LX=4 and STATUS=0");
                            break;
                        case "7E53492E-CF66-411F-83C4-7923467F59B4":
                            dt = dbc.ExecuteDataTable("select PJSD_ID ID,PJSD_MC MC from dbo.tb_b_PJSD where delflag=0");
                            break;
                    }
                }
            }
            return dt;
        }
    }

    [CSMethod("GetDWAndGl")]
    public object GetDWAndGl(string jsid, string UserId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                //单位
                DataTable dt = new DataTable();
                //权限
                DataTable dtqx = new DataTable();
                //权限关联
                DataTable dtqxgl = new DataTable();
                //用户关联
                DataTable dtusergl = new DataTable();

                string sqlStr = "select * from tb_b_JS where status=0 and JS_ID=" + jsid + "";
                DataTable dtjs = dbc.ExecuteDataTable(sqlStr);

                if (dtjs != null && dtjs.Rows.Count > 0)
                {
                    int jstype = Convert.ToInt32(dtjs.Rows[0]["JS_Type"]);
                    if (jstype == 0)
                    {

                        string str2 = "SELECT PRIVILEGECODE ID,MODULENAME MC FROM tb_b_YH_QX where MODULENAME not like '平价商店权限_%' order by substring(MODULENAME,1,locate('-',MODULENAME)),ORDERNO";
                        dtqx = dbc.ExecuteDataTable(str2);
                        if (UserId != null)
                        {
                            string str3 = "select a.PRIVILEGECODE,b.MODULENAME from tb_b_YH_YHQX a left join tb_b_YH_QX b on a.PRIVILEGECODE=b.PRIVILEGECODE where USERID=" + UserId + "";
                            dtqxgl = dbc.ExecuteDataTable(str3);
                        }
                    }
                    else
                    {
                        string str2 = "SELECT PRIVILEGECODE ID,MODULENAME MC FROM tb_b_YH_QX where MODULENAME like '平价商店权限_%' order by substring(MODULENAME,1,locate('-',MODULENAME)),ORDERNO";
                        dtqx = dbc.ExecuteDataTable(str2);
                        if (UserId != null)
                        {
                            string str3 = "select a.PRIVILEGECODE,b.MODULENAME from tb_b_YH_YHQX a left join tb_b_YH_QX b on a.PRIVILEGECODE=b.PRIVILEGECODE where USERID=" + UserId + "";
                            dtqxgl = dbc.ExecuteDataTable(str3);
                        }


                    }
                }


                //if (UserId != null)
                //{
                //    dtusergl = GetDWByJsid(UserId, jsid);
                //}

                return new { usergl = dtusergl, dtqx = dtqx, dtqxgl = dtqxgl };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetDeptsByType")]
    public DataTable GetDeptsByType(string DeptType)
    {
        string sqlStr = "select * from tb_b_Department where DW_LX = @DW_LX and STATUS = 0";
        using (DBConnection dbc = new DBConnection())
        {
            MySqlCommand cmd = new MySqlCommand(sqlStr);
            cmd.Parameters.AddWithValue("@DW_LX", DeptType);
            return dbc.ExecuteDataTable(cmd);
        }
    }
    [CSMethod("GetDepts")]
    public DataTable GetDepts()
    {
        string sqlStr = "select * from tb_b_Department where  STATUS = 0";
        using (DBConnection dbc = new DBConnection())
        {
            MySqlCommand cmd = new MySqlCommand(sqlStr);
            return dbc.ExecuteDataTable(cmd);
        }
    }

    [CSMethod("SaveUser")]
    public bool SaveUser(JSReader jsr, JSReader yhjs, JSReader yhjsdw, JSReader qxids, JSReader fj)
    {
        if (jsr["LoginName"].IsNull || jsr["LoginName"].IsEmpty)
        {
            throw new Exception("用户名不能为空");
        }
        if (jsr["Password"].IsNull || jsr["Password"].IsEmpty)
        {
            throw new Exception("密码不能为空");
        }

        if (yhjs.ToArray().Length == 0)
        {
            throw new Exception("没有用户角色！");
        }

        var EditUser = SystemUser.CurrentUser;

        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                if (jsr["User_ID"].ToString() == "")
                {
                    DataTable dt_user = dbc.ExecuteDataTable("select * from tb_b_Users where LoginName='" + jsr["LoginName"].ToString() + "' and User_DelFlag=0");
                    if (dt_user.Rows.Count > 0)
                    {
                        throw new Exception("该用户名已存在！");
                    }

                    var YHID = Guid.NewGuid().ToString();
                    //建立用户
                    string sqlStr = "";
                    //if (jsr["QY_ID"].ToString() != "")
                    //{
                    sqlStr = "insert into tb_b_Users (LoginName,Password,User_XM,User_SJ,User_Email,User_DZ,User_Enable,User_DelFlag,addtime,updatetime,updateuser,QY_ID,User_Sex,User_Age,User_From,User_Education,User_JobNo,User_IdCard,User_ContractNo,User_Mode,StartDate,EndDate) " +
                                             "values (@LoginName,@Password,@User_XM,@User_SJ,@User_Email,@User_DZ,@User_Enable,@User_DelFlag,@addtime,@updatetime,@updateuser,@qyid,@User_Sex,@User_Age,@User_From,@User_Education,@User_JobNo,@User_IdCard,@User_ContractNo,@User_Mode,@StartDate,@EndDate)";


                    //}
                    //else
                    //{
                    //    sqlStr = "insert into tb_b_Users (LoginName,Password,User_DM,User_XM,User_ZW,User_DH,User_SJ,User_Email,User_DZ,User_Enable,User_DelFlag,addtime,updatetime,updateuser) " +
                    //        "values (@LoginName,@Password,@User_DM,@User_XM,@User_ZW,@User_DH,@User_SJ,@User_Email,@User_DZ,@User_Enable,@User_DelFlag,@addtime,@updatetime,@updateuser)";
                    //}
                    MySqlCommand cmd = new MySqlCommand(sqlStr);
                    cmd.Parameters.AddWithValue("@LoginName", jsr["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Password", jsr["Password"].ToString());
                    cmd.Parameters.AddWithValue("@User_XM", jsr["User_XM"].ToString());
                    cmd.Parameters.AddWithValue("@User_SJ", jsr["User_SJ"].ToString());
                    cmd.Parameters.AddWithValue("@User_Email", jsr["User_Email"].ToString());
                    cmd.Parameters.AddWithValue("@User_DZ", jsr["User_DZ"].ToString());

                    cmd.Parameters.AddWithValue("@qyid", jsr["QY_ID"].ToString());

                    cmd.Parameters.AddWithValue("@User_Enable", Convert.ToInt32(jsr["User_Enable"].ToString()));
                    cmd.Parameters.AddWithValue("@User_DelFlag", 0);
                    cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);

                    cmd.Parameters.AddWithValue("@User_Sex", jsr["User_Sex"].ToString());
                    cmd.Parameters.AddWithValue("@User_Age", Convert.ToInt32(jsr["User_Age"].ToString()));
                    cmd.Parameters.AddWithValue("@User_From", jsr["User_From"].ToString());
                    cmd.Parameters.AddWithValue("@User_Education", jsr["User_Education"].ToString());
                    cmd.Parameters.AddWithValue("@User_JobNo", jsr["User_JobNo"].ToString());
                    cmd.Parameters.AddWithValue("@User_IdCard", jsr["User_IdCard"].ToString());
                    cmd.Parameters.AddWithValue("@User_ContractNo", jsr["User_ContractNo"].ToString());
                    cmd.Parameters.AddWithValue("@User_Mode", jsr["User_Mode"].ToString());
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(jsr["StartDate"].ToString()));
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(jsr["EndDate"].ToString()));

                    dbc.ExecuteNonQuery(cmd);

                    var fj_arr = fj.ToArray();
                    if (fj_arr.Length > 0)
                    {
                        var fj_ids = "";
                        for (int i = 0; i < fj_arr.Length; i++)
                        {
                            if (i == 0)
                            {
                                fj_ids += "'" + fj_arr[i].ToString() + "'";
                            }
                            else
                            {
                                fj_ids += ",'" + fj_arr[i].ToString() + "'";
                            }
                        }
                        dbc.ExecuteNonQuery("update tb_b_fj set fj_pid='" + YHID + "' where status=0 and fj_id in(" + fj_ids + ")");
                    }

                    //建立用户角色关联
                    for (int i = 0; i < yhjs.ToArray().Length; i++)
                    {
                        string sqlstr_js = "insert into tb_b_User_JS_Gl (User_ID,JS_ID,delflag,addtime,updatetime,updateuser) values(@User_ID,@JS_ID,@delflag,@addtime,@updatetime,@updateuser)";
                        cmd = new MySqlCommand(sqlstr_js);
                        cmd.Parameters.AddWithValue("@User_ID", YHID);
                        cmd.Parameters.AddWithValue("@JS_ID", yhjs.ToArray()[i].ToString());
                        cmd.Parameters.AddWithValue("@delflag", 0);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);
                        dbc.ExecuteNonQuery(cmd);
                    }


                    ////建立用户单位关联
                    //for (int i = 0; i < yhjsdw.ToArray().Length; i++)
                    //{
                    //    JSReader[] arr_dw = yhjsdw.ToArray()[i].ToArray();
                    //    for (int k = 0; k < arr_dw.Length; k++)
                    //    {
                    //        string sqlstr_dw = "insert into tb_b_User_Dw_Gl(UserDwGL_id,User_ID,DW_ID,delflag,addtime,updatetime,updateuser) values(@UserDwGL_id,@User_ID,@DW_ID,@delflag,@addtime,@updatetime,@updateuser)";
                    //        cmd = new MySqlCommand(sqlstr_dw);
                    //        cmd.Parameters.AddWithValue("@UserDwGL_id", Guid.NewGuid());
                    //        cmd.Parameters.AddWithValue("@User_ID", YHID);
                    //        cmd.Parameters.AddWithValue("@DW_ID", arr_dw[k].ToString());
                    //        cmd.Parameters.AddWithValue("@delflag", 0);
                    //        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                    //        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    //        cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);
                    //        dbc.ExecuteNonQuery(cmd);
                    //    }
                    //}

                    //建立用户权限关联
                    for (int i = 0; i < qxids.ToArray().Length; i++)
                    {
                        string sqlstr_qx = "insert into tb_b_YH_YHQX (PRIVILEGECODE,USERID) values(@PRIVILEGECODE,@USERID)";
                        cmd = new MySqlCommand(sqlstr_qx);
                        cmd.Parameters.AddWithValue("@PRIVILEGECODE", new Guid(qxids.ToArray()[i]));
                        cmd.Parameters.AddWithValue("@USERID", YHID);
                        dbc.ExecuteNonQuery(cmd);
                    }

                }
                else
                {
                    var YHID = jsr["User_ID"].ToString();
                    var oldname = dbc.ExecuteScalar("select LoginName from tb_b_Users where User_ID='" + YHID + "'");
                    if (!jsr["LoginName"].ToString().Equals(oldname.ToString()))
                    {
                        DataTable dt_user = dbc.ExecuteDataTable("select * from tb_b_Users where LoginName='" + jsr["LoginName"].ToString() + "' and User_DelFlag=0");
                        if (dt_user.Rows.Count > 0)
                        {
                            throw new Exception("该用户名已存在！");
                        }
                    }
                    var con = "";
                    if (jsr["QY_ID"].ToString() != "")
                    {
                        con = ",QY_ID='" + jsr["QY_ID"].ToString() + "'";
                    }
                    else
                    {
                        con = ",QY_ID=null";

                    }
                    string sqlstr = @"update tb_b_Users set LoginName=@LoginName,Password=@Password,User_XM=@User_XM,User_SJ=@User_SJ,User_Email=@User_Email,User_DZ=@User_DZ,User_Enable=@User_Enable,updatetime=@updatetime,updateuser=@updateuser 
,User_Sex=@User_Sex,User_Age=@User_Age,User_From=@User_From,User_Education=@User_Education,User_JobNo=@User_JobNo,User_IdCard=@User_IdCard,User_ContractNo=@User_ContractNo,User_Mode=@User_Mode,StartDate=@StartDate
,EndDate=@EndDate
" + con + " where User_ID=@User_ID";
                    MySqlCommand cmd = new MySqlCommand(sqlstr);
                    cmd.Parameters.AddWithValue("@LoginName", jsr["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Password", jsr["Password"].ToString());
                    cmd.Parameters.AddWithValue("@User_XM", jsr["User_XM"].ToString());
                    cmd.Parameters.AddWithValue("@User_SJ", jsr["User_SJ"].ToString());
                    cmd.Parameters.AddWithValue("@User_Email", jsr["User_Email"].ToString());
                    cmd.Parameters.AddWithValue("@User_DZ", jsr["User_DZ"].ToString());
                    cmd.Parameters.AddWithValue("@User_Enable", Convert.ToInt32(jsr["User_Enable"].ToString()));
                    cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);
                    cmd.Parameters.AddWithValue("@User_ID", YHID);

                    cmd.Parameters.AddWithValue("@User_Sex", jsr["User_Sex"].ToString());
                    cmd.Parameters.AddWithValue("@User_Age", Convert.ToInt32(jsr["User_Age"].ToString()));
                    cmd.Parameters.AddWithValue("@User_From", jsr["User_From"].ToString());
                    cmd.Parameters.AddWithValue("@User_Education", jsr["User_Education"].ToString());
                    cmd.Parameters.AddWithValue("@User_JobNo", jsr["User_JobNo"].ToString());
                    cmd.Parameters.AddWithValue("@User_IdCard", jsr["User_IdCard"].ToString());
                    cmd.Parameters.AddWithValue("@User_ContractNo", jsr["User_ContractNo"].ToString());
                    cmd.Parameters.AddWithValue("@User_Mode", jsr["User_Mode"].ToString());
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(jsr["StartDate"].ToString()));
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(jsr["EndDate"].ToString()));
                    dbc.ExecuteNonQuery(cmd);

                    //删除用户的角色关联
                    string del_js = "update tb_b_User_JS_Gl set delflag=1,updatetime=@updatetime,updateuser=@updateuser where User_ID=@User_ID";
                    cmd = new MySqlCommand(del_js);
                    cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);
                    cmd.Parameters.AddWithValue("@User_ID", YHID);
                    dbc.ExecuteNonQuery(cmd);
                    ////删除用户的单位关联
                    //string del_dw = "update tb_b_User_Dw_Gl set delflag=1,updatetime=@updatetime,updateuser=@updateuser where User_ID=@User_ID";
                    //cmd = new MySqlCommand(del_dw);
                    //cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);
                    //cmd.Parameters.AddWithValue("@User_ID", YHID);
                    //dbc.ExecuteNonQuery(cmd);
                    //删除用户的权限关联
                    string del_qx = "delete from tb_b_YH_YHQX where USERID=@USERID";
                    cmd = new MySqlCommand(del_qx);
                    cmd.Parameters.AddWithValue("@USERID", YHID);
                    dbc.ExecuteNonQuery(cmd);

                    var fj_arr = fj.ToArray();
                    if (fj_arr.Length > 0)
                    {
                        var fj_ids = "";
                        for (int i = 0; i < fj_arr.Length; i++)
                        {
                            if (i == 0)
                            {
                                fj_ids += "'" + fj_arr[i].ToString() + "'";
                            }
                            else
                            {
                                fj_ids += ",'" + fj_arr[i].ToString() + "'";
                            }
                        }
                        dbc.ExecuteNonQuery("update tb_b_fj set fj_pid='" + YHID + "' where status=0 and fj_id in(" + fj_ids + ")");
                    }

                    //建立用户角色关联
                    for (int i = 0; i < yhjs.ToArray().Length; i++)
                    {
                        string sqlstr_js = "insert into tb_b_User_JS_Gl (User_ID,JS_ID,delflag,addtime,updatetime,updateuser) values(@User_ID,@JS_ID,@delflag,@addtime,@updatetime,@updateuser)";
                        cmd = new MySqlCommand(sqlstr_js);
                        cmd.Parameters.AddWithValue("@User_ID", YHID);
                        cmd.Parameters.AddWithValue("@JS_ID", yhjs.ToArray()[i].ToString());
                        cmd.Parameters.AddWithValue("@delflag", 0);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);
                        dbc.ExecuteNonQuery(cmd);
                    }


                    ////建立用户单位关联
                    //for (int i = 0; i < yhjsdw.ToArray().Length; i++)
                    //{
                    //    JSReader[] arr_dw = yhjsdw.ToArray()[i].ToArray();
                    //    for (int k = 0; k < arr_dw.Length; k++)
                    //    {
                    //        string sqlstr_dw = "insert into tb_b_User_Dw_Gl(UserDwGL_id,User_ID,DW_ID,delflag,addtime,updatetime,updateuser) values(@UserDwGL_id,@User_ID,@DW_ID,@delflag,@addtime,@updatetime,@updateuser)";
                    //        cmd = new MySqlCommand(sqlstr_dw);
                    //        cmd.Parameters.AddWithValue("@UserDwGL_id", Guid.NewGuid());
                    //        cmd.Parameters.AddWithValue("@User_ID", YHID);
                    //        cmd.Parameters.AddWithValue("@DW_ID", arr_dw[k].ToString());
                    //        cmd.Parameters.AddWithValue("@delflag", 0);
                    //        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                    //        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    //        cmd.Parameters.AddWithValue("@updateuser", EditUser.UserID);
                    //        dbc.ExecuteNonQuery(cmd);
                    //    }
                    //}

                    //建立用户权限关联
                    for (int i = 0; i < qxids.ToArray().Length; i++)
                    {
                        string sqlstr_qx = "insert into tb_b_YH_YHQX (PRIVILEGECODE,USERID) values(@PRIVILEGECODE,@USERID)";
                        cmd = new MySqlCommand(sqlstr_qx);
                        cmd.Parameters.AddWithValue("@PRIVILEGECODE", new Guid(qxids.ToArray()[i]));
                        cmd.Parameters.AddWithValue("@USERID", YHID);
                        dbc.ExecuteNonQuery(cmd);
                    }
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

    //[CSMethod("SaveUser")]
    //public bool SaveUser(JSReader jsr, string[] Privileges)
    //{
    //    if (jsr["YH_DLM"].IsNull || jsr["YH_DLM"].IsEmpty)
    //    {
    //        throw new Exception("登陆名不能为空");
    //    }
    //    if (jsr["YH_MIMA"].IsNull || jsr["YH_MIMA"].IsEmpty)
    //    {
    //        throw new Exception("密码不能为空");
    //    }
    //    if (jsr["DW_LX"].IsNull || jsr["DW_LX"].IsEmpty)
    //    {
    //        throw new Exception("单位类型不能为空");
    //    }
    //    if (jsr["DW_ID"].IsNull || jsr["DW_ID"].IsEmpty)
    //    {
    //        throw new Exception("密码不能为空");
    //    }
    //    bool UserEditSuccess = false;
    //    if (jsr["YH_ID"].ToString() == "")
    //    {
    //        UserEditSuccess = SystemUser.CreateUser(jsr["YH_DLM"].ToString(),"", jsr["YH_MIMA"].ToString(),"","","","","","");
    //    }
    //    else
    //    {
    //        UserEditSuccess = EditUser(jsr["YH_DLM"].ToString(), jsr["YH_MIMA"].ToString(), jsr["DW_ID"].ToString(), jsr["YH_ID"].ToString());
    //    }
    //    if (UserEditSuccess)
    //    {

    //        if (jsr["DW_LX"].ToString() == "0" && Privileges.Length > 0)
    //        {
    //            try
    //            {
    //                SystemUser.GetUserByUserName(jsr["YH_DLM"].ToString()).RemoveAllPriviledge();
    //                foreach (string Privilege in Privileges)
    //                {
    //                    SystemUser.GetUserByUserName(jsr["YH_DLM"].ToString()).AddPriviledge(new Guid(Privilege));
    //                }
    //                UserEditSuccess = true;
    //            }
    //            catch
    //            {
    //                UserEditSuccess = false;
    //            }
    //        }
    //    }
    //    return UserEditSuccess;
    //}

    [CSMethod("DelUser")]
    public bool DelUser(string userid)
    {
        if (userid.Trim() == "")
            return false;
        using (DBConnection dbc = new DBConnection())
        {
            int retInt = dbc.ExecuteNonQuery("update tb_b_Users set User_DelFlag = 1 where user_id = " + userid + "");
            if (retInt > 0)
                return true;
            return false;
        }
    }


    [CSMethod("GetQy")]
    public object GetQy()
    {
        using (DBConnection dbc = new DBConnection())
        {
            string sqlStr = "select qy_id VALUE,qy_name TEXT from tb_b_qy";
            DataTable dt = dbc.ExecuteDataTable(sqlStr);
            return dt;
        }
    }

    [CSMethod("DelUserByids")]
    public bool DelUserByids(JSReader jsr)
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
                    string str = "update tb_b_Users set User_DelFlag = 1,Updatetime=@Updatetime,Updateuser=@Updateuser where user_id =@user_id";
                    MySqlCommand cmd = new MySqlCommand(str);
                    cmd.Parameters.AddWithValue("@Updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Updateuser", userid);
                    cmd.Parameters.AddWithValue("@user_id", jsr.ToArray()[i].ToString());
                    dbc.ExecuteNonQuery(cmd);

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

    [CSMethod("EnableUser")]
    public bool EnableUser(string[] userIds, bool enable)
    {
        if (userIds.Length > 0)
        {
            string userIdStr = string.Join(",", userIds);
            var sqlStr = string.Format("update tb_b_Users set User_Enable = @YH_ENABLE where User_ID IN({0})", userIdStr);
            MySqlCommand cmd = new MySqlCommand(sqlStr);
            cmd.Parameters.AddWithValue("@YH_ENABLE", enable ? 0 : 1);
            using (DBConnection dbc = new DBConnection())
            {
                var retInt = dbc.ExecuteNonQuery(cmd);
                if (retInt > 0)
                    return true;
                return false;
            }
        }
        return false;
    }
    private bool EditUser(string dlm, string mima, string dwid, string userid)
    {
        string sqlStr = "update tzclz_t_yh set yh_dlm=:yh_dlm,yh_mima = :yh_mima,updatetime = sysdate,dw_id = :dw_id,XGYH_ID = :XGYH_ID where YH_ID = :YH_ID and status = 0";
        MySqlCommand cmd = new MySqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("yh_dlm", dlm);
        cmd.Parameters.AddWithValue("yh_mima", mima);
        cmd.Parameters.AddWithValue("dw_id", dwid);
        cmd.Parameters.AddWithValue("YH_ID", userid);
        cmd.Parameters.AddWithValue("XGYH_ID", SystemUser.CurrentUser.UserID);
        using (DBConnection dbc = new DBConnection())
        {
            int retInt = dbc.ExecuteNonQuery(cmd);
            if (retInt > 0)
                return true;
            return false;
        }
    }
    [CSMethod("GetAllPrivilege")]
    public object GetAllPrivilege()
    {
        var Mod = PrivilegeDescription.PrivilegeType();

        using (DBConnection dbc = new DBConnection())
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select t.orderno,");
            sb.Append(" substr(ModuleName, 0, instr(ModuleName, '-') -1) as Mod,");
            sb.Append(" substr(ModuleName,");
            sb.Append(" instr(ModuleName, '-') + 1,");
            sb.Append(" (length(ModuleName) - instr(ModuleName, '-'))) as Item");
            sb.Append(" ,t.privilegecode");
            sb.Append(" from TZCLZ_T_YH_QX t");
            var Items = dbc.ExecuteDataTable(sb.ToString());
            return new { Mod = Mod, Items = Items };
        }
    }
    [CSMethod("GetUserPrivileges")]
    public string[] GetAllPrivilege(string userid)
    {
        List<string> Privileges = new List<string>();

        var dtPrivilege = SystemUser.GetUserByID(userid).GetUserPriviledgeInfo();
        foreach (DataRow drPrivilege in dtPrivilege.Rows)
        {
            Privileges.Add(drPrivilege["PRIVILEGECODE"].ToString());
        }
        return Privileges.ToArray();
    }

    [CSMethod("UploadPicForProduct", 1)]
    public object UploadPicForProduct(FileData[] fds, string lx, string bz)
    {
        var sqlStr = "insert into tb_b_FJ (fj_id,fj_mc,addtime,updatetime,status,xgyh_id,fj_lx,fj_bz,fj_url)"
                    + "values (@fj_id,@fj_mc,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_bz,@fj_url)";
        using (DBConnection dbc = new DBConnection())
        {
            string FJID = Guid.NewGuid().ToString();
            MySqlCommand cmd = new MySqlCommand(sqlStr);
            cmd.Parameters.AddWithValue("@fj_id", FJID);
            cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
            cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
            cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
            // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
            cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
            cmd.Parameters.AddWithValue("@fj_lx", lx);
            cmd.Parameters.AddWithValue("@fj_bz", bz);
            cmd.Parameters.AddWithValue("@fj_url", "approot/r/files/" + bz + "/" + FJID + "." + fds[0].FileName);
            int retInt = dbc.ExecuteNonQuery(cmd);

            if (retInt > 0)
            {
                HttpContext context = HttpContext.Current;
                var Server = context.Server;
                string truepath = "~/files/" + bz + "/" + FJID + "." + fds[0].FileName;
                if (!Directory.Exists(Server.MapPath("~/files")))
                    Directory.CreateDirectory(Server.MapPath("~/files"));
                if (!Directory.Exists(Server.MapPath("~/files/" + bz)))
                    Directory.CreateDirectory(Server.MapPath("~/files/" + bz));
                using (Stream iStream = new FileStream(Server.MapPath(truepath),
                               FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    iStream.Write(fds[0].FileBytes, 0, fds[0].FileBytes.Length);
                }

                return new { fileurl = "approot/r/files/" + bz + "/" + FJID + "." + fds[0].FileName, isdefault = 0, fileid = FJID };
            }
            return null;
        }
    }
    [CSMethod("GetProductImages")]
    public DataTable GetProductImages(JSReader jsr, string pid, string lx, string bz)
    {
        using (DBConnection dbc = new DBConnection())
        {
            DataTable dt = new DataTable();
            string fj_ids = "";
            var fjid_arr = jsr.ToArray();
            if (fjid_arr.Length > 0)
            {
                for (int i = 0; i < fjid_arr.Length; i++)
                {
                    if (i == 0)
                    {
                        fj_ids += "'" + fjid_arr[i].ToString() + "'";
                    }
                    else
                    {
                        fj_ids += ",'" + fjid_arr[i].ToString() + "'";
                    }
                }
            }
            if (!string.IsNullOrEmpty(pid))
            {
                string sqlStr = "";
                if (fjid_arr.Length > 0)
                {
                    sqlStr = "select * from tb_b_FJ where (fj_pid=" + pid + " or fj_id in(" + fj_ids + ")) and fj_lx='" + lx + "' and status=0 and fj_bz='" + bz + "'";
                }
                else
                {
                    sqlStr = "select * from tb_b_FJ where fj_pid=" + pid + " and fj_lx='" + lx + "' and status=0 and fj_bz='" + bz + "'";
                }
                dt = dbc.ExecuteDataTable(sqlStr);
            }
            else
            {
                if (fjid_arr.Length > 0)
                {
                    string sqlStr = "select * from tb_b_FJ where fj_id in(" + fj_ids + ") and fj_lx='" + lx + "' and status=0 and fj_bz='" + bz + "'";
                    dt = dbc.ExecuteDataTable(sqlStr);
                }
            }
            return dt;
        }
    }
    [CSMethod("DelProductImageByPicID")]
    public bool DelProductImageByPicID(string picid)
    {
        string sqlStr = "update tb_b_FJ set STATUS = 1,UPDATETIME = sysdate(),XGYH_ID = @XGYH_ID where fj_id = @fj_id ";
        // string sqlStr2 = "update tb_b_ProductList set product_picid = null ,product_pic = null,updatetime = getdate() where product_picid=@SP_PICID";
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                dbc.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand(sqlStr);
                cmd.Parameters.AddWithValue("@XGYH_ID", SystemUser.CurrentUser.UserID);
                cmd.Parameters.AddWithValue("@fj_id", picid);
                dbc.ExecuteNonQuery(cmd);
                dbc.CommitTransaction();
                return true;
            }
            catch
            {
                dbc.RoolbackTransaction();
                return false;
            }
        }
    }

}
