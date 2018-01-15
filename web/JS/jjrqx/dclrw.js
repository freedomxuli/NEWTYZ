
var pageSize = 15;

//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'FLOWID' },
       { name: 'SERVICETYPE' },
       { name: 'STATUS' },
       { name: 'CREATTIME' },
       { name: 'STEPINFO' }

    ],
    //sorters: [{ property: 'b', direction: 'DESC'}],
    onPageChange: function (sto, nPage, sorters) {
        loadData(nPage);
    }
});

var taskStore = Ext.create('Ext.data.Store', {
    fields: [
       { name: 'SERVICETYPE', type: 'string' },
       { name: 'STEPINFO', type: 'string' },
       { name: 'FQR', type: 'string' },
       { name: 'CREATTIME', type: 'string' },
       { name: 'CLR', type: 'string' },
       { name: 'FINISHTIME', type: 'string' },
        { name: 'RESULT', type: 'string' }
    ]

});

function loadData(nPage) {

    var cx_zt = Ext.getCmp("cx_zt").getValue();

    CS('CZCLZ.JjrDB.GetTaskList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_zt);

}

function ck(id) {
    var win = new taskWin();
    win.show(null, function () {
        CS('CZCLZ.JjrDB.GetTaskDetail', function (retVal) {
            taskStore.loadData(retVal);
        }, CS.onError, id);
    });
}

function cl(type) {
    var url = "";
    if (type == "门店申请")
        url = "approot/r/page/jjrqx/mdsh.html?isframe=true";

    FrameStack.pushFrame({
        url: url,
        onClose: function (ret) {
            loadData(1);
        }
    });
}

Ext.define('taskWin', {
    extend: 'Ext.window.Window',

    height: 400,
    width: 1000,
    layout: {
        type: 'fit'
    },
    modal: true,
    title: '任务明细',
    id: 'taskWin',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'gridpanel',
                margin: '0 0 0 0',
                id: 'tskgrid',
                store: taskStore,
                columnLines: true,
                border: true,
                autoscroll: true,
                columns: [
                    Ext.create('Ext.grid.RowNumberer'),
                     {
                         xtype: 'gridcolumn',
                         dataIndex: 'SERVICETYPE',
                         align: 'center',
                         text: '任务名称',
                         flex: 1,
                         sortable: false,
                         menuDisabled: true
                     },

                    {
                        xtype: 'gridcolumn',
                        dataIndex: 'STEPINFO',
                        align: 'center',
                        text: '环节名称',
                        flex: 1,
                        sortable: false,
                        menuDisabled: true
                    },
                      {
                          xtype: 'gridcolumn',
                          dataIndex: 'FQR',
                          align: 'center',
                          text: '发起人',
                          flex: 1,
                          sortable: false,
                          menuDisabled: true
                      },
                    {
                        xtype: 'datecolumn',
                        format: 'Y-m-d H:i:s',
                        flex: 1,
                        dataIndex: 'CREATTIME',
                        sortable: false,
                        menuDisabled: true,
                        align: 'center',
                        text: "发起时间"
                    },
                    {
                        xtype: 'gridcolumn',
                        dataIndex: 'CLR',
                        align: 'center',
                        text: '处理人',
                        flex: 1,
                        sortable: false,
                        menuDisabled: true
                    },
                    {
                        xtype: 'datecolumn',
                        format: 'Y-m-d H:i:s',
                        flex: 1,
                        dataIndex: 'FINISHTIME',
                        sortable: false,
                        menuDisabled: true,
                        align: 'center',
                        text: "处理时间"
                    },
                     {
                         xtype: 'gridcolumn',
                         dataIndex: 'RESULT',
                         align: 'center',
                         text: '处理结果',
                         flex: 1,
                         sortable: false,
                         menuDisabled: true,
                         renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                             if (value == 0)
                                 return "待处理";
                             if (value == 1)
                                 return "审核通过";
                             if (value == 2)
                                 return "审核不通过";
                         }
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
                              dataIndex: 'FLOWID',
                              hidden: true,
                              sortable: false,
                              menuDisabled: true,
                              align: 'center',
                              text: "ID"
                          },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'SERVICETYPE',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "任务名称"
                            },
                             {
                                 xtype: 'datecolumn',
                                 format: 'Y-m-d H:i:s',
                                 flex: 1,
                                 dataIndex: 'CREATTIME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "创建时间"
                             },
                              {
                                  xtype: 'gridcolumn',
                                  flex: 1,
                                  dataIndex: 'STEPINFO',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "所处环节"
                              },
                            {
                                text: '操作',
                                width: 120,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    var zt = Ext.getCmp("cx_zt").getValue();
                                    if (zt == 1) {
                                        str = "<a href='#' onclick='cl(\"" + record.data.SERVICETYPE + "\")'>处理</a>";
                                        str += "|<a href='#' onclick='ck(\"" + record.data.FLOWID + "\")'>明细</a>";
                                    }
                                    else if (zt == 2)
                                        str = "<a href='#' onclick='ck(\"" + record.data.FLOWID + "\")'>明细</a>";
                                    else
                                        str = "<a href='#' onclick='ck(\"" + record.data.FLOWID + "\")'>明细</a>";
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
                                             xtype: 'combobox',
                                             fieldLabel: '任务状态',
                                             width: 160,
                                             labelWidth: 60,
                                             id: 'cx_zt',
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             store: new Ext.data.ArrayStore({
                                                 fields: ['TEXT', 'VALUE'],
                                                 data: [
                                                     ['待处理', 1],
                                                     ['处理中', 2],
                                                     ['已处理', 3]
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
