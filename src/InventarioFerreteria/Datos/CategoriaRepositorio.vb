Imports MySqlConnector
Public Class CategoriaRepositorio
    Public Function Listar() As List(Of Categoria)
        Const sql As String =
            "SELECT id_categoria, nombre FROM categorias ORDER BY nombre;"

        Dim lista As New List(Of Categoria)()

        Using cn As MySqlConnection = ObtenerConexion(),
              cmd As New MySqlCommand(sql, cn)

            cn.Open()
            Using dr As MySqlDataReader = cmd.ExecuteReader()
                While dr.Read()                       ' avanza fila por fila
                    lista.Add(New Categoria With {
                        .IdCategoria = dr.GetInt32("id_categoria"),
                        .Nombre = dr.GetString("nombre")
                    })
                End While
            End Using
        End Using                                     ' aquí se cierra la conexión

        Return lista
    End Function

End Class