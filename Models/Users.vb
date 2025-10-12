Namespace UsersModel

    Public Class Users

        Public Property Id As Integer
        Public Property Name As String
        Public Property Surname As String
        Public Property Email As String
        Public Property Password As String
        Public Property Phone As String
        Public Property Role As Integer
        Public Property CF As String
        Public Property City As String
        Public Property Province As String
        Public Property Region As String
        Public Property Country As String
        Public Property CAP As String
        Public Property CreatedAt As DateTime
        Public Property UpdatedAt As DateTime

    End Class

    Module CurrentUser

        Private Property _userId As Integer
        Private Property _name As String
        Private Property _userRole As Integer
        Private Property _userEmail As String

        Public Property UserId As Integer
            Get
                Return _userId
            End Get
            Set(value As Integer)
                _userId = value
            End Set
        End Property


        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property
        Public Property Role As Integer
            Get
                Return _userRole
            End Get
            Set(value As Integer)
                _userRole = value
            End Set
        End Property
        Public Property Email As String
            Get
                Return _userEmail
            End Get
            Set(value As String)
                _userEmail = value
            End Set
        End Property

        Public Property IsAdmin As Boolean
            Get
                Return _userRole = 1
            End Get
            Set(value As Boolean)
                If value Then
                    _userRole = 1
                End If
            End Set
        End Property


    End Module


End Namespace
