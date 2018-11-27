create database SEVL

GO

use SEVL;

GO

CREATE TABLE [User] (
	[Id] Integer NOT NULL identity(1,1),
	[FirstName] Text NOT NULL,
	[LastName] Text NOT NULL,
	[Surname] Text NOT NULL,
	[DateOfBirth] Date NOT NULL,
	[Type] Integer NOT NULL,
	[Email] Text NOT NULL,
	[Group] Integer NOT NULL,

	-- merge of Login table
	[Login] varchar(250) not null,
	[Password] varchar(250) not null
 )

 create table [UserType] (
	Id int not null identity(1,1),
	Title varchar(1000)
 )

 create table Error(
	Id int not null identity(1,1),
	Description varchar(5000) not null,
	Title varchar(250) not null,
	Type int not null,
	Date datetime not null default getutcdate()
 )

 create table Institute (
	Id int not null identity(1,1),
	Title varchar(500) not null
 )

 create table [Group] (
	Id int not null identity(1,1),
	InstituteId int not null,
	Title varchar(250)
 )

 create table Mark (
	Id int not null identity(1,1),
	Description varchar(5000),
	TaskId int not null,
	UserId int not null,
	[Value] float not null default 0.0
 )

 create table Task (
	Id int not null identity(1,1),
	PLId int not null,
	[StandardId] int not null,
	DifficultyLevelId int not null,
	Title varchar(1000),
	[Description] varchar(2000)

 )

 create table DifficultyLevel (
	Id int not null identity(1,1),
	Title varchar(1000) 
 )

 create table ProgrammingLanguage (
	Id int not null identity(1,1),
	Title varchar(1000) 
 )

  create table [Standard] (
	Id int not null identity(1,1),
	Title varchar(1000),
	StandardFile varchar(max) 
 )

 -- create indexes
create index Id on [User](Id);

-- create constraints
	-- pk
	alter table [Standard]
		add constraint PK_Standard_Id primary key (Id);

	alter table ProgrammingLanguage
		add constraint PK_ProgrammingLanguage_Id primary key (Id);

	alter table DifficultyLevel
		add constraint PK_DifficultyLevel_Id primary key (Id);

	alter table Task
		add constraint PK_Task_Id primary key (Id);

	alter table Mark
		add constraint PK_Mark_Id primary key (Id);

	alter table [Group]
		add constraint PK_Group_Id primary key (Id);

	alter table [Institute]
		add constraint PK_Institute_Id primary key (Id);

	alter table [Error]
		add constraint PK_Error_Id primary key (Id);

	alter table [UserType]
		add constraint PK_UserType_Id primary key (Id);

	alter table [User]
		add constraint PK_User_Id primary key (Id);

	GO

	alter table [Group]
		add constraint FK_Group_Institute foreign key (InstituteId) references Institute(Id);

	alter table [Mark]
		add constraint FK_Mark_User foreign key (UserId) references [User] (Id),
			constraint FK_Mark_Task foreign key (TaskId) references [Task] (id);

	alter table Task
		add constraint FK_Task_DifficultyLevel foreign key (DifficultylevelId) references DifficultyLevel(Id),
			constraint FK_Task_ProgrammingLanguage foreign key (PLId) references ProgrammingLanguage (Id),
			constraint FK_Task_Standard foreign key (StandardId) references [Standard](Id);

	alter table [User]
		add constraint FK_User_UserType foreign key ([Type]) references UserType (Id),
			constraint FK_User_Group foreign key ([Group]) references [Group] (Id)