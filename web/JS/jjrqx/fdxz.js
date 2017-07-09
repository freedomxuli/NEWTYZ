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

Ext.define('deviceWin', {
    extend: 'Ext.window.Window',

    height: 250,
    width: 400,
    layout: {
        type: 'fit'
    },
    id: 'deviceWin',
    closeAction: 'destroy',
    modal: true,
    title: '设备信息',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                id: 'deviceform',
                frame: true,
                bodyPadding: 10,

                title: '',
                store: deviceStore,
                items: [
                    {
                        xtype: 'textfield',
                        fieldLabel: 'ID',
                        id: 'ID',
                        name: 'ID',
                        labelWidth: 70,
                        hidden: true,
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        id: 'DEVICE_NAME',
                        name: 'DEVICE_NAME',
                        fieldLabel: '设备类型',
                        labelWidth: 70,
                        allowBlank: false,
                        anchor: '100%'
                    },
                    {
                        xtype: 'numberfield',
                        id: 'DEVICE_NUMBER',
                        name: 'DEVICE_NUMBER',
                        fieldLabel: '设备数量',
                        labelWidth: 70,
                        allowBlank: false,
                        anchor: '100%'
                    },
                    {
                        xtype: 'numberfield',
                        id: 'DEVICE_MONEY',
                        name: 'DEVICE_MONEY',
                        fieldLabel: '金额',
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
                            var form = Ext.getCmp('deviceform');
                            if (form.form.isValid()) {
                                //取得表单中的内容
                                var values = form.form.getValues(false);

                                CS('CZCLZ.JjrDB.SaveDlsDevice', function (retVal) {
                                    if (retVal) {
                                        var grid = Ext.getCmp("devicegrid");
                                        var store = grid.getStore();
                                        store.insert(0, retVal[0]);
                                    }

                                    Ext.getCmp('deviceWin').close()
                                }, CS.onError, values);
                            }
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
                                              allowBlank: false,
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
                                             allowBlank: false,
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
                                             allowBlank: false,
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
                                             xtype: 'combobox',
                                             id: 'QY_ID',
                                             name: 'QY_ID',
                                             editable: false,
                                             store: dqstore,
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             margin: '10 10 10 10',
                                             fieldLabel: '所属区域',
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
                                                            '->',
                                                            {
                                                                xtype: 'button',
                                                                text: '添加设备',
                                                                handler: function () {

                                                                    var win = new deviceWin();

                                                                    win.show(null, function () {

                                                                        // wjdataBind(1);

                                                                    });

                                                                }
                                                            }
                                                        ]
                                                    }
                                                ],
                                                buttonAlign: 'center',
                                                buttons: [
                                                    {
                                                        text: '保存',
                                                        handler: function () {
                                                            var form = Ext.getCmp('addform');
                                                            if (form.form.isValid()) {
                                                                var values = form.getValues(false);
                                                                CS('CZCLZ.JjrDB.SaveFdXX', function (retVal) {
                                                                    if (retVal) {
                                                                        FrameStack.popFrame();
                                                                    }
                                                                }, CS.onError, values, picItem);
                                                            }
                                                        }

                                                    },
                                                    {
                                                        text: '返回',
                                                        handler: function () {
                                                            FrameStack.popFrame();
                                                        }
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

    CS('CZCLZ.YHGLClass.GetQy', function (retVal) {
        if (retVal) {
            dqstore.add([{ 'VALUE': '', 'TEXT': '所有区域' }]);
            dqstore.loadData(retVal, true);
            Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError);

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
                         }, CS.onError, lx, '房东');
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