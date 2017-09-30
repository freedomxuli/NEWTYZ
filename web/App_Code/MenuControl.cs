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
                <Item Name='代理商管理'>
                    <Tab p='' Name='代理商管理'>approot/r/page/jjrqx/dlsgl.html</Tab>
                </Item>
                <Item Name='房东管理'>
                    <Tab p='' Name='房东管理'>approot/r/page/jjrqx/fdgl.html</Tab>
                </Item>            
               <Item Name='保洁管理'>
                    <Tab p='' Name='保洁管理'>approot/r/page/jjrqx/bjgl.html</Tab>
                </Item>
                <Item Name='纠纷管理'>
                    <Tab p='' Name='纠纷管理'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
                <Item Name='投诉管理'>
                    <Tab p='' Name='投诉管理'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
            </Menu>
            <Menu Name='财务权限'>
                <Item Name='代理商财务确认'>
                    <Tab p='' Name='代理商财务确认'>approot/r/page/cwqx/dlsqrlist.html</Tab>
                </Item>
                <Item Name='房东财务确认'>
                    <Tab p='' Name='房东财务确认'>approot/r/page/cwqx/fdqrlist.html</Tab>
                </Item> 
                 
                <Item Name='与房东结算'>
                    <Tab p='' Name='与房东结算'>approot/r/page/cwqx/fdjs.html</Tab>
                </Item>     
                <Item Name='与房客结算'>
                    <Tab p='' Name='与房客结算'>approot/r/page/cwqx/fkjs.html</Tab>
                </Item>     
                <Item Name='与保洁结算'>
                    <Tab p='' Name='与保洁结算'>approot/r/page/cwqx/bjjs.html</Tab>
                </Item>                
             
            </Menu>
          <Menu Name='生产组权限'>
                <Item Name='代理商设备配货'>
                    <Tab p='' Name='代理商设备配货'>approot/r/page/sczqx/dlslist.html</Tab>
                </Item>
                <Item Name='房东财务设备配货'>
                    <Tab p='' Name='代理商设备配货'>approot/r/page/sczqx/fdlist.html</Tab>
                </Item>            
             
            </Menu>
          <Menu Name='客服权限'>
                <Item Name='订单查询'>
                    <Tab p='' Name='订单查询'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
                <Item Name='待处理纠纷'>
                    <Tab p='' Name='待处理纠纷'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>           
                 <Item Name='待处理投诉'>
                    <Tab p='' Name='待处理投诉'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
            </Menu>
          <Menu Name='订单专员权限'>
                <Item Name='超时未确认订单'>
                    <Tab p='' Name='超时未确认订单'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
                <Item Name='待确认取消订单'>
                    <Tab p='' Name='待确认取消订单'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>           
                 <Item Name='纠纷订单'>
                    <Tab p='' Name='纠纷订单'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
                 <Item Name='已处理订单'>
                    <Tab p='' Name='已处理订单'>approot/r/page/kfqx/ddlist.html</Tab>
                </Item>
            </Menu>
          <Menu Name='代理商权限'>
                <Item Name='信息查看'>
                    <Tab p='' Name='信息查看'>approot/r/page/dlsqx/dlslist.html</Tab>
                </Item>
               
            </Menu>
          <Menu Name='房东权限'>
                <Item Name='信息查看'>
                    <Tab p='' Name='信息查看'>approot/r/page/fdqx/fdlist.html</Tab>
                </Item>            
             
            </Menu>
          <Menu Name='待处理审核'>
                <Item Name='代理商申请审核'>
                    <Tab p='' Name='代理商申请审核'>approot/r/page/sqsh/dlsshlist.html</Tab>
                </Item>    
                <Item Name='房东申请审核'>
                    <Tab p='' Name='房东申请审核'>approot/r/page/sqsh/fdshlist.html</Tab>
                </Item>
                 <Item Name='保洁申请审核'>
                    <Tab p='' Name='保洁申请审核'>approot/r/page/sqsh/bjshlist.html</Tab>
                </Item>    
              
            </Menu>
            <Menu Name='系统维护中心'>
                <Item Name='角色管理'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_角色管理.角色管理' Name='角色管理'>approot/r/page/UserMag/JsManage.html</Tab>
                </Item>
                <Item Name='人员管理'>
                    <Tab p='' Name='人员管理'>approot/r/page/UserMag/UserManage.html</Tab>
                </Item>    
                <Item Name='设备管理'>
                    <Tab p='' Name='设备管理'>approot/r/page/UserMag/DeviceManage.html</Tab>
                </Item>        
              <Item Name='新闻管理'>
                    <Tab p='' Name='新闻管理'>approot/r/page/News/NewsList.html</Tab>
                </Item>
               <Item Name='平台规则设置'>
                    <Tab p='' Name='平台规则设置'>approot/r/page/Rule/Rule.html</Tab>
                </Item>
               <Item Name='页面内容设置'>
                    <Tab p='' Name='页面内容设置'>approot/r/page/WebSet/webset.html</Tab>
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
                    lis += "+ '<li class=\"fore\"><a class=\"MenuItem\" href=\"page/TabMenu.html?msg=" + msg + "\" target=\"mainframe\"><img height=16 width=16 align=\"absmiddle\" style=\"border:0\" src=\"../CSS/images/application.png\" />　" + secname + "</a></li>'";

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
