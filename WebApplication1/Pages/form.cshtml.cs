using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;

namespace WebApplication1
{
    public class formPageModel : PageModel
    {
        [BindProperty]
        public string SearchName { get; set; }

        public List<Person> Persons = new List<Person>();
        public void OnGet()
        {
            ViewData["Title"] = "Courses 1";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("WebApplication1Context");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataReader dr = null;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Personal";

                try
                {
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Person person = new Person((Guid)dr["UniqueId"], dr["Name"].ToString(), dr["Sex"].ToString(), dr["IdNumber"].ToString());
                        Persons.Add(person);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally//關閉資源
                {
                    if (dr != null)
                    {
                        cmd.Cancel();
                        dr.Close();
                    }
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        public void OnPostSearch()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("WebApplication1Context");
            Person person = new Person(Guid.Empty, String.Empty, String.Empty, String.Empty);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataReader dr = null;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Personal WHERE Name LIKE @Name";
                if (SearchName == null) SearchName = string.Empty;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = "%"+ SearchName +"%";
                try
                {
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        person = new Person((Guid)dr["UniqueId"], dr["Name"].ToString(), dr["Sex"].ToString(), dr["IdNumber"].ToString());
                        Persons.Add(person);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally//關閉資源
                {
                    if (dr != null)
                    {
                        cmd.Cancel();
                        dr.Close();
                    }
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
    }
}