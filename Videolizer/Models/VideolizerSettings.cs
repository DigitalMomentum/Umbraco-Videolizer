using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using static Videolizer.Helpers.SettingsHelper;

namespace Videolizer.Models
{
    [TableName("Videolizer_settings")]
    public class VideolizerSettings
    {


        [PrimaryKeyColumn(AutoIncrement = false)]
        public int ID { get; set; }

        [Length(4000)]
        public string Value { get; set; }


        public T GetValueAsType<T>() {
            if(Value == null)
            {
                return default(T);
            }
           return new JavaScriptSerializer().Deserialize<T>(Value);
        }


        public DateTime LastUpdated { get; set; }
    }
}