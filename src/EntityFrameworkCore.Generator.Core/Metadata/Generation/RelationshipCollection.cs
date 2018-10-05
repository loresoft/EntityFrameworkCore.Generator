using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    public class RelationshipCollection
      : ObservableCollection<Relationship>
    {
        public bool IsProcessed { get; set; }

        public Relationship ByName(string name)
        {
            return this.FirstOrDefault(x => x.RelationshipName == name);
        }

        public Relationship ByProperty(string propertyName)
        {
            return this.FirstOrDefault(x => x.ThisPropertyName == propertyName);
        }

        public Relationship ByOther(string name)
        {
            return this.FirstOrDefault(x => x.OtherEntity == name);
        }
    }
}
