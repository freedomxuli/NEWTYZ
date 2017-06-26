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
           
            <Menu Name='系统维护中心'>
                <Item Name='角色管理'>
                    <Tab p='Smart.SystemPrivilege.系统维护中心_角色管理.角色管理' Name='角色管理'>approot/r/page/UserMag/JsManage.html</Tab>
                </Item>
                <Item Name='人员管理'>
                    <Tab p='' Name='人员管理'>approot/r/page/UserMag/UserManage.html</Tab>
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
           <Menu Name='待处理审核'>
                <Item Name='房东申请审核'>
                    <Tab p='' Name='房东申请审核'>approot/r/page/sqsh/fdsh.html</Tab>
                </Item>
                <Item Name='代理商申请审核'>
                    <Tab p='' Name='代理商申请审核'>approot/r/page/sqsh/dlssh.html</Tab>
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
