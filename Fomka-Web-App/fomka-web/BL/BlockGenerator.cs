using System.Collections.Generic;
using System.Linq;
using fomka_web.Models;

namespace fomka_web.BL
{
    public class BlockGenerator
    {
        private const ushort BlockSize = 5;

        public static List<BlockOfCode> Code2Blocks(string code)
        {
            var lines = code.Split('\n').Select(s => s + '\n');

            var counter = BlockSize;
            var blocks = lines.GroupBy(_ => counter++ / BlockSize);

            var SequenceOfBlocks = new List<BlockOfCode>();

            foreach (var block in blocks)
            {
                SequenceOfBlocks.Add(new BlockOfCode(){BlockID = block.Key, Code = string.Join("", block)});
            }

            return SequenceOfBlocks;
        }
    }
}