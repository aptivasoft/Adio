using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using NLog;

/// <summary>
/// Summary description for DrugInfoDAL
/// </summary>
public class DrugInfoDAL
{
	public DrugInfoDAL()
	{
		 
	}

    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();

    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public int Ins_DrugTypeInfo(DrugInfo DInfo,string user)
    {
        int flag = 0;
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_set_DType";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter dType = sqlCmd.Parameters.Add("@DrugType", SqlDbType.VarChar, 50);
            dType.Value = DInfo.DrugType;

            SqlParameter userID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar,20);
            userID.Value = user;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            flag = 1;
            con.Close();
        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            
        }

        return flag;

    }

   

   public string Ins_DrugInfo(DrugInfo DInfo,string userID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_set_DrugInfo";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter dName = sqlCmd.Parameters.Add("@DName", SqlDbType.VarChar, 50);
            if (DInfo.DrugName != null)
                dName.Value = DInfo.DrugName;
            else
                dName.Value = Convert.DBNull;
           
            SqlParameter dCindex = sqlCmd.Parameters.Add("@DCostIndex", SqlDbType.Char);
            if (DInfo.DCostIndex != null)
                dCindex.Value = Convert.ToChar(DInfo.DCostIndex);
            else
                dCindex.Value = Convert.DBNull;

            int DTID = getDrugTypeID(DInfo);
            if (DTID != null)
                DInfo.DrugTypeID = DTID;

            SqlParameter dTypeID = sqlCmd.Parameters.Add("@DType_ID", SqlDbType.Int);
            if (DInfo.DrugTypeID != null)
                dTypeID.Value = DInfo.DrugTypeID;
            else
                dTypeID.Value = Convert.DBNull;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
        return "Drug Information Inserted Successfully...";       
    
    }

   public string update_DrugInfo(DrugInfo DInfo, string userID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_update_DrugInfo";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter dID = sqlCmd.Parameters.Add("@DID", SqlDbType.Int);
            if (DInfo.DrugID != null)
                dID.Value = DInfo.DrugID;
            else
                dID.Value = Convert.DBNull;

            
            SqlParameter dCindex = sqlCmd.Parameters.Add("@DCostIndex", SqlDbType.Char);
            if (DInfo.DCostIndex != null)
                dCindex.Value = DInfo.DCostIndex;
            else
                dCindex.Value = Convert.DBNull;

            int DTID = getDrugTypeID(DInfo);
            if (DTID != null)
                DInfo.DrugTypeID = DTID;

            SqlParameter dTypeID = sqlCmd.Parameters.Add("@DType_ID", SqlDbType.Int);
            if (DInfo.DrugTypeID != null)
                dTypeID.Value = DInfo.DrugTypeID;
            else
                dTypeID.Value = Convert.DBNull;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
        return "Drug Information Updated Successfully...";

    }
    public int delete_DrugInfo(DrugInfo DInfo,string userID)
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        int flag = 0;
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlCon;
            int drugCount=0;
            if(DInfo.DrugID != null)
                sqlCmd.CommandText = "select COUNT(*) from Drug_Inventory where Drug_ID=" + DInfo.DrugID.ToString();
            sqlCon.Open();
            drugCount=sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            if (drugCount !=-1)
            {
                flag = 0;
            }
            else
            {
                sqlCmd.CommandText = "sp_delete_DrugInfo";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter dID = sqlCmd.Parameters.Add("@DID", SqlDbType.Int);
                if (DInfo.DrugID != null)
                    dID.Value = DInfo.DrugID;
                else
                    dID.Value = Convert.DBNull;

                SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
                pUserID.Value = userID;

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                flag = 1;
            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            
        }
        finally
        {
            sqlCon.Close();
        }
        return flag;

    }    

       
   
  
    public DataTable DrugName()
    {
        SqlConnection con = new SqlConnection(ConStr);
        DataTable dtable = new DataTable();
        try
        {
           
            SqlCommand sqlCmd = new SqlCommand("select Drug_Name from Drug_Info", con);
            SqlDataReader sqlDr;
            
            DataRow dr;
            con.Open();
            sqlDr = sqlCmd.ExecuteReader();
            dtable.Columns.Add("DrugName");

            while (sqlDr.Read())
            {
                dr = dtable.NewRow();
                dr[0] = sqlDr[0].ToString();
                dtable.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return dtable;
    }

   
    public DataSet DTypeID()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlDataAdapter sqlda = new SqlDataAdapter("select DrugType from DrugType_Info", con);

            sqlda.Fill(ds, "drugtype");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return ds;
    
    }

    public int getDrugID(DrugInfo dInfo)
    {
        int dID = 0;
         SqlConnection con = new SqlConnection(ConStr);
         try
         {

             SqlCommand sqlCmd = new SqlCommand("select Drug_ID from Drug_Info where Drug_Name = '" + dInfo.DrugName + "'", con);

             con.Open();
             dID = Convert.ToInt32(sqlCmd.ExecuteScalar());
         }
         catch (Exception ex)
         {
             objNLog.Error("Exception : " + ex.Message);
         }
         finally
         {
             con.Close();
         }
        return dID;
    }
    

    public int getDrugTypeID(DrugInfo dInfo)
    {
        int dID = 0;
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand("select DrugType_ID from DrugType_Info where DrugType = '" + dInfo.DrugType + "'", con);

            con.Open();
            dID = Convert.ToInt32(sqlCmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        con.Close();

        return dID;
    }

    public DataTable get_DrugNames(string prefixText, int count, string contextKey)
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        string sqlQuery = "select * from Drug_Info where Drug_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsDrugNames = new DataSet();
        try
        {
            sqlDa.Fill(dsDrugNames, "Drug_Name");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsDrugNames.Tables[0];
    }

   
    public DataTable getDrugSearch(DrugInfo DInfo)
    { 
       
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select * from Drug_Info where Drug_Name = '" + DInfo.DrugName + "'", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        try
        {
            sqlDr = sqlCmd.ExecuteReader();
            dtable.Load(sqlDr);
            con.Close();
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dtable;
    }

    public string getDrugName(DrugInfo dInfo)
    {
        string drugName;
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Drug_Name from Drug_Info where Drug_ID = '" + dInfo.DrugID + "'", con);
        try
        {
            con.Open();
            drugName = sqlCmd.ExecuteScalar().ToString();
            con.Close();

        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
        return drugName;
        
    }

    
    public string getDrugType(DrugInfo dInfo)
    {
        string DrugType;
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select DrugType from DrugType_Info where DrugType_ID = '" + dInfo.DrugTypeID + "'", con);

        con.Open();
        try
        {
            DrugType = sqlCmd.ExecuteScalar().ToString();
            con.Close();
            return DrugType;
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
    }
      
    public DataTable InsuranceDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlDataAdapter sqlda = new SqlDataAdapter("select * from Insurance_Info", con);
            
            sqlda.Fill(ds, "Ins_Info");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return ds.Tables["Ins_Info"];
    }
}
