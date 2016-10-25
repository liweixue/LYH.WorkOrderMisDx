using System.IO;
using System.Xml;

namespace LYH.WorkOrder.share
{
    /// <summary>
    /// FileDirectoryUtility 类,方法不包含异常处理
    /// </summary>
    public class FileDirectoryUtility
    {
        /// <summary>
        /// 路径分割符
        /// </summary>
        private const string PathSplitChar = "\\";

        /// <summary>
        /// 构造函数
        /// </summary>
        private FileDirectoryUtility()
        {
        }

        /// <summary>
        /// 复制指定目录的所有文件,不包含子目录及子目录中的文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,表示覆盖同名文件,否则不覆盖</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite)
        {
            CopyFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// 复制指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        /// <param name="copySubDir">如果为true,包含目录,否则不包含</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir)
        {
            //复制当前目录文件
            foreach (var sourceFileName in Directory.GetFiles(sourceDir))
            {
                var targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PathSplitChar) + 1));

                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Copy(sourceFileName, targetFileName, overWrite);
                    }
                }
                else
                {
                    File.Copy(sourceFileName, targetFileName, overWrite);
                }
            }
            //复制子目录
            if (copySubDir)
            {
                foreach (var sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    var targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PathSplitChar) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    CopyFiles(sourceSubDir, targetSubDir, overWrite, true);
                }
            }
        }

        /// <summary>
        /// 剪切指定目录的所有文件,不包含子目录
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite)
        {
            MoveFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// 剪切指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        /// <param name="moveSubDir">如果为true,包含目录,否则不包含</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite, bool moveSubDir)
        {
            //移动当前目录文件
            foreach (var sourceFileName in Directory.GetFiles(sourceDir))
            {
                var targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PathSplitChar) + 1));
                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Delete(targetFileName);
                        File.Move(sourceFileName, targetFileName);
                    }
                }
                else
                {
                    File.Move(sourceFileName, targetFileName);
                }
            }
            if (moveSubDir)
            {
                foreach (var sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    var targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PathSplitChar) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    MoveFiles(sourceSubDir, targetSubDir, overWrite, true);
                    Directory.Delete(sourceSubDir);
                }
            }
        }

        /// <summary>
        /// 删除指定目录的所有文件，不包含子目录
        /// </summary>
        /// <param name="targetDir">操作目录</param>
        public static void DeleteFiles(string targetDir)
        {
            DeleteFiles(targetDir, false);
        }

        /// <summary>
        /// 删除指定目录的所有文件和子目录
        /// </summary>
        /// <param name="targetDir">操作目录</param>
        /// <param name="delSubDir">如果为true,包含对子目录的操作</param>
        public static void DeleteFiles(string targetDir, bool delSubDir)
        {
            foreach (var fileName in Directory.GetFiles(targetDir))
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
                File.Delete(fileName);
            }
            if (delSubDir)
            {
                var dir = new DirectoryInfo(targetDir);
                foreach (var subDi in dir.GetDirectories())
                {
                    DeleteFiles(subDi.FullName, true);
                    subDi.Delete();
                }
            }
        }

        /// <summary>
        /// 创建指定目录
        /// </summary>
        /// <param name="targetDir"></param>
        public static void CreateDirectory(string targetDir)
        {
            var dir = new DirectoryInfo(targetDir);
            if (!dir.Exists)
                dir.Create();
        }

        /// <summary>
        /// 建立子目录
        /// </summary>
        /// <param name="targetDir">目录路径</param>
        /// <param name="subDirName">子目录名称</param>
        public static void CreateDirectory(string parentDir, string subDirName)
        {
            CreateDirectory(parentDir + PathSplitChar + subDirName);
        }

        /// <summary>
        /// 删除指定目录
        /// </summary>
        /// <param name="targetDir">目录路径</param>
        public static void DeleteDirectory(string targetDir)
        {
            var dirInfo = new DirectoryInfo(targetDir);
            if (dirInfo.Exists)
            {
                DeleteFiles(targetDir, true);
                dirInfo.Delete(true);
            }
        }

        /// <summary>
        /// 删除指定目录的所有子目录,不包括对当前目录文件的删除
        /// </summary>
        /// <param name="targetDir">目录路径</param>
        public static void DeleteSubDirectory(string targetDir)
        {
            foreach (var subDir in Directory.GetDirectories(targetDir))
            {
                DeleteDirectory(subDir);
            }
        }

        /// <summary>
        /// 将指定目录下的子目录和文件生成xml文档
        /// </summary>
        /// <param name="targetDir">根目录</param>
        /// <returns>返回XmlDocument对象</returns>
        public static XmlDocument CreateXml(string targetDir)
        {
            var myDocument = new XmlDocument();
            var declaration = myDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            myDocument.AppendChild(declaration);
            var rootElement = myDocument.CreateElement(targetDir.Substring(targetDir.LastIndexOf(PathSplitChar) + 1));
            myDocument.AppendChild(rootElement);
            foreach (var fileName in Directory.GetFiles(targetDir))
            {
                var childElement = myDocument.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PathSplitChar) + 1);
                rootElement.AppendChild(childElement);
            }
            foreach (var directory in Directory.GetDirectories(targetDir))
            {
                var childElement = myDocument.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PathSplitChar) + 1));
                rootElement.AppendChild(childElement);
                CreateBranch(directory, childElement, myDocument);
            }
            return myDocument;
        }

        /// <summary>
        /// 生成Xml分支
        /// </summary>
        /// <param name="targetDir">子目录</param>
        /// <param name="xmlNode">父目录XmlDocument</param>
        /// <param name="myDocument">XmlDocument对象</param>
        private static void CreateBranch(string targetDir, XmlElement xmlNode, XmlDocument myDocument)
        {
            foreach (var fileName in Directory.GetFiles(targetDir))
            {
                var childElement = myDocument.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PathSplitChar) + 1);
                xmlNode.AppendChild(childElement);
            }
            foreach (var directory in Directory.GetDirectories(targetDir))
            {
                var childElement = myDocument.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PathSplitChar) + 1));
                xmlNode.AppendChild(childElement);
                CreateBranch(directory, childElement, myDocument);
            }
        }
    }
}