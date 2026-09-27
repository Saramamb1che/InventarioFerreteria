-- =============================================================
-- Archivo  : database/02_usuario_app.sql   (ejecutar como root)
-- Propósito: crear el usuario que usará la aplicación.
-- Principio de mínimo privilegio: solo puede leer y modificar
-- DATOS; no puede crear ni borrar tablas.
-- Contraseña de laboratorio: en producción NUNCA se sube a Git.
-- =============================================================

CREATE USER IF NOT EXISTS 'ferre_app'@'localhost'
    IDENTIFIED BY 'Ferre2026*';

GRANT SELECT, INSERT, UPDATE, DELETE
    ON ferreteria_db.*
    TO 'ferre_app'@'localhost';

FLUSH PRIVILEGES;

-- Verificación
SHOW GRANTS FOR 'ferre_app'@'localhost';
