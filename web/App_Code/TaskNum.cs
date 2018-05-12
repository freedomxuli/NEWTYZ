using SmartFramework4v2.Data.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TaskNum 的摘要说明
/// </summary>
namespace Task
{
    public class TaskNum
    {
        public static int GetTaskNum(string MenuName)
        {
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    int result = 0;
                    string sql = "";
                    if (MenuName == "房东申请审核,approot/r/page/sqsh/fdshlist.html")
                    {
                        sql = @"select count(*) from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id  
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=0 and d.SERVICETYPE='房东申请' and d.STATUS=0 and e.RESULT=0";
                    }
                    else if (MenuName == "房东设备申请审核,approot/r/page/sqsh/fdsbshlist.html")
                    {
                        sql = @"select count(*) from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id  
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=1 and d.SERVICETYPE='房东设备申请' and d.STATUS=0 and e.RESULT=0 and e.step=1 and e.TOUSERID=" + SystemUser.CurrentUser.UserID;
                    }
                    else if (MenuName == "代理商申请审核,approot/r/page/sqsh/dlsshlist.html")
                    {
                        sql = @"select count(*) from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id 
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=0 and d.SERVICETYPE='代理商申请' and d.STATUS=0 and e.RESULT=0";
                    }
                    else if (MenuName == "保洁申请审核,approot/r/page/sqsh/bjshlist.html")
                    {
                        sql = @"select count(*) from tb_b_cleaning a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id  
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=0 and d.SERVICETYPE='保洁申请' and d.STATUS=0 and e.RESULT=0";
                    }
                    else if (MenuName == "门店审核,approot/r/page/sqsh/mdsh.html")
                    {
                        //                        sql = @"select count(*) from Lock_HotelApply a left join aspnet_Members b on a.UserId=b.UserId 
                        //                          where 1=1 and SHZT=2";
                        result = jjrDB.JjrDB.GetHotelNum(2);
                    }
                    else if (MenuName == "房东合同审核,approot/r/page/sqsh/fdsh2.html")
                    {
                        sql = @"select count(*) from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID
left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and (a.ZT=2 or a.ZT=3 or a.ZT=4)";
                    }
                    else if (MenuName == "代理商合同审核,approot/r/page/sqsh/dlssh2.html")
                    {
                        sql = @"select count(*) from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID 
left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and (a.ZT=2 or a.ZT=3 or a.ZT=4)";
                    }
                    else if (MenuName == "保洁合同审核,approot/r/page/sqsh/bjsh2.html")
                    {
                        sql = @"select count(*) from tb_b_agent a left join tb_b_qy b on a.QY_ID=b.QY_ID
left join tb_b_users c on a.ROLE_ID=c.User_Id where a.STATUS=0 and (a.ZT=2 or a.ZT=3 or a.ZT=4)";
                    }
                    else if (MenuName == "门店审核,approot/r/page/jjrqx/mdsh.html")
                    {
                        //                        sql = @"select count(*) from Lock_HotelApply a left join aspnet_Members b on a.UserId=b.UserId 
                        //                          where 1=1 and SHZT=1";

                        result = jjrDB.JjrDB.GetHotelNum(1);
                    }
                    else if (MenuName == "待处理纠纷,approot/r/page/kfqx/dcljfdd.html|已处理纠纷,approot/r/page/kfqx/ycljfdd.html")
                    {
                        sql = @"select count(*) from tb_u_flow a
                                 left join tb_u_flow_step b on a.FLOWID=b.FLOWID
                                 left join tb_b_users c on b.FROMUSERID=c.User_ID
                                 left join tb_b_users d on b.TOUSERID=d.User_ID
                                 where RESULT=0 and SERVICETYPE='纠纷订单'";
                    }
                    else if (MenuName == "待处理投诉,approot/r/page/kfqx/dcltsdd.html|已处理投诉,approot/r/page/kfqx/ycltsdd.html")
                    {
                        sql = @"select count(*) from tb_u_flow a
                                 left join tb_u_flow_step b on a.FLOWID=b.FLOWID
                                 left join tb_b_users c on b.FROMUSERID=c.User_ID
                                 left join tb_b_users d on b.TOUSERID=d.User_ID
                                 where RESULT=0 and SERVICETYPE='投诉订单'";
                    }
                    else if (MenuName == "超时未确认订单,approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=1")
                    {
                        result = AuthorizeOrderDB.AuthorizeOrderDB.GetAuthorizeOrderNum(1);
                    }
                    else if (MenuName == "超时未审核订单,approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=2")
                    {
                        result = AuthorizeOrderDB.AuthorizeOrderDB.GetAuthorizeOrderNum(2);
                    }
                    else if (MenuName == "超时未结算订单,approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=3")
                    {
                        result = AuthorizeOrderDB.AuthorizeOrderDB.GetAuthorizeOrderNum(3);
                    }
                    else if (MenuName == "取消待确认订单,approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=4")
                    {
                        result = AuthorizeOrderDB.AuthorizeOrderDB.GetAuthorizeOrderNum(4);
                    }
                    else if (MenuName == "待处理纠纷订单,approot/r/page/ddqx/dcljflist.html|已处理纠纷订单,approot/r/page/ddqx/ycljflist.html")
                    {
                        sql = @"select count(*) from tb_u_flow a
                                 left join tb_u_flow_step b on a.FLOWID=b.FLOWID
                                 left join tb_b_users c on b.FROMUSERID=c.User_ID
                                 left join tb_b_users d on b.TOUSERID=d.User_ID
                                 where RESULT=0 and SERVICETYPE='纠纷订单'";
                    }
                    else if (MenuName == "待处理投诉订单,approot/r/page/ddqx/dcltslist.html|已处理投诉订单,approot/r/page/ddqx/ycltslist.html")
                    {
                        sql = @"select count(*) from tb_u_flow a
                                 left join tb_u_flow_step b on a.FLOWID=b.FLOWID
                                 left join tb_b_users c on b.FROMUSERID=c.User_ID
                                 left join tb_b_users d on b.TOUSERID=d.User_ID
                                 where RESULT=0 and SERVICETYPE='投诉订单'";
                    }
                    else if (MenuName == "房东设备申请审核,approot/r/page/cwqx/fdqrlist.html")
                    {
                        sql = @"select count(*) from tb_b_landlord a left join tb_b_qy b on a.QY_ID=b.QY_ID left join tb_b_users c on a.ROLE_ID=c.User_Id  
                              right join tb_u_flow d on a.ID=d.SERVICEID
                              left join tb_u_flow_step e on d.FLOWID=e.FLOWID
                              where a.STATUS=0 and a.ZT=1 and d.SERVICETYPE='房东设备申请' and d.STATUS=0 and e.RESULT=0 and e.step=2 and e.TOUSERID=" + SystemUser.CurrentUser.UserID;
                    }
                    else if (MenuName == "代理商设备申请审核,approot/r/page/cwqx/dlsqrlist.html")
                    {
                        sql = @"";
                    }
                    else if (MenuName == "房东提现,approot/r/page/cwqx/fdjs.html")
                    {
                        result = CwDB.CwDB.GetCWJSNum("房东", "1");
                    }
                    else if (MenuName == "房客提现,approot/r/page/cwqx/fkjs.html")
                    {
                        result = CwDB.CwDB.GetCWJSNum("Member", "1");
                    }
                    else if (MenuName == "保洁提现,approot/r/page/cwqx/bjjs.html")
                    {
                        result = CwDB.CwDB.GetCWJSNum("保洁", "1");
                    }
                    else if (MenuName.Substring(0, 5) == "待处理任务")
                    {
                        sql = "select count(*) from tb_u_flow a inner join tb_u_flow_step b on a.FLOWID=b.FLOWID where TOUSERID=" + SystemUser.CurrentUser.UserID + " and RESULT=0";
                    }
                    else if (MenuName == "")
                    {
                        sql = @"";
                    }
                    //                     sql = @"select count(*) from tb_u_flow_step a left join tb_u_flow b on a.FLOWID=b.FLOWID 
                    //            where TOUSERID=" + SystemUser.CurrentUser.UserID + " and RESULT=0" + where;

                    if (sql != "")
                        result = Convert.ToInt32(dbc.ExecuteScalar(sql).ToString());
                    return result;
                }
                catch (Exception ex)
                {
                    string kk = MenuName;
                    throw ex;
                }
            }
        }
    }
}