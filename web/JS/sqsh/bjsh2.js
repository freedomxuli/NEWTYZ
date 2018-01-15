var isframe = true;
if (window.queryString.isframe) {
    isframe = false;
}
var pageSize = 15;
var serviceId;
var bz = "";
var isValid = true;
var flowName;
var ishide = true;
var pid;
//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'ID' },
       { name: 'CLEANING_NAME' },
       { name: 'CLEANING_AGE' },
       { name: 'CLEANING_SEX' },
       { name: 'CLEANING_MOBILE_TEL' },
       { name: 'CLEANING_IDENTITY_NUMBER' },
       { name: 'CONTRACT_START_TIME' },
       { name: 'CONTRACT_END_TIME' },
        { name: 'ZT' }

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

    var cx_xm = Ext.getCmp("cx_xm").getValue();


    CS('CZCLZ.AdminDB.GetBjShList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_xm);

}

function sh(id, zt) {
    pid = id;
    var rec = Ext.getCmp("maingrid").getStore().findRecord("ID", id);
    if (zt == 2) {
        bz = "冻结说明";
        ishide = true;
    }
    else if (zt == 3) {
        bz = "解冻说明";
        ishide = true;
    }
    else if (zt == 4) {
        bz = "续签说明";
        ishide = false;
    }
    pid = id;
    var win = Ext.create('editWin');
    win.setTitle("审核");

    win.show(null, function () {
        CS('CZCLZ.AdminDB.GetSHInfo', function (retVal) {
            if (retVal) {
                Ext.getCmp("editForm").getForm().setValues(rec.data);
                Ext.getCmp("BZ").setValue(retVal[0]["BZ"]);
                Ext.getCmp("QSRQ").setValue(retVal[0]["QSRQ"]);
                Ext.getCmp("JSRQ").setValue(retVal[0]["JSRQ"]);
                Ext.getCmp("sdate").setValue(rec.data.CONTRACT_START_TIME.toStdString(true));
                Ext.getCmp("edate").setValue(rec.data.CONTRACT_END_TIME.toStdString(true));
            }
        }, CS.onError, id);
    });
}

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
                        name: 'CLEANING_NAME',
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
                         name: 'CLEANING_MOBILE_TEL',
                         fieldLabel: '联系方式',
                         labelWidth: 90,
                         anchor: '100%'
                     },
                      //{
                      //    xtype: 'displayfield',
                      //    name: 'LANDLORD_CONTRACT_NUMBER',
                      //    fieldLabel: '合同编号',
                      //    labelWidth: 90,
                      //    anchor: '100%'
                      //},
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
                        //{
                        //    xtype: 'displayfield',
                        //    name: 'LANDLORD_CONTRACT_STATE',
                        //    fieldLabel: '合同类型',
                        //    labelWidth: 90,
                        //    anchor: '100%'
                        //},
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
                         text: '审核',
                         handler: function () {

                             Ext.MessageBox.confirm('提示', '确定审核通过？', function (obj) {
                                 if (obj == "yes") {
                                     CS('CZCLZ.AdminDB.SH', function (retVal) {
                                         if (retVal) {
                                             Ext.getCmp('editWin').close()
                                             loadData(1);
                                         }
                                     }, CS.onError, pid, 1);
                                 }
                             });
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


function sh() {
    var win = new ShWin();
    win.show();
}


//************************************页面方法***************************************

function tp() {
    var win = new phWin();
    win.show();
}

function edit(v) {
    FrameStack.pushFrame({
        url: 'bjxz.html?id=' + v,
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
                              text: "保洁"
                          },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'CLEANING_NAME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "姓名"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 flex: 1,
                                 dataIndex: 'CLEANING_AGE',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "年龄"
                             },
                              {
                                  xtype: 'gridcolumn',
                                  flex: 1,
                                  dataIndex: 'CLEANING_MOBILE_TEL',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "保洁手机号"
                              },

                                {
                                    xtype: 'gridcolumn',
                                    flex: 1,
                                    dataIndex: 'CLEANING_IDENTITY_NUMBER',
                                    width: 180,
                                    sortable: false,
                                    menuDisabled: true,
                                    align: 'center',
                                    text: "保洁身份证号"
                                },

                            {
                                xtype: 'datecolumn',
                                flex: 1,
                                format: 'Y-m-d',
                                dataIndex: 'CONTRACT_START_TIME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "合同起始日期"
                            },
                             {
                                 xtype: 'datecolumn',
                                 flex: 1,
                                 format: 'Y-m-d',
                                 dataIndex: 'CONTRACT_END_TIME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "合同结束日期"
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
                                width: 120,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;

                                    str = "<a href='#' onclick='sh(\"" + record.data.ID + "\",\"" + record.data.ZT + "\")'>审核</a>";

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
                                             id: 'cx_xm',
                                             width: 180,
                                             labelWidth: 80,
                                             fieldLabel: '保洁姓名'
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
            //  Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError);

    loadData(1);
})
//************************************主界面*****************************************
