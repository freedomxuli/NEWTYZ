using MySql.Data.MySqlClient;
using SmartFramework4v2.Data.MySql;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// JjrDB 的摘要说明
/// </summary>
namespace jjrDB
{
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
        public bool SaveDls(JSReader jsr, JSReader fj, string imgs1, string imgs2)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    int id;
                    if (jsr["ID"].ToString() == "")
                    {
                        id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_agent'").ToString());

                        DataTable dtUser = dbc.ExecuteDataTable("select * from tb_b_users where LoginName='" + jsr["LoginName"].ToString().Trim() + "'");
                        if (dtUser.Rows.Count > 0)
                            throw new Exception("用户名已存在");

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
                        sql += "LoginName,";
                        sql += "PassWord,";
                        sql += "DealerAuthoriCode,";
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
                        sql += "@LoginName,";
                        sql += "@PassWord,";
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
                        cmd.Parameters.Add("@LoginName", jsr["LoginName"].ToString());
                        cmd.Parameters.Add("@PassWord", jsr["PassWord"].ToString());
                        cmd.Parameters.Add("@ZT", "0");

                        int flowId = Flow.SetFlow(dbc, id.ToString(), "代理商申请");
                        Flow.SetStep(dbc, flowId, 1, "发起代理商申请", 1, "");

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
                        sql += "UPDATETIME=@UPDATETIME,";
                        sql += "PassWord=@PassWord";
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
                        cmd.Parameters.Add("@PassWord", jsr["PassWord"].ToString());
                        dbc.ExecuteNonQuery(cmd);
                    }


                    dbc.ExecuteNonQuery("delete from tb_b_fj where fj_pid='" + id + "' and fj_lx=2");
                    string[] imglist1 = imgs1.Split(new char[] { ',' });
                    for (int i = 0; i < imglist1.Count(); i++)
                    {
                        string newfilename = GetNewFilePath(imglist1[i], "~/files/dls/");

                        var sqlStr = "insert into tb_b_FJ (fj_id,fj_pid,addtime,updatetime,status,xgyh_id,fj_lx,fj_url)"
                   + "values (@fj_id,@fj_pid,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_url)";

                        string FJID = Guid.NewGuid().ToString();
                        MySqlCommand cmd = new MySqlCommand(sqlStr);
                        cmd.Parameters.AddWithValue("@fj_id", FJID);
                        cmd.Parameters.AddWithValue("@fj_pid", id);
                        //  cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
                        cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.AddWithValue("@fj_lx", 2);
                        cmd.Parameters.AddWithValue("@fj_url", newfilename.Substring(2, newfilename.Length - 2));
                        dbc.ExecuteNonQuery(cmd);
                    }

                    dbc.ExecuteNonQuery("delete from tb_b_fj where fj_pid='" + id + "' and fj_lx=3");
                    string[] imglist2 = imgs2.Split(new char[] { ',' });
                    for (int i = 0; i < imglist2.Count(); i++)
                    {
                        string newfilename = GetNewFilePath(imglist2[i], "~/files/dls/");

                        var sqlStr = "insert into tb_b_FJ (fj_id,fj_pid,addtime,updatetime,status,xgyh_id,fj_lx,fj_url)"
                   + "values (@fj_id,@fj_pid,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_url)";

                        string FJID = Guid.NewGuid().ToString();
                        MySqlCommand cmd = new MySqlCommand(sqlStr);
                        cmd.Parameters.AddWithValue("@fj_id", FJID);
                        cmd.Parameters.AddWithValue("@fj_pid", id);
                        //  cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
                        cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.AddWithValue("@fj_lx", 3);
                        cmd.Parameters.AddWithValue("@fj_url", newfilename.Substring(2, newfilename.Length - 2));
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

                    string str = @"select a.*,b.QY_NAME,c.User_XM from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 ";
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

                    sql = "select fj_url,fj_lx from tb_b_fj where fj_pid=" + dls_id;

                    DataTable dt2 = dbc.ExecuteDataTable(sql);

                    return new { dt = dt, dtFJ = dt2 };
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
        public bool SaveDlsDevice(JSReader jsr, int serviceId)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    int id;
                    if (jsr["ID"].ToString() == "")
                    {

                        string sql = "insert into tb_b_agent_sp(";
                        sql += "AGENT_ID,";
                        sql += "DEVICE_NAME,";
                        sql += "DEVICE_NUMBER,";
                        sql += "DEVICE_MONEY,";
                        sql += "STATUS";
                        sql += ") values(";
                        sql += "@AGENT_ID,";
                        sql += "@DEVICE_NAME,";
                        sql += "@DEVICE_NUMBER,";
                        sql += "@DEVICE_MONEY,";
                        sql += "@STATUS";
                        sql += ")";

                        MySqlCommand cmd = new MySqlCommand(sql);
                        cmd.Parameters.Add("@AGENT_ID", serviceId);
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

                    return true;
                }
                catch (Exception ex)
                {
                    dbc.RoolbackTransaction();
                    throw ex;
                }
            }
        }

        [CSMethod("SaveFdXX")]
        public bool SaveFdXX(JSReader jsr, JSReader fj, string imgs1, string imgs2)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    int id;
                    if (jsr["ID"].ToString() == "")
                    {
                        DataTable dtUser = dbc.ExecuteDataTable("select * from tb_b_users where LoginName='" + jsr["LoginName"].ToString().Trim() + "'");
                        if (dtUser.Rows.Count > 0)
                            throw new Exception("用户名已存在");

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
                        sql += "LoginName,";
                        sql += "PassWord,";
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
                        sql += "@LoginName,";
                        sql += "@PassWord,";
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
                        cmd.Parameters.Add("@LoginName", jsr["LoginName"].ToString());
                        cmd.Parameters.Add("@PassWord", jsr["PassWord"].ToString());
                        cmd.Parameters.Add("@ZT", "0");

                        dbc.ExecuteNonQuery(cmd);

                        int flowId = Flow.SetFlow(dbc, id.ToString(), "房东申请");
                        Flow.SetStep(dbc, flowId, 1, "发起房东申请", 1, "");
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
                        sql += "UPDATETIME=@UPDATETIME,";
                        sql += "PassWord=@PassWord";
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
                        cmd.Parameters.Add("@PassWord", jsr["PassWord"].ToString());


                        dbc.ExecuteNonQuery(cmd);
                    }

                    //var fj_arr = fj.ToArray();
                    //if (fj_arr.Length > 0)
                    //{
                    //    var fj_ids = "";
                    //    for (int i = 0; i < fj_arr.Length; i++)
                    //    {
                    //        if (i == 0)
                    //        {
                    //            fj_ids += "'" + fj_arr[i].ToString() + "'";
                    //        }
                    //        else
                    //        {
                    //            fj_ids += ",'" + fj_arr[i].ToString() + "'";
                    //        }
                    //    }
                    //    dbc.ExecuteNonQuery("update tb_b_fj set fj_pid='" + id + "' where status=0 and fj_id in(" + fj_ids + ")");
                    //}

                    dbc.ExecuteNonQuery("delete from tb_b_fj where fj_pid='" + id + "' and fj_lx=2");
                    string[] imglist1 = imgs1.Split(new char[] { ',' });
                    for (int i = 0; i < imglist1.Count(); i++)
                    {
                        string newfilename = GetNewFilePath(imglist1[i], "~/files/fd/");

                        var sqlStr = "insert into tb_b_FJ (fj_id,fj_pid,addtime,updatetime,status,xgyh_id,fj_lx,fj_url)"
                   + "values (@fj_id,@fj_pid,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_url)";

                        string FJID = Guid.NewGuid().ToString();
                        MySqlCommand cmd = new MySqlCommand(sqlStr);
                        cmd.Parameters.AddWithValue("@fj_id", FJID);
                        cmd.Parameters.AddWithValue("@fj_pid", id);
                        //  cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
                        cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.AddWithValue("@fj_lx", 2);
                        cmd.Parameters.AddWithValue("@fj_url", newfilename.Substring(2, newfilename.Length - 2));
                        dbc.ExecuteNonQuery(cmd);
                    }

                    dbc.ExecuteNonQuery("delete from tb_b_fj where fj_pid='" + id + "' and fj_lx=3");
                    string[] imglist2 = imgs2.Split(new char[] { ',' });
                    for (int i = 0; i < imglist2.Count(); i++)
                    {
                        string newfilename = GetNewFilePath(imglist2[i], "~/files/fd/");

                        var sqlStr = "insert into tb_b_FJ (fj_id,fj_pid,addtime,updatetime,status,xgyh_id,fj_lx,fj_url)"
                   + "values (@fj_id,@fj_pid,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_url)";

                        string FJID = Guid.NewGuid().ToString();
                        MySqlCommand cmd = new MySqlCommand(sqlStr);
                        cmd.Parameters.AddWithValue("@fj_id", FJID);
                        cmd.Parameters.AddWithValue("@fj_pid", id);
                        //  cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
                        cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.AddWithValue("@fj_lx", 3);
                        cmd.Parameters.AddWithValue("@fj_url", newfilename.Substring(2, newfilename.Length - 2));
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

        public static string GetNewFilePath(string oldfilename, string newpath)
        {
            try
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(oldfilename)))
                {
                    FileInfo fileinfo = new FileInfo(HttpContext.Current.Server.MapPath(oldfilename));
                    string dirfilename = HttpContext.Current.Server.MapPath(newpath) + fileinfo.Name;
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(newpath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(newpath));
                    if (!File.Exists(dirfilename))
                        fileinfo.CopyTo(dirfilename);
                    return newpath + fileinfo.Name;
                }
                else
                    return "";
            }
            catch
            {
                return "";
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

                    sql = "select fj_url,fj_lx from tb_b_fj where fj_pid=" + fd_id;

                    DataTable dt2 = dbc.ExecuteDataTable(sql);

                    return new { dt = dt, dtFJ = dt2 };
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

        [CSMethod("FreezeFD")]
        public bool FreezeFD(JSReader jsr)
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
                        string str = "update tb_b_landlord set IsFreeze = 1,updatetime=@updatetime where ID =@ID";
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

        [CSMethod("UnFreezeFD")]
        public bool UnFreezeFD(JSReader jsr)
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
                        string str = "update tb_b_landlord set IsFreeze = 0,updatetime=@updatetime where ID =@ID";
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
        public bool SaveFdDevice(JSReader jsr, int serviceId)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    int id;
                    if (jsr["ID"].ToString() == "")
                    {

                        string sql = "insert into tb_b_landlord_sp(";
                        sql += "LANDLORD_ID,";
                        sql += "DEVICE_NAME,";
                        sql += "DEVICE_NUMBER,";
                        sql += "DEVICE_MONEY,";
                        sql += "STATUS";
                        sql += ") values(";
                        sql += "@LANDLORD_ID,";
                        sql += "@DEVICE_NAME,";
                        sql += "@DEVICE_NUMBER,";
                        sql += "@DEVICE_MONEY,";
                        sql += "@STATUS";
                        sql += ")";

                        MySqlCommand cmd = new MySqlCommand(sql);
                        cmd.Parameters.Add("@LANDLORD_ID", serviceId);
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

                    return true;
                }
                catch (Exception ex)
                {
                    dbc.RoolbackTransaction();
                    throw ex;
                }
            }
        }

        [CSMethod("DeleteFdDevice")]
        public bool DeleteFdDevice(int id)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    dbc.ExecuteNonQuery("update tb_b_landlord_sp set status=1 where id=" + id);
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


        [CSMethod("DeleteDlsDevice")]
        public bool DeleteDlsDevice(int id)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    dbc.ExecuteNonQuery("update tb_b_agent_sp set status=1 where id=" + id);
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

        [CSMethod("SaveBjXX")]
        public bool SaveBjXX(JSReader jsr, JSReader fj, string imgs1, string imgs2, string imgs3)
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
                        sql += "ROLE_ID,";
                        sql += "LoginName,";
                        sql += "PassWord,";
                        sql += "QY_ID,";
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
                        sql += "@ROLE_ID,";
                        sql += "@LoginName,";
                        sql += "@PassWord,";
                        sql += "@QY_ID,";
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
                        cmd.Parameters.Add("@ROLE_ID", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.Add("@LoginName", jsr["LoginName"].ToString());
                        cmd.Parameters.Add("@PassWord", jsr["PassWord"].ToString());
                        cmd.Parameters.Add("@QY_ID", Convert.ToInt16(jsr["QY_ID"].ToString()));
                        cmd.Parameters.Add("@ZT", "0");

                        dbc.ExecuteNonQuery(cmd);

                        int flowId = Flow.SetFlow(dbc, id.ToString(), "保洁申请");
                        Flow.SetStep(dbc, flowId, 1, "发起保洁申请", 1, "");
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

                    //var fj_arr = fj.ToArray();
                    //if (fj_arr.Length > 0)
                    //{
                    //    var fj_ids = "";
                    //    for (int i = 0; i < fj_arr.Length; i++)
                    //    {
                    //        if (i == 0)
                    //        {
                    //            fj_ids += "'" + fj_arr[i].ToString() + "'";
                    //        }
                    //        else
                    //        {
                    //            fj_ids += ",'" + fj_arr[i].ToString() + "'";
                    //        }
                    //    }
                    //    dbc.ExecuteNonQuery("update tb_b_fj set fj_pid='" + id + "' where status=0 and fj_id in(" + fj_ids + ")");
                    //}

                    dbc.ExecuteNonQuery("delete from tb_b_fj where fj_pid='" + id + "' and fj_lx=2");
                    string[] imglist1 = imgs1.Split(new char[] { ',' });
                    for (int i = 0; i < imglist1.Count(); i++)
                    {
                        string newfilename = GetNewFilePath(imglist1[i], "~/files/bj/");

                        var sqlStr = "insert into tb_b_FJ (fj_id,fj_pid,addtime,updatetime,status,xgyh_id,fj_lx,fj_url)"
                   + "values (@fj_id,@fj_pid,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_url)";

                        string FJID = Guid.NewGuid().ToString();
                        MySqlCommand cmd = new MySqlCommand(sqlStr);
                        cmd.Parameters.AddWithValue("@fj_id", FJID);
                        cmd.Parameters.AddWithValue("@fj_pid", id);
                        //  cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
                        cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.AddWithValue("@fj_lx", 2);
                        cmd.Parameters.AddWithValue("@fj_url", newfilename.Substring(2, newfilename.Length - 2));
                        dbc.ExecuteNonQuery(cmd);
                    }

                    dbc.ExecuteNonQuery("delete from tb_b_fj where fj_pid='" + id + "' and fj_lx=3");
                    string[] imglist2 = imgs2.Split(new char[] { ',' });
                    for (int i = 0; i < imglist2.Count(); i++)
                    {
                        string newfilename = GetNewFilePath(imglist2[i], "~/files/bj/");

                        var sqlStr = "insert into tb_b_FJ (fj_id,fj_pid,addtime,updatetime,status,xgyh_id,fj_lx,fj_url)"
                   + "values (@fj_id,@fj_pid,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_url)";

                        string FJID = Guid.NewGuid().ToString();
                        MySqlCommand cmd = new MySqlCommand(sqlStr);
                        cmd.Parameters.AddWithValue("@fj_id", FJID);
                        cmd.Parameters.AddWithValue("@fj_pid", id);
                        //  cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
                        cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.AddWithValue("@fj_lx", 3);
                        cmd.Parameters.AddWithValue("@fj_url", newfilename.Substring(2, newfilename.Length - 2));
                        dbc.ExecuteNonQuery(cmd);
                    }

                    dbc.ExecuteNonQuery("delete from tb_b_fj where fj_pid='" + id + "' and fj_lx=5");
                    string[] imglist3 = imgs3.Split(new char[] { ',' });
                    for (int i = 0; i < imglist3.Count(); i++)
                    {
                        string newfilename = GetNewFilePath(imglist3[i], "~/files/bj/");

                        var sqlStr = "insert into tb_b_FJ (fj_id,fj_pid,addtime,updatetime,status,xgyh_id,fj_lx,fj_url)"
                   + "values (@fj_id,@fj_pid,@addtime,@updatetime,0,@xgyh_id,@fj_lx,@fj_url)";

                        string FJID = Guid.NewGuid().ToString();
                        MySqlCommand cmd = new MySqlCommand(sqlStr);
                        cmd.Parameters.AddWithValue("@fj_id", FJID);
                        cmd.Parameters.AddWithValue("@fj_pid", id);
                        //  cmd.Parameters.AddWithValue("@fj_mc", fds[0].FileName);
                        cmd.Parameters.AddWithValue("@addtime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                        // cmd.Parameters.AddWithValue("@fj_nr", fds[0].FileBytes);
                        cmd.Parameters.AddWithValue("@xgyh_id", SystemUser.CurrentUser.UserID);
                        cmd.Parameters.AddWithValue("@fj_lx", 5);
                        cmd.Parameters.AddWithValue("@fj_url", newfilename.Substring(2, newfilename.Length - 2));
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

                    sql = "select fj_url,fj_lx from tb_b_fj where fj_pid=" + bj_id;

                    DataTable dt2 = dbc.ExecuteDataTable(sql);

                    return new { dt = dt, dtFJ = dt2 };
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


        [CSMethod("GetSQList")]
        public object GetSQList(int pagnum, int pagesize, string sj, string xm, int zt, int type)
        {
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    int cp = pagnum;
                    int ac = 0;

                    string where = "";
                    if (!string.IsNullOrEmpty(sj))
                    {
                        where += " and Mobile like'%" + sj + "%'";
                    }
                    if (!string.IsNullOrEmpty(xm))
                    {
                        where += " and Name like'%" + xm + "%'";
                    }

                    string str = "select * from tb_b_application where status=" + zt + " and ApplicationType=" + type + "";
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

        [CSMethod("GetFdSbList")]
        public object GetFdSbList(int id)
        {
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    string sqlStr = "select * from tb_b_landlord_sp where LANDLORD_ID=" + id + " and STATUS=0 and ZT is null";
                    DataTable dt = dbc.ExecuteDataTable(sqlStr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        [CSMethod("GetDlsSbList")]
        public object GetDlsSbList(int id)
        {
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    string sqlStr = "select * from tb_b_agent_sp where AGENT_ID=" + id + " and STATUS=0 and ZT is null";
                    DataTable dt = dbc.ExecuteDataTable(sqlStr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        [CSMethod("SendDeviceFlow")]
        public object SendDeviceFlow(int serviceId)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    int flowId = Flow.SetFlow(dbc, serviceId.ToString(), "房东设备申请");
                    Flow.SetStep(dbc, flowId, 1, "房东设备发送至管理员审核", 1, "");

                    dbc.ExecuteNonQuery("update tb_b_landlord_sp set ZT=0,FLOWID=" + flowId + " where STATUS=0 and LANDLORD_ID=" + serviceId);

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

        [CSMethod("SendDlsDeviceFlow")]
        public object SendDlsDeviceFlow(int serviceId)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    int flowId = Flow.SetFlow(dbc, serviceId.ToString(), "代理商设备申请");
                    Flow.SetStep(dbc, flowId, 1, "代理商设备发送至管理员审核", 1, "");

                    dbc.ExecuteNonQuery("update tb_b_agent_sp set ZT=0,FLOWID=" + flowId + " where STATUS=0 and AGENT_ID=" + serviceId);

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

        [CSMethod("GetHotelList")]
        public object GetHotelList(int pagnum, int pagesize, string hotelName, string zt)
        {
            using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
            {
                try
                {
                    int cp = pagnum;
                    int ac = 0;

                    string where = "";
                    if (!string.IsNullOrEmpty(hotelName))
                    {
                        where += " and " + dbc.C_Like("HotelName", hotelName, SmartFramework4v2.Data.LikeStyle.LeftAndRightLike);
                    }
                    if (!string.IsNullOrEmpty(zt))
                    {
                        where += " and SHZT=" + zt;
                    }


                    string str = @"select a.*,b.CellPhone,b.RealName from Lock_HotelApply a left join aspnet_Members b on a.UserId=b.UserId 
                where 1=1";
                    str += where;

                    //开始取分页数据
                    System.Data.DataTable dtPage = new System.Data.DataTable();
                    dtPage = dbc.GetPagedDataTable(str + " order by a.CreateDate desc", pagesize, ref cp, out ac);

                    return new { dt = dtPage, cp = cp, ac = ac };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int GetHotelNum(int zt)
        {
            using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
            {
                try
                {

                    string sql = @"select count(*) from Lock_HotelApply a left join aspnet_Members b on a.UserId=b.UserId where SHZT=" + zt;
                    return Convert.ToInt32(dbc.ExecuteScalar(sql).ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [CSMethod("GetHotelInfo")]
        public object GetHotelInfo(int ID)
        {
            using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
            {
                try
                {
                    string sqlStr = "select * from Lock_HotelApply where ID=" + ID;
                    DataTable dt = dbc.ExecuteDataTable(sqlStr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [CSMethod("SHHotel")]
        public object SHHotel(int ID, int result)
        {
            using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
            {
                dbc.BeginTransaction();
                try
                {
                    string sqlStr = "update Lock_HotelApply set SHZT=" + result + " where ID=" + ID;
                    dbc.ExecuteNonQuery(sqlStr);
                    if (result == 3)
                    {
                        int HotelId = Convert.ToInt16(dbc.ExecuteScalar("SELECT IDENT_CURRENT('Lock_Hotel') + IDENT_INCR('Lock_Hotel')").ToString());

                        DataTable dtApply = dbc.ExecuteDataTable("select * from Lock_HotelApply where ID=" + ID);
                        dtApply.TableName = "Lock_Hotel";
                        dtApply.Columns.Remove("ID");
                        dbc.InsertTable(dtApply);

                        DataTable dtTag = dbc.ExecuteDataTable("select * from Lock_ApplyTag where PID=" + ID + " and ZDLX=1");
                        foreach (DataRow dr in dtTag.Rows)
                        {
                            dr["PID"] = HotelId;
                        }
                        dtTag.TableName = "Lock_Tag";
                        dbc.InsertTable(dtTag);
                    }
                    using (DBConnection db = new DBConnection())
                    {
                        db.BeginTransaction();
                        try
                        {
                            if (result == 2)
                            {
                                int flowId = Flow.GetFlowId(db, ID.ToString(), "门店申请");
                                int setpId = Flow.GetStepId(db, flowId);
                                Flow.FinishStep(db, setpId, 1, "经纪人审核通过");
                                Flow.SetStep(db, flowId, 2, "管理员审核", 1, "");
                            }
                            if (result == 3)
                            {
                                int flowId = Flow.GetFlowId(db, ID.ToString(), "门店申请");
                                int setpId = Flow.GetStepId(db, flowId);
                                Flow.FinishFlow(db, flowId);
                                Flow.FinishStep(db, setpId, 1, "管理员审核通过");
                            }
                            db.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            db.RoolbackTransaction();
                            throw ex;
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

        [CSMethod("getUserInfo")]
        public object getUserInfo(string phone)
        {
            using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
            {
                try
                {
                    string sql = @"select a.RealName,d.RoleName,a.CellPhone,b.IdCardNo
  from aspnet_Members a left join aspnet_Users b on a.UserId=b.UserId
  left join aspnet_UsersInRoles c on a.UserId=c.UserId
  left join aspnet_Roles d on c.RoleId=d.RoleId
  where a.CellPhone= " + dbc.ToSqlValue(phone) + "  order by d.RoleName desc";
                    DataTable dt = dbc.ExecuteDataTable(sql);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [CSMethod("GetTag")]
        public object GetTag(int lx, string pid)
        {
            using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
            {
                try
                {
                    string sqlStr = "";
                    if (string.IsNullOrEmpty(pid))
                        sqlStr = "select *,'' VALUE from Lock_ZDB where LX=" + lx + " order by XH";
                    else
                        sqlStr = "select a.*,b.VALUE from Lock_ZDB a left join Lock_ApplyTag b on a.ZDBID=b.ZDBID and b.ZDLX=" + lx + " and b.PID=" + pid + " where a.LX=" + lx;
                    DataTable dt = dbc.ExecuteDataTable(sqlStr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [CSMethod("SetFlow")]
        public object SetFlow(JSReader jsr, int lx, string flowName)
        {
            using (DBConnection dbc = new DBConnection())
            {
                dbc.BeginTransaction();
                try
                {
                    int id = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_b_service'").ToString());

                    string sql = "insert into tb_b_service(";
                    sql += "BZ,";
                    sql += "LX,";
                    sql += "PID,";
                    sql += "ISEND,";
                    sql += "QSRQ,";
                    sql += "JSRQ,";
                    sql += "ADDRESS,";
                    sql += "LXR,";
                    sql += "LXDH";
                    sql += ") values(";
                    sql += "@BZ,";
                    sql += "@LX,";
                    sql += "@PID,";
                    sql += "@ISEND,";
                    sql += "@QSRQ,";
                    sql += "@JSRQ,";
                    sql += "@ADDRESS,";
                    sql += "@LXR,";
                    sql += "@LXDH";
                    sql += ")";

                    MySqlCommand cmd = new MySqlCommand(sql);
                    if (jsr["BZ"].ToString() != "")
                        cmd.Parameters.Add("@BZ", jsr["BZ"].ToString());
                    else
                        cmd.Parameters.Add("@BZ", DBNull.Value);

                    if (jsr["QSRQ"].ToString() != "")
                        cmd.Parameters.Add("@QSRQ", jsr["QSRQ"].ToString());
                    else
                        cmd.Parameters.Add("@QSRQ", DBNull.Value);

                    if (jsr["JSRQ"].ToString() != "")
                        cmd.Parameters.Add("@JSRQ", jsr["JSRQ"].ToString());
                    else
                        cmd.Parameters.Add("@JSRQ", DBNull.Value);

                    if (jsr["ADDRESS"].ToString() != "")
                        cmd.Parameters.Add("@ADDRESS", jsr["ADDRESS"].ToString());
                    else
                        cmd.Parameters.Add("@ADDRESS", DBNull.Value);

                    if (jsr["LXR"].ToString() != "")
                        cmd.Parameters.Add("@LXR", jsr["LXR"].ToString());
                    else
                        cmd.Parameters.Add("@LXR", DBNull.Value);

                    if (jsr["LXDH"].ToString() != "")
                        cmd.Parameters.Add("@LXDH", jsr["LXDH"].ToString());
                    else
                        cmd.Parameters.Add("@LXDH", DBNull.Value);
                    cmd.Parameters.Add("@LX", lx);
                    cmd.Parameters.Add("@PID", jsr["ID"].ToString());
                    cmd.Parameters.Add("@ISEND", "0");
                    dbc.ExecuteNonQuery(cmd);

                    if (lx == 1)
                    {
                        if (flowName.Contains("冻结"))
                            dbc.ExecuteNonQuery("update tb_b_landlord set ZT=2 where ID=" + jsr["ID"].ToString());
                        else if (flowName.Contains("解冻"))
                            dbc.ExecuteNonQuery("update tb_b_landlord set ZT=3 where ID=" + jsr["ID"].ToString());
                        else if (flowName.Contains("续签"))
                            dbc.ExecuteNonQuery("update tb_b_landlord set ZT=4 where ID=" + jsr["ID"].ToString());
                    }
                    else if (lx == 2)
                    {
                        if (flowName.Contains("冻结"))
                            dbc.ExecuteNonQuery("update tb_b_agent set ZT=2 where ID=" + jsr["ID"].ToString());
                        else if (flowName.Contains("解冻"))
                            dbc.ExecuteNonQuery("update tb_b_agent set ZT=3 where ID=" + jsr["ID"].ToString());
                        else if (flowName.Contains("续签"))
                            dbc.ExecuteNonQuery("update tb_b_agent set ZT=4 where ID=" + jsr["ID"].ToString());
                    }
                    else if (lx == 3)
                    {
                        if (flowName.Contains("冻结"))
                            dbc.ExecuteNonQuery("update tb_b_cleaning set ZT=2 where ID=" + jsr["ID"].ToString());
                        else if (flowName.Contains("解冻"))
                            dbc.ExecuteNonQuery("update tb_b_cleaning set ZT=3 where ID=" + jsr["ID"].ToString());
                        else if (flowName.Contains("续签"))
                            dbc.ExecuteNonQuery("update tb_b_cleaning set ZT=4 where ID=" + jsr["ID"].ToString());
                    }

                    int flowId = Flow.SetFlow(dbc, id.ToString(), flowName);
                    Flow.SetStep(dbc, flowId, 1, "发起" + flowName + "申请", 1, "");

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

        [CSMethod("GetTaskList")]
        public object GetTaskList(int pagnum, int pagesize, int zt)
        {
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    int cp = pagnum;
                    int ac = 0;

                    string sql = "";
                    if (zt == 1)
                    {
                        sql = "select a.SERVICETYPE,b.* from tb_u_flow a inner join tb_u_flow_step b on a.FLOWID=b.FLOWID where TOUSERID=" + SystemUser.CurrentUser.UserID + " and RESULT=0";
                    }
                    else if (zt == 2)
                    {
                        sql = "select a.SERVICETYPE,b.* from tb_u_flow a inner join tb_u_flow_step b on a.FLOWID=b.FLOWID where FROMUSERID=" + SystemUser.CurrentUser.UserID + " and STATUS=0";
                    }
                    else if (zt == 3)
                    {
                        sql = "select a.SERVICETYPE,b.* from tb_u_flow a inner join tb_u_flow_step b on a.FLOWID=b.FLOWID where (FROMUSERID=" + SystemUser.CurrentUser.UserID + " or TOUSERID=" + SystemUser.CurrentUser.UserID + ") and STATUS=1";
                    }

                    //开始取分页数据
                    System.Data.DataTable dtPage = new System.Data.DataTable();
                    dtPage = dbc.GetPagedDataTable(sql + " order by CREATTIME desc", pagesize, ref cp, out ac);

                    return new { dt = dtPage, cp = cp, ac = ac };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        [CSMethod("GetTaskDetail")]
        public object GetTaskDetail(int flowId)
        {
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    string sql = @"select a.*,b.*,c.User_XM FQR,d.User_XM CLR from tb_u_flow_step a left join tb_u_flow b on a.FLOWID=b.FLOWID 
                                   left join tb_b_users c on a.FROMUSERID=c.User_ID
                                   left join tb_b_users d on a.TOUSERID=d.User_ID
                                   where a.FLOWID=" + flowId + " order by STEPID";
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
}