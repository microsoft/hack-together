using FileSystemAbstraction;

namespace Sharepoint.IO;

public class FileServices : IFileSystem {
    private const string FILESERVICE = "Sharepoint.IO.FileServices";
    private readonly ISharepointHelperService sharepointHelperService;
    private readonly string sharepointSiteId;
    private readonly string siteUriPart;

    public FileServices(ISharepointHelperService sharepointHelperService, string sharepointSiteId, string siteUriPart) {
        this.sharepointHelperService = sharepointHelperService;
        this.sharepointSiteId = sharepointSiteId;
        this.siteUriPart = siteUriPart;
    }
    public bool FileExists(string path) {
        throw new NotImplementedException();
    }

    public bool FileExists(string path, long biggerThanBytes) {
        throw new NotImplementedException();
    }

    public Stream FileCreate(string path) {
        throw new NotImplementedException();
    }

    public Stream FileOpenRead(string path) {
        throw new NotImplementedException();
    }

    public bool DirectoryExists(string path) {
        throw new NotImplementedException();
    }

    public void CreateDirectory(string path) {
        throw new NotImplementedException();
    }

    public char[] GetInvalidPathChars() {
        throw new NotImplementedException();
    }

    public char[] GetInvalidFileNameChars() {
        throw new NotImplementedException();
    }

    public string PathCombine(params string[] paths) {
        throw new NotImplementedException();
    }

    public void CreateDirectoryIfNotExist(string path, bool hidden = false) {
        throw new NotImplementedException();
    }

    public void FileDelete(string path) {
        throw new NotImplementedException();
    }

    public void FileMove(string sourceFileName, string destFileName) {
        throw new NotImplementedException();
    }

    public void DeleteDirectoryAndFiles(string filePath) {
        throw new NotImplementedException();
    }

    public void MoveFileRemoveFolder(string sourceFileName, string destFileName) {
        throw new NotImplementedException();
    }

    public string GetContainingFolderPath(string filePath) {
        throw new NotImplementedException();
    }

    public string GetContainingParentFolderPath(string filePath) {
        throw new NotImplementedException();
    }

    public string GetFileNameWithoutExtension(string filePath) {
        throw new NotImplementedException();
    }

    public string GetFileNameFromPath(string filePath) {
        throw new NotImplementedException();
    }
}
