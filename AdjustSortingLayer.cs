using UnityEngine;
using System.Collections;

public class AdjustSortingLayer : MonoBehaviour {

    public string layer;
    public int orderInLayer;

	// Use this for initialization
	void Start () {
        renderer.sortingLayerName = layer;
        renderer.sortingOrder = orderInLayer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
