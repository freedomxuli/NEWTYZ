var Up_userid = queryString.id;
var picItem = [];

//************************************数据源*****************************************
var dqstore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT'],
    data: [
    ]
});
//************************************主界面*****************************************

function tp(v) {
    if (Up_userid != "" && Up_userid != null) {
        CS('CZCLZ.YHGLClass.GetProductImages', function (retVal) {
            for (var i = 0; i < retVal.length; i++) {
                Ext.getCmp('uploadproductpic').add(new SelectImg({
                    isSelected: false,
                    src: retVal[i].FJ_URL,
                    fileid: retVal[i].FJ_ID
                }));
            }
        }, CS.onError, picItem, Up_userid, v, 'user');
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
        }, CS.onError, picItem, Up_userid, v, 'user');
    }
    var win = new phWin({ lx: v });
    win.show();
}
function getQy() {
    CS('CZCLZ.YHGLClass.GetQy', function (retVal) {
        if (retVal) {
            dqstore.add([{ 'VALUE': '', 'TEXT': '所有区域' }]);
            dqstore.loadData(retVal, true);
            Ext.getCmp("QY_ID").setValue('');
        }
    }, CS.onError);
}

function bindData() {
    CS('CZCLZ.YHGLClass.GetJs', function (retVal) {
        if (retVal) {
            var Items = [];
            for (var i = 0; i < retVal.length; i++) {

                var checkbox = new Ext.form.field.Checkbox({
                    xtype: 'checkboxfield',
                    boxLabel: retVal[i].JS_NAME,
                    name: 'yhjs',
                    hideLabel: true,
                    inputValue: retVal[i].JS_ID + '',
                    handler: function (chk, checked) {
                        var jsid = this.inputValue;
                        var jsname = this.boxLabel;
                        if (checked) {
                            CS('CZCLZ.YHGLClass.GetDWAndGl', function (retVal) {


                                //判断是否勾选的是权限表里是否有内容
                                var dtqx = retVal.dtqx;
                                var qxpan;
                                if (dtqx.length > 0) {
                                    var qxStore = Ext.create('Ext.data.Store', {
                                        fields: [
                                           { name: 'ID' },
                                           { name: 'MC' }
                                        ]
                                    });
                                    qxStore.loadData(dtqx);
                                    qxpan = Ext.create('Ext.grid.Panel', {
                                        id: 'qx' + jsid,
                                        title: jsname + '权限',
                                        store: qxStore,
                                        stateful: false,
                                        layout: {
                                            type: 'fit'
                                        },
                                        selModel: Ext.create('Ext.selection.CheckboxModel', {

                                        }),
                                        columns: [
                                            {
                                                xtype: 'gridcolumn',
                                                dataIndex: 'MC',
                                                sortable: false,
                                                menuDisabled: true,
                                                width: 400,
                                                text: '名称'
                                            },
                                            {
                                                xtype: 'gridcolumn',
                                                dataIndex: 'ID',
                                                hidden: true,
                                                sortable: false,
                                                menuDisabled: true,
                                                text: 'ID'
                                            }
                                        ],
                                        viewConfig: {

                                        }
                                    });
                                    Ext.getCmp("jstab").add(qxpan);
                                }

                                //Ext.getCmp("jstab").setActiveTab(jspan);
                                var usergl = retVal.usergl;

                                if (dtqx.length > 0) {

                                    Ext.getCmp("jstab").setActiveTab(qxpan);
                                    var qxgl = retVal.dtqxgl;

                                    for (var i = 0; i < qxgl.length; i++) {

                                        var record = qxStore.findRecord("ID", qxgl[i].PRIVILEGECODE);
                                        qxpan.getSelectionModel().select(record, true, true);


                                    }
                                }

                            }, CS.onError, jsid, Up_userid)

                        }
                        else {
                            Ext.getCmp("jstab").remove(Ext.getCmp("tab" + jsid))
                            if (Ext.getCmp("qx" + jsid)) {
                                Ext.getCmp("jstab").remove(Ext.getCmp("qx" + jsid))
                            }
                        }
                    }
                });
                Items.push(checkbox);
            }
            Ext.getCmp("yhjsGroup").add(Items);

            if (Up_userid) {


                //根据用户和角色
                CS('CZCLZ.YHGLClass.GetUserAndJs', function (retVal) {
                    if (retVal) {
                        var dtuser = retVal.dtuser;
                        var dtjs = retVal.dtjs;
                        var addform = Ext.getCmp('addform');
                        addform.setTitle("修改");
                        addform.form.setValues(dtuser[0]);
                        if (dtuser[0]["QY_ID"] != "" && dtuser[0]["QY_ID"] != null)
                            Ext.getCmp("QY_ID").setValue(parseInt(dtuser[0]["QY_ID"]));
                        var SexRadio = Ext.getCmp('User_Sex').items;
                        if (parseInt(dtuser[0]["User_Sex"]) == 0) {
                            SexRadio.get(0).setValue(true);
                        }
                        else {
                            SexRadio.get(1).setValue(true);
                        }

                        var jsids = [];
                        for (var i = 0; i < dtjs.length; i++) {
                            jsids[i] = dtjs[i].JS_ID;
                        }

                        Ext.getCmp("yhjsGroup").setValue({
                            yhjs: jsids + ''
                        });

                    }
                }, CS.onError, Up_userid);


            }
        }
    }, CS.onError);

}

Ext.onReady(function () {
    Ext.define('useraddview', {
        extend: 'Ext.container.Viewport',

        layout: {
            type: 'fit'
        },

        initComponent: function () {
            var me = this;
            me.items = [
                {
                    xtype: 'form',
                    id: 'addform',
                    autoScroll: true,
                    layout: {
                        columns: 2,
                        type: 'table'
                    },
                    bodyPadding: 10,
                    title: '新增',
                    items: [
                        {
                            xtype: 'textfield',
                            fieldLabel: 'ID',
                            id: 'User_ID',
                            name: 'User_ID',
                            labelWidth: 70,
                            hidden: true,
                            colspan: 2
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '用户名',
                            id: 'LoginName',
                            name: 'LoginName',
                            labelWidth: 70,
                            allowBlank: false,
                            anchor: '100%'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '密码',
                            id: 'Password',
                            name: 'Password',
                            labelWidth: 70,
                            allowBlank: false,
                            anchor: '100%'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '姓名',
                            id: 'User_XM',
                            name: 'User_XM',
                            allowBlank: false,
                            labelWidth: 70,
                            anchor: '100%'
                        },
                         {
                             xtype: 'radiogroup',
                             fieldLabel: '性别',
                             id: 'User_Sex',
                             name: 'User_Sex',
                             labelWidth: 70,
                             anchor: '100%',
                             items: [
                                 { boxLabel: '男', name: 'sjck', inputValue: '0', width: 50, checked: true },
                                 { boxLabel: '女', name: 'sjck', inputValue: '1' }
                             ]

                         },
                        {
                            xtype: 'textfield',
                            fieldLabel: '年龄',
                            name: 'User_Age',
                            allowBlank: false,
                            labelWidth: 70,
                            anchor: '100%'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '籍贯',
                            name: 'User_From',
                            labelWidth: 70,
                            anchor: '100%'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '学历',
                            name: 'User_Education',
                            labelWidth: 70,
                            anchor: '100%'
                        },
                         {
                             xtype: 'textfield',
                             fieldLabel: '身份证号',
                             name: 'User_IdCard',
                             allowBlank: false,
                             labelWidth: 70,
                             anchor: '100%'
                         },
                          {
                              xtype: 'textfield',
                              width: 500,
                              fieldLabel: '住址',
                              colspan: 2,
                              id: 'User_DZ',
                              name: 'User_DZ',
                              labelWidth: 70,
                              anchor: '100%'
                          },
                        {
                            xtype: 'textfield',
                            fieldLabel: '手机号',
                            allowBlank: false,
                            name: 'User_SJ',
                            labelWidth: 70,
                            anchor: '100%'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '电子邮件',
                            id: 'User_Email',
                            name: 'User_Email',
                            labelWidth: 70,
                            anchor: '100%'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: '工号',
                            name: 'User_JobNo',
                            allowBlank: false,
                            labelWidth: 70,
                            anchor: '100%'
                        },
                         {
                             xtype: 'textfield',
                             fieldLabel: '合同备案号',
                             name: 'User_ContractNo',
                             labelWidth: 70,
                             anchor: '100%'
                         },
                         {
                             xtype: 'combobox',
                             fieldLabel: '用工方式',
                             name: 'User_Mode',
                             allowBlank: false,
                             labelWidth: 70,
                             anchor: '100%',
                             queryMode: 'local',
                             displayField: 'TEXT',
                             valueField: 'VALUE',
                             store: new Ext.data.ArrayStore({
                                 fields: ['TEXT', 'VALUE'],
                                 data: [
                                     ['合同', '合同'],
                                     ['兼职', '兼职'],
                                     ['派遣', '派遣']
                                 ]
                             })
                         },
                         {
                             xtype: 'combobox',
                             id: 'QY_ID',
                             name: 'QY_ID',
                             fieldLabel: '负责区域',
                             editable: false,
                             store: dqstore,
                             queryMode: 'local',
                             displayField: 'TEXT',
                             valueField: 'VALUE',
                             labelWidth: 70,
                             anchor: '100%'
                         },
                         {
                             xtype: 'datefield',
                             format: 'Y-m-d',
                             allowBlank: false,
                             fieldLabel: '签约起始日期',
                             name: 'StartDate',
                             labelWidth: 70,
                             anchor: '100%'
                         },
                         {
                             xtype: 'datefield',
                             format: 'Y-m-d',
                             allowBlank: false,
                             fieldLabel: '结束日期',
                             name: 'EndDate',
                             labelWidth: 70,
                             anchor: '100%'
                         },


                         {
                             xtype: 'displayfield',
                             value: '<a href="#" onclick="tp(\'1\')">上传</a>',
                             fieldLabel: '照片(*)',
                             labelWidth: 70,
                             anchor: '100%'
                         },
                         {
                             xtype: 'displayfield',
                             value: '<a href="#" onclick="tp(\'2\')">上传</a>',
                             fieldLabel: '身份证(*)',
                             labelWidth: 70,
                             anchor: '100%'
                         },
                         {
                             xtype: 'displayfield',
                             value: '<a href="#" onclick="tp(\'3\')">上传</a>',
                             fieldLabel: '电子合同(*)',
                             labelWidth: 70,
                             anchor: '100%'
                         },
                         {
                             xtype: 'displayfield',
                             value: '<a href="#" onclick="tp(\'4\')">上传</a>',
                             fieldLabel: '证书(*)',
                             labelWidth: 70,
                             anchor: '100%'
                         },


                        {
                            xtype: 'combobox',
                            fieldLabel: '在职状态',
                            boxLabel: '停用',
                            labelWidth: 70,
                            colspan: 2,
                            id: 'User_Enable',
                            name: 'User_Enable',
                            queryMode: 'local',
                            displayField: 'TEXT',
                            valueField: 'VALUE',
                            store: new Ext.data.ArrayStore({
                                fields: ['TEXT', 'VALUE'],
                                data: [
                                    ['在职', 0],
                                    ['停职', 1],
                                    ['离职', 2]
                                ]
                            })
                        },
                        {
                            xtype: 'checkboxgroup',
                            id: 'yhjsGroup',
                            width: 400,
                            layout: {
                                type: 'table'
                            },
                            fieldLabel: '角色',
                            labelWidth: 70,
                            colspan: 2,
                            items: [
                            ]
                        },
                        {
                            xtype: 'tabpanel',
                            id: 'jstab',
                            height: 300,
                            activeTab: 1,
                            colspan: 2,
                            items: [
                            ]
                        }
                    ],
                    buttonAlign: 'center',
                    buttons: [
                        {
                            text: '确定',
                            iconCls: 'dropyes',
                            handler: function () {
                                var form = Ext.getCmp('addform');
                                if (form.form.isValid()) {
                                    //取得表单中的内容
                                    var values = form.form.getValues(false);

                                    var Radio = Ext.getCmp('User_Sex').items;
                                    for (var i = 0; i < Radio.length; i++) {
                                        if (Radio.get(i).checked) {
                                            values["User_Sex"] = Radio.get(i).inputValue;
                                        }
                                    }

                                    //if (Ext.getCmp("User_Enable").value) {
                                    //    values["User_Enable"] = 1;
                                    //}
                                    //else {
                                    //    values["User_Enable"] = 0;
                                    //}

                                    var yhjgs = Ext.getCmp("yhjsGroup").items.items;
                                    var yhjsids = [];
                                    var yhjsDwids = [];
                                    var qxids = [];
                                    for (var i = 0; i < yhjgs.length; i++) {
                                        if (yhjgs[i].checked) {
                                            var num = yhjsids.length;
                                            yhjsids[yhjsids.length] = yhjgs[i].inputValue;

                                            //var tabgrid = Ext.getCmp("tab" + yhjgs[i].inputValue);
                                            //var rds = tabgrid.getSelectionModel().getSelection();

                                            //var idlist = [];
                                            //for (var n = 0, len = rds.length; n < len; n++) {
                                            //    var rd = rds[n];
                                            //    idlist.push(rd.get("ID"));
                                            //}
                                            //if (idlist.length == 0) {
                                            //    Ext.Msg.show({
                                            //        title: '提示',
                                            //        msg: '请至少选择一条数据!',
                                            //        buttons: Ext.MessageBox.OK,
                                            //        icon: Ext.MessageBox.INFO
                                            //    });
                                            //    return;
                                            //}

                                            //yhjsDwids[num] = idlist;

                                            var qxtab = Ext.getCmp("qx" + yhjgs[i].inputValue);
                                            if (qxtab) {
                                                var qxrds = qxtab.getSelectionModel().getSelection();
                                                for (var m = 0, qxlen = qxrds.length; m < qxlen; m++) {
                                                    var qxrd = qxrds[m];
                                                    qxids.push(qxrd.get("ID"));
                                                }
                                            }
                                        }
                                    }

                                    if (yhjsids.length == 0) {
                                        Ext.Msg.show({
                                            title: '提示',
                                            msg: '请选择角色类型!',
                                            buttons: Ext.MessageBox.OK,
                                            icon: Ext.MessageBox.INFO
                                        });
                                        return;
                                    }

                                    CS('CZCLZ.YHGLClass.SaveUser', function (retVal) {
                                        if (retVal) {
                                            FrameStack.popFrame();
                                        }
                                    }, CS.onError, values, yhjsids, yhjsDwids, qxids, picItem);

                                }
                            }
                        },
                        {
                            text: '返回',
                            iconCls: 'back',
                            handler: function () {
                                FrameStack.popFrame();
                            }
                        }
                    ]
                }
            ];
            me.callParent(arguments);
        }
    });

    new useraddview();

    getQy();

    bindData();



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