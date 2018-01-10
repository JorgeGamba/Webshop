Feature: Registration of new products
	In order to maintain the product information updated
	As a sales manager
	I want register new products

Scenario: The input data is valid
	Given I have provided all the information valid
	When I request to register
	Then the new product should be registered
	And I should see the product list

Scenario: The input data is invalid
	Given I have provided some invalid information
	When I request to register
	Then the new product should not be registered
	And I should be informed that my data is invalid
	And I should be able to try again fixing the data
