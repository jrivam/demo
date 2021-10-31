USE test;



INSERT INTO empresa
           
(razon_social, ruc,
           
,activo)
     
VALUES
           
('empresa 1', '1111111111', 1);




INSERT INTO empresa
           
(razon_social, ruc,
           
,activo)
     
VALUES
           
('empresa 2', '22222222222', 1);



INSERT INTO sucursal
           
(nombre
           
,codigo

,id_empresa
           
,fecha
           
,activo)
     
VALUES
           
('sucursal 1'

,'s1'           
,(select id from empresa where ruc = '1111111111')
           
,now()
           
,1);




INSERT INTO sucursal
           
(nombre

,codigo           
,id_empresa
           
,fecha
           
,activo)
     
VALUES
           
('sucursal 2'
,'s2'           
,(select id from empresa where ruc= '22222222222')
           
,now()
           
,1);

