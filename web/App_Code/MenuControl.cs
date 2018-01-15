using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;

/// <summary>
///MenuControl 的摘要说明
/// </summary>
public class MenuControl
{
    //<Tab p='Smart.SystemPrivilege.信息采集_平价商店数据.不符合要求的数据统计表' Name='不符合要求的数据统计表'>approot/r/page/jkpt/BFHYQTable.html</Tab>

    //<Tab p='Smart.SystemPrivilege.考核评分_网络监督考核.显示屏联网考核' Name='显示屏联网考核'>approot/r/page/LhKh/XspShow.html</Tab>

    public static String xmlMenu = @"
        <MainMenu>
             <Menu Name='经纪人权限'>
                <Item Name='待处理任务'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.待处理任务' Name='待处理任务'>approot/r/page/jjrqx/dclrw.html</Tab>
                </Item>  
               <Item Name='房东管理'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.房东管理' Name='房东管理'>approot/r/page/jjrqx/fdgl.html</Tab>
                </Item>       
                <Item Name='代理商管理'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.代理商管理' Name='代理商管理'>approot/r/page/jjrqx/dlsgl.html</Tab>
                </Item>
                    
               <Item Name='保洁管理'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.保洁管理' Name='保洁管理'>approot/r/page/jjrqx/bjgl.html</Tab>
                </Item>
                <Item Name='房东申请'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.房东申请' Name='房东申请'>approot/r/page/jjrqx/fdsqck.html</Tab>
                </Item>
                <Item Name='代理商申请'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.代理商申请' Name='代理商申请'>approot/r/page/jjrqx/dlssqck.html</Tab>
                </Item>
                
                <Item Name='保洁申请'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.保洁申请' Name='保洁申请'>approot/r/page/jjrqx/bjsqck.html</Tab>
                </Item>
                <Item Name='门店审核'>
                    <Tab p='Smart.SystemPrivilege.经纪人权限_经纪人权限.门店审核' Name='门店审核'>approot/r/page/jjrqx/mdsh.html</Tab>
                </Item>
            </Menu>
            <Menu Name='财务权限'>
                <Item Name='待处理任务'>
                    <Tab p='Smart.SystemPrivilege.财务权限_财务权限.待处理任务' Name='待处理任务'>approot/r/page/cwqx/dclrw.html</Tab>
                </Item>  
                 <Item Name='房东设备申请审核'>
                    <Tab p='Smart.SystemPrivilege.财务权限_财务权限.房东设备申请审核' Name='房东设备申请审核'>approot/r/page/cwqx/fdqrlist.html</Tab>
                </Item> 
                <Item Name='代理商设备申请审核'>
                    <Tab p='Smart.SystemPrivilege.财务权限_财务权限.代理商设备申请审核' Name='代理商设备申请审核'>approot/r/page/cwqx/dlsqrlist.html</Tab>
                </Item>
               <Item Name='资金到账情况审核'>
                    <Tab p='Smart.SystemPrivilege.财务权限_财务权限.资金到账情况审核' Name='资金到账情况审核'>approot/r/page/cwqx/dzqksh.html</Tab>
                </Item> 
                <Item Name='房东提现'>
                    <Tab p='Smart.SystemPrivilege.财务权限_财务权限.房东提现' Name='房东提现'>approot/r/page/cwqx/fdjs.html</Tab>
                </Item>     
                <Item Name='房客提现'>
                    <Tab p='Smart.SystemPrivilege.财务权限_财务权限.房客提现' Name='房客提现'>approot/r/page/cwqx/fkjs.html</Tab>
                </Item>     
                <Item Name='保洁提现'>
                    <Tab p='Smart.SystemPrivilege.财务权限_财务权限.保洁提现' Name='保洁提现'>approot/r/page/cwqx/bjjs.html</Tab>
                </Item>                
             
            </Menu>
          <Menu Name='生产组权限'>
 <Item Name='待处理任务'>
                    <Tab p='Smart.SystemPrivilege.生产组权限_生产组权限.待处理任务' Name='待处理任务'>approot/r/page/sczqx/dclrw.html</Tab>
                </Item>  
                 <Item Name='房东设备配货'>
                    <Tab p='Smart.SystemPrivilege.生产组权限_生产组权限.房东设备配货' Name='房东设备配货'>approot/r/page/sczqx/fdlist.html</Tab>
                </Item>  
                <Item Name='代理商设备配货'>
                    <Tab p='Smart.SystemPrivilege.生产组权限_生产组权限.代理商设备配货' Name='代理商设备配货'>approot/r/page/sczqx/dlslist.html</Tab>
                </Item>
                         
             
            </Menu>
          <Menu Name='客服权限'>
<Item Name='待处理任务'>
                    <Tab p='Smart.SystemPrivilege.客服权限_客服权限.待处理任务' Name='待处理任务'>approot/r/page/kfqx/dclrw.html</Tab>
                </Item>  

                <Item Name='订单查询'>
                    <Tab p='Smart.SystemPrivilege.客服权限_客服权限.订单查询' Name='订单查询'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
                <Item Name='纠纷订单'>
                    <Tab p='Smart.SystemPrivilege.客服权限_客服权限.待处理纠纷' Name='待处理纠纷'>approot/r/page/kfqx/dcljfdd.html</Tab>
                    <Tab p='Smart.SystemPrivilege.客服权限_客服权限.已处理纠纷' Name='已处理纠纷'>approot/r/page/kfqx/ycljfdd.html</Tab>
                </Item>           
                 <Item Name='投诉订单'>
                    <Tab p='Smart.SystemPrivilege.客服权限_客服权限.待处理投诉' Name='待处理投诉'>approot/r/page/kfqx/dcltsdd.html</Tab>
                    <Tab p='Smart.SystemPrivilege.客服权限_客服权限.已处理投诉' Name='已处理投诉'>approot/r/page/kfqx/ycltsdd.html</Tab>
                </Item>
            </Menu>
          <Menu Name='订单专员权限'>
<Item Name='待处理任务'>
                    <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.待处理任务' Name='待处理任务'>approot/r/page/ddqx/dclrw.html</Tab>
                </Item> 
               <Item Name='订单查询'>
                    <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.订单查询' Name='订单查询'>approot/r/page/ddqx/ddlist.html</Tab>
                </Item>
              <Item Name='超时未确认订单'>
                   <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.超时未确认订单' Name='超时未确认订单'>approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=1</Tab>
                </Item>
                <Item Name='超时未审核订单'>
                   <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.超时未审核订单' Name='超时未审核订单'>approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=2</Tab>
                </Item>
                 <Item Name='超时未结算订单'>
                   <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.超时未结算订单' Name='超时未结算订单'>approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=3</Tab>
                </Item>
                    <Item Name='取消待确认订单'>
                   <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.取消待确认订单' Name='取消待确认订单'>approot/r/page/ddqx/AuthorizeOrder/AuthorizeOrderList.html?zt=4</Tab>
                </Item>
                 <Item Name='纠纷订单'>
                    <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.待处理纠纷订单' Name='待处理纠纷订单'>approot/r/page/ddqx/dcljflist.html</Tab>
                    <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.已处理纠纷订单' Name='已处理纠纷订单'>approot/r/page/ddqx/ycljflist.html</Tab>
                </Item>
                 <Item Name='投诉订单'>
                    <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.待处理投诉订单' Name='待处理投诉订单'>approot/r/page/ddqx/dcltslist.html</Tab>
                    <Tab p='Smart.SystemPrivilege.订单专员权限_订单专员权限.已处理投诉订单' Name='已处理投诉订单'>approot/r/page/ddqx/ycltslist.html</Tab>
                </Item>
            </Menu>
          <Menu Name='代理商权限'>
                 <Item Name='设备信息'>
                    <Tab p='Smart.SystemPrivilege.代理商权限_代理商权限.设备信息' Name='设备信息'>approot/r/page/fdqx/mydevice.html</Tab>
                </Item>    
               
            </Menu>
          <Menu Name='房东权限'>
                <Item Name='设备信息'>
                    <Tab p='Smart.SystemPrivilege.房东权限_房东权限.设备信息' Name='设备信息'>approot/r/page/fdqx/mydevice.html</Tab>
                </Item>            
             
            </Menu>
          <Menu Name='待处理审核'>
<Item Name='待处理任务'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.待处理任务' Name='待处理任务'>approot/r/page/sqsh/dclrw.html</Tab>
                </Item> 
                <Item Name='房东申请审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.房东申请审核' Name='房东申请审核'>approot/r/page/sqsh/fdshlist.html</Tab>
                </Item>
                <Item Name='房东设备申请审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.房东设备申请审核' Name='房东设备申请审核'>approot/r/page/sqsh/fdsbshlist.html</Tab>
                </Item>
                <Item Name='代理商申请审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.代理商申请审核' Name='代理商申请审核'>approot/r/page/sqsh/dlsshlist.html</Tab>
                </Item>    
               
                 <Item Name='保洁申请审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.保洁申请审核' Name='保洁申请审核'>approot/r/page/sqsh/bjshlist.html</Tab>
                </Item>   
                 <Item Name='门店审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.门店审核' Name='门店审核'>approot/r/page/sqsh/mdsh.html</Tab>
                </Item>  
                <Item Name='房东合同审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.房东合同审核' Name='房东合同审核'>approot/r/page/sqsh/fdsh2.html</Tab>
                </Item>
                 <Item Name='代理商合同审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.代理商合同审核' Name='代理商合同审核'>approot/r/page/sqsh/dlssh2.html</Tab>
                </Item>
                 <Item Name='保洁合同审核'>
                    <Tab p='Smart.SystemPrivilege.待处理审核_待处理审核.保洁合同审核' Name='保洁合同审核'>approot/r/page/sqsh/bjsh2.html</Tab>
                </Item>
              
            </Menu>
            <Menu Name='系统维护中心'>
                <Item Name='角色管理'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_系统维护中心.角色管理' Name='角色管理'>approot/r/page/UserMag/JsManage.html</Tab>
                </Item>
                <Item Name='人员管理'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_系统维护中心.人员管理' Name='人员管理'>approot/r/page/UserMag/UserManage.html</Tab>
                </Item>    
                <Item Name='设备管理'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_系统维护中心.设备管理' Name='设备管理'>approot/r/page/UserMag/DeviceManage.html</Tab>
                </Item>        
              <Item Name='新闻管理'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_系统维护中心.新闻管理' Name='新闻管理'>approot/r/page/News/NewsList.html</Tab>
                </Item>
               <Item Name='平台规则设置'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_系统维护中心.平台规则设置' Name='平台规则设置'>approot/r/page/Rule/Rule.html</Tab>
                </Item>
               <Item Name='页面内容设置'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_系统维护中心.页面内容设置' Name='页面内容设置'>approot/r/page/WebSet/webset.html</Tab>
                </Item>
            </Menu>
          


           
            
          
        </MainMenu>
    ";




    public static string GenerateMenuByPrivilege()
    {
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        doc.LoadXml(xmlMenu);
        StringBuilder sb = new StringBuilder();
        int num = 0;

        var cu = SystemUser.CurrentUser;

        sb.Append("[");
        foreach (System.Xml.XmlElement MenuEL in doc.SelectNodes("/MainMenu/Menu"))
        {
            if (num > 0)
            {
                sb.Append(",");
            }
            num++;

            string title = MenuEL.GetAttribute("Name").ToString().Trim();



            string lis = "";
            foreach (System.Xml.XmlElement ItemEl in MenuEL.SelectNodes("Item"))
            {
                string secname = ItemEl.GetAttribute("Name");
                string msg = "";
                foreach (XmlElement TabEl in ItemEl.SelectNodes("Tab"))
                {
                    string p = TabEl.GetAttribute("p").ToString().Trim();
                    if (cu.CheckPrivilegeString(p))
                    {
                        string pantitle = TabEl.GetAttribute("Name").ToString().Trim();
                        string src = TabEl.InnerText;
                        if (msg == "")
                        {
                            msg += pantitle + "," + src;
                        }
                        else
                        {
                            msg += "|" + pantitle + "," + src;
                        }
                    }
                }
                if (msg != "")
                {
                    string numHtml = "";
                    int TaskNum = Task.TaskNum.GetTaskNum(msg);
                    if (TaskNum > 0)
                    {
                        numHtml = "<font style=\"color:red;font-size:13px;font-weight:bold\">+" + TaskNum + "</font>";
                    }
                    lis += "+ '<li class=\"fore\"><a class=\"MenuItem\" href=\"page/TabMenu.html?msg=" + msg + "\" target=\"mainframe\"><img height=16 width=16 align=\"absmiddle\" style=\"border:0\" src=\"../CSS/images/application.png\" />　" + secname + numHtml + "</a></li>'";

                }
            }

            if (lis != "")
            {
                sb.Append("{");
                sb.Append("xtype: 'panel',");
                sb.Append("collapsed: false,");
                sb.Append("iconCls: 'cf',");
                sb.Append("title: '" + title + "',");
                sb.Append("html: '<ul class=\"MenuHolder\">'");
                sb.Append(lis);
                sb.Append("+ '</ul>'");
                sb.Append("}");
            }
        }
        sb.Append("]");
        return sb.ToString();
    }
}
