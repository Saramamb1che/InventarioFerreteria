USE ferreteria_db;

CREATE TABLE proveedores (
    id_proveedor INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    ruc VARCHAR(20) NOT NULL UNIQUE,
    telefono VARCHAR(8) NOT NULL,
    correo VARCHAR(150) NOT NULL,
    CONSTRAINT chk_proveedor_nombre CHECK (CHAR_LENGTH(TRIM(nombre)) > 0),
    CONSTRAINT chk_proveedor_ruc CHECK (CHAR_LENGTH(TRIM(ruc)) > 0),
    CONSTRAINT chk_proveedor_telefono CHECK (telefono REGEXP '^[0-9]{8}$')
) ENGINE=InnoDB;

-- Los productos existentes quedan sin proveedor hasta que se les asigne uno.
ALTER TABLE productos
    ADD COLUMN id_proveedor INT NULL,
    ADD CONSTRAINT fk_productos_proveedores
        FOREIGN KEY (id_proveedor) REFERENCES proveedores(id_proveedor)
        ON UPDATE CASCADE ON DELETE RESTRICT;

SELECT COUNT(*) AS proveedores FROM proveedores;
