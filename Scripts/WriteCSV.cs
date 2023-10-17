using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class WriteCSV : MonoBehaviour
{
    public GameObject obj;
    public int frameDelay;

    private int frameCounter;
    private int fileCounter;    //creating a new file after certain number of frames
    private Grid grid;

    private int rows;
    private int columns;
    private string[] data;

    private void Start()
    {
        fileCounter = 0;
        frameCounter = 0;
        grid = obj.GetComponent<GridTest>().GetGrid();
        
        grid.GetDimensions(out rows, out columns);
        data = new string[rows * columns];
        
    }

    private void Update()
    {
        frameCounter++;
        if(frameCounter >= frameDelay)
        {
            for(int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = (i * columns) + j;
                    int value = grid.GetValue(i, j);

                    data[index] = (i.ToString() + ',' + j.ToString() + ',' + value.ToString());
                }
            }

            string dir = "D:/UCL Bartlett/Term 2- Digital Studio_Simulated Realities/Data/Unity/CSVData"; //Change directory here.
            string fileName = ("dataFile_" + fileCounter.ToString() + ".csv");  //Update file name here.
            fileCounter++;

            string filePath = Path.Combine(dir, fileName);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach(string entry in data)
                {
                    sw.WriteLine(entry);
                }
            }

            frameCounter = 0;
        }
    }
}
