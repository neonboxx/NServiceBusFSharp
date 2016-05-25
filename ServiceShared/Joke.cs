using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceShared
{
    public class JokeResponse
    {
        public string Type { get; set; }
        public JokeModel Value { get; set; }
    }
    public class JokeModel
    {
        public int Id { get; set; }
        public string Joke { get; set; }
        public string[] Categories{ get; set; }
    }
}
