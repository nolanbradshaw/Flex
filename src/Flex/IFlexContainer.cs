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
    }
}
