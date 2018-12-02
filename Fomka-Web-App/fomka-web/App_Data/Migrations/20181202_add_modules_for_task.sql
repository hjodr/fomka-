create table Module
(
	[Id] int not null identity (1,1),
	[Title] varchar(500)
)

alter table Module
	add constraint PK_Module_Id primary key (Id);

alter table Task
	add ModuleId int null,
		constraint FK_Task_Module foreign key (ModuleId) references Module(Id);