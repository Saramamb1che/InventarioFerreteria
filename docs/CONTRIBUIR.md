# Cómo se trabaja en este repositorio

1. La estructura inicial es el único cambio directo a `main` previsto por la guía.
2. Cada tarea posterior vive en su propia rama `feature/<descripcion-corta>`.
3. Antes de empezar una rama: `git switch main` y `git pull`.
4. Los commits se escriben en español y usan estos prefijos:
   - `feat:` nueva funcionalidad.
   - `fix:` corrección de un error.
   - `db:` cambios en scripts SQL.
   - `docs:` documentación.
   - `chore:` configuración, estructura o limpieza.
5. Los cambios posteriores se integran mediante Pull Requests. La guía pide
   revisión de otro integrante; al trabajar individualmente, se consultará con el
   docente cómo cumplir ese requisito. No se simularán revisiones de terceros.
6. Se mantiene un único responsable del diseño de cada formulario.
7. Los scripts SQL ya fusionados no se modifican: los cambios nuevos se agregan
   en un script con el siguiente número.
8. Antes de cada commit se revisa `git status`. No se suben `bin/`, `obj/`, `.vs/`
   ni respaldos locales de la base de datos.

## Ramas previstas

- `feature/base-datos`: esquema, usuario de aplicación y datos de prueba.
- `feature/proyecto-base`: solución y proyecto Windows Forms.
- `feature/capa-datos`: modelos, conexión y repositorios.
- `feature/formulario-productos`: diseño y eventos del CRUD de productos.
- `feature/existencia-baja`: resaltado de existencias y filtro de activos.

El módulo de proveedores se organizará en ramas adicionales al llegar a esa etapa.
