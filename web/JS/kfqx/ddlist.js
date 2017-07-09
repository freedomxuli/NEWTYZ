
var pageSize = 15;


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

    CS('CZCLZ.CwDB.GetFdList', function (retVal) {
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


//************************************页面方法***************************************

function tp() {
    var win = new phWin();
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
                                dataIndex: 'LANDLORD_MC',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "房客姓名"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 dataIndex: 'LANDLORD_NAME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "房客手机号"
                             },
                              {
                                  xtype: 'gridcolumn',
                                  dataIndex: 'User_XM',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "入住时间"
                              },

                                {
                                    xtype: 'gridcolumn',
                                    dataIndex: 'LANDLORD_MOBILE_TEL',
                                    sortable: false,
                                    menuDisabled: true,
                                    align: 'center',
                                    text: "退房时间"
                                },

                            {
                                xtype: 'datecolumn',
                                format: 'Y-m-d',
                                dataIndex: 'LANDLORD_START_TIME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "宾馆名称"
                            },
                             {
                                 xtype: 'datecolumn',
                                 format: 'Y-m-d',
                                 dataIndex: 'LANDLORD_END_TIME',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "宾馆地址"
                             },
                            {
                                xtype: 'gridcolumn',
                                dataIndex: 'QY_NAME',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "负责人姓名"
                            },
                            {
                                text: '操作',
                                width: 80,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    str = "<a href='#' onclick='sh(\"" + record.data.ID + "\")'>审核</a>";
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
                                            width: 160,
                                            labelWidth: 60,
                                            fieldLabel: '订单号'
                                        },
                                          {
                                              xtype: 'datefield',
                                              format: 'Y-m-d',
                                              width: 160,
                                              labelWidth: 60,
                                              fieldLabel: '入住时间'
                                          },
                                           {
                                               xtype: 'textfield',
                                               width: 160,
                                               labelWidth: 60,
                                               fieldLabel: '宾馆名称'
                                           },
                                          {
                                              xtype: 'textfield',
                                              width: 160,
                                              labelWidth: 60,
                                              fieldLabel: '前台电话'
                                          },
                                            {
                                                xtype: 'textfield',
                                                width: 160,
                                                labelWidth: 60,
                                                fieldLabel: '房客电话'
                                            },
                                              {
                                                  xtype: 'textfield',
                                                  width: 160,
                                                  labelWidth: 60,
                                                  fieldLabel: '房客姓名'
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
                                                        //   loadData(1);
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


    // loadData(1);
})
//************************************主界面*****************************************

