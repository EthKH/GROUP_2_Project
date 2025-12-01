Imports System.Data.SqlClient
Imports System.IO

Public Class frmMember
    Private myDB As CDB
    Private objMember As New CMember

#Region "Toolbars"
    Private Sub tsbCourse_MouseEnter(sender As Object, e As EventArgs) Handles tsbCourse.MouseEnter, tsbEvent.MouseEnter, tsbHelp.MouseEnter, tsbHome.MouseEnter, tsbLogout.MouseEnter, tsbMember.MouseEnter, tsbRole.MouseEnter, tsbRSVP.MouseEnter, tsbSemester.MouseEnter, tsbTutor.MouseEnter
        'need to be done one due to the img being non-img images and in the bckgrd imgae slot
        Dim tsbProxy As ToolStripButton
        tsbProxy = CType(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Text
    End Sub
    Private Sub tsbCourse_MouseLeave(sender As Object, e As EventArgs) Handles tsbCourse.MouseLeave, tsbEvent.MouseLeave, tsbHelp.MouseLeave, tsbHome.MouseLeave, tsbLogout.MouseLeave, tsbMember.MouseLeave, tsbRole.MouseLeave, tsbRSVP.MouseLeave, tsbSemester.MouseLeave, tsbTutor.MouseLeave
        Dim tsbProxy As ToolStripButton
        tsbProxy = CType(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Image
    End Sub
    'Private Sub tsbLogout_Click(sender As Object, e As EventArgs) Handles tsbLogout.Click
    '    EndProgram()
    'End Sub
#End Region
    Private Sub LoadMembers()
        Dim objDR As SqlDataReader
        'Dim n As Integer
        'n = 0
        lstMembers.Items.Clear()

        Try
            objDR = objMember.GetAllMembers()
            Do While objDR.Read
                lstMembers.Items.Add(objDR.Item("PID"))
                'lstMembers.Items.Insert(n, objDR.Item("FName"))
                'n += 1
            Loop
            objDR.Close()
        Catch ex As Exception

        End Try
        'If objMember.CurrentObject.
    End Sub

    Private Sub frmMember_Load(sender As Object, e As EventArgs) Handles Me.Load
        objMember = New CMember
    End Sub

    Private Sub frmMember_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ClearScreenControls(Me)
        LoadMembers()
    End Sub

    Private Sub txtPhoto_Click(sender As Object, e As EventArgs) Handles txtPhoto.Click
        Dim PhotoFile As StreamReader
        Dim photo As String
        ofdPhoto.ShowDialog()
        If ofdPhoto.ShowDialog() = DialogResult.OK Then
            Try
                picMemberAdd.Image = Image.FromFile(ofdPhoto.FileName)
                photo = ofdPhoto.FileName
                txtPhoto.Text = photo
            Catch ex As Exception
                Exit Sub
            End Try
        End If
    End Sub
End Class