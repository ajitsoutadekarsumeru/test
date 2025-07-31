using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// An interface for generating diffs between two objects.
    /// </summary>
    public interface IDiffGenerator
    {
        /// <summary>
        /// Compares the old and new objects and returns a minified JSON Patch document.
        /// </summary>
        /// <param name="oldObj">The original object state.</param>
        /// <param name="newObj">The new object state.</param>
        /// <returns>A minified JSON string representing the JSON Patch operations.</returns>
        string GenerateDiff(object oldObj, object newObj);
    }
}
