Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.Remoting.Metadata.W3cXsd2001

Public Class frmMember
    Private myDB As CDB
    Private objMembers As New CMembers
    Private blnClearing As Boolean
    Private blnReloading As Boolean
    Private gstrLoggedInSecLevelID As String
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
    Private Sub tsbCourse_Click(sender As Object, e As EventArgs) Handles tsbCourse.Click
        intNextAction = ACTION_COURSE
        Me.Hide()
    End Sub
    Private Sub tsbEvent_Click(sender As Object, e As EventArgs) Handles tsbEvent.Click
        intNextAction = ACTION_EVENT
        Me.Hide()
    End Sub
    Private Sub tsbHome_Click(sender As Object, e As EventArgs) Handles tsbHome.Click
        intNextAction = ACTION_HOME
        Me.Hide()
    End Sub
    Private Sub tsbLogout_Click(sender As Object, e As EventArgs) Handles tsbLogout.Click
        intNextAction = ACTION_LOGOUT
        Me.Hide()
    End Sub
    Private Sub tsbRole_Click(sender As Object, e As EventArgs) Handles tsbRole.Click
        intNextAction = ACTION_ROLE
        Me.Hide()
    End Sub
    Private Sub tsbRSVP_Click(sender As Object, e As EventArgs) Handles tsbRSVP.Click
        intNextAction = ACTION_RSVP
        Me.Hide()
    End Sub

    Private Sub tsbSemester_Click(sender As Object, e As EventArgs) Handles tsbSemester.Click
        intNextAction = ACTION_SEMESTER
        Me.Hide()
    End Sub

    Private Sub tsbTutor_Click(sender As Object, e As EventArgs) Handles tsbTutor.Click
        intNextAction = ACTION_TUTOR
        Me.Hide()
    End Sub
    Private Sub tsbMember_Click(sender As Object, e As EventArgs) Handles tsbMember.Click
        'Already Here
    End Sub
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
    Private Sub LoadPartialSearch()
        Dim objDR As SqlDataReader
        lstMembers.Items.Clear()
        Try
            objDR = objMembers.GetMemberPartialSearch(txtSearch.Text.ToString)
            Do While objDR.Read
                lstMembers.Items.Add(objDR.Item("PID"))
            Loop
            objDR.Close()
        Catch ex As Exception
            MessageBox.Show("Error Loading Member Value: " & ex.ToString, "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If objMembers.CurrentObject.PID <> 0 Then
            lstMembers.SelectedIndex = lstMembers.FindStringExact(objMembers.CurrentObject.PID)
        End If
        blnReloading = False
    End Sub
    Private Sub frmMember_Load(sender As Object, e As EventArgs) Handles Me.Load
        objMembers = New CMembers
    End Sub

    Private Sub frmMember_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'gstrLoggedInSecLevelID = "Guest"
        If gstrLoggedInSecLevelID = "Guest" Then
            intNextAction = ACTION_HOME
            Me.Hide()
        End If
        ClearScreenControls(Me)
        LoadMembers()
        grpMemberAddUpdate.Enabled = False
        txtPhoto.Text = " click to add "
    End Sub

    Private Sub txtPhoto_Click(sender As Object, e As EventArgs) Handles txtPhoto.Click
        'Dim PhotoFile As StreamReader
        txtPhoto.Clear()
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
        'If grpMemberAddUpdate.Enabled = True Then
        '    grpMemberAddUpdate.Enabled = False
        'End If
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
        txtPhoto.Clear()
        Try
            objMembers.GetMemberByPID(lstMembers.SelectedItem.ToString)
            With objMembers.CurrentObject
                txtPID.Text = .PID
                txtFirstName.Text = .FName
                txtLastName.Text = .LName
                txtMiddle.Text = .MI
                txtEmail.Text = .Email
                txtPhone.Text = .Phone
                txtPhoto.Text = .PhotoPath
                'picMemberAdd.ImageLocation = .PhotoPath
            End With
            'MsgBox(Application.StartupPath)
            picMemberAdd.ImageLocation = txtPhoto.Text
        Catch ex As Exception
            MessageBox.Show("Error Loading Member Value: " & ex.ToString, "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'loadparams()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadPartialSearch()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim blnErrors As Boolean
        If Not ValidateTextboxLength(txtPID, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxNumeric(txtPID, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxLength(txtFirstName, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxLength(txtLastName, errP) Then
            blnErrors = True
        End If
        'If Not ValidateTextboxLength(txtMiddle, errP) Then
        '    blnErrors = True
        'End If
        If Not ValidateTextboxLength(txtEmail, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxLength(txtPhone, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxNumeric(txtPhone, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxLength(txtPhoto, errP) Then
            blnErrors = True
        End If
        If blnErrors Then
            Exit Sub
        End If
        If chkAdd.Checked = True And chkUpdate.Checked = True Then
            MessageBox.Show("User selected both modes, this should not be.", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf chkAdd.Checked = False And chkUpdate.Checked = False Then
            MessageBox.Show("Please Select a mode.", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Try
                'push scren data into object using currentobject
                With objMembers.CurrentObject
                    .PID = txtPID.Text
                    .FName = txtFirstName.Text
                    .LName = txtLastName.Text
                    .MI = txtMiddle.Text
                    .Email = txtEmail.Text
                    .Phone = txtPhone.Text
                    .PhotoPath = txtPhoto.Text
                End With
                objMembers.Save()
            Catch ex As Exception
                MessageBox.Show("Error during Save/Update operation" & ex.ToString, "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If

    End Sub

    Private Sub chkAdd_CheckStateChanged(sender As Object, e As EventArgs) Handles chkAdd.CheckStateChanged
        If chkAdd.Checked And chkUpdate.Checked Then
            chkUpdate.Checked = False
        End If
        grpMemberAddUpdate.Enabled = True
    End Sub

    Private Sub chkUpdate_CheckStateChanged(sender As Object, e As EventArgs) Handles chkUpdate.CheckStateChanged
        If chkUpdate.Checked And chkAdd.Checked Then
            chkAdd.Checked = False
        End If
        grpMemberAddUpdate.Enabled = True
    End Sub

    Private Sub btnMemberReport_Click(sender As Object, e As EventArgs) Handles btnMemberReport.Click
        Dim MemberReport As New frmMemberReport
        If lstMembers.Items.Count = 0 Then
            MessageBox.Show("there are no record to print.")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        MemberReport.Display()
        Me.Cursor = Cursors.Default
    End Sub
End Class