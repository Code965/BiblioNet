Imports System.Security.Policy
Imports System.Web.Services.Discovery
Imports Mysqlx.Expr
Imports Newtonsoft.Json

Namespace BookModels

    Public Class Books

        Public Property Id As Integer
        Public Property Title As String
        Public Property AuthorID As String
        Public Property publisherID As String

        Public Property isbn As String

        Public Property stockQuantity As Integer

        Public Property publicationhDate As Date

        Public Property categoryID As Integer

        Public Property coverImageUrl As String
        Public Property Genre As String
        Public Property Price As Double

    End Class


    Public Class OpenLibraryEditionDetails
        <JsonProperty("url")>
        Public Property Url As String

        <JsonProperty("key")>
        Public Property Key As String

        <JsonProperty("title")>
        Public Property Title As String

        <JsonProperty("subtitle")>
        Public Property Subtitle As String

        <JsonProperty("authors")>
        Public Property Authors As List(Of Author)

        <JsonProperty("number_of_pages")>
        Public Property NumberOfPages As Integer?

        <JsonProperty("pagination")>
        Public Property Pagination As String

        <JsonProperty("by_statement")>
        Public Property ByStatement As String

        <JsonProperty("identifiers")>
        Public Property Identifiers As Identifiers

        <JsonProperty("classifications")>
        Public Property Classifications As Classifications

        <JsonProperty("publishers")>
        Public Property Publishers As List(Of Publisher)

        <JsonProperty("publish_date")>
        Public Property PublishDate As String

        <JsonProperty("subjects")>
        Public Property Subjects As List(Of Subject)

        Public Property SubjectPlaces As List(Of SubjectPlaces)
        Public Property subject_times As List(Of subject_times)
        Public Property subject_people As List(Of subject_people)

        <JsonProperty("ebooks")>
        Public Property Ebooks As List(Of Ebook)

        <JsonProperty("cover")>
        Public Property Cover As Cover

        Public Property Weight As String

        Public Property Notes As String

        Public Property Links As List(Of Links)

        Public Property Excerpts As List(Of Excerpts)

    End Class


    Public Class Author
        <JsonProperty("url")>
        Public Property Url As String

        <JsonProperty("name")>
        Public Property Name As String
    End Class

    Public Class Identifiers

        <JsonProperty("isbn_10")>
        Public Property Isbn10 As List(Of String)

        <JsonProperty("isbn_13")>
        Public Property Isbn13 As List(Of String)

        <JsonProperty("lccn")>
        Public Property Lccn As List(Of String)

        <JsonProperty("oclc")>
        Public Property Oclc As List(Of String)

        <JsonProperty("openlibrary")>
        Public Property OpenLibrary As List(Of String)
        Public Property Amazon As List(Of String)
    End Class


    Public Class Classifications
        <JsonProperty("lc_classifications")>
        Public Property LcClassifications As List(Of String)

        <JsonProperty("dewey_decimal_class")>
        Public Property DeweyDecimalClass As List(Of String)
    End Class

    Public Class Subject
        <JsonProperty("name")>
        Public Property Name As String

        <JsonProperty("url")>
        Public Property Url As String
    End Class
    Public Class SubjectPlaces
        <JsonProperty("name")>
        Public Property Name As String

        <JsonProperty("url")>
        Public Property Url As String
    End Class

    Public Class subject_people
        <JsonProperty("name")>
        Public Property Name As String

        <JsonProperty("url")>
        Public Property Url As String
    End Class
    Public Class subject_times
        <JsonProperty("name")>
        Public Property Name As String

        <JsonProperty("url")>
        Public Property Url As String
    End Class

    Public Class Publisher
        <JsonProperty("name")>
        Public Property Name As String
    End Class

    Public Class Ebook
        <JsonProperty("preview_url")>
        Public Property PreviewUrl As String

        <JsonProperty("availability")>
        Public Property Availability As String

        <JsonProperty("formats")>
        Public Property Formats As Object
    End Class

    Public Class Cover
        <JsonProperty("small")>
        Public Property Small As String

        <JsonProperty("medium")>
        Public Property Medium As String

        <JsonProperty("large")>
        Public Property Large As String
    End Class

    Public Class Links
        <JsonProperty("title")>
        Public Property Title As String
        <JsonProperty("url")>
        Public Property Url As String
    End Class

    Public Class Excerpts
        <JsonProperty("comment")>
        Public Property Comment As String
        <JsonProperty("text")>
        Public Property Text As String

        Public Property first_sentence As String
    End Class

End Namespace

