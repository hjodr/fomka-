using System;
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

        public static List<BlockOfCode> OrderBlocks(List<BlockOfCode> blocks, string order)
        {
            if (order != null && order != "")
            {
                var ids = order.Split(',');
                var orderedBlocks = new List<BlockOfCode>();
                foreach (var blockId in ids)
                {
                    var id = Convert.ToInt32(blockId);
                    orderedBlocks.Add(blocks.Find(b => b.BlockID == id));
                }

                return orderedBlocks;
            }
            else
            {
                return new List<BlockOfCode>();
            }
        }

        public static List<BlockOfCode> RandomizeBlocks(List<BlockOfCode> blocks)
        {
            return blocks.OrderBy(a => Guid.NewGuid()).ToList();
        }

        //private static Random rng = new Random();

        //public static void Shuffle<T>(this IList<T> list)
        //{
        //    int n = list.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = rng.Next(n + 1);
        //        T value = list[k];
        //        list[k] = list[n];
        //        list[n] = value;
        //    }
        //}

        public static string GetOrder(List<BlockOfCode> blocks)
        {
            var result = "";
            for (int i = 0; i < blocks.Count; i++)
            {
                if (i == 0)
                {
                    result = blocks[0].BlockID.ToString();
                }
                else
                {
                    result += "," + blocks[i].BlockID;
                }
            }

            return result;
        }

        public static List<BlockOfCode> SetSelected(List<BlockOfCode> blocks, List<BlockOfCode> selected)
        {
            foreach (var block in selected)
            {
                blocks.Find(b => b.BlockID == block.BlockID).selected = true;
            }

            return blocks;
        }
    



        public static float AnswerCorrectInPercent(List<BlockOfCode> answer, List<BlockOfCode> code)
        {
            ushort correct = 0;

            if (answer.Count != code.Count)
            return 0;

            for (int i = 0; i < answer.Count; i++)
                if (answer[i].BlockID == code[i].BlockID)
                    correct++;

            return (float) correct / answer.Count;
        }
    }
}