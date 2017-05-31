Feature: Menu
	In order to ensure that the menu works well
	As an user
	I want to check the menu correct

Scenario Outline: Navigate From Menu
	Given I am on the site home page with logon
	When I open menu "<MenuTitle>"
	Then I should see page "<PageTitle>"
	Examples: 
	| MenuTitle | PageTitle |
	| Home      | Home Page |
	| Contact   | Contact   |
	| About     | About     |
	| Register  | Register  |