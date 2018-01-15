
var pageSize = 15;


//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'WithdrawalOrderId' },
       { name: 'TxMoney' },
       { name: 'Status' },
       { name: 'CreateTime' },
       { name: 'FinishTime' },
       { name: 'OpenId' },
       { name: 'UserName' },
       { name: 'NickName' }

    ],
    //sorters: [{ property: 'b', direction: 'DESC'}],
    onPageChange: function (sto, nPage, sorters) {
        loadData(nPage);
    }
});


var JsStore = Ext.create('Ext.data.Store', {
    fields: [
       { name: 'JS_ID' },
       { name: 'JS_NAME' }
    ]
});

var dqstore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT'],
    data: [
    ]
});

function loadData(nPage) {

    var cx_mc = Ext.getCmp("cx_mc").getValue();
    var cx_xm = Ext.getCmp("cx_xm").getValue();
    var cx_zt = Ext.getCmp("cx_zt").getValue();

    CS('CZCLZ.CwDB.GetCWJSList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, "房东", cx_mc, cx_xm, cx_zt);

}

function dk(orderid, openid, money) {
    $.post("http://wx.zhisuroom.com/API/LockAPI.ashx", {
        "action": "TakeMoney",
        "orderid": orderid,
        "openid": openid,
        "money": money,
        "desc": money,
        "callback": "?"
    }, function (data) {
        alert(data);
    }, "jsonp");

}

function callback(response) {
    if (response.Status)
        Ext.Msg.show({
            title: '提示',
            msg: '打款成功！',
            buttons: Ext.MessageBox.OK,
            icon: Ext.MessageBox.INFO
        });
    else {
        var rd = jQuery.parseJSON(response.returndata);
        Ext.Msg.show({
            title: '提示',
            msg: rd.err_code_des,
            buttons: Ext.MessageBox.OK,
            icon: Ext.MessageBox.INFO
        });
    }
}

//************************************数据源*****************************************

//************************************页面方法***************************************

function tp() {
    var win = new phWin();
    win.show();
}

function sh(v) {
    FrameStack.pushFrame({
        url: 'dlsqr.html?id=' + v,
        onClose: function (ret) {
            loadData(1);
        }
    });
}

//************************************主界面*****************************************
Ext.onReady(function () {
    Ext.define('mainView', {
        extend: 'Ext.container.Viewport',

        layout: {
            type: 'fit'
        },

        initComponent: function () {
            var me = this;
            me.items = [
                {
                    xtype: 'gridpanel',
                    id: 'maingrid',
                    title: '',
                    store: store,
                    columnLines: true,
                    selModel: Ext.create('Ext.selection.CheckboxModel', {

                    }),

                    columns: [Ext.create('Ext.grid.RowNumberer'),

                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'UserName',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "手机或会员名"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 flex: 1,
                                 dataIndex: 'NickName',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "昵称"
                             },
                              {
                                  xtype: 'gridcolumn',
                                  flex: 1,
                                  dataIndex: 'TxMoney',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "打款金额"
                              },
                               {
                                   xtype: 'datecolumn',
                                   flex: 1,
                                   format: 'Y-m-d H:m:s',
                                   width: 150,
                                   dataIndex: 'CreateTime',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "创建时间"
                               },
                               {
                                   xtype: 'datecolumn',
                                   flex: 1,
                                   format: 'Y-m-d H:m:s',
                                   width: 150,
                                   dataIndex: 'FinishTime',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "结束时间"
                               },

                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'Status',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "状态",
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    if (value == 1)
                                        return "待打款";
                                    else if (value == 2)
                                        return "打款中";
                                    else if (value == 3)
                                        return "已打款";
                                    else if (value == 4)
                                        return "已拒绝";
                                    else if (value == 5)
                                        return "打款失败";
                                }
                            },

                            {
                                text: '操作',
                                width: 80,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    str = "<a href='#' onclick='dk(\"" + record.data.WithdrawalOrderId + "\",\"" + record.data.OpenId + "\",\"" + record.data.TxMoney + "\")'>打款</a>";
                                    return str;
                                }
                            }

                    ],
                    viewConfig: {

                    },
                    dockedItems: [
                                {
                                    xtype: 'toolbar',
                                    dock: 'top',
                                    items: [

                                        {
                                            xtype: 'textfield',
                                            id: 'cx_mc',
                                            width: 180,
                                            labelWidth: 80,
                                            fieldLabel: '手机或用户名'
                                        },
                                         {
                                             xtype: 'textfield',
                                             id: 'cx_xm',
                                             width: 140,
                                             labelWidth: 40,
                                             fieldLabel: '昵称'
                                         },
                                        {
                                            xtype: 'combobox',
                                            fieldLabel: '在职状态',
                                            width: 160,
                                            labelWidth: 60,
                                            id: 'cx_zt',
                                            queryMode: 'local',
                                            displayField: 'TEXT',
                                            valueField: 'VALUE',
                                            store: new Ext.data.ArrayStore({
                                                fields: ['TEXT', 'VALUE'],
                                                data: [
                                                    ['待打款', 1],
                                                    ['打款中', 2],
                                                    ['已打款', 3],
                                                    ['已拒绝', 4],
                                                    ['打款失败', 5]
                                                ]
                                            }),
                                            value: 1
                                        },
                                        {
                                            xtype: 'buttongroup',
                                            title: '',
                                            items: [
                                                {
                                                    xtype: 'button',
                                                    iconCls: 'search',
                                                    text: '查询',
                                                    handler: function () {
                                                        loadData(1);
                                                    }
                                                }
                                            ]
                                        }


                                    ]
                                },
                                {
                                    xtype: 'pagingtoolbar',
                                    displayInfo: true,
                                    store: store,
                                    dock: 'bottom'
                                }
                    ]
                }
            ];
            me.callParent(arguments);
        }
    });

    new mainView();

    loadData(1);
})
//************************************主界面*****************************************
