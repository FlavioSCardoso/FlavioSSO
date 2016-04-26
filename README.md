# FlavioSSO

Solução Single Sign On que autentica o usuário via LDAP e o persiste sua sessão em um SQL Server, junto com seus grupos para uso em Autorização.

A solução ainda precisa:
* Melhorar tratamento de exceções
* Gravar logs de erros
* Gravar logs de eventos dos usuários

About this document
===================
This document contains instructions to run and configure the application. Also contains notes and feedback about the assingnment tests.

Introduction
------------
1. The development environment was composed by:
 - Windows 10 Pro
 - This solution was developed using Visual Studio Community 2015 Update 1
 - All the projects was build using version 4.6 of the .net framework
 - Microsoft SQL Server 2014 Express instaled. The solution uses LocalDB
 - Hyper-V
   . Windows 2012 R2 Evaluation running Active Directory Domain Services
     * The host machine was not joined in the testing domain 
 - The fake domain was configured in hosts file (%SystemRoot%\System32\drivers\etc\hosts) for name resolving

2. Files/Folders (in file system):
 -packages - Visual Studio folder.
 - ReadmeFiles - Files for this document.
 - Recording - The recording files for this assignment test.
 - src - Projects and its files in this solution.
 - TestResults - Visual Studio folder.
 - tests - Test projects for this solution and its files.


Steps to install and configure any prequisites for the development environment
------------------------------------------------------------------------------
 - Visual Studio 2015 Update 1
 - .net Framework 4.6
 - Microsoft SQL Server 2014 Express instaled. The solution uses LocalDB
 - An Active Dicrectory in reachable by the machine and with its name resolvable by the local machine

Steps to prepare the source code to build property
--------------------------------------------------
 - Open Flavio.SSO.sln file with Visual Studio
 - Configure the solution (see 'Observations to run this solution' section above)
 - Press F5

Assumptions made and missing requirements that are not covered in the requirements
----------------------------------------------------------------------------------
General considerations
 1. The software will run on the company’s intranet. For this reason, the SSO solution doesn’t supports multi domain environment. 
 2. Considerations about the webservice:
    - The webservice’s endpoint was configured to use HTTP instead of HTTPS just because of possible problems installing it on IIS express for tests on development and evaluators machines. HTTPS is the preferable way to secure communication.
    - A solution that we could call one of the “desirable implementations” is to set the HTTP Module to automatically detect the application’s core DLLs in GAC and use them instead of using HTTP request to validate the user.
 3. I tryied ty maintain a lot of configurable options. As there was no meeting or interview for the project. I realized some configurations. Better things can shurelly be done.
 4. Assemblies not signed for GAC. They could be centralized in the servers.

Observations to run this solution
---------------------------------
1. Run the Visual Studio as Administrator. It is needed by the next topic (Configuring Namespace Reservations).
   This solution was developed using Visual Studio 2015 and no test was made using other version.
2. Configuring Namespace Reservations (https://msdn.microsoft.com/en-us/library/ms733768(v=vs.110).aspx)
   Yes, had this issue in my personal machine, never happened in my dev machine at work and I didn't get time to dig more information about it. Here is the Microsoft documentation: Configuring Namespace Reservations.
   I don't know your policies to run the solution unit tests, but I'd bet on run the tests without this configuration. If a test fails, set this up and try again. 
   Open the command prompt with Administrator privileges and run the folowing command, changed with your user account (if your machine is not domain joined, you can put only your usename, like user=Flavio): 
         netsh http add urlacl url=http://+:1234/UserService/UserService user=DOMAIN\user
   This needed for use in Unit Tests for the service because it initiates a service listening port 1234. Please, make shure you set the correct user name. 
3. The mainConfig.xml file, located with the solution file at the root of the zip file, contains the appSettings section used to configure the tests projects and the aplications itself. Here is an explanation of the parameters: 
   - Domain - The Active Directory name. If the machine which will run this application is not domain joined, the hosts file (%SystemRoot%\System32\drivers\etc\hosts) must be configured to resolve a Domain Controller IP address. 
       Open your hosts file and insert a line like that at its bottom: Example
            192.168.0.1 domain.local
       If the machine which will run the solution is domain joined it's already able to find a Domain Controller. 
   - DomainUserName - The username which will be used to connect to AD to retrieve information for the unit testing projects.
   - DomainUserPassword - The password for connect to Active Directory to retrieve information for the unit testing projects.
   - DomainUserGroupCount  - Is the number of user's authorization groups in Active Directory.  If you let it blank part of ShouldAuthenticateUserAndRetrieveGroups test will be ignored.  
   - SSO-ServiceUrl  - This parameters is part of the project itself. It points the WebService project's url. The actual configuration is the same as configured in the Web Service's project properties. If you change the Web Server Property of Flavio.SSO.Core.Services project, you must reconfigure this parameter to the new url. 
   - SSO-LoginUrl  - This parameters is part of the project itself. It sets the Login/Logout WebSite's project url. The actual configuration is the same as configured in the Login/Logout WebSite's project properties. If you change the Web Server Property of Flavio.SSO.Core.UI.Web.Login project, you must reconfigure this parameter to the new url. 
   - SSO-LogoutUrl  - This parameters is part of the project itself. It sets the Logout controller in the Login/Logout WebSite's project url. The actual configuration is the same as configured in the Login/Logout WebSite's project properties. If you change the Web Server Property of Flavio.SSO.Core.UI.Web.Login project, you must reconfigure this parameter to the new url. 
   - SSO-CookieName - This parameters is part of the project itself. It defines the security token cookie name. To change the default cookie's name, alter this option. 
4. You must have SQL Server LocalDb installed.
5. After setting the configurations at mainConfig.xml, just hit F5 in Visual Studio. The solution is configured to open 4 browser instances:
   - 1. The web service site
   - 2. The login web site
   - 3. The demo WebSite 1
   - 4. The demo WebSite 2
   This is needed to avoid setting up all websites in IIS. 
6. After the settings you can also run the tests in Visual Studio. 

**IMPORTANT: Please atention! if a Server setting in project's Properties/Web configuration is changed, tie mainConfig.xml file must be reconfigured.

The solution itself
-------------------
The image in file ReadmeFiles\solutionExplorer.png shows the solution organized architecturally over the architecture (DDD Domain Driven Design) layers and other parts of the solution.
The visible folders (logical) are Solution Folders used to separate the Projects
The projects are: 
 - Client
   . Flavio.SSO.Client - This is the HTTP Module.
 - Samples
   . Flavio.SSO.Samples.Web.Site1 - This web site is and ASP.Net MVC application used to expose the systems functionality. This site has the HTTP moduled installed. 
   .  Flavio.SSO.Samples.Web.Site2  - This web site is and ASP.Net WebForms application used to expose the systems functionality. This site has the HTTP moduled installed. 
 - Server
   . Core
     = Application
       . Flavio.SSO.Core.Application - This is the Application layer proposed in DDD, which exposes host logic the domain functionality . 
     = Crosscutting
       . Flavio.SSO.Core.CrosCutting.IoC - This project contains Invesion Of Control settings in the Crosscuting DDD layer. The crosscutting layer is accessible by all layers.
     = Domain
       . Flavio.SSO.Core.Domain - This is the Domain layer proposed in DDD
     = Infrastructure
       . Flavio.SSO.Core.Infra.Data - This project contains all the SQL Server Data Access. This is part of the Infrastructure layer.
       . Flavio.SSO.Core.Infra.LDAP - This project contains the parts to access Directory services to achieve authentication and authorization principals. This is part of the Infrastructure layer.
     = Services
       . Flavio.SSO.Core.Services - This is an Web Application project which exposes the domain functionality through services using WCF.
     = Tests - These are the Core Unit Test projects. 
   . UI
     . Flavio.SSO.Core.UI.Web.Login - This is the project for login and logout users.
