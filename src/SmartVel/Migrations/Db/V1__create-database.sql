create table user (
	id integer primary key autoincrement,
	login TEXT not null, 
	salt TEXT not null, 
	hash TEXT not null,
	created_date text not null,
	created_at text not null,
	last_modified_date datetime not null,
	last_modified_at text not null
);

create unique index ix1_user on user (login);

create table reference (
	id integer primary key autoincrement,
	code text not null, 
	domain text not null, 
	value text, 
	description text not null,
	created_date text not null,
	created_at text not null,
	last_modified_date datetime not null,
	last_modified_at text not null
);

create unique index ix1_reference on reference (code, domain);

create table cow (
	id integer primary key autoincrement,
	country_code text not null,
	national_identifier text not null,
	working_number text,
	breed_code text not null,
	name text not null,
	photo text,
	birth_date datetime not null,
	sensor text null,
	bull_name text null,
	equipped_last_date datetime null,
	last_insemination_date datetime null,
	expected_calving_date datetime null,
	calving_rank integer null,
	created_date text not null,
	created_at text not null,
	last_modified_date datetime not null,
	last_modified_at text not null
);

create unique index ix1_cow on cow (country_code, national_identifier);

