using System;
using System.Collections.Generic;
using System.Text;

namespace XplaneAirportParser
{
	class Program
	{
		public static void Main(string[] args)
		{ 
			Parser parser =  new Parser();
			parser.StartParsing();
		}
	}
}
