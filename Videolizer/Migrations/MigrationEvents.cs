using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Web;

namespace Videolizer.Migrations {
    public class MigrationEvents : ApplicationEventHandler {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            HandleMigration();
        }

        private void HandleMigration() {
            const string productName = "Videolizer";
            var currentVersion = new SemVersion(0, 0, 0);

            // get all migrations for "Statistics" already executed
            var migrations = ApplicationContext.Current.Services.MigrationEntryService.GetAll(productName);

            // get the latest migration for "Statistics" executed
            var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

            if (latestMigration != null)
                currentVersion = latestMigration.Version;

            var targetVersion = new SemVersion(1, 0, 0);
            if (targetVersion == currentVersion)
                return;

            var migrationsRunner = new MigrationRunner(
              ApplicationContext.Current.Services.MigrationEntryService,
              ApplicationContext.Current.ProfilingLogger.Logger,
              currentVersion,
              targetVersion,
              productName);

            try {
                migrationsRunner.Execute(UmbracoContext.Current.Application.DatabaseContext.Database);
            } catch (Exception e) {
                LogHelper.Error<MigrationEvents>("Error running Statistics migration", e);
            }
        }
    }
}