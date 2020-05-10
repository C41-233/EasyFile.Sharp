using SysFile = System.IO.File;
using SysDirectory = System.IO.Directory;

namespace EasyFile
{
    public partial struct File
    {

        public void Delete()
        {
            if (IsFile)
            {
                SysFile.Delete(FullName);
            }
            else if (IsDirectory)
            {
                SysDirectory.Delete(FullName, true);
            }
        }

        public void CreateDirectory()
        {
            SysDirectory.CreateDirectory(FullName);
        }

        public void CleanDirectory()
        {
            CheckIsDirectory();

            foreach (var file in GetFiles())
            {
                file.Delete();
            }
        }

        public void CopyTo(File target)
        {
            if (IsFile)
            {
                SysFile.Copy(FullName, target.FullName);
            }
            else if (IsDirectory)
            {
                if (target.IsDirectory)
                {
                    target.CleanDirectory();
                }
                else
                {
                    target.CreateDirectory();
                }
                foreach (var file in GetFiles())
                {
                    file.CopyTo(new File(target, file.Name));
                }
            }
        }

    }
}
