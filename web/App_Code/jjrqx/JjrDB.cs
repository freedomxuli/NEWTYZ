using MySql.Data.MySqlClient;
using SmartFramework4v2.Data.MySql;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// JjrDB 的摘要说明
/// </summary>
[CSClass("JjrDB")]
public class JjrDB
{
    public JjrDB()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    [CSMethod("SaveDls")]
    public bool SaveDls(JSReader jsr, JSReader fj)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                int id;
                if (jsr["ID"].ToString() == "")
                {
                    DataTable dt = dbc.ExecuteDataTable("select max(ID) ID from tb_b_agent");
                    if (dt.Rows[0]["ID"].ToString() == "")
                        id = 1;
                    else
                        id = Convert.ToInt32(dt.Rows[0]["ID"].ToString()) + 1;
                    string sql = "insert into tb_b_agent(";
                    sql += "AGENT_MC,";
                    sql += "AGENT_NAME,";
                    sql += "AGENT_LEVEL,";
                    sql += "AGENT_MOBILE_TEL,";
                    sql += "AGENT_EMAIL,";
                    sql += "DELIVER_ADDRESS,";
                    sql += "CONTACT_TEL,";
                    sql += "AGENT_IDENTITY_NUMBER,";
                    sql += "AGENT_CONTRACT_NUMBER,";
                    sql += "AGENT_TYPE,";
                    sql += "RATIO_TYPE,";
                    sql += "AGENT_START_TIME,";
                    sql += "AGENT_END_TIME,";
                    sql += "AGENT_CONTRACT_STATE,";
                    sql += "AGENT_DEPOSIT,";
                    //sql += "QY_ID,";
                    sql += "ROLE_ID,";
                    sql += "STATUS,";
                    sql += "ADDTIME,";
                    sql += "ZT";
                    sql += ") values(";
                    sql += "@AGENT_MC,";
                    sql += "@AGENT_NAME,";
                    sql += "@AGENT_LEVEL,";
                    sql += "@AGENT_MOBILE_TEL,";
                    sql += "@AGENT_EMAIL,";
                    sql += "@DELIVER_ADDRESS,";
                    sql += "@CONTACT_TEL,";
                    sql += "@AGENT_IDENTITY_NUMBER,";
                    sql += "@AGENT_CONTRACT_NUMBER,";
                    sql += "@AGENT_TYPE,";
                    sql += "@RATIO_TYPE,";
                    sql += "@AGENT_START_TIME,";
                    sql += "@AGENT_END_TIME,";
                    sql += "@AGENT_CONTRACT_STATE,";
                    sql += "@AGENT_DEPOSIT,";
                    //  sql += "@AGENT_APPLY_TIME,";
                    //sql += "@QY_ID,";
                    sql += "@ROLE_ID,";
                    sql += "@STATUS,";
                    sql += "@ADDTIME,";
                    sql += "@ZT";
                    sql += ")";

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@AGENT_MC", jsr["AGENT_MC"].ToString());
                    cmd.Parameters.Add("@AGENT_NAME", jsr["AGENT_NAME"].ToString());
                    cmd.Parameters.Add("@AGENT_LEVEL", jsr["AGENT_LEVEL"].ToString());
                    cmd.Parameters.Add("@AGENT_MOBILE_TEL", jsr["AGENT_MOBILE_TEL"].ToString());
                    cmd.Parameters.Add("@AGENT_EMAIL", jsr["AGENT_EMAIL"].ToString());
                    cmd.Parameters.Add("@DELIVER_ADDRESS", jsr["DELIVER_ADDRESS"].ToString());
                    cmd.Parameters.Add("@CONTACT_TEL", jsr["CONTACT_TEL"].ToString());
                    cmd.Parameters.Add("@AGENT_IDENTITY_NUMBER", jsr["AGENT_IDENTITY_NUMBER"].ToString());
                    cmd.Parameters.Add("@AGENT_CONTRACT_NUMBER", jsr["AGENT_CONTRACT_NUMBER"].ToString());
                    cmd.Parameters.Add("@AGENT_TYPE", jsr["AGENT_TYPE"].ToString());
                    cmd.Parameters.Add("@RATIO_TYPE", Convert.ToInt32(jsr["RATIO_TYPE"].ToString()));
                    cmd.Parameters.Add("@AGENT_START_TIME", Convert.ToDateTime(jsr["AGENT_START_TIME"].ToString()));
                    cmd.Parameters.Add("@AGENT_END_TIME", Convert.ToDateTime(jsr["AGENT_END_TIME"].ToString()));
                    cmd.Parameters.Add("@AGENT_CONTRACT_STATE", jsr["AGENT_CONTRACT_STATE"].ToString());
                    cmd.Parameters.Add("@AGENT_DEPOSIT", Convert.ToDecimal(jsr["AGENT_DEPOSIT"].ToString()));
                    //  cmd.Parameters.Add("@AGENT_APPLY_TIME", Convert.ToDateTime(jsr["AGENT_APPLY_TIME"].ToString()));
                    //cmd.Parameters.Add("@QY_ID", jsr["QY_ID"].ToString());
                    cmd.Parameters.Add("@ROLE_ID", SystemUser.CurrentUser.UserID);
                    cmd.Parameters.Add("@STATUS", "0");
                    cmd.Parameters.Add("@ADDTIME", DateTime.Now);
                    cmd.Parameters.Add("@ZT", "0");

                    dbc.ExecuteNonQuery(cmd);
                }
                else
                {
                    id = Convert.ToInt32(jsr["ID"].ToString());

                    string sql = "update tb_b_agent set ";
                    sql += "AGENT_MC=@AGENT_MC,";
                    sql += "AGENT_NAME=@AGENT_NAME,";
                    sql += "AGENT_LEVEL=@AGENT_LEVEL,";
                    sql += "AGENT_MOBILE_TEL=@AGENT_MOBILE_TEL,";
                    sql += "AGENT_EMAIL=@AGENT_EMAIL,";
                    sql += "DELIVER_ADDRESS=@DELIVER_ADDRESS,";
                    sql += "CONTACT_TEL=@CONTACT_TEL,";
                    sql += "AGENT_IDENTITY_NUMBER=@AGENT_IDENTITY_NUMBER,";
                    sql += "AGENT_CONTRACT_NUMBER=@AGENT_CONTRACT_NUMBER,";
                    sql += "AGENT_TYPE=@AGENT_TYPE,";
                    sql += "RATIO_TYPE=@RATIO_TYPE,";
                    sql += "AGENT_START_TIME=@AGENT_START_TIME,";
                    sql += "AGENT_END_TIME=@AGENT_END_TIME,";
                    sql += "AGENT_CONTRACT_STATE=@AGENT_CONTRACT_STATE,";
                    sql += "AGENT_DEPOSIT=@AGENT_DEPOSIT,";
                    //sql += "QY_ID,";
                    sql += "ROLE_ID=@ROLE_ID,";
                    sql += "UPDATETIME=@UPDATETIME";
                    sql += " where ID=" + id;


                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@AGENT_MC", jsr["AGENT_MC"].ToString());
                    cmd.Parameters.Add("@AGENT_NAME", jsr["AGENT_NAME"].ToString());
                    cmd.Parameters.Add("@AGENT_LEVEL", jsr["AGENT_LEVEL"].ToString());
                    cmd.Parameters.Add("@AGENT_MOBILE_TEL", jsr["AGENT_MOBILE_TEL"].ToString());
                    cmd.Parameters.Add("@AGENT_EMAIL", jsr["AGENT_EMAIL"].ToString());
                    cmd.Parameters.Add("@DELIVER_ADDRESS", jsr["DELIVER_ADDRESS"].ToString());
                    cmd.Parameters.Add("@CONTACT_TEL", jsr["CONTACT_TEL"].ToString());
                    cmd.Parameters.Add("@AGENT_IDENTITY_NUMBER", jsr["AGENT_IDENTITY_NUMBER"].ToString());
                    cmd.Parameters.Add("@AGENT_CONTRACT_NUMBER", jsr["AGENT_CONTRACT_NUMBER"].ToString());
                    cmd.Parameters.Add("@AGENT_TYPE", jsr["AGENT_TYPE"].ToString());
                    cmd.Parameters.Add("@RATIO_TYPE", Convert.ToInt32(jsr["RATIO_TYPE"].ToString()));
                    cmd.Parameters.Add("@AGENT_START_TIME", Convert.ToDateTime(jsr["AGENT_START_TIME"].ToString()));
                    cmd.Parameters.Add("@AGENT_END_TIME", Convert.ToDateTime(jsr["AGENT_END_TIME"].ToString()));
                    cmd.Parameters.Add("@AGENT_CONTRACT_STATE", jsr["AGENT_CONTRACT_STATE"].ToString());
                    cmd.Parameters.Add("@AGENT_DEPOSIT", Convert.ToDecimal(jsr["AGENT_DEPOSIT"].ToString()));
                    //  cmd.Parameters.Add("@AGENT_APPLY_TIME", Convert.ToDateTime(jsr["AGENT_APPLY_TIME"].ToString()));
                    //cmd.Parameters.Add("@QY_ID", jsr["QY_ID"].ToString());
                    cmd.Parameters.Add("@ROLE_ID", SystemUser.CurrentUser.UserID);
                    cmd.Parameters.Add("@UPDATETIME", DateTime.Now);
                    dbc.ExecuteNonQuery(cmd);
                }

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
                    dbc.ExecuteNonQuery("update tb_b_fj set fj_pid='" + id + "' where status=0 and fj_id in(" + fj_ids + ")");
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

                string str = "select a.*,b.QY_NAME,c.User_XM from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 ";
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
    public object GetDLsById(int dls_id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sql = "select * from tb_b_agent where ID=" + dls_id;

                DataTable dt = dbc.ExecuteDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}