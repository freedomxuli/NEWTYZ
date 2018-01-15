var id = queryString.id;
var flowId = queryString.flowId;
var stepId = queryString.stepId;
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
    CS('CZCLZ.AdminDB.GetDLsById', function (retVal) {
        if (retVal) {
            var addform = Ext.getCmp("addform");
            addform.form.setValues(retVal.dt[0]);

            var html1 = "";
            var html2 = "";
            for (var i in retVal.dtFJ) {
                if (retVal.dtFJ[i]["fj_lx"] == 2)
                    html1 += '<div class="file-item uploadimages" style="margin-left:5px;margin-bottom:5px" imageurl="~/' + retVal.dtFJ[i]["fj_url"] + '"><img src="approot/r/' + retVal.dtFJ[i]["fj_url"] + '" width="100px" height="100px"/></div>';
                else if (retVal.dtFJ[i]["fj_lx"] == 3)
                    html2 += '<div class="file-item uploadimages" style="margin-left:5px;margin-bottom:5px" imageurl="~/' + retVal.dtFJ[i]["fj_url"] + '"><img src="approot/r/' + retVal.dtFJ[i]["fj_url"] + '" width="100px" height="100px"/></div>';
            }
            $("#fileList").append(html1);
            $("#fileList2").append(html2);
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
                                        CS('CZCLZ.AdminDB.AgreeDls', function (retVal) {
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
                                text: '同意',
                                handler: function () {
                                    Ext.MessageBox.confirm('确认', '确认通过？', function (btn) {
                                        if (btn == 'yes') {

                                            CS('CZCLZ.AdminDB.AgreeDls', function (retVal) {
                                                if (retVal) {
                                                    FrameStack.popFrame();
                                                }
                                            }, CS.onError, id, flowId, stepId);
                                        }
                                    });
                                }

                            },
                             {
                                 text: '拒绝',
                                 handler: function () {
                                     Ext.MessageBox.confirm('确认', '是否拒绝？', function (btn) {
                                         if (btn == 'yes') {
                                             CS('CZCLZ.AdminDB.NoAgreeDls', function (retVal) {
                                                 if (retVal) {
                                                     FrameStack.popFrame();
                                                 }
                                             }, CS.onError, id);
                                         }
                                     });
                                 }

                             },
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
                                             id: 'tp1',
                                             value: ' <div id="fileList"><div id="filePicker" style="float:left;margin-right:10px;margin-bottom:5px;width:50px;height:50px;">点击选择图片</div></div>',
                                             margin: '10 10 10 10',
                                             fieldLabel: '代理商身份证图片',
                                             columnWidth: 1,
                                             labelWidth: 150
                                         },
                                           {
                                               xtype: 'displayfield',
                                               id: 'tp2',
                                               value: ' <div id="fileList2"><div id="filePicker2" style="float:left;margin-right:10px;margin-bottom:5px;width:50px;height:50px;">点击选择图片</div></div>',
                                               margin: '10 10 10 10',
                                               fieldLabel: '代理商合同',
                                               columnWidth: 1,
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
                                              name: 'AGENT_APPLY_TIME',
                                              margin: '10 10 10 10',
                                              fieldLabel: '申请时间',
                                              columnWidth: 0.5,
                                              labelWidth: 150
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
    initwebupload("filePicker", "fileList", 5);
    initwebupload("filePicker2", "fileList2", 5);
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