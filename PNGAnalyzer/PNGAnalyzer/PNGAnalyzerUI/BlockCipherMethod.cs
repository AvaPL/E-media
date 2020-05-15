using System;
using System.Linq;

namespace PNGAnalyzerUI
{
    public enum BlockCipherMethod
    {
        ElectronicCodebook,
        CipherBlockChaining,
        PropagatingCipherBlockChaining,
        CipherFeedback,
        OutputFeedback,
        Counter
    }
    
    public static class BlockCipherMethodExtensions
    {
        public static string ToMethodString(this BlockCipherMethod blockCipherMethod)
        {
            switch (blockCipherMethod)
            {
                case BlockCipherMethod.ElectronicCodebook:
                    return "Electronic codebook";
                case BlockCipherMethod.CipherBlockChaining:
                    return "Cipher block chaining";
                case BlockCipherMethod.PropagatingCipherBlockChaining:
                    return "Propagating cipher block chaining";
                case BlockCipherMethod.CipherFeedback:
                    return "Cipher feedback";
                case BlockCipherMethod.OutputFeedback:
                    return "Output feedback";
                case BlockCipherMethod.Counter:
                    return "Counter";
                default:
                    throw new ArgumentOutOfRangeException(nameof(blockCipherMethod), blockCipherMethod, null);
            }
        }
    }
}