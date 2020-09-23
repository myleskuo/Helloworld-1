using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class person
    {
        public int index { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string id { get; set; }

        public person(int _index, string _name, string _sex, string _id)
        {
            index = _index;
            name = _name;
            sex = _sex;
            id = _id;
        }
    }
}
