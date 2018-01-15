
var pageSize = 15;
var cx_xm;
var cx_gh;
var cx_sdate;
var cx_edate;
var cx_qy;
var cx_zt;

//************************************数据源*****************************************
var store = createSFW4Store({
    data: [],
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'User_ID' },
       { name: 'LoginName' },
       { name: 'User_XM' },
       { name: 'User_SJ' },
       { name: 'User_Email' },
       { name: 'User_DZ' },
       { name: 'User_Enable' },
       { name: 'User_Sex' },
       { name: 'User_Age' },
       { name: 'User_From' },
       { name: 'User_Education' },
       { name: 'User_JobNo' },
       { name: 'User_IdCard' },
       { name: 'User_ContractNo' },
       { name: 'StartDate' },
       { name: 'EndDate' },
       { name: 'QY_NAME' }
    ],
    //sorters: [{ property: 'b', direction: 'DESC'}],
    onPageChange: function (sto, nPage, sorters) {
        getUser(nPage);
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



//************************************数据源*****************************************

//************************************页面方法***************************************
function getUser(nPage) {
    cx_xm = Ext.getCmp("cx_xm").getValue();
    cx_gh = Ext.getCmp("cx_gh").getValue();
    cx_sdate = Ext.getCmp("cx_sdate").getValue();
    cx_edate = Ext.getCmp("cx_edate").getValue();
    cx_qy = Ext.getCmp("cx_qy").getValue();
    cx_zt = Ext.getCmp("cx_zt").getValue();

    CS('CZCLZ.YHGLClass.GetUserList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_xm, cx_gh, cx_sdate, cx_edate, cx_qy, cx_zt, Ext.getCmp("cx_js").getValue());

}

function showpic() {
    var win = new phWin();
    win.show();
}

function edit(v) {
    FrameStack.pushFrame({
        url: 'AddUser.html?id=' + v,
        onClose: function (ret) {
            getUser(1);
        }
    });
}
//************************************页面方法***************************************

//************************************弹出界面***************************************

//************************************弹出界面***************************************

//************************************主界面*****************************************
Ext.onReady(function () {
    Ext.define('YhView', {
        extend: 'Ext.container.Viewport',

        layout: {
            type: 'fit'
        },

        initComponent: function () {
            var me = this;
            me.items = [
                {
                    xtype: 'gridpanel',
                    id: 'usergrid',
                    title: '',
                    store: store,
                    columnLines: true,
                    selModel: Ext.create('Ext.selection.CheckboxModel', {

                    }),

                    columns: [Ext.create('Ext.grid.RowNumberer'),
                            //{
                            //    xtype: 'gridcolumn',
                            //    dataIndex: 'User_Enable',
                            //    sortable: false,
                            //    menuDisabled: true,

                            //    text: "状态",
                            //    align: 'center',
                            //    renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                            //        if (record.data.User_Enable == false) {
                            //            return "启用";
                            //        }
                            //        else {
                            //            return "停用";
                            //        }
                            //    }
                            //},

                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_XM',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "姓名"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_JobNo',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "工号"
                            },
                            {
                                xtype: 'datecolumn',
                                flex: 1,
                                format: 'Y-m-d',
                                dataIndex: 'StartDate',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "入职时间"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'QY_NAME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "负责区域"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_Sex',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "性别",
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    if (value == 0) {
                                        return "男";
                                    }
                                    else {
                                        return "女";
                                    }
                                }
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_Age',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "年龄"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_From',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "籍贯"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_SJ',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "手机"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_IdCard',
                                sortable: false,
                                menuDisabled: true,
                                width: 150,
                                align: 'center',
                                text: "身份证号码"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'User_DZ',
                                sortable: false,
                                menuDisabled: true,
                                width: 200,
                                align: 'center',
                                text: "地址"
                            },
                            {
                                text: '操作',
                                width: 80,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    str = "<a href='#' onclick='edit(\"" + record.data.User_ID + "\")'>修改</a>";
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
                                            id: 'cx_js',
                                            width: 140,
                                            fieldLabel: '角色',
                                            editable: false,
                                            labelWidth: 40,
                                            store: JsStore,
                                            queryMode: 'local',
                                            displayField: 'JS_NAME',
                                            valueField: 'JS_ID',
                                            value: ''
                                        },
                                        {
                                            xtype: 'textfield',
                                            id: 'cx_xm',
                                            width: 140,
                                            labelWidth: 40,
                                            fieldLabel: '姓名'
                                        },
                                        {
                                            xtype: 'textfield',
                                            id: 'cx_gh',
                                            width: 140,
                                            labelWidth: 40,
                                            fieldLabel: '工号'
                                        },
                                         {
                                             xtype: 'datefield',
                                             id: 'cx_sdate',
                                             format: 'Y-m-d',
                                             width: 160,
                                             labelWidth: 60,
                                             fieldLabel: '入职时间'
                                         },
                                         {
                                             xtype: 'datefield',
                                             id: 'cx_edate',
                                             format: 'Y-m-d',
                                             width: 120,
                                             labelWidth: 20,
                                             fieldLabel: '至'
                                         },
                                         {
                                             xtype: 'combobox',
                                             id: 'cx_qy',
                                             fieldLabel: '负责区域',
                                             editable: false,
                                             store: dqstore,
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             width: 160,
                                             labelWidth: 60
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
                                                     ['在职', 0],
                                                     ['停职', 1],
                                                     ['离职', 2]
                                                 ]
                                             }),
                                             value: 0
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
                                                        getUser(1);
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
                                                            url: 'AddUser.html',
                                                            onClose: function (ret) {
                                                                getUser(1);
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
                                                        var grid = Ext.getCmp("usergrid");
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

                                                                    idlist.push(rd.get("User_ID"));
                                                                }

                                                                CS('CZCLZ.YHGLClass.DelUserByids', function (retVal) {
                                                                    if (retVal) {
                                                                        getUser(cx_js, cx_yhm, cx_xm, cx_dm, store.currentPage);
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

    new YhView();

    CS('CZCLZ.YHGLClass.GetJs', function (retVal) {
        if (retVal) {
            JsStore.add([{ 'JS_ID': '', 'JS_NAME': '全部角色' }]);
            JsStore.loadData(retVal, true);
            Ext.getCmp("cx_js").setValue('');
        }
    }, CS.onError, "");

    CS('CZCLZ.YHGLClass.GetQy', function (retVal) {
        if (retVal) {
            dqstore.add([{ 'VALUE': '', 'TEXT': '所有区域' }]);
            dqstore.loadData(retVal, true);
            Ext.getCmp("cx_qy").setValue('');
        }
    }, CS.onError);

    cx_xm = Ext.getCmp("cx_xm").getValue();
    cx_gh = Ext.getCmp("cx_gh").getValue();
    cx_sdate = Ext.getCmp("cx_sdate").getValue();
    cx_edate = Ext.getCmp("cx_edate").getValue();
    cx_qy = Ext.getCmp("cx_qy").getValue();
    cx_zt = Ext.getCmp("cx_zt").getValue();

    getUser(1);

})
//************************************主界面*****************************************


