Feature: Search products by title
	In order to find quickly and exactly wanted products
	As a customer
	I want to search products by title

Scenario: There are no matching products
	Given I have provided a text to search
	And there are no products that match the searched text
	When I request the search
	Then I should be informed that no matches were found
	And I should be able to try again with other criteria

Scenario: There are multiple matching products
	Given I have provided a text to search
	And there are multiple products that match the searched text
	When I request the search
	Then I should see all the matching products
	And I should be able to try again with other criteria

Scenario: the searched request is invalid
	Given I have provided an invalid text to search
	When I request the search
	Then I should be informed that my search request is invalid
	And I should be able to try again with other criteria
