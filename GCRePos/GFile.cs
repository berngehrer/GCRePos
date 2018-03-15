using System.Collections.Generic;
using System.IO;

namespace GCRePos
{
    class GFile
    {
        FileInfo _infile, _outfile;

        public GFile(string file, string output)
        {
            _infile = new FileInfo(file);
            _outfile = new FileInfo(output);
        }

        public IEnumerable<string> ReadLines()
        {
            return _infile.Exists
                    ? File.ReadAllLines(_infile.FullName)
                    : new string[0];
        }

        public void WriteLine(string line)
        {
            File.AppendAllLines(_outfile.FullName, new[] { line });
        }

        public void Reset()
        {
            if (_outfile.Exists) {
                File.Delete(_outfile.FullName);
            }
        }
    }
}
