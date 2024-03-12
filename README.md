# DevOps Deploy
This app has a set of Releases, Environments, Projects and Deployments which are loaded from JSON files. 
Given a parameter deciding how many releases should be kept to reduce used space, it will return that amount of releases for each project/environment combo and return a distinct set of releases.

### Assumptions or Questions
- I went for a Clean Architecture approach with CQRS as I felt it would scale well should the requirement to implement 
  CRUD actions arise.
- Calling the `Environment`, `Deployment` and `Project` services felt out of the jurisdiction of a `Release` service 
  query. I thought aggregating the service calls in a layer above the `GetReleasesQuery` handler *may* improve it 
  logically, but I was weary of it turning into an onion.
- Mediatr really shines with async calls which this app doesn't have due to the requirement of not using a database. 
  I figure we can make use of these async calls when the app scales (requiring more than text files as data sources),
  and it still simplified communication between objects with the decoupling it provides. 

### Ideas or improvements
- Use a simple database like an EFCore DbContext. Trying to build this without a database didn't seem like best 
  practices by storing data in JSON files.
- Create POST endpoints to submit a new Project, Environment, Deployment or Release. A release request may only 
  return 200 if the associated project is present. Likewise for a Deployment requiring an existing Environment.
- Add DTO model mapping should the API consumer require different data than what is stored. 
- Use Fluent Validation for validation on POST requests.
- Address cross-cutting concerns such as logging and validation with Mediatr.
