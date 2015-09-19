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
/// Summary description for Login
/// </summary>
public class RegisterUserDAL
{
    int resultFlag = 0;
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    public RegisterUserDAL()
	{
    }

    public int CreateUser(Property objUser, string user)
    {
        EncryptPassword encPwd = new EncryptPassword();
        
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_set_Users", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;


        SqlParameter userid = sqlCmd.Parameters.Add("@User_ID", SqlDbType.VarChar, 50);
        userid.Value = objUser.UserID;

        SqlParameter passWord = sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 32);
        string encP = encPwd.EncryptText(objUser.Password, "helloworld");
        passWord.Value = encP.Trim();

        SqlParameter comments = sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 50);
        comments.Value = objUser.Comments;

        SqlParameter stampsLoc = sqlCmd.Parameters.Add("@StampLoc", SqlDbType.VarChar, 50);
        stampsLoc.Value = objUser.StampLoc;

        if (objUser.EMPID > 0)
        {
            SqlParameter empID = sqlCmd.Parameters.Add("@Emp_ID", SqlDbType.Int);
            empID.Value = objUser.EMPID;
            SqlParameter DocID = sqlCmd.Parameters.Add("@Doc_ID", SqlDbType.Int);
            DocID.Value = int.Parse(objUser.DocID);
            SqlParameter empFName = sqlCmd.Parameters.Add("@Emp_FName", SqlDbType.VarChar,50);
            empFName.Value = objUser.EMPFName;
            SqlParameter empLName = sqlCmd.Parameters.Add("@Emp_LName", SqlDbType.VarChar, 50);
            empLName.Value = objUser.EMPLName;
        }

        SqlParameter Userrole = sqlCmd.Parameters.Add("@User_Role", SqlDbType.Char, 1);
        Userrole.Value = objUser.UserRole;

        SqlParameter User_type = sqlCmd.Parameters.Add("@User_Type", SqlDbType.Char, 1);
        User_type.Value = "N";

        SqlParameter pUser = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        pUser.Value = user;


        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            resultFlag = 1;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Registering User Profile.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Registering User Profile.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return resultFlag;
    }

    public DataTable get_EmpNames(string prefixText, int count)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "";
        if (prefixText.IndexOf(',') == -1)

            sqlQuery = "select [Emp_ID] ,[Emp_FName] ,[Emp_LName] from Employee where Emp_LName like '" + prefixText + "%'";
        else
        {
            string[] Docname = prefixText.Split(',');

            sqlQuery = "select [Emp_ID] ,[Emp_FName] ,[Emp_LName] from Employee where Emp_LName = '" + Docname[0] + "' and Emp_FName like '" + Docname[1] + "%'";
        }
        //string sqlQuery = "select * from Employee where Emp_LName like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet Employee = new DataSet();
        try
        {            
            sqlDa.Fill(Employee, "Employee");            
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving User Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving User Info.", ex);
        }       
        return Employee.Tables["Employee"];
    }
   
    public DataTable get_EmpNames_Sup(string prefixText, int count,string username)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "";
        if (prefixText.IndexOf(',') == -1)

            sqlQuery = "select [Emp_ID],[Emp_FName],[Emp_LName] from Employee where Emp_LName like '" + prefixText + "%'   and (Emp_SupID2=(SELECT [Emp_ID] from [Users] where [User_Id]='" + username + "') or Emp_SupID1=(SELECT [Emp_ID] from [Users] where [User_Id]='" + username + "') )";
        else
        {
            string[] Docname = prefixText.Split(',');

            sqlQuery = "select [Emp_ID],[Emp_FName],[Emp_LName] from Employee where Emp_LName = '" + Docname[0] + "' and Emp_FName like '" + Docname[1] + "%' and (Emp_SupID2=@empId or Emp_SupID1=@empId )";
        }
        //string sqlQuery = "select * from Employee where Emp_LName like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet Employee = new DataSet();
        try
        {            
            sqlDa.Fill(Employee, "Employee");            
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Employee Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Employee Names.", ex);
        }          
        return Employee.Tables["Employee"];
    }

    public DataTable get_ACodes(string prefixText, int count)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Activity_Code,Activity_Name  from Activities where Activity_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsACodes = new DataSet();
        try
        {
            sqlDa.Fill(dsACodes, "ACodes");
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Activity Codes.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Activity Codes.", ex);
        }  
        return dsACodes.Tables["ACodes"];
    }

    public DataTable get_ABCodes(string prefixText, int count)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Absent_Code,Absent_Name  from AbsentCodes where Absent_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsACodes = new DataSet();
        try
        {
            sqlDa.Fill(dsACodes, "ACodes");
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Absent Codes.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Absent Codes.", ex);
        }
        return dsACodes.Tables["ACodes"];
    }
 
    public DataTable get_UserNames(string prefixText, int count)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select User_Id from Users where User_Id like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsUsrNames = new DataSet();
        try
        {
            sqlDa.Fill(dsUsrNames, "User_Id");
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving User Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving User Names.", ex);
        }
        return dsUsrNames.Tables["User_Id"];
    }

    public DataTable getUserSearch(Property obj)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(conStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_getUsers";
            sqlCmd.Connection = con;

            SqlParameter user_Id = sqlCmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 20);
            user_Id.Value = obj.UserID;
            SqlDataAdapter ada = new SqlDataAdapter(sqlCmd);
            
            ada.Fill(ds, "userdata");
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Searching User.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Searching User.", ex);
        }
        return ds.Tables["userdata"];
    }

    public DataTable getUserRole(Property obj)
    {
        SqlConnection con = new SqlConnection(conStr);
        DataTable dtable = new DataTable();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("select * from UserRole where UserID = '" + obj.UserID + "'", con);
            SqlDataReader sqlDr;
           
            // DataRow dr;
            con.Open();
            sqlDr = sqlCmd.ExecuteReader();
            dtable.Load(sqlDr);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving User Role.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving User Role.", ex);
        }
        finally
        {
            con.Close();
        }
        return dtable;
    }

    public DataTable getUserRole()
    {
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("select * from Roles", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        sqlDr = sqlCmd.ExecuteReader();
        dtable.Load(sqlDr);

        con.Close();
        return dtable;
    }
       

    public string Delete_UserInfo(Property objUser,string user)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        string msg = "";
        try
        {
           
            SqlCommand sqlCmd = new SqlCommand("sp_DelUserInfo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter userID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
            userID.Value = objUser.UserID.Trim().ToString();

            SqlParameter pUser = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            pUser.Value = user;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            msg = "User Information Deleted Successfully";            
        }

        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting User Info.", SqlEx);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting User Info.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return msg;
    }

    public string Update_UserInfo(Property objUser,string user)
    {
        EncryptPassword encPwd = new EncryptPassword();

        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_UpdateUser1", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter userid = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
        userid.Value = objUser.UserID;

        //SqlParameter passWord = sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 32);
        //string encP = encPwd.EncryptText(objUser.Password, "helloworld");
        //passWord.Value = encP.Trim();

        SqlParameter comments = sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 50);
        comments.Value = objUser.Comments;

        SqlParameter stampLoc = sqlCmd.Parameters.Add("@StampLoc", SqlDbType.VarChar, 50);
        stampLoc.Value = objUser.StampLoc;

        SqlParameter Userrole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.VarChar, 1);
        Userrole.Value = objUser.UserRole;
        if (objUser.EMPID > 0)
        {
            SqlParameter empID = sqlCmd.Parameters.Add("@Emp_ID", SqlDbType.Int);
            empID.Value = objUser.EMPID;
            SqlParameter DocID = sqlCmd.Parameters.Add("@Doc_ID", SqlDbType.Int);
            DocID.Value = int.Parse(objUser.DocID);
            SqlParameter empFName = sqlCmd.Parameters.Add("@Emp_FName", SqlDbType.VarChar, 50);
            empFName.Value = objUser.EMPFName;
            SqlParameter empLName = sqlCmd.Parameters.Add("@Emp_LName", SqlDbType.VarChar, 50);
            empLName.Value = objUser.EMPLName;
        }

        SqlParameter pUser = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        pUser.Value = user;

        string msg = "";

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            msg = "User Information Updated Successfully";
        }

        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating User Info.", SqlEx);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updaing User Info.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return msg;
    }

    public DataTable Get_ProvUserID()
    {
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("select UserID from Doctor_Info where Status<>'N' ", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        DataRow dr;
        con.Open();
        sqlDr = sqlCmd.ExecuteReader();
        dtable.Columns.Add("UserID");

        while (sqlDr.Read())
        {
            dr = dtable.NewRow();
            dr[0] = sqlDr[0].ToString();
            dtable.Rows.Add(dr);
        }
        con.Close();
        return dtable;
    }
    public DataTable Get_EmpNames()
    {
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("select Emp_ID,Emp_LName,Emp_FName from Employee", con);
        DataSet ds_empNames = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
        da.Fill(ds_empNames, "emp_Names");
        return ds_empNames.Tables["emp_Names"];
    }

    public int ChangeUserPassword(Property objProp, string user)
    {
        int flag = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        EncryptPassword objEncPwd = new EncryptPassword();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandText = "sp_ChangePassword";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = objProp.UserID;

            SqlParameter pOldPwd = sqlCmd.Parameters.Add("@OldPwd", SqlDbType.VarChar, 32);
            string encOldPwd = objEncPwd.EncryptText(objProp.OldPassword, "helloworld");
            pOldPwd.Value = encOldPwd;

            SqlParameter pNewPwd = sqlCmd.Parameters.Add("@NewPwd", SqlDbType.VarChar, 32);
            string encNewPwd = objEncPwd.EncryptText(objProp.Password, "helloworld");
            pNewPwd.Value = encNewPwd;

            SqlParameter pUser = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            pUser.Value = user;

            SqlParameter pFlag = sqlCmd.Parameters.Add("@Flag", SqlDbType.Int);
            pFlag.Direction = ParameterDirection.Output;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            flag = (int)pFlag.Value;
             
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Changing Password.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Changing Password.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return flag;
    }


    public DataTable GetStampAddrLocations()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_GetStampLoc", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsStampLocs = new DataSet();
        try
        {
            sqlDa.Fill(dsStampLocs, "StampLocs");
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Stamp Address Locations.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while  Retrieving Stamp Address Locations.", ex);
        }
        return dsStampLocs.Tables["StampLocs"];
    }
}
