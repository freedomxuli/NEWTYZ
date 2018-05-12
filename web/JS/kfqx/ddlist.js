var AuthorizeId;
var pageSize = 15;


//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'ID' },
       { name: 'AuthorizeNo' },
       { name: 'RealName' },
       { name: 'CellPhone' },
       { name: 'AuthorStatus' },
       { name: 'CellPhone' },
       { name: 'LiveStartDate' },
       { name: 'LiveEndDate' },
       { name: 'HotelName' },
       { name: 'Mobile' },
       { name: 'CompleteAddress' },
       { name: 'RoomNo' },
       { name: 'IssueState' }
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

var userStore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT']
});

var dqstore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT'],
    data: [
    ]
});

function loadData(nPage) {

    var cx_mc = Ext.getCmp("cx_mc").getValue();
    var cx_no = Ext.getCmp("cx_no").getValue();
    var cx_fjh = Ext.getCmp("cx_fjh").getValue();
    var cx_bgmc = Ext.getCmp("cx_bgmc").getValue();
    var cx_sqzt = Ext.getCmp("cx_sqzt").getValue();

    var cx_fksj = Ext.getCmp("cx_fksj").getValue();
    var cx_fdsj = Ext.getCmp("cx_fdsj").getValue();

    CS('CZCLZ.PayOrderDB.GetPayOrderList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_mc, cx_no, cx_fjh, cx_bgmc, cx_sqzt, cx_fksj, cx_fdsj);

}


function sh() {
    var win = new ShWin();
    win.show();
}


//************************************页面方法***************************************

function ck(id) {
    AuthorizeId = id;
    var win = new tsWin();
    win.show();
}

function sh(v) {
    FrameStack.pushFrame({
        url: 'fdqr.html?id=' + v,
        onClose: function (ret) {
            loadData(1);
        }
    });
}


Ext.define('tsWin', {
    extend: 'Ext.window.Window',
    id: 'tsWin',
    height: 250,
    width: 478,
    layout: {
        type: 'anchor'
    },
    title: '纠纷/投诉',
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
                         xtype: 'combobox',
                         name: 'IssueState',
                         margin: '10 10 10 10',
                         width: 200,
                         labelWidth: 60,
                         fieldLabel: '标记为',
                         queryMode: 'local',
                         displayField: 'TEXT',
                         valueField: 'VALUE',
                         allowBlank: false,
                         store: new Ext.data.ArrayStore({
                             fields: ['TEXT', 'VALUE'],
                             data: [
                                  ['纠纷', '1'],
                                  ['投诉', '2']
                             ]
                         })
                     },
                    {
                        xtype: 'combobox',
                        name: 'TOUSERID',
                        margin: '10 10 10 10',
                        width: 200,
                        labelWidth: 60,
                        fieldLabel: '发送至',
                        queryMode: 'local',
                        displayField: 'TEXT',
                        valueField: 'VALUE',
                        allowBlank: false,
                        store: userStore
                    },
                   {
                       xtype: 'textareafield',
                       name: 'ISSUEINFO',
                       height: 102,
                       width: 436,
                       labelWidth: 60,
                       margin: '10 10 10 10',
                       fieldLabel: '事件详情'
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
                    CS('CZCLZ.PayOrderDB.SendIssue', function (retVal) {
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
                    }, CS.onError, values, AuthorizeId);
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
                    selModel: Ext.create('Ext.selection.CheckboxModel', {

                    }),

                    columns: [Ext.create('Ext.grid.RowNumberer'),
                          {
                              xtype: 'gridcolumn',
                              dataIndex: 'ID',
                              hidden: true,
                              sortable: false,
                              menuDisabled: true,
                              align: 'center'
                          },
                           {
                               xtype: 'gridcolumn',
                               flex: 1,
                               dataIndex: 'AuthorizeNo',
                               sortable: false,
                               menuDisabled: true,
                               align: 'center',
                               text: "授权单号"
                           },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'RealName',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "房客姓名"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 flex: 1,
                                 dataIndex: 'CellPhone',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "房客电话"
                             },
                              {
                                  xtype: 'datecolumn',
                                  flex: 1,
                                  format: 'Y-m-d',
                                  dataIndex: 'LiveStartDate',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "预计入住时间"
                              },

                                {
                                    xtype: 'datecolumn',
                                    flex: 1,
                                    format: 'Y-m-d',
                                    dataIndex: 'LiveEndDate',
                                    sortable: false,
                                    menuDisabled: true,
                                    align: 'center',
                                    text: "预计退房时间"
                                },

                            {
                                xtype: 'gridcolumn',
                                flex: 2,
                                dataIndex: 'HotelName',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "宾馆名称"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 flex: 1,
                                 dataIndex: 'RoomNo',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "房间号"
                             },
                             {
                                 xtype: 'gridcolumn',
                                 flex: 2,
                                 dataIndex: 'CompleteAddress',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "宾馆地址"
                             },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'Mobile',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "宾馆电话"
                            },
                            {
                                text: '操作',
                                width: 80,
                                dataIndex: 'AuthorStatus',
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str = "";
                                    if (value == 2) {
                                        if (record.data.IssueState != 1 && record.data.IssueState != 2)
                                            str = "<a href='#' onclick='ck(\"" + record.data.AuthorizeNo + "\")'>纠纷/投诉</a>";
                                        else if (record.data.IssueState == 1)
                                            str = "纠纷处理中";
                                        else if (record.data.IssueState == 2)
                                            str = "投诉处理中";
                                    }
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
                                             xtype: 'textfield',
                                             id: 'cx_mc',
                                             width: 160,
                                             labelWidth: 60,
                                             fieldLabel: '房客姓名'
                                         },
                                           {
                                               xtype: 'textfield',
                                               id: 'cx_fjh',
                                               width: 160,
                                               labelWidth: 60,
                                               fieldLabel: '房间号'
                                           },
                                             {
                                                 xtype: 'textfield',
                                                 id: 'cx_bgmc',
                                                 width: 160,
                                                 labelWidth: 60,
                                                 fieldLabel: '宾馆名称'
                                             },
                                              {
                                                  xtype: 'combobox',
                                                  id: 'cx_sqzt',
                                                  width: 160,
                                                  labelWidth: 60,
                                                  fieldLabel: '授权状态',
                                                  queryMode: 'local',
                                                  displayField: 'TEXT',
                                                  valueField: 'VALUE',
                                                  store: new Ext.data.ArrayStore({
                                                      fields: ['TEXT', 'VALUE'],
                                                      data: [
                                                           ['待授权', '1'],
                                                           ['已授权', '2'],
                                                           ['授权失败', '3'],
                                                           ['授权挂起', '5'],
                                                           ['授权关闭', '6'],
                                                           ['授权取消', '7']
                                                      ]
                                                  }),
                                                  value: '2'
                                              }

                                    ]
                                },
                                 {
                                     xtype: 'toolbar',
                                     dock: 'top',
                                     items: [

                                         {
                                             xtype: 'textfield',
                                             id:'cx_fdsj',
                                             width: 160,
                                             labelWidth: 60,
                                             fieldLabel: '房东手机'
                                         },
                                          {
                                              xtype: 'textfield',
                                              id: 'cx_fksj',
                                              width: 160,
                                              labelWidth: 60,
                                              fieldLabel: '房客手机'
                                          },
                                            {
                                                xtype: 'datefield',
                                                format:'Y-m-d',
                                                id:'cx_rzsj',
                                                width: 160,
                                                labelWidth: 60,
                                                fieldLabel: '入住时间'
                                            },
                                             {
                                                 xtype: 'datefield',
                                                 format: 'Y-m-d',
                                                 id: 'cx_ldsj',
                                                 width: 160,
                                                 labelWidth: 60,
                                                 fieldLabel: '离店时间'
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

    CS('CZCLZ.PayOrderDB.GetUser', function (retVal) {
        if (retVal) {
            userStore.loadData(retVal, true);
            // Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError, 6);

    loadData(1);
})
//************************************主界面*****************************************

