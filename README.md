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


_NOT YET IMPLEMENTED_
- global exception-handling to standardize overall error shape and allow differentiation of validatation-specific exceptions, other expected exceptions, and unexpected exceptions (did not have time to complete)

- logging - I've been working primarily with an enterprise logging system and don't have a tremendous amount of insight into what the best loggers out there today are. (under the time constraint, chose to work on existing code rather than take the time needed to research properly)

- security, generally. See in-code comments about CORS.


_GENERAL NOTES_

 I approached this knowing that I would very likely run into issues I wouldn't know how to resolve because my experience is overwhelmingly with applications already at the base "everything basic to run/log/handle exceptions/handle access is implemented" level.
This is one of the first times I've bootstrapped an app, and my first time ever bootstrapping an app connecting to a MongoDB database. Between that and the time limit for the code challenge, there are missing and broken features as outlined in the Known Issues and Not Yet Implemented sections. 

During this challenge, when faced with the choice of spending the limited challenge time researching something I've never had to make a decision about before, implementing an unresearched version that may or may not be in the same zip code as best practices, or simply leaving it for later with a TODO or a note here, I chose the third option. This leaves me with minimal rework to implement each missing feature once I'm able to research and determine the best option for the app, and has the benefit of providing me with a handy checklist of features to consider the next time I'm bootstrapping an application or writing a template to bootstrap APIs against.
