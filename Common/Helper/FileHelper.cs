using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="ErrStr"></param>
        public static void WriteLog(string path, bool append, string ErrStr)
        {
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(path)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
            }
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, append))
            {
                writer.WriteLine(ErrStr);

                writer.Flush();

                writer.Dispose();
            }
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTxt(string path)
        {
            if (!System.IO.File.Exists(path))
                return "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
