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
    CS('CZCLZ.JjrDB.GetBjById', function (retVal) {
        if (retVal) {
            var addform = Ext.getCmp("addform");
            addform.form.setValues(retVal.dt[0]);

            var html1 = "";
            var html2 = "";
            var html3 = "";
            for (var i in retVal.dtFJ) {
                if (retVal.dtFJ[i]["fj_lx"] == 2)
                    html1 += '<div class="file-item uploadimages" style="margin-left:5px;margin-bottom:5px" imageurl="~/' + retVal.dtFJ[i]["fj_url"] + '"><img src="approot/r/' + retVal.dtFJ[i]["fj_url"] + '" width="100px" height="100px"/></div>';
                else if (retVal.dtFJ[i]["fj_lx"] == 3)
                    html2 += '<div class="file-item uploadimages" style="margin-left:5px;margin-bottom:5px" imageurl="~/' + retVal.dtFJ[i]["fj_url"] + '"><img src="approot/r/' + retVal.dtFJ[i]["fj_url"] + '" width="100px" height="100px"/></div>';
                else if (retVal.dtFJ[i]["fj_lx"] == 5)
                    html3 += '<div class="file-item uploadimages" style="margin-left:5px;margin-bottom:5px" imageurl="~/' + retVal.dtFJ[i]["fj_url"] + '"><img src="approot/r/' + retVal.dtFJ[i]["fj_url"] + '" width="100px" height="100px"/></div>';
            }
            $("#fileList").append(html1);
            $("#fileList2").append(html2);
            $("#fileList3").append(html3);
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
        }, CS.onError, picItem, id, v, "保洁");
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
        }, CS.onError, picItem, id, v, "保洁");
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
                            Ext.MessageBox.confirm('确认', '确认创建？', function (btn) {
                                if (btn == 'yes') {
                                    var form = Ext.getCmp('userform');
                                    if (form.form.isValid()) {
                                        //取得表单中的内容
                                        CS('CZCLZ.AdminDB.AgreeBj', function (retVal) {
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
                            type: 'fit'
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
                                title: '保洁基本信息',
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
                                             name: 'CLEANING_NAME',
                                             margin: '10 10 10 10',
                                             fieldLabel: '保洁姓名',
                                             allowBlank: false,
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'numberfield',
                                              name: 'CLEANING_AGE',
                                              margin: '10 10 10 10',
                                              fieldLabel: '年龄',
                                              columnWidth: 0.5,
                                              labelWidth: 150

                                          },
                                            {
                                                xtype: 'textfield',
                                                name: 'LoginName',
                                                margin: '10 10 10 10',
                                                fieldLabel: '用户名',
                                                allowBlank: false,
                                                columnWidth: 0.5,
                                                labelWidth: 150

                                            },
                                         {
                                             xtype: 'textfield',
                                             name: 'PassWord',
                                             margin: '10 10 10 10',
                                             fieldLabel: '密码',
                                             allowBlank: false,
                                             columnWidth: 0.5,
                                             labelWidth: 150

                                         },
                                          {
                                              xtype: 'combobox',
                                              name: 'CLEANING_SEX',
                                              margin: '10 10 10 10',
                                              fieldLabel: '性别',
                                              columnWidth: 0.5,
                                              labelWidth: 150,
                                              queryMode: 'local',
                                              displayField: 'TEXT',
                                              valueField: 'VALUE',
                                              store: new Ext.data.ArrayStore({
                                                  fields: ['TEXT', 'VALUE'],
                                                  data: [
                                                      ['男', '男'],
                                                      ['女', '女']
                                                  ]
                                              })
                                          },
                                          {
                                              xtype: 'textfield',
                                              name: 'CLEANING_MOBILE_TEL',
                                              margin: '10 10 10 10',
                                              fieldLabel: '保洁手机号',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          },
                                           {
                                               xtype: 'textfield',
                                               name: 'CLEANING_IDENTITY_NUMBER',
                                               margin: '10 10 10 10',
                                               fieldLabel: '保洁身份证号',
                                               columnWidth: 0.5,
                                               labelWidth: 150
                                           },
                                            {
                                                xtype: 'combobox',
                                                name: 'CLEANING_SUBORDINATE',
                                                margin: '10 10 10 10',
                                                fieldLabel: '保洁从属',
                                                columnWidth: 0.5,
                                                labelWidth: 150,
                                                queryMode: 'local',
                                                displayField: 'TEXT',
                                                valueField: 'VALUE',
                                                store: new Ext.data.ArrayStore({
                                                    fields: ['TEXT', 'VALUE'],
                                                    data: [
                                                        ['平台招募', '平台招募'],
                                                        ['房东聘用', '房东聘用']
                                                    ]
                                                })
                                            },
                                            {
                                                xtype: 'textfield',
                                                name: 'CLEANING_SALARY',
                                                margin: '10 10 10 10',
                                                fieldLabel: '基本薪资',
                                                columnWidth: 0.5,
                                                labelWidth: 150
                                            },

                                              {
                                                  xtype: 'combobox',
                                                  name: 'CLEANING_INSURANCE',
                                                  margin: '10 10 10 10',
                                                  fieldLabel: '五险一金',
                                                  columnWidth: 0.5,
                                                  labelWidth: 150,
                                                  queryMode: 'local',
                                                  displayField: 'TEXT',
                                                  valueField: 'VALUE',
                                                  store: new Ext.data.ArrayStore({
                                                      fields: ['TEXT', 'VALUE'],
                                                      data: [
                                                          ['有', '有'],
                                                          ['无', '无']
                                                      ]
                                                  })
                                              },
                                                {
                                                    xtype: 'textfield',
                                                    name: 'COMMISSION_SALARY',
                                                    margin: '10 10 10 10',
                                                    fieldLabel: '提成金额',
                                                    columnWidth: 0.5,
                                                    labelWidth: 150
                                                },

                                                   {
                                                       xtype: 'combobox',
                                                       name: 'COMMISSION_TYPE',
                                                       margin: '10 10 10 10',
                                                       fieldLabel: '提成方式',
                                                       columnWidth: 0.5,
                                                       labelWidth: 150,
                                                       queryMode: 'local',
                                                       displayField: 'TEXT',
                                                       valueField: 'VALUE',
                                                       store: new Ext.data.ArrayStore({
                                                           fields: ['TEXT', 'VALUE'],
                                                           data: [
                                                               ['按间提成', '按间提成'],
                                                               ['按客评+间', '按客评+间']
                                                           ]
                                                       })
                                                   },
                                          {
                                              xtype: 'datefield',
                                              name: 'CONTRACT_START_TIME',
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
                                             name: 'CONTRACT_END_TIME',
                                             editable: false,
                                             margin: '10 10 10 10',
                                             fieldLabel: '合同失效时间',
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'displayfield',
                                              id: 'tp1',
                                              value: ' <div id="fileList"><div id="filePicker" style="float:left;margin-right:10px;margin-bottom:5px;width:50px;height:50px;">点击选择图片</div></div>',
                                              margin: '10 10 10 10',
                                              fieldLabel: '保洁身份证图片',
                                              columnWidth: 1,
                                              labelWidth: 150
                                          },
                                           {
                                               xtype: 'displayfield',
                                               id: 'tp2',
                                               value: ' <div id="fileList2"><div id="filePicker2" style="float:left;margin-right:10px;margin-bottom:5px;width:50px;height:50px;">点击选择图片</div></div>',
                                               margin: '10 10 10 10',
                                               fieldLabel: '保洁合同照片',
                                               columnWidth: 1,
                                               labelWidth: 150
                                           },
                                            {
                                                xtype: 'displayfield',
                                                id: 'tp3',
                                                value: ' <div id="fileList3"><div id="filePicker3" style="float:left;margin-right:10px;margin-bottom:5px;width:50px;height:50px;">点击选择图片</div></div>',
                                                margin: '10 10 10 10',
                                                fieldLabel: '保洁体检报告',
                                                columnWidth: 1,
                                                labelWidth: 150
                                            }



                                ],
                                buttonAlign: 'center',
                                buttons: [
                                    {
                                        text: '同意',
                                        handler: function () {
                                            Ext.MessageBox.confirm('确认', '确认通过？', function (btn) {
                                                if (btn == 'yes') {

                                                    CS('CZCLZ.AdminDB.AgreeBj', function (retVal) {
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
                                                   CS('CZCLZ.AdminDB.NoAgreeBj', function (retVal) {
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
    initwebupload("filePicker3", "fileList3", 5);

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