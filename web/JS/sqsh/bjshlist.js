var isframe = true;
if (window.queryString.isframe) {
    isframe = false;
}
var pageSize = 15;


//************************************数据源*****************************************
var store = createSFW4Store({
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
       { name: 'ID' },
         { name: 'FLOWID' },
         { name: 'STEPID' },
       { name: 'CLEANING_NAME' },
       { name: 'CLEANING_AGE' },
       { name: 'CLEANING_SEX' },
       { name: 'CLEANING_MOBILE_TEL' },
       { name: 'CLEANING_IDENTITY_NUMBER' },
       { name: 'CONTRACT_START_TIME' },
       { name: 'CONTRACT_END_TIME' },
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

    var cx_xm = Ext.getCmp("cx_xm").getValue();


    CS('CZCLZ.AdminDB.GetBjList', function (retVal) {
        store.setData({
            data: retVal.dt,
            pageSize: pageSize,
            total: retVal.ac,
            currentPage: retVal.cp
            //sorters: { property: 'a', direction: 'DESC' }
        });
    }, CS.onError, nPage, pageSize, cx_xm);

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

function sh(v, flowId, stepId) {
    FrameStack.pushFrame({
        url: 'bjsh.html?id=' + v + '&flowId=' + flowId + '&stepId=' + stepId,
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
                                text: '操作',
                                width: 80,
                                align: 'center',
                                sortable: false,
                                menuDisabled: true,
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    var str;
                                    str = "<a href='#' onclick='sh(\"" + record.data.ID + "\",\"" + record.data.FLOWID + "\",\"" + record.data.STEPID + "\")'>审核</a>";
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
            // Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError);

    loadData(1);
})
//************************************主界面*****************************************
