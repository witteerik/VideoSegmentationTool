Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure
Imports Emgu.Util
Imports System.Windows.Forms
Imports System.Drawing


Public Class MainForm

    Private CurrentVideo As VideoCapture = Nothing
    Private CurrentVideoFolder As String = ""
    Private CurrentPath As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ImageBox1.SizeMode = PictureBoxSizeMode.Zoom
        Video_TrackBar.Minimum = 0

    End Sub

    Private Sub Folder_Button_Click(sender As Object, e As EventArgs) Handles Folder_Button.Click

        'Getting the folder in which to look for videos
        Dim VideoFolder As New FolderBrowserDialog
        VideoFolder.Description = "Video folder"
        Dim FbdResult = VideoFolder.ShowDialog()
        If FbdResult = DialogResult.OK Then

            CurrentVideoFolder = VideoFolder.SelectedPath

            Dim FilePaths = System.IO.Directory.GetFiles(CurrentVideoFolder)

            Dim FileNames As New List(Of String)

            For Each FilePath In FilePaths
                Dim CurrentExtension = System.IO.Path.GetExtension(FilePath)
                Dim AllowedExtension = "." & Extensions_TextBox.Text.Trim.Replace(".", "")
                If CurrentExtension = AllowedExtension Then
                    FileNames.Add(System.IO.Path.GetFileName(FilePath))
                End If
            Next

            FileName_ListBox.Items.Clear()
            For Each FileName In FileNames
                FileName_ListBox.Items.Add(FileName)
            Next
            If FileName_ListBox.Items.Count > 0 Then
                FileName_ListBox.SelectedIndex = 0
            End If

        End If

    End Sub

    Private Sub FileName_ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FileName_ListBox.SelectedIndexChanged

        If SaveResults() = False Then Exit Sub

        SetNewVideoFile()
    End Sub

    Private Sub Next_Button_Click(sender As Object, e As EventArgs) Handles Next_Button.Click

        If SaveResults() = False Then Exit Sub

        If FileName_ListBox.SelectedIndex = FileName_ListBox.Items.Count - 1 Then
            MsgBox("You allready display the last video!")
            Exit Sub
        Else
            FileName_ListBox.SelectedIndex += 1
        End If

    End Sub

    Private Sub Previous_Button_Click(sender As Object, e As EventArgs) Handles Previous_Button.Click

        If SaveResults() = False Then Exit Sub

        If FileName_ListBox.SelectedIndex = 0 Then
            MsgBox("You allready display the first video!")
            Exit Sub
        Else
            FileName_ListBox.SelectedIndex -= 1
        End If

    End Sub


    Private Sub SetNewVideoFile()

        If FileName_ListBox.SelectedIndex < 0 Then Exit Sub
        If FileName_ListBox.SelectedIndex > FileName_ListBox.Items.Count - 1 Then Exit Sub

        CurrentPath = System.IO.Path.Combine(CurrentVideoFolder, FileName_ListBox.SelectedItems(0))

        If System.IO.File.Exists(CurrentPath) = False Then
            MsgBox("The file " & CurrentPath & " could not be found!", MsgBoxStyle.Exclamation, "File not found!")
            Exit Sub
        End If

        'Loading the new video
        CurrentVideo = New Emgu.CV.VideoCapture(CurrentPath)
        'Setting its location to 0 (probably not necessary!)
        CurrentVideo.Set(CapProp.PosFrames, 0)

        CurrentFrameRate = CurrentVideo.Get(CapProp.Fps)

        'Setting the track bar limits to the number of frames
        Video_TrackBar.Maximum = Math.Max(0, CurrentVideo.Get(CapProp.FrameCount) - 1)
        Video_TrackBar.Value = 0

        'Calling Video_TrackBar_Scroll to display the first frame
        Video_TrackBar_Scroll(Nothing, Nothing)

    End Sub

    Private Sub Video_TrackBar_Scroll(sender As Object, e As EventArgs) Handles Video_TrackBar.Scroll

        If CurrentVideo IsNot Nothing Then

            'Getting the index of the frame to display
            If Video_TrackBar.Value < CurrentVideo.Get(CapProp.FrameCount) Then
                CurrentVideo.Set(CapProp.PosFrames, Video_TrackBar.Value)
            Else
                Exit Sub
            End If

            'Displaying the frame
            'Try
            Dim CurrentImage As Image(Of Bgr, Byte) = New Image(Of Bgr, Byte)(CurrentVideo.Width, CurrentVideo.Height, New Bgr(255, 0, 0))
            CurrentVideo.Read(CurrentImage)
            ImageBox1.Image = CurrentImage
            ImageBox1.Update()
            'Catch ex As Exception
            'MsgBox(ex.ToString)
            'End Try

        End If

    End Sub

    Private CurrentStartFrame As Integer = -1
    Private CurrentEndFrame As Integer = -1
    Private CurrentFrameRate As Integer

    Private Sub LockStart_Button_Click(sender As Object, e As EventArgs) Handles LockStart_Button.Click
        CurrentStartFrame = Video_TrackBar.Value
        Start_TextBox.Text = CurrentStartFrame
    End Sub

    Private Sub LockEnd_Button_Click(sender As Object, e As EventArgs) Handles LockEnd_Button.Click
        CurrentEndFrame = Video_TrackBar.Value
        End_TextBox.Text = CurrentEndFrame
    End Sub

    Private Function SaveResults() As Boolean

        'Saving current results
        If CurrentStartFrame = -1 And CurrentEndFrame = -1 Then
            'Returns True, since there is nothing to save
            Return True
        ElseIf CurrentStartFrame = -1 Or CurrentEndFrame = -1 Then
            'Warns the user that he/she may have missed a segmentation point and aborts
            MsgBox("Did you forget 'start' or 'end' segmentation?")
            Return False
        End If

        Dim ResultMessage As String = String.Join(", ", New List(Of String) From {
                                                  CurrentPath,
                                                  CurrentStartFrame,
                                                  CurrentEndFrame,
                                                  Math.Round(1000 * CurrentStartFrame / CurrentFrameRate),
                                                  Math.Round(1000 * CurrentEndFrame / CurrentFrameRate)})

        Result_TextBox.AppendText(ResultMessage & vbCrLf)

        CurrentStartFrame = -1
        CurrentEndFrame = -1
        Start_TextBox.Text = ""
        End_TextBox.Text = ""

        Return True

    End Function

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        AboutBox1.ShowDialog()

    End Sub

    Private Sub Extensions_TextBox_TextChanged(sender As Object, e As EventArgs) Handles Extensions_TextBox.TextChanged

    End Sub
End Class
