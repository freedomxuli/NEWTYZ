
function DataBind() {
    CS('JSSF.SFXM.GetXMXG', function (retVal) {
        if (retVal) {


        }
    }, CS.onError, XM_ID);
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
                        xtype: 'form',
                        id: 'addform',
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
                                         name: 'YJSX',
                                         margin: '10 10 10 10',
                                         fieldLabel: '佣金比例上限',
                                         allowBlank: false,
                                         columnWidth: 0.5,
                                         labelWidth: 150
                                     },
                                    {
                                        xtype: 'textfield',
                                        name: 'YHXX',
                                        margin: '10 10 10 10',
                                        fieldLabel: '下限',
                                        columnWidth: 0.5,
                                        allowBlank: false,
                                        labelWidth: 150
                                    },
                                     {
                                         xtype: 'textfield',
                                         name: 'JSZQSX',
                                         margin: '10 10 10 10',
                                         fieldLabel: '平台结算周期上限',
                                         columnWidth: 0.5,
                                         allowBlank: false,
                                         labelWidth: 150
                                     },
                                      {
                                          xtype: 'textfield',
                                          name: 'JSZQXX',
                                          margin: '10 10 10 10',
                                          fieldLabel: '下限',
                                          columnWidth: 0.5,
                                          allowBlank: false,
                                          labelWidth: 150
                                      },
                                       {
                                           xtype: 'textfield',
                                           name: 'JFJRSX',
                                           margin: '10 10 10 10',
                                           fieldLabel: '纠纷介入时限',
                                           columnWidth: 0.5,
                                           allowBlank: false,
                                           labelWidth: 150
                                       },
                                        {
                                            xtype: 'textfield',
                                            name: 'TSCLSX',
                                            margin: '10 10 10 10',
                                            fieldLabel: '投诉处理时限',
                                            columnWidth: 0.5,
                                            allowBlank: false,
                                            labelWidth: 150
                                        },
                                         {
                                             xtype: 'textfield',
                                             name: 'TCJSSX',
                                             margin: '10 10 10 10',
                                             fieldLabel: '提成基数权上限',
                                             columnWidth: 0.5,
                                             allowBlank: false,
                                             labelWidth: 150
                                         },
                                          {
                                              xtype: 'textfield',
                                              name: 'TCJSXX',
                                              margin: '10 10 10 10',
                                              fieldLabel: '下限',
                                              columnWidth: 0.5,
                                              allowBlank: false,
                                              labelWidth: 150
                                          },
                                           {
                                               xtype: 'textfield',
                                               name: 'YYEZSJQ',
                                               margin: '10 10 10 10',
                                               fieldLabel: '营业额增速加权',
                                               columnWidth: 0.5,
                                               allowBlank: false,
                                               labelWidth: 150
                                           },
                                            {
                                                xtype: 'textfield',
                                                name: 'TSCLLJQ',
                                                margin: '10 10 10 10',
                                                fieldLabel: '投诉处理率加权',
                                                columnWidth: 0.5,
                                                allowBlank: false,
                                                labelWidth: 150
                                            },
                                             {
                                                 xtype: 'textfield',
                                                 name: 'JFCLLJQ',
                                                 margin: '10 10 10 10',
                                                 fieldLabel: '纠纷处理率加权',
                                                 columnWidth: 0.5,
                                                 allowBlank: false,
                                                 labelWidth: 150
                                             },
                                             {
                                                 xtype: 'textfield',
                                                 name: 'XZFWSJQ',
                                                 margin: '10 10 10 10',
                                                 fieldLabel: '新增房屋数加权',
                                                 columnWidth: 0.5,
                                                 allowBlank: false,
                                                 labelWidth: 150
                                             },
                                              {
                                                  xtype: 'textfield',
                                                  name: 'JSJQ',
                                                  margin: '10 10 10 10',
                                                  fieldLabel: '角色加权',
                                                  columnWidth: 0.5,
                                                  allowBlank: false,
                                                  labelWidth: 150
                                              },
                                               {
                                                   xtype: 'displayfield',
                                                   margin: '10 10 10 10',
                                                   fieldLabel: '计算方法',
                                                   columnWidth: 1,
                                                   allowBlank: false,
                                                   labelWidth: 150,
                                                   value: '营业额*基数权*角色加权*按营业额增速加权*投诉处理率加权*纠纷处理率加权*新增房屋数加权'
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


                                        CS('CZCLZ.News.SaveRules', function (retVal) {
                                            if (retVal) {
                                                Ext.Msg.show({
                                                    title: '提示',
                                                    msg: '保存成功',
                                                    buttons: Ext.MessageBox.OK,
                                                    icon: Ext.MessageBox.INFO
                                                });
                                            }
                                        }, CS.onError, values);

                                    }
                                }
                            }
                            //{
                            //    text: '返回',
                            //    iconCls: 'back',
                            //    handler: function () {
                            //        FrameStack.popFrame();
                            //    }
                            //}
                        ]
                    }
                ]
            });

            me.callParent(arguments);
        }

    });

    new add();

    CS('CZCLZ.News.GetRules', function (retVal) {
        if (retVal) {
            var addform = Ext.getCmp('addform');
            addform.form.setValues(retVal[0]);
        }
    }, CS.onError);
})

