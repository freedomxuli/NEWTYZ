﻿
var pageSize = 15;
var serviceId;
var bz = "";
var isValid = true;
var flowName;
var ishide = true;
//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'ID' },
       { name: 'LANDLORD_MC' },
       { name: 'User_XM' },
       { name: 'LANDLORD_NAME' },
       { name: 'AGENT_NAME' },
       { name: 'LANDLORD_MOBILE_TEL' },
       { name: 'LANDLORD_START_TIME' },
       { name: 'LANDLORD_END_TIME' },
       { name: 'LoginName' },
       { name: 'LANDLORD_CONTRACT_STATE' },
       { name: 'LANDLORD_CONTRACT_NUMBER' },
        { name: 'QY_NAME' },
         { name: 'ZT' },
         { name: 'IsFreeze' }

    ],
    //sorters: [{ property: 'b', direction: 'DESC'}],
    onPageChange: function (sto, nPage, sorters) {
        loadData(nPage);
    }
});

var deviceStore = Ext.create('Ext.data.Store', {
    fields: [
       { name: 'ID', type: 'string' },
       { name: 'DEVICE_NAME', type: 'string' },
       { name: 'DEVICE_NUMBER', type: 'string' },
       { name: 'DEVICE_MONEY', type: 'string' }
    ]

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

    CS('CZCLZ.JjrDB.GetFdList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_mc, cx_xm, cx_qy);

}


//************************************页面方法***************************************

function sbsq(id) {
    serviceId = id;
    var win = new deviceWin();
    win.show(null, function () {
        CS('CZCLZ.JjrDB.GetFdSbList', function (retVal) {
            deviceStore.loadData(retVal);
        }, CS.onError, id);
    });
}

function edit(v) {
    FrameStack.pushFrame({
        url: 'fdxz.html?id=' + v,
        onClose: function (ret) {
            loadData(1);
        }
    });
}

function editDevice(id) {
    var win = new addWin();
    win.show(null, function () {
        var entity = deviceStore.findRecord("ID", id).data;
        var form = Ext.getCmp("deviceform").getForm();
        form.setValues(entity);
    });

}

function delDevice(id) {
    Ext.MessageBox.confirm('删除提示', '是否要删除数据!', function (obj) {
        if (obj == "yes") {
            CS('CZCLZ.JjrDB.DeleteFdDevice', function (retVal) {
                if (retVal) {
                    CS('CZCLZ.JjrDB.GetFdSbList', function (retVal) {
                        deviceStore.loadData(retVal);
                    }, CS.onError, serviceId);
                }
            }, CS.onError, id);
        }
    });
}

function dj(id) {
    var rec = Ext.getCmp("maingrid").getStore().findRecord("ID", id);
    isValid = true;
    ishide = true;
    bz = "冻结说明";
    flowName = "房东冻结";
    var win = Ext.create('editWin');
    win.setTitle("冻结");

    win.show(null, function () {
        Ext.getCmp("editForm").getForm().setValues(rec.data);
        Ext.getCmp("sdate").setValue(rec.data.LANDLORD_START_TIME.toStdString(true));
        Ext.getCmp("edate").setValue(rec.data.LANDLORD_END_TIME.toStdString(true));
    });
}

function jd(id) {
    var rec = Ext.getCmp("maingrid").getStore().findRecord("ID", id);
    isValid = true;
    ishide = true;
    bz = "解冻说明";
    flowName = "房东解冻";
    var win = Ext.create('editWin');
    win.setTitle("解冻");
    win.show(null, function () {
        Ext.getCmp("editForm").getForm().setValues(rec.data);
        Ext.getCmp("sdate").setValue(rec.data.LANDLORD_START_TIME.toStdString(true));
        Ext.getCmp("edate").setValue(rec.data.LANDLORD_END_TIME.toStdString(true));
    });
}

function xq(id) {
    var rec = Ext.getCmp("maingrid").getStore().findRecord("ID", id);
    isValid = false;
    ishide = false;
    bz = "续签说明";
    flowName = "房东续签";
    var win = Ext.create('editWin');
    win.setTitle("续签");
    win.show(null, function () {
        Ext.getCmp("editForm").getForm().setValues(rec.data);
        Ext.getCmp("sdate").setValue(rec.data.LANDLORD_START_TIME.toStdString(true));
        Ext.getCmp("edate").setValue(rec.data.LANDLORD_END_TIME.toStdString(true));
    });
}

Ext.define('addWin', {
    extend: 'Ext.window.Window',

    height: 250,
    width: 400,
    layout: {
        type: 'fit'
    },
    id: 'deviceWin',
    closeAction: 'destroy',
    modal: true,
    title: '添加设备',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                id: 'deviceform',
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
                        id: 'DEVICE_NAME',
                        name: 'DEVICE_NAME',
                        fieldLabel: '设备类型',
                        labelWidth: 70,
                        allowBlank: false,
                        anchor: '100%'
                    },
                    {
                        xtype: 'numberfield',
                        id: 'DEVICE_NUMBER',
                        name: 'DEVICE_NUMBER',
                        fieldLabel: '设备数量',
                        labelWidth: 70,
                        allowBlank: false,
                        anchor: '100%'
                    },
                    {
                        xtype: 'numberfield',
                        id: 'DEVICE_MONEY',
                        name: 'DEVICE_MONEY',
                        fieldLabel: '金额',
                        labelWidth: 70,
                        allowBlank: false,
                        anchor: '100%'
                    }
                ],
                buttonAlign: 'center',
                buttons: [
                    {
                        text: '确定',
                        handler: function () {
                            var form = Ext.getCmp('deviceform');
                            if (form.form.isValid()) {
                                //取得表单中的内容
                                var values = form.form.getValues(false);

                                CS('CZCLZ.JjrDB.SaveFdDevice', function (retVal) {
                                    if (retVal) {
                                        CS('CZCLZ.JjrDB.GetFdSbList', function (retVal) {
                                            deviceStore.loadData(retVal);
                                        }, CS.onError, serviceId);
                                    }

                                    Ext.getCmp('deviceWin').close()
                                }, CS.onError, values, serviceId);
                            }
                        }
                    },
                    {
                        text: '取消',
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


Ext.define('deviceWin', {
    extend: 'Ext.window.Window',

    height: 300,
    width: 600,
    layout: {
        type: 'fit'
    },
    closeAction: 'destroy',
    modal: true,
    title: '设备信息',
    id: 'dWin',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'gridpanel',
                margin: '0 0 0 0',
                id: 'devicegrid',
                store: deviceStore,
                columnLines: true,
                border: true,
                autoscroll: true,
                columns: [
                     {
                         xtype: 'gridcolumn',
                         dataIndex: 'DEVICE_NAME',
                         align: 'center',
                         text: '设备类型',
                         flex: 1,
                         sortable: false,
                         menuDisabled: true
                     },

                    {
                        xtype: 'gridcolumn',
                        dataIndex: 'DEVICE_NUMBER',
                        align: 'center',
                        text: '设备数量',
                        flex: 1,
                        sortable: false,
                        menuDisabled: true
                    },
                    {
                        xtype: 'gridcolumn',
                        dataIndex: 'DEVICE_MONEY',
                        align: 'center',
                        text: '押金金额',
                        flex: 1,
                        sortable: false,
                        menuDisabled: true
                    },
                     {
                         text: '操作',
                         width: 80,
                         align: 'center',
                         sortable: false,
                         menuDisabled: true,
                         renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                             var str;
                             str = "<a href='#' onclick='editDevice(\"" + record.data.ID + "\")'>编辑</a>|<a href='#' onclick='delDevice(\"" + record.data.ID + "\")'>删除</a>";
                             return str;
                         }
                     }

                ],
                dockedItems: [
                    {
                        xtype: 'toolbar',
                        dock: 'top',
                        items: [
                               {
                                   xtype: 'buttongroup',
                                   title: '',
                                   items: [
                                       {
                                           xtype: 'button',
                                           iconCls: 'add',
                                           text: '添加设备',
                                           handler: function () {
                                               var win = new addWin();
                                               win.show();
                                           }
                                       }
                                   ]
                               }
                        ]
                    }
                ],
                buttonAlign: 'center',
                buttons: [
                    {
                        text: '提交审核',
                        handler: function () {
                            CS('CZCLZ.JjrDB.SendDeviceFlow', function (retVal) {
                                if (retVal) {
                                    Ext.Msg.show({
                                        title: '提示',
                                        msg: '提交成功!',
                                        buttons: Ext.MessageBox.OK,
                                        icon: Ext.MessageBox.INFO
                                    });
                                    Ext.getCmp('dWin').close();
                                }


                            }, CS.onError, serviceId);
                        }
                    },
                    {
                        text: '取消',
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

Ext.define('editWin', {
    extend: 'Ext.window.Window',

    height: 420,
    width: 400,
    layout: {
        type: 'fit'
    },
    id: 'editWin',
    closeAction: 'destroy',
    modal: true,
    initComponent: function () {
        var me = this;
        var lx = me.lx;
        me.items = [
            {
                xtype: 'form',
                id: 'editForm',
                frame: true,
                bodyPadding: 10,

                title: '',
                items: [
                    {
                        xtype: 'textfield',
                        name: 'ID',
                        fieldLabel: '姓名',
                        labelWidth: 70,
                        hidden: true,
                        anchor: '100%'
                    },
                    {
                        xtype: 'displayfield',
                        name: 'LANDLORD_NAME',
                        fieldLabel: '姓名',
                        labelWidth: 90,
                        anchor: '100%'
                    },
                    {
                        xtype: 'displayfield',
                        name: 'LoginName',
                        fieldLabel: '账号',
                        labelWidth: 90,
                        anchor: '100%'
                    },
                     {
                         xtype: 'displayfield',
                         name: 'LANDLORD_MOBILE_TEL',
                         fieldLabel: '联系方式',
                         labelWidth: 90,
                         anchor: '100%'
                     },
                      {
                          xtype: 'displayfield',
                          name: 'LANDLORD_CONTRACT_NUMBER',
                          fieldLabel: '合同编号',
                          labelWidth: 90,
                          anchor: '100%'
                      },
                       {
                           xtype: 'displayfield',
                           id: 'sdate',
                           fieldLabel: '原合同生效时间',
                           labelWidth: 90,
                           anchor: '100%'
                       },
                        {
                            xtype: 'displayfield',
                            id: 'edate',
                            fieldLabel: '原合同失效时间',
                            labelWidth: 90,
                            anchor: '100%'
                        },
                        {
                            xtype: 'displayfield',
                            name: 'LANDLORD_CONTRACT_STATE',
                            fieldLabel: '合同类型',
                            labelWidth: 90,
                            anchor: '100%'
                        },
                         {
                             xtype: 'datefield',
                             format: 'Y-m-d',
                             name: 'QSRQ',
                             fieldLabel: '新合同生效时间',
                             labelWidth: 90,
                             hidden: ishide,
                             allowBlank: isValid,
                             anchor: '100%'
                         },
                        {
                            xtype: 'datefield',
                            format: 'Y-m-d',
                            name: 'JSRQ',
                            fieldLabel: '新合同失效时间',
                            labelWidth: 90,
                            hidden: ishide,
                            allowBlank: isValid,
                            anchor: '100%'
                        },
                         {
                             xtype: 'textarea',
                             name: 'BZ',
                             id: 'BZ',
                             height: 80,
                             fieldLabel: bz,
                             labelWidth: 90,
                             anchor: '100%'
                         }
                ],
                buttonAlign: 'center',
                buttons: [
                    {
                        text: '提交',
                        handler: function () {
                            var form = Ext.getCmp('editForm');
                            if (form.form.isValid()) {
                                //取得表单中的内容
                                Ext.MessageBox.confirm('提示', '确定发起申请？', function (obj) {
                                    if (obj == "yes") {
                                        var values = form.form.getValues(false);
                                        CS('CZCLZ.JjrDB.SetFlow', function (retVal) {
                                            if (retVal) {
                                                Ext.getCmp('editWin').close()
                                                loadData(1);
                                            }
                                        }, CS.onError, values, 1, flowName);
                                    }
                                });
                            }
                        }
                    },
                    {
                        text: '取消',
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
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'ZT',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "状态",
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    if (value == 0)
                                        return "审核中";
                                    else if (value == 1)
                                        return "已审核";
                                    else if (value == 2)
                                        return "冻结审核中";
                                    else if (value == 3)
                                        return "解冻审核中";
                                    else if (value == 4)
                                        return "续签审核中";
                                    else if (value == 5)
                                        return "已冻结";
                                }
                            },
                            {
                                text: '操作',
                                width: 180,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    if (record.data.ZT == 1) {
                                        str = "<a href='#' onclick='edit(\"" + record.data.ID + "\")'>编辑</a>";

                                        str += "|<a href='#' onclick='dj(\"" + record.data.ID + "\")'>冻结</a>";
                                        str += "|<a href='#' onclick='xq(\"" + record.data.ID + "\")'>续签</a>";
                                        str += "|<a href='#' onclick='sbsq(\"" + record.data.ID + "\")'>设备申请</a>";
                                    }
                                    else if (record.data.ZT == 5) {
                                        str += "|<a href='#' onclick='jd(\"" + record.data.ID + "\")'>解冻</a>";
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
                                                    xtype: 'button',
                                                    iconCls: 'add',
                                                    text: '新增',
                                                    handler: function () {
                                                        FrameStack.pushFrame({
                                                            url: "fdxz.html",
                                                            onClose: function (ret) {
                                                                loadData(1);
                                                            }
                                                        });
                                                    }
                                                }
                                            ]
                                        },
                                        
                                        {
                                            xtype: 'buttongroup',
                                            title: '',
                                            items: [
                                                {
                                                    xtype: 'button',
                                                    iconCls: 'delete',
                                                    text: '删除',
                                                    handler: function () {
                                                        var idlist = [];
                                                        var grid = Ext.getCmp("maingrid");
                                                        var rds = grid.getSelectionModel().getSelection();
                                                        if (rds.length == 0) {
                                                            Ext.Msg.show({
                                                                title: '提示',
                                                                msg: '请选择至少一条要删除的记录!',
                                                                buttons: Ext.MessageBox.OK,
                                                                icon: Ext.MessageBox.INFO
                                                            });
                                                            return;
                                                        }

                                                        Ext.MessageBox.confirm('删除提示', '是否要删除数据!', function (obj) {
                                                            if (obj == "yes") {
                                                                for (var n = 0, len = rds.length; n < len; n++) {
                                                                    var rd = rds[n];

                                                                    idlist.push(rd.get("ID"));
                                                                }

                                                                CS('CZCLZ.JjrDB.DelFdByids', function (retVal) {
                                                                    if (retVal) {
                                                                        loadData(1);
                                                                    }
                                                                }, CS.onError, idlist);
                                                            }
                                                            else {
                                                                return;
                                                            }
                                                        });

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
            // Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError);

    loadData(1);
})
