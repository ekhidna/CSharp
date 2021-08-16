using CSharp8Console.Interfaces;

namespace CSharp8Console
{
    public class Operations : IOperations
    {
        private readonly IAdding _adding;

        public Operations(IAdding adding)
        {
            _adding = adding;
        }

        public void Sum()
        {
            _adding.Add(7, 2);
        }
    }
}
