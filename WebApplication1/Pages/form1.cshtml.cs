using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Model;

namespace WebApplication1
{
    public enum sex
    {
        none,
        male,
        female
    }
    public class form1PageModel : PageModel
    {
        [BindProperty]
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "您必須輸入姓名")]
        public string Name { get; set; }
        [BindProperty]
        public int Sex { get; set; }
        [BindProperty]
        [Display(Name = "身分證")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "您必須輸入合法的身分證")]
        [Required(ErrorMessage = "您必須輸入身分證")]
        public string ID { get; set; }
        //public List<person> Person_List = new List<person>() { new person(0, "a", "male", "A123456789"), new person(1, "b", "female", "B123456789"), new person(2, "c", "male", "C123456789"), new person(3, "d", "female", "D123456789") };
        public void OnGet()
        {
            
        }
        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid)
            {
                //sex sexTemp = (sex)Sex;
                //string sexString = sexTemp.ToString();
                //Person_List.Add(new person(4, Name, sexString, ID));
                TempData["test"] = "I an come from form.";
                return RedirectToPage("form");
            }
            return Page();
        }
    }
}
