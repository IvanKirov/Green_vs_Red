using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red
{
    public class StartUp
    {
        public static void Main()
        {
            var matrix = CreateAndFillZeroMatrix();            

            // Receiving the coordinates of the observed cell and how many generations will there be
            var additionalArguments = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            // The coordinates of the cell to be checked
            var cellColumn = additionalArguments[0];
            var cellRow = additionalArguments[1];            
           
            Cell cell = matrix.Find(c => c.Row == cellRow && c.Col == cellColumn);
            TargetCell targetCell = new TargetCell
            {
                Row = cell.Row,
                Col = cell.Col,
                Value = cell.Value
            };

            // Check if cell is red or green in Generation Zero matrix
            TargetCellStatusCheck(targetCell);

            // The count of the new generations
            var lastGeneration = additionalArguments[2];

            for (int generation = 0; generation < lastGeneration; generation++)
            {
                //The new matrix generation
                GenerateNewMatrix(matrix);
                
                // Check if cell is red or green in the new matrix
                targetCell.Value = matrix.Find(c => c.Row == targetCell.Row && c.Col == targetCell.Col).Value;
                TargetCellStatusCheck(targetCell);
            }

            //Count of changes of the target cell
            Console.WriteLine(targetCell.Counter);
        }

        public static List<Cell> CreateAndFillZeroMatrix()
        {
            // Receiving the matrix dimensions
            var matrixSizeInput = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var width = matrixSizeInput[0];
            var height = matrixSizeInput[1];

            // Creating the 'Generation Zero' matrix
            var matrix = new List<Cell>();

            //Filing the matrix
            for (int row = 0; row < width; row++)
            {
                var inputRow = Console.ReadLine();

                for (int column = 0; column < height; column++)
                {
                    matrix.Add(new Cell { Row = row, Col = column, Value = int.Parse(inputRow[column].ToString()) });
                }
            }

            return matrix;
        }

        public static void GenerateNewMatrix(List<Cell> matrix)
        {
            //Temporary new matrix to hold the changes
            var newMatrix = new List<Cell>();
            
            foreach (Cell cell in matrix)
            {
                //Checking the conditions for each cell
                var countOfGreenCells = checkForSurroundingGreenCells(cell.Row, cell.Col, matrix);

                Cell newCell = new Cell() {Row = cell.Row, Col = cell.Col, Value = cell.Value };
                
                if (cell.Value == 1)
                {
                    //The green cell does not change if surrounded by 2,3 or 6 green cells
                    if (countOfGreenCells == 2 || countOfGreenCells == 3 || countOfGreenCells == 6)
                    {
                        
                    }
                    //Otherwise change to red
                    else
                    {
                        newCell.Value = 0;
                    }
                }
                
                if (cell.Value == 0)
                {
                    //If the cell is red and surrounded by 3 or 6 green cells change to green
                    if (countOfGreenCells == 3 || countOfGreenCells == 6)
                    {
                        newCell.Value = 1;
                    }
                }

                //Add the cell to the new matrix
                newMatrix.Add(newCell);
            }

            //Replacing the old matrix with the new one
            matrix.Clear();
            matrix.AddRange(newMatrix);
        }

        public static int checkForSurroundingGreenCells(int row, int col, List<Cell> matrix)
        {
            var countOfSurroundingGreenCells = 0;

            // Check if there is cell row-1, col-1 and green.
            if (matrix.Any(c => c.Row == row - 1 && c.Col == col - 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row-1, col and green.
            if (matrix.Any(c => c.Row == row - 1 && c.Col == col && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row-1, col+1 and green.
            if (matrix.Any(c => c.Row == row - 1 && c.Col == col + 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row, col-1 and green.
            if (matrix.Any(c => c.Row == row && c.Col == col - 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row, col+1 and green.
            if (matrix.Any(c => c.Row == row && c.Col == col + 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row+1, col-1 and green.
            if (matrix.Any(c => c.Row == row + 1 && c.Col == col - 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row+1, col and green.
            if (matrix.Any(c => c.Row == row + 1 && c.Col == col && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row+1, col+1 and green.
            if (matrix.Any(c => c.Row == row + 1 && c.Col == col + 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }

            return countOfSurroundingGreenCells;
        }

        public static void TargetCellStatusCheck(TargetCell cell)
        {
            if (cell.Value == 1)
            {
                cell.Counter++;
            }
        }
    }
}
