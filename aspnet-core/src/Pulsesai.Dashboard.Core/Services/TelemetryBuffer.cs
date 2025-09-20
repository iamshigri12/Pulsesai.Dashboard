using Pulsesai.Dashboard.Pulses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Services
{
    public class TelemetryBuffer
    {
        private readonly SensorReading[] _buffer;
        private int _head;
        private int _count;
        private readonly object _lock = new();

        public TelemetryBuffer(int capacity = 100_000)
        {
            _buffer = new SensorReading[capacity];
        }

        public int Capacity => _buffer.Length;

        public void Add(SensorReading item)
        {
            lock (_lock)
            {
                _buffer[_head] = item;
                _head = (_head + 1) % _buffer.Length;
                if (_count < _buffer.Length) _count++;
            }
        }
        public void AddRange(IEnumerable<SensorReading> items)
        {
            if (items == null) return;
            lock (_lock)
            {
                foreach (var item in items)
                {
                    _buffer[_head] = item;
                    _head = (_head + 1) % _buffer.Length;
                    if (_count < _buffer.Length) _count++;
                }
            }
        }
        public SensorReading[] ToArray()
        {
            lock (_lock)
            {
                var outArr = new SensorReading[_count];
                var start = (_head - _count + _buffer.Length) % _buffer.Length;
                for (int i = 0; i < _count; i++)
                {
                    outArr[i] = _buffer[(start + i) % _buffer.Length];
                }
                return outArr;
            }
        }

        public int Count { get { lock (_lock) { return _count; } } }
    }
}
