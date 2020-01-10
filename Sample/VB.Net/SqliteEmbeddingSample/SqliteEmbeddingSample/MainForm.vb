Imports System.Data.SQLite
Imports System.Reflection
Imports System.IO
Imports Microsoft.Win32.SafeHandles

Public Class MainForm

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' First of all initialize BoxedApp SDK
        BoxedAppSDK.NativeMethods.BoxedAppSDK_Init()

        ' Note that you can always get list of embedded resources names using GetManifestResourceNames()
        Dim manifestResourceNames() As String = Assembly.GetExecutingAssembly.GetManifestResourceNames

        ' Create virtual files using the data of embedded resources
        CreateVirtualFile(Path.Combine(Application.StartupPath, "System.Data.SQLite.dll"), "SqliteEmbeddingSample.System.Data.SQLite.dll")
        CreateVirtualFile(Path.Combine(Path.Combine(Application.StartupPath, "x64"), "SQLite.Interop.dll"), "SqliteEmbeddingSample.SQLite.Interop.dll.x64")
        CreateVirtualFile(Path.Combine(Path.Combine(Application.StartupPath, "x86"), "SQLite.Interop.dll"), "SqliteEmbeddingSample.SQLite.Interop.dll")

        DoSomeSqliteJob()
    End Sub

    Private Sub DoSomeSqliteJob()
        Dim configDb As String = "sample.db"
        Dim connectionString As String = "Data Source={0};Version=3;"

        If Not My.Computer.FileSystem.FileExists(configDb) Then
            ' Create the SQLite database
            SQLiteConnection.CreateFile(configDb)

            MessageBox.Show("Database Created...")

            Using SQLConn As SQLiteConnection = New SQLiteConnection(connectionString)
                ' Do something
            End Using
        End If
    End Sub

    Private Sub CreateVirtualFile(virtualFilePath As String, embeddedResourceName As String)
        ' Create virtual file and immediate close handle to avoid handle leaks
        Using hHandle As SafeFileHandle = New SafeFileHandle(BoxedAppSDK.NativeMethods.BoxedAppSDK_CreateVirtualFile(
            virtualFilePath,
            BoxedAppSDK.NativeMethods.EFileAccess.GenericWrite,
            BoxedAppSDK.NativeMethods.EFileShare.Read,
            IntPtr.Zero,
            BoxedAppSDK.NativeMethods.ECreationDisposition.CreateAlways,
            BoxedAppSDK.NativeMethods.EFileAttributes.Normal,
            IntPtr.Zero), True)
        End Using

        Using embeddedResourceDataStream As Stream = Assembly.GetExecutingAssembly.GetManifestResourceStream(embeddedResourceName)
            Dim bufferSize As Integer = 1024
            Dim buffer(bufferSize) As Byte
            Dim readBytes As Integer

            Using virtualFileStream As FileStream = New FileStream(virtualFilePath, FileMode.Open)
                While True
                    readBytes = embeddedResourceDataStream.Read(buffer, 0, bufferSize)
                    If readBytes = 0 Then Exit While
                    virtualFileStream.Write(buffer, 0, readBytes)
                End While
            End Using
        End Using
    End Sub
End Class
