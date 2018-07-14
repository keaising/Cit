using CommandLine;
using System;
using System.Collections.Generic;

namespace Shuxiao.Wang.Cit
{

    public class Options
    {
        [Option('c', "clone", Required = false, HelpText = "the git url of github repo.")]
        public String Clone { get; set; }

        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option('p', "path", Required = false, HelpText = "the local github.com path, e.g.: \\c\\source\\github.com.")]
        public String Path { get; set; }
    }
}