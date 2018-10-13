using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Various constant values used internally
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// A key for the per-request nonce
        /// </summary>
        internal const string DefaultNonceKey = "NETESCAPADES_NONCE";

        /// <summary>
        /// The default number of bytes to use for generating a nonce
        /// </summary>
        internal const int DefaultBytesInNonce = 32;
    }
}