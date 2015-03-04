using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System
{
    internal interface ISterlingTuple
    {
        // Methods
        int GetHashCode(IEqualityComparer comparer);
        string ToString(StringBuilder sb);

        // Properties
        int Size { get; }
    }

}
