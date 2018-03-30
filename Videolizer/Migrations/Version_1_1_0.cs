﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Services;

namespace Videolizer.Migrations {
    [Migration("1.0.0", 1, "Videolizer")]
    public class Version_1_1_0 : MigrationBase {
        public Version_1_1_0(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger) {
        }

        public override void Up() {
            //Create a new Data Type called "Videolizer"
            DataTypeService dataTypeService = (DataTypeService)ApplicationContext.Current.Services.DataTypeService;

            if (dataTypeService.GetDataTypeDefinitionByName("Videolizer") == null) { //check to see if it already exists
                //Create a new one
                DataTypeDefinition VideolizerDataTypeDef = new DataTypeDefinition("DigitalMomentum.Videolizer");
                VideolizerDataTypeDef.Name = "Videolizer";

                dataTypeService.Save(VideolizerDataTypeDef);
            }

        }

        public override void Down() {
            //Nothing to do!
        }
    }
}