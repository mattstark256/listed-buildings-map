using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour {

    [SerializeField]
    private Color colorA = Color.white;
    [SerializeField]
    private Color colorB = Color.white;
    [SerializeField]
    private Color colorC = Color.white;

    public string Title { get; set; }
    public string Date { get; set; }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetListing(string listing)
    {
        Color markerColor = Color.black;
        if (listing == "A")
        {
            markerColor = colorA;
        }
        if (listing == "B")
        {
            markerColor = colorB;
        }
        if (listing == "C")
        {
            markerColor = colorC;
        }

        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material.color = markerColor;
        }
    }
}
