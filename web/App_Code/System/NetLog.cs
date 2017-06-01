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
using System.IO;
using Aspose.Cells;
using System.Data.SqlClient;

/// <summary>
///NetLog 的摘要说明
/// </summary>
[CSClass("NetLogDBClass")]
public class NetLog
{
    public NetLog()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    [CSMethod("GetNetLog")]
    public object GetNetLog(string qyid, string pjsdmc, DateTime date, string time)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = date.ToString("yyyy-MM-dd");
                DateTime st = Convert.ToDateTime(rq + " " + time).AddHours(-1);
                DateTime st1 = Convert.ToDateTime(rq + " " + time);
                //string sql = "select a.pjsd_id,a.pjsd_mc,(select top 1 addtime from tb_P_POSLOG  where PJSD_ID=a.pjsd_id and addtime<@st1 order by addtime desc) addtime,"
                //    + " (case when (select top 1 addtime  from tb_P_POSLOG where PJSD_ID=a.pjsd_id order by addtime desc)<@st then 1"
                //    + " when (select top 1 addtime from tb_P_POSLOG where pjsd_id=a.pjsd_id order by addtime desc) is null"
                //    + " then 0 else 0 end ) cc,b.QY_NAME from tb_b_PJSD a left join tb_b_Eare b on a.QY_ID=b.QY_ID where a.PJSD_Enable=0 and a.delflag=0 ";

                string sql = "select m.QY_NAME,m.PJSD_ID,m.PJSD_MC,m.addtime,(case when m.tt<@st1 then 1 when m.tt is null then 0 else 0 end) cc  from"
                        + " (select a.pjsd_id,a.pjsd_mc,a.PJSD_NO,b.addtime,c.tt,d.QY_NAME,d.QY_CODE,d.QY_PX from tb_b_PJSD a left join ("
                        + " select PJSD_ID,MAX(addtime) addtime from tb_P_POSLOG  where  addtime<@st"
                        + " group by  PJSD_ID) b on a.PJSD_ID=b.PJSD_ID"
                        + " left join"
                        + " (select PJSD_ID,MAX(addtime) tt from tb_P_POSLOG"
                        + " group by  PJSD_ID) c on a.PJSD_ID=c.PJSD_ID"
                        + " left join tb_b_Eare d on a.QY_ID=d.QY_ID"
                        + " where a.PJSD_Enable=0 and a.delflag=0";

                if (string.IsNullOrEmpty(SystemUser.CurrentUser.QY_ID))
                {
                    if (!string.IsNullOrEmpty(qyid))
                    {
                        if (qyid.Equals("sq"))
                            sql += " and a.QY_ID in(select QY_ID from tb_b_Eare where QY_CODE in(" + System.Configuration.ConfigurationManager.AppSettings["SQ_Code"] + "))";
                        else
                            sql += " and a.QY_ID='" + qyid + "'";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(qyid))
                    {
                        sql += " and a.QY_ID='" + qyid + "'";
                    }
                    else
                    {
                        sql += " and a.QY_ID='" + SystemUser.CurrentUser.QY_ID + "'";
                    }
                }

                if (pjsdmc != null && pjsdmc.Trim() != "")
                {
                    sql += " and a.pjsd_mc like '%" + pjsdmc.Trim() + "%'";
                }
                sql += ") m order by QY_PX,PJSD_NO";
                SqlCommand ocmd = new SqlCommand(sql);
                ocmd.Parameters.AddWithValue("@st", st);
                ocmd.Parameters.AddWithValue("@st1", st1);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    [CSMethod("GetNetLogMX")]
    public object GetNetLogMX(string dwid, string date, string time)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "select * from tb_P_POSLOG where  PJSD_ID='" + dwid.Trim() + "' and addtime>='" + rq + "' and addtime<'" + st + "'  order by addtime desc";
                SqlCommand ocmd = new SqlCommand(sql);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    /// <summary>
    ///导出所有在日期范围内的商品进行价格比对（搜索主表：预统计表）
    /// </summary>
    [CSMethod("GetDate", 2)]
    public byte[] GetDate(string qyid, string pjsdmc, DateTime date, string time, string title)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = date.ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");

                string sql = @"select m.QY_NAME,m.PJSD_ID,m.PJSD_MC,m.addtime  
                from 
                (
	                select a.pjsd_id,a.pjsd_mc,a.PJSD_NO,b.addtime,d.QY_NAME,d.QY_CODE,d.QY_PX from tb_b_PJSD a 
	                left join ( select PJSD_ID,MAX(addtime) addtime from tb_P_POSLOG  where  addtype=1 and addtime>='" + rq + @"' and addtime<'" + st + @"' group by  PJSD_ID) b on a.PJSD_ID=b.PJSD_ID 
	                left join tb_b_Eare d on a.QY_ID=d.QY_ID where a.PJSD_Enable=0 and a.delflag=0";
                if (qyid != null && qyid.Trim() != "0")
                {
                    sql += " and a.QY_ID='" + qyid + "'";
                }
                if (pjsdmc != null && pjsdmc.Trim() != "")
                {
                    sql += " and a.pjsd_mc like '%" + pjsdmc.Trim() + "%'";
                }
                sql += ") m order by QY_PX,PJSD_NO";
                DataTable dt = dbc.ExecuteDataTable(sql);

                dt.Columns.Remove("pjsd_id");

                dt.Columns["pjsd_mc"].Caption = "平价商店名称";
                dt.Columns["addtime"].Caption = "最近连接时间";
                dt.Columns["QY_NAME"].Caption = "区域";

                MemoryStream ms = OutFileToStreamSpjgbd(dt, title, rq + " " + time);
                return ms.ToArray();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    [CSMethod("GetDzxspLog")]
    public object GetDzxspLog(string qyid, string pjsdmc, DateTime date, string time)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = date.ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");
                string sql_set = "select DZXSPSJ_START,DZXSPSJ_END from tb_b_JGPT_SHSZ where STATUS = 0";
                DataTable dt_set = dbc.ExecuteDataTable(sql_set);
                string start_time = dt_set.Rows[0]["DZXSPSJ_START"].ToString();
                string end_time = dt_set.Rows[0]["DZXSPSJ_END"].ToString();
                string sql = @"select m.QY_NAME,m.PJSD_ID,m.PJSD_MC,m.addtime,m.sumtime
                from 
                (
	                select a.pjsd_id,a.pjsd_mc,a.PJSD_NO,b.addtime,c.sumtime,d.QY_NAME,d.QY_CODE,d.QY_PX from tb_b_PJSD a 
	                left join ( select PJSD_ID,MAX(updatetime) addtime from tb_u_DZXSPJK  where  addtype=1 and updatetime>='" + rq + @"' and updatetime<'" + st + @"' group by  PJSD_ID) b on a.PJSD_ID=b.PJSD_ID 
                    left join ( select PJSD_ID, SUM(bdtime) sumtime from tb_u_DZXSPJK  where  addtype=1 and updatetime>='" + rq + " " + start_time + @"' and updatetime<'" + rq + " " + end_time + @"' group by  PJSD_ID) c on a.PJSD_ID=c.PJSD_ID   
	                left join tb_b_Eare d on a.QY_ID=d.QY_ID where a.PJSD_Enable=0 and a.delflag=0";
                if (qyid != null && qyid.Trim() != "0")
                {
                    sql += " and a.QY_ID='" + qyid + "'";
                }
                if (pjsdmc != null && pjsdmc.Trim() != "")
                {
                    sql += " and a.pjsd_mc like '%" + pjsdmc.Trim() + "%'";
                }
                sql += ") m order by QY_PX,PJSD_NO";
                SqlCommand ocmd = new SqlCommand(sql);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    [CSMethod("GetDzxspLogMX")]
    public object GetDzxspLogMX(string dwid, string date, string time)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "select * from tb_u_DZXSPJK where PJSD_ID='" + dwid.Trim() + "' and updatetime>='" + rq + "' and updatetime<'" + st + "'  order by updatetime desc";
                SqlCommand ocmd = new SqlCommand(sql);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    /// <summary>
    ///导出所有在日期范围内的商品进行价格比对（搜索主表：预统计表）
    /// </summary>
    [CSMethod("GetDate_Dzxsp", 2)]
    public byte[] GetDate_Dzxsp(string qyid, string pjsdmc, DateTime date, string time, string title)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = date.ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");

                string sql = @"select m.QY_NAME,m.PJSD_ID,m.PJSD_MC,m.addtime  
                from 
                (
	                select a.pjsd_id,a.pjsd_mc,a.PJSD_NO,b.addtime,d.QY_NAME,d.QY_CODE,d.QY_PX from tb_b_PJSD a 
	                left join ( select PJSD_ID,MAX(updatetime) addtime from tb_u_DZXSPJK  where  addtype=1 and updatetime>='" + rq + @"' and updatetime<'" + st + @"' group by  PJSD_ID) b on a.PJSD_ID=b.PJSD_ID 
	                left join tb_b_Eare d on a.QY_ID=d.QY_ID where a.PJSD_Enable=0 and a.delflag=0";
                if (qyid != null && qyid.Trim() != "0")
                {
                    sql += " and a.QY_ID='" + qyid + "'";
                }
                if (pjsdmc != null && pjsdmc.Trim() != "")
                {
                    sql += " and a.pjsd_mc like '%" + pjsdmc.Trim() + "%'";
                }
                sql += ") m order by QY_PX,PJSD_NO";
                DataTable dt = dbc.ExecuteDataTable(sql);

                dt.Columns.Remove("pjsd_id");

                dt.Columns["pjsd_mc"].Caption = "平价商店名称";
                dt.Columns["addtime"].Caption = "最近连接时间";
                dt.Columns["QY_NAME"].Caption = "区域";

                MemoryStream ms = OutFileToStreamSpjgbd(dt, title, rq + " " + time);
                return ms.ToArray();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    [CSMethod("GetDzcmpLog")]
    public object GetDzcmpLog(string qyid, string pjsdmc, DateTime date, string time)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = date.ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");

                string sql = @"select m.QY_NAME,m.PJSD_ID,m.PJSD_MC,m.addtime  
                from 
                (
	                select a.pjsd_id,a.pjsd_mc,a.PJSD_NO,b.addtime,d.QY_NAME,d.QY_CODE,d.QY_PX from tb_b_PJSD a 
	                left join ( select PJSD_ID,MAX(updatetime) addtime from tb_u_DZCMPJK  where  addtype=1 and updatetime>='" + rq + @"' and updatetime<'" + st + @"' group by  PJSD_ID) b on a.PJSD_ID=b.PJSD_ID 
	                left join tb_b_Eare d on a.QY_ID=d.QY_ID where a.PJSD_Enable=0 and a.delflag=0";
                if (qyid != null && qyid.Trim() != "0")
                {
                    sql += " and a.QY_ID='" + qyid + "'";
                }
                if (pjsdmc != null && pjsdmc.Trim() != "")
                {
                    sql += " and a.pjsd_mc like '%" + pjsdmc.Trim() + "%'";
                }
                sql += ") m order by QY_PX,PJSD_NO";
                SqlCommand ocmd = new SqlCommand(sql);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    [CSMethod("GetDzcmpLogMX")]
    public object GetDzcmpLogMX(string dwid, string date, string time)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "select * from tb_u_DZCMPJK where PJSD_ID='" + dwid.Trim() + "' and updatetime>='" + rq + "' and updatetime<'" + st + "'  order by updatetime desc";
                SqlCommand ocmd = new SqlCommand(sql);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    /// <summary>
    ///导出所有在日期范围内的商品进行价格比对（搜索主表：预统计表）
    /// </summary>
    [CSMethod("GetDate_Dzcmp", 2)]
    public byte[] GetDate_Dzcmp(string qyid, string pjsdmc, DateTime date, string time, string title)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = date.ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq + " " + time).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");

                string sql = @"select m.QY_NAME,m.PJSD_ID,m.PJSD_MC,m.addtime  
                from 
                (
	                select a.pjsd_id,a.pjsd_mc,a.PJSD_NO,b.addtime,d.QY_NAME,d.QY_CODE,d.QY_PX from tb_b_PJSD a 
	                left join ( select PJSD_ID,MAX(updatetime) addtime from tb_u_DZCMPJK  where  addtype=1 and updatetime>='" + rq + @"' and updatetime<'" + st + @"' group by  PJSD_ID) b on a.PJSD_ID=b.PJSD_ID 
	                left join tb_b_Eare d on a.QY_ID=d.QY_ID where a.PJSD_Enable=0 and a.delflag=0";
               
                if (qyid != null && qyid.Trim() != "0")
                {
                    sql += " and a.QY_ID='" + qyid + "'";
                }
                if (pjsdmc != null && pjsdmc.Trim() != "")
                {
                    sql += " and a.pjsd_mc like '%" + pjsdmc.Trim() + "%'";
                }
                sql += ") m order by QY_PX,PJSD_NO";
                DataTable dt = dbc.ExecuteDataTable(sql);

                dt.Columns.Remove("pjsd_id");

                dt.Columns["pjsd_mc"].Caption = "平价商店名称";
                dt.Columns["addtime"].Caption = "最近连接时间";
                dt.Columns["QY_NAME"].Caption = "区域";

                MemoryStream ms = OutFileToStreamSpjgbd(dt, title, rq + " " + time);
                return ms.ToArray();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    [CSMethod("GetDzxspLogMX")]
    public object GetDzxspLogMX(string dwid, string date)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "select * from tb_u_DZXSPJK where PJSD_ID='" + dwid.Trim() + "' and updatetime>='" + rq + "' and updatetime<'" + st + "'  order by updatetime desc";
                SqlCommand ocmd = new SqlCommand(sql);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


    [CSMethod("GetNMDzxspLogMX")]
    public object GetNMDzxspLogMX(string dwid, string date)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string rq = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                string st = Convert.ToDateTime(rq).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "select * from tb_u_DZXSPJK_NMSC where DW_ID='" + dwid.Trim() + "' and updatetime>='" + rq + "' and updatetime<'" + st + "'  order by updatetime desc";
                SqlCommand ocmd = new SqlCommand(sql);
                DataTable dt = dbc.ExecuteDataTable(ocmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


    public static MemoryStream OutFileToStreamSpjgbd(DataTable dt, string tableName, string time)
    {
        Workbook workbook = new Workbook(); //工作簿
        Worksheet sheet = workbook.Worksheets[0]; //工作表
        Cells cells = sheet.Cells;//单元格

        //为标题设置样式  
        Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式
        styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
        styleTitle.Font.Name = "宋体";//文字字体
        styleTitle.Font.Size = 26;//文字大小
        styleTitle.Font.IsBold = true;//粗体

        //样式2
        Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式
        style2.HorizontalAlignment = TextAlignmentType.Center;//文字靠左
        style2.Font.Name = "宋体";//文字字体
        style2.Font.Size = 12;//文字大小
        style2.Font.IsBold = true;//粗体

        //样式3
        Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式
        style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中
        style3.Font.Name = "宋体";//文字字体
        style3.Font.Size = 10;//文字大小
        style3.Font.IsBold = true;//粗体
        style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

        //样式4
        Style style4 = workbook.Styles[workbook.Styles.Add()];//新增样式
        style4.HorizontalAlignment = TextAlignmentType.Center;//文字居中
        style4.VerticalAlignment = TextAlignmentType.Center;
        style4.Font.Name = "宋体";//文字字体
        style4.Font.Size = 11;//文字大小
        style4.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style4.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style4.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style4.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

        //样式5
        Style style5 = workbook.Styles[workbook.Styles.Add()];//新增样式
        style5.HorizontalAlignment = TextAlignmentType.Center;//文字居中
        style5.Font.Name = "宋体";//文字字体
        style5.Font.Size = 11;//文字大小
        style5.Font.Color = System.Drawing.Color.Red;//文字红色
        style5.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style5.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style5.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style5.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;


        int Colnum = dt.Columns.Count;//表格列数
        int Rownum = dt.Rows.Count;//表格行数

        //生成行1 标题行 
        cells.Merge(0, 0, 1, Colnum - 1);//合并单元格
        cells[0, 0].PutValue(tableName);//填写内容
        cells[0, 0].SetStyle(styleTitle);
        cells.SetRowHeight(0, 38);

        cells.Merge(1, 0, 1, Colnum - 1);
        cells[1, 0].PutValue("查询时间：" + time);
        cells[1, 0].SetStyle(style2);
        cells.SetRowHeight(1, 36);

        //生成行2 列名行
        cells.SetRowHeight(2, 80);
        for (int i = 0; i < Colnum - 1; i++)
        {
            cells[2, i].PutValue(dt.Columns[i].Caption);
            cells[2, i].SetStyle(style3);
            cells.SetColumnWidth(i, 8);
        }

        string qy_name = "";
        int first = 3;
        int length = 1;
        //生成数据行
        for (int i = 0; i < Rownum; i++)
        {
            if (qy_name != dt.Rows[i]["QY_NAME"].ToString())
            {
                if (i != 0)
                {
                    cells.Merge(first, 0, length, 1);
                    cells[first, 0].PutValue(qy_name);
                    cells[first, 0].SetStyle(style4);

                    cells[first + length - 1, 0].SetStyle(style4);
                }

                qy_name = dt.Rows[i]["QY_NAME"].ToString();
                length = 1;
                first = 3 + i;
            }
            else
            {
                if (i == Rownum - 1)
                {
                    length++;
                    cells.Merge(first, 0, length, 1);
                    cells[first, 0].PutValue(qy_name);
                    cells[first, 0].SetStyle(style4);

                    cells[first + length - 1, 0].SetStyle(style4);
                }
                else
                {
                    length++;
                }
            }




            for (int k = 1; k < Colnum; k++)
            {
                if (k <= 2)
                {
                    if (k == 2)
                    {
                        if (dt.Rows[i]["cc"].ToString() == "1")
                        {
                            cells[3 + i, k].PutValue(dt.Rows[i]["addtime"] == DBNull.Value ? "" : ((DateTime)dt.Rows[i]["addtime"]).ToString("yyyy-MM-dd HH:mm:ss"));
                            cells[3 + i, k].SetStyle(style5);
                        }
                        else
                        {
                            cells[3 + i, k].PutValue(dt.Rows[i]["addtime"] == DBNull.Value ? "" : ((DateTime)dt.Rows[i]["addtime"]).ToString("yyyy-MM-dd HH:mm:ss"));
                            cells[3 + i, k].SetStyle(style4);
                        }
                    }
                    else
                    {
                        cells[3 + i, k].PutValue(dt.Rows[i][k].ToString());
                        cells[3 + i, k].SetStyle(style4);
                    }
                }
            }
            cells.SetRowHeight(3 + i, 24);
        }

        sheet.AutoFitColumns();

        MemoryStream ms = workbook.SaveToStream();
        return ms;
    }

    /// <summary>
    /// 获取区域
    /// </summary>
    /// <returns></returns>
    [CSMethod("GetQY")]
    public object GetQY()
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                string sql = "select QY_ID,QY_NAME FROM tb_b_Eare where STATUS=0 order by QY_PX";
                return dbc.ExecuteDataTable(sql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    /// <summary>
    /// 电子屏通启率
    /// </summary>
    /// <returns></returns>
    [CSMethod("GetDzxspTQL")]
    public object GetDzxspTQL(string qy_id, string stime, string etime, string pjsdmc)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                DateTime st = Convert.ToDateTime(stime);
                DateTime et = Convert.ToDateTime(etime).AddDays(1);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select  m.PJSD_ID,m.PJSD_MC,m.QY_NAME,m.QY_PX,sum(case when bg>8 then 1 else 0 end) dbs from
                                 (select convert(varchar(10),b.updatetime,120) date,a.PJSD_ID,a.PJSD_MC,c.QY_NAME,c.QY_PX,sum(bdtime) bg
                                  from tb_b_PJSD a left join  tb_u_DZXSPJK b on a.PJSD_ID=b.PJSD_ID and b.updatetime>=@stime and b.updatetime<@etime and b.addtype=1 left join tb_b_Eare c on a.QY_ID=c.QY_ID and c.STATUS=0 where  a.delflag=0 and a.PJSD_Enable=0 ";

                if (PrivilegeDescription.IsNeedAreaPrivilege("平价商店"))
                {
                    if (SystemUser.CurrentUser.QY_ID != "")
                    {
                        cmd.CommandText += " and a.QY_ID='" + SystemUser.CurrentUser.QY_ID + "'";
                    }
                }
                if (qy_id != "")
                {
                    cmd.CommandText += " and a.QY_ID='" + qy_id + "'";
                }
                if (pjsdmc != "")
                {
                    cmd.CommandText += " and a.PJSD_MC like'%" + pjsdmc + "%'";
                }

                cmd.CommandText += " group by convert(varchar(10),b.updatetime,120),a.PJSD_ID,a.PJSD_MC ,c.QY_NAME,c.QY_PX) m group by m.PJSD_ID,m.PJSD_MC,m.QY_NAME,m.QY_PX order by m.QY_PX";

                cmd.Parameters.AddWithValue("@stime", st);
                cmd.Parameters.AddWithValue("@etime", et);

                DataTable dt = dbc.ExecuteDataTable(cmd);
                dt.Columns.Add("zts");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int days = (et - st).Days;
                        dt.Rows[i]["zts"] = days;
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    /// <summary>
    /// 农贸市场电子屏通启率
    /// </summary>
    /// <returns></returns>
    [CSMethod("GetNMDzxspTQL")]
    public object GetNMDzxspTQL(string qy_id, string stime, string etime, string nmscmc)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                DateTime st = Convert.ToDateTime(stime);
                DateTime et = Convert.ToDateTime(etime).AddDays(1);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select  m.DW_ID,m.DW_MC,m.QY_NAME,m.QY_PX,sum(case when bg>8 then 1 else 0 end) dbs from
                                 (select convert(varchar(10),b.updatetime,120) date,a.DW_ID,a.DW_MC,c.QY_NAME,c.QY_PX,sum(bdtime) bg
                                  from tb_b_DW a left join  tb_u_DZXSPJK_NMSC b on a.DW_ID=b.DW_ID and b.updatetime>=@stime and b.updatetime<@etime and b.addtype=1 left join tb_b_Eare c on a.QY_ID=c.QY_ID and c.STATUS=0 where  a.status=0 and a.dw_enabled=0 ";
                if (PrivilegeDescription.IsNeedAreaPrivilege("平价商店"))
                {
                    if (SystemUser.CurrentUser.QY_ID != "")
                    {
                        cmd.CommandText += " and a.QY_ID='" + SystemUser.CurrentUser.QY_ID + "'";
                    }
                }
                if (qy_id != "")
                {
                    if (qy_id.Equals("sq"))
                        cmd.CommandText += " and a.QY_ID in(select QY_ID from tb_b_Eare where QY_CODE in(" + System.Configuration.ConfigurationManager.AppSettings["SQ_Code"] + "))";
                    else
                        cmd.CommandText += " and a.QY_ID='" + qy_id + "'";
                }
                if (nmscmc != "")
                {
                    cmd.CommandText += " and a.DW_MC like'%" + nmscmc + "%'";
                }

                cmd.CommandText += " group by convert(varchar(10),b.updatetime,120),a.DW_ID,a.DW_MC ,c.QY_NAME,c.QY_PX) m group by m.DW_ID,m.DW_MC,m.QY_NAME,m.QY_PX order by m.QY_PX";

                cmd.Parameters.AddWithValue("@stime", st);
                cmd.Parameters.AddWithValue("@etime", et);

                DataTable dt = dbc.ExecuteDataTable(cmd);
                dt.Columns.Add("zts");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int days = (et - st).Days;
                        dt.Rows[i]["zts"] = days;
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    /// <summary>
    /// 电子显示屏在线时长未达标明细
    /// </summary>
    /// <returns></returns>
    [CSMethod("GetBhgMx")]
    public object GetBhgMx(string pjsdid, string stime, string etime)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                DateTime st = Convert.ToDateTime(stime);
                DateTime et = Convert.ToDateTime(etime).AddDays(1);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select m.date,m.bg from(select convert(varchar(10),updatetime,120) date,sum(bdtime) bg from tb_u_DZXSPJK
                                    where PJSD_ID=@pjsdid and addtype=1 and updatetime>=@stime and updatetime<@etime
                                   group by convert(varchar(10),updatetime,120)) m
                                   where m.bg<8 order by m.date desc";
                cmd.Parameters.AddWithValue("@pjsdid", pjsdid);
                cmd.Parameters.AddWithValue("@stime", st);
                cmd.Parameters.AddWithValue("@etime", et);

                DataTable dt = dbc.ExecuteDataTable(cmd);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    /// <summary>
    /// 农贸市场电子显示屏在线时长未达标明细
    /// </summary>
    /// <returns></returns>
    [CSMethod("GetNMBhgMx")]
    public object GetNMBhgMx(string nmscid, string stime, string etime)
    {
        using (DBConnection dbc = new DBConnection())
        {
            try
            {
                DateTime st = Convert.ToDateTime(stime);
                DateTime et = Convert.ToDateTime(etime).AddDays(1);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select m.date,m.bg from(select convert(varchar(10),updatetime,120) date,sum(bdtime) bg from tb_u_DZXSPJK_NMSC
                                    where DW_ID=@nmscid and addtype=1 and updatetime>=@stime and updatetime<@etime
                                   group by convert(varchar(10),updatetime,120)) m
                                   where m.bg<8 order by m.date desc";
                cmd.Parameters.AddWithValue("@nmscid", nmscid);
                cmd.Parameters.AddWithValue("@stime", st);
                cmd.Parameters.AddWithValue("@etime", et);

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
