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
                    sql += "QY_ID,";
                    sql += "ROLE_ID,";
                    sql += "STATUS";
                    sql += ") values(";
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
                    sql += "@AGENT_APPLY_TIME,";
                    sql += "@QY_ID,";
                    sql += "@ROLE_ID,";
                    sql += "@STATUS";
                    sql += ")";

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@AGENT_NAME", jsr["AGENT_NAME"].ToString());
                    cmd.Parameters.Add("@AGENT_LEVEL", jsr["AGENT_LEVEL"].ToString());
                    cmd.Parameters.Add("@AGENT_MOBILE_TEL", jsr["AGENT_MOBILE_TEL"].ToString());
                    cmd.Parameters.Add("@AGENT_EMAIL", jsr["AGENT_EMAIL"].ToString());
                    cmd.Parameters.Add("@DELIVER_ADDRESS", jsr["DELIVER_ADDRESS"].ToString());
                    cmd.Parameters.Add("@CONTACT_TEL", jsr["CONTACT_TEL"].ToString());
                    cmd.Parameters.Add("@AGENT_IDENTITY_NUMBER", jsr["AGENT_IDENTITY_NUMBER"].ToString());
                    cmd.Parameters.Add("@AGENT_CONTRACT_NUMBER,", jsr["AGENT_CONTRACT_NUMBER"].ToString());
                    cmd.Parameters.Add("@AGENT_TYPE", jsr["AGENT_TYPE"].ToString());
                    cmd.Parameters.Add("@RATIO_TYPE", Convert.ToInt32(jsr["RATIO_TYPE"].ToString()));
                    cmd.Parameters.Add("@AGENT_START_TIME", Convert.ToDateTime(jsr["AGENT_START_TIME"].ToString()));
                    cmd.Parameters.Add("@AGENT_END_TIME", Convert.ToDateTime(jsr["AGENT_END_TIME"].ToString()));
                    cmd.Parameters.Add("@AGENT_CONTRACT_STATE", jsr["AGENT_CONTRACT_STATE"].ToString());
                    cmd.Parameters.Add("@AGENT_DEPOSIT", Convert.ToDecimal(jsr["AGENT_DEPOSIT"].ToString()));
                    cmd.Parameters.Add("@AGENT_APPLY_TIME", Convert.ToDateTime(jsr["AGENT_APPLY_TIME"].ToString()));
                    cmd.Parameters.Add("@QY_ID", jsr["QY_ID"].ToString());
                    cmd.Parameters.Add("@ROLE_ID", SystemUser.CurrentUser.UserID);
                    cmd.Parameters.Add("@STATUS", 0);

                    dbc.ExecuteNonQuery(cmd);
                }
                else
                {
                    id = Convert.ToInt32(jsr["ID"].ToString());
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
}