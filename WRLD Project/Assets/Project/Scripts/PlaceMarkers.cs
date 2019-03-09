using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text.RegularExpressions;

public class PlaceMarkers : MonoBehaviour
{

    [SerializeField]
    private Vector2 parentEastingNorthing;
    [SerializeField]
    private float defaultElevation = 50;

    [SerializeField]
    private Marker markerPrefab;

    [SerializeField]
    private ReadCSV readCSV;

    // Use this for initialization
    void Start()
    {
        for (int i = 1; i < readCSV.RowCount-1; i++)
        {
            List<Vector2> coordsList = GetCoordinates(i, 8);
            Vector2 averageCoords = GetAverageCoordinates(coordsList);

            Marker newMarker = Instantiate(markerPrefab, transform);

            Vector3 markerPosition = new Vector3(averageCoords.x - parentEastingNorthing.x, defaultElevation, averageCoords.y - parentEastingNorthing.y);

            // I unsuccessfully attempted to find the ground height (but have since disabled terrain collisions)
            //RaycastHit hit;
            //if (Physics.Raycast(
            //    markerPosition + transform.TransformVector(Vector3.up)*1000,
            //    transform.TransformVector(Vector3.down) * 1000,
            //    out hit))
            //{
            //    markerPosition = transform.InverseTransformVector(hit.point);
            //}

            newMarker.transform.localPosition = markerPosition;

            newMarker.SetListing(readCSV.GetCell(i, 4));
            newMarker.Title =readCSV.GetCell(i, 6);
            newMarker.Date = readCSV.GetCell(i, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaceMarker(Vector2 eastingNorthing, float elevation)
    {
    }


    Vector2 GetAverageCoordinates(List<Vector2> vectors)
    {
        Vector2 sumVector = Vector2.zero;
        foreach(Vector2 vector in vectors)
        {
            sumVector += vector;
        }
        sumVector /= vectors.Count;
        return sumVector;
    }


    // Each listed building has a bunch of coordinates that make up its outline
    List<Vector2> GetCoordinates(int row, int column)
    {
        List<Vector2> vectors = new List<Vector2>();

        string cell = readCSV.GetCell(row, column);
        bool xValueFound = false;
        
        string[] numbers = Regex.Split(cell, @"[^0-9\.]+"); // I guess this covers 0 to 9 and .
        foreach (string value in numbers)
        {
            if (!string.IsNullOrEmpty(value))
            {
                float f = float.Parse(value);

                if (xValueFound)
                {
                    int i = vectors.Count - 1;
                    vectors[i] = new Vector2(vectors[i].x, f);
                    xValueFound = false;
                }
                else
                {
                    vectors.Add(new Vector2(f, 0));
                    xValueFound = true;
                }
            }
        }

        return vectors;
    }
}
