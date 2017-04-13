Feature: Manage Music Store
	In order to Manage my music albums
	As a music lovers
	I want to build a music store

Scenario: View All Stored Music Albums
	Given I am on the site home page
	When I open menu "MusicStore"
	Then I should see page "Music Store"
	#And I should see data list

Scenario: Add Album To Music Store
	Given I am on the site home page
	When I open menu "MusicStore"
	And I press "Add" button
	And I populate form with
	| Title     | Price | AlbumArtUrl     |
	| Yesterday | 8.99  | placeholder.png |
	And I press "Save" button
	Then I should see page "Album Adding"
	And I should see "Create alubum successful." on page
	And I should see page "Music Store"

#Scenario: View Album Details in Music Store
#Scenario: Edit Album in Music Store
#Scenario: Remove Album From Music Store
