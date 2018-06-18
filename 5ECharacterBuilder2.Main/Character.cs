using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5ECharacterBuilder2.Main
{
    public class Character
    {
        public Character()
        {
            Attributes = new AttributeCollection(this);
        }

        public AttributeCollection Attributes { get; set; }
        public List<Class> Classes { get; set; }
        public Race Race { get; set; }
    }
}