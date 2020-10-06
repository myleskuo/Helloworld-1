using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Model;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public enum sex
    {
        [Display(Name = "無")]
        無,
        [Display(Name = "男")]
        男,
        [Display(Name = "女")]
        女
    }
    public class form1PageModel : PageModel
    {
        [BindProperty]
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "您必須輸入姓名")]
        public string Name { get; set; }

        [BindProperty]
        [Display(Name = "性別")]
        public int Sex { get; set; }

        [BindProperty]
        [Display(Name = "身分證")]
        [RegularExpression("^[A-Za-z]{1}[1-2]{1}[0-9]{8}$", ErrorMessage = "您必須輸入合法的身分證")]
        [Required(ErrorMessage = "您必須輸入身分證")]
        public string IdNumber { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Create";
        }
        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid)
            {
                sex sexTemp = (sex)Sex;
                string sexString = sexTemp.ToString();
                //sexString = sexString == "male" ? "男" : sexString == "female" ? "女" : "無";
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var config = builder.Build();
                var connectionString = config.GetConnectionString("WebApplication1Context");
                Person person = new Person(Guid.Empty, String.Empty, String.Empty, String.Empty);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO 
                                        Personal(UniqueId, Name, Sex, IdNumber)
                                        VALUES(@UniqueId, @Name, @Sex, @IdNumber) ";
                    cmd.Parameters.Add("@UniqueId", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                    cmd.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = sexString;
                    cmd.Parameters.Add("@IdNumber", SqlDbType.NVarChar).Value = IdNumber;
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally//關閉資源
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
            return Page();
        }
    }
}
