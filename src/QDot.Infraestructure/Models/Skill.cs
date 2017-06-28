using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDot.Infraestructure.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var skill = (Skill)obj;
            return Name.Equals(skill.Name) &&
                Type.Equals(skill.Type) &&
                Level == skill.Level;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = Name != null ? (hash * 7) + Name.GetHashCode() : hash;
            hash = Type != null ? (hash * 7) + Type.GetHashCode() : hash;
            hash = (hash * 7) + Level.GetHashCode();

            return hash;
        }
    }
}
