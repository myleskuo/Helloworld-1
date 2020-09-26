using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Person
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string IdNumber { get; set; }

        public Person(Guid _UniqueId, string _Name, string _Sex, string _IdNumber)
        {
            UniqueId = _UniqueId;
            Name = _Name;
            Sex = _Sex;
            IdNumber = _IdNumber;
        }
    }
}
