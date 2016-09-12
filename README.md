# Umbraco7-Videolizer
By David Sheiles

A simple Datatype for pasting in Youtube / Vimeo URLs and embeding the appropriate iFrame  into your site

## Installation
Install the package and a new datatype called 'Videolizer' will be added
Also avaliable via NuGet: 
```Install-Package DigitalMomentum.Videolizer```

## Features
The Property editor has a single textbox for pasting in a website link to
either a YouTube or Vimeo video and extracts the video ID and displays a preview.

In the view, you have access to the Video ID, Service (Youtube/Vimeo), Raw URL, the embed URL
and a function to generate the Embed Code

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
