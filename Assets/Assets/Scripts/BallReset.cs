using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallReset : MonoBehaviour {

    public int _ball = 0;
    bool doUpdate = false;
    

    private void LateUpdate()
    {
        if (doUpdate)
        {
            _ball += 1;
            _ball = _ball % 3;
            StartCoroutine(DelayUpdate());
        }

        if (transform.position.y < -4)
            ResetFrame();
        doUpdate = false;
    }

    public IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SendMessage("UpdateScore", _ball, SendMessageOptions.RequireReceiver);
        yield return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ThrowBall>() != null)
        {
            doUpdate = true;
            other.gameObject.SendMessage("Reset", _ball, SendMessageOptions.DontRequireReceiver);
        }
    }

    void ResetFrame()
    {
        foreach (var v in GameObject.FindGameObjectsWithTag("Pin"))
        {
            v.SendMessage("ResetPin", (_ball), SendMessageOptions.DontRequireReceiver);
        }
        _ball = 0;
    }
}
