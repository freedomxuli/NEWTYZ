
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

    CS('CZCLZ.AdminDB.GetMyDevice', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, sbbh);
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


