using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public enum sex
    {
        none,
        male,
        female
    }
    public class person
    {
        public string name { get; set; }
        public string sex { get; set; }
        public string id { get; set; }

        public person(string _name, string _sex, string _id)
        {
            name = _name;
            sex = _sex;
            id = _id;
        }
    }
    public class formPageModel : PageModel
    {
        [BindProperty]
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "您必須輸入姓名")]
        public string Name { get; set; }
        [BindProperty]
        public int Sex { get; set; }
        [BindProperty]
        public string SearchName { get; set; }
        [BindProperty]
        [Display(Name = "身分證")]
        [Required(ErrorMessage = "您必須輸入身分證")]
        public string ID { get; set; }
        
        public List<person> Person_List = new List<person>()
        { 
            new person("a", "male", "A123456789"), 
            new person("b", "female", "B123456789"), 
            new person("c", "male", "C123456789"), 
            new person("d", "female", "D123456789") 
        };
        public List<person> Personal = new List<person>();
        public void OnGet()
        {
        
        }
        public void OnPostForm1()
        {
           foreach (var temp in Person_List)
           {
                if (temp.name == SearchName)
                {
                    Personal.Add(temp);
                }
           }
        }
        public void OnPostForm2()
        {
            if (ModelState.IsValid) 
            {
                sex sexTemp = (sex)Sex;
                string sexString = sexTemp.ToString();
                Person_List.Add(new person(Name, sexString, ID));
            }
        }
    }
}