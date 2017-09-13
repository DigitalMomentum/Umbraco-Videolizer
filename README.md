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
- Login to your vimeo account
- Then you will need to create a free API Key from https://developer.vimeo.com/apps
  - Click Create App
  - Enter a name and description for the App and the URL of your live website. Click *Create App*
  - Once created, click the *Authentication* tab
  - Click *Generate Token* at the bottom of the page
  - Copy the new token for the next step
- Back in the Umbraco Back Office, Open up the DataType under Developer -> DataTypes -> Videolizer
- Paste Your Token into the Vimeo Access Token box.


## How to use

### Document Types
You will need to add the Videolizer data type to one or more of your document types so that the
editors can paste in the video URLs.

### In your Views
There are two ways to use the values from the property editor in the views.

```
@(Model.Content.GetPropertyValue<VideolizerVideo>("video").GetSimpleEmbed("600", "300"))
```

or like this

```
@{ var video = Model.Content.GetPropertyValue<VideolizerVideo>("video"); }
<iframe width="100%" height="281" src="@video.embedUrl" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
 ```
