Module modObjects
    Public Sub ClearScreenControls(ByVal objContainer As Control)
        'this procedure clear all controls on form passed through as arg
        'not all controkl types have been implamented. add as needed
        Dim obj As Control
        Dim strControlType As String
        For Each obj In objContainer.Controls
            strControlType = TypeName(obj) 'type name returns class name of control
            Select Case strControlType
                Case "TextBox"
                    Dim cntrl As TextBox
                    cntrl = DirectCast(obj, TextBox)
                    cntrl.Clear()
                    'or cntrl.Text=""
                    'or cntrl.text=vbnNullString
                Case "CheckBox"
                    Dim cntrl As CheckBox
                    cntrl = DirectCast(obj, CheckBox)
                    cntrl.Checked = False
                    'or cntrl.CheckedState = CheckState.Unchecked
                Case "ComboBox"
                    Dim cntrl As ComboBox
                    cntrl = DirectCast(obj, ComboBox)
                    cntrl.SelectedIndex = -1
                Case "RadioButton"
                    Dim cntrl As RadioButton
                    cntrl = DirectCast(obj, RadioButton)
                    cntrl.Checked = False
                Case "ListBox"
                    Dim cntrl As ListBox
                    cntrl = DirectCast(obj, ListBox)
                    cntrl.SelectedIndex = -1 'clear slection only not list
                Case "MaskedTextBox"
                    Dim cntrl As MaskedTextBox
                    cntrl = DirectCast(obj, MaskedTextBox)
                    cntrl.Clear()
                Case "GroupBox"
                    'must recursively through control collection
                    Dim cntrl As GroupBox
                    cntrl = DirectCast(obj, GroupBox)
                    ClearScreenControls(cntrl)
                Case Else
                    'do nothing, or error trap if needed
            End Select
        Next
    End Sub
End Module
