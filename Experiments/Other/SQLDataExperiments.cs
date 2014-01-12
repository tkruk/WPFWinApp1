using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.Other
{
    public class SQLDataExperiments
    {
        public SqlConnection DataConnection { get; set; }
        
        public SQLDataExperiments()
        {
            DataConnection = CreateConnection();
        }

        public int ExecuteSimpleInsertAccount()
        {
            return InsertAccountData("Account 1", "Test");
        }

        public int ExecuteDeleteAccount()
        {
            return DeleteAccountData("Account 1");
        }
  
        private SqlConnection CreateConnection()
        {
            var connectionString = "Persist Security Info=True;User ID=sa;Password=test1234;Database=TestDB;Server=.\\SQLSVR2012";
            var conn = new SqlConnection(connectionString);
            conn.Open();    

            return conn;
        }

        private int InsertAccountData(string accountID, string description)
        {
            string queryString = String.Format("INSERT INTO ACCOUNT " + "(Account_ID, Description) Values('{0}', '{1}')", accountID, description);

            SqlCommand command = new SqlCommand(queryString, DataConnection);
            int recordsAffected = command.ExecuteNonQuery();

            return recordsAffected;
        }

        private int DeleteAccountData(string accountID)
        {
            string queryString = String.Format("DELETE FROM ACCOUNT WHERE ACCOUNT_ID = '{0}'", accountID);

            SqlCommand command = new SqlCommand(queryString, DataConnection);
            int recordsAffected = command.ExecuteNonQuery();

            return recordsAffected;
        }

        public int DeleteAllAccountData()
        {
            string queryString = String.Format("DELETE FROM ACCOUNT");

            SqlCommand command = new SqlCommand(queryString, DataConnection);
            int recordsAffected = command.ExecuteNonQuery();

            return recordsAffected;
        }

        public void InsertMultipleAccounts(int count)
        {
            var rand = new Random();
            
            for (int i = 0; i < count; i++)
            {
                //var accountId = rand.Next().ToString();
                var accountId = i.ToString();
                var description = accountId;

                InsertAccountData(accountId, description);
            }
        }

        public int CountRecordsInAccountTable()
        {
            int rtnVal = 0;

            string queryString = "SELECT COUNT(*) FROM ACCOUNT";
            SqlCommand command = new SqlCommand(queryString, DataConnection);

            rtnVal = Convert.ToInt32(command.ExecuteScalar());
            
            return rtnVal;
        }
    }
}