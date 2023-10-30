using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFactory : MonoBehaviour
{
    [SerializeField]
    private Box box;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(box.hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
