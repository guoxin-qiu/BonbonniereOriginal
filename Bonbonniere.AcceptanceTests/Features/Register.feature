Feature: Register a new site user
	In order to use this site
	As a potential site user
	I want to be able to register for a new account on this site

Scenario: Register Successfully
	Given I am on the site home page
	When I open menu "Register"
	And I populate form with
	| Username | Email          | Password  | ConfirmPassword |
	| Denis   | denis@qyq.net | p@55w0rd! | p@55w0rd!       |
	And I press "Register" button
	Then I should see page "Registration"
	And I should see "Account Created!" on page
	And I should see "Denis" on page
	And I should see "denis@qyq.net" on page

Scenario Outline: Register Failed
	Given I am on the site home page
	When I open menu "Register"
	And I populate form with
	| Username   | Email   | Password   | ConfirmPassword   |
	| <Username> | <Email> | <Password> | <ConfirmPassword> |
	And I press "Register" button
	Then I should see page "Register"
	And I should see the error message "<ErrorMessage>"
	Examples: 
	| Username | Email         | Password  | ConfirmPassword | ErrorMessage                               |
	|          | denis@qyq.net | p@55w0rd! | p@55w0rd!       | Username is required.                      |
	|          |               | p@55w0rd! | p@55w0rd!       | Username is required.Email is required.    |
	| Dennis   | invalid@      | p@55w0rd! | p@55w0rd!       | Email is not valid.                        |
	| Dennis   | denis@qyq.net | p@55w     | p@55w           | Password must be longer than 5 characters. |
	| Dennis   | denis@qyq.net | p@55w0rd! | p@55w0rd        | Password not match.                        |