# Local configuration for this source import

Copy App.config.example to App.config and fill the SQL connection string, PubNub keys and QuickBooks company path locally. The real file is ignored by Git.
This import excludes private runtime data, generated build outputs and old Git history.
Restore the packages listed in packages.config and build with the required .NET Framework and QuickBooks SDK dependencies.
Existing workflows are retained with a .disabled suffix and will not deploy automatically. Review their repository and runner settings before enabling them.
