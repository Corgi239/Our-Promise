using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer rend= GetComponent<MeshRenderer>();
        rend.sortingLayerName = "Tools";
    }
    
}
