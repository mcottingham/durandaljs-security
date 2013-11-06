alter table [login] add constraint createdDateConstraint default CURRENT_TIMESTAMP for CreationDate
alter table [login] add constraint lastLoginConstraint default CURRENT_TIMESTAMP for LastLogin