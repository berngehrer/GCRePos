using System;

namespace GCRePos
{
    class OffsetController
    {
        double _min = 1000.0d;
        double _max = 0.0d;

        public OffsetController(double offset)
        {
            Offset = offset;
        }

        public double Offset { get; }

        public double Size => _min <= _max ? _max - _min : 0.0d;


        public void Approve(double value)
        {
            if (value > 0) {
                _min = Math.Min(_min, value);
                _max = Math.Max(_max, value);
            }
        }

        public double Adjust(double value)
        {
            if (value > 0) {
                return Math.Max(Offset + (value - _min), 0.0d);
            }
            return 0.0d;
        }
        
        public string PrintInfo(string key)
        {
            return string.Format(
                new DoubleFormatter(), 
                "{0} =>  Adjust: {1}\tSize: {2}", 
                key,            _min,       Size
            );
        }
    }
}
