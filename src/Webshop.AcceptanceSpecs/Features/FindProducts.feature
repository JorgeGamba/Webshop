Feature: Find products
	In order to buy more products
	As a customer
	I want to find quickly and exactly wanted products

@end_to_end
Scenario: Full use case
	Given I have searched for a product
	And I have filtered the search results
	And I have ordered the search results
	Then all the search results should match with the search criteria
	And all the search results should match with the filter criteria
	And all the search results should appear in the requested order
