Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
Public Class CMember

    Private _mstrPID As String
    Private _mstrFName As String
    Private _mstrLName As String
    Private _mstrMI As String
    Private _mstrEmail As String
    Private _mstrPhone As String
    Private _mstrPhotoPath As String
    Private _isNewMember As Boolean

    Public Sub New()
        _mstrPID = ""
        _mstrFName = ""
        _mstrLName = ""
        _mstrMI = ""
        _mstrEmail = ""
        _mstrPhone = ""
        _mstrPhotoPath = ""
    End Sub
#Region "exposed params"
    Public Property PID As String
        Get
            Return _mstrPID
        End Get
        Set(pidvalue As String)
            _mstrPID = pidvalue
        End Set
    End Property
    Public Property FName As String
        Get
            Return _mstrFName
        End Get
        Set(Fnamevalue As String)
            _mstrFName = Fnamevalue
        End Set
    End Property
    Public Property LName As String
        Get
            Return _mstrLName
        End Get
        Set(Lnamevalue As String)
            _mstrLName = Lnamevalue
        End Set
    End Property
    Public Property MI As String
        Get
            Return _mstrMI
        End Get
        Set(MIvalue As String)
            _mstrMI = MIvalue
        End Set
    End Property
    Public Property Email As String
        Get
            Return _mstrEmail
        End Get
        Set(Emailvalue As String)
            _mstrEmail = Emailvalue
        End Set
    End Property
    Public Property Phone As String
        Get
            Return _mstrPhone
        End Get
        Set(Phonevalue As String)
            _mstrPhone = Phonevalue
        End Set
    End Property
    Public Property PhotoPath As String
        Get
            Return _mstrPhotoPath
        End Get
        Set(Photovalue As String)
            _mstrPhotoPath = Photovalue
        End Set
    End Property
    Public Property IsNewMember As Boolean
        Get
            Return _isNewMember
        End Get
        Set(INMvalue As Boolean)
            _isNewMember = INMvalue
        End Set
    End Property

    Public ReadOnly Property GetSaveParams() As ArrayList 'arranging the user values from frmMember into an arraylist to be fed to a DB sp
        Get
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            params.Add(New SqlParameter("FName", _mstrFName))
            params.Add(New SqlParameter("LName", _mstrLName))
            params.Add(New SqlParameter("MI", _mstrMI))
            params.Add(New SqlParameter("Email", _mstrEmail))
            params.Add(New SqlParameter("Phone", _mstrPhone))
            params.Add(New SqlParameter("PhotoPath", _mstrPhotoPath))
            Return params
        End Get
    End Property
#End Region
    Public Function Save() As Integer 'check for existing members and savin the user input values as a new member.
        If IsNewMember Then
            Dim params1 As New ArrayList
            params1.Add(New SqlParameter("PID", _mstrPID))
            Dim strResult As String = myDB.GetSingleValueFromSP("sp_checkMemberPIDExists", params1)
            If Not strResult = 0 Then
                Return -1
            End If
        End If
        Return myDB.ExecSP("sp_SaveNewMember", GetSaveParams())
    End Function
    Public Function GetReportData() As SqlDataAdapter 'send data to frmMmeberReport
        Return myDB.GetDataAdapterbySP("dbo.sp_GetMemberAndSecurityLevel", Nothing)
    End Function
End Class
