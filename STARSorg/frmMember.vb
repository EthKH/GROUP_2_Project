Imports System.Data.SqlClient
Imports System.IO

Public Class frmMember
    Private myDB As CDB
    Private objMembers As New CMembers
    Private blnClearing As Boolean
    Private blnReloading As Boolean

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
        lstMembers.Items.Clear()
        Try
            objDR = objMembers.GetAllMembers()
            Do While objDR.Read
                lstMembers.Items.Add(objDR.Item("PID"))
            Loop
            objDR.Close()
        Catch ex As Exception

        End Try
        If objMembers.CurrentObject.PID <> "" Then
            lstMembers.SelectedIndex = lstMembers.FindStringExact(objMembers.CurrentObject.PID)
        End If
        blnReloading = False
    End Sub

    Private Sub frmMember_Load(sender As Object, e As EventArgs) Handles Me.Load
        objMembers = New CMembers
    End Sub

    Private Sub frmMember_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ClearScreenControls(Me)
        LoadMembers()
        grpMemberAddUpdate.Enabled = False
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

    Private Sub lstMembers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMembers.SelectedIndexChanged
        If blnClearing Then
            Exit Sub
        End If
        If blnReloading Then
            Exit Sub
        End If
        If lstMembers.SelectedIndex = -1 Then
            Exit Sub
        End If
        chkAdd.Checked = False
        LoadSelectedRecord()
        grpMemberAddUpdate.Enabled = True
    End Sub
    Private Sub LoadSelectedRecord()
        Try
            objMembers.GetMemberByPID(lstMembers.SelectedItem.ToString)
            With objMembers.CurrentObject
                txtPID.Text = .PID
            End With
        Catch ex As Exception
            MessageBox.Show("Error Loading Member Value: " & ex.ToString, "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class