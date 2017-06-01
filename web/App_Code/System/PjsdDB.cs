using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartFramework4v2.Web.WebExcutor;
using SmartFramework4v2.Data;
using SmartFramework4v2.Data.SqlServer;
using System.Collections;
using System.Data;
using SmartFramework4v2.Web.Common.JSON;
using System.Data.SqlClient;

/// <summary>
///PjsdDB 的摘要说明
/// </summary>
[CSClass("PjsdDBClass")]
public class PjsdDB
{
    public PjsdDB()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    [CSMethod("SavePsjd")]
    public object SavePsjd(JSReader jsr, JSReader fj1, JSReader fj2, JSReader fj3, JSReader fj4, JSReader fj5)
    {
        var user = SystemUser.CurrentUser;
        string userid = user.UserID;
        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                int dwcount = Convert.ToInt16(dbc.ExecuteScalar("select count(*) from tb_b_pjsd where delflag=0").ToString());
                if (dwcount >= 89)
                {
                    throw new Exception("平价店数量已达到89家，不能添加");
                }


                string PJSD_ID = jsr["PJSD_ID"].ToString();
                string QY_ID = jsr["QY_ID"].ToString();
                string PJSD_MC = jsr["PJSD_MC"].ToString();
                string PJSD_DZ = jsr["PJSD_DZ"].ToString();
                int PJSD_LX = jsr["PJSD_LX"].ToInteger();
                string PJSD_FR = jsr["PJSD_FR"].ToString();
                string PJSD_FRDH = jsr["PJSD_FRDH"].ToString();
                string PJSD_LXR = jsr["PJSD_LXR"].ToString();
                string PJSD_LXRDH = jsr["PJSD_LXRDH"].ToString();
                string PJSD_LXRSJ = jsr["PJSD_LXRSJ"].ToString();
                string PJSD_NO = jsr["PJSD_NO"].ToString();
                string PJSD_PEOPLE = jsr["PJSD_PEOPLE"].ToString();
                string PJSD_PHONE = jsr["PJSD_PHONE"].ToString();

                string PJSD_TBYY = jsr["PJSD_TBYY"].ToString();


                JSReader[] fjList1 = fj1.ToArray();
                JSReader[] fjList2 = fj2.ToArray();
                JSReader[] fjList3 = fj3.ToArray();
                JSReader[] fjList4 = fj4.ToArray();
                JSReader[] fjList5 = fj5.ToArray();

                //List<string> listArr = new List<string>();
                //listArr.Add(fj1);
                //listArr.Add(fj2);
                //listArr.Add(fj3);
                //listArr.Add(fj4);
                //listArr.Add(fj5);

                // string[] fj = listArr.ToArray();


                var PJSD_ISLS = jsr["PJSD_ISLS"];
                var PJSD_Enable = jsr["PJSD_Enable"];
                //纬度
                var PJSD_Latitude = jsr["PJSD_Latitude"];
                //经度
                var PJSD_Longitude = jsr["PJSD_Longitude"];
                //上报类型
                var SBType_id = jsr["PJSD_SBType"];
                //模式ID
                string ms_id = jsr["MS_ID"].ToString();
                Pjsd.tb_b_PJSDDataTable hh = new Pjsd.tb_b_PJSDDataTable();
                var sr = hh.Newtb_b_PJSDRow();
                sr.QY_ID = new Guid(QY_ID);
                sr.PJSD_MC = PJSD_MC;
                sr.PJSD_DZ = PJSD_DZ;
                sr.PJSD_LX = PJSD_LX;
                sr.PJSD_FR = PJSD_FR;
                sr.PJSD_FRDH = PJSD_FRDH;
                sr.PJSD_LXR = PJSD_LXR;
                sr.PJSD_LXRDH = PJSD_LXRDH;
                sr.PJSD_LXRSJ = PJSD_LXRSJ;
                sr.PJSD_NO = PJSD_NO;
                sr.PJSD_Latitude = PJSD_Latitude;
                sr.PJSD_Longitude = PJSD_Longitude;
                sr.PJSD_PEOPLE = PJSD_PEOPLE;
                sr.PJSD_PHONE = PJSD_PHONE;

                if (PJSD_ISLS)
                {
                    sr.PJSD_ISLS = 1;
                }
                else
                {
                    sr.PJSD_ISLS = 0;
                }

                if (PJSD_Enable)
                {
                    sr.PJSD_Enable = true;
                    sr.PJSD_TBRQ = DateTime.Now;
                    sr.PJSD_TBYY = PJSD_TBYY;

                    Pjsd.tb_b_PJSD_TBJLDataTable hh_jl = new Pjsd.tb_b_PJSD_TBJLDataTable();
                    Pjsd.tb_b_PJSD_TBJLRow hr = hh_jl.Newtb_b_PJSD_TBJLRow();
                    hr.TB_ID = Guid.NewGuid();
                    hr.DW_ID = new Guid(PJSD_ID);
                    hr.TB_GSSJ = DateTime.Now;
                    hr.TB_BZ = PJSD_TBYY;
                    hh_jl.Rows.Add(hr);
                    dbc.InsertTable(hh_jl);
                }
                else
                {
                    sr.PJSD_Enable = false;


                    sr.PJSD_TBYY = "";
                }

                sr.delflag = false;
                sr.addtime = DateTime.Now;
                sr.updatetime = sr.addtime;
                sr.updateuser = userid;
                sr.MS_ID = new Guid(ms_id);


                if (SBType_id != "")
                {
                    sr.PJSD_SBType = new Guid(SBType_id);
                }

                if (PJSD_ID != "")
                {
                    sr.PJSD_ID = new Guid(PJSD_ID);
                    dbc.UpdateTable(sr);
                }
                else
                {
                    PJSD_ID = Guid.NewGuid().ToString();
                    sr.PJSD_ID = new Guid(PJSD_ID);
                    dbc.InsertTable(sr);
                }


                if (fjList1.Length > 0)
                {
                    string list = "";
                    //for (int i = 0; i < fjList1.Length; i++)
                    //{
                    //    if (i > 0)
                    //    {
                    //        list += "," + "'" + fjList1[i] + "'";
                    //    }
                    //    else
                    //    {
                    //        list = "'" + fjList1[i] + "'";
                    //    }
                    //}
                    //将附件记录关联ID全部设置为初始GUID
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=1";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                    //将附件记录重新关联
                    for (int i = 0; i < fjList1.Length; i++)
                    {
                        //var sqlStrGL2 = "insert tb_b_GSandFILE set PJSDID='" + PJSD_ID + "' where attachID in (" + list + ")";
                        var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES ('" + Guid.NewGuid().ToString() + "','" + PJSD_ID + "','" + fjList1[i] + "',1)";

                        //SqlCommand cmd = new SqlCommand(sqlStrGL2);
                        //cmd.Parameters.Add("@glid",Guid.NewGuid());
                        //cmd.Parameters.Add("@PJSDID", PJSD_ID);
                        //cmd.Parameters.Add("@attachID", fjList1[i]);
                        //cmd.Parameters.Add("@type", 1);
                        dbc.ExecuteNonQuery(sqlStrGL2);
                    }


                }
                else
                {
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=1";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                }
                if (fjList2.Length > 0)
                {
                    string list = "";
                    //for (int i = 0; i < fjList2.Length; i++)
                    //{
                    //    if (i > 0)
                    //    {
                    //        list += "," + "'" + fjList2[i] + "'";
                    //    }
                    //    else
                    //    {
                    //        list = "'" + fjList2[i] + "'";
                    //    }
                    //}
                    //将附件记录关联ID全部设置为初始GUID
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=2";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                    //将附件记录重新关联

                    for (int i = 0; i < fjList2.Length; i++)
                    {
                        //var sqlStrGL2 = "insert tb_b_GSandFILE set PJSDID='" + PJSD_ID + "' where attachID in (" + list + ")";
                        // var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES (@glid,@PJSDID,@attachID,@type)";
                        var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES ('" + Guid.NewGuid().ToString() + "','" + PJSD_ID + "','" + fjList2[i] + "',2)";

                        //SqlCommand cmd = new SqlCommand(sqlStrGL2);
                        //cmd.Parameters.Add("@glid", Guid.NewGuid().ToString());
                        //cmd.Parameters.Add("@PJSDID", PJSD_ID);
                        //cmd.Parameters.Add("@attachID", fjList2[i]);
                        //cmd.Parameters.Add("@type", 2);
                        dbc.ExecuteNonQuery(sqlStrGL2);
                    }

                }
                else
                {
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=2";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                }
                if (fjList3.Length > 0)
                {
                    string list = "";
                    //for (int i = 0; i < fjList3.Length; i++)
                    //{
                    //    if (i > 0)
                    //    {
                    //        list += "," + "'" + fjList3[i] + "'";
                    //    }
                    //    else
                    //    {
                    //        list = "'" + fjList3[i] + "'";
                    //    }
                    //}
                    //将附件记录关联ID全部设置为初始GUID
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=3";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                    //将附件记录重新关联

                    for (int i = 0; i < fjList3.Length; i++)
                    {
                        //var sqlStrGL2 = "insert tb_b_GSandFILE set PJSDID='" + PJSD_ID + "' where attachID in (" + list + ")";
                        //var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES (@glid,@PJSDID,@attachID,@type)";
                        //SqlCommand cmd = new SqlCommand(sqlStrGL2);
                        //cmd.Parameters.Add("@glid", Guid.NewGuid());
                        //cmd.Parameters.Add("@PJSDID", PJSD_ID);
                        //cmd.Parameters.Add("@attachID", fjList3[i]);
                        //cmd.Parameters.Add("@type", 3);
                        var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES ('" + Guid.NewGuid().ToString() + "','" + PJSD_ID + "','" + fjList3[i] + "',3)";

                        dbc.ExecuteNonQuery(sqlStrGL2);
                    }

                }
                else
                {
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=3";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                }
                if (fjList4.Length > 0)
                {
                    //string list = "";
                    //for (int i = 0; i < fjList4.Length; i++)
                    //{
                    //    if (i > 0)
                    //    {
                    //        list += "," + "'" + fjList4[i] + "'";
                    //    }
                    //    else
                    //    {
                    //        list = "'" + fjList4[i] + "'";
                    //    }
                    //}
                    //将附件记录关联ID全部设置为初始GUID
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=4";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                    //将附件记录重新关联


                    for (int i = 0; i < fjList4.Length; i++)
                    {
                        //var sqlStrGL2 = "insert tb_b_GSandFILE set PJSDID='" + PJSD_ID + "' where attachID in (" + list + ")";
                        //var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES (@glid,@PJSDID,@attachID,@type)";
                        //SqlCommand cmd = new SqlCommand(sqlStrGL2);
                        //cmd.Parameters.Add("@glid", Guid.NewGuid());
                        //cmd.Parameters.Add("@PJSDID", PJSD_ID);
                        //cmd.Parameters.Add("@attachID", fjList4[i]);
                        //cmd.Parameters.Add("@type", 4);
                        var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES ('" + Guid.NewGuid().ToString() + "','" + PJSD_ID + "','" + fjList4[i] + "',4)";

                        dbc.ExecuteNonQuery(sqlStrGL2);

                    }

                }
                else
                {
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=4";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                }
                if (fjList5.Length > 0)
                {
                    string list = "";
                    //for (int i = 0; i < fjList5.Length; i++)
                    //{
                    //    if (i > 0)
                    //    {
                    //        list += "," + "'" + fjList5[i] + "'";
                    //    }
                    //    else
                    //    {
                    //        list = "'" + fjList5[i] + "'";
                    //    }
                    //}
                    //将附件记录关联ID全部设置为初始GUID
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=5";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                    //将附件记录重新关联


                    for (int i = 0; i < fjList5.Length; i++)
                    {
                        //var sqlStrGL2 = "insert tb_b_GSandFILE set PJSDID='" + PJSD_ID + "' where attachID in (" + list + ")";
                        //var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES (@glid,@PJSDID,@attachID,@type)";
                        //SqlCommand cmd = new SqlCommand(sqlStrGL2);
                        //cmd.Parameters.Add("@glid", Guid.NewGuid());
                        //cmd.Parameters.Add("@PJSDID", PJSD_ID);
                        //cmd.Parameters.Add("@attachID", fjList5[i]);
                        //cmd.Parameters.Add("@type", 5);
                        var sqlStrGL2 = @"INSERT INTO  tb_b_GSandFILE (glid,PJSDID,attachID,type) VALUES ('" + Guid.NewGuid().ToString() + "','" + PJSD_ID + "','" + fjList5[i] + "',5)";
                        dbc.ExecuteNonQuery(sqlStrGL2);
                    }

                }
                else
                {
                    var sqlStrGL = "delete from  tb_b_GSandFILE where PJSDID=@PJSDID2 and type=5";
                    SqlCommand cmdGL = new SqlCommand(sqlStrGL);
                    cmdGL.Parameters.AddWithValue("@PJSDID2", PJSD_ID);
                    dbc.ExecuteNonQuery(cmdGL);
                }


                //if (fj.Length > 0)
                //{
                //    for (int i = 0; i < fj.Length; i++)
                //    {
                //        if (fj[i] != null && fj[i].ToString() != "")
                //        {
                //            var sqlStrGL = "update tb_b_GSandFILE set PJSDID=@PJSDID where glid=@glid";
                //            SqlCommand cmdGL = new SqlCommand(sqlStrGL);

                //            cmdGL.Parameters.AddWithValue("@PJSDID", PJSD_ID);
                //            cmdGL.Parameters.AddWithValue("@glid", fj[i]);
                //            dbc.ExecuteNonQuery(cmdGL);
                //        }
                //    }

                //}



                dbc.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }

        }
    }

    [CSMethod("GetPjsdList")]
    public object GetPjsdList(string qyid, string pjsdmc)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {


                string str = "select a.*,c.QY_Name,b.ms_name from tb_b_PJSD a left join tb_b_pjsd_ms b on a.MS_ID=b.MS_ID left join tb_b_Eare c on a.QY_ID=c.QY_ID where a.delflag=0 ";
                if (string.IsNullOrEmpty(SystemUser.CurrentUser.QY_ID))
                {
                    if (!string.IsNullOrEmpty(qyid))
                    {
                        if (qyid.Equals("sq"))
                            str += " and a.QY_ID in(select QY_ID from tb_b_Eare where QY_CODE in(" + System.Configuration.ConfigurationManager.AppSettings["SQ_Code"] + "))";
                        else
                            str += " and a.QY_ID='" + qyid + "'";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(qyid))
                    {
                        str += " and a.QY_ID='" + qyid + "'";
                    }
                    else
                    {
                        str += " and a.QY_ID='" + SystemUser.CurrentUser.QY_ID + "'";
                    }
                }
                if (!string.IsNullOrEmpty(pjsdmc))
                {
                    str += " and (a.PJSD_MC like '%" + pjsdmc.Trim() + "%' or PJSD_NO like '%" + pjsdmc.Trim() + "%' collate Chinese_PRC_CS_AS )";
                }
                str += " order by a.PJSD_NO";
                DataTable dt = dbc.ExecuteDataTable(str);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetPjsdByQY")]
    public object GetPjsdByQY(string qyid, string pjsdmc)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {


                string str = "select a.*,c.QY_Name,b.ms_name from tb_b_PJSD a left join tb_b_pjsd_ms b on a.MS_ID=b.MS_ID left join tb_b_Eare c on a.QY_ID=c.QY_ID where a.delflag=0 ";

                if (!string.IsNullOrEmpty(qyid))
                {
                    str += " and a.QY_ID='" + qyid + "'";
                }
              

                if (!string.IsNullOrEmpty(pjsdmc))
                {
                    str += " and (a.PJSD_MC like '%" + pjsdmc.Trim() + "%' or PJSD_NO like '%" + pjsdmc.Trim() + "%' collate Chinese_PRC_CS_AS )";
                }
                str += " order by a.PJSD_NO";
                DataTable dt = dbc.ExecuteDataTable(str);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [CSMethod("GetPjsdListTwo")]
    public object GetPjsdListTwo()
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                var userid = SystemUser.CurrentUser.UserID;

                string str = "select * from tb_b_PJSD where PJSD_ID in (select DW_ID from tb_b_User_Dw_Gl where USER_ID='" + userid + "' and delflag=0) ";

                str += " order by PJSD_NO";
                DataTable dt = dbc.ExecuteDataTable(str);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    [CSMethod("DeletePro")]
    public object DeletePro(JSReader jsr)
    {
        var user = SystemUser.CurrentUser;
        string userid = user.UserID;

        using (DBConnection dbc = new DBConnection())
        {
            dbc.BeginTransaction();
            try
            {
                for (int i = 0; i < jsr.ToArray().Length; i++)
                {
                    string str = "update tb_b_PJSD set delflag=1,updatetime=@updatetime,updateuser=@updateuser where PJSD_ID=@PJSD_ID";
                    SqlCommand cmd = new SqlCommand(str);
                    cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@updateuser", userid);
                    cmd.Parameters.AddWithValue("@PJSD_ID", jsr.ToArray()[i].ToString());
                    dbc.ExecuteNonQuery(cmd);

                }

                dbc.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    [CSMethod("GetQy")]
    public object GetQy()
    {
        using (DBConnection dbc = new DBConnection())
        {
            string str = "select QY_ID VALUE,QY_Name TEXT from tb_b_Eare  order by QY_PX";
            DataTable dt = dbc.ExecuteDataTable(str);
            return dt;
        }
    }

    [CSMethod("GetPjd")]
    public object GetPjd(string qyid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            string str = "select PJD_ID VALUE,PJD_Name TEXT from tb_b_PJD where delflag=0 and qy_id='" + qyid + "' order by PJD_PX,PJD_Name";
            DataTable dt = dbc.ExecuteDataTable(str);
            return dt;
        }
    }

    [CSMethod("SaveCCGL")]
    public bool SaveCCGL(string pjsdid, string mnstr)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                dbc.BeginTransaction();
                var nmsc = mnstr.Split(',');
                var delstr = "delete from dbo.tb_b_PJNMGL where pjsd_id='" + pjsdid + "'";
                dbc.ExecuteNonQuery(delstr);
                var gldt = new Pjsd.tb_b_PJNMGLDataTable();
                for (int i = 0; i < nmsc.Length; i++)
                {
                    if (nmsc[i].Length > 30)
                    {
                        var dr = gldt.Newtb_b_PJNMGLRow();
                        dr.PJSD_ID = new Guid(pjsdid);
                        dr.NMSC_ID = new Guid(nmsc[i]);
                        dbc.InsertTable(dr);
                    }
                }
                dbc.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                dbc.RoolbackTransaction();
                throw ex;
            }
        }
    }

    [CSMethod("GetNMSC")]
    public DataTable GetNMSC(string pjsdid, string con)
    {
        using (DBConnection dbc = new DBConnection())
        {
            string str = " select tb_b_DW.*,tb_b_Eare.QY_Name,case when DW_ID in (select NMSC_ID from dbo.tb_b_PJNMGL where PJSD_ID='" + pjsdid + "') then 1 else 0 end BZ from dbo.tb_b_DW left join tb_b_Eare on tb_b_DW.QY_ID=tb_b_Eare.QY_ID where DW_LX=4 " + con;
            return dbc.ExecuteDataTable(str);
        }
    }

    [CSMethod("GetPJSD_MS")]
    public DataTable GetPJSD_MS()
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select ms_id VALUE,ms_name TEXT from tb_b_pjsd_ms WHERE STATUS=0 order by ms_orderNo ";
                return dbc.ExecuteDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    [CSMethod("GetPJSD_TBJL")]
    public object GetPJSD_TBJL(string pjsdid)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select a.*,b.PJSD_MC from tb_b_PJSD_TBJL a left join tb_b_PJSD b on a.dw_id=b.pjsd_id where a.dw_id='" + pjsdid + "' order by a.TB_GSSJ desc";
                DataTable dt = dbc.ExecuteDataTable(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
