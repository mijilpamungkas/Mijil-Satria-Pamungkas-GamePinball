using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float returnTime;
    public float followSpeed;
    public float length;
    public Transform target;
    private Vector3 defaultPosition;

    public bool hasTarget { get { return target != null; } }
    private void Start()
    {
        defaultPosition = transform.position;
        target = null;
    }

    private void Update()
    {
            // fungsi update kita ubah total menjadi fungsi untuk kamera mengikuti object
		    // secara terus menerus sampai target kamera dikosongkam kembali

        if (hasTarget)

        {   Vector3 targetToCameraDirection = transform.rotation * -Vector3.forward;
            Vector3 targetPosition = target.position + (targetToCameraDirection * length);

            // disini kamera dipindahkan menggunakan lerp biasa yang terjadi tiap update
			// Lerp yang dijalankan disini secara otomatis menjadi smoothing karena menggunakan
			// transform.position secara langsung
         transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

     public void FollowTarget(Transform targetTransform, float targetLength)
    {
        // saat mulai follow, pastikan coroutine gerakan kamera ke posisi default berhenti

    StopAllCoroutines();

		// disini kita hanya set saja target dan length nya, nanti fungsi update akan otomatis
		// bekerja karena hasTarget akan menjadi true

    target = targetTransform;
    length = targetLength;
    }

    public void GoBackToDefault()
    {
        // sama seperti FollowTarget, pastikan coroutine berhenti
    StopAllCoroutines();
		
		// kita set targetnya ke null agar hasTarget menjadi false
    target = null;

        //gerakan ke posisi default dalam waktu return time

    StartCoroutine(MovePosition(defaultPosition, returnTime));
    }

    private IEnumerator MovePosition(Vector3 target, float time)
    {
        float timer = 0;
        Vector3 start = transform.position;
        while (timer < time)
        {
            //pindahkan posisi camera secara bertahap
            transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0.0f, 1.0f, timer/time));

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = target;
    }
}
