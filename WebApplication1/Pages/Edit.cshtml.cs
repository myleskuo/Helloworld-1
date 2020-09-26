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
    public enum sex
    {
        [Display(Name = "�L")]
        �L,
        [Display(Name = "�k")]
        �k,
        [Display(Name = "�k")]
        �k
    }
    public class EditModel : PageModel
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
        [BindProperty]
        public Guid Uid { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Edit";
        }
        public IActionResult OnPostEdit()
        {
            if (ModelState.IsValid)
            {
                sex sexTemp = (sex)Sex;
                string sexString = sexTemp.ToString();
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var config = builder.Build();
                var connectionString = config.GetConnectionString("WebApplication1Context");
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Personal SET Name = @Name, Sex = @Sex, IdNumber = @IdNumber WHERE UniqueId = @UniqueId";
                    cmd.Parameters.Add("@UniqueId", SqlDbType.NVarChar).Value = Uid.ToString();
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