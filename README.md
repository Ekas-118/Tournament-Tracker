# Tournament-Tracker
.NET desktop application for creating and managing tournaments using Windows Forms.

Created by following the [tutorial series by IAmTimCorey on YouTube](https://www.youtube.com/playlist?list=PLLWMQd6PeGY3t63w-8MMIjIyYS7MsFcCi).

## Prerequisites:
- SQL Server

## Original Features
- 5 forms:
  - Tournament Dashboard (Main form)
  - Create Tournament Form
  - Create Team Form
  - Create Prize Form
  - Tournament Viewer Form
- Creating tournaments (including teams, team members and prizes) and saving them to a text file or SQL Server database
- Automatic matchup generation
- Managing of matchup scores
- Sending emails to teams (locally) if they have a new matchup or if the tournament has ended

## Additional Changes And Features
- Minor design changes
- Added an SQL Server Database Project
- Prize Display Form that can be viewed once a tournament has ended
- Text files get saved to the CommonApplicationData special folder
- Emails actually get sent out using Gmail's SMTP server
- Emails get sent using the FluentEmail NuGet Package instead of System.Net.Mail
- The tournament winner gets displayed in the Tournament Viewer Form if the tournament has ended
- Tournaments are now marked as active/inactive in text files as well instead of just the database
- Tournaments can now be deleted
- Checkbox in the Dashboard form to choose whether to show inactive tournaments
- Each tournament can have it's own GreaterScoreWins value instead of it being a global value
- Tournaments can now have an initial prize pool
- No longer possible to change scores once the matchup winner is decided
- No longer possible to gives scores in matchups with only 1 team or in undetermined matchups
- The list in the Dashboard form gets updated once a new tournament is created or if an existing tournament has ended
- Prizes are properly deleted from text files/the database if they are removed in the Create Tournament Form
- No longer possible to open several of the same form
- Create Tournament Form gets validated before creating a tournament
- A few optimizations and bug fixes

## Screenshots
![Tournament Dashboard](https://i.gyazo.com/0906012f592d278a1a146f730ac34a4c.png)
![Create Tournament Form](https://i.gyazo.com/3ecbe568db2291c32a5a886d6087aa30.png)
![Tournament Viewer Form](https://i.gyazo.com/df3731d2ce22c312a80dcfbd92612450.png)
![Prize Display Form](https://i.gyazo.com/97d313f257b3f02964d6e6011559eeac.png)
