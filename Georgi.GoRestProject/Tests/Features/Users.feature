Feature: Users


Scenario: Get All Users
	Given I prepare a request for <endpoint> GET
	When I get all users from the users endpoint
	Then The response status code should be <statusCode>
		And the response should contain a list of users
Examples: 
| endpoint | statusCode |
| users    | OK         |
| /%       | NotFound   |


@Authenticate
Scenario: Create a new user with OK 200
	Given I want to create new user request body
	When I send a request to the <email> <endpoint>
	Then the response status code should be <statusCode>
		And  The user should be created successfully

Examples: 
| email | statusCode          | endpoint |
| @@/   | UnprocessableEntity | users    |
| m     | Created             | users    |


@Authenticate
Scenario: Update an existing user
	Given I want to create new user request body
	When Update I send a request to the <endpoint>  update with <email>
	Then The response status code for update should be <statusCode> 
		And The user should be updaates successfully
Examples: 
| email | statusCode | endpoint |
| @@/   | NotFound   | users    |
| m     | OK         | users    |


