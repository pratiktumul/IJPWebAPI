using System.Collections.Generic;
using System.Linq;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class DynamicReportRepo
    {
        readonly private IJPDBEntities db;
        public DynamicReportRepo()
        {
            db = new IJPDBEntities();
        }

        public List<TablenameModel> GetTableNames()
        {
            List<TablenameModel> tablenames = new List<TablenameModel>();
            var queryRes = db.Database.SqlQuery<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES").ToList();
            foreach (var _item in queryRes)
            {
                tablenames.Add(new TablenameModel
                {
                    TableName = _item
                });
            }
            return tablenames;
        }

        public List<ColnameModel> GetColumnNames(string tableName)
        {
            List<ColnameModel> colnames = new List<ColnameModel>();
            var queryRes = db.Database.SqlQuery<string>($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'").ToList();
            foreach (var _item in queryRes)
            {
                colnames.Add(new ColnameModel
                {
                    ColName = _item,
                });
            }
            return colnames;
        }

       /* public List<string> GenerateDynamicReport(string selectedCols, string tblName)
        {
            List<string> queryResult = new List<string>();
            string query = $"SELECT {selectedCols} FROM {tblName}";
            queryResult = db.Database.ExecuteSqlCommand(query,);
            
            return queryResult;
        }*/
    }
}