using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Services;

namespace Videolizer.Migrations {
    [Migration("1.2.0", 2, "Videolizer")]
    public class Version_1_2_0 : MigrationBase {
        private readonly UmbracoDatabase _database = ApplicationContext.Current.DatabaseContext.Database;
        private readonly DatabaseSchemaHelper _schemaHelper;
        public Version_1_2_0(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger) {
            _schemaHelper = new DatabaseSchemaHelper(_database, logger, sqlSyntax);
        }

        public override void Up()
        {
            //Create a table to store global settings
            _schemaHelper.CreateTable<Videolizer.Models.VideolizerSettings>(false);


        }

        public override void Down() {
            _schemaHelper.DropTable<Videolizer.Models.VideolizerSettings>();
        }
    }
}