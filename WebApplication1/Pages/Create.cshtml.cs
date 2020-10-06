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
        [Display(Name = "�L")]
        �L,
        [Display(Name = "�k")]
        �k,
        [Display(Name = "�k")]
        �k
    }
    public class form1PageModel : PageModel
    {
        [BindProperty]
        [Display(Name = "�m�W")]
        [Required(ErrorMessage = "�z������J�m�W")]
        public string Name { get; set; }

        [BindProperty]
        [Display(Name = "�ʧO")]
        public int Sex { get; set; }

        [BindProperty]
        [Display(Name = "������")]
        [RegularExpression("^[A-Za-z]{1}[1-2]{1}[0-9]{8}$", ErrorMessage = "�z������J�X�k��������")]
        [Required(ErrorMessage = "�z������J������")]
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
                //sexString = sexString == "male" ? "�k" : sexString == "female" ? "�k" : "�L";
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
            return Page();
        }
    }
}
