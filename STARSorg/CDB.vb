Imports System.Data.SqlClient
Public Class CDB
    Public Function OpenDB() As Boolean
        objSQLCommand = New SqlCommand 'instantiate command obj
        Dim blnResult As Boolean
        If objSQLConn Is Nothing Then 'need to instantiate connection obj
            Try
                objSQLConn = New SqlConnection(gstrConn)
                objSQLConn.Open()
                blnResult = True
            Catch exOpenConnError As Exception
                MessageBox.Show("Cannot open DB: " & exOpenConnError.ToString, " Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                blnResult = False
                'error should be logged
            End Try
        Else
            If objSQLConn.State = ConnectionState.Open Then
                Return True
            Else
                Return False
            End If
        End If
        Return blnResult
    End Function
    Public Sub CloseDB()
        Try
            objSQLConn.Close()
        Catch ex As Exception
            MessageBox.Show("Error when attempting to close DB: " & ex.ToString, " DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function GetDataReaderBySP(ByVal strSP As String, ByRef params As ArrayList) As SqlDataReader
        If Not OpenDB() Then
            'error log this
            Return Nothing
        End If
        objSQLCommand = New SqlCommand(strSP, objSQLConn)
        objSQLCommand.CommandType = CommandType.StoredProcedure
        If Not params Is Nothing Then
            For Each p As SqlParameter In params
                objSQLCommand.Parameters.Add(p)
            Next
        End If
        Try
            Return objSQLCommand.ExecuteReader
        Catch ex As Exception
            MessageBox.Show("Error getting data reader: " & ex.ToString, " DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function
    Public Function GetDataAdapterbySP(ByVal strSP As String, ByRef params As ArrayList) As SqlDataAdapter
        objSQLCommand = New SqlCommand(strSP, objSQLConn)
        Dim sqlDA As SqlDataAdapter
        objSQLCommand.CommandType = CommandType.StoredProcedure
        If Not params Is Nothing Then
            For Each p As SqlParameter In params
                objSQLCommand.Parameters.Add(p)
            Next
        End If
        Try
            sqlDA = New SqlDataAdapter(objSQLCommand)
            Return sqlDA
        Catch ex As Exception
            MessageBox.Show("Error getting data adapter: " & ex.ToString, " DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function
    Public Function ExecSP(ByVal strSP As String, ByRef params As ArrayList) As Integer
        If Not OpenDB() Then
            Return -1
        End If
        objSQLCommand = New SqlCommand(strSP, objSQLConn)
        objSQLCommand.CommandType = CommandType.StoredProcedure
        Try
            If Not params Is Nothing Then
                For Each p As SqlParameter In params
                    objSQLCommand.Parameters.Add(p)
                Next
            End If
            Return objSQLCommand.ExecuteNonQuery
        Catch ex As Exception
            MessageBox.Show("Error executing SP: " & ex.ToString, " DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        End Try
    End Function
    Public Function GetSingleValueFromSP(ByVal strSP As String, ByRef params As ArrayList) As String
        Dim sqlDR As SqlDataReader = GetDataReaderBySP(strSP, params)
        Dim strResult As String
        If Not sqlDR Is Nothing Then
            If sqlDR.Read Then 'we have some data
                strResult = sqlDR.GetValue(0).ToString
                sqlDR.Close()
                Return strResult
            Else ' no data
                Return "-1"
            End If
        End If
        Return "-2" 'failed to connect or get data reader
    End Function
End Class
