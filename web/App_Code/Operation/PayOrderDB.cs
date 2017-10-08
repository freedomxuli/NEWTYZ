using SmartFramework4v2.Data;
using SmartFramework4v2.Data.SqlServer;
using SmartFramework4v2.Web.WebExcutor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

[CSClass("PayOrderDB")]
public class PayOrderDB
{
    [CSMethod("GetPayOrderList")]
    public object GetPayOrderList(int pagnum, int pagesize, string mc, string no, string fjh, string bgmc, int sqzt)
    {
        using (DBConnection dbc = new DBConnection(ConfigurationManager.ConnectionStrings["LockConnStr"].ConnectionString))
        {
            try
            {
                int cp = pagnum;
                int ac = 0;

                string mStr = "";
                if (!string.IsNullOrEmpty(mc))
                    mStr += " and " + dbc.C_Like("a.RealName", mc.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(no))
                    mStr += " and " + dbc.C_Like("a.AuthorizeNo", no.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(fjh))
                    mStr += " and " + dbc.C_Like("a.RoomNo", no.Trim(), LikeStyle.LeftAndRightLike);
                if (!string.IsNullOrEmpty(bgmc))
                    mStr += " and " + dbc.C_Like("a.HotelName", no.Trim(), LikeStyle.LeftAndRightLike);


                string sqlStr = @"select a.*,b.HotelName,b.Mobile,b.CompleteAddress from Lock_AuthorizeOrder a
                                 left join Lock_Hotel b on a.HotelId=b.ID
                                 where AuthorStatus=" + sqzt + mStr + " order by a.CreateTime desc";
                DataTable dtPage = dbc.GetPagedDataTable(sqlStr, pagesize, ref cp, out ac);
                return new { dt = dtPage, cp = cp, ac = ac };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}