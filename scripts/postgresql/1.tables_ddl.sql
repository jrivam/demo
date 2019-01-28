CREATE SEQUENCE public.empresa_id_seq;

ALTER SEQUENCE public.empresa_id_seq
    OWNER TO postgres;
	
CREATE TABLE public.empresa
(
    razon_social character varying(100) COLLATE pg_catalog."default",
    activo boolean,
    id integer NOT NULL DEFAULT nextval('empresa_id_seq'::regclass),
    CONSTRAINT empresa_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.empresa
    OWNER to postgres;
	
	--------------------------------------
	
CREATE SEQUENCE public.sucursal_id_seq;

ALTER SEQUENCE public.sucursal_id_seq
    OWNER TO postgres;
	
	
CREATE TABLE public.sucursal
(
    nombre character varying(100) COLLATE pg_catalog."default",
    fecha date,
    id_empresa integer,
    activo boolean,
    id integer NOT NULL DEFAULT nextval('sucursal_id_seq'::regclass),
    CONSTRAINT sucursal_pkey PRIMARY KEY (id),
    CONSTRAINT fk_sucursal_empresa FOREIGN KEY (id_empresa)
        REFERENCES public.empresa (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.sucursal
    OWNER to postgres;