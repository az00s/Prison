USE [Prison]
GO

INSERT INTO [dbo].[Detainee]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[BirstDate]
           ,[MaritalStatusID]
           ,[WorkPlace]
           ,[ImagePath]
           ,[ResidenceAddress]
           ,[AdditionalData])
     VALUES
           ('�������','��������',null,'21-04-1921',2,'���������� ������� �����,������, ����-������� 2','/Content/Images/ProfilePhotos/Einstein.jpg','�������� ���������, 27',null),
		   ('������','�����',null,'18-03-1896',1,'����������� ������, ���-����, �������,33','/Content/Images/ProfilePhotos/Tesla.jpg','��.����������,29','���������� ������'),
		   ('��������','������',null,'01-04-1888',1,'��������� ����� �������������,�����, ��.��-�����,10','/Content/Images/ProfilePhotos/Felini.jpg','���,��.����������� 22','��������'),
		   ('�����','������',null,'21-02-1844',1,'�������� "������",���������, ��.�������,4','/Content/Images/ProfilePhotos/Edison.jpg','���-����, ��.�������������,25','������'),
		   ('�����','����',null,'06-09-1921',1,'���������� ������� �����, ������, �������� �����������, 7','/Content/Images/ProfilePhotos/Codd.jpg','���������, ��.�������, 22','�������� ����������� ��'),
		   ('���','������-��',null,'27-05-1955',1,'����������� �����������,�������, ��.������,66','/Content/Images/ProfilePhotos/Li.jpg','�������, �������� �������,10','��������'),
		   ('����','����',null,'03-08-1980',1,'�����-���, ���-�����, ��.�����������,22','/Content/Images/ProfilePhotos/Musk.jpg','���-�����,��.�������������,9','������� ������')

GO


