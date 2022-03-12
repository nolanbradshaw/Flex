using System.Collections.Generic;

namespace Flex
{
    public interface IFlexContainer<T> where T : class
    {
        public T Data { get; }
    }

    public interface IFlexContainer
    {
        public string Get(string key);

        bool TryGetValue(string key, out string value);

        public string this[string key] { get; }
    }
}
