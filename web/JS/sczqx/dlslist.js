
var pageSize = 15;


//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'ID' },
       { name: 'AGENT_MC' },
       { name: 'User_XM' },
       { name: 'AGENT_LEVEL' },
       { name: 'AGENT_NAME' },
       { name: 'AGENT_AREA' },
       { name: 'AGENT_MOBILE_TEL' },
       { name: 'AGENT_START_TIME' },
       { name: 'AGENT_END_TIME' }

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

    CS('CZCLZ.SczDB.GetDlsList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_mc, cx_xm, cx_qy);

}


function sh() {
    var win = new ShWin();
    win.show();
}


Ext.define('ShWin', {
    extend: 'Ext.window.Window',
    layout: {
        type: 'fit'
    },
    border: false,
    resizable: false,
    modal: true,
    title: '审核',
    initComponent: function () {
        var me = this;
        Ext.applyIf(me, {
            items: [
                {
                    xtype: 'form',

                    layout: {
                        type: 'fit'
                    },
                    width: 660,
                    scrollable: 'y',
                    buttonAlign: 'center',
                    buttons: [
                        {
                            text: '同意',
                            handler: function () {
                                me.close();
                            }
                        },
                          {
                              text: '拒绝',
                              handler: function () {
                                  me.close();
                              }
                          },
                        {
                            text: '取消',
                            handler: function () {
                                me.close();
                            }
                        }
                    ],
                    items: [
                        {
                            xtype: 'form',
                            autoScroll: true,
                            height: 500,
                            layout: 'column',
                            defaultType: 'displayfield',
                            bodyPadding: 6,
                            defaults: {
                                labelWidth: 120,
                                columnWidth: 0.5,
                                margin: '6 3 6 3'
                            },
                            items: [
                                {
                                    fieldLabel: '代理商名称',
                                    name: 'DW_DKBM',
                                    value: '代理商1'
                                },
                                  {
                                      fieldLabel: '代理商姓名',
                                      name: 'DW_DKBM',
                                      value: '小张'
                                  },
                                  {
                                      fieldLabel: '代理商级别',
                                      name: 'DW_DKBM',
                                      value: '一级'
                                  },
                                  {
                                      fieldLabel: '代理商电话',
                                      name: 'DW_DKBM',
                                      value: '15958456663'
                                  },
                                  {
                                      fieldLabel: '代理商邮箱',
                                      name: 'DW_DKBM',
                                      value: ''
                                  },
                                  {
                                      fieldLabel: '代理商身份证号',
                                      name: 'DW_DKBM',
                                      value: ''
                                  },
                                  {
                                      fieldLabel: '代理商身份证图片',
                                      name: 'DW_DKBM',
                                      value: '<a href="#" onclick="tp()">查看</a>'
                                  },
                                  {
                                      fieldLabel: '代理商合同编号',
                                      name: 'DW_DKBM',
                                      value: ''
                                  },
                                  {
                                      fieldLabel: '代理商合同',
                                      name: 'DW_DKBM',
                                      value: '<a href="#" onclick="tp()">查看</a>'
                                  },
                                  {
                                      fieldLabel: '申请时间',
                                      name: 'DW_DKBM',
                                      value: '2017-6-1'
                                  },
                                   {
                                       fieldLabel: '代理商合同生效时间',
                                       name: 'DW_DKBM',
                                       value: '2017-6-1'
                                   }
                                   ,
                                   {
                                       fieldLabel: '代理商合同失效时间',
                                       name: 'DW_DKBM',
                                       value: '2017-7-1'
                                   }
                                   ,
                                   {
                                       fieldLabel: '代理商合同类型',
                                       name: 'DW_DKBM'
                                   }
                                   ,
                                   {
                                       fieldLabel: '代理商所属区域',
                                       name: 'DW_DKBM',
                                       value: '新北区'
                                   }
                                   ,
                                   {
                                       fieldLabel: '签约经纪人',
                                       name: 'DW_DKBM',
                                       value: '经纪人甲'
                                   }
                                   ,
                                   {
                                       fieldLabel: '发货地址',
                                       name: 'DW_DKBM'
                                   }
                                   ,
                                   {
                                       fieldLabel: '联系人电话',
                                       name: 'DW_DKBM'
                                   }

                            ]

                        }
                    ]
                }
            ]
        });
        me.callParent(arguments);
    }
});
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
                              dataIndex: 'ID',
                              hidden: true,
                              sortable: false,
                              menuDisabled: true,
                              align: 'center',
                              text: "代理商"
                          },
                            {
                                xtype: 'gridcolumn',
                                dataIndex: 'AGENT_MC',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "代理商"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 dataIndex: 'AGENT_NAME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "姓名"
                             },
                              {
                                  xtype: 'gridcolumn',
                                  dataIndex: 'User_XM',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "经纪人"
                              },
                               {
                                   xtype: 'gridcolumn',
                                   dataIndex: 'AGENT_LEVEL',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "代理商级别"
                               },
                                {
                                    xtype: 'gridcolumn',
                                    dataIndex: 'AGENT_MOBILE_TEL',
                                    sortable: false,
                                    menuDisabled: true,
                                    align: 'center',
                                    text: "电话"
                                },

                            {
                                xtype: 'datecolumn',
                                format: 'Y-m-d',
                                dataIndex: 'AGENT_START_TIME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "合同起始日期"
                            },
                             {
                                 xtype: 'datecolumn',
                                 format: 'Y-m-d',
                                 dataIndex: 'AGENT_END_TIME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "合同结束日期"
                             },
                            {
                                xtype: 'gridcolumn',
                                dataIndex: 'AGENT_AREA',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "负责区域"
                            },
                            {
                                text: '操作',
                                width: 80,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    str = "<a href='#' onclick='sh(\"" + record.data.ID + "\")'>查看</a>";
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
                                            fieldLabel: '代理商名称'
                                        },
                                         {
                                             xtype: 'textfield',
                                             id: 'cx_xm',
                                             width: 180,
                                             labelWidth: 80,
                                             fieldLabel: '代理商姓名'
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
            Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError);

    loadData(1);
})
//************************************主界面*****************************************
