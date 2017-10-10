var AuthorizeId;
var pageSize = 15;


//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
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

    CS('CZCLZ.PayOrderDB.GetJfddList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_no, 0, "投诉订单");

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
                                   text: "投诉详情"
                               },
                                {
                                    xtype: 'gridcolumn',
                                    flex: 1,
                                    dataIndex: 'CLR',
                                    sortable: false,
                                    menuDisabled: true,
                                    align: 'center',
                                    text: "处理人"
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

