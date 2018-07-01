Automated Information System "Special detention center"

The system allows you to keep records of detentions, detainees, as well as places of their maintenance, 
and also provides an opportunity to easily add, edit, search and delete any information stored in the system.

Detainees can be searched for both the date of detention and the name of the detainee or his address. 
The system has authentication and authorization, and can work in 3 modes: 
-user mode; 
-editor mode; 
-Administrator mode.

How to deploy:

1. After loading (unpacking) the project, run the file "CreateDataBase.bat" (you will need the name of your sqlServer).
2. If the database "Prison" is created, -go to step 4, if the database is not created, go to step 3
3. Open and run the "Summary.sql" file on your server.
4. Run Prison.sln and deploy Prison.App.Web on local storage. Then make new app in IIS.- specify the Site Name: "Prison" (port 80).
5. Deploy Prison.AdvertismentService on the IIS,- specify port 8000.

Three application modes:

1. Admin mode (default):
	Login: Пуаро
	Password: пароль

2. Editor mode:
	Login: Холмс
	Password: пароль

3. User mode:
	Login: Дэп
	Password: пароль
