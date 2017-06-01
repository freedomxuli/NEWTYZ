using Newtonsoft.Json;
using SmartFramework4v2.Data.SqlServer;
using SmartFramework4v2.Web.Common.JSON;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// MessageInfo 的摘要说明
/// </summary>
[CSClass("PM")]
public class PrivateMessage
{
    public class MessagesInfo
    {
        public enum MessagePriority
        {
            Top = 0,
            Important = 1,
            Normal = 2
        }
        public enum OpenModel
        {
            Normal = 0,
            NewWindow = 1
        }
        public enum MessageType
        {
            Notify = 0,
            ShortMessage = 1
        }
        public enum ReadStatus
        {
            Read = 0,
            Unread = 1,
            All = 2
        }
        /// <summary>
        /// 优先级 Top）置顶 Important）重要 Normal）普通
        /// </summary>
        public MessagePriority Priority { get; set; }
        /// <summary>
        /// 查看模式 Normal）普通模式 NewWindow）新窗口
        /// </summary>
        public OpenModel Model { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 接收人列表（"用户ID","用户名"）
        /// </summary>
        public SortedList<string, string> SendTo { get; set; }
        /// <summary>
        /// 打开链接
        /// </summary>
        public string OpenUrl { get; set; }
        /// <summary>
        /// 窗口名
        /// </summary>
        public string WindowName { get; set; }
        /// <summary>
        /// 消息类型 Notify）通知 ShortMessage）普通短消息
        /// </summary>
        public MessageType Type { get; set; }
        /// <summary>
        /// 附件列表 （"附件id"）
        /// </summary>
        //public List<string> AttachmentList { get; set; }

        /// <summary>
        /// AppName
        /// </summary>
        //public string AppName { get; set; }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <returns>true:成功;false:失败</returns>
        public bool SendMessage()
        {
            var mi = this;
            if (mi == null)
                throw new ArgumentNullException();
            if (SystemUser.CurrentUser == null)
                return false;
            var cUser = SystemUser.CurrentUser;
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    dbc.BeginTransaction();
                    var dtMsg = dbc.GetEmptyDataTable("TB_U_ZNXX");
                    var drMsg = dtMsg.NewRow();
                    string MsgId = Guid.NewGuid().ToString().ToUpper();
                    var CreateDate = DateTime.Now;
                    drMsg["ZNXX_ID"] = MsgId;
                    if (mi.Title == null)
                        drMsg["ZNXX_BT"] = DBNull.Value;
                    else
                        drMsg["ZNXX_BT"] = mi.Title;
                    if (mi.Content == null)
                        drMsg["ZNXX_NR"] = DBNull.Value;
                    else
                        drMsg["ZNXX_NR"] = mi.Content;

                    drMsg["ZNXX_LX"] = (int)mi.Type;
                    drMsg["FSR_ID"] = cUser.UserID;
                    drMsg["FSR_MC"] = cUser.UserName;
                    drMsg["FSR_BM_ID"] = cUser.DW_ID;
                    drMsg["FSR_BM_MC"] = cUser.User_DM;
                    drMsg["ADDTIME"] = CreateDate;
                    drMsg["UPDATETIME"] = CreateDate;
                    drMsg["ADDUSER"] = cUser.UserID;
                    drMsg["UPDATEUSER"] = cUser.UserID;
                    drMsg["STATUS"] = 0;
                    drMsg["ZNXX_YDCS"] = 0;
                    drMsg["ZNXX_YXJ"] = (int)mi.Priority;
                    drMsg["ZNXX_CKMS"] = (int)mi.Model;
                    if (mi.OpenUrl == null)
                        drMsg["ZNXX_URL"] = DBNull.Value;
                    else
                        drMsg["ZNXX_URL"] = mi.OpenUrl;
                    if (mi.WindowName == null)
                        drMsg["ZNXX_CKM"] = DBNull.Value;
                    else
                        drMsg["ZNXX_CKM"] = mi.WindowName;

                    List<string> listUserName = new List<string>();
                    var dtMsgRelate = dbc.GetEmptyDataTable("TB_U_ZNXX_GL");

                    foreach (var UserInfo in mi.SendTo)
                    {
                        listUserName.Add(UserInfo.Value);
                        var drMsgRelate = dtMsgRelate.NewRow();
                        drMsgRelate["GL_ID"] = Guid.NewGuid().ToString().ToUpper();
                        drMsgRelate["ZNXX_ID"] = MsgId;
                        drMsgRelate["FSR_ID"] = cUser.UserID;
                        drMsgRelate["JSR_ID"] = UserInfo.Key;
                        drMsgRelate["JSR_ISREADED"] = 0;
                        drMsgRelate["ADDTIME"] = CreateDate;
                        drMsgRelate["ADDUSER"] = cUser.UserID;
                        drMsgRelate["UPDATETIME"] = CreateDate;
                        drMsgRelate["UPDATEUSER"] = cUser.UserID;
                        drMsgRelate["STATUS"] = 0;
                        drMsgRelate["JSR_ISNOTIFIED"] = 0;
                        dtMsgRelate.Rows.Add(drMsgRelate);
                    }

                    drMsg["JSR_XX"] = string.Join(";", listUserName.ToArray());
                    dtMsg.Rows.Add(drMsg);
                    dbc.InsertTable(dtMsg);
                    dbc.InsertTable(dtMsgRelate);
                    //if (mi.AttachmentList != null && mi.AttachmentList.Count > 0)
                    //{
                    //    Core.AttachmentClass.ConnectFilesByPid(mi.AttachmentList.ToArray(), MsgId, Core.Common.FileStorageName);
                    //}
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

        /// <summary>
        /// 自定义发送方发送消息
        /// </summary>
        /// <param name="From">发送发名称</param>
        /// <returns>true:成功;false:失败</returns>
        public bool SendMessage(string From)
        {
            var mi = this;
            if (mi == null)
                throw new ArgumentNullException();
            if (SystemUser.CurrentUser == null)
                return false;
            using (DBConnection dbc = new DBConnection())
            {
                try
                {
                    dbc.BeginTransaction();
                    var dtMsg = dbc.GetEmptyDataTable("TB_U_ZNXX");
                    var drMsg = dtMsg.NewRow();
                    string MsgId = Guid.NewGuid().ToString().ToUpper();
                    var CreateDate = DateTime.Now;
                    drMsg["ZNXX_ID"] = MsgId;
                    if (mi.Title == null)
                        drMsg["ZNXX_BT"] = DBNull.Value;
                    else
                        drMsg["ZNXX_BT"] = mi.Title;
                    if (mi.Content == null)
                        drMsg["ZNXX_NR"] = DBNull.Value;
                    else
                        drMsg["ZNXX_NR"] = mi.Content;

                    drMsg["ZNXX_LX"] = (int)mi.Type;
                    drMsg["FSR_MC"] = From;
                    drMsg["ADDTIME"] = CreateDate;
                    drMsg["UPDATETIME"] = CreateDate;
                    drMsg["STATUS"] = 0;
                    drMsg["ZNXX_YDCS"] = 0;
                    drMsg["ZNXX_YXJ"] = (int)mi.Priority;
                    drMsg["ZNXX_CKMS"] = (int)mi.Model;
                    if (mi.OpenUrl == null)
                        drMsg["ZNXX_URL"] = DBNull.Value;
                    else
                        drMsg["ZNXX_URL"] = mi.OpenUrl;
                    if (mi.WindowName == null)
                        drMsg["ZNXX_CKM"] = DBNull.Value;
                    else
                        drMsg["ZNXX_CKM"] = mi.WindowName;
                    List<string> listUserName = new List<string>();
                    var dtMsgRelate = dbc.GetEmptyDataTable("TB_U_ZNXX_GL");

                    foreach (var UserInfo in mi.SendTo)
                    {
                        listUserName.Add(UserInfo.Value);
                        var drMsgRelate = dtMsgRelate.NewRow();
                        drMsgRelate["GL_ID"] = Guid.NewGuid().ToString().ToUpper();
                        drMsgRelate["ZNXX_ID"] = MsgId;
                        drMsgRelate["JSR_ID"] = UserInfo.Key;
                        drMsgRelate["JSR_ISREADED"] = 0;
                        drMsgRelate["ADDTIME"] = CreateDate;
                        drMsgRelate["UPDATETIME"] = CreateDate;
                        drMsgRelate["STATUS"] = 0;
                        drMsgRelate["JSR_ISNOTIFIED"] = 0;
                        dtMsgRelate.Rows.Add(drMsgRelate);
                    }

                    drMsg["JSR_XX"] = string.Join(";", listUserName.ToArray());
                    dtMsg.Rows.Add(drMsg);
                    dbc.InsertTable(dtMsg);
                    dbc.InsertTable(dtMsgRelate);
                    //if (mi.AttachmentList != null && mi.AttachmentList.Count > 0)
                    //{
                    //    Core.AttachmentClass.ConnectFilesByPid(mi.AttachmentList.ToArray(), MsgId, Core.Common.FileStorageName);
                    //}
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

    public class TreeNode
    {
        public string id { get; set; }

        [JsonIgnore]
        public string parent { get; set; }

        public string text { get; set; }

        public bool leaf { get; set; }

        public bool expanded { get; set; }

        public bool flag { get; set; }

        public string iconCls { get; set; }

        public List<TreeNode> children { get; set; }

        public TreeNode()
        {
            leaf = true;
            children = new List<TreeNode>();
        }

        public bool ShouldSerializechildren()
        {
            if (children.Count == 0)
                return false;
            else
                return true;
        }
    }
    public static class TreeNodeExtensions
    {
        public static TreeNode ToTree(List<TreeNode> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            var root = list.SingleOrDefault(x => x.parent == null);
            if (root == null) throw new InvalidOperationException("root == null");

            PopulateChildren(root, list);

            return root;
        }

        private static void PopulateChildren(TreeNode node, List<TreeNode> all)
        {
            var childs = all.Where(x => x.parent == node.id).ToList();

            foreach (var item in childs)
            {
                node.expanded = true;
                node.leaf = false;
                node.children.Add(item);
            }

            foreach (var item in childs)
                all.Remove(item);

            foreach (var item in childs)
                PopulateChildren(item, all);
        }
    }

    [CSMethod("GetInstantMassages")]
    public DataTable GetInstantMassages(int Status)
    {
        var readStatus = (MessagesInfo.ReadStatus)Status;
        using (DBConnection dbc = new DBConnection())
        {
            var cmd = dbc.CreateCommand();
            var sqlStr = @"
                    select t2.ZNXX_ID,
                           t2.ZNXX_BT,
                           t2.ZNXX_NR,
                           t2.FSR_MC,
                           t2.FSR_DW_MC,
                           t2.FSR_BM_MC,
                           t1.JSR_ISREADED,
                           t2.FSR_ID,
                           t2.JSR_XX,
                           t2.ADDTIME
                      from TB_U_ZNXX_GL t1
                      left join TB_U_ZNXX t2
                        on t1.ZNXX_ID = t2.ZNXX_ID
                     where t2.STATUS = 0
                       and t1.jsr_id = @UserId
                       and t2.status = 0
                       {0}
                     order by t2.znxx_yxj asc,t2.addtime desc";
            string searchCondition = "";
            if (readStatus == MessagesInfo.ReadStatus.Read)
            {
                searchCondition += " and JSR_ISREADED = 1";
            }
            else if (readStatus == MessagesInfo.ReadStatus.Unread)
            {
                searchCondition += " and JSR_ISREADED = 0";
            }
            sqlStr = string.Format(sqlStr, searchCondition);
            cmd.CommandText = sqlStr;
            cmd.Parameters.AddWithValue("@UserId", SystemUser.CurrentUser.UserID);
            var dtZNXX = dbc.ExecuteDataTable(cmd);
            foreach (DataRow drZNXX in dtZNXX.Rows)
            {
                if (drZNXX["ZNXX_NR"] != DBNull.Value)
                {
                    drZNXX["ZNXX_NR"] = SmartFramework4v2.Web.Common.Helper.HtmlToTxt(drZNXX["ZNXX_NR"].ToString());
                }
            }
            return dtZNXX;
        }
    }

    [CSMethod("GetInstantMassages2")]
    public object GetInstantMassages2(int pagnum, int pagesize, DateTime datetime, DateTime datetime2)
    {
        using (DBConnection dbc = new DBConnection())
        {
            int cp = pagnum;
            int ac = 0;

            var cmd = dbc.CreateCommand();
            var sqlStr = @"
                    select t2.ZNXX_ID,
                           t2.ZNXX_BT,
                           t2.ZNXX_NR,
                           t2.FSR_MC,
                           t2.FSR_DW_MC,
                           t2.FSR_BM_MC,
                           t1.JSR_ISREADED,
                           t2.FSR_ID,
                           t2.JSR_XX,
                           t2.ADDTIME
                      from TB_U_ZNXX_GL t1
                      left join TB_U_ZNXX t2
                        on t1.ZNXX_ID = t2.ZNXX_ID
                     where t2.STATUS = 0
                       and t1.jsr_id = @UserId
                       and t2.status = 0
                        {0}
                     order by t1.JSR_ISREADED desc,t2.znxx_yxj asc,t2.addtime desc";


            sqlStr = string.Format(sqlStr, " and t2.ADDTIME > '" + datetime.ToString("yyyy-MM-dd") + "' and t2.ADDTIME <= '" + datetime2.ToString("yyyy-MM-dd") + "'");


            cmd.CommandText = sqlStr;
            cmd.Parameters.AddWithValue("@UserId", SystemUser.CurrentUser.UserID);
            var dtZNXX = dbc.GetPagedDataTable(cmd, pagesize, ref cp, out ac);
            foreach (DataRow drZNXX in dtZNXX.Rows)
            {
                if (drZNXX["ZNXX_NR"] != DBNull.Value)
                {
                    drZNXX["ZNXX_NR"] = SmartFramework4v2.Web.Common.Helper.HtmlToTxt(drZNXX["ZNXX_NR"].ToString());
                }
            }
            return new { dt = dtZNXX, ac = ac, cp = cp };
        }
    }


    /// <summary>
    /// 根据接受范围获取短消息
    /// </summary>
    /// <param name="RangeType">范围类型 0）不接收 1）全部 2）指定APPNAME的短消息</param>
    /// <param name="AppName">子系统简称</param>
    /// <returns>短消息列表</returns>
    [CSMethod("GetInstantMessageByRange")]
    public DataTable GetInstantMessageByRange(int RangeType)
    {
        using (DBConnection dbc = new DBConnection())
        {
            var cmd = dbc.CreateCommand();
            var sqlStr = @"
                    select t2.ZNXX_ID,
                           t2.ZNXX_BT,
                           t2.ZNXX_NR,
                           t2.FSR_MC,
                           t2.FSR_DW_MC,
                           t2.FSR_BM_MC,
                           t1.JSR_ISREADED,
                           t2.FSR_ID,
                           t2.JSR_XX,
                           t2.ADDTIME
                      from TB_U_ZNXX_GL t1
                      left join TB_U_ZNXX t2
                        on t1.ZNXX_ID = t2.ZNXX_ID
                     where t2.STATUS = 0
                       and t1.jsr_id = @UserId
                       and t2.status = 0
                     order by t2.znxx_yxj asc,t2.addtime desc";
            cmd.CommandText = sqlStr;
            cmd.Parameters.AddWithValue("@UserId", SystemUser.CurrentUser.UserID);
            return dbc.ExecuteDataTable(cmd);
        }
    }



    public DataTable GetMassages(MessagesInfo.ReadStatus readStatus)
    {
        using (DBConnection dbc = new DBConnection())
        {
            var cmd = dbc.CreateCommand();
            var sqlStr = @"
                    select t2.ZNXX_ID,
                           t2.ZNXX_BT,
                           t2.ZNXX_LX,
                           t2.ZNXX_YXJ,
                           t2.ZNXX_URL,
                           t2.ZNXX_CKM,
                           t2.ZNXX_CKMS,
                           t2.FSR_MC,
                           t2.FSR_DW_MC,
                           t2.FSR_BM_MC,
                           t1.JSR_ISREADED,
                           t2.FSR_ID
                      from TB_U_ZNXX_GL t1
                      left join TB_U_ZNXX t2
                        on t1.ZNXX_ID = t2.ZNXX_ID
                     where t2.STATUS = 0
                       and t1.jsr_id = @UserId
                       and t2.status = 0
                       {0}
                     order by t2.znxx_yxj asc,t2.addtime desc";
            string searchCondition = "";
            if (readStatus == MessagesInfo.ReadStatus.Read)
            {
                searchCondition += " and JSR_ISREADED = 1";
            }
            else if (readStatus == MessagesInfo.ReadStatus.Unread)
            {
                searchCondition += " and JSR_ISREADED = 0";
            }
            sqlStr = string.Format(sqlStr, searchCondition);
            cmd.CommandText = sqlStr;
            cmd.Parameters.AddWithValue("@UserId", SystemUser.CurrentUser.UserID);
            return dbc.ExecuteDataTable(cmd);
        }
    }
    public DataTable GetMessageDetail(string id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            string sqlStr = @"select ZNXX_BT,ZNXX_NR,ZNXX_LX,FSR_MC,FSR_DW_MC,FSR_BM_MC,JSR_XX,ZNXX_YXJ,ZNXX_CKMS,
                    ZNXX_URL,ZNXX_CKM from  TB_U_ZNXX where STATUS = 0 and ZNXX_ID = @ZNXX_ID";
            var cmd = dbc.CreateCommand(sqlStr);
            cmd.Parameters.AddWithValue("@ZNXX_ID", id);
            return dbc.ExecuteDataTable(cmd);
        }
    }


    public string GetParentCode(string code)
    {
        if (string.IsNullOrEmpty(code))
            return null;
        var CodeLen = code.Length / 2;
        List<string> SplitCode = new List<string>();
        for (int i = 0; i < CodeLen; i++)
        {
            SplitCode.Add(code.Substring(i * 2, 2));
        }
        SplitCode.Reverse();
        for (int i = 0; i < CodeLen; i++)
        {
            if (SplitCode[i] != "00")
            {
                SplitCode[i] = "00";
                break;
            }
        }
        SplitCode.Reverse();
        return string.Join("", SplitCode.ToArray());
    }
    [CSMethod("GetUserList")]
    public object GetUserList()
    {
        using (DBConnection dbc = new DBConnection())
        {
            var cmd = dbc.CreateCommand();
            var CurrentUser = SystemUser.CurrentUser;
            cmd.CommandText = @"select distinct t1.JSR_ID as id,case when t2.[User_XM] is null then t2.[User_DM] else t2.[User_XM] end as text
                    from TB_U_ZNXX_GL t1, tb_b_Users t2 where t1.JSR_ID = t2.User_ID 
                    and t1.FSR_ID = @FSR_ID and t2.[User_Enable] = 0 and t2.[User_DelFlag] = 0";
            cmd.Parameters.AddWithValue("@FSR_ID", CurrentUser.UserID);
            var recentContactUser = dbc.ExecuteDataTable(cmd);

            List<TreeNode> tn = new List<TreeNode>();
            var PJSDNode = new TreeNode()
            {
                id = "1",
                text = "平价商店",
                leaf = false,
                flag = false,
                parent = Guid.Empty.ToString()
            };
            var WJJNode = new TreeNode()
            {
                id = "0",
                text = "物价局",
                leaf = false,
                flag = false,
                parent = Guid.Empty.ToString()
            };

            var sqlStr = @"
 select PJSD_ID as id,PJSD_MC as text from tb_b_PJSD Where PJSD_ID in
 (select DW_ID from tb_b_User_DW_GL where User_ID in 
 (select USER_ID from tb_b_Users where User_Enable = 0 and User_DelFlag = 0) and delflag = 0)
  and delflag = 0 and PJSD_Enable = 0
";
            var dtPJSD = dbc.ExecuteDataTable(sqlStr);
            foreach (DataRow drPJSD in dtPJSD.Rows)
            {
                var node = new TreeNode();
                node.expanded = false;
                node.id = drPJSD["id"].ToString();
                node.parent = "1";
                node.text = drPJSD["text"].ToString();
                node.leaf = false;
                node.flag = false;
                cmd.Parameters.Clear();
                cmd.CommandText = @"
                    select User_ID,LoginName,User_DM,User_XM from tb_b_Users where User_Enable = 0 
                    and User_DelFlag = 0 and User_ID in (select User_ID from tb_b_User_DW_GL where delflag = 0 and DW_ID = @PJSD_ID)";
                cmd.Parameters.AddWithValue("@PJSD_ID", node.id);
                var dtUser = dbc.ExecuteDataTable(cmd);
                foreach (DataRow drUser in dtUser.Rows)
                {
                    var childNode = new TreeNode();
                    childNode.text = drUser["User_DM"] == DBNull.Value ? 
                        (drUser["User_XM"] == DBNull.Value ? drUser["LoginName"].ToString() : drUser["User_XM"].ToString()) 
                        : drUser["User_DM"].ToString();
                    childNode.id = drUser["User_ID"].ToString();
                    childNode.leaf = true;
                    childNode.parent = node.id;
                    childNode.flag = true;
                    node.children.Add(childNode);
                }
                PJSDNode.children.Add(node);
            }
            sqlStr = @"
 select [DW_ID] as id,[DW_MC] as text from [tb_b_Department] Where [DW_ID] in
 (select [DW_ID] from [tb_b_User_Dw_Gl] where [User_ID] in 
 (select USER_ID from tb_b_Users where User_Enable = 0 and User_DelFlag = 0) and delflag = 0)
  and [DW_ZT] = 0 and [STATUS] = 0
";
            var dtWJJ = dbc.ExecuteDataTable(sqlStr);
            foreach (DataRow drWJJ in dtWJJ.Rows)
            {
                var node = new TreeNode();
                node.expanded = false;
                node.id = drWJJ["id"].ToString();
                node.parent = "0";
                node.text = drWJJ["text"].ToString();
                node.leaf = false;
                node.flag = false;
                cmd.Parameters.Clear();
                cmd.CommandText = @"
                    select User_ID,LoginName,User_DM,User_XM from tb_b_Users where User_Enable = 0 
                    and User_DelFlag = 0 and User_ID in (select User_ID from tb_b_User_Dw_Gl where delflag = 0 and DW_ID = @DW_ID)";
                cmd.Parameters.AddWithValue("@DW_ID", node.id);
                var dtUser = dbc.ExecuteDataTable(cmd);
                foreach (DataRow drUser in dtUser.Rows)
                {
                    var childNode = new TreeNode();
                    childNode.text = drUser["User_DM"] == DBNull.Value ?
                        (drUser["User_XM"] == DBNull.Value ? drUser["LoginName"].ToString() : drUser["User_XM"].ToString())
                        : drUser["User_DM"].ToString();
                    childNode.id = drUser["User_ID"].ToString();
                    childNode.leaf = true;
                    childNode.parent = node.id;
                    childNode.flag = true;
                    node.children.Add(childNode);
                }
                WJJNode.children.Add(node);
            }
            tn.Add(PJSDNode);
            tn.Add(WJJNode);

            var treeStr = Newtonsoft.Json.JsonConvert.SerializeObject(tn,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                }
            );

            return new { RecenctContactUser = recentContactUser, ContactUser = treeStr };
        }
    }
    /*
    [CSMethod("GetUserList")]
    public object GetUserList()
    {
        using (DBConnection dbc = new DBConnection())
        {
            var cmd = dbc.CreateCommand();
            var CurrentUser = SystemUser.CurrentUser;
            cmd.CommandText = @"select distinct t1.JSR_ID as ""id"",t2.YH_XM as ""text"" from TB_U_ZNXX_GL t1,
                    TB_B_YH t2 where t1.JSR_ID = t2.YH_ID and t1.FSR_ID = @FSR_ID and t2.STATUS = 0";
            cmd.Parameters.AddWithValue("@FSR_ID", CurrentUser.UserID);
            var recentContactUser = dbc.ExecuteDataTable(cmd);

            var sqlStr = @"
                            select t1.dq_bm || t1.wg_bm as bm,
                                t1.wg_id as id,
                                t1.wg_mc as text,
                                case
                                    when exists (select yh_id
                                            from tb_b_yh t2
                                        where t2.status = 0
                                            and t2.wg_id = t1.wg_id) then
                                    0
                                    else
                                    1
                                end leaf
                            from TB_B_WORKGROUP t1
                            where t1.wg_lx = 1
                            and t1.wg_isdel = 0
                            and t1.wg_zt = 0
                        ";

            var areacode = CurrentUser.AreaCode;

            if (areacode.EndsWith("000000"))
            {
                sqlStr += "";
            }
            else if (areacode.EndsWith("0000"))
            {
                sqlStr += string.Format(" and t1.DQ_BM like '{0}%'", areacode.Substring(0, 2));
            }
            else if (areacode.EndsWith("00"))
            {
                sqlStr += string.Format(" and t1.DQ_BM like '{0}%'", areacode.Substring(0, 4));
            }
            else
            {
                sqlStr += string.Format(" and t1.DQ_BM = '{0}'", areacode);
            }
            sqlStr += " order by t1.DQ_BM||t1.WG_BM";

            cmd.Parameters.Clear();
            var dtDept = dbc.ExecuteDataTable(sqlStr);
            var listTree = new List<TreeNode>();
            foreach (DataRow drDept in dtDept.Rows)
            {
                var code = drDept["BM"].ToString();
                var pCode = GetParentCode(code);
                var pRow = dtDept.AsEnumerable().FirstOrDefault(r => r.Field<string>("BM") == pCode);
                var ParenId = (pRow != null ? pRow["ID"].ToString() : null);
                var leaf = Convert.ToInt32(drDept["LEAF"]);
                var id = drDept["ID"].ToString();
                var nodeDept = new TreeNode();
                nodeDept.expanded = false;
                nodeDept.id = id;
                nodeDept.parent = ParenId;
                nodeDept.text = drDept["TEXT"].ToString();
                nodeDept.leaf = leaf == 1 ? true : false;
                nodeDept.flag = false;
                nodeDept.iconCls = "Chartorganisation";
                if (leaf == 0)
                {
                    var children = new List<TreeNode>();
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"select yh_id as id,wg_id as pid,yh_xm as text from tb_b_yh where status = 0 and wg_id = :wg_id order by yh_xh";
                    cmd.Parameters.AddWithValue(":wg_id", id);
                    var dtUsers = dbc.ExecuteDataTable(cmd);
                    foreach (DataRow drUsers in dtUsers.Rows)
                    {
                        var userNode = new TreeNode();
                        userNode.leaf = true;
                        userNode.expanded = false;
                        userNode.id = drUsers["ID"].ToString();
                        userNode.text = drUsers["TEXT"].ToString();
                        userNode.parent = drUsers["PID"].ToString();
                        userNode.flag = true;
                        children.Add(userNode);
                    }
                    if (children.Count > 0)
                    {
                        nodeDept.children = children;
                    }
                }
                listTree.Add(nodeDept);
            }
            var tt = TreeNodeExtensions.ToTree(listTree);
            var treeStr = Newtonsoft.Json.JsonConvert.SerializeObject(tt,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                }
            );

            return new { RecenctContactUser = recentContactUser, ContactUser = treeStr };
        }
    }
    */
    [CSMethod("SendShortMsg")]
    public void SendShortMsg(string title, string content, JSReader[] recevierList)
    {
        MessagesInfo mi = new MessagesInfo();
        mi.Title = title;
        mi.Content = content;
        mi.Priority = MessagesInfo.MessagePriority.Normal;
        mi.Type = MessagesInfo.MessageType.ShortMessage;
        mi.Model = MessagesInfo.OpenModel.Normal;
        SortedList<string, string> SendTo = new SortedList<string, string>();
        foreach (JSReader recevier in recevierList)
        {
            SendTo.Add(recevier["id"].ToString(), recevier["name"].ToString());
        }
        mi.SendTo = SendTo;
        mi.SendMessage();
    }

    [CSMethod("GetUserMessage")]
    public object GetUserMessage(int pageNum, int pageSize, string readStatus, string msgType, string mailBox)
    {
        using (DBConnection dbc = new DBConnection())
        {
            int cp = pageNum;
            int ac = 0;
            var cmd = dbc.CreateCommand();
            string sqlStr = "";
            if (mailBox == "1")
            {
                sqlStr = @"
select ZNXX_ID ID,
       ZNXX_BT TITLE,
       ZNXX_LX MSGTYPE,
       JSR_XX USERSNAME,
       ZNXX_NR CONTENT,
       ADDTIME CREATETIME,
       1 MAILBOX,
       (select case
                 when sum(case when t2.JSR_ISREADED = 1 then 1 else 0 end) = count(*) then
                  1
                 else
                  0
               end
          from tb_u_znxx_gl t2
         where t1.ZNXX_ID = t2.ZNXX_ID and t2.status = 0) as ISREADED
  from TB_U_ZNXX t1
 where STATUS = 0
   and FSR_ID =@UserId
   {0}
 order by ZNXX_YXJ asc, ADDTIME desc
                    ";
            }
            else
            {
                sqlStr = @"
                        select t2.ZNXX_ID ID,
                               t2.ZNXX_BT TITLE,
                               t2.ZNXX_LX MSGTYPE,
                               t2.FSR_MC USERSNAME,
                               t2.ZNXX_NR CONTENT,
                               t2.FSR_DW_MC USERSORGNAME,
                               t2.FSR_BM_MC USERSDEPTNAME,
                               t1.JSR_ISREADED ISREADED,
                               t2.ADDTIME CREATETIME,
                               0 MAILBOX
                          from TB_U_ZNXX_GL t1
                          left join TB_U_ZNXX t2
                            on t1.ZNXX_ID = t2.ZNXX_ID
                         where (t2.STATUS = 0 or t2.STATUS = 3)
                           and t1.jsr_id = @UserId
                           and t1.status = 0
                           {0}
                         order by t2.znxx_yxj asc,t2.addtime desc";
            }
            string searchCondition = "";
            if (!string.IsNullOrEmpty(readStatus) && mailBox != "0")
            {
                searchCondition += " and JSR_ISREADED = @JSR_ISREADED";
                cmd.Parameters.AddWithValue("@JSR_ISREADED", readStatus);
            }
            if (!string.IsNullOrEmpty(msgType))
            {
                searchCondition += " and ZNXX_LX = @ZNXX_LX";
                cmd.Parameters.AddWithValue("@ZNXX_LX", msgType);
            }
            sqlStr = string.Format(sqlStr, searchCondition);
            cmd.CommandText = sqlStr;
            cmd.Parameters.AddWithValue("@UserId", SystemUser.CurrentUser.UserID);
            var dt = dbc.GetPagedDataTable(cmd, pageSize, ref cp, out ac);
            foreach (DataRow drZNXX in dt.Rows)
            {
                if (drZNXX["CONTENT"] != DBNull.Value)
                {
                    drZNXX["CONTENT"] = SmartFramework4v2.Web.Common.Helper.HtmlToTxt(drZNXX["CONTENT"].ToString());
                }
            }
            return new { dt = dt, cp = cp, ac = ac };
        }
    }
    [CSMethod("GetUserMessageDetailWithoutChangeStatus")]
    public object GetUserMessageDetailWithoutChangeStatus(string id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                dbc.BeginTransaction();
                var sqlStr = @"select ZNXX_BT,ADDTIME,ZNXX_NR,FSR_MC,FSR_DW_MC,FSR_BM_MC,
                        JSR_XX,ZNXX_URL,ZNXX_CKMS,ZNXX_CKM,ZNXX_ID,FSR_ID,ZNXX_LX,ZNXX_APPNAME from TB_U_ZNXX where ZNXX_ID = @ZNXX_ID and STATUS = 0";
                var cmd = dbc.CreateCommand(sqlStr);
                cmd.Parameters.AddWithValue("@ZNXX_ID", id);
                var dtMessage = dbc.ExecuteDataTable(cmd);
                dbc.CommitTransaction();
                return new { MessageDetail = dtMessage};
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }
    [CSMethod("GetUserMessageDetail")]
    public object GetUserMessageDetail(string id)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                dbc.BeginTransaction();
                var sqlStr = @"select ZNXX_BT,ADDTIME,ZNXX_NR,FSR_MC,FSR_DW_MC,FSR_BM_MC,
                        JSR_XX,ZNXX_URL,ZNXX_CKMS,ZNXX_CKM,ZNXX_ID,FSR_ID,ZNXX_LX,ZNXX_APPNAME from TB_U_ZNXX where ZNXX_ID = @ZNXX_ID and STATUS = 0";
                var cmd = dbc.CreateCommand(sqlStr);
                cmd.Parameters.AddWithValue("@ZNXX_ID", id);
                var dtMessage = dbc.ExecuteDataTable(cmd);
                cmd.CommandText = "update TB_U_ZNXX_GL set JSR_ISREADED = 1 where ZNXX_ID = @ZNXX_ID and JSR_ID = @JSR_ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ZNXX_ID", id);
                cmd.Parameters.AddWithValue("@JSR_ID", SystemUser.CurrentUser.UserID);
                dbc.ExecuteNonQuery(cmd);
                dbc.CommitTransaction();
                return new { MessageDetail = dtMessage};
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    [CSMethod("MarkMessagesReaded")]
    public void MarkMessagesReaded(string[] ids)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                dbc.BeginTransaction();
                var cmd = dbc.CreateCommand();
                cmd.CommandText = "update TB_U_ZNXX_GL set JSR_ISREADED = 1 where ZNXX_ID = @ZNXX_ID and JSR_ID = @JSR_ID";
                foreach (string msgId in ids)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ZNXX_ID", msgId);
                    cmd.Parameters.AddWithValue("@JSR_ID", SystemUser.CurrentUser.UserID);
                    dbc.ExecuteNonQuery(cmd);
                }
                dbc.CommitTransaction();
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }
    [CSMethod("DeleteMsg")]
    public void DeleteMsg(string[] delIds, int mailBox)
    {
        using (DBConnection dbc = new DBConnection())
        {

            var cmd = dbc.CreateCommand();
            var CurrentUserId = SystemUser.CurrentUser.UserID;
            if (mailBox == 0)
            {
                if (delIds.Length > 0)
                {
                    var sqlStr = "update TB_U_ZNXX_GL set STATUS=1,UPDATETIME = getdate(),UPDATEUSER = @UPDATEUSER where JSR_ID=@JSR_ID and ZNXX_ID = @ZNXX_ID";
                    cmd.CommandText = sqlStr;
                    foreach (string id in delIds)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UPDATEUSER", CurrentUserId);
                        cmd.Parameters.AddWithValue("@JSR_ID", CurrentUserId);
                        cmd.Parameters.AddWithValue("@ZNXX_ID", id);
                        dbc.ExecuteNonQuery(cmd);
                    }
                }
            }
            else
            {
                if (delIds.Length > 0)
                {
                    var sqlStr = "update TB_U_ZNXX set STATUS=3,UPDATETIME = getdate(),UPDATEUSER = @UPDATEUSER where FSR_ID=@FSR_ID and ZNXX_ID = @ZNXX_ID";
                    cmd.CommandText = sqlStr;
                    foreach (string id in delIds)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UPDATEUSER", CurrentUserId);
                        cmd.Parameters.AddWithValue("@FSR_ID", CurrentUserId);
                        cmd.Parameters.AddWithValue("@ZNXX_ID", id);
                        dbc.ExecuteNonQuery(cmd);
                    }
                }
            }
        }
    }

}