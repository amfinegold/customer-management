# customer-management
An api widget to manage agents and customers

_CODE CHALLENGE QUESTIONS_

Assumptions from the acceptance criteria:

- Customer management needs more than just a list-all method. Customer list methods should permit pagination/be easily expanded into a robust search method.
  
- this will be deployed through API Gateway and that ip whitelisting/specific CORS origins will be handled in the gateway
  
- there is a deployment path and appsettings.release.json will actually have variables to be substituted in (no point in current implementation as there's no deployment path)


Questions for the PM/Stakeholders:

- How will authentication work? Is everyone able to access the site?

- How will authorization work? is access to the API access to the entire API, or should portions be limited? If so, by what? AgentId?

- Is there a company-wide auth solution? AD? JWT? The api currently has no auth, but in an actual dev/production situation, this would contain authentication to lock it down to the correct group of users.
	
  

_LOCAL INSTALLATION INSTRUCTIONS_

Application will run locally at https://localhost:44387/index.html

To run locally, simply update the appsettings.json value for Database.ConnectionString to point to your local database.


_KNOWN ISSUES_

- BsonClassMap mappings are incorrect - objects return from the database, but their contents do not

-  all CRUD methods - the provided source data uses an int value in the id field, and I ran out of time to create an auto-incrementing integer system to enforce unique id values.

- The originally provided customers.json file contained a duplicate record. I'm nearly certain I re-saved the file once I found and removed the duplicate, but heads up in case you get a nasty error importing the data.

- Version conflicts between the CustomerWidget.Api and CustomerWidget.Test.Unit projects - ran out of time to resolve.

