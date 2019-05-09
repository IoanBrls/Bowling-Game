using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPinPosition : MonoBehaviour {

    private Vector3 position;
    private Quaternion rotation;
    


    private void OnEnable()
    {
        position = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
    }

    public void ResetPin(object _ball)
    {
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }


}
