using UnityEngine;

public class LightSortingLayer : MonoBehaviour
{
    public string sortingLayerName;

    private void Start()
    {
        int sortingLayerID = SortingLayer.NameToID(sortingLayerName);
        GetComponent<Light>().cullingMask = 1 << sortingLayerID;
    }
}