Imports MySqlConnector

Public Class ProveedorRepositorio
    Public Function EstaInstalado() As Boolean
        Using cn = ObtenerConexion(), cmd As New MySqlCommand(
            "SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=DATABASE() " &
            "AND TABLE_NAME='productos' AND COLUMN_NAME='id_proveedor'", cn)
            cn.Open()
            Return Convert.ToInt32(cmd.ExecuteScalar()) = 1
        End Using
    End Function

    Public Function Listar(Optional filtro As String = "") As List(Of Proveedor)
        Dim lista As New List(Of Proveedor)
        Using cn = ObtenerConexion(), cmd As New MySqlCommand(
            "SELECT id_proveedor,nombre,ruc,telefono,correo FROM proveedores " &
            "WHERE nombre LIKE @filtro OR ruc LIKE @filtro ORDER BY nombre", cn)
            cmd.Parameters.AddWithValue("@filtro", "%" & filtro.Trim() & "%")
            cn.Open()
            Using dr = cmd.ExecuteReader()
                While dr.Read()
                    lista.Add(New Proveedor With {
                        .IdProveedor = dr.GetInt32("id_proveedor"),
                        .Nombre = dr.GetString("nombre"), .Ruc = dr.GetString("ruc"),
                        .Telefono = dr.GetString("telefono"), .Correo = dr.GetString("correo")})
                End While
            End Using
        End Using
        Return lista
    End Function

    Public Function Insertar(p As Proveedor) As Integer
        Using cn = ObtenerConexion(), cmd As New MySqlCommand(
            "INSERT INTO proveedores(nombre,ruc,telefono,correo) VALUES(@nombre,@ruc,@telefono,@correo)", cn)
            Parametros(cmd, p)
            cn.Open()
            cmd.ExecuteNonQuery()
            Return CInt(cmd.LastInsertedId)
        End Using
    End Function

    Public Function Actualizar(p As Proveedor) As Integer
        Using cn = ObtenerConexion(), cmd As New MySqlCommand(
            "UPDATE proveedores SET nombre=@nombre,ruc=@ruc,telefono=@telefono,correo=@correo " &
            "WHERE id_proveedor=@id", cn)
            Parametros(cmd, p)
            cmd.Parameters.AddWithValue("@id", p.IdProveedor)
            cn.Open()
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    Public Function Eliminar(id As Integer) As Integer
        Using cn = ObtenerConexion(), cmd As New MySqlCommand(
            "DELETE FROM proveedores WHERE id_proveedor=@id", cn)
            cmd.Parameters.AddWithValue("@id", id)
            cn.Open()
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    Private Sub Parametros(cmd As MySqlCommand, p As Proveedor)
        cmd.Parameters.AddWithValue("@nombre", p.Nombre)
        cmd.Parameters.AddWithValue("@ruc", p.Ruc)
        cmd.Parameters.AddWithValue("@telefono", p.Telefono)
        cmd.Parameters.AddWithValue("@correo", p.Correo)
    End Sub
End Class
