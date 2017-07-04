var id = queryString.id;
var picItem = [];

var dqstore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT'],
    data: [
    ]
});

function DataBind() {
    CS('JSSF.SFXM.SetNum', function (retVal) {
        if (retVal) {

        }
    }, CS.onError, hyzgbh);
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
        }, CS.onError, picItem, id, v, "代理商");
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
        }, CS.onError, picItem, id, v, "代理商");
    }
    var win = new phWin({ lx: v });
    win.show();
}

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
                                title: '代理商信息',
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
                                             name: 'AGENT_MC',
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商名称',
                                             allowBlank: false,
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'textfield',
                                              name: 'AGENT_NAME',
                                              margin: '10 10 10 10',
                                              fieldLabel: '代理商姓名',
                                              columnWidth: 0.5,
                                              labelWidth: 150

                                          },
                                          {
                                              xtype: 'combobox',
                                              name: 'AGENT_LEVEL',
                                              margin: '10 10 10 10',
                                              fieldLabel: '代理商级别',
                                              columnWidth: 0.5,
                                              labelWidth: 150,
                                              queryMode: 'local',
                                              displayField: 'TEXT',
                                              valueField: 'VALUE',
                                              store: new Ext.data.ArrayStore({
                                                  fields: ['TEXT', 'VALUE'],
                                                  data: [
                                                      ['一级', '一级'],
                                                      ['二级', '二级'],
                                                      ['三级', '三级']
                                                  ]
                                              })
                                          },
                                          {
                                              xtype: 'textfield',
                                              name: 'AGENT_MOBILE_TEL',
                                              margin: '10 10 10 10',
                                              fieldLabel: '代理商电话',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                           {
                                               xtype: 'textfield',
                                               name: 'AGENT_EMAIL',
                                               margin: '10 10 10 10',
                                               fieldLabel: '代理商邮箱',
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
                                               fieldLabel: '联系人电话',
                                               columnWidth: 0.5,
                                               labelWidth: 150
                                           },
                                         {
                                             xtype: 'textfield',
                                             name: 'AGENT_IDENTITY_NUMBER',
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商身份证号',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                         {
                                             xtype: 'displayfield',
                                             value: '<a href="#" onclick="tp(\'2\')">上传</a>',
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商身份证图片',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                         {
                                             xtype: 'displayfield',
                                             value: '<a href="#" onclick="tp(\'3\')">上传</a>',
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商合同',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'textfield',
                                              name: 'AGENT_CONTRACT_NUMBER',
                                              margin: '10 10 10 10',
                                              fieldLabel: '代理商合同编号',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                         {
                                             xtype: 'combobox',
                                             name: 'AGENT_TYPE',
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理方式',
                                             columnWidth: 0.5,
                                             labelWidth: 150,
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             store: new Ext.data.ArrayStore({
                                                 fields: ['TEXT', 'VALUE'],
                                                 data: [
                                                     ['按件提成', '按件提成'],
                                                     ['按佣金提成', '按佣金提成']
                                                 ]
                                             })
                                         },
                                         {
                                             xtype: 'textfield',
                                             name: 'RATIO_TYPE',
                                             margin: '10 10 10 10',
                                             fieldLabel: '提成比例',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },


                                         {
                                             xtype: 'datefield',
                                             name: 'AGENT_START_TIME',
                                             format: 'Y-m-d',
                                             editable: false,
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商合同生效时间',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                         {
                                             xtype: 'datefield',
                                             format: 'Y-m-d',
                                             name: 'AGENT_END_TIME',
                                             editable: false,
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商合同失效时间',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'textfield',
                                              name: 'AGENT_CONTRACT_STATE',
                                              margin: '10 10 10 10',
                                              fieldLabel: '代理商合同类型',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                           {
                                               xtype: 'textfield',
                                               name: 'AGENT_DEPOSIT',
                                               margin: '10 10 10 10',
                                               fieldLabel: '押金总额',
                                               columnWidth: 0.5,
                                               labelWidth: 150
                                           },
                                         {
                                             xtype: 'combobox',
                                             id: 'QY_ID',
                                             editable: false,
                                             store: dqstore,
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商所属区域',
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
                                                id: 'wjgl2',
                                                //  store: store2,
                                                height: 300,
                                                columnLines: true,
                                                border: true,
                                                autoscroll: true,
                                                columns: [Ext.create('Ext.grid.RowNumberer'),
                                                     {
                                                         xtype: 'gridcolumn',
                                                         dataIndex: 'WJYJ_LB',
                                                         align: 'center',
                                                         text: '设备类型',
                                                         flex: 1,
                                                         sortable: false,
                                                         menuDisabled: true
                                                     },
                                                    {
                                                        xtype: 'gridcolumn',
                                                        dataIndex: 'WJYJ_TITLE',
                                                        align: 'center',
                                                        text: '设备SN号',
                                                        flex: 1,
                                                        sortable: false,
                                                        menuDisabled: true
                                                    },
                                                    {
                                                        xtype: 'gridcolumn',
                                                        dataIndex: 'WJYJ_FILENO',
                                                        align: 'center',
                                                        text: '设备数量',
                                                        flex: 1,
                                                        sortable: false,
                                                        menuDisabled: true
                                                    },
                                                    {
                                                        xtype: 'gridcolumn',
                                                        dataIndex: 'WJYJ_FWDW',
                                                        align: 'center',
                                                        text: '押金金额',
                                                        flex: 1,
                                                        sortable: false,
                                                        menuDisabled: true
                                                    },
                                                    {
                                                        xtype: 'gridcolumn',
                                                        dataIndex: 'WJYJ_FWRQnew',
                                                        align: 'center',
                                                        text: '设备生效时间',
                                                        flex: 1,
                                                        sortable: false,
                                                        menuDisabled: true
                                                    },
                                                     {
                                                         xtype: 'gridcolumn',
                                                         dataIndex: 'WJYJ_FWRQnew',
                                                         align: 'center',
                                                         text: '设备失效时间',
                                                         flex: 1,
                                                         sortable: false,
                                                         menuDisabled: true
                                                     },
                                                      {
                                                          xtype: 'gridcolumn',
                                                          dataIndex: 'WJYJ_FWRQnew',
                                                          align: 'center',
                                                          text: '设备授权码',
                                                          flex: 1,
                                                          sortable: false,
                                                          menuDisabled: true
                                                      },
                                                      {
                                                          xtype: 'gridcolumn',
                                                          dataIndex: 'WJYJ_FWRQnew',
                                                          align: 'center',
                                                          text: '设备状态',
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

                                                                    var wjgl_add = new wjgl();

                                                                    wjgl_add.show(null, function () {

                                                                        wjdataBind(1);

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
                                                                CS('CZCLZ.JjrDB.SaveDls', function (retVal) {
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