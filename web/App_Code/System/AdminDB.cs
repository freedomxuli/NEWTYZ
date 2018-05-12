using DB;
using MySql.Data.MySqlClient;
using SmartFramework4v2.Data.MySql;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

[CSClass("AdminDB")]
public class AdminDB
{
    public AdminDB()
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

                string str = @"select a.*,b.QY_NAME,c.User_XM from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id 
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=0 and d.SERVICETYPE='代理商申请' and d.STATUS=0 and e.RESULT=0";
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


    [CSMethod("SaveFdXX")]
    public bool SaveFdXX(JSReader jsr, JSReader fj)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                int id;
                if (jsr["ID"].ToString() == "")
                {
                    id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_landlord'").ToString());

                    string sql = "insert into tb_b_landlord(";
                    sql += "LANDLORD_MC,";
                    sql += "LANDLORD_NAME,";
                    sql += "LANDLORD_MOBILE_TEL,";
                    sql += "LANDLORD_EMAIL,";
                    sql += "ROOM_TYPE,";
                    sql += "ROOM_ATTRIBUTE,";
                    sql += "COMMISSION_TYPE,";
                    sql += "SETTLEMENT_CYCLE,";
                    sql += "COMMISSION_RATIO,";
                    sql += "LEASE_TYPE,";
                    sql += "LANDLORD_IDENTITY_NUMBER,";
                    sql += "LANDLORD_CONTRACT_NUMBER,";
                    sql += "LANDLORD_START_TIME,";
                    sql += "LANDLORD_END_TIME,";
                    sql += "LANDLORD_CONTRACT_STATE,";
                    sql += "LANDLORD_DEPOSIT,";
                    sql += "DELIVER_ADDRESS,";
                    sql += "CONTACT_TEL,";
                    sql += "QY_ID,";
                    sql += "ROLE_ID,";
                    sql += "STATUS,";
                    sql += "ADDTIME,";
                    sql += "ZT";
                    sql += ") values(";
                    sql += "@LANDLORD_MC,";
                    sql += "@LANDLORD_NAME,";
                    sql += "@LANDLORD_MOBILE_TEL,";
                    sql += "@LANDLORD_EMAIL,";
                    sql += "@ROOM_TYPE,";
                    sql += "@ROOM_ATTRIBUTE,";
                    sql += "@COMMISSION_TYPE,";
                    sql += "@SETTLEMENT_CYCLE,";
                    sql += "@COMMISSION_RATIO,";
                    sql += "@LEASE_TYPE,";
                    sql += "@LANDLORD_IDENTITY_NUMBER,";
                    sql += "@LANDLORD_CONTRACT_NUMBER,";
                    sql += "@LANDLORD_START_TIME,";
                    sql += "@LANDLORD_END_TIME,";
                    sql += "@LANDLORD_CONTRACT_STATE,";
                    sql += "@LANDLORD_DEPOSIT,";
                    sql += "@DELIVER_ADDRESS,";
                    sql += "@CONTACT_TEL,";
                    sql += "@QY_ID,";
                    sql += "@ROLE_ID,";
                    sql += "@STATUS,";
                    sql += "@ADDTIME,";
                    sql += "@ZT";
                    sql += ")";

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@LANDLORD_MC", jsr["LANDLORD_MC"].ToString());
                    cmd.Parameters.Add("@LANDLORD_NAME", jsr["LANDLORD_NAME"].ToString());
                    cmd.Parameters.Add("@LANDLORD_MOBILE_TEL", jsr["LANDLORD_MOBILE_TEL"].ToString());
                    cmd.Parameters.Add("@LANDLORD_EMAIL", jsr["LANDLORD_EMAIL"].ToString());
                    cmd.Parameters.Add("@ROOM_TYPE", jsr["ROOM_TYPE"].ToString());
                    cmd.Parameters.Add("@ROOM_ATTRIBUTE", jsr["ROOM_ATTRIBUTE"].ToString());
                    cmd.Parameters.Add("@COMMISSION_TYPE", jsr["COMMISSION_TYPE"].ToString());
                    cmd.Parameters.Add("@SETTLEMENT_CYCLE", jsr["SETTLEMENT_CYCLE"].ToString());
                    cmd.Parameters.Add("@COMMISSION_RATIO", Convert.ToInt16(jsr["COMMISSION_RATIO"].ToString()));
                    cmd.Parameters.Add("@LEASE_TYPE", jsr["LEASE_TYPE"].ToString());
                    cmd.Parameters.Add("@LANDLORD_IDENTITY_NUMBER", jsr["LANDLORD_IDENTITY_NUMBER"].ToString());
                    cmd.Parameters.Add("@LANDLORD_CONTRACT_NUMBER", jsr["LANDLORD_CONTRACT_NUMBER"].ToString());
                    cmd.Parameters.Add("@LANDLORD_START_TIME", Convert.ToDateTime(jsr["LANDLORD_START_TIME"].ToString()));
                    cmd.Parameters.Add("@LANDLORD_END_TIME", Convert.ToDateTime(jsr["LANDLORD_END_TIME"].ToString()));
                    cmd.Parameters.Add("@LANDLORD_CONTRACT_STATE", jsr["LANDLORD_CONTRACT_STATE"].ToString());
                    cmd.Parameters.Add("@LANDLORD_DEPOSIT", Convert.ToDecimal(jsr["LANDLORD_DEPOSIT"].ToString()));
                    cmd.Parameters.Add("@DELIVER_ADDRESS", jsr["DELIVER_ADDRESS"].ToString());
                    cmd.Parameters.Add("@CONTACT_TEL", jsr["CONTACT_TEL"].ToString());
                    //  cmd.Parameters.Add("@AGENT_APPLY_TIME", Convert.ToDateTime(jsr["AGENT_APPLY_TIME"].ToString()));
                    cmd.Parameters.Add("@QY_ID", Convert.ToInt16(jsr["QY_ID"].ToString()));
                    cmd.Parameters.Add("@ROLE_ID", SystemUser.CurrentUser.UserID);
                    cmd.Parameters.Add("@STATUS", "0");
                    cmd.Parameters.Add("@ADDTIME", DateTime.Now);
                    cmd.Parameters.Add("@ZT", "0");

                    dbc.ExecuteNonQuery(cmd);
                }
                else
                {
                    id = Convert.ToInt32(jsr["ID"].ToString());

                    string sql = "update tb_b_landlord set ";
                    sql += "LANDLORD_MC=@LANDLORD_MC,";
                    sql += "LANDLORD_NAME=@LANDLORD_NAME,";
                    sql += "LANDLORD_MOBILE_TEL=@LANDLORD_MOBILE_TEL,";
                    sql += "LANDLORD_EMAIL=@LANDLORD_EMAIL,";
                    sql += "ROOM_TYPE=@ROOM_TYPE,";
                    sql += "ROOM_ATTRIBUTE=@ROOM_ATTRIBUTE,";
                    sql += "COMMISSION_TYPE=@COMMISSION_TYPE,";
                    sql += "SETTLEMENT_CYCLE=@SETTLEMENT_CYCLE,";
                    sql += "COMMISSION_RATIO=@COMMISSION_RATIO,";
                    sql += "LEASE_TYPE=@LEASE_TYPE,";
                    sql += "LANDLORD_IDENTITY_NUMBER=@LANDLORD_IDENTITY_NUMBER,";
                    sql += "LANDLORD_CONTRACT_NUMBER=@LANDLORD_CONTRACT_NUMBER,";
                    sql += "LANDLORD_START_TIME=@LANDLORD_START_TIME,";
                    sql += "LANDLORD_END_TIME=@LANDLORD_END_TIME,";
                    sql += "LANDLORD_CONTRACT_STATE=@LANDLORD_CONTRACT_STATE,";
                    sql += "LANDLORD_DEPOSIT=@LANDLORD_DEPOSIT,";
                    sql += "DELIVER_ADDRESS=@DELIVER_ADDRESS,";
                    sql += "CONTACT_TEL=@CONTACT_TEL,";
                    //sql += "QY_ID,";
                    sql += "ROLE_ID=@ROLE_ID,";
                    sql += "UPDATETIME=@UPDATETIME";
                    sql += " where ID=" + id;

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@LANDLORD_MC", jsr["LANDLORD_MC"].ToString());
                    cmd.Parameters.Add("@LANDLORD_NAME", jsr["LANDLORD_NAME"].ToString());
                    cmd.Parameters.Add("@LANDLORD_MOBILE_TEL", jsr["LANDLORD_MOBILE_TEL"].ToString());
                    cmd.Parameters.Add("@LANDLORD_EMAIL", jsr["LANDLORD_EMAIL"].ToString());
                    cmd.Parameters.Add("@ROOM_TYPE", jsr["ROOM_TYPE"].ToString());
                    cmd.Parameters.Add("@ROOM_ATTRIBUTE", jsr["ROOM_ATTRIBUTE"].ToString());
                    cmd.Parameters.Add("@COMMISSION_TYPE", jsr["COMMISSION_TYPE"].ToString());
                    cmd.Parameters.Add("@SETTLEMENT_CYCLE", jsr["SETTLEMENT_CYCLE"].ToString());
                    cmd.Parameters.Add("@COMMISSION_RATIO", Convert.ToInt16(jsr["COMMISSION_RATIO"].ToString()));
                    cmd.Parameters.Add("@LEASE_TYPE", jsr["LEASE_TYPE"].ToString());
                    cmd.Parameters.Add("@LANDLORD_IDENTITY_NUMBER", jsr["LANDLORD_IDENTITY_NUMBER"].ToString());
                    cmd.Parameters.Add("@LANDLORD_CONTRACT_NUMBER", jsr["LANDLORD_CONTRACT_NUMBER"].ToString());
                    cmd.Parameters.Add("@LANDLORD_START_TIME", Convert.ToDateTime(jsr["LANDLORD_START_TIME"].ToString()));
                    cmd.Parameters.Add("@LANDLORD_END_TIME", Convert.ToDateTime(jsr["LANDLORD_END_TIME"].ToString()));
                    cmd.Parameters.Add("@LANDLORD_CONTRACT_STATE", jsr["LANDLORD_CONTRACT_STATE"].ToString());
                    cmd.Parameters.Add("@LANDLORD_DEPOSIT", Convert.ToDecimal(jsr["LANDLORD_DEPOSIT"].ToString()));
                    cmd.Parameters.Add("@DELIVER_ADDRESS", jsr["DELIVER_ADDRESS"].ToString());
                    cmd.Parameters.Add("@CONTACT_TEL", jsr["CONTACT_TEL"].ToString());
                    cmd.Parameters.Add("@QY_ID", Convert.ToInt16(jsr["QY_ID"].ToString()));
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

                string str = @"select a.*,b.QY_NAME,c.User_XM,e.* from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id  
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=0 and d.SERVICETYPE='房东申请' and d.STATUS=0 and e.RESULT=0";
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

    [CSMethod("SaveBjXX")]
    public bool SaveBjXX(JSReader jsr, JSReader fj)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                int id;
                if (jsr["ID"].ToString() == "")
                {
                    id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_cleaning'").ToString());

                    string sql = "insert into tb_b_cleaning(";
                    sql += "CLEANING_NAME,";
                    sql += "CLEANING_AGE,";
                    sql += "CLEANING_SEX,";
                    sql += "CLEANING_MOBILE_TEL,";
                    sql += "CLEANING_IDENTITY_NUMBER,";
                    sql += "CLEANING_SUBORDINATE,";
                    sql += "CLEANING_SALARY,";
                    sql += "CLEANING_INSURANCE,";
                    sql += "COMMISSION_SALARY,";
                    sql += "COMMISSION_TYPE,";
                    sql += "CONTRACT_START_TIME,";
                    sql += "CONTRACT_END_TIME,";
                    sql += "STATUS,";
                    sql += "ADDTIME,";
                    sql += "ZT";
                    sql += ") values(";
                    sql += "@CLEANING_NAME,";
                    sql += "@CLEANING_AGE,";
                    sql += "@CLEANING_SEX,";
                    sql += "@CLEANING_MOBILE_TEL,";
                    sql += "@CLEANING_IDENTITY_NUMBER,";
                    sql += "@CLEANING_SUBORDINATE,";
                    sql += "@CLEANING_SALARY,";
                    sql += "@CLEANING_INSURANCE,";
                    sql += "@COMMISSION_SALARY,";
                    sql += "@COMMISSION_TYPE,";
                    sql += "@CONTRACT_START_TIME,";
                    sql += "@CONTRACT_END_TIME,";
                    sql += "@STATUS,";
                    sql += "@ADDTIME,";
                    sql += "@ZT";
                    sql += ")";

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@CLEANING_NAME", jsr["CLEANING_NAME"].ToString());
                    cmd.Parameters.Add("@CLEANING_AGE", jsr["CLEANING_AGE"].ToString());
                    cmd.Parameters.Add("@CLEANING_SEX", jsr["CLEANING_SEX"].ToString());
                    cmd.Parameters.Add("@CLEANING_MOBILE_TEL", jsr["CLEANING_MOBILE_TEL"].ToString());
                    cmd.Parameters.Add("@CLEANING_IDENTITY_NUMBER", jsr["CLEANING_IDENTITY_NUMBER"].ToString());
                    cmd.Parameters.Add("@CLEANING_SUBORDINATE", jsr["CLEANING_SUBORDINATE"].ToString());
                    cmd.Parameters.Add("@CLEANING_SALARY", Convert.ToDecimal(jsr["CLEANING_SALARY"].ToString()));
                    cmd.Parameters.Add("@CLEANING_INSURANCE", jsr["CLEANING_INSURANCE"].ToString());
                    cmd.Parameters.Add("@COMMISSION_SALARY", Convert.ToDecimal(jsr["COMMISSION_SALARY"].ToString()));
                    cmd.Parameters.Add("@COMMISSION_TYPE", jsr["COMMISSION_TYPE"].ToString());

                    cmd.Parameters.Add("@CONTRACT_START_TIME", Convert.ToDateTime(jsr["CONTRACT_START_TIME"].ToString()));
                    cmd.Parameters.Add("@CONTRACT_END_TIME", Convert.ToDateTime(jsr["CONTRACT_END_TIME"].ToString()));

                    cmd.Parameters.Add("@STATUS", "0");
                    cmd.Parameters.Add("@ADDTIME", DateTime.Now);
                    cmd.Parameters.Add("@ZT", "0");

                    dbc.ExecuteNonQuery(cmd);
                }
                else
                {
                    id = Convert.ToInt32(jsr["ID"].ToString());

                    string sql = "update tb_b_cleaning set ";
                    sql += "CLEANING_NAME=@CLEANING_NAME,";
                    sql += "CLEANING_AGE=@CLEANING_AGE,";
                    sql += "CLEANING_SEX=@CLEANING_SEX,";
                    sql += "CLEANING_MOBILE_TEL=@CLEANING_MOBILE_TEL,";
                    sql += "CLEANING_IDENTITY_NUMBER=@CLEANING_IDENTITY_NUMBER,";
                    sql += "CLEANING_SUBORDINATE=@CLEANING_SUBORDINATE,";
                    sql += "CLEANING_SALARY=@CLEANING_SALARY,";
                    sql += "CLEANING_INSURANCE=@CLEANING_INSURANCE,";
                    sql += "COMMISSION_SALARY=@COMMISSION_SALARY,";
                    sql += "COMMISSION_TYPE=@COMMISSION_TYPE,";
                    sql += "CONTRACT_START_TIME=@CONTRACT_START_TIME,";
                    sql += "CONTRACT_END_TIME=@CONTRACT_END_TIME,";

                    sql += "UPDATETIME=@UPDATETIME";
                    sql += " where ID=" + id;

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@LANDLORD_MC", jsr["LANDLORD_MC"].ToString());
                    cmd.Parameters.Add("@CLEANING_NAME", jsr["CLEANING_NAME"].ToString());
                    cmd.Parameters.Add("@CLEANING_AGE", jsr["CLEANING_AGE"].ToString());
                    cmd.Parameters.Add("@CLEANING_SEX", jsr["CLEANING_SEX"].ToString());
                    cmd.Parameters.Add("@CLEANING_MOBILE_TEL", jsr["CLEANING_MOBILE_TEL"].ToString());
                    cmd.Parameters.Add("@CLEANING_IDENTITY_NUMBER", jsr["CLEANING_IDENTITY_NUMBER"].ToString());
                    cmd.Parameters.Add("@CLEANING_SUBORDINATE", jsr["CLEANING_SUBORDINATE"].ToString());
                    cmd.Parameters.Add("@CLEANING_SALARY", Convert.ToDecimal(jsr["CLEANING_SALARY"].ToString()));
                    cmd.Parameters.Add("@CLEANING_INSURANCE", jsr["CLEANING_INSURANCE"].ToString());
                    cmd.Parameters.Add("@COMMISSION_SALARY", Convert.ToDecimal(jsr["COMMISSION_SALARY"].ToString()));
                    cmd.Parameters.Add("@COMMISSION_TYPE", jsr["COMMISSION_TYPE"].ToString());

                    cmd.Parameters.Add("@CONTRACT_START_TIME", Convert.ToDateTime(jsr["CONTRACT_START_TIME"].ToString()));
                    cmd.Parameters.Add("@CONTRACT_END_TIME", Convert.ToDateTime(jsr["CONTRACT_END_TIME"].ToString()));

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

    [CSMethod("GetBjList")]
    public object GetBjList(int pagnum, int pagesize, string xm)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";
                if (!string.IsNullOrEmpty(xm))
                {
                    where += " and CLEANING_NAME like'%" + xm + "%'";
                }

                string str = @"select a.*,b.QY_NAME,c.User_XM,e.* from tb_b_cleaning a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id  
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=0 and d.SERVICETYPE='保洁申请' and d.STATUS=0 and e.RESULT=0";
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

    [CSMethod("GetBjById")]
    public DataTable GetBjById(int bj_id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sql = "select * from tb_b_cleaning where ID=" + bj_id;

                DataTable dt = dbc.ExecuteDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("AgreeFd")]
    public bool AgreeFd(int id, int flowId, int stepId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {

                DataTable dt = GetFdById(id);
                int user_id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_users'").ToString());
                string sql = @"insert into tb_b_users(LoginName,Password,User_XM,StartDate,EndDate,User_Enable,User_Delflag,AddTime)  
                             values(@LoginName,@Password,@User_XM,@StartDate,@EndDate,@User_Enable,@User_Delflag,@AddTime)";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.Add("@LoginName", dt.Rows[0]["LoginName"].ToString());
                cmd.Parameters.Add("@Password", dt.Rows[0]["PassWord"].ToString());
                cmd.Parameters.Add("@User_XM", dt.Rows[0]["LANDLORD_NAME"].ToString());
                cmd.Parameters.Add("@StartDate", Convert.ToDateTime(dt.Rows[0]["LANDLORD_START_TIME"].ToString()));
                cmd.Parameters.Add("@EndDate", Convert.ToDateTime(dt.Rows[0]["LANDLORD_END_TIME"].ToString()));
                cmd.Parameters.Add("@User_Enable", Convert.ToInt16("0"));
                cmd.Parameters.Add("@User_Delflag", Convert.ToInt16("0"));
                cmd.Parameters.Add("@AddTime", DateTime.Now);
                dbc.ExecuteNonQuery(cmd);

                dbc.ExecuteNonQuery("insert into tb_b_user_js_gl(User_ID,JS_ID,delflag,addtime) values(" + user_id + ",8,0,sysdate())");
                dbc.ExecuteNonQuery("update tb_b_landlord set ZT=1,User_ID=" + user_id + ",ADMIN_ID=" + SystemUser.CurrentUser.UserID + ",DealerAuthoriCode='" + DateTime.Now.ToString("yyMMddss") + "',COMFIRM_TIME=sysdate() where id=" + id);
                Flow.FinishFlow(dbc, flowId);
                Flow.FinishStep(dbc, stepId, 1, "审核通过");

                InsertUser(dt.Rows[0]["LANDLORD_MOBILE_TEL"].ToString(), dt.Rows[0]["PassWord"].ToString(), dt.Rows[0]["LANDLORD_NAME"].ToString(), "55B5B7EE-6046-464B-B4A0-0D4151C38097");

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

    [CSMethod("AgreeDls")]
    public bool AgreeDls(int id, int flowId, int stepId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                DataTable dt = GetDLsById(id);
                int user_id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_users'").ToString());
                string sql = @"insert into tb_b_users(LoginName,Password,User_XM,StartDate,EndDate,User_Enable,User_Delflag,AddTime)  
                             values(@LoginName,@Password,@User_XM,@StartDate,@EndDate,@User_Enable,@User_Delflag,@AddTime)";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.Add("@LoginName", dt.Rows[0]["LoginName"].ToString());
                cmd.Parameters.Add("@Password", dt.Rows[0]["PassWord"].ToString());
                cmd.Parameters.Add("@User_XM", dt.Rows[0]["AGENT_NAME"].ToString());
                cmd.Parameters.Add("@StartDate", Convert.ToDateTime(dt.Rows[0]["AGENT_START_TIME"].ToString()));
                cmd.Parameters.Add("@EndDate", Convert.ToDateTime(dt.Rows[0]["AGENT_END_TIME"].ToString()));
                cmd.Parameters.Add("@User_Enable", Convert.ToInt16("0"));
                cmd.Parameters.Add("@User_Delflag", Convert.ToInt16("0"));
                cmd.Parameters.Add("@AddTime", DateTime.Now);
                dbc.ExecuteNonQuery(cmd);

                dbc.ExecuteNonQuery("insert into tb_b_user_js_gl(User_ID,JS_ID,delflag,addtime) values(" + user_id + ",9,0,sysdate())");
                dbc.ExecuteNonQuery("update tb_b_agent set ZT=1,User_ID=" + user_id + ",ADMIN_ID=" + SystemUser.CurrentUser.UserID + ",DealerAuthoriCode='" + DateTime.Now.ToString("yyMMddss") + "',COMFIRM_TIME=sysdate() where id=" + id);
                Flow.FinishFlow(dbc, flowId);
                Flow.FinishStep(dbc, stepId, 1, "审核通过");

                //InsertUser(dt.Rows[0]["LoginName"].ToString(), dt.Rows[0]["PassWord"].ToString(), dt.Rows[0]["AGENT_NAME"].ToString());
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

    [CSMethod("NoAgreeDls")]
    public bool NoAgreeDls(int id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_agent set ZT=-1,ADMIN_ID=" + SystemUser.CurrentUser.UserID + ",COMFIRM_TIME=sysdate() where id=" + id);

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



    public void InsertUser(string loginName, string passWord, string userName, string rolid)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            dbc.BeginTransaction();
            try
            {
                DataTable dt = dbc.ExecuteDataTable("select * from aspnet_Users where LoweredUserName='" + loginName + "'");
                if (dt.Rows.Count == 0)
                {
                    int userId = Convert.ToInt16(dbc.ExecuteScalar("SELECT IDENT_CURRENT('aspnet_Users') + IDENT_INCR('aspnet_Users')").ToString());
                    var dtUser = dbc.GetEmptyDataTable("aspnet_Users");
                    var drUser = dtUser.NewRow();
                    drUser["UserName"] = loginName;
                    drUser["LoweredUserName"] = loginName;
                    drUser["PassWord"] = passWord;
                    drUser["IsAnonymous"] = 0;
                    drUser["PasswordFormat"] = 0;
                    drUser["IsApproved"] = 1;
                    drUser["IsLockedOut"] = 0;
                    drUser["CreateDate"] = DateTime.Now;
                    drUser["LastActivityDate"] = DateTime.Now;
                    drUser["PasswordSalt"] = "";
                    drUser["LastLoginDate"] = DateTime.Now;
                    drUser["LastPasswordChangedDate"] = DateTime.Now;
                    drUser["LastLockoutDate"] = DateTime.Now;
                    drUser["FailedPasswordAttemptWindowStart"] = DateTime.Now;
                    drUser["FailedPasswordAnswerAttemptWindowStart"] = DateTime.Now;
                    drUser["FailedPasswordAnswerAttemptCount"] = 0;
                    drUser["FailedPasswordAttemptCount"] = 0;
                    drUser["SessionId"] = Guid.NewGuid().ToString();
                    dtUser.Rows.Add(drUser);

                    var dtRole = dbc.GetEmptyDataTable("aspnet_UsersInRoles");
                    var drRole = dtRole.NewRow();
                    drRole["UserId"] = userId;
                    drRole["RoleId"] = rolid;
                    dtRole.Rows.Add(drRole);

                    drRole = dtRole.NewRow();
                    drRole["UserId"] = userId;
                    drRole["RoleId"] = "E5AD331C-8CC4-4B3E-9B9D-658CB3DD5AE4";
                    dtRole.Rows.Add(drRole);

                    dbc.InsertTable(dtUser);
                    dbc.InsertTable(dtRole);
                }
                dbc.CommitTransaction();

            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }



    [CSMethod("NoAgreeFd")]
    public bool NoAgreeFd(int id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_landlord set ZT=-1,ADMIN_ID=" + SystemUser.CurrentUser.UserID + ",COMFIRM_TIME=sysdate() where id=" + id);

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

    [CSMethod("AgreeBj")]
    public bool AgreeBj(int id, int flowId, int stepId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                DataTable dt = GetBjById(id);
                int user_id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_users'").ToString());
                string sql = @"insert into tb_b_users(LoginName,Password,User_XM,StartDate,EndDate,User_Enable,User_Delflag,AddTime)  
                             values(@LoginName,@Password,@User_XM,@StartDate,@EndDate,@User_Enable,@User_Delflag,@AddTime)";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.Add("@LoginName", dt.Rows[0]["LoginName"].ToString());
                cmd.Parameters.Add("@Password", dt.Rows[0]["Password"].ToString());
                cmd.Parameters.Add("@User_XM", dt.Rows[0]["CLEANING_NAME"].ToString());
                cmd.Parameters.Add("@StartDate", Convert.ToDateTime(dt.Rows[0]["CONTRACT_START_TIME"].ToString()));
                cmd.Parameters.Add("@EndDate", Convert.ToDateTime(dt.Rows[0]["CONTRACT_END_TIME"].ToString()));
                cmd.Parameters.Add("@User_Enable", Convert.ToInt16("0"));
                cmd.Parameters.Add("@User_Delflag", Convert.ToInt16("0"));
                cmd.Parameters.Add("@AddTime", DateTime.Now);
                dbc.ExecuteNonQuery(cmd);

                dbc.ExecuteNonQuery("insert into tb_b_user_js_gl(User_ID,JS_ID,delflag,addtime) values(" + user_id + ",10,0,sysdate())");


                dbc.ExecuteNonQuery("update tb_b_cleaning set ZT=1,User_ID=" + user_id + " where id=" + id);
                Flow.FinishFlow(dbc, flowId);
                Flow.FinishStep(dbc, stepId, 1, "审核通过");

                InsertUser(dt.Rows[0]["LoginName"].ToString(), dt.Rows[0]["PassWord"].ToString(), dt.Rows[0]["CLEANING_NAME"].ToString(), "E3EE2BCE-1041-4ABE-9245-70E088A983A2");


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

    [CSMethod("NoAgreeBj")]
    public bool NoAgreeBj(int id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_cleaning set ZT=-1,UPDATETIME=sysdate() where id=" + id);

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

    [CSMethod("GetFdSbSHList")]
    public object GetFdSbSHList(int pagnum, int pagesize, string mc, string xm, string qy, int step)
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

                string str = @"select a.*,b.QY_NAME,c.User_XM,e.* from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id  
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=1 and d.SERVICETYPE='房东设备申请' and d.STATUS=0 and e.RESULT=0 and e.step=" + step + " and e.TOUSERID=" + SystemUser.CurrentUser.UserID;
                str += where;

                //开始取分页数据
                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(str + " order by e.creattime desc", pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    [CSMethod("GetFdSbByFlow")]
    public object GetFdSbByFlow(int flowId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                DataTable dt = dbc.ExecuteDataTable("select a.*,b.Type from tb_b_landlord_sp a left join equipmenttype_table b on a.DEVICE_NAME=b.TypeNo where FLOWID=" + flowId + " and STATUS=0");
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    [CSMethod("SendDeviceFlow")]
    public object SendDeviceFlow(int flowId, int stepId, int result, int toUserId, string resultInfo, int nextStep)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                Flow.FinishStep(dbc, stepId, 1, "");
                Flow.SetStep(dbc, flowId, nextStep, "", toUserId, resultInfo);

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

    [CSMethod("CheckDevice")]
    public object CheckDevice(string SN, string type, int num)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string[] arr = SN.Split(',');
                if (arr.Length != num)
                    throw new Exception("申请设备个数" + num + "个，" + "扫描设备个数" + arr.Length + "个，不一致");
                foreach (string sn in arr)
                {
                    DataTable dt = dbc.ExecuteDataTable("select * from equipmentinfo_table where EquipmentId='" + sn + "'");
                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception("设备" + sn + "在数据库中不存在");
                    }
                    else
                    {
                        if (dt.Rows[0]["EquipmentDealer"].ToString() != "")
                            throw new Exception("设备" + sn + "已被授权");
                        else if (!type.Equals(dt.Rows[0]["EquipmentType"].ToString()))
                            throw new Exception("设备" + sn + "不是申请设备类型");
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    [CSMethod("UpdateFdDevice")]
    public object UpdateFdDevice(JSReader jsr)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                string sql = "update tb_b_landlord_sp set ";
                sql += "SN=@SN";
                sql += " where ID=" + jsr["ID"].ToString();

                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.Add("@SN", jsr["SN"].ToString());

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

    [CSMethod("AuthorizeDevice")]
    public object AuthorizeDevice(int serviceId, int flowId, int stepId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                DataTable dtFd = dbc.ExecuteDataTable("select USER_ID,DealerAuthoriCode from tb_b_landlord where ID=" + serviceId);
                DataTable dtDevice = dbc.ExecuteDataTable("select SN from tb_b_landlord_sp where STATUS=0 and FLOWID=" + flowId);
                foreach (DataRow dr in dtDevice.Rows)
                {
                    if (dr["SN"].ToString() != "")
                    {
                        string[] arr = dr["SN"].ToString().Split(',');
                        foreach (string sn in arr)
                        {
                            string deviceid = sn.Trim();
                            DataTable dtInfo = dbc.ExecuteDataTable("select * from equipmentinfo_table where EquipmentId=" + dbc.ToSqlValue(deviceid));
                            DataTable dtPara = dbc.ExecuteDataTable("select * from equipmentpara_table where EquipmentId=" + dbc.ToSqlValue(deviceid));

                            string EquipmentId = dtInfo.Rows[0]["EquipmentId"].ToString();
                            int EquipmentType = Convert.ToInt32(dtInfo.Rows[0]["EquipmentType"].ToString());
                            string EquipmentVersion = dtInfo.Rows[0]["EquipmentVersion"].ToString();
                            int EquipmentConNum = Convert.ToInt32(dtPara.Rows[0]["EquipmentConNum"].ToString());
                            string EquipmentCon1Type = dtPara.Rows[0]["EquipmentCon1Type"].ToString();
                            string EquipmentCon2Type = dtPara.Rows[0]["EquipmentCon2Type"].ToString();
                            string EquipmentCon3Type = dtPara.Rows[0]["EquipmentCon3Type"].ToString();
                            string EquipmentCon4Type = dtPara.Rows[0]["EquipmentCon4Type"].ToString();
                            string EquipmentCon5Type = dtPara.Rows[0]["EquipmentCon5Type"].ToString();
                            int EquipmentInfoNum = Convert.ToInt32(dtPara.Rows[0]["EquipmentInfoNum"].ToString());
                            string EquipmentInfo1Type = dtPara.Rows[0]["EquipmentInfo1Type"].ToString();
                            string EquipmentInfo2Type = dtPara.Rows[0]["EquipmentInfo2Type"].ToString();
                            string EquipmentInfo3Type = dtPara.Rows[0]["EquipmentInfo3Type"].ToString();
                            string EquipmentInfo4Type = dtPara.Rows[0]["EquipmentInfo4Type"].ToString();
                            string EquipmentInfo5Type = dtPara.Rows[0]["EquipmentInfo5Type"].ToString();
                            string EquipIEEEAddress = dtInfo.Rows[0]["EquipIEEEAddress"].ToString();
                            string EquipmentBluetoothMAC = dtInfo.Rows[0]["EquipmentBluetoothMAC"].ToString();
                            string EquipmentWireMAC = dtInfo.Rows[0]["EquipmentWireMAC"].ToString();
                            string EquipmentWirelessMAC = dtInfo.Rows[0]["EquipmentWirelessMAC"].ToString();
                            string ServerIP = dtInfo.Rows[0]["ServerIP"].ToString();
                            string BackupServerIP = dtInfo.Rows[0]["BackupServerIP"].ToString();
                            string EquipmentGatewayConnectKey = dtInfo.Rows[0]["EquipmentGatewayConnectKey"].ToString();
                            string EquipmentFieldConnectKey = dtInfo.Rows[0]["EquipmentFieldConnectKey"].ToString();
                            string EquipmentDate = dtInfo.Rows[0]["EquipmentDate"].ToString();
                            string EquipmentTester = dtInfo.Rows[0]["EquipmentTester"].ToString();
                            string EquipmentDealer = dtFd.Rows[0]["USER_ID"].ToString();
                            string DealerAuthoriCode = dtFd.Rows[0]["DealerAuthoriCode"].ToString();
                            DBHelper db = new DBHelper();
                            bool isSend = db.xuInsert(EquipmentId, EquipmentType, EquipmentVersion, EquipmentConNum, EquipmentCon1Type, EquipmentCon2Type, EquipmentCon3Type, EquipmentCon4Type, EquipmentCon5Type, EquipmentInfoNum, EquipmentInfo1Type, EquipmentInfo2Type, EquipmentInfo3Type, EquipmentInfo4Type, EquipmentInfo5Type, EquipIEEEAddress, EquipmentBluetoothMAC, EquipmentWireMAC, EquipmentWirelessMAC, ServerIP, BackupServerIP, EquipmentGatewayConnectKey, EquipmentFieldConnectKey, EquipmentDate, EquipmentTester, EquipmentDealer, DealerAuthoriCode);
                            if (isSend)
                            {
                                dbc.ExecuteNonQuery("update equipmentinfo_table set EquipmentDealer='" + EquipmentDealer + "',DealerAuthoriCode='" + DealerAuthoriCode + "' where EquipmentId='" + EquipmentId + "'");
                            }
                        }
                    }
                }

                Flow.FinishFlow(dbc, flowId);
                Flow.FinishStep(dbc, stepId, 1, "");
                dbc.ExecuteNonQuery("update tb_b_landlord_sp set ZT=1 where FLOWID=" + flowId);
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

    [CSMethod("GetMyDevice")]
    public object GetMyDevice(int pagnum, int pagesize, string sbbh)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";

                if (!string.IsNullOrEmpty(sbbh))
                {
                    where += " and EquipmentId=" + sbbh;
                }

                string str = @"select a.EquipmentId,a.EquipmentDate,a.EquipmentVersion,a.DealerAuthoriCode,b.Type
                              from equipmentinfo_table a left join equipmenttype_table b on a.EquipmentType=b.TypeNo 
                              where EquipmentDealer=" + SystemUser.CurrentUser.UserID + where;
                str += where;

                //开始取分页数据
                System.Data.DataTable dtPage = new System.Data.DataTable();
                dtPage = dbc.GetPagedDataTable(str + " order by a.ID desc", pagesize, ref cp, out ac);

                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    [CSMethod("GetManagerList")]
    public object GetManagerList(int pagnum, int pagesize)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string sql = "select * from tb_b_manager where State=0";

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

    [CSMethod("PassManager")]
    public object PassManager(int ID)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_manager set State=1 where ID=" + ID);
                DataTable dt = dbc.ExecuteDataTable("select * from tb_b_manager where ID=" + ID);
                InsertYTZUser(dt);
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

    public void InsertYTZUser(DataTable dt)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            dbc.BeginTransaction();
            try
            {
                string Mobile = dt.Rows[0]["Mobile"].ToString();
                string RealName = dt.Rows[0]["Name"].ToString();
                string Email = dt.Rows[0]["Email"].ToString();
                string IdCard = dt.Rows[0]["IdCard"].ToString();
                int FDUserId = Convert.ToInt16(dt.Rows[0]["UserID"].ToString());
                int UserID = Convert.ToInt16(dbc.ExecuteScalar("SELECT IDENT_CURRENT('aspnet_Users') + IDENT_INCR('aspnet_Users')").ToString());
                DataTable dtUser = dbc.GetEmptyDataTable("aspnet_Users");
                DataRow drUser = dtUser.NewRow();
                drUser["UserName"] = Mobile;
                drUser["LoweredUserName"] = Mobile;
                drUser["IsAnonymous"] = 0;
                string PasswordSalt = Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes("123456")));
                string Password = GetPassWord("123456", PasswordSalt);
                drUser["PasswordFormat"] = 1;
                drUser["PasswordSalt"] = PasswordSalt;
                drUser["Password"] = Password;
                drUser["Email"] = Email;
                drUser["LoweredEmail"] = Email;
                drUser["IsApproved"] = 1;
                drUser["IsLockedOut"] = 0;
                drUser["CreateDate"] = DateTime.Now;
                drUser["FailedPasswordAttemptCount"] = 0;
                drUser["FailedPasswordAnswerAttemptCount"] = 0;
                drUser["Gender"] = 0;
                drUser["UserRole"] = 1;
                drUser["IdCardNo"] = IdCard;
                drUser["CreditGrade"] = 0;
                drUser["LastLoginDate"] = DateTime.Now;
                drUser["LastPasswordChangedDate"] = DateTime.Now;
                drUser["LastLockoutDate"] = DateTime.Now;
                drUser["FailedPasswordAttemptWindowStart"] = DateTime.Now;
                drUser["FailedPasswordAnswerAttemptWindowStart"] = DateTime.Now;
                drUser["SessionId"] = Guid.NewGuid().ToString();
                drUser["LastActivityDate"] = DateTime.Now;
                dtUser.Rows.Add(drUser);

                DataTable dtMember = dbc.GetEmptyDataTable("aspnet_Members");
                DataRow drMember = dtMember.NewRow();
                drMember["UserId"] = UserID;
                drMember["GradeId"] = 13;
                drMember["ReferralUserId"] = 0;
                drMember["IsOpenBalance"] = 1;
                drMember["TradePassword"] = Password;
                drMember["TradePasswordSalt"] = PasswordSalt;
                drMember["TradePasswordFormat"] = 1;
                drMember["OrderNumber"] = 0;
                drMember["Expenditure"] = 0;
                drMember["Points"] = 0;
                drMember["Balance"] = 0;
                drMember["RequestBalance"] = 0;
                drMember["RealName"] = RealName;
                drMember["CellPhone"] = Mobile;
                drMember["MemberShipRightId"] = 13;
                dtMember.Rows.Add(drMember);

                var dtRole = dbc.GetEmptyDataTable("aspnet_UsersInRoles");
                var drRole = dtRole.NewRow();
                drRole["UserId"] = UserID;
                drRole["RoleId"] = "291FB826-BFDA-4DEC-8329-E63212F6AA15";
                dtRole.Rows.Add(drRole);

                drRole = dtRole.NewRow();
                drRole["UserId"] = UserID;
                drRole["RoleId"] = "E5AD331C-8CC4-4B3E-9B9D-658CB3DD5AE4";
                dtRole.Rows.Add(drRole);

                var dtGL = dbc.GetEmptyDataTable("aspnet_FdAndMdUser");
                var drGL = dtGL.NewRow();
                drGL["FDUSERID"] = FDUserId;
                drGL["MEUSERID"] = UserID;
                dtGL.Rows.Add(drGL);

                dbc.InsertTable(dtUser);
                dbc.InsertTable(dtMember);
                dbc.InsertTable(dtRole);
                dbc.InsertTable(dtGL);

                dbc.CommitTransaction();

            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    public string GetPassWord(string pw, string pws)
    {
        byte[] bytes = Encoding.Unicode.GetBytes(pw);
        byte[] src = Convert.FromBase64String(pws);
        byte[] dst = new byte[src.Length + bytes.Length];
        byte[] inArray = null;
        Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
        return Convert.ToBase64String(HashAlgorithm.Create("SHA1").ComputeHash(dst));
    }

    [CSMethod("GetFdShList")]
    public object GetFdShList(int pagnum, int pagesize, string mc, string xm, string qy)
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

                string str = "select a.*,b.QY_NAME,c.User_XM from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and (a.ZT=2 or a.ZT=3 or a.ZT=4)";
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

    [CSMethod("GetBjShList")]
    public object GetBjShList(int pagnum, int pagesize, string xm)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string where = "";
                if (!string.IsNullOrEmpty(xm))
                {
                    where += " and CLEANING_NAME like'%" + xm + "%'";
                }


                string str = "select * from tb_b_cleaning  where STATUS=0 and (ZT=2 or ZT=3 or ZT=4)";
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

    [CSMethod("GetDlsShList")]
    public object GetDlsShList(int pagnum, int pagesize, string mc, string xm, string qy)
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

                string str = @"select a.*,b.QY_NAME,c.User_XM from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and (a.ZT=2 or a.ZT=3 or a.ZT=4)";
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

    [CSMethod("GetSHInfo")]
    public object GetSHInfo(int id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sql = "select * from tb_b_service where PID=" + id + " and ISEND=0";
                return dbc.ExecuteDataTable(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("SH")]
    public object SH(int pid, int type)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                string sqlStr = "select a.ID,b.*,c.STEPID from tb_b_service a left join tb_u_flow b on a.ID=b.SERVICEID left join tb_u_flow_step c on b.FLOWID=c.FLOWID where pid=" + pid + " and ISEND=0";
                DataTable dtService = dbc.ExecuteDataTable(sqlStr);

                int flowid = Convert.ToInt16(dtService.Rows[0]["FLOWID"].ToString());
                int stepid = Convert.ToInt16(dtService.Rows[0]["STEPID"].ToString());
                string serviceType = dtService.Rows[0]["SERVICETYPE"].ToString();

                Flow.FinishFlow(dbc, flowid);
                Flow.FinishStep(dbc, stepid, 1, "审核通过");

                if (type == 1)
                {
                    if (serviceType == "房东冻结")
                        dbc.ExecuteNonQuery("update tb_b_landlord set zt=5 where ID=" + pid);
                    else
                        dbc.ExecuteNonQuery("update tb_b_landlord set zt=1 where ID=" + pid);
                }
                else if (type == 2)
                {
                    if (serviceType == "代理商冻结")
                        dbc.ExecuteNonQuery("update tb_b_agent set zt=5 where ID=" + pid);
                    else
                        dbc.ExecuteNonQuery("update tb_b_agent set zt=1 where ID=" + pid);
                }
                else if (type == 3)
                {
                    if (serviceType == "保洁冻结")
                        dbc.ExecuteNonQuery("update tb_b_cleaning set zt=5 where ID=" + pid);
                    else
                        dbc.ExecuteNonQuery("update tb_b_cleaning set zt=1 where ID=" + pid);
                }
                dbc.ExecuteNonQuery("update tb_b_service set ISEND=1 where ID=" + dtService.Rows[0]["ID"].ToString());

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