var AuthorizeId;
var flowId;
var stepId;
var pageSize = 15;
var isframe = true;
if (window.queryString.isframe) {
    isframe = false;
}

//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
         { name: 'FLOWID' },
          { name: 'STEPID' },
       { name: 'SERVICEID' },
       { name: 'FQR' },
       { name: 'CLR' },
       { name: 'CREATTIME' },
       { name: 'ISSUEINFO' }
    ],
    //sorters: [{ property: 'b', direction: 'DESC'}],
    onPageChange: function (sto, nPage, sorters) {
        loadData(nPage);
    }
});


function loadData(nPage) {
    var cx_no = Ext.getCmp("cx_no").getValue();

    CS('CZCLZ.PayOrderDB.GetDdqxjftsList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_no, 0, "纠纷订单");

}

function edit(serviceid, flowid, stepid) {

    AuthorizeId = serviceid;
    flowId = flowid;
    stepId = stepid;
    var win = new tsWin();
    win.show();
}


Ext.define('tsWin', {
    extend: 'Ext.window.Window',
    id: 'tsWin',
    height: 180,
    width: 478,
    layout: {
        type: 'anchor'
    },
    title: '解决纠纷',
    modal: true,
    initComponent: function () {
        var me = this;
        Ext.applyIf(me, {
            items: [
                {
                    xtype: 'form',
                    id: 'form',
                    items: [
                        {
                            xtype: 'textareafield',
                            name: 'RESULTINFO',
                            height: 102,
                            width: 436,
                            labelWidth: 60,
                            margin: '10 10 10 10',
                            fieldLabel: '回复'
                        }
                    ]
                }

            ]
        });

        me.callParent(arguments);
    },
    buttonAlign: 'center',
    buttons: [
        {
            text: '确定',
            handler: function () {
                var form = Ext.getCmp('form');
                if (form.form.isValid()) {
                    //取得表单中的内容
                    var values = form.form.getValues(false);
                    CS('CZCLZ.PayOrderDB.EndIssue', function (retVal) {
                        if (retVal) {
                            Ext.Msg.show({
                                title: '提示',
                                msg: '提交成功',
                                buttons: Ext.MessageBox.OK,
                                icon: Ext.MessageBox.INFO,
                                fn: function () {
                                    loadData(1);
                                    Ext.getCmp("tsWin").close();
                                }
                            });
                        }
                    }, CS.onError, values, AuthorizeId, flowId, stepId);
                }
            }
        },
        {
            text: '关闭',
            handler: function () {
                Ext.getCmp('tsWin').close();
            }
        }
    ]

});

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
                    columns: [Ext.create('Ext.grid.RowNumberer'),
                           {
                               xtype: 'gridcolumn',
                               flex: 1,
                               dataIndex: 'SERVICEID',
                               sortable: false,
                               menuDisabled: true,
                               align: 'center',
                               text: "授权单号"
                           },

                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'FQR',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "发起人"
                            },
                              {
                                  xtype: 'datecolumn',
                                  flex: 1,
                                  format: 'Y-m-d H:m:s',
                                  dataIndex: 'CREATTIME',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "发起时间"
                              },
                               {
                                   xtype: 'gridcolumn',
                                   flex: 3,
                                   dataIndex: 'ISSUEINFO',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "纠纷详情"
                               },
                                {
                                    xtype: 'gridcolumn',
                                    flex: 1,
                                    dataIndex: 'CLR',
                                    sortable: false,
                                    menuDisabled: true,
                                    align: 'center',
                                    text: "处理人"
                                },
                                 {
                                     text: '操作',
                                     width: 80,
                                     align: 'center',
                                     sortable: false,
                                     menuDisabled: true,
                                     renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                         var str;
                                         str = "<a href='#' onclick='edit(\"" + record.data.SERVICEID + "\",\"" + record.data.FLOWID + "\",\"" + record.data.STEPID + "\")'>解决纠纷</a>";
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
                                            id: 'cx_no',
                                            width: 160,
                                            labelWidth: 60,
                                            fieldLabel: '授权单号'
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
                                        },
                                         {
                                             xtype: 'buttongroup',
                                             title: '',
                                             items: [
                                                 {
                                                     text: '返回',
                                                     iconCls: 'back',
                                                     hidden: isframe,
                                                     handler: function () {
                                                         FrameStack.popFrame();
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

