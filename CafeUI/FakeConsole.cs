using System;
using System.Collections.Generic;

namespace CafeUI
{
    public class FakeConsole : IConsole
    {
        public Queue<string> UserInput;
        public string Output;

        public FakeConsole(IEnumerable<string> input)
        {
            UserInput = new Queue<string>(input);
            Output = "";
        }
        public void Clear()
        {
            Output += "Called Clear Method\n";
        }

        public ConsoleKeyInfo ReadKey()
        {
            return new ConsoleKeyInfo();
        }

        public string ReadLine()
        {
            return UserInput.Dequeue();
        }

        public void WriteLine(string s)
        {
            Output += s + "\n";
        }

        public void WriteLine(object o)
        {
            Output += o + "\n";
        }
    }
}
