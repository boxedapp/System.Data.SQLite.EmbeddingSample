using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace SqliteEmbeddingSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // First of all initialize BoxedApp SDK
            BoxedAppSDK.NativeMethods.BoxedAppSDK_Init();

            // Note that you can always get list of embedded resources names using GetManifestResourceNames()
            string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            // Create virtual files using the data of embedded resources
            CreateVirtualFile(Path.Combine(Application.StartupPath, "System.Data.SQLite.dll"), "SqliteEmbeddingSample.EmbeddedResources.System.Data.SQLite.dll");
            CreateVirtualFile(Path.Combine(Path.Combine(Application.StartupPath, "x64"), "SQLite.Interop.dll"), "SqliteEmbeddingSample.EmbeddedResources.x64.SQLite.Interop.dll");
            CreateVirtualFile(Path.Combine(Path.Combine(Application.StartupPath, "x86"), "SQLite.Interop.dll"), "SqliteEmbeddingSample.EmbeddedResources.x86.SQLite.Interop.dll");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void CreateVirtualFile(string virtualFilePath, string embeddedResourceName)
        {
            // Create virtual file and immediate close handle to avoid handle leaks
            using (SafeFileHandle hHandle =
                new SafeFileHandle(
                    BoxedAppSDK.NativeMethods.BoxedAppSDK_CreateVirtualFile(
                        virtualFilePath,
                        BoxedAppSDK.NativeMethods.EFileAccess.GenericWrite,
                        BoxedAppSDK.NativeMethods.EFileShare.Read,
                        IntPtr.Zero,
                        BoxedAppSDK.NativeMethods.ECreationDisposition.New,
                        BoxedAppSDK.NativeMethods.EFileAttributes.Normal,
                        IntPtr.Zero
                    ),
                    true
                )
            )
            {
            }

            using (Stream embeddedResourceDataStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourceName))
            {
                const int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];

                int readBytes;

                using (FileStream virtualFileStream = new FileStream(virtualFilePath, FileMode.Open))
                {
                    while ((readBytes = embeddedResourceDataStream.Read(buffer, 0, bufferSize)) > 0)
                        virtualFileStream.Write(buffer, 0, readBytes);
                }
            }
        }
    }
}
