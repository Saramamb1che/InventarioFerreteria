# Verificación

## Comprobaciones automáticas

Ejecutar desde la raíz:

```powershell
dotnet run --project tests/Verificacion/Verificacion.csproj -- .
```

La prueba crea una base `test_ferreteria_<identificador>`, ejecuta allí los scripts
01, 03 y 04 y la elimina al terminar. No cambia los datos de `ferreteria_db`.
Requiere permisos para crear y eliminar esa base temporal; la instalación local
permite hacerlo mediante los permisos de pruebas de MariaDB.

Comprobaciones realizadas correctamente:

- Compilación y construcción de ambos formularios.
- Carga de 6 categorías y 14 productos.
- Búsqueda, altas, modificaciones y bajas de productos.
- Filtro de activos, código duplicado y precio inválido.
- Conservación de apóstrofos y valores decimales.
- Migración de proveedores y sus operaciones CRUD.
- RUC duplicado y teléfono de longitud inválida.
- Relación producto-proveedor y bloqueo de eliminación con error 1451.
- Conservación de los 14 productos iniciales en la base de prueba.
- Validación de formularios vacíos, correo inválido y proveedor válido.
- Rango del precio y selección de productos que ya no aparecen en la cuadrícula.

## Revisión manual pendiente

1. Abrir la solución de la raíz y ejecutar con F5.
2. Verificar cuadrícula, precios en C$ y tres filas con existencia menor de 10.
3. Probar Buscar con Enter y alternar Solo activos.
4. Intentar guardar un producto sin nombre, categoría o precio válido.
5. Seleccionar un producto y comprobar la carga de los campos.
6. Comprobar que responder No a Eliminar conserva el registro.
7. Ejecutar el script 04 en la base de trabajo y reiniciar la aplicación.
8. Registrar un proveedor, asignarlo a un producto y comprobar el mensaje al
   intentar eliminar ese proveedor.
9. Revisar el diseño al maximizar y navegar con Tab.

La compilación y las pruebas de repositorios no sustituyen esta revisión visual.
