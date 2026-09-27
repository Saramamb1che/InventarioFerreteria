Public Class Producto
    Public Property IdProveedor As Integer?

    Public Property IdProducto As Integer          ' id_producto (0 = aún no existe)
    Public Property Codigo As String = ""          ' codigo
    Public Property Nombre As String = ""          ' nombre
    Public Property IdCategoria As Integer         ' id_categoria (FK)
    Public Property Unidad As String = "Unidad"    ' unidad
    Public Property Precio As Decimal              ' precio  (C$)
    Public Property Existencia As Integer          ' existencia
    Public Property Activo As Boolean = True       ' activo

End Class
