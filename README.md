# Description
The project is an implementation of the tasks management system. There are several functionalities provided by the system:
- Users are able to add new Tasks.
- Users are able to edit Tasks.
- Users are able to delete Tasks.
- Users are able to sort by “Task title, Date created”.
- Users are able to search by Task title.
- Users are able to set status (Enum).
- Users are able to set progress (Percentage)

Real time updates are also provided on create, update or delete action via websockets. 

# Components
This project consists of the following components.
- Front-end app is built with Angular CLI version 13.3.7
- Asp.net Core version 6.0 API service
- PostgresSQL dateabase

# Get Started
- Make sure your system has .net 6, angular cli version 13 and postgresSQL installed
- Clone from main branch to your local machine
- Open the task API solution
- Replace "TaskDetailsDb" in the appsettings file with our postgresSQL connectionstring
- Run migration from PM console with command "Update-Database"
- Start task API webservice
- Go to task-client and open a command window in this directory 
- Run command npm i
- Run command npm start
- Go to browser http://localhost:4200/


