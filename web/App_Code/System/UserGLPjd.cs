using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SmartFramework4v2.Data.SqlServer;
using SmartFramework4v2.Data;
using System.Data.SqlClient;
using SmartFramework4v2.Web.WebExcutor;
using System.Collections;
using SmartFramework4v2.Web.Common.JSON;

/// <summary>
///UserGLPjd 的摘要说明
/// </summary>
[CSClass("UserGLPjd")]
public class UserGLPjd
{
    public UserGLPjd()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    [CSMethod("GetXCUser")]
    public DataTable GetXCUser(string qy_id, string userName)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select a.*,b.User_ID,b.LoginName,b.User_XM,c.QY_NAME from tb_b_YH_YHQX a 
                inner join tb_b_Users b on a.USERID=b.User_ID ";
                if (qy_id != "")
                {
                    cmd.CommandText += " and b.qy_id='" + qy_id + "'";
                }
                if (userName != "")
                {
                    cmd.CommandText += " and b.User_XM like '%" + userName + "%'";
                }
                cmd.CommandText += @"left join tb_b_Eare c on b.QY_ID=c.QY_ID
                where PRIVILEGECODE in (select PRIVILEGECODE from tb_b_YH_QX  where MODULENAME like '巡查人员权限_%' 
                ) and b.User_DelFlag = 0";
                DataTable dt = dbc.ExecuteDataTable(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    [CSMethod("GetPJSDByUser")]
    public DataTable GetPJSDByUser(string userid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select a.pjsd_id,a.PJSD_MC,d.qy_name,a.PJSD_LX,a.PJSD_ISLS,c.User_XM,(case when b.user_id=@user_id then 1 else 0 end) BZ from tb_b_PJSD a left join tb_b_User_PJSD_GL b on a.PJSD_ID=b.PJSD_ID and b.delflag=0 
                        left join tb_b_Users c on b.User_ID=c.User_ID left join tb_b_Eare d on a.qy_id=d.qy_id where a.PJSD_Enable=0 and a.delflag=0 order by BZ desc, b.User_ID,d.qy_code,a.pjsd_lx";
                cmd.Parameters.AddWithValue("@user_id", userid);
                DataTable dt = dbc.ExecuteDataTable(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    [CSMethod("SaveGL")]
    public int SaveGL(string userid, JSReader jsr)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                JSReader[] js = jsr.ToArray();
                string where_dwid = "(";
                for (int i = 0; i < js.Length; i++)
                {
                    if (i == 0)
                    {
                        where_dwid += "'" + js[i].ToString() + "'";
                    }
                    else
                    {
                        where_dwid += ",'" + js[i].ToString() + "'";
                    }
                }
                where_dwid += ")";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"delete from tb_b_User_PJSD_GL where User_ID='" + userid + "' or PJSD_ID in " + where_dwid;
                dbc.ExecuteNonQuery(cmd);



                Pjsd.tb_b_User_PJSD_GLDataTable upg = new Pjsd.tb_b_User_PJSD_GLDataTable();
                for (int i = 0; i < js.Length; i++)
                {
                    Pjsd.tb_b_User_PJSD_GLRow dr = upg.Newtb_b_User_PJSD_GLRow();
                    dr.UGP_id = Guid.NewGuid();
                    dr.User_ID = new Guid(userid);
                    dr.PJSD_ID = new Guid(js[i].ToString());
                    dr.delflag = false;
                    dr.addtime = DateTime.Now;
                    dr.updatetime = DateTime.Now;
                    dr.updateuser = SystemUser.CurrentUser.UserID;
                    upg.Rows.Add(dr);
                }
                dbc.InsertTable(upg);
                dbc.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    [CSMethod("JCGL")]
    public int JCGL(string userid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"delete from tb_b_User_PJSD_GL where User_ID=@User_ID";
                cmd.Parameters.AddWithValue("@User_ID", userid);
                dbc.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
