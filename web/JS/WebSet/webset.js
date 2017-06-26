function tp(v) {
    //if (Up_userid != "" && Up_userid != null) {
    //    CS('CZCLZ.YHGLClass.GetProductImages', function (retVal) {
    //        for (var i = 0; i < retVal.length; i++) {
    //            Ext.getCmp('uploadproductpic').add(new SelectImg({
    //                isSelected: false,
    //                src: retVal[i].FJ_URL,
    //                fileid: retVal[i].FJ_ID
    //            }));
    //        }
    //    }, CS.onError, picItem, Up_userid, v, 'user');
    //}
    //else {
    //    CS('CZCLZ.YHGLClass.GetProductImages', function (retVal) {
    //        for (var i = 0; i < retVal.length; i++) {
    //            Ext.getCmp('uploadproductpic').add(new SelectImg({
    //                isSelected: false,
    //                src: retVal[i].FJ_URL,
    //                fileid: retVal[i].FJ_ID
    //            }));
    //        }
    //    }, CS.onError, picItem, Up_userid, v, 'user');
    //}
    var win = new phWin({ lx: v });
    win.show();
}


Ext.onReady(function () {
    Ext.define('add', {
        extend: 'Ext.container.Viewport',
        layout: {
            type: 'fit'
        },
        autoScroll: true,
        initComponent: function () {
            var me = this;

            Ext.applyIf(me, {
                items: [
                    {
                        xtype: 'panel',
                        layout: {
                            type: 'anchor'
                        },
                        autoScroll: false,
                        items: [
                            {
                                xtype: 'panel',
                                layout: {
                                    type: 'column'
                                },
                                margin: 10,
                                items: [

                                     {
                                         xtype: 'textfield',
                                         margin: '10 10 10 10',
                                         fieldLabel: '平台电话',
                                         editable: false,
                                         columnWidth: 1,
                                         labelWidth: 150
                                     },
                                    {
                                        xtype: 'displayfield',
                                        margin: '10 10 10 10',
                                        fieldLabel: '背景图片',
                                        columnWidth: 1,
                                        allowBlank: false,
                                        labelWidth: 150,
                                        value: '<a href="#" onclick="tp(\'4\')">上传</a>',
                                    }
                                ]
                            }
                        ],
                        buttonAlign: 'center',
                        buttons: [
                            {
                                text: '保存',
                                iconCls: 'dropyes',
                                handler: function () {
                                    var form = Ext.getCmp('addform');
                                    if (form.form.isValid()) {
                                        //取得表单中的内容
                                        var values = form.form.getValues(false);


                                        CS('CZCLZ.YHGLClass.SaveUser', function (retVal) {
                                            if (retVal) {
                                                FrameStack.popFrame();
                                            }
                                        }, CS.onError, values, yhjsids, yhjsDwids, qxids, picItem);

                                    }
                                }
                            }
                          
                        ]
                    }
                ]
            });

            me.callParent(arguments);
        }

    });

    new add();
})

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