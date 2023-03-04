Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure
Imports Emgu.Util
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Net.Sockets


Public Class MainForm

    Private CurrentVideo As VideoCapture = Nothing
    Private CurrentVideoFolder As String = ""
    Private CurrentPath As String = ""
    Private CurrentStartFrame As Integer = -1
    Private CurrentEndFrame As Integer = -1
    Private CurrentFrameRate As Integer
    Private LinePlot As ControlsLibrary.LineDiagram
    Private IsPlaying As Boolean = False
    Private WithEvents PlayLoopTimer As New Windows.Forms.Timer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ImageBox1.SizeMode = PictureBoxSizeMode.Zoom
        Video_TrackBar.Minimum = 0
        CurrentLocation_Label.Text = ""

        Dim LinePlotPanel As New Panel
        LinePlotPanel.Dock = DockStyle.Fill
        LinePlotPanel.Padding = New Padding(13, 0, 13, 0)
        Video_TableLayoutPanel.Controls.Add(LinePlotPanel, 0, 1)
        Video_TableLayoutPanel.SetColumnSpan(LinePlotPanel, 3)

        LinePlot = New ControlsLibrary.LineDiagram With {.PlotAreaRelativeMarginLeft = 0, .PlotAreaRelativeMarginRight = 0}
        'LinePlot.PlotAreaBorderColor = Color.Transparent
        LinePlot.Dock = DockStyle.Fill
        LinePlotPanel.Controls.Add(LinePlot)
        LinePlot.BorderStyle = BorderStyle.FixedSingle

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

            FileCount_Label.Text = FileName_ListBox.Items.Count & " files"

        End If

    End Sub

    Private Sub FileName_ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FileName_ListBox.SelectedIndexChanged

        'Stopping playback
        If IsPlaying = True Then
            PlayLoopTimer.Stop()
            IsPlaying = False
            EnableControls(True)
        End If

        If SaveResults() = False Then Exit Sub

        SetNewVideoFile()
    End Sub

    Private Sub Next_Button_Click(sender As Object, e As EventArgs) Handles Next_Button.Click

        'Stopping playback
        If IsPlaying = True Then
            PlayLoopTimer.Stop()
            IsPlaying = False
            EnableControls(True)
        End If

        If SaveResults() = False Then Exit Sub

        If FileName_ListBox.SelectedIndex = FileName_ListBox.Items.Count - 1 Then
            MsgBox("You allready display the last video!")
            Exit Sub
        Else
            FileName_ListBox.SelectedIndex += 1
        End If

    End Sub

    Private Sub Previous_Button_Click(sender As Object, e As EventArgs) Handles Previous_Button.Click

        'Stopping playback
        If IsPlaying = True Then
            PlayLoopTimer.Stop()
            IsPlaying = False
            EnableControls(True)
        End If

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

        CheckAndUnlockPlayButtons()
        EnableControls(False)

        LinePlot.ClearLines()

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

        'Looks in the Result_TextBox for segmentation data
        LookForSegmentationResult()

        'Calculates and draws the difference vector
        If DistanceVector_CheckBox.Checked = True Then
            Dim DifferenceVector = CalculateNormalizedDifferenceVector()
            LinePlot.DrawLineAndPointData(DifferenceVector.Item1.ToArray, DifferenceVector.Item2.ToArray)
        End If

        EnableControls(True)
        CheckAndUnlockPlayButtons()

        'Runs autoplay if any of the autoplay checkboxes are checked
        AutoPlay()

    End Sub

    Public Function CalculateNormalizedDifferenceVector() As Tuple(Of List(Of Single), List(Of Single))

        Dim ResultListX As New List(Of Single)
        Dim ResultListY As New List(Of Single)

        If CurrentVideo IsNot Nothing Then

            Try

                Dim Ymax As Single = Single.Epsilon

                'Getting the index of the frame to display
                If Video_TrackBar.Value < CurrentVideo.Get(CapProp.FrameCount) Then
                End If

                'Displaying the frame
                'Try

                Dim FrameCount As Integer = CurrentVideo.Get(CapProp.FrameCount)

                'Returns the empty list if no, or only one frame exist
                If FrameCount < 8 Then
                    Return New Tuple(Of List(Of Single), List(Of Single))(ResultListX, ResultListY)
                End If

                For frameIndex = 1 To FrameCount - 2 Step 6

                    CurrentVideo.Set(CapProp.PosFrames, frameIndex)
                    Dim FirstImage As Image(Of Bgr, Byte) = New Image(Of Bgr, Byte)(CurrentVideo.Width, CurrentVideo.Height, New Bgr(255, 0, 0))
                    CurrentVideo.Read(FirstImage)

                    CurrentVideo.Set(CapProp.PosFrames, frameIndex + 1)
                    Dim SecondImage As Image(Of Bgr, Byte) = New Image(Of Bgr, Byte)(CurrentVideo.Width, CurrentVideo.Height, New Bgr(255, 0, 0))
                    CurrentVideo.Read(SecondImage)

                    Dim AbsoluteDifference = FirstImage.AbsDiff(SecondImage)
                    Dim ColorChannelSum = AbsoluteDifference.GetSum()
                    Dim ColorDifferenceSum As Single = Math.Min(Single.MaxValue, ColorChannelSum.Blue + ColorChannelSum.Red + ColorChannelSum.Green)

                    Ymax = Math.Max(Ymax, ColorDifferenceSum)

                    ResultListY.Add(ColorDifferenceSum)
                    ResultListX.Add(frameIndex)

                Next

                'Normalizing
                For i = 0 To ResultListY.Count - 1
                    ResultListY(i) /= Ymax
                Next

            Catch ex As Exception
                'Returns an empty list if something went wrong
                Return New Tuple(Of List(Of Single), List(Of Single))(New List(Of Single), New List(Of Single))

            End Try

        End If

        'System.IO.File.WriteAllText("C:\Temp\Segm.txt", String.Join(vbCrLf, ResultList))

        Return New Tuple(Of List(Of Single), List(Of Single))(ResultListX, ResultListY)

    End Function


    ''' <summary>
    ''' Returns a list of segmentations results that match the current file name
    ''' </summary>
    ''' <returns></returns>
    Private Function GetCurrentPathSegmentationResults() As List(Of String)

        Dim SegmentationLines = Result_TextBox.Lines

        Dim DetectedSegmentationsList As New List(Of String)

        For Each Line In SegmentationLines
            If Line.Trim.StartsWith(System.IO.Path.GetFileName(CurrentPath)) Then
                DetectedSegmentationsList.Add(Line)
            End If
        Next

        Return DetectedSegmentationsList

    End Function

    Private Sub LookForSegmentationResult()

        Dim DetectedSegmentationsList = GetCurrentPathSegmentationResults()

        If DetectedSegmentationsList.Count > 1 Then
            MsgBox("There are multiple segmentations of " & CurrentPath & vbCrLf & vbCrLf & "The last one in the list will be used!", MsgBoxStyle.Information, "Multiple segmentations!")
        End If

        If DetectedSegmentationsList.Count = 0 Then
            'Exits sub if no segmentation is found
            Exit Sub
        End If

        Dim SegmentationSplit = DetectedSegmentationsList(DetectedSegmentationsList.Count - 1).Split(",")

        'Using the fourth and third slots from the end, as the filename may contain commas
        If SegmentationSplit.Length > 4 Then
            Dim StartIndex As Integer
            If Integer.TryParse(SegmentationSplit(SegmentationSplit.Length - 4), StartIndex) = True Then
                CurrentStartFrame = StartIndex
                Start_TextBox.Text = CurrentStartFrame
            End If

            Dim EndIndex As Integer
            If Integer.TryParse(SegmentationSplit(SegmentationSplit.Length - 3), EndIndex) = True Then
                CurrentEndFrame = EndIndex
                End_TextBox.Text = CurrentEndFrame
            End If
        End If

    End Sub

    Private Sub Video_TrackBar_Scroll(sender As Object, e As EventArgs) Handles Video_TrackBar.ValueChanged

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

            CurrentLocation_Label.Text = Video_TrackBar.Value & " / " & Video_TrackBar.Maximum
            CurrentLocation_Label.Update()

        End If

    End Sub

    Private Sub LockStart_Button_Click(sender As Object, e As EventArgs) Handles LockStart_Button.Click
        CurrentStartFrame = Video_TrackBar.Value
        Start_TextBox.Text = CurrentStartFrame
        CheckAndUnlockPlayButtons()
    End Sub

    Private Sub CheckAndUnlockPlayButtons()
        If HasSegmentation() = True Then
            Play1_Button.Enabled = True
            Play1_Rev_Button.Enabled = True
            Play2_Button.Enabled = True
            Play2_Rev_Button.Enabled = True
            Play3_Button.Enabled = True
            Play3_Rev_Button.Enabled = True
        Else
            Play1_Button.Enabled = False
            Play1_Rev_Button.Enabled = False
            Play2_Button.Enabled = False
            Play2_Rev_Button.Enabled = False
            Play3_Button.Enabled = False
            Play3_Rev_Button.Enabled = False
        End If
    End Sub

    Private Sub LockEnd_Button_Click(sender As Object, e As EventArgs) Handles LockEnd_Button.Click
        CurrentEndFrame = Video_TrackBar.Value
        End_TextBox.Text = CurrentEndFrame
        CheckAndUnlockPlayButtons()
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
                                                  System.IO.Path.GetFileName(CurrentPath),
                                                  CurrentStartFrame,
                                                  CurrentEndFrame,
                                                  Math.Round(1000 * CurrentStartFrame / CurrentFrameRate),
                                                  Math.Round(1000 * CurrentEndFrame / CurrentFrameRate)})

        'Only saves results that do not already exist in the list
        Dim DetectedSegmentationsList = GetCurrentPathSegmentationResults()

        Dim AlreadyExist As Boolean = False

        For Each Item In DetectedSegmentationsList
            If Item.Trim = ResultMessage.Trim Then
                AlreadyExist = True
                Exit For
            End If
        Next

        If AlreadyExist = False Then
            Result_TextBox.AppendText(ResultMessage & vbCrLf)
        End If

        'Resets values
        CurrentStartFrame = -1
        CurrentEndFrame = -1
        Start_TextBox.Text = ""
        End_TextBox.Text = ""
        CurrentLocation_Label.Text = ""

        Return True

    End Function

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        AboutBox1.ShowDialog()

    End Sub


    Private Sub Play_Button_Click(sender As Object, e As EventArgs) Handles Play1_Button.Click, Play1_Rev_Button.Click, Play2_Button.Click, Play2_Rev_Button.Click, Play3_Button.Click, Play3_Rev_Button.Click

        Dim CastSender = DirectCast(sender, ControlsLibrary.PlayButton)

        Select Case CastSender.ViewMode
            Case ControlsLibrary.PlayButton.ViewModes.Play, ControlsLibrary.PlayButton.ViewModes.Reverse

                Select Case CastSender.Name
                    Case Play1_Button.Name
                        PlaySection(PlayTypes.Section1)
                    Case Play1_Rev_Button.Name
                        PlaySection(PlayTypes.Section1Rev)
                    Case Play2_Button.Name
                        PlaySection(PlayTypes.Section2)
                    Case Play2_Rev_Button.Name
                        PlaySection(PlayTypes.Section2Rev)
                    Case Play3_Button.Name
                        PlaySection(PlayTypes.Section3)
                    Case Play3_Rev_Button.Name
                        PlaySection(PlayTypes.Section3Rev)
                End Select

            Case ControlsLibrary.PlayButton.ViewModes.Stop

                'Stopping playback
                PlayLoopTimer.Stop()
                IsPLaying = False
                EnableControls(True)

        End Select

    End Sub


    Private PlayEndFrame As Integer = -1

    Private Function HasSegmentation() As Boolean

        If CurrentStartFrame = -1 Or CurrentEndFrame = -1 Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Enum PlayTypes
        Section1
        Section1Rev
        Section2
        Section2Rev
        Section3
        Section3Rev
    End Enum

    Private CurrentPlayType As PlayTypes

    Private Sub PlaySection(ByVal PlayType As PlayTypes)

        CurrentPlayType = PlayType

        If HasSegmentation() = False Then
            'Exits without warning if segmentation in incomplete
            Play1_Button.Enabled = False
            Play1_Rev_Button.Enabled = False
            Play2_Button.Enabled = False
            Play2_Rev_Button.Enabled = False
            Play3_Button.Enabled = False
            Play3_Rev_Button.Enabled = False
            Exit Sub
        End If

        IsPlaying = True

        EnableControls(False)

        Select Case PlayType

            Case PlayTypes.Section1
                Video_TrackBar.Value = 0
                PlayEndFrame = CurrentStartFrame
                Play1_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Stop
                Play1_Button.Enabled = True
            Case PlayTypes.Section1Rev
                Video_TrackBar.Value = CurrentStartFrame
                PlayEndFrame = 0
                Play1_Rev_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Stop
                Play1_Rev_Button.Enabled = True

            Case PlayTypes.Section2
                Video_TrackBar.Value = CurrentStartFrame
                PlayEndFrame = CurrentEndFrame
                Play2_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Stop
                Play2_Button.Enabled = True
            Case PlayTypes.Section2Rev
                Video_TrackBar.Value = CurrentEndFrame
                PlayEndFrame = CurrentStartFrame
                Play2_Rev_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Stop
                Play2_Rev_Button.Enabled = True

            Case PlayTypes.Section3
                Video_TrackBar.Value = CurrentEndFrame
                PlayEndFrame = Video_TrackBar.Maximum
                Play3_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Stop
                Play3_Button.Enabled = True
            Case PlayTypes.Section3Rev
                Video_TrackBar.Value = Video_TrackBar.Maximum
                PlayEndFrame = CurrentEndFrame
                Play3_Rev_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Stop
                Play3_Rev_Button.Enabled = True

            Case Else
                IsPlaying = False
                EnableControls(False)
                Exit Sub
        End Select

        'Setting timer interval depending on the vidoe frame rate
        PlayLoopTimer.Interval = Math.Max(1, 1000 * (1 / CurrentFrameRate))

        PlayLoopTimer.Start()

    End Sub


    Private Sub PlayLoopTimer_Tick() Handles PlayLoopTimer.Tick


        Select Case CurrentPlayType
            Case PlayTypes.Section1Rev, PlayTypes.Section2Rev, PlayTypes.Section3Rev
                'This code bit is duplicated also initially in this sub, if it happens to be called before the value gets updated
                If Video_TrackBar.Value <= PlayEndFrame Then
                    PlayLoopTimer.Stop()
                    IsPLaying = False
                    EnableControls(True)
                    Exit Sub
                End If

                Video_TrackBar.Value -= 1

                If Video_TrackBar.Value <= PlayEndFrame Then
                    PlayLoopTimer.Stop()
                    IsPLaying = False
                    EnableControls(True)
                End If

            Case Else
                'This code bit is duplicated also initially in this sub, if it happens to be called before the value gets updated
                If Video_TrackBar.Value >= PlayEndFrame Then
                    PlayLoopTimer.Stop()
                    IsPLaying = False
                    EnableControls(True)
                    Exit Sub
                End If

                Video_TrackBar.Value += 1

                If Video_TrackBar.Value >= PlayEndFrame Then
                    PlayLoopTimer.Stop()
                    IsPLaying = False
                    EnableControls(True)
                End If

        End Select


    End Sub

    Private Sub EnableControls(ByVal Enabled As Boolean)

        'Resetting to Play look an every call
        Play1_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Play
        Play1_Rev_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Reverse
        Play2_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Play
        Play2_Rev_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Reverse
        Play3_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Play
        Play3_Rev_Button.ViewMode = ControlsLibrary.PlayButton.ViewModes.Reverse

        If Enabled = True Then
            CheckAndUnlockPlayButtons()
        Else
            Play1_Button.Enabled = Enabled
            Play1_Rev_Button.Enabled = Enabled
            Play2_Button.Enabled = Enabled
            Play2_Rev_Button.Enabled = Enabled
            Play3_Button.Enabled = Enabled
            Play3_Rev_Button.Enabled = Enabled
        End If

        Folder_Button.Enabled = Enabled
        Extensions_TextBox.Enabled = Enabled
        FileName_ListBox.Enabled = Enabled
        Result_TextBox.Enabled = Enabled
        LockStart_Button.Enabled = Enabled
        LockEnd_Button.Enabled = Enabled


        If IsPLaying = False Then
            Previous_Button.Enabled = Enabled
            Next_Button.Enabled = Enabled
        Else
            'These are allways enabled during playback, so to enable switching to next/previous without pressing stop
            Previous_Button.Enabled = True
            Next_Button.Enabled = True
        End If

    End Sub


    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        'Stores first the start frame and then the end frame on space keydown, but never overwrites
        If e.KeyData = Keys.Space Then
            If CurrentStartFrame = -1 Then
                CurrentStartFrame = Video_TrackBar.Value
                Start_TextBox.Text = CurrentStartFrame
            Else
                If CurrentEndFrame = -1 Then
                    CurrentEndFrame = Video_TrackBar.Value
                    End_TextBox.Text = CurrentEndFrame
                End If
            End If
        End If

        CheckAndUnlockPlayButtons()

    End Sub

    Private Sub Filter_Button_Click(sender As Object, e As EventArgs) Handles Filter_Button.Click

        SaveResults()

        'Filter out the files not included in the results text box
        Dim KeepList As New List(Of String)

        Dim SegmentationLines = Result_TextBox.Lines.ToList

        For Each Item In SegmentationLines
            If Item.Trim = "" Then Continue For
            Dim SegmentationSplitList = Item.Trim.Split(",").ToList
            If SegmentationSplitList.Count >= 5 Then

                'Using this odd method, since the filename may contain commas, causing the split to contain more than five slots
                Dim FileName = String.Join(",", SegmentationSplitList.GetRange(0, SegmentationSplitList.Count - 4))
                If FileName.Trim = "" Then Continue For
                If FileName_ListBox.Items.Contains(FileName.Trim) Then
                    KeepList.Add(FileName.Trim)
                End If
            End If
        Next

        'Deselecting video
        CurrentPath = ""
        FileName_ListBox.SelectedIndex = -1

        FileName_ListBox.Items.Clear()
        For Each FileName In KeepList
            FileName_ListBox.Items.Add(FileName)
        Next
        If FileName_ListBox.Items.Count > 0 Then
            FileName_ListBox.SelectedIndex = 0
        End If

        FileCount_Label.Text = FileName_ListBox.Items.Count & " files"

        SetNewVideoFile()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles DistanceVector_CheckBox.CheckedChanged

        SetNewVideoFile()

    End Sub

    Private Sub Start_TextBox_Click(sender As Object, e As EventArgs) Handles Start_TextBox.Click

        If CurrentStartFrame > -1 Then
            Video_TrackBar.Value = CurrentStartFrame
        End If

    End Sub

    Private Sub End_TextBox_Click(sender As Object, e As EventArgs) Handles End_TextBox.Click

        If CurrentEndFrame > -1 Then
            Video_TrackBar.Value = CurrentEndFrame
        End If

    End Sub

    Private Sub AutoPlay_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AutoPlay_1R_CheckBox.CheckedChanged, AutoPlay_1R_CheckBox.CheckedChanged


    End Sub

    Private Sub AutoPlay_1R_CheckBox_Click(sender As Object, e As EventArgs) Handles AutoPlay_3R_CheckBox.Click, AutoPlay_3F_CheckBox.Click, AutoPlay_2R_CheckBox.Click, AutoPlay_2F_CheckBox.Click, AutoPlay_1R_CheckBox.Click, AutoPlay_1F_CheckBox.Click

        Dim CastSender = DirectCast(sender, CheckBox)

        If CastSender.Checked = True Then
            If CastSender IsNot AutoPlay_1R_CheckBox Then AutoPlay_1R_CheckBox.Checked = False
            If CastSender IsNot AutoPlay_1F_CheckBox Then AutoPlay_1F_CheckBox.Checked = False
            If CastSender IsNot AutoPlay_2R_CheckBox Then AutoPlay_2R_CheckBox.Checked = False
            If CastSender IsNot AutoPlay_2F_CheckBox Then AutoPlay_2F_CheckBox.Checked = False
            If CastSender IsNot AutoPlay_3R_CheckBox Then AutoPlay_3R_CheckBox.Checked = False
            If CastSender IsNot AutoPlay_3F_CheckBox Then AutoPlay_3F_CheckBox.Checked = False
        End If

    End Sub

    Public Sub AutoPlay()

        If AutoPlay_1R_CheckBox.Checked = True Then
            PlaySection(PlayTypes.Section1Rev)
            Exit Sub
        End If

        If AutoPlay_1F_CheckBox.Checked = True Then
            PlaySection(PlayTypes.Section1)
            Exit Sub
        End If

        If AutoPlay_2R_CheckBox.Checked = True Then
            PlaySection(PlayTypes.Section2Rev)
            Exit Sub
        End If

        If AutoPlay_2F_CheckBox.Checked = True Then
            PlaySection(PlayTypes.Section2)
            Exit Sub
        End If

        If AutoPlay_3R_CheckBox.Checked = True Then
            PlaySection(PlayTypes.Section3Rev)
            Exit Sub
        End If

        If AutoPlay_3F_CheckBox.Checked = True Then
            PlaySection(PlayTypes.Section3)
            Exit Sub
        End If

    End Sub


End Class
