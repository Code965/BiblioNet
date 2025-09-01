Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports MySql.Data.MySqlClient

Namespace Database
    Public Class MySql

        ' Connection string dal Web.config
        Private connStr As String = ConfigurationManager.ConnectionStrings("biblionet").ConnectionString

        ' Parti della query
        Public Property _Select As String
        Public Property _From As String
        Public Property _Insert As String
        Public Property _Update As String
        Public Property _Delete As String
        Public Property _Where As String
        Public Property _OrderBy As String
        Public Property _GroupBy As String

        ' SELECT
        Public Function [Select](columns As String) As MySql
            _Select = "SELECT " & columns
            Return Me
        End Function

        ' FROM
        Public Function From(table As String) As MySql
            _From = "FROM " & table
            Return Me
        End Function

        ' WHERE
        Public Function Where(condition As String) As MySql
            _Where = "WHERE " & condition
            Return Me
        End Function

        ' ORDER BY
        Public Function OrderBy(columns As String) As MySql
            _OrderBy = "ORDER BY " & columns
            Return Me
        End Function

        ' GROUP BY
        Public Function GroupBy(columns As String) As MySql
            _GroupBy = "GROUP BY " & columns
            Return Me
        End Function

        ' SQL finale
        Public Function Build() As String
            Dim parts As New List(Of String)
            If Not String.IsNullOrEmpty(_Select) Then parts.Add(_Select)
            If Not String.IsNullOrEmpty(_From) Then parts.Add(_From)
            If Not String.IsNullOrEmpty(_Where) Then parts.Add(_Where)
            If Not String.IsNullOrEmpty(_OrderBy) Then parts.Add(_OrderBy)
            If Not String.IsNullOrEmpty(_GroupBy) Then parts.Add(_GroupBy)

            Return String.Join(" ", parts)
        End Function

        ' Restituisce DataTable
        Public Function ToDataTable() As DataTable
            Dim sql = Build()
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(sql, conn)
                    conn.Open()
                    Dim dt As New DataTable()
                    dt.Load(cmd.ExecuteReader())
                    Return dt
                End Using
            End Using
        End Function

        ' Restituisce lista di liste
        Public Function ToList() As List(Of List(Of Object))
            Dim sql = Build()
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(sql, conn)
                    conn.Open()
                    Dim result As New List(Of List(Of Object))()
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim row As New List(Of Object)()
                            For i As Integer = 0 To reader.FieldCount - 1
                                row.Add(reader.GetValue(i))
                            Next
                            result.Add(row)
                        End While
                    End Using
                    Return result
                End Using
            End Using
        End Function

        ' Esegue query non di selezione (INSERT, UPDATE, DELETE)
        Public Function ExecuteNonQuery() As Integer
            Dim sql = Build()
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(sql, conn)
                    conn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        End Function

    End Class
End Namespace