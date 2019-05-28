using System;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

using System.Collections;
using System.Text;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Collections.Generic;


public class ConnexionDB
{
     public static SqlConnection MaConn;
    public static SqlConnection MaConnBP;
    public static SqlConnection MaConnPV;
    public SqlDataReader dr;
    public static SqlConnection MaConn2;
    public static SqlTransaction transaction2;
    public static SqlTransaction transaction;
    public static bool connect2()
    {
        try
        {
            MaConn2.Open();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static bool deconnect2()
    {
        try
        {
            MaConn2.Close();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool ExecuteSqlWithTransaction(ref string erreur, List<string> listSql, List<SqlCommand> cmds, SqlConnection con)
    {
        //ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = con.BeginTransaction("SampleTransaction");
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd = con.CreateCommand();
        sqlCmd.Transaction = transaction;
        try
        {
            foreach (var sql in listSql)
            {

                sqlCmd.CommandText = sql;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }

            foreach (var cmd in cmds)
            {
                sqlCmd = new SqlCommand();
                sqlCmd = MaConn.CreateCommand();
                sqlCmd.Transaction = transaction;
                sqlCmd.CommandText = cmd.CommandText;
                foreach (SqlParameter p in cmd.Parameters)
                {
                    sqlCmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }
                sqlCmd.ExecuteNonQuery();
            }
            transaction.Commit();
            return true;


        }
        catch (Exception ex)
        {

            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);

            try
            {
                transaction.Rollback();
                erreur = ex.Message;
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }
            return false;
        }
    }

    public static bool ExecuteSqlWithTransactionDOUBLEBASE(ref string erreur, string Connect, List<SqlCommand> cmds, string Connect2, List<SqlCommand> cmds2)
    {


        try
        {
            //foreach (var sql in cm)
            //{

            //    sqlCmd.CommandText = sql;
            //    sqlCmd.CommandType = CommandType.Text;
            //    sqlCmd.ExecuteNonQuery();
            //}
            ConnexionDB.MaConn = new SqlConnection(Connect);

            ConnexionDB.connect();

            transaction = MaConn.BeginTransaction("SampleTransaction");

            foreach (var cmd in cmds)
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd = MaConn.CreateCommand();
                sqlCmd.Transaction = transaction;
                sqlCmd.CommandText = cmd.CommandText;
                foreach (SqlParameter p in cmd.Parameters)
                {
                    sqlCmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }
                sqlCmd.ExecuteNonQuery();
            }
            if (Connect2 != "")
            {
                ConnexionDB.MaConn2 = new SqlConnection(Connect2);
                ConnexionDB.connect2();
                transaction2 = MaConn2.BeginTransaction("SampleTransaction");
                foreach (var cmd in cmds2)
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd = MaConn2.CreateCommand();
                    sqlCmd.Transaction = transaction2;
                    sqlCmd.CommandText = cmd.CommandText;
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        sqlCmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                    }
                    sqlCmd.ExecuteNonQuery();
                }
            }
            transaction.Commit();
             if (Connect2 != "")
            transaction2.Commit();
            ConnexionDB.deconnect2();
            ConnexionDB.deconnect();
            return true;

        }
        catch (Exception ex)
        {

            Console.WriteLine();

            try
            {
                erreur = ex.Message;
                transaction.Rollback();
                transaction.Dispose();
                transaction2.Rollback();
                transaction2.Dispose();


            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }

            ConnexionDB.deconnect();
            return false;
        }


    }
  
    public static bool ExecuteSqlWithTransactionbluck(ref string erreur, string Connect, string table, List<SqlCommand> cmds)
    {
        ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = MaConn.BeginTransaction("SampleTransaction");

        try
        {
            //foreach (var sql in cm)
            //{

            //    sqlCmd.CommandText = sql;
            //    sqlCmd.CommandType = CommandType.Text;
            //    sqlCmd.ExecuteNonQuery();
            //}
            foreach (var cmd in cmds)
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd = MaConn.CreateCommand();
                sqlCmd.Transaction = transaction;
                sqlCmd.CommandText = cmd.CommandText;
                foreach (SqlParameter p in cmd.Parameters)
                {
                    sqlCmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }
                sqlCmd.ExecuteNonQuery();
            }


            //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Connect))
            //{
            //    bulkCopy.DestinationTableName = table;

            //    //try
            //    //{
            //    // Write the array of rows to the destination.
            //    bulkCopy.WriteToServer(rowArray);
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    Console.WriteLine(ex.Message);
            //    //}

            //}

            transaction.Commit();

            ConnexionDB.deconnect();
            return true;

        }
        catch (Exception ex)
        {

            Console.WriteLine();

            try
            {
                transaction.Rollback();
                transaction.Dispose();

                erreur = ex.Message;
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }

            ConnexionDB.deconnect();
            return false;
        }


    }
  
    public static bool ExecuteSqlWithTransactionbluck(ref string erreur, string Connect, string table, DataRow[] rowArray)
    {
        ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = MaConn.BeginTransaction("SampleTransaction");

        try
        {
            
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Connect))
            {
                bulkCopy.DestinationTableName = table;

                //try
                //{
                // Write the array of rows to the destination.
                bulkCopy.WriteToServer(rowArray);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

            }
            transaction.Commit();

            ConnexionDB.deconnect();
            return true;

        }
        catch (Exception ex)
        {

            Console.WriteLine();

            try
            {
                transaction.Rollback();
                transaction.Dispose();

                erreur = ex.Message;
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }

            ConnexionDB.deconnect();
            return false;
        }


    }
   
    public static bool ExecuteSqlWithTransactionbluck(ref string erreur, string Connect, string table, DataRow[] rowArray, List<SqlCommand> cmds)
    {
        ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = MaConn.BeginTransaction("SampleTransaction");

        try
        {
            //foreach (var sql in cm)
            //{

            //    sqlCmd.CommandText = sql;
            //    sqlCmd.CommandType = CommandType.Text;
            //    sqlCmd.ExecuteNonQuery();
            //}
            foreach (var cmd in cmds)
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd = MaConn.CreateCommand();
                sqlCmd.Transaction = transaction;
                sqlCmd.CommandText = cmd.CommandText;
                foreach (SqlParameter p in cmd.Parameters)
                {
                    sqlCmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }
                sqlCmd.ExecuteNonQuery();
            }
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Connect))
            {
                bulkCopy.DestinationTableName = table;

                //try
                //{
                // Write the array of rows to the destination.
                bulkCopy.WriteToServer(rowArray);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

            }
            transaction.Commit();

            ConnexionDB.deconnect();
            return true;

        }
        catch (Exception ex)
        {

            Console.WriteLine();

            try
            {
                transaction.Rollback();
                transaction.Dispose();

                erreur = ex.Message;
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }

            ConnexionDB.deconnect();
            return false;
        }


    }
    public static bool ExecuteSqlWithTransaction(ref string erreur, string Connect, List<SqlCommand> cmds)
    {
        ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = MaConn.BeginTransaction("SampleTransaction");

        try
        {
            //foreach (var sql in cm)
            //{

            //    sqlCmd.CommandText = sql;
            //    sqlCmd.CommandType = CommandType.Text;
            //    sqlCmd.ExecuteNonQuery();
            //}
            foreach (var cmd in cmds)
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd = MaConn.CreateCommand();
                sqlCmd.Transaction = transaction;
                sqlCmd.CommandText = cmd.CommandText;
                foreach (SqlParameter p in cmd.Parameters)
                {
                    sqlCmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }
                sqlCmd.ExecuteNonQuery();
            }
            transaction.Commit();

            ConnexionDB.deconnect();
            return true;

        }
        catch (Exception ex)
        {

            Console.WriteLine();

            try
            {
                transaction.Rollback();
                transaction.Dispose();

                erreur = ex.Message;
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }

            ConnexionDB.deconnect();
            return false;
        }


    }
   
    public static bool PingTest()
    {
        try
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send("192.168.1.44", timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
                return true;
            return false;
        }
        catch (Exception ex) { deconnect(ConnexionDB.MaConn); throw ex; }
    }

    public static bool connect(SqlConnection con)
    {
        try
        {
            con.Close();
            con.Open();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool deconnect(SqlConnection con)
    {
        try
        {
            con.Close();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static bool connect()
    {
        try
        {
            MaConn.Open();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static bool ExecuteSql(String sql, SqlConnection con)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd = MaConn.CreateCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.Text;
            if (connect(con))
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                deconnect(con);
                return true;
            }
            deconnect(con);

            return false;
        }
        catch (Exception ex)
        {
            deconnect(ConnexionDB.MaConn);
            throw ex;
        }
    }
    public static bool ExecuteSql(ref string erreur,String sql, SqlConnection con)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd = MaConn.CreateCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.Text;
            if (connect(con))
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                deconnect(con);
                return true;
            }
            deconnect(con);

            return false;
        }
        catch (Exception ex)
        {
            erreur = ex.Message;
            deconnect(ConnexionDB.MaConn);
            throw ex;
        }
    }

     

    //public static bool ExecuteSqlWithTransaction(List<string> listSql, string Connect, ListBox ListLogTXT)
    //{
    //    ConnexionDB.MaConn = new SqlConnection(Connect);
    //    ConnexionDB.connect();

    //    transaction = MaConn.BeginTransaction("SampleTransaction");
    //    SqlCommand sqlCmd = new SqlCommand();
    //    sqlCmd = MaConn.CreateCommand();
    //    sqlCmd.Transaction = transaction;
    //    try
    //    {
    //        foreach (var sql in listSql)
    //        {

    //            sqlCmd.CommandText = sql;
    //            sqlCmd.CommandType = CommandType.Text;
    //            sqlCmd.ExecuteNonQuery();
    //        }
    //        transaction.Commit();

    //        ConnexionDB.deconnect();
    //        return true;

    //    }
    //    catch (Exception ex)
    //    {

    //        Console.WriteLine();
    //         ListLogTXT.Items.Add("Erreur Dintégration !!  "+ ex.Message ); 
                          
    //        try
    //        {
    //            transaction.Rollback();
    //            transaction.Dispose();
    //        }
    //        catch (Exception ex2)
    //        {
    //            // This catch block will handle any errors that may have occurred 
    //            // on the server that would cause the rollback to fail, such as 
    //            // a closed connection.
    //            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
    //            Console.WriteLine("  Message: {0}", ex2.Message);
    //        }

    //        ConnexionDB.deconnect();
    //        return false;
    //    }


    //}


    public static bool ExecuteSqlWithTransaction(List<string> listSql, string Connect)
    {
        ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = MaConn.BeginTransaction("SampleTransaction");
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd = MaConn.CreateCommand();
        sqlCmd.Transaction = transaction;
        try
        {
            foreach (var sql in listSql)
            {

                sqlCmd.CommandText = sql;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            transaction.Commit();

            ConnexionDB.deconnect();
            return true;

        }
        catch (Exception ex)
        {

            Console.WriteLine();
             
            try
            {
                transaction.Rollback();
                transaction.Dispose();
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }

            ConnexionDB.deconnect();
            return false;
        }


    }

    //public static bool ExecuteSqlWithTransaction(List<string> listSql, string Connect , ref SqlTransaction transaction, ListBox ListLogTXT)
    //{

    //    ConnexionDB.MaConn = new SqlConnection(Connect);
    //    ConnexionDB.connect();

    //    transaction = MaConn.BeginTransaction("SampleTransaction");
    //    SqlCommand sqlCmd = new SqlCommand();
    //    sqlCmd = MaConn.CreateCommand();
    //    sqlCmd.Transaction = transaction;
    //    try
    //    {
    //        foreach (var sql in listSql)
    //        {

    //            sqlCmd.CommandText = sql;
    //            sqlCmd.CommandType = CommandType.Text;
    //            sqlCmd.ExecuteNonQuery();
    //        }
            
    //        return true;

    //    }
    //    catch (Exception ex)
    //    {

    //        Console.WriteLine();
    //         ListLogTXT.Items.Add("Erreur Dintégration !!  " + ex.Message);

             

          
    //        return false;
    //    }

    //}


    // dans 
    //public static bool ExecuteSqlWithTransactionOuverte(List<string> listSql, string Connect, ref SqlTransaction transaction, ListBox ListLogTXT)
    //{

    //    ConnexionDB.MaConn = new SqlConnection(Connect);
    //    ConnexionDB.connect();

    //    transaction = MaConn.BeginTransaction("SampleTransaction");
    //    SqlCommand sqlCmd = new SqlCommand();
    //    sqlCmd = MaConn.CreateCommand();
    //    sqlCmd.Transaction = transaction;
    //    try
    //    {
    //        foreach (var sql in listSql)
    //        {

    //            sqlCmd.CommandText = sql;
    //            sqlCmd.CommandType = CommandType.Text;
    //            sqlCmd.ExecuteNonQuery();
    //        }

    //        return true;

    //    }
    //    catch (Exception ex)
    //    {

    //        Console.WriteLine();
    //         ListLogTXT.Items.Add("Erreur Dintégration !!  " + ex.Message);




    //        return false;
    //    }

    //}
    
    public static bool deconnect()
    {
        try
        {
            MaConn.Close();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static bool ExecuteSqlWithTransaction(List<string> listSql, SqlConnection con)
    {
        //ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = con.BeginTransaction("SampleTransaction");
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd = con.CreateCommand();
        sqlCmd.Transaction = transaction;
        try
        {
            foreach (var sql in listSql)
            {

                sqlCmd.CommandText = sql;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            transaction.Commit();
            return true;


        }
        catch (Exception ex)
        {

            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);

            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }
            return false;
        }
    }




      public static bool ExecuteSqlWithTransaction(ref string erreur,List<string> listSql, SqlConnection con)
    {
        //ConnexionDB.MaConn = new SqlConnection(Connect);
        ConnexionDB.connect();

        transaction = con.BeginTransaction("SampleTransaction");
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd = con.CreateCommand();
        sqlCmd.Transaction = transaction;
        try
        {
            foreach (var sql in listSql)
            {

                sqlCmd.CommandText = sql;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            transaction.Commit();
            return true;


        }
        catch (Exception ex)
        {

            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);

            try
            {
                transaction.Rollback();
                erreur = ex.Message ;
            }
            catch (Exception ex2)
            {     
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }
            return false;
        }
    }


    public static DataAdapter GetResultSql(String sql, SqlConnection con)
    {
        try
        {
            
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd = con.CreateCommand();
                sqlCmd.CommandText = sql;
                sqlCmd.CommandType = CommandType.Text;
                if (connect(con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                    adapter.SelectCommand.CommandTimeout = 120;
                    deconnect(con);
                    return adapter;
                }
                deconnect(con);
                return null;
           
          
        }
        catch (Exception ex)
        {
            deconnect(con);
            throw ex;
        }
    }
  
    public static SqlDataReader getDataReader(String SQL, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(SQL, con);

        SqlDataReader dr;
        try
        {
            if (connect(con))
            {
                dr = cmd.ExecuteReader();
                cmd.Dispose();
                 
                return dr;
               
            }
            return null;
        }
        catch (Exception ex)
        {
            deconnect(con);
            throw ex;
        }


    }

    public static DataAdapter procStockOrder(string sQuery, ArrayList paramIN, ArrayList paramOUT)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd = MaConn.CreateCommand();
        sqlCmd.CommandText = sQuery;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        if (paramIN.Count > 0)
            foreach (string[] tableau in paramIN)
            {

                sqlCmd.Parameters.Add(tableau[0], tableau[1]).Direction = ParameterDirection.Input;
            }
        if (paramOUT.Count > 0)
            foreach (string[] tableau in paramOUT)
            {
                sqlCmd.Parameters.Add(tableau[0], SqlDbType.VarChar).Direction = ParameterDirection.Output;
            }
        try
        {

            if (connect(ConnexionDB.MaConn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                deconnect(ConnexionDB.MaConn);
                return adapter;
            }
            deconnect(ConnexionDB.MaConn);
            return null;
        }
        catch (Exception ex)
        {
            deconnect(ConnexionDB.MaConn);
            throw ex;
        }
    }

 

       public static bool ExecuteSql(String sql)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd = MaConn.CreateCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.Text;
            if (connect())
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                deconnect();
                return true;
            }
            deconnect();

            return false;
        }
        catch (Exception ex)
        {
            deconnect();
            throw ex;
        }
    }
    public static DataAdapter GetResultSql(String sql)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd = MaConn.CreateCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.Text;
            try
            {
                if (connect())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                    deconnect();
                    return adapter;
                }
                deconnect();
            }
            catch { }
            return null;
        }
        catch (Exception ex)
        {
            deconnect();
            throw ex;
        }
    }

}

