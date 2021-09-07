using System;
namespace Picross_3._0
{
    public struct Dimensions
    {
        public int x;
        public int y;
    }

    public class PicrossGame
    {
        public Dimensions gameBoardDimensions;
        public int[][] rowReq;
        public int[][] colReq;

        public PicrossGame()
        {
        }

        public PicrossGame(string path)
        {
            this.parseBoard(path);
        }

        public void parseBoard(string path)
        {
            // row 0: dimensions
            // row 1: row requirments
            // row 2: column requirments
            string[] boardInfo = System.IO.File.ReadAllLines(path);

            // board dimensions
            string[] dimensionsString = boardInfo[0].Split(' ');
            this.gameBoardDimensions.x = Int32.Parse(dimensionsString[0]);
            this.gameBoardDimensions.y = Int32.Parse(dimensionsString[1]);

            // row requirments
            this.rowReq = this.parseRequirments(boardInfo[1], this.gameBoardDimensions.x);
            this.colReq = this.parseRequirments(boardInfo[2], this.gameBoardDimensions.y);
        }

        public char getBoardPixel(bool filled)
        {
            return filled ? '█' : ' ';
        }
        
        private int[][] parseRequirments(string reqs, int dimensionLength)
        {
            int[][] reqInts = new int[dimensionLength][];
            string[] reqStrings = reqs.Split(',');
            for (int i = 0; i < reqStrings.Length; i++)
            {
                string[] dimensionReqString = reqStrings[i].Split(' ');
                reqInts[i] = Array.ConvertAll(dimensionReqString, s => int.Parse(s));
            }

            return reqInts;
        }

        public override string ToString()
        {
            string board = "";
            int maxRowReqs = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.gameBoardDimensions.x) / 2.0));
            int maxColReqs = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.gameBoardDimensions.y) / 2.0));
            string leftPadding = new string(' ', maxRowReqs*2-1);

            board += $"{this.gameBoardDimensions.x}x{this.gameBoardDimensions.y}\n";

            for(int i = 0; i < maxColReqs; i++)
            {
                board += leftPadding;
                foreach(int[] req in this.colReq)
                {
                    int newIndex = i - (maxColReqs - req.Length);
                    if (newIndex >= 0)
                    {
                        board += req[newIndex];
                    }
                    else
                    {
                        board += ' ';
                    }
                }

                board += '\n';
            }

            foreach(int[] req in this.rowReq)
            {
                for(int i = 0; i < maxRowReqs; i++)
                {
                    int newIndex = i - (maxRowReqs - req.Length);
                    if (newIndex >= 0)
                    {
                        board += req[newIndex];
                        if(i+1 != maxRowReqs)
                        {
                            board += ' ';
                        }
                    }
                    else
                    {
                        board += "  ";
                    }

                }

                board += '\n';
            }

            return board;
        }
    }

}
