use test

--empresa

CREATE TABLE `empresa` (
  `id` int(11) NOT NULL,
  `ruc` varchar(50) NULL,
  `razon_social` varchar(100) NULL,
  `activo` tinyint(1) NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `empresa`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `empresa`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--sucursal

CREATE TABLE `sucursal` (
  `id` int(11) NOT NULL,
  `codigo` varchar(50) NOT NULL,
  `nombre` varchar(100) NULL,
  `fecha` datetime NULL,
  `activo` tinyint(1) NULL,
  `id_empresa` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `sucursal`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_empresa_id` (`id_empresa`);

ALTER TABLE `sucursal`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `sucursal`
  ADD CONSTRAINT `fk_empresa_id` FOREIGN KEY (`id_empresa`) REFERENCES `empresa` (`id`);