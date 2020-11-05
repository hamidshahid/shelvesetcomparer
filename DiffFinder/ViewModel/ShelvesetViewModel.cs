// <copyright file="GridViewSorter.cs" company="http://shelvesetcomparer.codeplex.com">
// Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html).
// This is sample code only, do not use in production environments.
// </copyright>

namespace WiredTechSolutions.ShelvesetComparer
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using System;

    /// <summary>
    /// Helper class to abstract from Microsoft Shelvset (to allow test values for debugging)
    /// </summary>
    public class ShelvesetViewModel
    {
        public ShelvesetViewModel(string name, DateTime creationDate, string ownerDisplayName)
        {
            Name = name;
            CreationDate = creationDate;
            OwnerDisplayName = ownerDisplayName;
        }
        public ShelvesetViewModel(Shelveset shelveset)
            : this(shelveset.Name, shelveset.CreationDate, shelveset.OwnerDisplayName)
        {
            Shelveset = shelveset;
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerDisplayName { get; set; }
        public Shelveset Shelveset { get; set; }
    }
}
