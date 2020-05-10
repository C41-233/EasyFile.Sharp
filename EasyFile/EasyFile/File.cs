using System;
using System.IO;
using SysFile = System.IO.File;
using SysDirectory = System.IO.Directory;

namespace EasyFile
{

    public partial struct File : IEquatable<File>
    {

        private readonly FileInfo mFile;

        public File(string path)
        {
            mFile = new FileInfo(path);
        }

        public File(File parent, string path)
        : this(Path.Combine(parent.FullName, path))
        {
        }

        public File(File parent, string path1, string path2)
        : this(Path.Combine(parent.FullName, path1, path2))
        {
        }

        public File(File parent, string path1, string path2, string path3)
            : this(Path.Combine(parent.FullName, path1, path2, path3))
        {
        }

        public File(File parent, params string[] paths)
        {
            var newArray = new string[paths.Length + 1];
            newArray[0] = parent.FullName;
            Array.Copy(paths, 0, newArray, 1, paths.Length);
            mFile = new FileInfo(Path.Combine(newArray));
        }

        public string FullName => mFile.FullName;

        public string Name => mFile.Name;

        public string PlainName
        {
            get
            {
                var i = Name.IndexOf('.');
                if (i < 0)
                {
                    return Name;
                }

                return Name.Substring(0, i);
            }
        }

        public string Extension
        {
            get
            {
                var i = Name.IndexOf('.');
                if (i < 0)
                {
                    return null;
                }

                if (i == Name.Length - 1)
                {
                    return "";
                }

                return Name.Substring(i + 1);
            }
        }

        public bool IsFile => SysFile.Exists(FullName);

        public bool IsDirectory => SysDirectory.Exists(FullName);

        private void CheckIsDirectory()
        {
            if (!IsDirectory)
            {
                throw new FileNotFoundException($"{FullName} is not a directory");
            }
        }

        public File Parent
        {
            get
            {
                var parent = SysDirectory.GetParent(FullName);
                return new File(parent.FullName);
            }
        }

        public File[] GetFiles()
        {
            CheckIsDirectory();

            var directories = SysDirectory.GetDirectories(FullName);
            var files = SysDirectory.GetFiles(FullName);
            var rst = new File[directories.Length + files.Length];
            var i = 0;
            foreach (var file in directories)
            {
                rst[i++] = new File(file);
            }

            foreach (var file in files)
            {
                rst[i++] = new File(file);
            }
            return rst;
        }

        public string[] GetFileNames()
        {
            CheckIsDirectory();

            var directories = SysDirectory.GetDirectories(FullName);
            var files = SysDirectory.GetFiles(FullName);
            var rst = new string[directories.Length + files.Length];
            Array.Copy(directories, 0, rst, 0 ,directories.Length);
            Array.Copy(files, 0, rst, directories.Length, files.Length);
            return rst;
        }

        public long Size
        {
            get
            {
                if (IsFile)
                {
                    return mFile.Length;
                }
                if (IsDirectory)
                {
                    long total = 0;
                    foreach (var file in GetFiles())
                    {
                        total += file.Size;
                    }

                    return total;
                }
                throw new FileNotFoundException(FullName);
            }
        }

        public DateTime LastAccessTime
        {
            get
            {
                if (IsFile)
                {
                    return SysFile.GetLastAccessTime(FullName);
                }

                if (IsDirectory)
                {
                    return SysDirectory.GetLastAccessTime(FullName);
                }

                throw new FileNotFoundException(FullName);
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
                if (IsFile)
                {
                    return SysFile.GetLastWriteTime(FullName);
                }

                if (IsDirectory)
                {
                    return SysDirectory.GetLastWriteTime(FullName);
                }

                throw new FileNotFoundException(FullName);
            }
        }

        public bool Equals(File other)
        {
            return Equals(FullName, other.FullName);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj is File other && Equals(other);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        public static bool operator ==(File left, File right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(File left, File right)
        {
            return !left.Equals(right);
        }
    }

}
