var id = queryString.id;
var zt = queryString.zt;
var AuthorizeNo;
var userid;
var RoomId;
var HotelId;

var HotelStore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT'],
    data: [
    ]
});

var RoomStore = Ext.create('Ext.data.Store', {
    fields: ['VALUE', 'TEXT'],
    data: [
    ]
});

var UserStore = Ext.create('Ext.data.Store', {
    fields: ['BJUserId', 'UserName'],
    data: [
    ]
});

var goodsStore = Ext.create('Ext.data.Store', {
    fields: [
       { name: 'ID', type: 'string' },
       { name: 'Name', type: 'string' },
       { name: 'Number', type: 'string' },
       { name: 'Money', type: 'string' },
       { name: 'Unit', type: 'string' }
    ]
});

var rzjlStore = Ext.create('Ext.data.Store', {
    fields: [
       { name: 'RealName', type: 'string' },
       { name: 'CellPhone', type: 'string' },
       { name: 'LiveStartDate', type: 'string' },
       { name: 'LiveEndDate', type: 'string' }
    ]
});

function DataBind() {
    CS('CZCLZ.AuthorizeOrderDB.GetAuthorizeOrderById', function (retVal) {
        if (retVal) {


            AuthorizeNo = retVal[0]["AuthorizeNo"];
            userid = retVal[0]["UserId"];
            RoomId = retVal[0]["RoomId"];
            HotelId = retVal[0]["HotelId"];
            CS('CZCLZ.AuthorizeOrderDB.GetRoomCombobox', function (ret) {
                if (retVal) {
                    RoomStore.loadData(ret, true);
                    var addform = Ext.getCmp("addform");
                    addform.form.setValues(retVal[0]);
                    Ext.getCmp("EarliestHour").setValue(getHourMinute(retVal[0]["EarliestDate"]));
                    Ext.getCmp("LatestHour").setValue(getHourMinute(retVal[0]["LatestDate"]));
                    Ext.getCmp("LiveStartHour").setValue(getHourMinute(retVal[0]["LiveStartDate"]));
                    Ext.getCmp("LiveEndHour").setValue(getHourMinute(retVal[0]["LiveEndDate"]));
                    var AuthorStatus = retVal[0]["AuthorStatus"];
                    var AuthorizeBookStatus = retVal[0]["AuthorizeBookStatus"];
                    var AuthorLiveStatus = retVal[0]["AuthorLiveStatus"];
                    Ext.getCmp("ActualTotalPrice").setValue(retVal[0]["LiveTotalPrice"] + retVal[0]["DepositPrice"]);
                    //if (AuthorStatus == 1) {
                    //    Ext.getCmp("sc").show();
                    //    Ext.getCmp("qx").show();
                    //    if (AuthorizeBookStatus == 1)
                    //        Ext.getCmp("qryd").show();
                    //}
                    //else if (AuthorStatus == 2) {
                    //    Ext.getCmp("cxxf").show();
                    //    Ext.getCmp("ys").show();
                    //    Ext.getCmp("xz").show();
                    //    Ext.getCmp("tf").show();
                    //    Ext.getCmp("fkxx").show();
                    //}
                    //else if (AuthorStatus == 2) {
                    //    Ext.getCmp("cxxf").show();
                    //    Ext.getCmp("ys").show();
                    //    Ext.getCmp("xz").show();
                    //    Ext.getCmp("tf").show();
                    //    Ext.getCmp("fkxx").show();
                    //}
                    //else if (AuthorStatus == 3) {
                    //    Ext.getCmp("qtsq").show();
                    //    Ext.getCmp("fwzp").show();
                    //    Ext.getCmp("fkxx").show();
                    //    if (AuthorLiveStatus == 5) {
                    //        CS('CZCLZ.AuthorizeOrderDB.CheckIsJudge', function (retVal) {
                    //            if (retVal) {
                    //                Ext.getCmp("pj").show();
                    //            }
                    //        }, CS.onError, AuthorizeNo);
                    //    }
                    //    if (AuthorLiveStatus == 6)
                    //        Ext.getCmp("tfqr").show();
                    //}
                  

                   
                }
            }, CS.onError, HotelId);



        }
    }, CS.onError, id);
}

function getHourMinute(date) {
    date = new Date(date);
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    return hour + ":" + minute;
}

function ysAndxz(v) {
    var win = new rzxxWin({ lx: v });
    win.show(null, function () {
        CS('CZCLZ.AuthorizeOrderDB.GetAuthorizeOrderById', function (retVal) {
            if (retVal) {
                var rzxxForm = Ext.getCmp("rzxxForm");
                rzxxForm.form.setValues(retVal[0]);
                if (v == 2)
                    Ext.getCmp("ApplyDay").show();
                else
                    Ext.getCmp("ApplyHour").show();
                if (retVal[0]["ParentRoomNo"] == null || retVal[0]["ParentRoomNo"] == "")
                    Ext.getCmp("ParentRoomNo").setValue(retVal[0].RoomNo);
            }
        }, CS.onError, id);
    });
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
                                title: '授权信息',
                                items: [
                                        {
                                            xtype: 'textfield',
                                            name: 'ID',
                                            margin: '10 10 10 10',
                                            fieldLabel: '主键ID',
                                            hidden: true,
                                            columnWidth: 0.5,
                                            labelWidth: 80
                                        },
                                         {
                                             xtype: 'combobox',
                                             name: 'PlatType',
                                             margin: '10 10 10 10',
                                             fieldLabel: '下单平台',
                                             allowBlank: false,
                                             columnWidth: 0.5,
                                             labelWidth: 80,
                                             queryMode: 'local',
                                             displayField: 'TEXT',
                                             valueField: 'VALUE',
                                             store: new Ext.data.ArrayStore({
                                                 fields: ['TEXT', 'VALUE'],
                                                 data: [
                                                     ['其他', 0],
                                                     ['E家智宿', 1],
                                                     ['美团', 2],
                                                     ['线下', 3]
                                                 ]
                                             }),
                                             value: 0
                                         },
                                        {
                                            xtype: 'combobox',
                                            margin: '10 10 10 10',
                                            name: 'HotelId',
                                            fieldLabel: '选择门店',
                                            allowBlank: false,
                                            editable: false,
                                            columnWidth: 0.5,
                                            labelWidth: 80,
                                            store: HotelStore,
                                            queryMode: 'local',
                                            displayField: 'TEXT',
                                            valueField: 'VALUE',
                                            value: '',
                                            listeners: {
                                                'select': function (field, val, obj) {
                                                    CS('CZCLZ.AuthorizeOrderDB.GetRoomCombobox', function (retVal) {
                                                        if (retVal) {
                                                            RoomStore.loadData(retVal, true);
                                                        }
                                                    }, CS.onError, field.value);
                                                }
                                            }
                                        },

                                          {
                                              xtype: 'combobox',
                                              margin: '10 10 10 10',
                                              id: 'RoomId',
                                              name: 'RoomId',
                                              fieldLabel: '选择房间',
                                              allowBlank: false,
                                              editable: false,
                                              columnWidth: 0.5,
                                              labelWidth: 80,
                                              store: RoomStore,
                                              queryMode: 'local',
                                              displayField: 'TEXT',
                                              valueField: 'VALUE'

                                          },
                                             {
                                                 xtype: 'combobox',
                                                 name: 'AuthorRoomStyle',
                                                 allowBlank: false,
                                                 margin: '10 10 10 10',
                                                 fieldLabel: '授权房型',
                                                 columnWidth: 0.5,
                                                 labelWidth: 80,
                                                 queryMode: 'local',
                                                 displayField: 'TEXT',
                                                 valueField: 'VALUE',
                                                 store: new Ext.data.ArrayStore({
                                                     fields: ['TEXT', 'VALUE'],
                                                     data: [
                                                         ['全天房', 1],
                                                         ['钟点房', 2],
                                                         ['看房', 3]
                                                     ]
                                                 }),
                                                 value: 1,
                                                 listeners: {
                                                     'select': function (field, val, obj) {
                                                         if (field.value == 1) {
                                                             Ext.getCmp("LiveDays").show();
                                                             Ext.getCmp("LiveHour").hide();

                                                         }
                                                         else if (field.value == 2) {
                                                             Ext.getCmp("LiveDays").hide();
                                                             Ext.getCmp("LiveHour").show();
                                                         }
                                                     }
                                                 }
                                             },
                                          {
                                              xtype: 'textfield',
                                              name: 'RealName',
                                              allowBlank: false,
                                              margin: '10 10 10 10',
                                              fieldLabel: '姓名',
                                              columnWidth: 0.5,
                                              labelWidth: 80

                                          },

                                         {
                                             xtype: 'textfield',
                                             name: 'CellPhone',
                                             allowBlank: false,
                                             margin: '10 10 10 10',
                                             fieldLabel: '手机号码',
                                             columnWidth: 0.5,
                                             labelWidth: 80

                                         },
                                          {
                                              xtype: 'datefield',
                                              name: 'EarliestDate',
                                              allowBlank: false,
                                              format: 'Y-m-d',
                                              margin: '10 10 10 10',
                                              fieldLabel: '最早到店时间',
                                              columnWidth: 0.25,
                                              labelWidth: 80

                                          },
                                             {
                                                 xtype: 'timefield',
                                                 id: 'EarliestHour',
                                                 name: 'EarliestHour',
                                                 allowBlank: false,
                                                 format: 'H:i',
                                                 increment: 1,
                                                 margin: '10 10 10 0',
                                                 columnWidth: 0.25,
                                                 value: '0'

                                             },
                                              {
                                                  xtype: 'datefield',
                                                  allowBlank: false,
                                                  name: 'LatestDate',
                                                  format: 'Y-m-d',
                                                  margin: '10 10 10 10',
                                                  fieldLabel: '最早到店时间',
                                                  columnWidth: 0.25,
                                                  labelWidth: 80

                                              },
                                             {
                                                 xtype: 'timefield',
                                                 id: 'LatestHour',
                                                 name: 'LatestHour',
                                                 allowBlank: false,
                                                 format: 'H:i',
                                                 increment: 1,
                                                 margin: '10 10 10 0',
                                                 columnWidth: 0.25,
                                                 value: '0'

                                             },
                                          {
                                              xtype: 'datefield',
                                              name: 'LiveStartDate',
                                              allowBlank: false,
                                              format: 'Y-m-d',
                                              margin: '10 10 10 10',
                                              fieldLabel: '入住时间',
                                              columnWidth: 0.25,
                                              labelWidth: 80

                                          },
                                          {
                                              xtype: 'timefield',
                                              id: 'LiveStartHour',
                                              name: 'LiveStartHour',
                                              allowBlank: false,
                                              format: 'H:i',
                                              increment: 1,
                                              margin: '10 10 10 0',
                                              columnWidth: 0.25,
                                              value: '0'

                                          },

                                           {
                                               xtype: 'numberfield',
                                               id: 'LiveDays',
                                               name: 'LiveDays',
                                               margin: '10 10 10 10',
                                               fieldLabel: '入住天数',
                                               columnWidth: 0.5,
                                               labelWidth: 80

                                           },
                                           {
                                               xtype: 'combobox',
                                               id: 'LiveHour',
                                               name: 'LiveHour',
                                               hidden: true,
                                               margin: '10 10 10 10',
                                               fieldLabel: '钟点房时长',
                                               columnWidth: 0.5,
                                               labelWidth: 80,
                                               queryMode: 'local',
                                               displayField: 'TEXT',
                                               valueField: 'VALUE',
                                               store: new Ext.data.ArrayStore({
                                                   fields: ['TEXT', 'VALUE'],
                                                   data: [
                                                       ['3小时', 3],
                                                       ['4小时', 4],
                                                       ['5小时', 5]
                                                   ]
                                               })
                                           },
                                              {
                                                  xtype: 'datefield',
                                                  name: 'LiveEndDate',
                                                  allowBlank: false,
                                                  format: 'Y-m-d',
                                                  margin: '10 10 10 10',
                                                  fieldLabel: '退房时间',
                                                  columnWidth: 0.25,
                                                  labelWidth: 80

                                              },
                                          {
                                              xtype: 'timefield',
                                              id: 'LiveEndHour',
                                              name: 'LiveEndHour',
                                              allowBlank: false,
                                              format: 'H:i',
                                              increment: 1,
                                              margin: '10 10 10 0',
                                              columnWidth: 0.25,
                                              value: '0'

                                          },


                                                {
                                                    xtype: 'combobox',
                                                    margin: '10 10 10 10',
                                                    name: 'TakepowerType',
                                                    fieldLabel: '取电方式',
                                                    allowBlank: false,
                                                    columnWidth: 0.5,
                                                    labelWidth: 80,
                                                    queryMode: 'local',
                                                    displayField: 'TEXT',
                                                    valueField: 'VALUE',
                                                    store: new Ext.data.ArrayStore({
                                                        fields: ['TEXT', 'VALUE'],
                                                        data: [
                                                            ['身份证', 1],
                                                            ['无条件取电', 2]
                                                        ]
                                                    }),
                                                    value: 1
                                                },
                                                 {
                                                     xtype: 'combobox',
                                                     margin: '10 10 10 10',
                                                     name: 'UnlockType',
                                                     fieldLabel: '开锁方式',
                                                     allowBlank: false,
                                                     columnWidth: 0.5,
                                                     labelWidth: 80,
                                                     queryMode: 'local',
                                                     displayField: 'TEXT',
                                                     valueField: 'VALUE',
                                                     store: new Ext.data.ArrayStore({
                                                         fields: ['TEXT', 'VALUE'],
                                                         data: [
                                                             ['身份证', 1],
                                                             ['组合', 2]
                                                         ]
                                                     }),
                                                     value: 1
                                                 },
                                                   {
                                                       xtype: 'numberfield',
                                                       name: 'UnitPrice',
                                                       margin: '10 10 10 10',
                                                       fieldLabel: '房费(单价)',
                                                       allowBlank: false,
                                                       columnWidth: 0.5,
                                                       labelWidth: 80

                                                   },
                                                     {
                                                         xtype: 'numberfield',
                                                         name: 'LiveTotalPrice',
                                                         margin: '10 10 10 10',
                                                         fieldLabel: '房费(总价)',
                                                         allowBlank: false,
                                                         columnWidth: 0.5,
                                                         labelWidth: 80

                                                     },
                                                       {
                                                           xtype: 'numberfield',
                                                           name: 'DepositPrice',
                                                           margin: '10 10 10 10',
                                                           allowBlank: false,
                                                           fieldLabel: '押金',
                                                           columnWidth: 0.5,
                                                           labelWidth: 80

                                                       },
                                                         {
                                                             xtype: 'numberfield',
                                                             id: 'ActualTotalPrice',
                                                             name: 'ActualTotalPrice',
                                                             allowBlank: false,
                                                             margin: '10 10 10 10',
                                                             fieldLabel: '合计',
                                                             columnWidth: 0.5,
                                                             labelWidth: 80

                                                         }
                                ]

                            }
                        ],
                        buttonAlign: 'center',
                        buttons: [
  
                            {
                                text: '返回',
                                handler: function () {
                                    FrameStack.popFrame();
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

    CS('CZCLZ.AuthorizeOrderDB.GetHotelCombobox', function (retVal) {
        if (retVal) {
            HotelStore.loadData(retVal, true);
            if (id != null && id != "") {

                DataBind();

            }
            else {
                Ext.getCmp("bc").show();
            }
        }
    }, CS.onError);
});