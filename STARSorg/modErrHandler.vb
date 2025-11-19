Module modErrHandler
    'useful routines for input validation
    Public Function ValidateTextboxLength(ByRef obj As TextBox, ByRef errP As ErrorProvider) As Boolean
        'this function validates that a text box is not empty
        If obj.Text.Length = 0 Then
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleLeft)
            errP.SetError(obj, "You must enter a value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function ValidateTextboxNumeric(ByRef obj As TextBox, ByRef errP As ErrorProvider) As Boolean
        'this function validates that a textbox has a numeric value
        If Not IsNumeric(obj.Text) Then
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleLeft)
            errP.SetError(obj, "You must enter a numeric value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function ValidateTextboxDate(ByRef obj As TextBox, ByRef errP As ErrorProvider) As Boolean
        'this function validates that a textbox has a valid date value
        If Not IsDate(obj.Text) Then
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleLeft)
            errP.SetError(obj, "You must enter a date value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function ValidateCombo(ByRef obj As ComboBox, ByRef errP As ErrorProvider) As Boolean
        If obj.SelectedIndex = -1 Then 'no selection made
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleLeft)
            errP.SetError(obj, "You make a selection here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function ValidateMaskedTextboxLength(ByRef obj As MaskedTextBox, ByRef errP As ErrorProvider) As Boolean
        If obj.Text.Length = 6 Then
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleLeft)
            errP.SetError(obj, "You must enter a value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function

End Module
