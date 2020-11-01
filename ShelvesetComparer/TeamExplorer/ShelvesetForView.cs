using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiredTechSolutions.ShelvesetComparer
{
    public class ShelvesetForView
    {
        public ShelvesetForView(string name, DateTime creationDate, string ownerDisplayName)
        {
            Name = name;
            CreationDate = creationDate;
            OwnerDisplayName = ownerDisplayName;
        }
        public ShelvesetForView(Shelveset shelveset)
        {
            Name = shelveset.Name;
            CreationDate = shelveset.CreationDate;
            OwnerDisplayName = shelveset.OwnerDisplayName;
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerDisplayName { get; set; }
    }
}
