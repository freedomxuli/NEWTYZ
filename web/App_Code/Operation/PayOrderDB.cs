using MySql.Data.MySqlClient;
using SmartFramework4v2.Data;
using SmartFramework4v2.Data.MySql;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

[CSClass("PayOrderDB")]
public class PayOrderDB
{
    [CSMethod("GetPayOrderList")]
    public object GetPayOrderList(int pagnum, int pagesize, string mc, string no, string fjh, string bgmc, string sqzt,string fksj,string fdsj)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string mStr = "";
                if (!string.IsNullOrEmpty(mc))
                    mStr += " and " + dbc.C_Like("a.RealName", mc.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(no))
                    mStr += " and " + dbc.C_Like("a.AuthorizeNo", no.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(fjh))
                    mStr += " and " + dbc.C_Like("a.RoomNo", no.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(bgmc))
                    mStr += " and " + dbc.C_Like("b.HotelName", bgmc.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(sqzt))
                    mStr += " and AuthorStatus=" + sqzt;
                if (!string.IsNullOrEmpty(fksj))
                    mStr += " and " + dbc.C_Like("a.CellPhone", fksj.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(fdsj))
                    mStr += " and " + dbc.C_Like("b.Mobile", fdsj.Trim(), LikeStyle.LeftAndRightLike);

                string sqlStr = @"select a.*,b.HotelName,b.Mobile,b.CompleteAddress from Lock_AuthorizeOrder a
                                 inner join Lock_Hotel b on a.HotelId=b.ID
                                 where AuthorStatus=" + sqzt + mStr + " order by a.CreateTime desc";
                DataTable dtPage = dbc.GetPagedDataTable(sqlStr, pagesize, ref cp, out ac);
                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetUser")]
    public object GetUser(int roleId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sqlStr = "select a.User_ID VALUE,User_XM TEXT from tb_b_users a inner join tb_b_user_js_gl b on a.User_ID=b.User_ID where b.JS_ID=" + roleId + " and b.delflag=0 and a.User_Enable=0 and a.User_Delflag=0";
                DataTable dt = dbc.ExecuteDataTable(sqlStr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("SendIssue")]
    public bool SendIssue(JSReader jsr, string SERVICEID)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            dbc.BeginTransaction();
            try
            {
                var IssueState = jsr["IssueState"].ToInteger();
                var TOUSERID = jsr["TOUSERID"].ToInteger();
                var ISSUEINFO = jsr["ISSUEINFO"].ToString();

                SetFlow(IssueState, SERVICEID, TOUSERID, ISSUEINFO);

                string sqlStr = "update Lock_AuthorizeOrder set IssueState=" + IssueState + " where AuthorizeNo=" + SERVICEID;
                dbc.ExecuteNonQuery(sqlStr);
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

    public void SetFlow(int IssueState, string SERVICEID, int toUser, string issueInfo)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                string serviceType = "";
                if (IssueState == 1)
                    serviceType = "纠纷订单";
                else
                    serviceType = "投诉订单";
                int flowId = Convert.ToInt16(dbc.ExecuteScalar("select AUTO_INCREMENT from INFORMATION_SCHEMA.TABLES where TABLE_NAME='tb_u_flow'").ToString());
                string sqlStr = "insert into tb_u_flow(";
                sqlStr += "SERVICEID,";
                sqlStr += "SERVICETYPE,";
                sqlStr += "STATUS";
                sqlStr += ") values(";
                sqlStr += "@SERVICEID,";
                sqlStr += "@SERVICETYPE,";
                sqlStr += "@STATUS";
                sqlStr += ")";
                MySqlCommand cmd = new MySqlCommand(sqlStr);
                cmd.Parameters.AddWithValue("@SERVICEID", SERVICEID);
                cmd.Parameters.AddWithValue("@SERVICETYPE", serviceType);
                cmd.Parameters.AddWithValue("@STATUS", 0);
                dbc.ExecuteNonQuery(cmd);

                sqlStr = "insert into tb_u_flow_step(";
                sqlStr += "FLOWID,";
                sqlStr += "STEP,";
                sqlStr += "STEPINFO,";
                sqlStr += "FROMUSERID,";
                sqlStr += "TOUSERID,";
                sqlStr += "RESULT,";
                sqlStr += "CREATTIME,";
                sqlStr += "ISSUEINFO";
                sqlStr += ") values(";
                sqlStr += "@FLOWID,";
                sqlStr += "@STEP,";
                sqlStr += "@STEPINFO,";
                sqlStr += "@FROMUSERID,";
                sqlStr += "@TOUSERID,";
                sqlStr += "@RESULT,";
                sqlStr += "@CREATTIME,";
                sqlStr += "@ISSUEINFO";
                sqlStr += ")";
                cmd = new MySqlCommand(sqlStr);
                cmd.Parameters.AddWithValue("@FLOWID", flowId);
                cmd.Parameters.AddWithValue("@STEP", 1);
                cmd.Parameters.AddWithValue("@STEPINFO", "发起纠纷/投诉");
                cmd.Parameters.AddWithValue("@FROMUSERID", SystemUser.CurrentUser.UserID);
                cmd.Parameters.AddWithValue("@TOUSERID", toUser);
                cmd.Parameters.AddWithValue("@RESULT", 0);
                cmd.Parameters.AddWithValue("@CREATTIME", DateTime.Now);
                cmd.Parameters.AddWithValue("@ISSUEINFO", issueInfo);
                dbc.ExecuteNonQuery(cmd);

                dbc.CommitTransaction();
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }


    [CSMethod("GetJfddList")]
    public object GetJfddList(int pagnum, int pagesize, string no, int reslut, string serviceType)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string mStr = "";
                if (!string.IsNullOrEmpty(no))
                    mStr += " and " + dbc.C_Like("a.SERVICEID", no.Trim(), LikeStyle.LeftAndRightLike);

                string sqlStr = @"select a.*,b.*,c.User_XM FQR,d.User_XM CLR from tb_u_flow a
                                 left join tb_u_flow_step b on a.FLOWID=b.FLOWID
                                 left join tb_b_users c on b.FROMUSERID=c.User_ID
                                 left join tb_b_users d on b.TOUSERID=d.User_ID
                                 where RESULT=" + reslut + " and SERVICETYPE='" + serviceType + "'" + mStr + " order by b.CREATTIME desc";
                DataTable dtPage = dbc.GetPagedDataTable(sqlStr, pagesize, ref cp, out ac);
                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetDdqxjftsList")]
    public object GetDdqxjftsList(int pagnum, int pagesize, string no, int reslut, string serviceType)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string mStr = " and TOUSERID=" + SystemUser.CurrentUser.UserID;
                if (!string.IsNullOrEmpty(no))
                    mStr += " and " + dbc.C_Like("a.SERVICEID", no.Trim(), LikeStyle.LeftAndRightLike);

                string sqlStr = @"select a.SERVICEID,b.*,c.User_XM FQR,d.User_XM CLR from tb_u_flow a
                                 left join tb_u_flow_step b on a.FLOWID=b.FLOWID
                                 left join tb_b_users c on b.FROMUSERID=c.User_ID
                                 left join tb_b_users d on b.TOUSERID=d.User_ID
                                 where RESULT=" + reslut + " and SERVICETYPE='" + serviceType + "'" + mStr + " order by b.CREATTIME desc";
                DataTable dtPage = dbc.GetPagedDataTable(sqlStr, pagesize, ref cp, out ac);
                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("EndIssue")]
    public bool EndIssue(JSReader jsr, string SERVICEID, int flowId, int setpId)
    {
        using (SmartFramework4v2.Data.SqlServer.DBConnection dbc = new SmartFramework4v2.Data.SqlServer.DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            dbc.BeginTransaction();
            try
            {
                var RESULTINFO = jsr["RESULTINFO"].ToString();

                SetFlow2(RESULTINFO, flowId, setpId);

                string sqlStr = "update Lock_AuthorizeOrder set IssueState=0 where AuthorizeNo=" + SERVICEID;
                dbc.ExecuteNonQuery(sqlStr);
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

    public void SetFlow2(string RESULTINFO, int flowId, int setpId)
    {
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                dbc.ExecuteNonQuery("update tb_u_flow set status=1 where flowid=" + flowId);
                dbc.ExecuteNonQuery("update tb_u_flow_step set RESULT=1,RESULTINFO='" + RESULTINFO + "',FINISHTIME=sysdate() where STEPID=" + setpId);
                dbc.CommitTransaction();
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }
}