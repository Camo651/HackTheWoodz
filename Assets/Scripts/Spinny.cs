using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinny : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(0, 0, Time.fixedDeltaTime * 20f);
    }
}
