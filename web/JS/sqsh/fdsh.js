
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

//var dqstore = Ext.create('Ext.data.Store', {
//    fields: ['VALUE', 'TEXT'],
//    data: [
//    ]
//});

function sh() {
    var win = new ShWin();
    win.show();
}


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
    CS('CZCLZ.JjrDB.GetFdById', function (retVal) {
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
                            type: 'vbox',
                            align: 'center'
                        },
                        autoScroll: true,
                        buttonAlign: 'center',
                        buttons: [
                            {
                                text: '同意',
                                handler: function () {
                                    Ext.MessageBox.confirm('确认', '确认通过？', function (btn) {
                                        if (btn == 'yes') {

                                            CS('CZCLZ.AdminDB.AgreeFd', function (retVal) {
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
                                             CS('CZCLZ.AdminDB.NoAgreeFd', function (retVal) {
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
                                width: 850,
                                height: 800,
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
                                                 xtype: 'combobox',
                                                 name: 'SETTLEMENT_CYCLE',
                                                 margin: '10 10 10 10',
                                                 fieldLabel: '结算周期',
                                                 columnWidth: 0.5,
                                                 labelWidth: 150,
                                                 queryMode: 'local',
                                                 displayField: 'TEXT',
                                                 valueField: 'VALUE',
                                                 store: new Ext.data.ArrayStore({
                                                     fields: ['TEXT', 'VALUE'],
                                                     data: [
                                                         ['T+3', '3'],
                                                         ['T+7', '7'],
                                                         ['T+10', '10'],
                                                         ['T+15', '15'],
                                                         ['T+20', '20'],
                                                         ['T+30', '30']

                                                     ]
                                                 })
                                             },
                                            {
                                                xtype: 'combobox',
                                                allowBlank: false,
                                                name: 'COMMISSION_RATIO',
                                                margin: '10 10 10 10',
                                                fieldLabel: '佣金比例',
                                                columnWidth: 0.5,
                                                labelWidth: 150,
                                                queryMode: 'local',
                                                displayField: 'TEXT',
                                                valueField: 'VALUE',
                                                store: new Ext.data.ArrayStore({
                                                    fields: ['TEXT', 'VALUE'],
                                                    data: [
                                                         ['1%', 0.01],
                                                        ['3%', 0.03],
                                                        ['5%', 0.05],
                                                        ['8%', 0.08],
                                                        ['10%', 0.1],
                                                        ['15%', 0.15],
                                                        ['30%', 0.3],
                                                        ['50%', 0.5],
                                                        ['70%', 0.7]
                                                    ]
                                                })
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
                                             id: 'LANDLORD_IDENTITY_NUMBER',
                                             name: 'LANDLORD_IDENTITY_NUMBER',
                                             margin: '10 10 10 10',
                                             fieldLabel: '身份证号',
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
                                              xtype: 'displayfield',
                                              id: 'tp1',
                                              value: ' <div id="fileList"><div id="filePicker" style="float:left;margin-right:10px;margin-bottom:5px;width:50px;height:50px;">点击选择图片</div></div>',
                                              margin: '10 10 10 10',
                                              fieldLabel: '身份证图片',
                                              columnWidth: 1,
                                              labelWidth: 80
                                          },
                                           {
                                               xtype: 'displayfield',
                                               id: 'tp2',
                                               value: ' <div id="fileList2"><div id="filePicker2" style="float:left;margin-right:10px;margin-bottom:5px;width:50px;height:50px;">点击选择图片</div></div>',
                                               margin: '10 10 10 10',
                                               fieldLabel: '用工合同照片',
                                               columnWidth: 1,
                                               labelWidth: 80
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
                                              xtype: 'combobox',
                                              name: 'LANDLORD_CONTRACT_STATE',
                                              margin: '10 10 10 10',
                                              fieldLabel: '合同类型',
                                              columnWidth: 0.5,
                                              labelWidth: 150,
                                              queryMode: 'local',
                                              displayField: 'TEXT',
                                              valueField: 'VALUE',
                                              store: new Ext.data.ArrayStore({
                                                  fields: ['TEXT', 'VALUE'],
                                                  data: [
                                                      ['托管合同', '托管合同'],
                                                      ['合作合同', '合作合同']
                                                  ]
                                              })
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
                                                allowBlank: false,
                                                store: dqstore,
                                                queryMode: 'local',
                                                displayField: 'TEXT',
                                                valueField: 'VALUE',
                                                margin: '10 10 10 10',
                                                fieldLabel: '所属区域',
                                                columnWidth: 0.5,
                                                labelWidth: 150
                                            },
                                          {
                                              xtype: 'displayfield',
                                              hidden:true,
                                              name: 'DealerAuthoriCode',
                                              margin: '10 10 10 10',
                                              fieldLabel: '授权码',
                                              columnWidth: 0.5,
                                              labelWidth: 150
                                          }
                                        //{
                                        //    xtype: 'displayfield',
                                        //    name: 'QY_NAME',

                                        //    margin: '10 10 10 10',
                                        //    fieldLabel: '代理商所属区域',
                                        //    columnWidth: 0.5,
                                        //    labelWidth: 150
                                        //},
                                        //  {
                                        //      xtype: 'displayfield',
                                        //      name: 'User_XM',
                                        //      margin: '10 10 10 10',
                                        //      fieldLabel: '经纪人',
                                        //      columnWidth: 0.5,
                                        //      labelWidth: 150
                                        //  },
                                        //  {
                                        //      xtype: 'displayfield',
                                        //      name: 'LANDLORD_APPLY_TIME',
                                        //      margin: '10 10 10 10',
                                        //      fieldLabel: '申请时间',
                                        //      columnWidth: 0.5,
                                        //      labelWidth: 150
                                        //  }


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


    ACS('CZCLZ.YHGLClass.GetQy', function (retVal) {
        if (retVal) {
            dqstore.loadData(retVal, true);
            if (id != null && id != "")
                DataBind();
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
