var newstid = window.queryString.newstid;
/*
---combox数据加载
*/
var store = new Ext.data.Store({
    fields: ['XWLB_ID', 'XWLB_NAME'],
    data: [],
    proxy: {
        type: 'memory',
        reader: {
            type: 'json'
        }
    }
});

//------------------------------------
var upfiles = [];
var FJ_ID = [];
var size = 0;

//删除上传附件
function deleteupfile(up, num) {
    var id = "div" + up;
    //删除附件标签元素
    Ext.get(id).remove();
    //删除数组选定的数组元素
    upfiles.splice(num, 1);
    //删除附件GUID
    for (var i = 0; i < FJ_ID.length; i++) {
        if (FJ_ID[i] == up) {
            FJ_ID.splice(i, 1);
        }
    }

}

//打开文件
function GetFile(fjid, fjmc) {

    DownloadFile("DataCenter.newsListMaxClass.GetFile", fjmc, fjid);
}

//--判断是不是图片
function isimg(src) {
    var ext = ['.gif', '.jpg', '.jpeg', '.png'];
    var s = src.toLowerCase();
    var r = false;
    for (var i = 0; i < ext.length; i++) {
        if (s.indexOf(ext[i]) > 0) {
            // alert(ext[i]);
            r = true;
            break;
        }
    }
    return r;
}
//--插入文件---
function charu(fjid, fjmc) {

    // alert(Ext.getCmp('contxt').getValue());
    if (isimg(fjmc)) {
        strHref = "<img alt='" + fjmc + "' src='~/files/" + fjid + "/" + fjmc + "' class='locImg' />";
    }
    else {
        strHref = "<a href='approot/r/files/" + fjid + "/" + fjmc + "' class = 'locFile' id ='" + fjid + "' >" + fjmc + "</a>";
    }

    Ext.getCmp("contxt").insertAtCursor(strHref);

    //Ext.getCmp("contxt").setValue(Ext.getCmp('contxt').getValue() + strHref);

}


//附件列表
function fjlist(fj) {
    var html = "";
    html += '<table width="95%" border="0" cellspacing="0" cellpadding="0" >';
    html += '  <tr>';
    html += '    <td width="65" align="right"></td>';
    html += '    <td bgcolor="#D5E2F2"></td>';
    html += '  </tr>';
    html += '  <tr>';
    html += '    <td></td>';
    html += '    <td height=20 id=uptd bgcolor="#D5E2F2" >';
    for (var i = 0; i < fj.length; i++) {
        html += '       <div id=div' + fj[i]['fj_id'] + ' style="FONT-SIZE: 12px; COLOR: #000;">&nbsp;';
        html += '<img src="approot/d/images/fj.gif" /><span><a  href="#" onclick="GetFile(\'' + fj[i]['fj_id'] + '\',\'' + fj[i]['fj_filename'] + '\')">' + fj[i]['fj_filename'] + '</a></span> &nbsp;&nbsp;&nbsp;&nbsp;<span>' + fj[i]['fj_filesize'] + 'K</span>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" onclick="charu(\'' + fj[i]['fj_id'] + '\',\'' + fj[i]['fj_filename'] + '\')" >插入</a>&nbsp;&nbsp;<a href="#" onclick="deleteupfile(\'' + fj[i]['fj_id'] + '\',' + i + ')" >删除</a>';
        html += '       </div>';
        size += fj[i]['fj_filesize'];
    }
    html += '    </td>';
    html += '  </tr><tr height=5><td></td><td></td></tr>';
    html += '</table>';

    Ext.getCmp("upfile").setText(html, false);
}

//***********************************数据源******************************************


//*******************************附件******************************************


Ext.define('phWin2', {
    extend: 'Ext.window.Window',
    height: 275,
    width: 653,
    modal: true,
    layout: 'border',
    initComponent: function () {
        var me = this;
        var SPID = me.SPID;


        me.items = [{
            xtype: 'UploaderPanel',
            id: 'uploadproductpicx',
            region: 'center',
            autoScroll: true,
            dockedItems: [{
                xtype: 'toolbar',
                dock: 'top',
                items: [
                        {
                            xtype: 'filefield',
                            width: 300,
                            labelWidth: 65,
                            fieldLabel: '上传附件',
                            buttonText: '浏览'
                        },
                        {
                            xtype: 'button',
                            text: '上传',
                            iconCls: 'upload',
                            handler: function () {
                                Ext.getCmp('uploadproductpicx').upload('DataCenter.newsListMaxClass.UploadPicForFJx', function (retVal) {
                                    var fj = retVal.ht;
                                    FJ_ID.push(fj["fj_id"]);
                                    upfiles.push(fj);
                                    fjlist(upfiles);

                                    me.close();
                                }, CS.onError);
                            }
                        }
                ]
            }]
        }];
        me.callParent(arguments);
    }
});


function tp2() {
    var win2 = new phWin2();
    win2.show();
}



//***********************************************************************




/*--------------------------------------我是完美的上帝分割线--------------------------
-----------------------------------------【弹出界面】-------------------------------
--------------------------------------我是完美的上帝分割线--------------------------*/
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

/*-----------------------------------tiaoshi----------------------------------------*/
Ext.define('phWin', {
    extend: 'Ext.window.Window',
    height: 275,
    width: 653,
    modal: true,
    layout: 'border',
    initComponent: function () {
        var me = this;
        var SPID = me.SPID;


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
                }, '->', {
                    xtype: 'button',
                    text: '上传',
                    iconCls: 'upload',
                    handler: function () {
                        Ext.getCmp('uploadproductpic').upload('DataCenter.newsListMaxClass.UploadPicForProduct', function (retVal) {
                            var isDefault = false;
                            if (retVal.isdefault == 1)
                                isDefault = true;
                            Ext.getCmp('uploadproductpic').add(new SelectImg({
                                isSelected: isDefault,
                                src: retVal.fileurl,
                                fileid: retVal.fileid
                            }));
                        }, CS.onError, SPID);
                    }
                }]
            }],
            buttonAlign: 'center',
            buttons: [{
                text: '设为默认',
                handler: function () {
                    Ext.MessageBox.confirm('确认', '是否设置该图片为默认图片？', function (btn) {
                        if (btn == 'yes') {
                            var selPics = Ext.getCmp('uploadproductpic').query('image[isSelected=true]');
                            if (selPics.length > 0) {
                                CS('DataCenter.newsListMaxClass.SetDefaultPicForProduct', function (retVal) {
                                    if (retVal)
                                        Ext.Msg.alert('提示', '设置成功！');
                                    else
                                        Ext.Msg.alert('提示', '设置失败！');
                                }, CS.onError, selPics[0].fileid, SPID);
                            }
                        }
                    });
                }
            }, {
                text: '删除',
                handler: function () {
                    Ext.MessageBox.confirm('确认', '是否删除该图片？', function (btn) {
                        if (btn == 'yes') {
                            var selPics = Ext.getCmp('uploadproductpic').query('image[isSelected=true]');
                            if (selPics.length > 0) {
                                CS('DataCenter.newsListMaxClass.DelProductImageByPicID', function (retVal) {
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

function tp(v) {

    var picItem = [];
    CS('DataCenter.newsListMaxClass.GetProductImages', function (retVal) {
        for (var i = 0; i < retVal.length; i++) {
            var isDefault = false;
            if (retVal[i].ISDEFAULT == 1)
                isDefault = true;
            // alert(retVal[i].FILEURL);
            // alert(retVal[i].fj_id);
            Ext.getCmp('uploadproductpic').add(new SelectImg({

                isSelected: isDefault,
                src: retVal[i].FILEURL,
                fileid: retVal[i].fj_id
            }));
        }
    }, CS.onError, v);
    var win = new phWin({ SPID: v });
    win.show();
}


///-----数据导入模式-------
//***********************************弹出界面****************************************
Ext.define('moveWin', {
    extend: 'Ext.window.Window',

    height: 126,
    width: 350,
    layout: {
        type: 'table',
        columns: 2
    },
    closeAction: 'destroy',
    title: '移动栏目',
    id: 'movewin',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'UploaderPanel',
                id: 'movetxt',
                frame: true,
                width: 350,
                bodyPadding: 10,
                title: '',
                items: [
                        {
                            xtype: 'combobox',
                            fieldLabel: '移动至',
                            displayField: 'XWLB_NAME',
                            allowBlank: false,
                            valueField: 'XWLB_ID',
                            queryMode: 'local',
                            name: 'XWLB_ID',
                            id: 'lbid',
                            store: store
                        }
                ]
                ,
                buttonAlign: 'center',
                buttons: [
                    {
                        text: '确定',
                        handler: function () {


                            var idlist = [];
                            var grid = Ext.getCmp("shGrid");
                            var rds = grid.getSelectionModel().getSelection();
                            for (var n = 0, len = rds.length; n < len; n++) {
                                var rd = rds[n];

                                idlist.push(rd.get("XW_ID"));
                            }
                            CS('DataCenter.newsListMaxClass.MoveNews', function (retVal) {
                                if (retVal) {
                                    var title = Ext.getCmp("titlesec").getValue();
                                    var datetime1 = Ext.getCmp("datetime1").getValue();
                                    var datetime2 = Ext.getCmp("datetime2").getValue();
                                    var newslbid = Ext.getCmp("cbo").getValue();
                                    DateGrid(1, title, datetime1, datetime2, newslbid);
                                    //this.up('window').close();
                                    Ext.getCmp('movewin').close();
                                }
                            }, CS.onError, idlist, Ext.getCmp('lbid').getValue());

                        }
                    },
                    {
                        text: '关闭',
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

/*-----------------------------------插入图片---------------------------------------*/



/*-----------------------------------tiaoshi----------------------------------------*/

Ext.define('yjzsz', {
    extend: 'Ext.window.Window',

    height: 550,
    width: 650,
    layout: {
        type: 'border'
    },
    title: '新闻列表',


    initComponent: function () {
        var me = this;
        me.items = [
{
    id: 'foem1',
    xtype: 'form',
    frame: true,
    autoScroll: true,
    padding: '10,0,0,10',


    bodyPadding: 10,
    region: 'center',
    items: [
       {
           id: 'newsid',
           name: 'newsid',
           xtype: 'textfield',
           hidden: true,
           fieldLabel: '新闻ID',
           labelWidth: 50
       },
         {
             id: 'userid',
             name: 'userid',
             xtype: 'textfield',
             hidden: true,
             fieldLabel: '修改人员ID',
             labelWidth: 50
         },

         {
             xtype: 'combobox',
             fieldLabel: '栏目',
             labelWidth: 50,
             allowBlank: false,
             store: store,
             id: 'tynum',
             name: 'tynum',
             displayField: 'XWLB_NAME',
             valueField: 'XWLB_ID',
             queryMode: 'local',
             editable: false,
             //allowBlank: false,     //不允许为空
             emptyText: '请选择',
             colspan: 2
         },
        {
            id: 'title',
            name: 'title',
            xtype: 'textfield',
            fieldLabel: '标题',
            labelWidth: 50,
            width: 400
        },
             {
                 xtype: 'displayfield',
                 value: '<a href="#" onclick="tp2()">上传附件</a>',
                 fieldLabel: '附件',
                 labelWidth: 65,
                 anchor: '100%'
             },
           {
               id: 'upfile',
               xtype: 'label',
               text: "",
               bodyStyle: 'padding:5px;',
               anchor: '95%'
           },
          {
              labelWidth: 50,
              id: 'contxt',
              xtype: 'ueditor',
              name: 'contxt',
              fieldLabel: '内容',
              height: 300,
              anchor: '100%'
          }
    ],
    buttonAlign: 'center',
    buttons: [
        {
            text: '确定',
            handler: function () {
                var form = Ext.getCmp('foem1');
                if (form.form.isValid()) {

                    //取得表单中的内容
                    var values = form.form.getValues(false);
                    //保存操作
                    var me = this;

                    CS('DataCenter.newsListMaxClass.SaveNews', function (retVal) {



                        Ext.getCmp("upfile").setText("");
                        me.up('window').close();
                        FJ_ID = [];
                        upfiles = [];
                        DateGrid(1, "", "", "", "");
                    }, function (err) {
                        alert(err);
                    }, values, FJ_ID);
                }
            }
        },
        {
            text: '关闭',
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
var yjzszWin;
function show(str1) {
    window.location.href = 'newsedit.html?id=' + str1;
    return;
    var tc = new yjzsz();
    tc.show(null, function () {
        CS('DataCenter.newsListMaxClass.selectInformation', function (retVal) {
            var dtfor = retVal.dt;
            var dtfile = retVal.dtmax;

            Ext.getCmp("newsid").setValue(dtfor[0].XW_ID);
            //Ext.getCmp("tynum").setRawValue(str2);
            Ext.getCmp("title").setValue(dtfor[0].XW_TITLE);
            Ext.getCmp("contxt").setValue(dtfor[0].XW_CONTEXT);
            Ext.getCmp("tynum").setValue(dtfor[0].XWLB_ID);
            Ext.getCmp("tynum").setRawValue(dtfor[0].XWLB_NAME);
            Ext.getCmp("userid").setValue(dtfor[0].XGYH_ID);

            upfiles = dtfile;
            for (var n = 0; n < dtfile.length; n++) {
                FJ_ID.push(dtfile[n]["fj_id"]);
            }
            fjlist(upfiles);
        }, CS.onError, str1);



    });
}
/*--------------------------------------我是完美的上帝分割线--------------------------
-----------------------------------------【数据源模式】-------------------------------
--------------------------------------我是完美的上帝分割线--------------------------*/
var UserStore = createSFW4Store({
    data: [],
    pageSize: 10,
    total: 1,
    currentPage: 1,
    fields: [
{ name: 'XW_ID', type: 'string' },
{ name: 'XW_TITLE', type: 'string' },
{ name: 'XWLB_NAME', type: 'string' },
{ name: 'people', type: 'string' },
{ name: 'nADDTIME', type: 'string' }
    ],
    //sorters: [{ property: 'b', direction: 'DESC'}],
    onPageChange: function (sto, nPage, sorters) {
        DateGrid(nPage, "", "", "");
    }
});

function DateGrid(nPage, title, datetime1, datetime2) {
    CS('CZCLZ.News.GetNewsList', function (retVal) {
        UserStore.setData({
            data: retVal.dt,
            pageSize: 20,
            total: retVal.ac,
            currentPage: retVal.cp
        });
    }, function (err) {
        alert(err);
    }, nPage, title, datetime1, datetime2);
}

//----数据库链接----------------------------------
//---删除数据方法----
//-----删除数据DeleteNewsType
function DeleteNewsType(str) {
    CS('DataCenter.newsListMaxClass.DeleteNews', function (retVal) {
        UserStore.load();

    }, CS.onError, str);

}
Ext.onReady(function () {
    var sm = Ext.create('Ext.selection.CheckboxModel');
    /*--------------------------------------我是完美的上帝分割线--------------------------
   -----------------------------------------【主界面】-------------------------------
   --------------------------------------我是完美的上帝分割线--------------------------*/
    Ext.define('knjtjbxxcx', {
        extend: 'Ext.container.Viewport',
        listeners: {
            destroy: function () {
                FJ_ID = [];
                upfiles = [];
            }
        },
        layout: {
            type: 'border'
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

        region: 'center',
        items: [
            {
                xtype: 'panel',
                region: 'center',
                layout: {
                    type: 'fit'
                },
                items: [
                    {

                        xtype: 'gridpanel',
                        id: 'shGrid',

                        store: UserStore,
                        layout: {
                            type: 'fit'
                        },
                        frame: true,
                        selModel: sm,
                        columns: [
                            {
                                dataIndex: 'XW_TITLE',
                                flex: 1,
                                text: '标题',
                                sortable: false,
                                menuDisabled: true
                            },

                            {
                                dataIndex: 'people',
                                text: '发布人',
                                flex: 1,
                                sortable: false,
                                menuDisabled: true
                            }
                            ,
                           
                            {
                                dataIndex: 'nADDTIME',
                                flex: 1,
                                text: '时间',
                                sortable: false,
                                menuDisabled: true

                            },
                            {
                                dataIndex: 'cz',
                                text: '操作',
                                width: 100,
                                fixed: true,
                                align: "center",
                                renderer: function (value, cellmeta, record, rowIndex, columnIndex, store) {
                                    return "<a href='#' onclick='show(\"" + record.data['XW_ID'] + "\")' >修改</a>";
                                },
                                sortable: false,
                                menuDisabled: true
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
                                            id: 'datetime1',
                                            name: 'datetime1',
                                            xtype: 'datefield',
                                            fieldLabel: '开始日期',
                                            labelWidth: 60,
                                            format: 'Y-m-d',
                                            //value: new Date(new Date() - 1000 * 3600 * 24 * 7),
                                            width: 150
                                        },
                                        {
                                            id: 'datetime2',
                                            name: 'datetime2',
                                            xtype: 'datefield',
                                            fieldLabel: '结束日期',
                                            labelWidth: 60,
                                            width: 150,
                                            format: 'Y-m-d'//,
                                            //value: new Date()
                                        }
                                    ]
                                },
                                {
                                    xtype: 'toolbar',
                                    dock: 'top',
                                    items: [
                                        {
                                            id: 'titlesec',
                                            name: 'titlesec',
                                            xtype: 'textfield',
                                            width: 400,
                                            fieldLabel: '标题',
                                            labelWidth: 60
                                        },
                                        {
                                            xtype: 'buttongroup',
                                            columns: 2,
                                            items: [
                                                {
                                                    xtype: 'button',
                                                    iconCls: 'search',
                                                    text: '查询',
                                                    handler: function () {
                                                        var title = Ext.getCmp("titlesec").getValue();
                                                        var datetime1 = Ext.getCmp("datetime1").getValue();
                                                        var datetime2 = Ext.getCmp("datetime2").getValue();
                                                        DateGrid(1, title, datetime1, datetime2);
                                                    }
                                                }
                                            ]
                                        },
                                        {
                                            xtype: 'buttongroup',
                                            columns: 2,
                                            items: [
                                                {
                                                    xtype: 'button',
                                                    text: '新增',
                                                    iconCls: 'add',
                                                    handler: function () {
                                                        window.location.href = 'NewsEdit.html';
                                                    }
                                                }
                                            ]
                                        },
                                        {
                                            xtype: 'buttongroup',
                                            columns: 2,
                                            items: [
                                                {
                                                    xtype: 'button',
                                                    iconCls: 'delete',
                                                    text: '删除',
                                                    handler: function () {
                                                        var idlist = [];
                                                        var grid = Ext.getCmp("shGrid");
                                                        var rds = grid.getSelectionModel().getSelection();
                                                        if (rds.length == 0) {
                                                            Ext.Msg.show({
                                                                title: '提示',
                                                                msg: '请选择至少一条要删除的记录!',
                                                                buttons: Ext.MessageBox.OK,
                                                                icon: Ext.MessageBox.INFO
                                                            });
                                                            return;
                                                        }
                                                        Ext.MessageBox.confirm("提示", "是否删除你所选?", function (obj) {
                                                            if (obj == "yes") {
                                                                for (var n = 0, len = rds.length; n < len; n++) {
                                                                    var rd = rds[n];

                                                                    idlist.push(rd.get("XW_ID"));
                                                                }

                                                                CS('CZCLZ.News.DeleteXW', function (retVal) {
                                                                    DateGrid(1, "", "", "");
                                                                }, CS.onError, idlist);
                                                            }
                                                            else {
                                                                return;
                                                            }
                                                        });
                                                    }
                                                }
                                            ]
                                        }
                                    ]
                                },
                                {
                                    xtype: 'pagingtoolbar',
                                    displayInfo: true,
                                    store: UserStore,
                                    dock: 'bottom'
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
    new knjtjbxxcx();
    DateGrid(1, "", "", "");

});




