using System.Collections.Generic;
using System.IO;
using System.Text;
using SysFile = System.IO.File;

namespace EasyFile
{

    public partial struct File
    {

        public string ReadAllText()
        {
            return ReadAllText(Encoding.UTF8);
        }

        public string ReadAllText(Encoding encoding)
        {
            return SysFile.ReadAllText(FullName, encoding);
        }

        public void WriteAllText(string content)
        {
            WriteAllText(content, Encoding.UTF8);
        }

        public void WriteAllText(string content, Encoding encoding)
        {
            SysFile.WriteAllText(FullName, content, encoding);
        }

        public byte[] ReadAllBytes()
        {
            return SysFile.ReadAllBytes(FullName);
        }

        public void WriteAllBytes(byte[] content)
        {
            SysFile.WriteAllBytes(FullName, content);
        }

        public string[] ReadAllLines()
        {
            return ReadAllLines(Encoding.UTF8);
        }

        public string[] ReadAllLines(Encoding encoding)
        {
            return SysFile.ReadAllLines(FullName, encoding);
        }

        public void WriteAllLines(IEnumerable<string> content)
        {
            WriteAllLines(content, Encoding.UTF8);
        }

        public void WriteAllLines(IEnumerable<string> content, Encoding encoding)
        {
            SysFile.WriteAllLines(FullName, content, encoding);
        }

        public void WriteAllLines(string[] content)
        {
            WriteAllLines(content, Encoding.UTF8);
        }

        public void WriteAllLines(string[] content, Encoding encoding)
        {
            SysFile.WriteAllLines(FullName, content, encoding);
        }

        public FileStream Open(FileMode mode)
        {
            return new FileStream(FullName, mode);
        }

        public FileStream Open(FileMode mode, FileAccess access)
        {
            return new FileStream(FullName, mode, access);
        }

    }

}
