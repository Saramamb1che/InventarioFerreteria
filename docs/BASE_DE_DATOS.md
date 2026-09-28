# Paso 3: preparar MariaDB

## Conexión local

La configuración local de MariaDB 12.3 indica el puerto **3307**.

En HeidiSQL, crear o seleccionar una sesión con:

- Tipo de red: MariaDB o MySQL (TCP/IP).
- Servidor: `127.0.0.1`.
- Puerto: `3307`.
- Usuario: `root`.
- Contraseña: la definida al instalar MariaDB; se escribe solo en HeidiSQL.

Abrir una pestaña de consulta y ejecutar primero:

```sql
SELECT VERSION() AS version_servidor, @@port AS puerto;
SHOW DATABASES LIKE 'ferreteria_db';
```

Si la base ya existe, detenerse y revisar su contenido antes de ejecutar scripts.
El script de esquema omite el `DROP DATABASE` de la guía para preservar datos.

## Ejecutar en orden

Abrir cada archivo desde Archivo > Cargar archivo SQL y ejecutarlo con F9.
Revisar el resultado antes de pasar al siguiente; detenerse si aparece un error.

1. `database/01_esquema.sql`: crea `ferreteria_db`, `categorias` y `productos`.
2. `database/02_usuario_app.sql`: crea `ferre_app` con permisos de lectura y CRUD.
3. `database/03_datos_prueba.sql`: inserta 6 categorías y 14 productos.

Los datos de prueba se cargan una sola vez, sobre las tablas recién creadas.
La contraseña de `ferre_app` incluida en el script es la de laboratorio de la guía.
Si ese usuario ya existía, `CREATE USER IF NOT EXISTS` no cambia su contraseña.

## Verificación

```sql
USE ferreteria_db;
SELECT COUNT(*) AS categorias FROM categorias;
SELECT COUNT(*) AS productos FROM productos;
SELECT p.codigo, p.nombre, c.nombre AS categoria, p.precio, p.existencia
FROM productos AS p
INNER JOIN categorias AS c ON c.id_categoria = p.id_categoria
ORDER BY p.nombre;
```

Se esperan 6 categorías, 14 productos y los nombres con tildes correctas.
Crear después otra sesión en HeidiSQL con `ferre_app`, el mismo servidor y puerto,
y la contraseña del script 02. Verificar que puede consultar los productos:

```sql
USE ferreteria_db;
SELECT COUNT(*) AS productos FROM productos;
SHOW GRANTS;
```

Los permisos sobre `ferreteria_db` deben ser SELECT, INSERT, UPDATE y DELETE.
USAGE sobre `*.*` no concede permisos adicionales para modificar tablas.

## Estado

Scripts 01, 02 y 03 ejecutados: 6 categorías y 14 productos de prueba cargados.
Conexión con `ferre_app` verificada en MariaDB 12.3.3, puerto 3307.

## Proveedores

El script `database/04_proveedores.sql` ya fue ejecutado en `ferreteria_db`.
Creó el catálogo y la relación con productos sin borrar registros existentes.
Después de ejecutarlo, se debe reiniciar la aplicación para habilitar la selección
de proveedor.
