# Ultimate-Asp.NET-Core-Web-API
My implementation of CompanyEmployees app in the Ultimate Asp.NET Web API book with PostgreSQL and Asp.Net core 7.

## Building and running CompanyEmployees
Clone the repository to your local machine:

```shell
git clone https://github.com/build5/Ultimate-Asp.NET-Core-Web-API.git
```
Download and Install Docker desktop.
Create data volume:
```shell
docker volume create postgres-data
```
Run PostgreSQL in Docker with following configuration:
```shell
docker run -d -v postgres-data:/var/lib/postgresql/data -p 5432:5432 --name db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=example postgres
```
Navigate into CompanyEmployees directory. Run Database update using efcore tools:
```shell
dotnet ef database update
```
Run the application:
```shell
dotnet restore
dotnet build
dotnet run
```
Download [Postman](https://www.postman.com)
## Note
+ **The api rate limit is 3 requests per 5 minutes. This behaviour could be changed in [ServiceExtensions](CompanyEmployees/CompanyEmployees/Extensions/ServiceExtensions.cs)**
+ **The GetCompanies action is protected by authorization and needs a user with Manage role.**
## Supported endpoints
Get all Companies:

https://localhost:5001/api/companies/

Get a company by providing the company's id ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}

Get all employees for a company by providing company's id ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}/employees

Get one employee for a company by providing company's id and employee's id ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}/employees/{employeeid}

Get collection of companies by prodiving multiple company's ids ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/collection/{companyid},{companyid})

Post a company to create a company:

https://localhost:5001/api/companies/

Post an employee for a company to create an employee ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}/employees

Delete a company by providing company's id ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}

Delete an employee for a company by providing company's id and employee's id ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}/employees/{employeeid}

Update a company by providing company's id ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}

Partially update an employee for a company by providing company's id and employee's id ({} is a place holder and replace it by actual Guid id):

https://localhost:5001/api/companies/{companyid}/employees/{employeeid}

Create new User: 

https://localhost:5001/api/authentication

Login endpoint:

https://localhost:5001/api/authentication/login
