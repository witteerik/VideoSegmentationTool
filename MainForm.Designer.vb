<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ImageBox1 = New Emgu.CV.UI.ImageBox()
        Me.Next_Button = New System.Windows.Forms.Button()
        Me.Video_TrackBar = New System.Windows.Forms.TrackBar()
        Me.LockStart_Button = New System.Windows.Forms.Button()
        Me.LockEnd_Button = New System.Windows.Forms.Button()
        Me.Start_TextBox = New System.Windows.Forms.TextBox()
        Me.End_TextBox = New System.Windows.Forms.TextBox()
        Me.Folder_Button = New System.Windows.Forms.Button()
        Me.FileName_ListBox = New System.Windows.Forms.ListBox()
        Me.Extensions_TextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Previous_Button = New System.Windows.Forms.Button()
        Me.Result_TextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Splitter4 = New System.Windows.Forms.Splitter()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.CurrentLocation_Label = New System.Windows.Forms.Label()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.ImageBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Video_TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageBox1
        '
        Me.ImageBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TableLayoutPanel2.SetColumnSpan(Me.ImageBox1, 4)
        Me.ImageBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImageBox1.Location = New System.Drawing.Point(3, 3)
        Me.ImageBox1.Name = "ImageBox1"
        Me.ImageBox1.Size = New System.Drawing.Size(603, 401)
        Me.ImageBox1.TabIndex = 2
        Me.ImageBox1.TabStop = False
        '
        'Next_Button
        '
        Me.Next_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Next_Button.Location = New System.Drawing.Point(185, 450)
        Me.Next_Button.Name = "Next_Button"
        Me.TableLayoutPanel2.SetRowSpan(Me.Next_Button, 2)
        Me.Next_Button.Size = New System.Drawing.Size(176, 54)
        Me.Next_Button.TabIndex = 3
        Me.Next_Button.Text = "Next video"
        Me.Next_Button.UseVisualStyleBackColor = True
        '
        'Video_TrackBar
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.Video_TrackBar, 3)
        Me.Video_TrackBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Video_TrackBar.Location = New System.Drawing.Point(3, 410)
        Me.Video_TrackBar.Name = "Video_TrackBar"
        Me.Video_TrackBar.Size = New System.Drawing.Size(540, 34)
        Me.Video_TrackBar.TabIndex = 4
        '
        'LockStart_Button
        '
        Me.LockStart_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LockStart_Button.Location = New System.Drawing.Point(367, 450)
        Me.LockStart_Button.Name = "LockStart_Button"
        Me.LockStart_Button.Size = New System.Drawing.Size(176, 24)
        Me.LockStart_Button.TabIndex = 5
        Me.LockStart_Button.Text = "Set start:"
        Me.LockStart_Button.UseVisualStyleBackColor = True
        '
        'LockEnd_Button
        '
        Me.LockEnd_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LockEnd_Button.Location = New System.Drawing.Point(367, 480)
        Me.LockEnd_Button.Name = "LockEnd_Button"
        Me.LockEnd_Button.Size = New System.Drawing.Size(176, 24)
        Me.LockEnd_Button.TabIndex = 6
        Me.LockEnd_Button.Text = "Set end:"
        Me.LockEnd_Button.UseVisualStyleBackColor = True
        '
        'Start_TextBox
        '
        Me.Start_TextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Start_TextBox.Location = New System.Drawing.Point(549, 450)
        Me.Start_TextBox.Name = "Start_TextBox"
        Me.Start_TextBox.ReadOnly = True
        Me.Start_TextBox.Size = New System.Drawing.Size(57, 23)
        Me.Start_TextBox.TabIndex = 7
        Me.Start_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'End_TextBox
        '
        Me.End_TextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.End_TextBox.Location = New System.Drawing.Point(549, 480)
        Me.End_TextBox.Name = "End_TextBox"
        Me.End_TextBox.ReadOnly = True
        Me.End_TextBox.Size = New System.Drawing.Size(57, 23)
        Me.End_TextBox.TabIndex = 8
        Me.End_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Folder_Button
        '
        Me.Folder_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Folder_Button.Location = New System.Drawing.Point(3, 3)
        Me.Folder_Button.Name = "Folder_Button"
        Me.Folder_Button.Size = New System.Drawing.Size(227, 24)
        Me.Folder_Button.TabIndex = 9
        Me.Folder_Button.Text = "Select folder"
        Me.Folder_Button.UseVisualStyleBackColor = True
        '
        'FileName_ListBox
        '
        Me.FileName_ListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FileName_ListBox.FormattingEnabled = True
        Me.FileName_ListBox.ItemHeight = 15
        Me.FileName_ListBox.Location = New System.Drawing.Point(3, 85)
        Me.FileName_ListBox.Name = "FileName_ListBox"
        Me.FileName_ListBox.Size = New System.Drawing.Size(227, 419)
        Me.FileName_ListBox.TabIndex = 11
        '
        'Extensions_TextBox
        '
        Me.Extensions_TextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Extensions_TextBox.Location = New System.Drawing.Point(3, 57)
        Me.Extensions_TextBox.Name = "Extensions_TextBox"
        Me.Extensions_TextBox.Size = New System.Drawing.Size(227, 23)
        Me.Extensions_TextBox.TabIndex = 12
        Me.Extensions_TextBox.Text = ".mp4"
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(227, 24)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Allowed file extensions"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Previous_Button
        '
        Me.Previous_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Previous_Button.Location = New System.Drawing.Point(3, 450)
        Me.Previous_Button.Name = "Previous_Button"
        Me.TableLayoutPanel2.SetRowSpan(Me.Previous_Button, 2)
        Me.Previous_Button.Size = New System.Drawing.Size(176, 54)
        Me.Previous_Button.TabIndex = 14
        Me.Previous_Button.Text = "Previous video"
        Me.Previous_Button.UseVisualStyleBackColor = True
        '
        'Result_TextBox
        '
        Me.Result_TextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Result_TextBox.Location = New System.Drawing.Point(3, 3)
        Me.Result_TextBox.Multiline = True
        Me.Result_TextBox.Name = "Result_TextBox"
        Me.Result_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Result_TextBox.Size = New System.Drawing.Size(234, 501)
        Me.Result_TextBox.TabIndex = 15
        Me.Result_TextBox.WordWrap = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Folder_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FileName_ListBox, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Extensions_TextBox, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(233, 507)
        Me.TableLayoutPanel1.TabIndex = 16
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TableLayoutPanel3)
        Me.Panel2.Controls.Add(Me.Splitter4)
        Me.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel2.Controls.Add(Me.Splitter3)
        Me.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 24)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1102, 507)
        Me.Panel2.TabIndex = 18
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Result_TextBox, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(862, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(240, 507)
        Me.TableLayoutPanel3.TabIndex = 20
        '
        'Splitter4
        '
        Me.Splitter4.Location = New System.Drawing.Point(852, 0)
        Me.Splitter4.MinExtra = 10
        Me.Splitter4.MinSize = 10
        Me.Splitter4.Name = "Splitter4"
        Me.Splitter4.Size = New System.Drawing.Size(10, 507)
        Me.Splitter4.TabIndex = 19
        Me.Splitter4.TabStop = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ImageBox1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.End_TextBox, 3, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.LockEnd_Button, 2, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Start_TextBox, 3, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Video_TrackBar, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LockStart_Button, 2, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Next_Button, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Previous_Button, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.CurrentLocation_Label, 3, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(243, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(609, 507)
        Me.TableLayoutPanel2.TabIndex = 18
        '
        'CurrentLocation_Label
        '
        Me.CurrentLocation_Label.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrentLocation_Label.Location = New System.Drawing.Point(549, 407)
        Me.CurrentLocation_Label.Name = "CurrentLocation_Label"
        Me.CurrentLocation_Label.Size = New System.Drawing.Size(57, 40)
        Me.CurrentLocation_Label.TabIndex = 15
        Me.CurrentLocation_Label.Text = "Label2"
        Me.CurrentLocation_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Splitter3
        '
        Me.Splitter3.Location = New System.Drawing.Point(233, 0)
        Me.Splitter3.MinExtra = 10
        Me.Splitter3.MinSize = 10
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(10, 507)
        Me.Splitter3.TabIndex = 17
        Me.Splitter3.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MenuStrip1.Size = New System.Drawing.Size(1102, 24)
        Me.MenuStrip1.TabIndex = 19
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1102, 531)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.Text = "Video Segmentation Tool"
        CType(Me.ImageBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Video_TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ImageBox1 As Emgu.CV.UI.ImageBox
    Friend WithEvents Next_Button As Button
    Friend WithEvents Video_TrackBar As TrackBar
    Friend WithEvents LockStart_Button As Button
    Friend WithEvents LockEnd_Button As Button
    Friend WithEvents Start_TextBox As TextBox
    Friend WithEvents End_TextBox As TextBox
    Friend WithEvents Folder_Button As Button
    Friend WithEvents FileName_ListBox As ListBox
    Friend WithEvents Extensions_TextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Previous_Button As Button
    Friend WithEvents Result_TextBox As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Splitter4 As Splitter
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Splitter3 As Splitter
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CurrentLocation_Label As Label
End Class
