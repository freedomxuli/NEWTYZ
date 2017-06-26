using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///SystemPrivilege 的摘要说明
/// </summary>
namespace Smart.SystemPrivilege
{
    public class 系统维护中心_角色管理
    {
        public static PrivilegeDescription 角色管理 = new PrivilegeDescription("系统维护中心_角色管理-角色管理", "查看", 1);
        public static PrivilegeDescription 新闻管理 = new PrivilegeDescription("系统维护中心_新闻管理-新闻管理", "查看", 1);
    }
}

