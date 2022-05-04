drop table if exists tours;
CREATE TABLE TOURS(
    id uuid not null primary key,
	name varchar(30) not null,
	description text not null,
    duration time not null,
	distance float(2) not null,
    type varchar(30) not null,
    start varchar(200) not null,
    destination varchar(200) not null
);

drop table if exists logs;
CREATE TABLE LOGS(
    id uuid not null primary key,
	date timestamp not null,
	comment text not null,
    difficulty smallint not null,
    duration time not null,
	rating smallint not null
);



