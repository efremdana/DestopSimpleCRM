using System.Collections.Generic;

namespace Clients
{
    public class MyList<T> : List<T>
    {
        public T this[T index]
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.Equals(index))
                    {
                        return item;
                    }
                }
                throw new KeyNotFoundException();
            }
            set { this[index] = value; }
        }
        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
