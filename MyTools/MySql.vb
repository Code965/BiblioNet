﻿Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports MySql.Data.MySqlClient

Namespace Database
    Public Class MySql


        'FUNZIONAMENTO
        '1) Creo delle prorietà per ogni parte della query che tengono dentro la stringa
        '2) Creo dei metodi che una volta chiamati settano le proprietà - incapsulamento
        '3) Creo un metodo Build che mi crea la query finale

        ' Connection string dal Web.config
        Private ReadOnly connStr As String = ConfigurationManager.ConnectionStrings("biblionet").ConnectionString

        ' Attributi della classe MySql
        Public Property _Select As String
        Public Property _From As String
        Public Property _Insert As String
        Public Property _Update As String
        Public Property _Delete As String
        Public Property _Where As String
        Public Property _OrderBy As String
        Public Property _GroupBy As String

        Public Property _Sql As String

        'Costruttore

        Public Sub New()
            'Inizializzo le proprietà a stringa vuota
            _Select = String.Empty
            _From = String.Empty
            _Insert = String.Empty
            _Update = String.Empty
            _Delete = String.Empty
            _Where = String.Empty
            _OrderBy = String.Empty
            _GroupBy = String.Empty
        End Sub



        'METODI

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
        Public Function Where(columnOne As String, operand As String, value As String) As MySql
            _Where = "WHERE " & columnOne & operand & value
            Return Me
        End Function

        Public Function WhereLike(columnOne As String, value As String) As MySql
            _Where = "WHERE " & columnOne & " LIKE '%" & value & "%'"
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

        Public Function InsertInto(table As String, columns As String, values As String) As MySql
            _Insert = "INSERT INTO " & table & " ( " & columns & ") VALUES (" & values & ")"
            Return Me
        End Function
        Public Function Update(table As String, setClause As String) As MySql
            _Update = "UPDATE " & table & " SET " & setClause
            Return Me
        End Function


        Public Function DeleteFrom(table As String) As MySql
            _Delete = "DELETE FROM " & table
            Return Me
        End Function

        Public Function InsertMultipleRows(table As String, columns As String, valuesList As List(Of String)) As MySql
            Dim allValues As String = String.Join(", ", valuesList)
            _Insert = $"INSERT INTO {table} ({columns}) VALUES {allValues}"
            Return Me
        End Function



        ' SQL finale
        Public Function Build() As String

            Dim parts As New List(Of String)

            ' INSERT
            If Not String.IsNullOrEmpty(_Insert) Then
                parts.Add(_Insert)
            End If

            'Aggiungo i metodi alla lista 
            If Not String.IsNullOrEmpty(_Select) Then parts.Add(_Select)
            If Not String.IsNullOrEmpty(_From) Then parts.Add(_From)
            If Not String.IsNullOrEmpty(_Where) Then parts.Add(_Where)
            If Not String.IsNullOrEmpty(_OrderBy) Then parts.Add(_OrderBy)
            If Not String.IsNullOrEmpty(_GroupBy) Then parts.Add(_GroupBy)
            If Not String.IsNullOrEmpty(_Delete) Then parts.Add(_Delete)
            If Not String.IsNullOrEmpty(_Update) Then parts.Add(_Update)

            'Mi crea la query finale
            Return String.Join(" ", parts)
        End Function

        'Esegue una query sql che gli passo come stringa
        Public Function Sql(query As String) As MySql
            _Sql = query
            Return Me
        End Function

        Public Sub Reset()

            'Svuoto i vari valori
            'In questo modo quando fa la build ho tutto vuoto per poi resettarli
            Me._Select = ""
            Me._Delete = ""
            Me._Insert = ""
            Me._Where = ""
            Me._GroupBy = ""
            Me._OrderBy = ""
            Me._Update = ""
            Me._From = ""

        End Sub


        'RITORNO DEI VALORI

        Public Function ToList(Of T As New)() As List(Of T)
            Dim sql = Build()
            Dim list As New List(Of T)()
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(sql, conn)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim obj As New T()
                            For Each prop In GetType(T).GetProperties()
                                If ColumnExists(reader, prop.Name) AndAlso Not IsDBNull(reader(prop.Name)) Then
                                    prop.SetValue(obj, Convert.ChangeType(reader(prop.Name), prop.PropertyType))
                                End If
                            Next
                            list.Add(obj)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function ToObj(Of T As New)() As T
            Dim sql = Build()
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(sql, conn)
                    conn.Open()

                    ' Se T è un tipo scalare
                    If GetType(T).IsPrimitive OrElse GetType(T) Is GetType(String) OrElse GetType(T) Is GetType(Decimal) Then
                        Dim result = cmd.ExecuteScalar()
                        Return If(result Is Nothing OrElse IsDBNull(result), Nothing, CType(Convert.ChangeType(result, GetType(T)), T))
                    End If

                    ' Altrimenti assume che sia un oggetto complesso
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim obj As New T()
                            For Each prop In GetType(T).GetProperties()
                                If ColumnExists(reader, prop.Name) AndAlso Not IsDBNull(reader(prop.Name)) Then
                                    prop.SetValue(obj, Convert.ChangeType(reader(prop.Name), prop.PropertyType))
                                End If
                            Next
                            Return obj
                        Else
                            Return Nothing
                        End If
                    End Using

                End Using
            End Using
        End Function

        ' Funzione helper per verificare se la colonna esiste nel DataReader
        Private Function ColumnExists(reader As MySqlDataReader, columnName As String) As Boolean
            For i As Integer = 0 To reader.FieldCount - 1
                If reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
            Return False
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


        'ESECUZIONE
        Public Function ExecuteSql() As Integer
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(Me._Sql, conn)
                    conn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        End Function

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