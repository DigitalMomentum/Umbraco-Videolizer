using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Videolizer.Core.Models;
using Videolizer.Models;

namespace Videolizer.Helpers
{
    public class SettingsHelper
    {
        public enum SettingTypes
        {
            YT_AppKeys = 100,
            YT_TokenSet = 110,
            Vimeo_AppKeys = 200,
            Vimeo_TokenSet = 210,
        }


        private UmbracoDatabase db;

        public SettingsHelper() {
            db = ApplicationContext.Current.DatabaseContext.Database;
        }

        public SettingsHelper(UmbracoDatabase db)
        {
            this.db = db;
        }

        public void Set(SettingTypes settingType, string value)
        {

            var setting = db.SingleOrDefault<VideolizerSettings>((int)settingType);

            if (setting == null)
            {
                setting =  new VideolizerSettings()
                {
                    ID = (int)settingType,
                    LastUpdated = DateTime.Now,
                    Value = value
            };

                db.Insert(setting);
            }
            else
            {
                setting.LastUpdated = DateTime.Now;
                setting.Value = value;
                db.Save(setting);
            }

           
        }

        internal TokenSet RefreshYTTokenIfExpired(TokenSet accessToken)
        {
            if (accessToken.Expires < DateTime.Now)
            {
                ProviderAppDetails appKeys = Get(SettingTypes.YT_AppKeys).GetValueAsType<ProviderAppDetails>();
                Core.Auth myAuth = new Core.Auth(Core.Enums.ProviderType.YouTube, appKeys);

                accessToken = myAuth.RefreshAccessToken(accessToken);

                Set(SettingTypes.YT_TokenSet, accessToken);
            }

            return accessToken;
        }

        public void Set(SettingTypes settingType, object value)
        {

            var setting = db.SingleOrDefault<VideolizerSettings>((int)settingType);

            if (setting == null)
            {
                setting = new VideolizerSettings()
                {
                    ID = (int)settingType,
                    LastUpdated = DateTime.Now,
                    Value = new JavaScriptSerializer().Serialize(value)
            };

                db.Insert(setting);
            }
            else
            {
                setting.LastUpdated = DateTime.Now;
                setting.Value = new JavaScriptSerializer().Serialize(value);
                db.Save(setting);
            }


        }

        public VideolizerSettings Get(SettingTypes settingType) {
            return db.SingleOrDefault<VideolizerSettings>((int)settingType);
        }

        public VideolizerSettings GetDynamic(SettingTypes settingType) {
            return db.SingleOrDefault<VideolizerSettings>((int)settingType);
        }
    }
}