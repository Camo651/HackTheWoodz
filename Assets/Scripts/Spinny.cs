using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinny : MonoBehaviour
{
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(0, 0, Time.fixedDeltaTime * 20f);
        Debug.Log(name + " " + Time.fixedDeltaTime);
    }
}
