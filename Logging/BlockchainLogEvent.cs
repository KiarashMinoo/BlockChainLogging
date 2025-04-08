using System.Security.Cryptography;
using System.Text;

namespace BlockChainLogging.Logging;

public class BlockchainLogEvent
{
    public DateTime Timestamp { get; private set; }
    public string Message { get; private set; }
    public string PreviousHash { get; private set; }
    public string Hash { get; set; }

    public BlockchainLogEvent(string message, string previousHash)
    {
        Timestamp = DateTime.UtcNow;
        Message = message;
        PreviousHash = previousHash;
        Hash = ComputeHash();
    }

    private string ComputeHash()
    {
        var input = $"{Timestamp:O}|{Message}|{PreviousHash}";
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
    }
}