using StreamProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessing.Service
{
    public class StreamProcessService : IStreamProcessService
    {
        private StreamPos _pos;

        public void SetInput(string input)
        {
            _pos = new StreamPos(input);
        }

        // get score for each layer group
        public int GetScore()
        {
            var group = ParseGroup();
            return group.Score(0);
        }

        public Group ParseGroup()
        {
            if (NextUnignoredChar() != '{')
                throw new Exception("'{' misssing");

            var streams = ParseStreams();

            if (NextUnignoredChar() != '}')
                throw new Exception("'}' missing");

            return new Group(streams);
        }

        IStream[] ParseStreams()
        {
            var list = new List<IStream>();
            while (CurrentChar != '}')
            {
                list.Add(ParseStream());

                if (CurrentChar == '}')
                    break;

                if (NextUnignoredChar() != ',')
                    throw new Exception("',' or '}' expected");
            }

            return list.ToArray();

        }

        IStream ParseStream()
        {
            if (CurrentChar == '{')
                return ParseGroup();
            if (CurrentChar == '<')
                return ParseGarbage();

            return null;
        }

        Garbage ParseGarbage()
        {
            if (NextUnignoredChar() != '<')
                throw new Exception("'<' missing");

            var garbage = new List<char>();

            while (true)
            {
                var next = NextUnignoredChar();
                if (next == null)
                    throw new Exception("end of stream while parsing garbage");
                if (next == '>')
                    return new Garbage(new String(garbage.ToArray()));
                garbage.Add(next.Value);

            }
        }

        char? NextUnignoredChar()
        {
            var c = NextChar();
            if (c == '!')
            {
                NextChar();
                return NextUnignoredChar();
            }
            return c;
        }

        char? CurrentChar => _pos.Current;

        char? NextChar()
        {
            var c = _pos.Current;
            _pos = _pos.MoveRight();
            return c;
        }

    }

    class StreamPos
    {
        private readonly string _input;
        private readonly int _index;

        public StreamPos(string input, int index = 0)
        {
            _input = input;
            _index = index;
            if (_index < 0)
                _index = 0;
            if (_index >= input.Length)
                _index = input.Length;
        }

        public StreamPos MoveRight()
        {
            return new StreamPos(_input, _index + 1);
        }

        public bool EndOfStream => _index >= _input.Length;

        public char? Current => !EndOfStream ? _input[_index] : (char?)null;
    }
}
