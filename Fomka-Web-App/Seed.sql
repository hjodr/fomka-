delete from UserTypes;

GO

set identity_insert UserTypes on

insert into UserTypes (Id, Title) values (1, 'Student'), (2, 'Lecturer');

set identity_insert UserTypes off