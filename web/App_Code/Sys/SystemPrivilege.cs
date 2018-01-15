using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///SystemPrivilege 的摘要说明
/// </summary>
namespace Smart.SystemPrivilege
{
    public class 经纪人权限_经纪人权限
    {
        public static PrivilegeDescription 待处理任务 = new PrivilegeDescription("经纪人权限_经纪人权限-待处理任务", "查看", 0);
        public static PrivilegeDescription 房东管理 = new PrivilegeDescription("经纪人权限_经纪人权限-房东管理", "查看", 1);
        public static PrivilegeDescription 代理商管理 = new PrivilegeDescription("经纪人权限_经纪人权限-代理商管理", "查看", 2);
        public static PrivilegeDescription 保洁管理 = new PrivilegeDescription("经纪人权限_经纪人权限-保洁管理", "查看", 3);
        public static PrivilegeDescription 房东申请 = new PrivilegeDescription("经纪人权限_经纪人权限-房东申请", "查看", 4);
        public static PrivilegeDescription 代理商申请 = new PrivilegeDescription("经纪人权限_经纪人权限-代理商申请", "查看", 5);
        public static PrivilegeDescription 保洁申请 = new PrivilegeDescription("经纪人权限_经纪人权限-保洁申请", "查看", 6);
        public static PrivilegeDescription 门店审核 = new PrivilegeDescription("经纪人权限_经纪人权限-门店审核", "查看", 7);
    }

    public class 财务权限_财务权限
    {
        public static PrivilegeDescription 待处理任务 = new PrivilegeDescription("财务权限_财务权限-待处理任务", "查看", 0);
        public static PrivilegeDescription 房东设备申请审核 = new PrivilegeDescription("财务权限_财务权限-房东设备申请审核", "查看", 1);
        public static PrivilegeDescription 代理商设备申请审核 = new PrivilegeDescription("财务权限_财务权限-代理商设备申请审核", "查看", 2);
        public static PrivilegeDescription 资金到账情况审核 = new PrivilegeDescription("财务权限_财务权限-资金到账情况审核", "查看", 3);

        public static PrivilegeDescription 房东提现 = new PrivilegeDescription("财务权限_财务权限-房东提现", "查看", 3);
        public static PrivilegeDescription 房客提现 = new PrivilegeDescription("财务权限_财务权限-房客提现", "查看", 4);
        public static PrivilegeDescription 保洁提现 = new PrivilegeDescription("财务权限_财务权限-保洁提现", "查看", 5);
    }

    public class 生产组权限_生产组权限
    {
        public static PrivilegeDescription 待处理任务 = new PrivilegeDescription("生产组权限_生产组权限-待处理任务", "查看", 0);
        public static PrivilegeDescription 房东设备配货 = new PrivilegeDescription("生产组权限_生产组权限-房东设备配货", "查看", 1);
        public static PrivilegeDescription 代理商设备配货 = new PrivilegeDescription("生产组权限_生产组权限-代理商设备配货", "查看", 2);
    }

    public class 客服权限_客服权限
    {
        public static PrivilegeDescription 待处理任务 = new PrivilegeDescription("客服权限_客服权限-待处理任务", "查看", 0);
        public static PrivilegeDescription 订单查询 = new PrivilegeDescription("客服权限_客服权限-订单查询", "查看", 1);
        public static PrivilegeDescription 待处理纠纷 = new PrivilegeDescription("客服权限_客服权限-待处理纠纷", "查看", 2);
        public static PrivilegeDescription 已处理纠纷 = new PrivilegeDescription("客服权限_客服权限-已处理纠纷", "查看", 3);
        public static PrivilegeDescription 待处理投诉 = new PrivilegeDescription("客服权限_客服权限-待处理投诉", "查看", 4);
        public static PrivilegeDescription 已处理投诉 = new PrivilegeDescription("客服权限_客服权限-已处理投诉", "查看", 5);
    }

    public class 订单专员权限_订单专员权限
    {
        public static PrivilegeDescription 待处理任务 = new PrivilegeDescription("订单专员权限_订单专员权限-待处理任务", "查看", 0);
        public static PrivilegeDescription 订单查询 = new PrivilegeDescription("订单专员权限_订单专员权限-订单查询", "查看", 1);
        public static PrivilegeDescription 待处理纠纷订单 = new PrivilegeDescription("订单专员权限_订单专员权限-待处理纠纷订单", "查看", 1);
        public static PrivilegeDescription 已处理纠纷订单 = new PrivilegeDescription("订单专员权限_订单专员权限-已处理纠纷订单", "查看", 2);
        public static PrivilegeDescription 待处理投诉订单 = new PrivilegeDescription("订单专员权限_订单专员权限-待处理投诉订单", "查看", 3);
        public static PrivilegeDescription 已处理投诉订单 = new PrivilegeDescription("订单专员权限_订单专员权限-已处理投诉订单", "查看", 4);

        public static PrivilegeDescription 超时未确认订单 = new PrivilegeDescription("订单专员权限_订单专员权限-超时未确认订单", "查看", 4);
        public static PrivilegeDescription 超时未审核订单 = new PrivilegeDescription("订单专员权限_订单专员权限-超时未审核订单", "查看", 4);
        public static PrivilegeDescription 超时未结算订单 = new PrivilegeDescription("订单专员权限_订单专员权限-超时未结算订单", "查看", 4);
        public static PrivilegeDescription 取消待确认订单 = new PrivilegeDescription("订单专员权限_订单专员权限-取消待确认订单", "查看", 4);
    }

    public class 代理商权限_代理商权限
    {
        public static PrivilegeDescription 设备信息 = new PrivilegeDescription("代理商权限_代理商权限-设备信息", "查看", 1);
    }

    public class 房东权限_房东权限
    {
        public static PrivilegeDescription 设备信息 = new PrivilegeDescription("房东权限_房东权限-设备信息", "查看", 1);
    }

    public class 待处理审核_待处理审核
    {
        public static PrivilegeDescription 待处理任务 = new PrivilegeDescription("待处理审核_待处理审核-待处理任务", "查看", 0);
        public static PrivilegeDescription 房东申请审核 = new PrivilegeDescription("待处理审核_待处理审核-房东申请审核", "查看", 1);
        public static PrivilegeDescription 房东设备申请审核 = new PrivilegeDescription("待处理审核_待处理审核-房东设备申请审核", "查看", 2);
        public static PrivilegeDescription 代理商申请审核 = new PrivilegeDescription("待处理审核_待处理审核-代理商申请审核", "查看", 3);
        public static PrivilegeDescription 保洁申请审核 = new PrivilegeDescription("待处理审核_待处理审核-保洁申请审核", "查看", 4);
        public static PrivilegeDescription 门店管理员审核 = new PrivilegeDescription("待处理审核_待处理审核-门店管理员审核", "查看", 5);
        public static PrivilegeDescription 门店审核 = new PrivilegeDescription("待处理审核_待处理审核-门店审核", "查看", 6);
        public static PrivilegeDescription 房东合同审核 = new PrivilegeDescription("待处理审核_待处理审核-房东合同审核", "查看", 7);
        public static PrivilegeDescription 代理商合同审核 = new PrivilegeDescription("待处理审核_待处理审核-代理商合同审核", "查看", 8);
        public static PrivilegeDescription 保洁合同审核 = new PrivilegeDescription("待处理审核_待处理审核-保洁合同审核", "查看", 9);
    }

    public class 系统维护中心_系统维护中心
    {
        public static PrivilegeDescription 角色管理 = new PrivilegeDescription("系统维护中心_系统维护中心-角色管理", "查看", 1);
        public static PrivilegeDescription 人员管理 = new PrivilegeDescription("系统维护中心_系统维护中心-人员管理", "查看", 2);
        public static PrivilegeDescription 设备管理 = new PrivilegeDescription("系统维护中心_系统维护中心-设备管理", "查看", 3);
        public static PrivilegeDescription 新闻管理 = new PrivilegeDescription("系统维护中心_系统维护中心-新闻管理", "查看", 4);
        public static PrivilegeDescription 平台规则设置 = new PrivilegeDescription("系统维护中心_系统维护中心-平台规则设置", "查看", 5);
        public static PrivilegeDescription 页面内容设置 = new PrivilegeDescription("系统维护中心_系统维护中心-页面内容设置", "查看", 6);
    }
}

