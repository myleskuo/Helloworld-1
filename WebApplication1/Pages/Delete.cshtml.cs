using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication1.Pages
{
    public class DeleteModel : PageModel
    {
        [Display(Name = "�m�W")]
        public string Name { get; set; }

        [Display(Name = "�ʧO")]
        public string Sex { get; set; }

        [Display(Name = "������")]
        public string IdNumber { get; set; }
        [BindProperty]
        public Guid Uid { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "�R��";
        }
        public IActionResult OnPostDelete()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("WebApplication1Context");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Personal WHERE UniqueId = @UniqueId";
                cmd.Parameters.Add("@UniqueId", SqlDbType.NVarChar).Value = Uid.ToString();
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally//�����귽
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return RedirectToPage("form");
        }
    }
}
