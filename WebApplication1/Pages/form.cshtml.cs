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

    public class formPageModel : PageModel
    {
        [BindProperty]
        public string SearchName { get; set; }

        public List<person> Person_List = new List<person>() { new person(0, "a", "male", "A123456789"), new person(1, "b", "female", "B123456789"), new person(2, "c", "male", "C123456789"), new person(3, "d", "female", "D123456789"), new person(4, "e", "female", "E123456789"), new person(5, "f", "male", "F123456789"), new person(6, "g", "female", "G123456789"), new person(7, "h", "female", "H123456789") };
        public List<person> Personal = new List<person>();
        public void OnGet()
        {
            
        }
        public void OnPostSearch()
        {
            foreach (var temp in Person_List)
            {
                if (temp.name == SearchName)
                {
                    Personal.Add(temp);
                    break;
                }
            }
        }
        public IActionResult OnPostNew()
        {
            return RedirectToPage("form1");
        }
    }
}