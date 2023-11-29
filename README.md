# StudentRegistration

Tech Stack-
ASP.NET Core Web App 6
EntityFramework 6
Javascript
Jquery
HTML
CSS

Before executing the project please change the database information in the appsettings.json file. I have used windows authentication. So, provide the server name and database name.

Please run the command in package manager console:
 add-migragtion "MigName"
 update-database

Please insert some values in the states and cities table before running the application for the first time.
Sample command to be run to insert values in both tables are -

1.) INSERT INTO [RegistrationMVC].[dbo].[StatesTbl] VALUES
  ('Haryana', 'HR'),
  ('Gujarat', 'GJ'),
  ('Jammu and Kashmir', 'JK'),
  ('Uttar Pradesh', 'UP'),
  ('Uttrakhand', 'UK');
