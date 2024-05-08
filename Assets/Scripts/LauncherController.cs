using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    public Collider bola;
    public KeyCode input;
    public float maxTimeHold;
    public float maxforce;

    private Renderer renderer;
    private bool isHold;

    private void Start()
    {
        isHold = false;
    }

    // Start is called before the first frame update
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider == bola)
        {
            ReadInput(bola);
        }
    }

    private void ReadInput(Collider collider)
    {
        if (Input.GetKey(input) && !isHold)
        {
            StartCoroutine(StartHold(collider));
        }
    }
    private IEnumerator StartHold(Collider collider)
    {
        isHold = true;

        float force = 0.0f;
        float timehold = 0.0f;
        
        while (Input.GetKey(input))
        {
            force = Mathf.Lerp(0,maxforce, timehold/maxTimeHold);

            yield return new WaitForEndOfFrame();
            timehold += Time.deltaTime;
        }

        collider.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
        isHold = false;
    }
}
