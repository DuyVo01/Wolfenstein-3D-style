using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleProperties : MonoBehaviour
{
    public RectTransform reticleSize;

    public static Vector3 reticlePoint;


    private void Awake()
    {
        reticleSize = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
