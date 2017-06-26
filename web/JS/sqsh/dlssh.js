
var pageSize = 15;
var cx_xm;
var cx_gh;
var cx_sdate;
var cx_edate;
var cx_qy;
var cx_zt;

//************************************数据源*****************************************
var store = createSFW4Store({
    data: [{ 'AGENT_MC': '代理商1', 'AGENT_LEVEL': '一级', 'AGENT_NAME': '小张', 'AGENT_AREA': '新北区', 'AGENT_MOBILE_TEL': '15958456663', 'AGENT_START_TIME': '2017-6-1', 'AGENT_END_TIME': '2017-7-1', 'User_XM': '经纪人甲' }],
    pageSize: pageSize,
    total: 1,
    currentPage: 1,
    fields: [
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
    }, CS.onError, nPage, pageSize, cx_xm, cx_gh, cx_sdate, cx_edate, cx_qy, cx_zt);

}

function tp() {
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
                                    str = "<a href='#' onclick='sh()'>审核</a>";
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
                                            width: 180,
                                            labelWidth: 80,
                                            fieldLabel: '经纪人工号'
                                        },
                                        {
                                            xtype: 'textfield',
                                            width: 140,
                                            labelWidth: 40,
                                            fieldLabel: '授权码'
                                        },
                                           {
                                               xtype: 'textfield',
                                               width: 160,
                                               labelWidth: 60,
                                               fieldLabel: '代理商码'
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
                                                    text: '批量审核',
                                                    handler: function () {

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

    CS('CZCLZ.YHGLClass.GetQy', function (retVal) {
        if (retVal) {
            dqstore.add([{ 'VALUE': '', 'TEXT': '所有区域' }]);
            dqstore.loadData(retVal, true);
            Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError);


})
//************************************主界面*****************************************

Ext.define('phWin', {
    extend: 'Ext.window.Window',
    height: 275,
    width: 653,
    modal: true,
    layout: 'border',
    initComponent: function () {
        var me = this;
        var lx = me.lx;
        me.items = [{
            xtype: 'UploaderPanel',
            id: 'uploadproductpic',
            region: 'center',
            autoScroll: true,
            dockedItems: [{
                xtype: 'toolbar',
                dock: 'top',
                items: [{
                    xtype: 'filefield',
                    fieldLabel: '上传图片',
                    buttonText: '浏览'
                }

                ]
            }],
            buttonAlign: 'center',
            buttons: [
                 {
                     xtype: 'button',
                     text: '上传',
                     iconCls: 'upload',
                     handler: function () {
                         Ext.getCmp('uploadproductpic').upload('CZCLZ.YHGLClass.UploadPicForProduct', function (retVal) {
                             var isDefault = false;
                             if (retVal.isdefault == 1)
                                 isDefault = true;
                             Ext.getCmp('uploadproductpic').add(new SelectImg({
                                 isSelected: isDefault,
                                 src: retVal.fileurl,
                                 fileid: retVal.fileid
                             }));
                             picItem.push(retVal.fileid);
                         }, CS.onError, lx, 'user');
                     }
                 },
            {
                text: '删除',
                handler: function () {
                    Ext.MessageBox.confirm('确认', '是否删除该图片？', function (btn) {
                        if (btn == 'yes') {
                            var selPics = Ext.getCmp('uploadproductpic').query('image[isSelected=true]');
                            if (selPics.length > 0) {
                                CS('CZCLZ.YHGLClass.DelProductImageByPicID', function (retVal) {
                                    if (retVal) {
                                        Ext.getCmp('uploadproductpic').remove(selPics[0]);
                                    }
                                }, CS.onError, selPics[0].fileid);
                            }
                        }
                    });
                }
            }]
        }];
        me.callParent(arguments);
    }
});

Ext.define('SelectImg', {
    extend: 'Ext.Img',

    height: 80,
    width: 120,
    margin: 5,
    padding: 2,
    constructor: function (config) {
        var me = this;
        config = config || {};
        config.cls = config.isSelected ? "clsSelected" : "clsUnselected";
        me.callParent([config]);
        me.on('render', function () {
            Ext.fly(me.el).on('click', function () {
                var oldSelectImg = Ext.getCmp('uploadproductpic').query('image[isSelected=true]');
                if (oldSelectImg.length < 0 || oldSelectImg[0] != me) {
                    me.removeCls('clsUnselected');
                    me.addCls('clsSelected');
                    me.isSelected = true;
                    if (oldSelectImg.length > 0) {
                        oldSelectImg[0].removeCls('clsSelected');
                        oldSelectImg[0].addCls('clsUnselected');
                        oldSelectImg[0].isSelected = false;
                    }
                }
            });
        });

    },

    initComponent: function () {
        var me = this;
        me.callParent(arguments);
    }
});
