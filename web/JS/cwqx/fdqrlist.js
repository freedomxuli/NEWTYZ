var isframe = true;
if (window.queryString.isframe) {
    isframe = false;
}
var pageSize = 15;


//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'ID' },
        { name: 'FLOWID' },
         { name: 'STEPID' },
       { name: 'LANDLORD_MC' },
       { name: 'User_XM' },
       { name: 'LANDLORD_NAME' },
       { name: 'AGENT_NAME' },
       { name: 'LANDLORD_MOBILE_TEL' },
       { name: 'LANDLORD_START_TIME' },
       { name: 'LANDLORD_END_TIME' },
        { name: 'QY_NAME' }

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
    var cx_qy = Ext.getCmp("cx_qy").getValue();

    CS('CZCLZ.AdminDB.GetFdSbSHList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_mc, cx_xm, cx_qy, 2);

}


function sh() {
    var win = new ShWin();
    win.show();
}


//************************************页面方法***************************************

function tp() {
    var win = new phWin();
    win.show();
}

function sh(v, flowId, stepId) {
    FrameStack.pushFrame({
        url: 'fdqr.html?id=' + v + '&flowId=' + flowId + '&stepId=' + stepId,
        onClose: function (ret) {
            loadData(1);
        }
    });
}

function zfpz(fid) {
    flowid = fid;
    var win = new zfpzWin();
    win.show(null, function () {
        CS('CZCLZ.JjrDB.GetZFPZ', function (retVal) {
            if (retVal) {
                var form = Ext.getCmp("zfpzForm");
                form.form.setValues(retVal[0]);
            }
        }, CS.onError, fid);
    });
}

Ext.define('zfpzWin', {
    extend: 'Ext.window.Window',

    height: 250,
    width: 400,
    layout: {
        type: 'fit'
    },
    id: 'zfpzWin',
    closeAction: 'destroy',
    modal: true,
    title: '支付凭证',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                id: 'zfpzForm',
                frame: true,
                bodyPadding: 10,

                title: '',
                items: [
                     {
                         xtype: 'textfield',
                         name: 'ID',
                         hidden: true,
                         fieldLabel: 'ID',
                         labelWidth: 70,
                         anchor: '100%'
                     },
                    {
                        xtype: 'textfield',
                        name: 'Account',
                        fieldLabel: '账号',
                        labelWidth: 70,
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        name: 'Payment',
                        fieldLabel: '支付方式',
                        labelWidth: 70,
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        name: 'OpeningBank',
                        fieldLabel: '开户行',
                        labelWidth: 70,
                        anchor: '100%'
                    },
                    {
                        xtype: 'datefield',
                        name: 'PayDate',
                        fieldLabel: '支付时间',
                        labelWidth: 70,
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        name: 'TradeSheet',
                        fieldLabel: '交易号',
                        labelWidth: 70,
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        name: 'AnswerSheet',
                        fieldLabel: '回单',
                        labelWidth: 70,
                        anchor: '100%'
                    }
                ],
                buttonAlign: 'center',
                buttons: [

                    {
                        text: '关闭',
                        handler: function () {
                            this.up('window').close();
                        }
                    }
                ]
            }
        ];
        me.callParent(arguments);
    }
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
                              align: 'center',
                              text: "房东"
                          },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'LANDLORD_MC',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "名称"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 flex: 1,
                                 dataIndex: 'LANDLORD_NAME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "姓名"
                             },
                              {
                                  xtype: 'gridcolumn',
                                  flex: 1,
                                  dataIndex: 'User_XM',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "经纪人"
                              },

                                {
                                    xtype: 'gridcolumn',
                                    flex: 1,
                                    dataIndex: 'LANDLORD_MOBILE_TEL',
                                    sortable: false,
                                    menuDisabled: true,
                                    align: 'center',
                                    text: "电话"
                                },

                            {
                                xtype: 'datecolumn',
                                flex: 1,
                                format: 'Y-m-d',
                                dataIndex: 'LANDLORD_START_TIME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "合同起始日期"
                            },
                             {
                                 xtype: 'datecolumn',
                                 flex: 1,
                                 format: 'Y-m-d',
                                 dataIndex: 'LANDLORD_END_TIME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "合同结束日期"
                             },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'QY_NAME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "所属区域"
                            },
                            {
                                text: '操作',
                                width: 120,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    str = "<a href='#' onclick='sh(\"" + record.data.ID + "\",\"" + record.data.FLOWID + "\",\"" + record.data.STEPID + "\")'>审核</a>";
                                    str += "|<a href='#' onclick='zfpz(\"" + record.data.FLOWID + "\")'>支付凭证</a>";
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
                                            fieldLabel: '房东名称'
                                        },
                                         {
                                             xtype: 'textfield',
                                             id: 'cx_xm',
                                             width: 180,
                                             labelWidth: 80,
                                             fieldLabel: '房东姓名'
                                         },
                                         {
                                             xtype: 'combobox',
                                             id: 'cx_qy',
                                             fieldLabel: '地区',
                                             editable: false,
                                             store: dqstore,
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             width: 140,
                                             labelWidth: 40
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

    CS('CZCLZ.YHGLClass.GetQy', function (retVal) {
        if (retVal) {
            dqstore.add([{ 'VALUE': '', 'TEXT': '所有区域' }]);
            dqstore.loadData(retVal, true);
            Ext.getCmp("cx_qy").setValue('');
        }
    }, CS.onError);

    loadData(1);
})
//************************************主界面*****************************************

