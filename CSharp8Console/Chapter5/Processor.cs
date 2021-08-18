using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8Console.Chapter5
{
    public class Processor
    {
        public TextAndNumber GetTheData()
        {
            return new TextAndNumber
            {
                Text = "What's the meaning of life?",
                Number = 42
            };
        }
    }
}
