using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartFramework4v2.Web.WebExcutor;
using SmartFramework4v2.Data;
using SmartFramework4v2.Data.SqlServer;
using System.Collections;
using System.Data;
using SmartFramework4v2.Web.Common.JSON;
using System.Data.SqlClient;

/// <summary>
///PjdDB 的摘要说明
/// </summary>
[CSClass("PjdDBClass")]
public class PjdDB
{
    public PjdDB()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    [CSMethod("GetPjdList")]
    public object GetPjdList(string qyid, string pjdmc)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                if (!string.IsNullOrEmpty(qyid))
                {
                    try
                    {
                        Guid guid = new Guid(qyid);
                    }
                    catch
                    {
                        throw new Exception("地区ID出错！");
                    }
                }

                string str = "select a.*,b.QY_Name from tb_b_PJD a,tb_b_Eare b where a.QY_ID=b.QY_ID and a.delflag=0 ";
                if (!string.IsNullOrEmpty(qyid))
                {
                    str += " and a.QY_ID='" + qyid + "'";
                }
                if (!string.IsNullOrEmpty(pjdmc))
                {
                    str += " and a.PJD_Name like '%" + pjdmc.Trim() + "'%";
                }
                
                DataTable dt = dbc.ExecuteDataTable(str);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetSPByPjd")]
    public object GetSPByPjd(string pjdid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string str = "SELECT * FROM tb_b_ProRel where isdel=0 and PJD_ID='" + pjdid + "'";

                DataTable dt = dbc.ExecuteDataTable(str);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("IsGlspBySbsp")]
    public object IsGlspBySbsp(string spsbid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string str = "select product_id from tb_b_ProRel where PG_ID='" + spsbid + "'";

                object obj = dbc.ExecuteScalar(str);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("SaveSbspGlSpId")]
    public object SaveSbspGlSpId(string spsbid,string spid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string str = "update tb_b_ProRel set product_id=@product_id,updatetime=@updatetime where PG_ID=@PG_ID";

                SqlCommand cmd = new SqlCommand(str);
                cmd.Parameters.AddWithValue("@product_id", spid);
                cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                cmd.Parameters.AddWithValue("@PG_ID", spsbid);
                dbc.ExecuteNonQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("SaveSbspGlSpIdNull")]
    public object SaveSbspGlSpIdNull(string spsbid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string str = "update tb_b_ProRel set product_id=null,updatetime=@updatetime where PG_ID=@PG_ID";

                SqlCommand cmd = new SqlCommand(str);
                cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                cmd.Parameters.AddWithValue("@PG_ID", spsbid);
                dbc.ExecuteNonQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
