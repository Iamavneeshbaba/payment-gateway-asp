using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    private string strFieldValue, strSql;
	public Class1()
	{
		//
		// TODO: Add constructor logic here
		//


	}
    public string getValueTwoIn(string TableName, string ColumnNameIn1, string ColumnNameIn2, string ColumnNameOut, string ColumnValue1, string ColumnValue2)
    {
        try
        {
            strFieldValue = "";
            myConn.Open();

            SqlCommand mycmd = myConn.CreateCommand();
            mycmd.CommandText = "Select * from  where";

            mycmd.Parameters.Add(new SqlParameter("@TableName", TableName));
            mycmd.Parameters.Add(new SqlParameter("@ColumnNameIn1", ColumnNameIn1));
            mycmd.Parameters.Add(new SqlParameter("@ColumnNameIn2", ColumnNameIn2));
            mycmd.Parameters.Add(new SqlParameter("@columnNameOut", ColumnNameOut));
            mycmd.Parameters.Add(new SqlParameter("@ColumnValue1", ColumnValue1));
            mycmd.Parameters.Add(new SqlParameter("@ColumnValue2", ColumnValue2));


            SqlDataReader drData = mycmd.ExecuteReader();

            if (drData.Read())
            {
                strFieldValue = drData.GetValue(0).ToString();
            }
            drData.Close();
            myConn.Close();
            return strFieldValue;

        }
        catch (Exception ex)
        {
            string err = ex.Message;
            return err.ToString();
        }
        finally
        {
            myConn.Close();
        }
    }
}




