using System;
using System.Collections.ObjectModel;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    public class MethodCollection
        : ObservableCollection<Method>
    {
        public bool IsProcessed { get; set; }
    }
}
