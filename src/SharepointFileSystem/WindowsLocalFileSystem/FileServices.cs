using FileSystemAbstraction;

namespace WindowsLocalFileSystem {
    public class FileServices : IFileSystem {
        private const string FILESERVICE = "WindowsLocalFileSystem.FileServices";

        public bool FileExists(string path) {
            return File.Exists(path);
        }

        public bool FileExists(string path, long biggerThanBytes) {
            var fileinfo = new FileInfo(path);
            return (File.Exists(path) && fileinfo.Length > biggerThanBytes);
        }

        public Stream FileCreate(string path) {
            return File.Create(path);
        }

        public bool DirectoryExists(string path) {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path) {
            Directory.CreateDirectory(path);
        }

        public char[] GetInvalidPathChars() {
            return Path.GetInvalidPathChars();
        }

        public char[] GetInvalidFileNameChars() {
            return Path.GetInvalidFileNameChars();
        }
        public string PathCombine(params string[] paths) {
            //never returns on NetStandard2.0
            return Path.Combine(paths);
        }
        public System.IO.Stream FileOpenRead(string path) {
            return File.OpenRead(path);
        }

        public void CreateDirectoryIfNotExist(string path, bool hidden = false) {
            var fileInfo = new FileInfo(path);
            var destFolder = fileInfo.Directory;
            try {
                if (destFolder != null && !this.DirectoryExists(destFolder.FullName)) {
                    destFolder.Create();
                    if (hidden)
                        destFolder.Attributes = destFolder.Attributes & FileAttributes.Hidden;
                }
            }
            catch (Exception ex) {
                throw new Exception($"Unable to access: {path}", ex);
            }
        }

        public void FileDelete(string path) {
            File.Delete(path);
        }

        public void FileMove(string sourceFileName, string destFileName) {
            File.Move(sourceFileName, destFileName);
        }

        public void DeleteDirectoryAndFiles(string filePath) {
            var fileInfo = new FileInfo(filePath);
            var destFolder = fileInfo.Directory;
            if (destFolder != null && !this.DirectoryExists(destFolder.FullName))
                return;

            try {
                if (File.Exists(filePath)) {
                    File.Delete(filePath);
                    fileInfo = new FileInfo(filePath);
                    destFolder = fileInfo.Directory;
                }
            }
            catch (Exception ex) {
                throw new Exception($"Unable to delete file ('{filePath}')", ex);
            }
            bool removefailed = false;
            try {
                destFolder.Delete();
            }
            catch {
                removefailed = true;
            }
            if (removefailed) {
                //wait and try again
                System.Threading.Thread.Sleep(3000);
            }
            try {
                if (destFolder.Exists)
                    destFolder.Delete();
            }
            catch (Exception ex) {
                throw new Exception($"Unable to remove directory('{destFolder}')", ex);
            }
        }

        public void MoveFileRemoveFolder(string sourceFileName, string destFileName) {

            var srcFileInfo = new FileInfo(sourceFileName);
            if (srcFileInfo.Directory == null) throw new NullReferenceException("Folder not found");
            var srcFileInfoDirectory = srcFileInfo.Directory.ToString();
            try {
                if (File.Exists(sourceFileName) && !File.Exists(destFileName)) {
                    File.Move(sourceFileName, destFileName);
                }
                else if (File.Exists(sourceFileName) && File.Exists(destFileName)) {
                    File.Delete(sourceFileName);
                }

                srcFileInfo = new FileInfo(sourceFileName);
            }
            catch (Exception ex) {
                throw new Exception($"Error during cleaning temp files during move '{sourceFileName}' to '{destFileName}'", ex);
            }

            //remove temp folder
            try {
                if (srcFileInfo.Directory != null && srcFileInfo.Directory.Exists)
                    srcFileInfo.Directory.Delete();
            }
            catch (Exception ex) {
                throw new Exception($"Unable to remove directory('{srcFileInfoDirectory}')", ex);
            }
        }

        public string GetContainingFolderPath(string filePath) {
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Directory == null) throw new NullReferenceException("Folder not found");
            var parentDirectoryPath = fileInfo.Directory.ToString();
            return parentDirectoryPath;
        }

        public string GetContainingParentFolderPath(string filePath) {
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Directory == null) throw new NullReferenceException("Folder not found");
            if (fileInfo.Directory.Parent == null) throw new NullReferenceException("Folder Parent not found");
            var parentParentDirectoryPath = fileInfo.Directory.Parent.FullName;
            return parentParentDirectoryPath;
        }

        public string GetFileNameWithoutExtension(string filePath) {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public string GetFileNameFromPath(string filePath) {
            try {
                // only works for filesystem paths, URLs fail
                var f = new FileInfo(filePath);
                return f.Name;
            }
            catch { return filePath; }
        }
    }
}
