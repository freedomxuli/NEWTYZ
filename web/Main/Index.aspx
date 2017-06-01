<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Main_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>南通菜篮子管理平台</title>
    <style type="text/css">
        .x-grid-record-red table {
            color: #FF0000;
        }

        .x-grid-record-green table {
            color: #0C0;
        }

        .x-grid-record-magenta table {
            color: #FF00FF;
        }

        .x-grid-record-orange table {
            color: #FFA500;
            text-decoration: line-through;
        }
    </style>
    <link rel="Stylesheet" href="../js/extjs/resources/css/ext-all.css" />

    <link rel="Stylesheet" href="../CSS/ext-patch.css" />

    <link rel="Stylesheet" href="../CSS/Main.css" />
    <link href="../CSS/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/BoxSelect/BoxSelect.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JS/extjs/ext-all-debug-w-comments.js"></script>

    <script type="text/javascript" src="../js/extjs/ext-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../JS/BoxSelect/BoxSelect.js"></script>

    <script type="text/javascript" src="../js/json.js"></script>
    <script type="text/javascript" src="../js/fun.js"></script>
    <script type="text/javascript" src="../js/cb.js"></script>
    <link href="css/msgs-dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/PrivateMessage/pm.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.7.1.min.js"></script>

    <script type="text/javascript">
        function modifyPwd() {
            var winMP = new Ext.window.Window({
                height: 169,
                width: 318,
                modal: true,
                draggable: false,
                layout: {
                    type: 'fit'
                },
                title: '修改密码',
                items: [
                {
                    xtype: 'form',
                    id: "frmPWD",
                    border: false,
                    bodyPadding: 10,
                    items: [
                        {
                            xtype: 'textfield',
                            fieldLabel: '原密码',
                            labelWidth: 60,
                            anchor: '100%',
                            allowBlank: false,
                            name: 'oldpwd',
                            inputType: 'password'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '新密码',
                            labelWidth: 60,
                            anchor: '100%',
                            allowBlank: false,
                            inputType: 'password',
                            name: 'newpwd',
                            id: 'firstPWD'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '确认密码',
                            labelWidth: 60,
                            anchor: '100%',
                            allowBlank: false,
                            inputType: 'password',
                            id: 'secondPWD',
                            vtype: 'repwd'
                        }
                    ]
                }
                ],
                buttons: [
                    {
                        text: '确定',
                        handler: function() {
                            var form = Ext.getCmp("frmPWD");
                            if (form.form.isValid()) {
                                var values = form.form.getValues(false);
                                CS("CZCLZ.SystemUser.ModifyPassword", function() {            
                                    Ext.MessageBox.alert("确认", "修改成功");
                                }, CS.onError, values["oldpwd"], values["newpwd"]);
                            }
                        }
                    },
                    {
                        text: '取消',
                        handler: function() { this.up("window").close(); }
                    }
                ]
            });
            Ext.apply(Ext.form.field.VTypes, {
                repwd: function(val, field) {
                    return Ext.getCmp('secondPWD').getValue() == Ext.getCmp('firstPWD').getValue();
                },
                repwdText: '两次输入的密码必须相同'
            });
            winMP.show();
        }
       
        //CS("CZCLZ.SystemUser.UserID", function (retValue) {
        //    if(retValue=="9B628843-8FF6-4100-A7F9-73B4043871AD")
        //         strHelp = '<span style="text-align:right;float:right;width: 380px" class="clshelp"><a href="javascript:void(0);" onclick="showSZ();">       个性化首页     </a>　　<a href="javascript:void(0);" onclick="tc();">       系统服务中心     </a>　　<a href="javascript:void(0);" onclick="modifyPwd();">修改密码</a>　　<a href="approot/r/help.htm" target="_blank">帮助</a>　　<a onclick="suojin()" href="javascript:void(0);">缩进</a></span>';
        //    else
        //        strHelp = '<span style="text-align:right;float:right;width: 380px" class="clshelp">　<a href="javascript:void(0);" onclick="tc();">       系统服务中心     </a>　　<a href="javascript:void(0);" onclick="modifyPwd();">修改密码</a>　　<a href="approot/r/help.htm" target="_blank">帮助</a>　　<a onclick="suojin()" href="javascript:void(0);">缩进</a></span>';
        //}, CS.onError);
        var strHelp = '<span style="text-align:right;float:right;width: 380px" class="clshelp"><a href="javascript:void(0);" onclick="showSZ();">       个性化首页     </a>　　<a href="javascript:void(0);" onclick="tc();">       系统服务中心     </a>　　<a href="javascript:void(0);" onclick="modifyPwd();">修改密码</a>　　<a href="approot/r/help.htm" target="_blank">帮助</a>　　<a onclick="suojin()" href="javascript:void(0);">缩进</a></span>';

        
    </script>

    <script src="../js/Main/Index.js" type="text/javascript" defer="defer"></script>

</head>
<body>
    <form runat="server"></form>
    <div class="notify-dialog" style="display:none;" >
        <div class="header">
            <div class="left"></div>
            <div class="center"><span>我的消息</span><a href="javascript:void(0);" onclick="msgLib.close();">
                <img src="images/msg-dialog-close-btn.png" style="float: right; margin-top: 9px; margin-right: 10px;" /></a></div>
            <div class="right"></div>
        </div>
        <ul class="content">
        </ul>
        <div class="tools-container">
            <a href="javascript:void(0);" onclick="msgLib.markAllReaded();">全部标记为已读</a>
        </div>
        <div class="footer"></div>
    </div>
</body>
</html>
