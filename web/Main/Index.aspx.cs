using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Index : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SetJSVariant("UserName", SystemUser.CurrentUser.UserName);
        this.SetJSVariant("itemmsg", MenuControl.GenerateMenuByPrivilege());
        //this.SetJSVariant("top_qyid", SystemUser.CurrentUser.QY_ID);
        //if (SystemUser.CurrentUser.User_DM != "")
        //{
        //    this.SetJSVariant("UserDW", "(" + SystemUser.CurrentUser.User_DM + ")");
        //}
        //else
        //{
        //    this.SetJSVariant("UserDW", "");
        //}
        //if (User.UserType == "0")
        //{
        //    //this.SetJSVariant("yhjs", "page/JDZD/Index.html");
        //    if (UserMag.getJs_cj())
        //    {
        //        this.SetJSVariant("yhjs", "page/Sjsb/Sjsb.html");
        //    }
        //    else
        //    {
        //        //this.SetJSVariant("yhjs", "page/JDZD/Index.html");
        //        this.SetJSVariant("yhjs", "page/Leader/indexTab.html");
        //    }
        //    this.SetJSVariant("logo", "logo.gif");
        //    this.SetJSVariant("help", "help.htm");
        //}
        //else if (User.UserType == "1")
        //{
        //    this.SetJSVariant("yhjs", "page/POSCX/NewsListXXZX/Desktop.html");
        //    this.SetJSVariant("logo", "logokh.gif");
        //    this.SetJSVariant("help", "help.htm");
        //}
        //else
        //{
        //    this.SetJSVariant("yhjs", "page/jypt/ProductSend.html");
        //    this.SetJSVariant("logo", "logokh.gif");
        //    this.SetJSVariant("help", "help.htm");
        //}

        ////this.SetJSVariant("DeptName", SystemUser.CurrentUser.OrgName);
        //var cu = SystemUser.CurrentUser;
        //this.SetJSVariant("CZCLZUser", new { UserID = cu.UserID, UserName = cu.UserName, QY_ID = cu.QY_ID });
    }
}
