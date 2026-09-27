-- =============================================================
-- Archivo  : database/03_datos_prueba.sql
-- Propósito: datos iniciales para practicar (precios en córdobas)
-- =============================================================
USE ferreteria_db;

INSERT INTO categorias (nombre) VALUES
    ('Herramientas manuales'),       -- 1
    ('Electricidad'),                -- 2
    ('Fontanería'),                  -- 3
    ('Pinturas'),                    -- 4
    ('Materiales de construcción'),  -- 5
    ('Tornillería y fijación');      -- 6

INSERT INTO productos (codigo, nombre, id_categoria, unidad, precio, existencia) VALUES
    ('HER-001', 'Martillo de uña 16 oz',            1, 'Unidad',  285.00,  24),
    ('HER-002', 'Cinta métrica 5 m',                1, 'Unidad',  165.00,  40),
    ('HER-003', 'Juego de desarmadores 6 piezas',   1, 'Unidad',  420.00,  12),
    ('ELE-001', 'Cable THHN calibre 12',            2, 'Metro',    28.50, 500),
    ('ELE-002', 'Tomacorriente doble polarizado',   2, 'Unidad',   95.00,  60),
    ('ELE-003', 'Bujía LED 9 W',                    2, 'Unidad',   75.00,   8),
    ('FON-001', 'Tubo PVC 1/2 pulgada x 6 m',       3, 'Unidad',  118.00,  35),
    ('FON-002', 'Llave de chorro metálica 1/2',     3, 'Unidad',  210.00,   6),
    ('PIN-001', 'Pintura látex blanco hueso',       4, 'Galón',   650.00,  18),
    ('PIN-002', 'Brocha de 3 pulgadas',             4, 'Unidad',   75.00,  30),
    ('CON-001', 'Cemento gris 42.5 kg',             5, 'Bolsa',   335.00,  80),
    ('CON-002', 'Varilla corrugada 3/8 x 6 m',      5, 'Unidad',  190.00, 120),
    ('TOR-001', 'Clavo de acero 2 1/2 pulgadas',    6, 'Libra',    42.00,  90),
    ('TOR-002', 'Tornillo para gypsum 1 pulgada',   6, 'Caja',    160.00,   5);

-- Verificación: debe mostrar 6 categorías y 14 productos
SELECT (SELECT COUNT(*) FROM categorias) AS categorias,
       (SELECT COUNT(*) FROM productos)  AS productos;
