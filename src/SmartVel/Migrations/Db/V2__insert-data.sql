-- reference data
insert into reference (code, domain, value, description, created_date, created_at, last_modified_date, last_modified_at) values ('default_language', 'parameter','fr','dernière langue utilisée', date('now'), 'system', date('now'), 'system' );
insert into reference (code, domain, value, description, created_date, created_at, last_modified_date, last_modified_at) values ('remember_login','parameter', NULL, 'dernier identifiant de connexion enregistré', date('now'), 'system',  date('now'), 'system');
-- user data (login = adventiel, password = adventiel ) 
insert into user ( login, salt, hash, created_date, created_at, last_modified_date, last_modified_at ) values ( 'adventiel','0f627ae02cecb30ccd2dbd73b937f1de','c4e800a2e19f38fa3da5a2261087339f4d489df1ec93a7789f278f25ac323aeb', date('now'), 'system',  date('now'), 'system');


