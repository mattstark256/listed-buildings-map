using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelection : MonoBehaviour {

    Camera cam;

    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    MarkerUI markerUiPrefab;
    [SerializeField]
    RectTransform markerUiParent;
    [SerializeField]
    Vector3 uiOffset;

    Marker selectedMarker = null;
    MarkerUI markerUI;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        CheckForClick();

        if (selectedMarker!=null && markerUI!=null)
        {
            markerUI.transform.position = cam.WorldToScreenPoint(selectedMarker.transform.position + selectedMarker.transform.TransformVector(uiOffset));
        }
    }

    void CheckForClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                Transform objectHit = hit.transform;

                // Do something with the object that was hit by the raycast.
                selectedMarker = objectHit.GetComponentInParent<Marker>();
                if (selectedMarker != null)
                {
                    if (markerUI == null) {
                        markerUI = Instantiate(markerUiPrefab);
                        markerUI.transform.SetParent(markerUiParent);
                    }

                    markerUI.DisplayName(selectedMarker.Title);
                    markerUI.DisplayDate(selectedMarker.Date);
                }
                else
                {
                    if (markerUI!= null) { Destroy(markerUI.gameObject); }
                }
            }
            else
            {
                if (markerUI != null) { Destroy(markerUI.gameObject); }
            }
        }
    }
}
