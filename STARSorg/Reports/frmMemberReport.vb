Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class frmMemberReport
    Private ds As DataSet
    Private sd As DataSet 'SECURITY DATASET
    Private da As SqlDataAdapter
    Private sa As SqlDataAdapter ' SECURITY SQLDATA ADAPTER
    Private Member As CMember
    Private Sub frmMemberReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.rpvMemberReport.RefreshReport()
    End Sub
    Public Sub Display()
        Member = New CMember
        rpvMemberReport.LocalReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory & "Reports\rptMembers.rdlc"
        ds = New DataSet
        da = Member.GetReportData
        da.Fill(ds)

        sd = New DataSet 'SECURITY DATA IN
        sa = Member.GetReportData 'REPLACE FOR THE EQUIVALENT IN SECURITY
        da.Fill(sd)

        rpvMemberReport.LocalReport.DataSources.Add(New ReportDataSource("dsMember", ds.Tables(0)))
        rpvMemberReport.LocalReport.DataSources.Add(New ReportDataSource("dsSecurity", sd.Tables(0))) '< this may be wierd due the second table, don't know if it will work. can't test without everything else
        rpvMemberReport.SetDisplayMode(DisplayMode.PrintLayout)
        rpvMemberReport.RefreshReport()
        Me.Cursor = Cursors.Default
        Me.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class