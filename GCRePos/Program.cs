using CommandLine;
using System;
using System.Collections.Generic;

namespace GCRePos
{
    class Program
    {
        const string XPattern = @"X(?<value>\d+\.\d{4})";
        const string YPattern = @"Y(?<value>\d+\.\d{4})";


        static void Main(string[] args)
        {
            bool hasErrors = false;
            CmdArgs cmdArgs = null;
            
            Action<IEnumerable<Error>> printErrors = (errors) => 
            {
                hasErrors = true;
                foreach (var e in errors) {
                    Console.WriteLine(e.ToString());
                }
            };

            Parser.Default.ParseArguments<CmdArgs>(args)
                          .WithParsed(x => cmdArgs = x)
                          .WithNotParsed(printErrors);

            if (!(hasErrors || cmdArgs == null))
            {
                StartProcess(cmdArgs);
            }
        }

        static void StartProcess(CmdArgs args)
        {
            var file = new GFile(args.InFile, args.OutFile);
            var xOffset = new OffsetController(args.OffsetX);
            var yOffset = new OffsetController(args.OffsetY);

            file.Reset();
            foreach (var line in file.ReadLines())
            {
                if (RegexHelper.TryParse(line, XPattern, out double x))
                {
                    xOffset.Approve(x);
                }
                if (RegexHelper.TryParse(line, YPattern, out double y))
                {
                    yOffset.Approve(y);
                }
            }

            string output;
            foreach (var line in file.ReadLines())
            {
                output = line;
                output = RegexHelper.Replace(output, XPattern, xOffset);
                output = RegexHelper.Replace(output, YPattern, yOffset);

                file.WriteLine(output);
            }
            
            Console.WriteLine($"Created file: {args.OutFile}");
            Console.WriteLine(xOffset.PrintInfo("X"));
            Console.WriteLine(yOffset.PrintInfo("Y"));
        }
    }
}
