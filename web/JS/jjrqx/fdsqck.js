
var pageSize = 15;


//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'ID' },
       { name: 'Name' },
       { name: 'Mobile' },
       { name: 'Province' },
       { name: 'City' },
       { name: 'IdCard' },
       { name: 'ApplicationLevel' },
        { name: 'RoomNum' },
         { name: 'RoomSize' },
          { name: 'LandlordType' },
           { name: 'ApplicationLevel' },
       { name: 'Experience' }
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

    var cx_sj = Ext.getCmp("cx_sj").getValue();
    var cx_xm = Ext.getCmp("cx_xm").getValue();
    var cx_zt = Ext.getCmp("cx_zt").getValue();

    CS('CZCLZ.JjrDB.GetSQList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_sj, cx_xm, cx_zt, 2);

}


function sh(v) {
    FrameStack.pushFrame({
        url: 'fdxz.html?pid=' + v,
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
                    columns: [Ext.create('Ext.grid.RowNumberer'),
                          {
                              xtype: 'gridcolumn',
                              dataIndex: 'ID',
                              hidden: true,
                              sortable: false,
                              menuDisabled: true,
                              align: 'center'
                          },
                            {
                                xtype: 'gridcolumn',
                                flex: 1,
                                dataIndex: 'Name',
                                sortable: false,
                                menuDisabled: true,
                                align: 'center',
                                text: "姓名"
                            },
                             {
                                 xtype: 'gridcolumn',
                                 flex: 1,
                                 dataIndex: 'Mobile',
                                 sortable: false,
                                 menuDisabled: true,
                                 align: 'center',
                                 text: "手机号"
                             },
                              {
                                  xtype: 'gridcolumn',
                                  flex: 1,
                                  dataIndex: 'IdCard',
                                  sortable: false,
                                  menuDisabled: true,
                                  align: 'center',
                                  text: "身份证"
                              },
                               {
                                   xtype: 'gridcolumn',
                                   flex: 1,
                                   dataIndex: 'Province',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "省"
                               },
                               {
                                   xtype: 'gridcolumn',
                                   flex: 1,
                                   dataIndex: 'City',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "市"
                               },
                               {
                                   xtype: 'gridcolumn',
                                   flex: 1,
                                   dataIndex: 'RoomNum',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "房屋数量"
                               },
                               {
                                   xtype: 'gridcolumn',
                                   flex: 1,
                                   dataIndex: 'RoomSize',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "房屋大小"
                               },
                               {
                                   xtype: 'gridcolumn',
                                   flex: 1,
                                   dataIndex: 'LandlordType',
                                   sortable: false,
                                   menuDisabled: true,
                                   align: 'center',
                                   text: "房东类型"
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
                                            id: 'cx_sj',
                                            width: 180,
                                            labelWidth: 60,
                                            fieldLabel: '手机号'
                                        },
                                         {
                                             xtype: 'textfield',
                                             id: 'cx_xm',
                                             width: 160,
                                             labelWidth: 40,
                                             fieldLabel: '姓名'
                                         },
                                         {
                                             xtype: 'combobox',
                                             id: 'cx_zt',
                                             fieldLabel: '状态',
                                             width: 160,
                                             labelWidth: 40,
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             store: new Ext.data.ArrayStore({
                                                 fields: ['TEXT', 'VALUE'],
                                                 data: [
                                                     ['待处理', '0'],
                                                     ['已处理', '1'],
                                                     ['不予处理', '2']
                                                 ]
                                             }),
                                             value: '0'
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

