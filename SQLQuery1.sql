CREATE TABLE [dbo].[guest] (
    [guest_id]  INT     IDENTITY (1, 1) NOT NULL,
    [guest_name]      VARCHAR (20) NOT NULL,
     [guest_surname]      VARCHAR (20) NOT NULL,
     [guest_id_number]      VARCHAR (20) NOT NULL,
    [guest_is_active] BIT   NOT NULL,
    PRIMARY KEY CLUSTERED ([guest_id] ASC),
    UNIQUE NONCLUSTERED ([guest_id_number] ASC)
);


INSERT INTO [dbo].[guest] (guest_id, guest_name,guest_surname,guest_id_number, guest_is_active) VALUES (1, 'Milica','Simic',1305006508090, 1);
SET IDENTITY_INSERT [dbo].guest ON;
SET IDENTITY_INSERT [dbo].guest OFF;
select * from guest
drop table guest
SELECT * FROM [user]

/*  public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JMBG { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserType UserType { get; set; }
        public bool IsActive { get; set; } = true;
        CREATE TABLE [dbo].[room] (
    [room_id]        INT          IDENTITY (1, 1) NOT NULL,
    [room_number]    VARCHAR (25) NOT NULL,
    [has_TV]         BIT          NOT NULL,
    [has_mini_bar]   BIT          NOT NULL,
    [room_is_active] BIT          NOT NULL,
    [room_type_id]   INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([room_id] ASC),
    UNIQUE NONCLUSTERED ([room_number] ASC),
    CONSTRAINT [FK_ROOM_ROOM_TYPE] FOREIGN KEY ([room_type_id]) REFERENCES [dbo].[room_type] ([room_type_id])
);*/

CREATE TABLE [dbo].[user] (
    [user_id]  INT     IDENTITY (1, 1) NOT NULL,
    [user_name]      VARCHAR (20) NOT NULL,
     [user_surname]      VARCHAR (20) NOT NULL,
     [user_id_number]      VARCHAR (20) NOT NULL,
     [user_username]      VARCHAR (20) NOT NULL,
     [user_password]      VARCHAR (20) NOT NULL,
     [user_type]     VARCHAR (20)  NOT NULL,
    [guest_is_active] BIT   NOT NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC),
    UNIQUE NONCLUSTERED ([user_id_number] ASC)
);

SET IDENTITY_INSERT [dbo].[user] ON;

INSERT INTO [dbo].[user] (user_id, user_name,user_surname,user_id_number,user_username,user_password,user_type, guest_is_active) 
VALUES (2, 'Milan','Milanic',1405026508090, 'milan','123456','Administrator',1);

select * from [user]
select * from guest
SET IDENTITY_INSERT [dbo].[user] OFF;