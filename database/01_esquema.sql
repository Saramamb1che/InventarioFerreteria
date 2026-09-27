-- =============================================================
-- Proyecto : InventarioFerreteria
-- Archivo  : database/01_esquema.sql
-- Motor    : MariaDB 11.x / 12.3   (ejecutar como root)
-- Propósito: crear la base de datos y sus tablas
-- =============================================================

-- Si ferreteria_db ya existe, detenerse y revisar antes de continuar.

CREATE DATABASE ferreteria_db
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE ferreteria_db;

-- -------------------------------------------------------------
-- Tabla catálogo: categorias
-- Alimenta el ComboBox cboCategoria del formulario.
-- -------------------------------------------------------------
CREATE TABLE categorias (
    id_categoria  INT          NOT NULL AUTO_INCREMENT,
    nombre        VARCHAR(60)  NOT NULL,
    CONSTRAINT pk_categorias        PRIMARY KEY (id_categoria),
    CONSTRAINT uq_categorias_nombre UNIQUE (nombre)
) ENGINE = InnoDB;

-- -------------------------------------------------------------
-- Tabla principal: productos
-- Sobre esta tabla se hacen las cuatro operaciones CRUD.
-- -------------------------------------------------------------
CREATE TABLE productos (
    id_producto     INT            NOT NULL AUTO_INCREMENT,
    codigo          VARCHAR(15)    NOT NULL,
    nombre          VARCHAR(100)   NOT NULL,
    id_categoria    INT            NOT NULL,
    unidad          VARCHAR(20)    NOT NULL DEFAULT 'Unidad',
    precio          DECIMAL(10,2)  NOT NULL,
    existencia      INT            NOT NULL DEFAULT 0,
    activo          TINYINT(1)     NOT NULL DEFAULT 1,
    fecha_registro  DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT pk_productos        PRIMARY KEY (id_producto),
    CONSTRAINT uq_productos_codigo UNIQUE (codigo),
    CONSTRAINT fk_productos_categorias
        FOREIGN KEY (id_categoria) REFERENCES categorias (id_categoria)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT chk_productos_precio     CHECK (precio > 0),
    CONSTRAINT chk_productos_existencia CHECK (existencia >= 0)
) ENGINE = InnoDB;

-- Índice para acelerar la búsqueda por nombre
CREATE INDEX ix_productos_nombre ON productos (nombre);
