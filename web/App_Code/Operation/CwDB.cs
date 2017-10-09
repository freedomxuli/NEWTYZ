using MySql.Data.MySqlClient;
using SmartFramework4v2.Data.MySql;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// CwDB 的摘要说明
/// </summary>
[CSClass("CwDB")]
public class CwDB
{
    public CwDB()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    [CSMethod("GetDlsList")]
    public object GetDlsList(int pagnum, int pagesize, string mc, string xm, string qy)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";
                if (!string.IsNullOrEmpty(mc))
                {
                    where += " and AGENT_MC like'%" + mc + "%'";
                }
                if (!string.IsNullOrEmpty(xm))
                {
                    where += " and AGENT_NAME like'%" + xm + "%'";
                }
                if (!string.IsNullOrEmpty(qy))
                {
                    where += " and QY_ID='" + qy + "'";
                }

                string str = "select a.*,b.QY_NAME,c.User_XM from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and a.ZT=1 ";
                str += where;

                //开始取分页数据
                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(str + " order by addtime desc", pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    [CSMethod("GetDLsById")]
    public DataTable GetDLsById(int dls_id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sql = "select a.*,b.QY_NAME,c.User_XM from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where ID=" + dls_id;

                DataTable dt = dbc.ExecuteDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetFdList")]
    public object GetFdList(int pagnum, int pagesize, string mc, string xm, string qy)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";
                if (!string.IsNullOrEmpty(mc))
                {
                    where += " and LANDLORD_MC like'%" + mc + "%'";
                }
                if (!string.IsNullOrEmpty(xm))
                {
                    where += " and LANDLORD_NAME like'%" + xm + "%'";
                }
                if (!string.IsNullOrEmpty(qy))
                {
                    where += " and QY_ID='" + qy + "'";
                }

                string str = "select a.*,b.QY_NAME,c.User_XM from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and a.ZT=1";
                str += where;

                //开始取分页数据
                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(str + " order by addtime desc", pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    [CSMethod("GetFdById")]
    public DataTable GetFdById(int fd_id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sql = "select a.*,b.QY_NAME,c.User_XM from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where ID=" + fd_id;

                DataTable dt = dbc.ExecuteDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("AgreeDls")]
    public bool AgreeDls(int id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_agent set ZT=2,UPDATETIME=sysdate() where id=" + id);

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

    [CSMethod("AgreeFd")]
    public bool AgreeFd(int id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_landlord set ZT=2,UPDATETIME=sysdate() where id=" + id);

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

    [CSMethod("GetCWJSList")]
    public object GetCWJSList(int pagnum, int pagesize, string jsmc, string xm, string nc, string zt)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";
                if (!string.IsNullOrEmpty(xm))
                {
                    where += " and UserName like'%" + xm + "%'";
                }
                if (!string.IsNullOrEmpty(nc))
                {
                    where += " and NickName like'%" + nc + "%'";
                }
                if (!string.IsNullOrEmpty(zt))
                {
                    where += " and a.status=" + zt;
                }


                string str = @"select a.*,b.* from Lock_HotelWithdrawals a 
                               left join aspnet_Users b on a.UserId=b.UserId
                               left join aspnet_UsersInRoles c on a.UserId=c.UserId
                               left join aspnet_Roles d on c.RoleId=d.RoleId
                               where a.PayType=2 and d.RoleName=" + dbc.ToSqlValue(jsmc) + " " + where;
                str += where;

                //开始取分页数据
                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(str + " order by a.CreateTime desc", pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

  

}