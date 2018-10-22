using System.IO;

namespace SolidPrinciples
{
    public interface IFileLocator
    {
        FileInfo GetFileInfo(int id);
    }
}