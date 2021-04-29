// <copyright file="PendingChangeFacadeStub.cs" company="http://shelvesetcomparer.codeplex.com">
// Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html).
// This is sample code only, do not use in production environments.
// </copyright>

#if StubbingWithoutServer

namespace WiredTechSolutions.ShelvesetComparer
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using System;
    using System.IO;

    public class PendingChangeFacadeStub : IPendingChange
    {
        public PendingChangeFacadeStub(string localItem, string sourceLocalItem, string serverItem, ItemType itemType, int itemId, ChangeType changeType, byte[] uploadHashValue)
        {
            LocalItem = localItem;
            SourceLocalItem = sourceLocalItem;
            ServerItem = serverItem;
            var serverItemParent = Path.GetDirectoryName(serverItem).Replace('\\', '/');
            LocalOrServerFolder = serverItemParent;
            FileName = Path.GetFileName(serverItem);
            LocalOrServerItem = serverItem;
            ItemType = itemType;
            ItemId = itemId;
            ChangeType = changeType;
            UploadHashValue = uploadHashValue;
        }

        public string PendingSetOwner { get; }
        public string PendingSetName { get; }
        public bool IsCandidate { get; set; }
        public ChangeType ChangeType { get; }
        public string ToolTipText { get; }
        public string EncodingName { get; }
        public string LockLevelShortName { get; }
        public string LockLevelName { get; }
        public string ChangeTypeName { get; }
        public bool IsImplicit { get; }
        public bool IsUnshelvedChange { get; }
        public ConflictType ConflictType { get; }
        public DateTime CreationDate { get; }
        public int SourceVersionFrom { get; set; }
        public string SourceServerItem { get; }
        public string SourceLocalItem { get; }
        public int SourceDeletionId { get; set; }
        public string ServerItem { get; }
        public int PendingChangeId { get; }
        public bool IsLock { get; }
        public LockLevel LockLevel { get; }
        public long Length { get; }
        public ItemType ItemType { get; }
        public int ItemId { get; }
        public byte[] HashValue { get; }
        public int Encoding { get; }
        public int DeletionId { get; }
        public string LocalItem { get; }
        public bool IsRollback { get; }
        public bool IsMerge { get; }
        public bool IsBranch { get; }
        public byte[] UploadHashValue { get; }
        public string LocalOrServerItem { get; }
        public string LocalOrServerFolder { get; }
        public string FileName { get; }
        public bool Undone { get; }
        public bool IsLocalItemDelete { get; }
        public int Version { get; }
        public bool IsEdit { get; }
        public bool IsRename { get; }
        public bool IsEncoding { get; }
        public bool IsDelete { get; }
        public bool IsUndelete { get; }
        public bool IsAdd { get; }
        public Stream DownloadBaseFile() { return null; }
        public void DownloadBaseFile(string localFileName) { }
        public Stream DownloadShelvedFile() { return null; }
        public void DownloadShelvedFile(string localFileName) { }

    }
}

#endif