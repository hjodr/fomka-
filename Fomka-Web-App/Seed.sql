delete from UserType;

GO

set identity_insert UserType on

insert into UserType (Id, Title) values (1, 'Student'), (2, 'Lecturer');

set identity_insert UserType off

GO
set identity_insert [Institute] on
insert into [Institute] (Id, Title)
	values (1, 'ШКНІ');
set identity_insert [Institute] off


GO
set identity_insert [Group] on
insert into [Group] (Id, Title, InstituteId)
	values (1, 'ПЗ-35', 1);
set identity_insert [Group] off


GO
set identity_insert [User] on

insert into [User] (Id, FirstName, LastName, SurName, [Group], Email, DateOfBirth, [Login], [Password], [Type])
values
	(1, 'Bohdan', 'Bryzhatyi', '', 1, 'email', dateadd(Y, -12, getutcdate()), 'bbrizhaty', 'bbrizhaty', 1),
	(2, 'Dmutro', 'Tsubera', '', 1, 'email', dateadd(Y, -12, getutcdate()), 'dtsubera', 'dtsubera', 1),
	(3, 'Olena', 'Niemova', '', 1, 'email', dateadd(Y, -12, getutcdate()), 'oniemova', 'oniemova', 1),
	(4, 'Ivan', 'Kuts', '', 1, 'email', dateadd(Y, -12, getutcdate()), 'ikuts', 'ikuts', 1),
	(5, 'Lecturer', 'Demo', '', 1, 'email', dateadd(Y, -12, getutcdate()), 'lecturerdemo', 'lecturerdemo', 2)

set identity_insert [User] off