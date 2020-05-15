using System;
using System.Linq;

namespace PNGAnalyzerUI
{
    public enum RSAMethod
    {
        MicrosoftRSA,
        MyRSA
    }

    public static class RSAMethodExtensions
    {
        public static string ToMethodString(this RSAMethod rsaMethod)
        {
            switch (rsaMethod)
            {
                case RSAMethod.MicrosoftRSA:
                    return "Microsoft RSA";
                case RSAMethod.MyRSA:
                    return "My RSA";
                default:
                    throw new ArgumentOutOfRangeException(nameof(rsaMethod), rsaMethod, null);
            }
        }
    }
}