using System.Collections.Generic;

namespace Flex
{
    public class FlexContainer : IFlexContainer
    {
        internal Dictionary<string, string> Data { get; }
        public FlexContainer(Dictionary<string, string> data)
        {
            Data = data;
        }

        public FlexContainer()
        {
            Data = new Dictionary<string, string>();
        }

        public string Get(string key)
        {
            return Data[key];
        }

        public bool TryGetValue(string key, out string value)
        {
            if (Data.TryGetValue(key, out value))
            {
                return true;
            }

            return false;
        }

        public string this[string key]
        {
            get
            {
                return this.Get(key);
            }
        }
    }

    public class FlexContainer<T> : IFlexContainer<T>
        where T : class
    {
        public T Data { get; }
        public FlexContainer()
        {
        }

        public FlexContainer(T data)
        {
            Data = data;
        }
    }
}
