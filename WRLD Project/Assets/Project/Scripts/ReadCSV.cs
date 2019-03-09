using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{

    [SerializeField]
    private TextAsset csvFile;
    
    private string[,] cells;

    public int RowCount
    {
        get { return cells.GetLength(0); } 
    }

    public string GetCell(int row, int column)
    {
        return cells[row, column];
    }
    
    void Awake()
    {
        // Populate cells with data from the CSV file
        string[] lines = csvFile.ToString().Split("\n"[0]); // [0] specifies the first character
        string[] firstLine = SplitIgnoringQuotes(lines[0], ',');
        cells = new string[lines.Length, firstLine.Length];
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            string[] line = SplitIgnoringQuotes(lines[i], ',');
            for (int j = 0; j < cells.GetLength(1) && j < line.GetLength(0); j++)
            {
                cells[i, j] = line[j];
            }
        }
    }

    // This is like C#'s String.Split except it ignores split characters inside quotation marks.
    string[] SplitIgnoringQuotes(string inputString, char splitChar)
    {
        List<string> stringsList = new List<string>();
        string currentString = "";
        bool isInQuote = false;
        for (int i = 0; i < inputString.Length; i++)
        {
            if (inputString[i] == splitChar && !isInQuote)
            {
                stringsList.Add(currentString);
                currentString = "";
            }
            else if (inputString[i] == '"')
            {
                isInQuote = !isInQuote;
            }
            else
            {
                currentString = currentString + inputString[i];
            }
        }
        stringsList.Add(currentString);
        string[] stringsArray = new string[stringsList.Count];
        for (int i = 0; i < stringsList.Count; i++)
        {
            stringsArray[i] = stringsList[i];
        }
        return stringsArray;
    }
}
