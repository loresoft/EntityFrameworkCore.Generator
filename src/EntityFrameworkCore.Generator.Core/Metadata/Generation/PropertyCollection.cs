using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    public class PropertyCollection
      : ObservableCollection<Property>
    {
        public bool IsProcessed { get; set; }

        public IEnumerable<Property> PrimaryKeys
        {
            get { return this.Where(p => p.IsPrimaryKey == true); }
        }

        public IEnumerable<Property> ForeignKeys
        {
            get { return this.Where(p => p.IsForeignKey == true); }
        }

        public Property ByColumn(string columnName)
        {
            return this.FirstOrDefault(x => x.ColumnName == columnName);
        }

        public Property ByProperty(string propertyName)
        {
            return this.FirstOrDefault(x => x.PropertyName == propertyName);
        }
    }
}
