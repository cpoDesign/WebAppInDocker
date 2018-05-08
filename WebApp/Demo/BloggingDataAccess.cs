using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using WebApp.Entities;

namespace WebApp.Demo
{

    public class BloggingDataAccess
    {
        private string connectionString = @"Server=localhost;Database=Test;User=sa;Password=P@55w0rd;";

        public IEnumerable<Post> GetPosts()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
               
                   const string insertQuery = @"SELECT * FROM POSTS";
                    
                return db.Query<Post>(insertQuery, db);
            }
        }
    }
}