using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red
{
    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Input (x, y): ");

            // Recieving the matrix dimentions
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
                    matrix.Add(new Cell { Row = row, Col = column, Value = int.Parse(inputRow[column].ToString())});
                }
            }

            // Recieving the coordinates of the observed cell and how many generations will there be
            var additionalArguments = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            // The coordinates of the cell to be checked
            var cellRow = additionalArguments[0];
            var cellColumn = additionalArguments[1];
            Cell cell = matrix.Find(c => c.Row == cellRow && c.Col == cellColumn);

            // The cell collor
            //Cell cell = new Cell();
            //cell = matrix.Find(c => cell.Row == cellRow && cell.Col == cellColumn);

            int greenCounter = 0;
            // Check if cell is red or green in Generation Zero matrix
            greenCounter = TargetCellStatusCheck(cell, greenCounter);            
            
            // The count of the new generations
            var lastGeneration = additionalArguments[2];

            for (int generation = 0; generation < lastGeneration; generation++)
            {
                //Starting calculating the new generation
                GenerateNewMatrix(matrix);
                Console.WriteLine(cell.Value);                
                greenCounter = TargetCellStatusCheck(cell, greenCounter);
            }

            //Count of chenges of the cell
            Console.WriteLine(greenCounter);
        }

        private static void GenerateNewMatrix(List<Cell> matrix)
        {
            //Temporary new matrix to hold the changes
            var newMatrix = new List<Cell>();

            for (int i = 0; i < matrix.Count; i++)
            {
                //Checking the conditions for each cell
                var currentCell = matrix[i];                
                var countOfGreenCells=checkForSurroundingGreenCells(currentCell.Row, currentCell.Col, matrix);

                //If the cell is green and surrounded by 2 or 3 or 6 green cells change to red
                if (currentCell.Value == 1 && countOfGreenCells == 2 || countOfGreenCells == 3 || countOfGreenCells ==6)
                {
                    currentCell.Value = 0;
                }

                //If the cell is red and surrounded by 3 or 6 green cells change to green
                if (currentCell.Value == 0 &&  countOfGreenCells == 3 || countOfGreenCells == 6)
                {
                    currentCell.Value = 1;
                }

                //Add the cell to the new matrix
                newMatrix.Add(currentCell);
            }

            matrix.Clear();
            matrix.AddRange(newMatrix);
        }

        private static int checkForSurroundingGreenCells(int row, int col, List<Cell> oldMatrix)
        {
            var countOfSurroundingGreenCells = 0;

            // Check if there is cell row-1, col-1 and green.
            if (oldMatrix.Any(c=> c.Row == row - 1 && c.Col == col - 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row-1, col and green.
            if (oldMatrix.Any(c => c.Row == row - 1 && c.Col == col && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row-1, col+1 and green.
            if (oldMatrix.Any(c => c.Row == row - 1 && c.Col == col + 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row, col-1 and green.
            if (oldMatrix.Any(c => c.Row == row && c.Col == col - 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }            
            // Check if there is cell row, col+1 and green.
            if (oldMatrix.Any(c => c.Row == row && c.Col == col + 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row+1, col-1 and green.
            if (oldMatrix.Any(c => c.Row == row + 1 && c.Col == col - 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row+1, col and green.
            if (oldMatrix.Any(c => c.Row == row + 1 && c.Col == col && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }
            // Check if there is cell row+1, col+1 and green.
            if (oldMatrix.Any(c => c.Row == row + 1 && c.Col == col + 1 && c.Value == 1))
            {
                countOfSurroundingGreenCells++;
            }

            return countOfSurroundingGreenCells;          
        }

        private static int TargetCellStatusCheck(Cell cell, int greenCounter)
        {
            if (cell.Value == 1)
            {
                greenCounter++;
            }

            return greenCounter;
        }
    }
}
