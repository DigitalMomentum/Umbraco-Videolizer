using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Videolizer {
    [PropertyValueType(typeof(VideolizerVideo))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class VideoPickerPropertyEditorValueConverter : PropertyValueConverterBase {

        public override bool IsConverter(PublishedPropertyType propertyType) {
            return propertyType.PropertyEditorAlias.Equals("DigitalMomentum.Videolizer");
        }

        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview) {
            if (source == null) return new VideolizerVideo();

            var sourceString = source.ToString();

            try {
                var obj = JsonConvert.DeserializeObject<VideolizerVideo>(sourceString);

                if (obj != null)
                    return obj;

                return new VideolizerVideo();

            } catch (Exception ex) {
                return new VideolizerVideo();
            }

        }


    }
}