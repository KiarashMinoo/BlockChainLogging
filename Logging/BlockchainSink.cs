using Serilog.Core;
using Serilog.Events;

namespace BlockChainLogging.Logging;

public class BlockchainSink : ILogEventSink, IBlockchainValidator
{
    private readonly List<BlockchainLogEvent> _chain = [];
    private string _lastHash = "GENESIS";

    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage();
        var block = new BlockchainLogEvent(message, _lastHash);
        _lastHash = block.Hash;
        _chain.Add(block);

        // Write to file, console, or any destination
        Console.WriteLine($"[{block.Timestamp:O}] {message}");
        Console.WriteLine($"  ➤ Hash: {block.Hash}");
        Console.WriteLine($"  ↩ Prev: {block.PreviousHash}");
    }

    public bool IsChainValid()
    {
        for (int i = 1; i < _chain.Count; i++)
        {
            if (_chain[i].PreviousHash != _chain[i - 1].Hash)
                return false;
        }

        return true;
    }
}