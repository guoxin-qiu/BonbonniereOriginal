Feature: Register a new site user
	In order to use this site
	As a potential site user
	I want to be able to register for a new account on this site

@useconfig
Scenario: Register Successfully
	Given I am on the site home page
	When I open menu "Register"
	And I fill in
	| Username | Email         | Gender | FirstName | LastName | Password  | ConfirmPassword | Favorite          | Gender2 | Favorite2           |
	| Denis    | denis@qyq.net | Female | Denis     | Qiu      | p@55w0rd! | p@55w0rd!       | Orange^Watermelon | Male    | Orange^Apple^Banana |
	And I hit "Register"
	Then I should see page "Home Page"

Scenario Outline: Register Failed
	Given I am on the site home page
	When I open menu "Register"
	And I fill in
	| Username   | Email   | FirstName   | LastName   | Password   | ConfirmPassword   |
	| <Username> | <Email> | <FirstName> | <LastName> | <Password> | <ConfirmPassword> |
	And I hit "Register"
	Then I should see page "Register"
	And I should see the error message "<ErrorMessage>"
	Examples: 
	| Username | Email         | FirstName | LastName | Password  | ConfirmPassword | ErrorMessage                               |
	|          | denis@qyq.net | Denis     | Qiu      | p@55w0rd! | p@55w0rd!       | Username is required.                      |
	|          |               | Denis     | Qiu      | p@55w0rd! | p@55w0rd!       | Username is required.Email is required.    |
	| Dennis   | invalid@      | Denis     | Qiu      | p@55w0rd! | p@55w0rd!       | Email is not valid.                        |
	| Dennis   | denis@qyq.net | Denis     | Qiu      | p@55w     | p@55w           | Password must be longer than 5 characters. |
	| Dennis   | denis@qyq.net | Denis     | Qiu      | p@55w0rd! | p@55w0rd        | Password not match.                        |
	| Dennis   | denis@qyq.net |           | Qiu      | p@55w0rd! | p@55w0rd!       | The FirstName field is required.                     |
	| Dennis   | denis@qyq.net | Denis     |          | p@55w0rd! | p@55w0rd!       | The LastName field is required.                      |