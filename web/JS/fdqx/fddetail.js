
var pageSize = 15;
var cx_xm;
var cx_gh;
var cx_sdate;
var cx_edate;
var cx_qy;
var cx_zt;

//************************************数据源*****************************************
var store = createSFW4Store({
    data: [{ 'LANDLORD_MC': '房东2', 'AGENT_LEVEL': '一级', 'AGENT_NAME': '小张', 'AGENT_AREA': '新北区', 'AGENT_MOBILE_TEL': '15958456663', 'AGENT_START_TIME': '2017-6-1', 'AGENT_END_TIME': '2017-7-1', 'User_XM': '经纪人甲' }],
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


var id = queryString.id;
var picItem = [];

var dqstore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT'],
    data: [
    ]
});

var deviceStore = Ext.create('Ext.data.Store', {
    fields: [
       { name: 'ID', type: 'string' },
       { name: 'DEVICE_NAME', type: 'string' },
       { name: 'DEVICE_NUMBER', type: 'string' },
       { name: 'DEVICE_MONEY', type: 'string' }
    ]

});




function DataBind() {
    CS('CZCLZ.JjrDB.GetFdById', function (retVal) {
        if (retVal) {
            var addform = Ext.getCmp("addform");
            addform.form.setValues(retVal[0]);
        }
    }, CS.onError, id);
}

function tp(v) {
    if (id != "" && id != null) {
        CS('CZCLZ.YHGLClass.GetProductImages', function (retVal) {
            for (var i = 0; i < retVal.length; i++) {
                Ext.getCmp('uploadproductpic').add(new SelectImg({
                    isSelected: false,
                    src: retVal[i].FJ_URL,
                    fileid: retVal[i].FJ_ID
                }));
            }
        }, CS.onError, picItem, id, v, "房东");
    }
    else {
        CS('CZCLZ.YHGLClass.GetProductImages', function (retVal) {
            for (var i = 0; i < retVal.length; i++) {
                Ext.getCmp('uploadproductpic').add(new SelectImg({
                    isSelected: false,
                    src: retVal[i].FJ_URL,
                    fileid: retVal[i].FJ_ID
                }));
            }
        }, CS.onError, picItem, id, v, "房东");
    }
    var win = new phWin({ lx: v });
    win.show();
}

Ext.define('userWin', {
    extend: 'Ext.window.Window',

    height: 250,
    width: 400,
    layout: {
        type: 'fit'
    },
    id: 'userWin',
    closeAction: 'destroy',
    modal: true,
    title: '创建账户信息',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                id: 'userform',
                frame: true,
                bodyPadding: 10,

                title: '',
                store: deviceStore,
                items: [
                    {
                        xtype: 'textfield',
                        id: 'yhm',
                        fieldLabel: '用户名',
                        labelWidth: 70,
                        allowBlank: false,
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        id: 'mm',
                        fieldLabel: '密码',
                        labelWidth: 70,
                        allowBlank: false,
                        anchor: '100%'
                    }
                ],
                buttonAlign: 'center',
                buttons: [
                    {
                        text: '确定',
                        handler: function () {
                            Ext.MessageBox.confirm('确认', '确认后将发送至财务组，是否继续？', function (btn) {
                                if (btn == 'yes') {
                                    var form = Ext.getCmp('userform');
                                    if (form.form.isValid()) {
                                        //取得表单中的内容
                                        CS('CZCLZ.AdminDB.AgreeFd', function (retVal) {
                                            if (retVal) {
                                                FrameStack.popFrame();
                                            }
                                        }, CS.onError, id, Ext.getCmp("yhm").getValue(), Ext.getCmp("mm").getValue());
                                    }
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

Ext.onReady(function () {
    Ext.define('add', {
        extend: 'Ext.container.Viewport',
        layout: {
            type: 'fit'
        },

        initComponent: function () {
            var me = this;

            Ext.applyIf(me, {
                items: [
                    {
                        xtype: 'panel',
                        layout: {
                            type: 'anchor'
                        },
                        autoScroll: true,
                        buttonAlign: 'center',
                        buttons: [
                            
                            {
                                text: '返回',
                                handler: function () {
                                    FrameStack.popFrame();
                                }
                            }
                        ],
                        items: [
                            {
                                xtype: 'form',
                                id: 'addform',
                                layout: {
                                    type: 'column'
                                },
                                border: true,
                                // margin: 10,
                                title: '房东基本信息',
                                items: [
                                        {
                                            xtype: 'textfield',
                                            name: 'ID',
                                            margin: '10 10 10 10',
                                            fieldLabel: '主键ID',
                                            hidden: true,
                                            columnWidth: 0.5,
                                            labelWidth: 150
                                        },
                                         {
                                             xtype: 'textfield',
                                             name: 'LANDLORD_MC',
                                             margin: '10 10 10 10',
                                             fieldLabel: '房东名称',
                                             allowBlank: false,
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'textfield',
                                              name: 'LANDLORD_NAME',
                                              margin: '10 10 10 10',
                                              fieldLabel: '房东姓名',
                                              columnWidth: 0.5,
                                              labelWidth: 150

                                          },

                                          {
                                              xtype: 'textfield',
                                              name: 'LANDLORD_MOBILE_TEL',
                                              margin: '10 10 10 10',
                                              fieldLabel: '手机',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                           {
                                               xtype: 'textfield',
                                               name: 'LANDLORD_EMAIL',
                                               margin: '10 10 10 10',
                                               fieldLabel: '邮箱',
                                               columnWidth: 0.5,
                                               labelWidth: 150
                                           },
                                            {
                                                xtype: 'combobox',
                                                name: 'ROOM_TYPE',
                                                margin: '10 10 10 10',
                                                fieldLabel: '房源类型',
                                                columnWidth: 0.5,
                                                labelWidth: 150,
                                                queryMode: 'local',
                                                displayField: 'TEXT',
                                                valueField: 'VALUE',
                                                store: new Ext.data.ArrayStore({
                                                    fields: ['TEXT', 'VALUE'],
                                                    data: [
                                                        ['套房', '套房'],
                                                        ['单身公寓', '单身公寓']
                                                    ]
                                                })
                                            },
                                             {
                                                 xtype: 'combobox',
                                                 name: 'ROOM_ATTRIBUTE',
                                                 margin: '10 10 10 10',
                                                 fieldLabel: '房源属性',
                                                 columnWidth: 0.5,
                                                 labelWidth: 150,
                                                 queryMode: 'local',
                                                 displayField: 'TEXT',
                                                 valueField: 'VALUE',
                                                 store: new Ext.data.ArrayStore({
                                                     fields: ['TEXT', 'VALUE'],
                                                     data: [
                                                         ['自有', '自有'],
                                                         ['租赁', '租赁']
                                                     ]
                                                 })
                                             },
                                           {
                                               xtype: 'combobox',
                                               name: 'COMMISSION_TYPE',
                                               margin: '10 10 10 10',
                                               fieldLabel: '佣金方式',
                                               columnWidth: 0.5,
                                               labelWidth: 150,
                                               queryMode: 'local',
                                               displayField: 'TEXT',
                                               valueField: 'VALUE',
                                               store: new Ext.data.ArrayStore({
                                                   fields: ['TEXT', 'VALUE'],
                                                   data: [
                                                       ['按单收取', '按单收取'],
                                                       ['按次收取', '按次收取']
                                                   ]
                                               })
                                           },
                                           {
                                               xtype: 'textfield',
                                               name: 'SETTLEMENT_CYCLE',
                                               margin: '10 10 10 10',
                                               fieldLabel: '结算周期',
                                               columnWidth: 0.5,
                                               labelWidth: 150
                                           },
                                            {
                                                xtype: 'textfield',
                                                name: 'COMMISSION_RATIO',
                                                margin: '10 10 10 10',
                                                fieldLabel: '佣金比例',
                                                columnWidth: 0.5,
                                                labelWidth: 150
                                            },
                                              {
                                                  xtype: 'combobox',
                                                  name: 'LEASE_TYPE',
                                                  margin: '10 10 10 10',
                                                  fieldLabel: '租赁方式',
                                                  columnWidth: 0.5,
                                                  labelWidth: 150,
                                                  queryMode: 'local',
                                                  displayField: 'TEXT',
                                                  valueField: 'VALUE',
                                                  store: new Ext.data.ArrayStore({
                                                      fields: ['TEXT', 'VALUE'],
                                                      data: [
                                                          ['整租', '整租'],
                                                          ['分租', '分租']
                                                      ]
                                                  })
                                              },

                                         {
                                             xtype: 'textfield',
                                             name: 'LANDLORD_IDENTITY_NUMBER',
                                             margin: '10 10 10 10',
                                             fieldLabel: '身份证号',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                         {
                                             xtype: 'displayfield',
                                             value: '<a href="#" onclick="tp(\'2\')">上传</a>',
                                             margin: '10 10 10 10',
                                             fieldLabel: '身份证图片',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                         {
                                             xtype: 'displayfield',
                                             value: '<a href="#" onclick="tp(\'3\')">上传</a>',
                                             margin: '10 10 10 10',
                                             fieldLabel: '用工合同照片',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'textfield',
                                              name: 'LANDLORD_CONTRACT_NUMBER',
                                              margin: '10 10 10 10',
                                              fieldLabel: '合同编号',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                         {
                                             xtype: 'datefield',
                                             name: 'LANDLORD_START_TIME',
                                             format: 'Y-m-d',
                                             editable: false,
                                             margin: '10 10 10 10',
                                             fieldLabel: '合同生效时间',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                         {
                                             xtype: 'datefield',
                                             format: 'Y-m-d',
                                             name: 'LANDLORD_END_TIME',
                                             editable: false,
                                             margin: '10 10 10 10',
                                             fieldLabel: '合同失效时间',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'textfield',
                                              name: 'LANDLORD_CONTRACT_STATE',
                                              margin: '10 10 10 10',
                                              fieldLabel: '合同类型',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                           {
                                               xtype: 'textfield',
                                               name: 'LANDLORD_DEPOSIT',
                                               margin: '10 10 10 10',
                                               fieldLabel: '押金总额',
                                               columnWidth: 0.5,
                                               labelWidth: 150
                                           },
                                            {
                                                xtype: 'textfield',
                                                name: 'DELIVER_ADDRESS',
                                                margin: '10 10 10 10',
                                                fieldLabel: '发货地址',
                                                columnWidth: 0.5,
                                                labelWidth: 150
                                            },
                                            {
                                                xtype: 'textfield',
                                                name: 'CONTACT_TEL',
                                                margin: '10 10 10 10',
                                                fieldLabel: '联系人',
                                                columnWidth: 0.5,
                                                labelWidth: 150
                                            },
                                        {
                                            xtype: 'displayfield',
                                            name: 'QY_NAME',

                                            margin: '10 10 10 10',
                                            fieldLabel: '代理商所属区域',
                                            columnWidth: 0.5,
                                            labelWidth: 150
                                        },
                                          {
                                              xtype: 'displayfield',
                                              name: 'User_XM',
                                              margin: '10 10 10 10',
                                              fieldLabel: '经纪人',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                          {
                                              xtype: 'displayfield',
                                              name: 'LANDLORD_APPLY_TIME',
                                              margin: '10 10 10 10',
                                              fieldLabel: '申请时间',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          }


                                ]
                            },
                            {
                                xtype: 'panel',
                                margin: '10 0 0 0',
                                // border: true,
                                items: [
                                    {
                                        xtype: 'panel',
                                        items: [
                                            {
                                                xtype: 'gridpanel',
                                                margin: '0 0 0 0',
                                                id: 'devicegrid',
                                                //  store: store2,
                                                height: 300,
                                                columnLines: true,
                                                border: true,
                                                autoscroll: true,
                                                columns: [
                                                     {
                                                         xtype: 'gridcolumn',
                                                         dataIndex: 'DEVICE_NAME',
                                                         align: 'center',
                                                         text: '设备类型',
                                                         flex: 1,
                                                         sortable: false,
                                                         menuDisabled: true
                                                     },

                                                    {
                                                        xtype: 'gridcolumn',
                                                        dataIndex: 'DEVICE_NUMBER',
                                                        align: 'center',
                                                        text: '设备数量',
                                                        flex: 1,
                                                        sortable: false,
                                                        menuDisabled: true
                                                    },
                                                    {
                                                        xtype: 'gridcolumn',
                                                        dataIndex: 'DEVICE_MONEY',
                                                        align: 'center',
                                                        text: '押金金额',
                                                        flex: 1,
                                                        sortable: false,
                                                        menuDisabled: true
                                                    }

                                                ],
                                                dockedItems: [
                                                    {
                                                        xtype: 'toolbar',
                                                        dock: 'top',
                                                        items: [
                                                            {
                                                                xtype: 'displayfield',
                                                                fieldLabel: '',
                                                                value: '<div style="font-size:14px; color:#007ED2;">代理设备列表</div>'
                                                            },
                                                            '->'

                                                        ]
                                                    }
                                                ]
                                            }
                                        ]
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
    new add();



    if (id != null && id != "")
        DataBind();
});

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
            autoScroll: true

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
//************************************数据源*****************************************
