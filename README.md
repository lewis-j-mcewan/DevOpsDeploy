# Octopus Deploy Coding Challenge
### Assumptions or Questions
- Trying to figure out the best architectural approach was tricky. I went for a Clean Architecture approach with CQRS. 
  I've never implemented CQRS with multiple object types/database tables (only read and write on a single type like 
  "Release"), so was unsure how to compile the data for my GetReleasesQuery.
  <br>
  Calling the Env, Deployment and Project Services felt out of the jurisdiction of a Release Service Query. I thought 
  aggregating services calls in a layer above the Release Service may improve it logically, 
  however that would also extract the logic into another layer and I was weary of it turning into an onion.
- Mediatr is a useful tool for decoupling object communication, however I found it works best with async calls. 
  Despite our data source not requiring async, I thought the decoupling it created was beneficial and the async calls 
  would be useful had we been able to use an In-Memory DbContext as our data source.  

### Ideas or improvements
- Use a simple database like an EFCore DbContext. 
  Trying to build this without a database felt like I was going against best practices by loading the data in via the 
  sample data files. 
  This also made it trickier to test as I couldn't input data easily without raising the input data filepath to the 
  top to use separate files for the tests.
- Create POST endpoints to submit a new Project, Environment, Deployment or Release. A release request may only 
  return 200 if the associated project is present. Likewise for a Deployment/Environment.
- Add more complex error handling and validation on input by making use of the IPipelineBehaviour provided by Mediatr 
  (validation could be created with FluentValidation).