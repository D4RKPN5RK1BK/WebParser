﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.CommandLine
{
	internal class CommandLineOptions
	{
		[Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
		public IEnumerable<string> InputFiles { get; set; }

		// Omitting long name, defaults to name of property, ie "--verbose"
		[Option(Default = false, HelpText = "Prints all messages to standard output.")]
		public bool Verbose { get; set; }

		[Option("stdin", Default = false, HelpText = "Read from stdin")]
		public bool stdin { get; set; }

		[Value(0, MetaName = "offset", HelpText = "File offset.")]
		public long? Offset { get; set; }

		public static int RunDisplayPageData(DisplayPageData options)
		{
			return 0;
		}

	}
}
