using Microsoft.Extensions.VectorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryVectorStorePOC.DataModel
{
    internal class FinanceInfo
    {
        [VectorStoreKey]
        public ulong Key { get; set; }

        [VectorStoreData]
        public string Text { get; set; } = string.Empty;

        // Note that the vector property is typed as a string, and
        // its value is derived from the Text property. The string
        // value will however be converted to a vector on upsert and
        // stored in the database as a vector.
        [VectorStoreVector(384)]
        public string Embedding => this.Text;
    }
}
