using MySql.Data.MySqlClient;
using SmartFramework4v2.Data.MySql;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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

                string str = "select a.*,b.QY_NAME,c.User_XM from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and a.ZT=0";
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


                string str = "select * from tb_b_cleaning  where STATUS=0 and ZT=0";
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

    [CSMethod("AgreeDls")]
    public bool AgreeDls(int id, string yhm, string mm)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_agent set ZT=1,ADMIN_ID=" + SystemUser.CurrentUser.UserID + ",COMFIRM_TIME=sysdate() where id=" + id);

                DataTable dt = GetDLsById(id);
                int user_id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_users'").ToString());
                string sql = @"insert into tb_b_users(LoginName,Password,User_XM,StartDate,EndDate,User_Enable,User_Delflag,AddTime)  
                             values(@LoginName,@Password,@User_XM,@StartDate,@EndDate,@User_Enable,@User_Delflag,@AddTime)";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.Add("@LoginName", yhm);
                cmd.Parameters.Add("@Password", mm);
                cmd.Parameters.Add("@User_XM", dt.Rows[0]["AGENT_NAME"].ToString());
                cmd.Parameters.Add("@StartDate", Convert.ToDateTime(dt.Rows[0]["AGENT_START_TIME"].ToString()));
                cmd.Parameters.Add("@EndDate", Convert.ToDateTime(dt.Rows[0]["AGENT_END_TIME"].ToString()));
                cmd.Parameters.Add("@User_Enable", '0');
                cmd.Parameters.Add("@User_Delflag", '0');
                cmd.Parameters.Add("@AddTime", DateTime.Now);
                dbc.ExecuteNonQuery(cmd);

                dbc.ExecuteNonQuery("insert into tb_b_user_js_gl(User_ID,JS_ID,delflag,addtime) values(" + user_id + ",9,0,sysdate())");

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
    public bool AgreeBj(int id, string yhm, string mm)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_b_cleaning set ZT=3,UPDATETIME=sysdate() where id=" + id);

                DataTable dt = GetDLsById(id);
                int user_id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_users'").ToString());
                string sql = @"insert into tb_b_users(LoginName,Password,User_XM,StartDate,EndDate,User_Enable,User_Delflag,AddTime)  
                             values(@LoginName,@Password,@User_XM,@StartDate,@EndDate,@User_Enable,@User_Delflag,@AddTime)";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.Add("@LoginName", yhm);
                cmd.Parameters.Add("@Password", mm);
                cmd.Parameters.Add("@User_XM", dt.Rows[0]["AGENT_NAME"].ToString());
                cmd.Parameters.Add("@StartDate", Convert.ToDateTime(dt.Rows[0]["AGENT_START_TIME"].ToString()));
                cmd.Parameters.Add("@EndDate", Convert.ToDateTime(dt.Rows[0]["AGENT_END_TIME"].ToString()));
                cmd.Parameters.Add("@User_Enable", '0');
                cmd.Parameters.Add("@User_Delflag", '0');
                cmd.Parameters.Add("@AddTime", DateTime.Now);
                dbc.ExecuteNonQuery(cmd);

                dbc.ExecuteNonQuery("insert into tb_b_user_js_gl(User_ID,JS_ID,delflag,addtime) values(" + user_id + ",10,0,sysdate())");

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

}