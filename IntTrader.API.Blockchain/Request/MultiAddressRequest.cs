using System;
using System.Collections.Generic;
using System.Text;

namespace IntTrader.API.Blockchain.Request
{
    public class MultiAddressRequest : BlockchainRequestBase
    {
        public MultiAddressRequest(List<String> addresses)
        {
            RequestUri = "multiaddr?active=" + PrepareAddresses(addresses);
        }

        private String PrepareAddresses(IList<string> addresses)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < addresses.Count; i++)
            {
                sb.Append(addresses[i]);
                if (i < addresses.Count - 1)
                {
                    sb.Append("|");
                }
            }
            return sb.ToString();
        }
    }
}
