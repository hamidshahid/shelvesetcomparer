// <copyright file="PendingChangeFacade.cs" company="http://shelvesetcomparer.codeplex.com">
// Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html).
// This is sample code only, do not use in production environments.
// </copyright>

namespace WiredTechSolutions.ShelvesetComparer
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using System;
    using System.IO;

    public class PendingChangeFacade : IPendingChange
    {
        private readonly PendingChange m_PendingChange;
        public PendingChangeFacade(PendingChange pendingChange)
        {
            m_PendingChange = pendingChange;
        }

        public string PendingSetOwner => m_PendingChange.PendingSetOwner;
        public string PendingSetName => m_PendingChange.PendingSetName;
        public bool IsCandidate { get { return m_PendingChange.IsCandidate; } set { m_PendingChange.IsCandidate = value; } }
        public ChangeType ChangeType => m_PendingChange.ChangeType;
        public string ToolTipText => m_PendingChange.ToolTipText;
        public string EncodingName => m_PendingChange.EncodingName;
        public string LockLevelShortName => m_PendingChange.LockLevelShortName;
        public string LockLevelName => m_PendingChange.LockLevelName;
        public string ChangeTypeName => m_PendingChange.ChangeTypeName;
        public bool IsImplicit => m_PendingChange.IsImplicit;
        public bool IsUnshelvedChange => m_PendingChange.IsUnshelvedChange;
        public ConflictType ConflictType => m_PendingChange.ConflictType;
        public DateTime CreationDate => m_PendingChange.CreationDate;
        public int SourceVersionFrom { get { return m_PendingChange.SourceVersionFrom; } set { m_PendingChange.SourceVersionFrom = value; } }
        public string SourceServerItem => m_PendingChange.SourceServerItem;
        public string SourceLocalItem => m_PendingChange.SourceLocalItem;
        public int SourceDeletionId { get { return m_PendingChange.SourceDeletionId; } set { m_PendingChange.SourceDeletionId = value; } }
        public string ServerItem => m_PendingChange.ServerItem;
        public int PendingChangeId => m_PendingChange.PendingChangeId;
        public bool IsLock => m_PendingChange.IsLock;
        public LockLevel LockLevel => m_PendingChange.LockLevel;
        public long Length => m_PendingChange.Length;
        public ItemType ItemType => m_PendingChange.ItemType;
        public int ItemId => m_PendingChange.ItemId;
        public byte[] HashValue => m_PendingChange.HashValue;
        public int Encoding => m_PendingChange.Encoding;
        public int DeletionId => m_PendingChange.DeletionId;
        public string LocalItem => m_PendingChange.LocalItem;
        public bool IsRollback => m_PendingChange.IsRollback;
        public bool IsMerge => m_PendingChange.IsMerge;
        public bool IsBranch => m_PendingChange.IsBranch;
        public byte[] UploadHashValue => m_PendingChange.UploadHashValue;
        public string LocalOrServerItem => m_PendingChange.LocalOrServerItem;
        public string LocalOrServerFolder => m_PendingChange.LocalOrServerFolder;
        public string FileName => m_PendingChange.FileName;
        public bool Undone => m_PendingChange.Undone;
        public bool IsLocalItemDelete => m_PendingChange.IsLocalItemDelete;
        public int Version => m_PendingChange.Version;
        public bool IsEdit => m_PendingChange.IsEdit;
        public bool IsRename => m_PendingChange.IsRename;
        public bool IsEncoding => m_PendingChange.IsEncoding;
        public bool IsDelete => m_PendingChange.IsDelete;
        public bool IsUndelete => m_PendingChange.IsUndelete;
        public bool IsAdd => m_PendingChange.IsAdd;
        public Stream DownloadBaseFile() { return m_PendingChange.DownloadBaseFile(); }
        public void DownloadBaseFile(string localFileName) { m_PendingChange.DownloadBaseFile(localFileName); }
        public Stream DownloadShelvedFile() { return m_PendingChange.DownloadShelvedFile(); }
        public void DownloadShelvedFile(string localFileName) { m_PendingChange.DownloadShelvedFile(localFileName); }
    }
}
