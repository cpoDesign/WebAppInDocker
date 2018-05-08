using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace WebApp.Demo
{
   
    public class BloggingDataAccess
    {
        private string connectionString = @"Server=localhost;Database=Test;User=sa;Password=DemoPassword.1;";

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

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}