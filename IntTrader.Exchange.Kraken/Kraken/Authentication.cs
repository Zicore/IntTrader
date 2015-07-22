using System;
using System.Security.Cryptography;
using System.Text;

namespace IntTrader.API.Exchange.Kraken
{
    public class Authentication
    {
        public Authentication()
        {

        }

        //byte[] base64DecodedSecred = Convert.FromBase64String(_secret);

        //    var np = nonce + Convert.ToChar(0) + props;

        //    var pathBytes = Encoding.UTF8.GetBytes(path);
        //    var hash256Bytes = sha256_hash(np);
        //    var z = new byte[pathBytes.Count() + hash256Bytes.Count()];
        //    pathBytes.CopyTo(z, 0);
        //    hash256Bytes.CopyTo(z, pathBytes.Count());

        //    var signature = getHash(base64DecodedSecred, z);

        //Message signature using HMAC-SHA512 of (URI path + SHA256(nonce + POST data)) and base64 decoded secret API key

        public String CreateSignature(String uri, String nonce, String postData, String apiSecret)
        {
            var decodedKey = Convert.FromBase64String(apiSecret);
            using (var hmac = new HMACSHA512(decodedKey))
            {
                using (var sha256 = new SHA256Managed())
                {

                    var nonceBytes = Encoding.UTF8.GetBytes(nonce);
                    var postDataBytes = Encoding.UTF8.GetBytes(postData);
                    var uriBytes = Encoding.UTF8.GetBytes(uri);


                    var sha256Bytes = new byte[nonceBytes.Length + postDataBytes.Length];
                    nonceBytes.CopyTo(sha256Bytes, 0);
                    postDataBytes.CopyTo(sha256Bytes, nonceBytes.Length);
                    sha256Bytes = sha256.ComputeHash(sha256Bytes);



                    var complementMessage = new byte[uriBytes.Length + sha256Bytes.Length];
                    uriBytes.CopyTo(complementMessage, 0);
                    sha256Bytes.CopyTo(complementMessage, uriBytes.Length);


                    var signature = hmac.ComputeHash(complementMessage);
                    return Convert.ToBase64String(signature);
                }
            }
        }
    }
}
