using CommandLine;

namespace GCRePos
{
    class CmdArgs
    {
        [Option('i', "input", Required = true, HelpText = "File to process.")]
        public string InFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output filename.")]
        public string OutFile { get; set; }

        [Option('x', "OffsetX", Default = 0.0, HelpText = "X Offset.")]
        public double OffsetX { get; set; }

        [Option('y', "OffsetY", Default = 0.0, HelpText = "Y Offset")]
        public double OffsetY { get; set; }
    }
}
