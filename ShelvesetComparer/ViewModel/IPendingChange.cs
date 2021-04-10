// <copyright file="IPendingChange.cs" company="http://shelvesetcomparer.codeplex.com">
// Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html).
// This is sample code only, do not use in production environments.
// </copyright>

namespace WiredTechSolutions.ShelvesetComparer
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using System;
    using System.IO;

    /// <summary>
    /// MS PendingChange is sealed, so we have our own Facade here
    /// </summary>
    public interface IPendingChange
    {
        ChangeType ChangeType { get; }
        string ChangeTypeName { get; }
        ConflictType ConflictType { get; }
        DateTime CreationDate { get; }
        int DeletionId { get; }
        int Encoding { get; }
        string EncodingName { get; }
        string FileName { get; }
        byte[] HashValue { get; }
        bool IsAdd { get; }
        bool IsBranch { get; }
        bool IsCandidate { get; set; }
        bool IsDelete { get; }
        bool IsEdit { get; }
        bool IsEncoding { get; }
        bool IsImplicit { get; }
        bool IsLocalItemDelete { get; }
        bool IsLock { get; }
        bool IsMerge { get; }
        bool IsRename { get; }
        bool IsRollback { get; }
        bool IsUndelete { get; }
        bool IsUnshelvedChange { get; }
        int ItemId { get; }
        ItemType ItemType { get; }
        long Length { get; }
        string LocalItem { get; }
        string LocalOrServerFolder { get; }
        string LocalOrServerItem { get; }
        LockLevel LockLevel { get; }
        string LockLevelName { get; }
        string LockLevelShortName { get; }
        int PendingChangeId { get; }
        string PendingSetName { get; }
        string PendingSetOwner { get; }
        string ServerItem { get; }
        int SourceDeletionId { get; set; }
        string SourceLocalItem { get; }
        string SourceServerItem { get; }
        int SourceVersionFrom { get; set; }
        string ToolTipText { get; }
        bool Undone { get; }
        byte[] UploadHashValue { get; }
        int Version { get; }

        Stream DownloadBaseFile();
        void DownloadBaseFile(string localFileName);
        Stream DownloadShelvedFile();
        void DownloadShelvedFile(string localFileName);
    }
}
