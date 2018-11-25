delete from UserType;

GO

set identity_insert UserType on

insert into UserType (Id, Title) values (1, 'Student'), (2, 'Lecturer');

set identity_insert UserType off