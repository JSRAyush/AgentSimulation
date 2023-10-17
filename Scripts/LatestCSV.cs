using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

public class LatestCSV : MonoBehaviour
{
    public string directory;
    public string saveFolderLocation;
    public int frameDelay;

    private DirectoryInfo directoryInfo;
    private string fileName;
    private int frameCounter;
    private void Start()
    {
        directoryInfo = new DirectoryInfo(directory);
        fileName = "LatestFile_Name.txt";
        frameCounter = 0;
    }
    private void Update()
    {
        frameCounter++;

        if(frameCounter >= frameDelay)
        {
            frameCounter = frameDelay + 1;   //To avoid frameCounter from reaching a very high value.

            FileInfo latestFile = (from file in directoryInfo.GetFiles() orderby file.LastWriteTime descending select file).First();   //Get fileInfo of latest file in the directory

            string filePath = Path.Combine(saveFolderLocation, fileName);
            using (StreamWriter sw = new StreamWriter(filePath, false))    //using false to overwrite previous 'latestFileName'.
            {
                sw.WriteLine(latestFile.FullName);
            }
        }
        
    }
}
