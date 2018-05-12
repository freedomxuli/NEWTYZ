using SmartFramework4v2.Data;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

[CSClass("SystemDB")]
public class SystemDB
{
    #region 字典表
    [CSMethod("GetZDBLXList")]
    public object GetZDBLXList(int pagnum, int pagesize)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string sql = "select * from Lock_ZDBLX where ZT=0 order by LXID";

                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(sql, pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("SaveZDBLX")]
    public object SaveZDBLX(JSReader jsr)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                string id = jsr["LXID"].ToString();
                string LXMC = jsr["LXMC"].ToString();

                if (string.IsNullOrEmpty(id))
                {
                    DataTable dt = dbc.GetEmptyDataTable("Lock_ZDBLX");
                    DataRow dr = dt.NewRow();
                    dr["LXMC"] = LXMC;
                    //dr["LX"] = LX;
                    dr["ZT"] = 0;
                    dt.Rows.Add(dr);
                    dbc.InsertTable(dt);
                }
                else
                {
                    dbc.ExecuteNonQuery("update Lock_ZDBLX set LXMC=" + dbc.ToSqlValue(LXMC) + " where LXID=" + Convert.ToInt16(id));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("DeleteZDBLX")]
    public object DeleteZDBLX(JSReader jsr)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            dbc.BeginTransaction();
            try
            {
                for (int i = 0; i < jsr.ToArray().Length; i++)
                {
                    int ID = jsr.ToArray()[i].ToInteger();
                    dbc.ExecuteNonQuery("update Lock_ZDBLX set ZT=1 where LXID=" + ID);
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

    [CSMethod("GetZDBLX")]
    public object GetZDBLX(int ID)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                string sqlStr = "select * from Lock_ZDBLX where LXID=" + ID;
                DataTable dt = dbc.ExecuteDataTable(sqlStr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetZDBList")]
    public object GetZDBList(int pagnum, int pagesize, string lx)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";
                if (!string.IsNullOrEmpty(lx))
                    where = " and LX=" + lx;

                string sql = "select * from Lock_ZDB where ZT=0 " + where + " order by XH";

                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(sql, pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("SaveZDB")]
    public object SaveZDB(JSReader jsr)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                string id = jsr["ZDBID"].ToString();
                string MC = jsr["MC"].ToString();
                int LX = Convert.ToInt16(jsr["LX"].ToString());
                string XH = jsr["XH"].ToString();
                if (string.IsNullOrEmpty(id))
                {
                    DataTable dt = dbc.GetEmptyDataTable("Lock_ZDB");
                    DataRow dr = dt.NewRow();
                    dr["MC"] = MC;
                    dr["LX"] = LX;
                    dr["XH"] = XH;
                    dr["ZT"] = 0;
                    dt.Rows.Add(dr);
                    dbc.InsertTable(dt);
                }
                else
                {
                    DataTable dt = dbc.GetEmptyDataTable("Lock_ZDB");
                    dt.Columns["ZDBID"].ReadOnly = false;
                    DataTableTracker dtt = new DataTableTracker(dt);
                    DataRow dr = dt.NewRow();
                    dr["ZDBID"] = id;
                    dr["MC"] = MC;
                    dr["LX"] = LX;
                    dr["XH"] = XH;
                    dr["ZT"] = 0;
                    dt.Rows.Add(dr);
                    dbc.UpdateTable(dt, dtt);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("DeleteZDB")]
    public object DeleteZDB(JSReader jsr)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            dbc.BeginTransaction();
            try
            {
                for (int i = 0; i < jsr.ToArray().Length; i++)
                {
                    int ID = jsr.ToArray()[i].ToInteger();
                    dbc.ExecuteNonQuery("update Lock_ZDB set ZT=1 where ZDBID=" + ID);
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

    [CSMethod("GetZDB")]
    public object GetZDB(int ID)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                string sqlStr = "select * from Lock_ZDB where ZDBID=" + ID;
                DataTable dt = dbc.ExecuteDataTable(sqlStr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetZDLXCombo")]
    public object GetZDLXCombo()
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                string sqlStr = "select LXID,LXMC from Lock_ZDBLX where ZT=0";
                DataTable dt = dbc.ExecuteDataTable(sqlStr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    #endregion
}