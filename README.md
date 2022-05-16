# PC-Service .Net Core App
Web App which helps to manage data in PC-Service. Application has a database in SQLite and includes simple microsoft authentication cookie scheme.
Application has three type of users:
* Admin
* Manager
* User
## Roles Description
* Admin has access to all content on its page
    * ``` user name: bob, password: windows.12```
* Manager has access to *Repairs* *Panel* and *Parts* *Panel*. This user has an access to its own repairs on *User* *Panel* -> *My* *Repairs*
    * ```user name: Kamila1928, password: windows.12```
* User has access to *User* *Panel* where all its repairs can be checked.
    * ```user name: marcel2002, password: windows.12```

## Pages overview
* Home 
![alt text](/md_pics/Capture.PNG)
simple page with short description of a company.
* Search for Repair
![alt text](/md_pics/Capture1.PNG)
you can search here repair by its id
* User Panel
    * My Repairs
    ![alt text](/md_pics/Capture2.PNG)
    user can find all his repairs
    * Add Repair
    ![alt text](/md_pics/Capture3.PNG)
    user can add a new repair request

* Manager Panel
    * Repair Panel
    ![alt text](/md_pics/Capture4.PNG)
    All repairs which are in service. On left side there are some search filters

    * Parts Panel
    ![alt text](/md_pics/Capture5.PNG)
    All parts which are in service with price and type. There are some search filters on left side.

* Admin Panel
    * *Repair* *Panel* and *Parts* *Panel* are the same as **Manager**.

    * User Panel
    ![alt text](/md_pics/Capture6.PNG)
    Here are all users which are registered in our service. Admin can change them a password. There are some filters on left side.

    * Deliveries Panel
    ![alt text](/md_pics/Capture7.PNG)
    Here are deliveries which we have in our service with its price. There are some filters on left.

    * PartsTypes Panel
    
    Here are all Types Parts which are in database.

## Repair overview
Repairs are The most important thing in this application. Here you can see (from Manager or Admin point of view) Edit form of a repair. 
![alt text](/md_pics/Capture9.PNG)


Manager or Admin can change a status of an repair it can also write a report and finally people with this role can add parts which they used. When someone add more parts than we have in magazine and save form, an application returns an error and do not save changes. When someone puts smaller or equal number of parts than we have in database a webpage successfully add parts and decrease its amount in database. In the end an app shows a new repair price with value of all parts and staff labour added.

## Extra feautures
* When you change part type name it will swap type name in all parts with chaged type.
* When you change delivery name it will swap delivery name in all repairs with old delivery name.
* When you rename a part it will change name of this part in all repairs.

# Run
You neeed to have dotnet (at least 6.0) installed

Check it typing in terminal command
```
dotnet --version
```
when you see version number you can successfully run an app with kestrel

In terminal in folder with application type:
```
dotnet run
```
Then go to webpage
```
http://localhost:5091/
```
**Enjoy** **an** **app!**

# Technologies: 
* ASP .Net Core
* C# 
* SQLite
* Entity Framework
