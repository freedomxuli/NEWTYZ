
var pageSize = 20;
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
       { name: 'EquipmentId' },
       { name: 'Type' },
       { name: 'EquipmentDate' },
       { name: 'EquipmentVersion' },
       { name: 'DealerAuthoriCode' }

    ],
    //sorters: [{ property: 'b', direction: 'DESC'}],
    onPageChange: function (sto, nPage, sorters) {
        dataBind(nPage);
    }
});


//************************************页面方法***************************************
function dataBind(nPage) {
    var sbbh = Ext.getCmp("sbbh").getValue();
    var gsyh = Ext.getCmp("gsyh").getValue()
    CS('CZCLZ.YHGLClass.GetDeviceList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, sbbh, gsyh);
}


//************************************页面方法***************************************

//************************************弹出界面***************************************

//************************************弹出界面***************************************

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
                                dataIndex: 'EquipmentId',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "设备出厂编号"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'Type',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "设备类别"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'EquipmentDate',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "设备生产日期"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'EquipmentVersion',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "设备版本号"
                            },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'DealerAuthoriCode',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "设备授权码"

                            }
                            //{
                            //    text: '操作',
                            //    width: 80,
                            //    align: 'center',
                            //    sortable: false,
                            //    menuDisabled: true,
                            //    renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                            //        var str;
                            //        str = "<a href='#' onclick='edit(\"" + record.data.User_ID + "\")'>修改</a>";
                            //        return str;
                            //    }
                            //}

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
                                             id: 'sbbh',
                                             width: 180,
                                             labelWidth: 80,
                                             fieldLabel: '设备出厂编号'
                                         },
                                        {
                                            xtype: 'textfield',
                                            id: 'gsyh',
                                            hidden: true,
                                            width: 160,
                                            labelWidth: 60,
                                            fieldLabel: '归属用户'
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
                                                        dataBind(1);
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
                                                     text: '授权',
                                                     handler: function () {
                                                         var idlist = [];
                                                         var grid = Ext.getCmp("maingrid");
                                                         var rds = grid.getSelectionModel().getSelection();
                                                         if (rds.length == 0) {
                                                             Ext.Msg.show({
                                                                 title: '提示',
                                                                 msg: '请选择至少一条授权的数据!',
                                                                 buttons: Ext.MessageBox.OK,
                                                                 icon: Ext.MessageBox.INFO
                                                             });
                                                             return;
                                                         }

                                                         Ext.MessageBox.confirm('提示', '是否授权!', function (obj) {
                                                             if (obj == "yes") {
                                                                 for (var n = 0, len = rds.length; n < len; n++) {
                                                                     var rd = rds[n];

                                                                     idlist.push(rd.get("EquipmentId"));
                                                                 }

                                                                 CS('CZCLZ.YHGLClass.DeviceAuthorization', function (retVal) {
                                                                     if (retVal) {
                                                                         dataBind(1);
                                                                         Ext.Msg.show({
                                                                             title: '提示',
                                                                             msg: '授权成功!',
                                                                             buttons: Ext.MessageBox.OK,
                                                                             icon: Ext.MessageBox.INFO
                                                                         });

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
                                         },

                                        {
                                            xtype: 'buttongroup',
                                            title: '',
                                            hidden: true,
                                            items: [
                                                {
                                                    xtype: 'button',
                                                    iconCls: 'delete',
                                                    text: '删除授权',
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

    new mainView();
    dataBind(1);
})
//************************************主界面*****************************************


