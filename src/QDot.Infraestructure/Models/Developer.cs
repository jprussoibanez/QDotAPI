using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDot.Infraestructure.Models
{
    public class Developer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Skill> Skills { get; set; }

        public Developer()
        {
            Skills = new List<Skill>();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var developer = (Developer)obj;
            return FirstName.Equals(developer.FirstName) && 
                LastName.Equals(developer.LastName) && 
                Age == developer.Age && 
                Skills.Equals(developer.Skills);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = FirstName != null ? (hash * 7) + FirstName.GetHashCode() : hash;
            hash = LastName != null ? (hash * 7) + LastName.GetHashCode() : hash;
            hash = (hash * 7) + Age.GetHashCode();
            hash = Skills != null ? (hash * 7) + Skills.GetHashCode() : hash;

            return hash;
        }
    }
}
