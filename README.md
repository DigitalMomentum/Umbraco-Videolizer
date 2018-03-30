# Umbraco7-Videolizer
By David Sheiles - [Digital Momentum](http://www.digitalmomentum.com.au)

An Umbraco Data Type that allows you to search and pick from YouTube and Vimeo videos. 

It can also be configured to search and pick from your own YouTube/Vimeo video feeds.


## Features
- Supports both YouTube and Vimeo Videos
- Add videos by pasting the VideoURL
- Seach your YouTube Channel OR Vimeo feed to select your videos
- Easy embedding into the templates OR access to properties to customise the video yourself
- Video Preview for the content editors
- Strongly Typed models for Models Builder
- Works with nested content


### Installation
Install the package and a new datatype called 'Videolizer' will be added

Download the Latest Umbraco package from [here](https://github.com/DigitalMomentum/Umbraco7-Videolizer/releases/) 

Also avaliable via NuGet: 
```Install-Package DigitalMomentum.Videolizer```

You can then add the new Data Type to your DocTypes and your ready to go. You may want to configure the DocType to enable searching

### Configuration
Without any configuration, Content Editors will be able to copy/paste of urls directly into the Property Editor, however extra steps are nessesary to enable searches

#### YouTube Configuration
- First you will need to create a free API Key from https://console.developers.google.com/apis/credentials  
  - Click "Dashboard" and then "Enable APIS and Services"
  - Select *YouTube Data API* and then click *Enable*
  - Click Credentials in the menu and then *Create Credentials* -> *API Key*
  - Copy The Key for the next Step
- Back in the Umbraco Back Office, Open up the DataType under Developer -> DataTypes -> Videolizer
- Paste Your YouTube API Key in to the box.

To Enable Searching your Channel (rather than a public search), you will need to supply your YouTube Channel ID.

- Log Into your YouTube Account
- Click your profile image and then "My Channel"
- Copy the part of the URL that looks like UCwCDfuhoUHOZJPtDDMS1aCA (between the "https://www.youtube.com/channel/" and the "?"). This is your Channel ID
- Paste your channel ID into the YouTube Channel ID in the DataType 

#### Vimeo Configuration

This plugin allows you to search both public video and videos in your library that are publlic. 
*Note: Videolizer does not currently support searching protected video files*
- Login to your vimeo account
- Then you will need to create a free API Key from https://developer.vimeo.com/apps
  - Click Create App
  - Enter a name and description for the App and the URL of your live website. Click *Create App*
  - Once created, click the *Authentication* tab
  - Here you will find your Client Identifier and Client Secret for the next step
- Back in the Umbraco Back Office, Open up the DataType under Developer -> DataTypes -> Videolizer
  - Paste Your Client Identifier and Client Secret into the settings for Videolizer.
- If you would like to search within only your videos you will need to get your User ID  
  - Go to https://vimeo.com/settings/account/general amd look for the User ID that is shown on he page
  - Past it in as the Vimeo User ID field and this will allow you to search public videos under your account
 

## How to use

### Document Types
You will need to add the Videolizer data type to one or more of your document types so that the
editors can paste in the video URLs.

#### In your Views
There are two ways to use the values from the property editor in the views.

```
@(Model.Content.GetPropertyValue<VideolizerVideo>("video").GetSimpleEmbed("600", "300"))
```

or like this

```
@{ var video = Model.Content.GetPropertyValue<VideolizerVideo>("video"); 
if(video.HasVideo())
	<iframe width="100%" height="281" src="@video.EmbedUrl" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
}
 ```
## Grid Editor
A YouTube/Vimeo option will now appear as a selection in your grid editor. It will allow youto paste in a URL to Youtube / Vimeo video and 
it will display a preview video as well as alloing to select options for:
* Auto Play
* Loop
* Show Title (Vimeo Only)
* Show Byline (Vimeo Only)
* Start time (YouTube Only)
* Display Related Videos (YouTube Only)
* Show/Hide Controls (YouTube Only)

### Rendering in the template
The new video should appear on the site without any need for code. 

For us to make the vide's responsive by default, we have added some 
inline styles in the partial view. 

We highly reccomend moving these styles to your stylesheet or using the 
bootstrap responsive framework. You can find the partial view under `/Views/Partials/Grid/Editors/Videolizer.cshtml`

We didnt like doing this, but want to make the video responsive for 
the grid layout out of the box.

## Contributing to Videolizer
We'd love your help and feedback. You can help in a couple of ways:

### Bugs, features and suggestions
The easiest way for you to help us out is by submitting bugs via github issue so that we can improve the plugin for everyone.

### Pull Requests
Better still... you can help us solve issues by submitting pull requests for bug fixes and new features.

You can get up and running with the project in visual studio. There are 3 main parts to this project:
 - *Videolizer*: This is the umbraco plugin portion of the project which mainly deals with the propery editor files and functionality unique to Umbraco
 - *Videolizer.Core*: We split out the business logic of the plugin, so that you can utilize some of the features in .net projects outside of Umbraco
 - *VideoLizer.Web*: This is a simple umbraco install to test the plugin updates. Building the solution will copy all the nessesary files videolizer files
 into the VideoLizer.Web project. I have included the SQL Compact database, so that you simply need to run the project to get started. 
The backoffice username is **admin** and password is **password**

Contact us if your stuck in getting setup.
