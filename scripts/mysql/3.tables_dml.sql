USE test;



INSERT INTO empresa
           
(razon_social
           
,activo)
     
VALUES
           
('empresa 1', 1);




INSERT INTO empresa
           
(razon_social
           
,activo)
     
VALUES
           
('empresa 2', 1);



INSERT INTO sucursal
           
(nombre
           
,id_empresa
           
,fecha
           
,activo)
     
VALUES
           
('sucursal 1'
           
,(select id from empresa where razon_social = 'empresa 1')
           
,now()
           
,1);




INSERT INTO sucursal
           
(nombre
           
,id_empresa
           
,fecha
           
,activo)
     
VALUES
           
('sucursal 2'
           
,(select id from empresa where razon_social = 'empresa 2')
           
,now()
           
,1);

