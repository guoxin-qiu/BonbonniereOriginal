Feature: WordTutor
	In order to better use English
	As an English beginner
	I want to remember more words

Scenario: WordTutor
	Given I am on the site home page
	When I open menu "WordTutor"
	Then I should see page "WordTutor Home"
	And I should see "Welcome to the WordTutor home." on page
