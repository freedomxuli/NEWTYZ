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

    [CSMethod("DelDLsByids")]
    public bool DelDLsByids(JSReader jsr)
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
                    string str = "update tb_b_agent set status = 1,updatetime=@updatetime where ID =@ID";
                    MySqlCommand cmd = new MySqlCommand(str);

                    cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ID", jsr.ToArray()[i].ToString());
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

    [CSMethod("SaveDlsDevice")]
    public object SaveDlsDevice(JSReader jsr)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                int id;
                if (jsr["ID"].ToString() == "")
                {
                    id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_agent_sp'").ToString());

                    string sql = "insert into tb_b_agent_sp(";
                    sql += "DEVICE_NAME,";
                    sql += "DEVICE_NUMBER,";
                    sql += "DEVICE_MONEY,";
                    sql += "STATUS";
                    sql += ") values(";
                    sql += "@DEVICE_NAME,";
                    sql += "@DEVICE_NUMBER,";
                    sql += "@DEVICE_MONEY,";
                    sql += "@STATUS";
                    sql += ")";

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@DEVICE_NAME", jsr["DEVICE_NAME"].ToString());
                    cmd.Parameters.Add("@DEVICE_NUMBER", Convert.ToDecimal(jsr["DEVICE_NUMBER"].ToString()));
                    cmd.Parameters.Add("@DEVICE_MONEY", Convert.ToDecimal(jsr["DEVICE_MONEY"].ToString()));
                    cmd.Parameters.Add("@STATUS", "0");
                    dbc.ExecuteNonQuery(cmd);
                }
                else
                {
                    id = Convert.ToInt32(jsr["ID"].ToString());

                    string sql = "update tb_b_agent_sp set ";
                    sql += "DEVICE_NAME=@DEVICE_NAME,";
                    sql += "DEVICE_NUMBER=@DEVICE_NUMBER,";
                    sql += "DEVICE_MONEY=@DEVICE_MONEY";
                    sql += " where ID=" + id;

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@DEVICE_NAME", jsr["DEVICE_NAME"].ToString());
                    cmd.Parameters.Add("@DEVICE_NUMBER", Convert.ToDecimal(jsr["DEVICE_NUMBER"].ToString()));
                    cmd.Parameters.Add("@DEVICE_MONEY", Convert.ToDecimal(jsr["DEVICE_MONEY"].ToString()));
                    dbc.ExecuteNonQuery(cmd);
                }

                dbc.CommitTransaction();

                DataTable dt = dbc.ExecuteDataTable("select ID,DEVICE_NAME,DEVICE_NUMBER,DEVICE_MONEY from tb_b_agent_sp where id=" + id);
                return dt;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
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

                string str = "select a.*,b.QY_NAME,c.User_XM from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 ";
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
    public object GetFdById(int fd_id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sql = "select * from tb_b_landlord where ID=" + fd_id;

                DataTable dt = dbc.ExecuteDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("DelFdByids")]
    public bool DelFdByids(JSReader jsr)
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
                    string str = "update tb_b_landlord set status = 1,updatetime=@updatetime where ID =@ID";
                    MySqlCommand cmd = new MySqlCommand(str);

                    cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ID", jsr.ToArray()[i].ToString());
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

    [CSMethod("SaveFdDevice")]
    public object SaveFdDevice(JSReader jsr)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                int id;
                if (jsr["ID"].ToString() == "")
                {
                    id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_landlord_sp'").ToString());

                    string sql = "insert into tb_b_landlord_sp(";
                    sql += "DEVICE_NAME,";
                    sql += "DEVICE_NUMBER,";
                    sql += "DEVICE_MONEY,";
                    sql += "STATUS";
                    sql += ") values(";
                    sql += "@DEVICE_NAME,";
                    sql += "@DEVICE_NUMBER,";
                    sql += "@DEVICE_MONEY,";
                    sql += "@STATUS";
                    sql += ")";

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@DEVICE_NAME", jsr["DEVICE_NAME"].ToString());
                    cmd.Parameters.Add("@DEVICE_NUMBER", Convert.ToDecimal(jsr["DEVICE_NUMBER"].ToString()));
                    cmd.Parameters.Add("@DEVICE_MONEY", Convert.ToDecimal(jsr["DEVICE_MONEY"].ToString()));
                    cmd.Parameters.Add("@STATUS", "0");
                    dbc.ExecuteNonQuery(cmd);
                }
                else
                {
                    id = Convert.ToInt32(jsr["ID"].ToString());

                    string sql = "update tb_b_landlord_sp set ";
                    sql += "DEVICE_NAME=@DEVICE_NAME,";
                    sql += "DEVICE_NUMBER=@DEVICE_NUMBER,";
                    sql += "DEVICE_MONEY=@DEVICE_MONEY";
                    sql += " where ID=" + id;

                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.Add("@DEVICE_NAME", jsr["DEVICE_NAME"].ToString());
                    cmd.Parameters.Add("@DEVICE_NUMBER", Convert.ToDecimal(jsr["DEVICE_NUMBER"].ToString()));
                    cmd.Parameters.Add("@DEVICE_MONEY", Convert.ToDecimal(jsr["DEVICE_MONEY"].ToString()));
                    dbc.ExecuteNonQuery(cmd);
                }

                dbc.CommitTransaction();

                DataTable dt = dbc.ExecuteDataTable("select ID,DEVICE_NAME,DEVICE_NUMBER,DEVICE_MONEY from tb_b_agent_sp where id=" + id);
                return dt;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
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


                string str = "select * from tb_b_cleaning  where STATUS=0 ";
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
    public object GetBjById(int bj_id)
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

    [CSMethod("DelBjByids")]
    public bool DelBjByids(JSReader jsr)
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
                    string str = "update tb_b_cleaning set status = 1,updatetime=@updatetime where ID =@ID";
                    MySqlCommand cmd = new MySqlCommand(str);

                    cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ID", jsr.ToArray()[i].ToString());
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

}