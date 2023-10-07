# Conferencia Visual.NET - PostgreSQL

## Consultas

```sql
-- CREAR BASE DE DATOS
create database db_conf;

-- CREAR USUARIO ENCARGADO DE LA BASE DE DATOS
create user uconf 
with encrypted password 'pconf';

-- ASIGNAR PERMISOS DEL USUARIO A LA BASE DE DATOS
grant all privileges on database db_conf to uconf;

-- CREAR TABLAS
create table usuario (
	id serial primary key , 
	nombre varchar(50) not null , 
	apellido varchar(50) not null , 
	nota numeric default 0
) ; 

-- SELECCIONAR DATOS
select * 
from usuario u 
order by u.id asc
```
