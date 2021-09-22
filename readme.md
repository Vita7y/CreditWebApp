# Technical Test Application #

This is my application to test new technologies

## Credit Services Tech Test WebApi ##

Created a REST API for a simple data set that provide (**data.json**).

The Credit REST API allow for the following actions on the resource:
 
- create
- read
- update 
- delete 

## Docker: ##

Create image: `docker build -t creditwebapp .`

Run container: `docker run -p 5000:5000 -p 5001:5001 creditwebapp`

BasicAuthentication: 
- UserName =`userName` 
- Password =`password`
