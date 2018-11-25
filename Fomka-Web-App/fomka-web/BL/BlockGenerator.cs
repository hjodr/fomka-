using System.Linq;
using fomka_web.Models;

namespace fomka_web.BL
{
    public class BlockGenerator
    {
        private const ushort BlockSize = 5;

        public static Task Code2Blocks(ushort id, string name, string code, string description)
        {
            var lines = code.Split('\n').Select(s => s + '\n');

            var counter = BlockSize;
            var blocks = lines.GroupBy(_ => counter++ / BlockSize);

            var task = new Task() { TaskID = id, Name = name, Description = description };

            foreach (var block in blocks)
            {
                task.SequenceOfBlocks.Add(new BlockOfCode(){BlockID = block.Key, Code = string.Join("", block)});
            }

            return task;
        }
    }
}