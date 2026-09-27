Imports MySqlConnector
Public Module ConexionBD

    Private Const CadenaConexion As String =
        "Server=localhost;" &
        "Port=3307;" &
        "Database=ferreteria_db;" &
        "Uid=ferre_app;" &
        "Pwd=Ferre2026*;"
    Public Function ObtenerConexion() As MySqlConnection
        Dim configurada = Environment.GetEnvironmentVariable("FERRETERIA_CONEXION")
        Return New MySqlConnection(If(String.IsNullOrWhiteSpace(configurada), CadenaConexion, configurada))
    End Function
    Public Function ProbarConexion(ByRef mensaje As String) As Boolean
        Try
            Using cn As MySqlConnection = ObtenerConexion()
                cn.Open()
                mensaje = $"Conectado a MariaDB {cn.ServerVersion} · base ferreteria_db"
                Return True
            End Using
        Catch ex As MySqlException
            mensaje = $"Error {ex.Number}: {ex.Message}"
            Return False
        End Try
    End Function

End Module
