Feature: Login
	Login to application

@smoke
Scenario: Perform Login of Application site
	Given I launch the application
	And I enter the following details
		| Username | Password |
		| 100      | test1    |
	When I click login button
	Then I should navigate to the home screen
