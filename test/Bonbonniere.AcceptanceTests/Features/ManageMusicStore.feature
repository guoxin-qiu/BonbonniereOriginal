Feature: Manage Music Store
	In order to Manage my music albums
	As a music lovers
	I want to build a music store

Scenario: View All Stored Music Albums
	Given I am on the site home page with logon
	When I open menu "MusicStore"
	Then I should see page "Music Store"
	And I should see grid with
	| Title       | Price | Release Date | Art Url |                               |
	| Fifth Album | ¥7.80 | 2016-06-06   | N/A     | Edit #$$# Details #$$# Delete |
	| First Album | ¥9.90 | 1999-03-05   | N/A     | Edit #$$# Details #$$# Delete |
	| Forth Album | ¥3.60 | 2013-05-03   | N/A     | Edit #$$# Details #$$# Delete |

@ignore
Scenario: Add Album To Music Store
	Given I am on the site home page with logon
	When I open menu "MusicStore"
	And I follow "Create New"
	And I fill in
	| Title     | Price | Release Date | AlbumArtUrl     |
	| Yesterday | 8.99  | 2017-01-01   | placeholder.png |
	And I hit "Create"
	Then I should see page "Create Album"
	And I should see "Create alubum successful." on page
	And I should see page "Music Store"
	And I should see grid with
	| Title       | Price | Release Date | Art Url         |                               |
	| Fifth Album | ¥7.80 | 2016-06-06   | N/A             | Edit #$$# Details #$$# Delete |
	| First Album | ¥9.90 | 1999-03-05   | N/A             | Edit #$$# Details #$$# Delete |
	| Forth Album | ¥3.60 | 2013-05-03   | N/A             | Edit #$$# Details #$$# Delete |
	| Yesterday   | 8.99  | 2017-01-01   | placeholder.png | Edit #$$# Details #$$# Delete |


#Scenario: View Album Details in Music Store
#Scenario: Edit Album in Music Store
#Scenario: Remove Album From Music Store
