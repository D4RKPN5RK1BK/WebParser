using CommandLine;

namespace WebPareser.CommandVerbs {
	[Verb("config", false, HelpText = "Configure database connection and log options")]
	class ConfigOptionsVerb {
		[Option('d', "database-folder", Required = false, HelpText = "configure database connection string")]
		public string DatabaseFolder { get; set; }

		[Option('r', "restore-defaults", HelpText = "Restores default config options")]
		public bool RestoreDefaults { get; set; }

		[Option("use-sqlite", HelpText = "Set Database Settings for SQLLite connection")]
		public bool UseSQLLite { get; set; }

		[Option("use-mysql", HelpText = "Set Database Settings for MySQL connection")]
		public bool UseMySQL { get; set; }

		[Option("use-sqlserver", HelpText = "Set Database Settings for SQL-Server connection")]
		public bool UseSQLServer { get; set; }
	}
}