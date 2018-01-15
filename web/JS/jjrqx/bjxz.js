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
                        items: [
                            {
                                xtype: 'form',
                                id: 'addform',
                                layout: {
                                    type: 'column'
                                },
                                width: 850,
                                height: 800,
                                border: true,
                                // margin: 10,
                                //title: '保洁基本信息',
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
                                              allowBlank: false,
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
                                             allowBlank: false,
                                             columnWidth: 0.5,
                                             labelWidth: 150
                                         },
                                         //{
                                         //    xtype: 'displayfield',
                                         //    value: '<a href="#" onclick="tp(\'2\')">上传</a>',
                                         //    margin: '10 10 10 10',
                                         //    fieldLabel: '保洁身份证图片',
                                         //    columnWidth: 0.5,
                                         //    labelWidth: 150
                                         //},
                                         //{
                                         //    xtype: 'displayfield',
                                         //    value: '<a href="#" onclick="tp(\'3\')">上传</a>',
                                         //    margin: '10 10 10 10',
                                         //    fieldLabel: '保洁合同照片',
                                         //    columnWidth: 0.5,
                                         //    labelWidth: 150
                                         //},
                                         // {
                                         //     xtype: 'displayfield',
                                         //     value: '<a href="#" onclick="tp(\'5\')">上传</a>',
                                         //     margin: '10 10 10 10',
                                         //     fieldLabel: '保洁体检报告',
                                         //     columnWidth: 0.5,
                                         //     labelWidth: 150
                                         // },
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
                                        text: '保存',
                                        handler: function () {
                                            var form = Ext.getCmp('addform');
                                            if (form.form.isValid()) {
                                                var values = form.getValues(false);

                                                var imglist = "";
                                                $("#fileList .file-item").each(function () {
                                                    imglist += $(this).attr("imageurl") + ",";
                                                })

                                                if (imglist.length > 0)
                                                    imglist = imglist.substr(0, imglist.length - 1);

                                                var imglist2 = "";
                                                $("#fileList2 .file-item").each(function () {
                                                    imglist2 += $(this).attr("imageurl") + ",";
                                                })

                                                if (imglist2.length > 0)
                                                    imglist2 = imglist2.substr(0, imglist2.length - 1);

                                                var imglist3 = "";
                                                $("#fileList3 .file-item").each(function () {
                                                    imglist3 += $(this).attr("imageurl") + ",";
                                                })

                                                if (imglist3.length > 0)
                                                    imglist3 = imglist3.substr(0, imglist3.length - 1);

                                                CS('CZCLZ.JjrDB.SaveBjXX', function (retVal) {
                                                    if (retVal) {
                                                        FrameStack.popFrame();
                                                    }
                                                }, CS.onError, values, picItem, imglist, imglist2, imglist3);
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
            });

            me.callParent(arguments);
        }

    });
    new add();
    initwebupload("filePicker", "fileList", 5);
    initwebupload("filePicker2", "fileList2", 5);
    initwebupload("filePicker3", "fileList3", 5);
    ACS('CZCLZ.YHGLClass.GetQy', function (retVal) {
        if (retVal) {
            dqstore.loadData(retVal, true);
            if (id != null && id != "")
                DataBind();
        }
    }, CS.onError);

    //if (id != null && id != "")
    //    DataBind();
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
                         }, CS.onError, lx, '保洁');
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