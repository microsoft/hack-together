using System;
using System.IO;

namespace FileSystemAbstraction {
    public interface IFileSystem
    {
        bool FileExists(string path);
        bool FileExists(string path, long biggerThanBytes);
        Stream FileCreate(string path);
        Stream FileOpenRead(string path);
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        char[] GetInvalidPathChars();
        char[] GetInvalidFileNameChars();
        string PathCombine(params string[] paths);
        void CreateDirectoryIfNotExist(string path, bool hidden = false);
        void FileDelete(string path);
        void FileMove(string sourceFileName, string destFileName);
        void DeleteDirectoryAndFiles(string filePath);
        void MoveFileRemoveFolder(string sourceFileName, string destFileName);
        string GetContainingFolderPath(string filePath);
        string GetContainingParentFolderPath(string filePath);
        string GetFileNameWithoutExtension(string filePath);
        string GetFileNameFromPath(string filePath);
    }
}
