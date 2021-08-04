using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Videolizer.Umb;

namespace Videolizer.Umb.PropertyValueConverter {
    [PropertyValueType(typeof(VideolizerVideo))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class VideoPickerPVC : PropertyValueConverterBase {

        public override bool IsConverter(PublishedPropertyType propertyType) {
            return propertyType.PropertyEditorAlias.Equals("DigitalMomentum.Videolizer");
        }

        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview) {
            if (source == null) return new UmbVideolizerVideo();

            var sourceString = source.ToString();

            try {
                var obj = JsonConvert.DeserializeObject<UmbVideolizerVideo>(sourceString);
               //var obj = new JavaScriptSerializer().Deserialize<UmbVideolizerVideo>(sourceString);

                if (obj != null) {
                    return obj;
                }
                return new UmbVideolizerVideo();

            } catch {
                try {
					//Not a Videolizer Object. Maybe its just a URL string and we can convert it?
					UmbVideolizerVideo newVideo = new UmbVideolizerVideo(sourceString);
                    return newVideo;
                } catch {
                    return new UmbVideolizerVideo();
                }
                
            }

        }


    }
}
