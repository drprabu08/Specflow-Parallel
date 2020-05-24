Feature: LoginFail
	Login Fail

@mytag
Scenario: Login with wrong credentials
	Given I have navigated to the login page
	And I entered username and password as below
		| Username | Password |
		| 100      | test1    |
	When I press login
	Then the homepage shpuld not be displayed
