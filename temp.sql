

CREATE TABLE hollidays.dbo.empleados (
	id int IDENTITY(1,1) NOT NULL,
	idsap int NOT NULL,
	nombre varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	email varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	contrasena varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	area varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	banda varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	fecha_ingreso_grupo date NULL,
	fecha_ingreso_uen date NULL,
	dias_disponibles int NULL,
	ultimo_desconecte date NULL,
	idsap_padre int NULL,
	nombre_line varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	email_line varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	estatus int NULL DEFAULT 0,
	esquema varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	sexo varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	avatar varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	tipo varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	rol int NULL DEFAULT 0,
	CONSTRAINT PK__Empleado__2B031D43DCFA702E PRIMARY KEY (id)
) ;

INSERT INTO hollidays.dbo.empleados (idsap,nombre,email,contrasena,area,banda,fecha_ingreso_grupo,fecha_ingreso_uen,dias_disponibles,ultimo_desconecte,idsap_padre,nombre_line,email_line,estatus,esquema,sexo,avatar,tipo,rol) VALUES 
(101010,'ADMIN','rodrigo290883@gmail.com','12345','ADMIN','ADMINISTRACION','2020-07-01','2020-07-01',10,NULL,101010,'ADMIN','rodrigo290883@hotmail.com',0,'0',NULL,NULL,'A',0)
,(111111,'CARGA','rodrigo290883@gmail.com','12345','CARGA','CARGA','2020-07-01','2020-07-01',10,NULL,111111,'CARGA','rodrigo290883@hotmail.com',0,'0',NULL,NULL,'R',0)
;

CREATE TABLE hollidays.dbo.solicitudes (
	folio int IDENTITY(1,1) NOT NULL,
	idsap int NULL,
	tipo_solicitud int NULL,
	fecha_solicitud datetime NULL DEFAULT GETDATE(),
	fecha_inicio date NULL,
	fecha_fin date NULL,
	dias int NULL,
	observacion_solicitante text COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	estatus int NULL,
	idsap_aprobador int NULL,
	nombre_aprobador varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	email_aprobador varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	fecha_aprobacion datetime NULL,
	observacion_aprobador text COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	con_goce char(1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ultima_notificacion datetime NULL,
	fecha_asignacion datetime NULL,
	idsap_aprobo int NULL,
	CONSTRAINT PK_solicitudes PRIMARY KEY (folio)
);

CREATE TABLE hollidays.dbo.registros_dias (
	registro int IDENTITY(0,1) NOT NULL,
	registro_padre int NULL,
	id_tipo_solicitud int NULL,
	dias int NULL,
	disponibles int NULL,
	fecha_creacion datetime NULL DEFAULT GETDATE(),
	caducidad date NULL,
	idsap int NULL,
	periodo varchar(4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	folio_solicitud int NULL
);

CREATE TABLE hollidays.dbo.regla_genera_dias (
	registro int IDENTITY(0,1) NOT NULL,
	esquema int NULL,
	meses_min int NULL,
	meses_max int NULL,
	genera_dias int NULL,
	antiguedad varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);

INSERT INTO hollidays.dbo.regla_genera_dias (esquema,meses_min,meses_max,genera_dias,antiguedad) VALUES 
(0,6,11,5,'6 meses')
,(0,12,23,5,'1 año')
,(0,24,35,10,'2 años')
,(0,36,47,12,'3 años')
,(0,48,59,13,'4 años')
,(0,60,71,15,'5 años')
,(0,72,107,18,'6 -8 años')
,(0,108,167,20,'9 -13 años')
,(0,168,239,22,'14 -19 años')
,(0,240,10000000,25,'20 - delante')
;
INSERT INTO hollidays.dbo.regla_genera_dias (esquema,meses_min,meses_max,genera_dias,antiguedad) VALUES 
(1,6,11,3,'6 meses')
,(1,12,23,3,'1 año')
,(1,24,35,6,'2 años')
,(1,36,47,8,'3 años')
,(1,48,59,10,'4 años')
,(1,60,71,12,'5 años')
,(1,72,131,14,'6 - 10 años')
,(1,132,191,16,'11 -15 años')
,(1,192,251,18,'16 - 20 años')
,(1,252,311,20,'21 - 25 años')
;
INSERT INTO hollidays.dbo.regla_genera_dias (esquema,meses_min,meses_max,genera_dias,antiguedad) VALUES 
(1,312,371,22,'26 - 30 años')
,(1,372,431,24,'31 - 35 años')
,(1,432,10000000,26,'36 - delante')
;

CREATE TABLE hollidays.dbo.croles (
	rol int NULL,
	semana varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	descripcion varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);

INSERT INTO hollidays.dbo.croles (rol,semana,descripcion) VALUES 
(0,'1,1,1,1,1,0,0','Fines de semana')
,(1,'1,1,1,1,1,1,0','Domingos')
,(2,'1,1,1,1,1,1,1','Sin Descanso')
;

CREATE TABLE hollidays.dbo.log_vacaciones (
	registro int IDENTITY(0,1) NOT NULL,
	idsap int NULL,
	log text COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	fecha_creacion datetime NULL DEFAULT GETDATE(),
	idsap_creacion int NULL
);

CREATE TABLE hollidays.dbo.ctipos_solicitud (
	id_tipo_solicitud int IDENTITY(0,1) NOT NULL,
	solicitud varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	maximo_dias int NULL,
	seleccionable char(1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	consume_vacaciones char(1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	con_goce int NULL
);

INSERT INTO hollidays.dbo.ctipos_solicitud (solicitud,maximo_dias,seleccionable,consume_vacaciones,con_goce) VALUES 
('VACACIONES',30,'1','S',0)
,('PERMISO',3,'1','N',1)
,('PATERNIDAD',7,'1','N',0)
,('MATERNIDAD',30,'1','N',0)
,('MATRIMONIO',3,'1','N',0)
,('DEFUNCION',3,'1','N',0)
;

CREATE TABLE hollidays.dbo.cestatus (
	estatus int IDENTITY(0,1) NOT NULL,
	descripcion varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);

INSERT INTO hollidays.dbo.cestatus (descripcion) VALUES 
('PENDIENTE')
,('APROBADO')
,('DENEGADO')
;

CREATE TABLE hollidays.dbo.cdias_festivos (
	registro int IDENTITY(0,1) NOT NULL,
	fecha date NULL,
	descripcion varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	pais varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	esquema int NULL,
	empresa varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);

INSERT INTO hollidays.dbo.cdias_festivos (fecha,descripcion,pais,esquema,empresa) VALUES 
('2020-01-01','Año nuevo','MX',0,'')
,('2020-02-03','Dia Constitucion','MX',0,'')
,('2020-03-16','Natalicio Benito Juarez','MX',0,'')
,('2020-05-01','Dia del Trabajo','MX',0,'')
,('2020-09-16','Dia Independencia','MX',0,'')
,('2020-11-16','Inicio Revolucion','MX',0,'')
,('2020-12-25','Navidad','MX',0,'')
;