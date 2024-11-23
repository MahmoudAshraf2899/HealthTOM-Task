using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haflty.SharedKernel.Helpers
{
    public class CultureCollection
    {
        private string[] arr = new string[10];
        int nextIndex = 0;

        public string this[int i] => arr[i];

        public void Add(string value)
        {
            if (nextIndex >= arr.Length)
                throw new IndexOutOfRangeException($"The collection can hold only {arr.Length} elements.");
            arr[nextIndex++] = value;
        }
    }
}
