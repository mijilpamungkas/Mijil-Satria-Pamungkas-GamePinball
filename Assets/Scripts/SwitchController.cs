using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    private enum SwitchState
    {
        Off,
        On,
        Blink
    }

    public Collider bola;
    public Material offMaterial;
    public Material onMaterial;
    public AudioManager audioManager;
    public VFXManager vfxManager;
    public float score;

     public ScoreManager scoreManager;

    private SwitchState state;
    private Renderer renderer;

    private void Start ()
    {
        renderer = GetComponent<Renderer>();

        Set(false);

        StartCoroutine(BlinkTimerStart(5));
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other == bola)
        {
            Toggle();
        }

        if (other == bola)
        {
            //playsfx
            audioManager.PlaySFX(other.transform.position);

            //playvfx
            vfxManager.PlayVFX(other.transform.position);
        }
        
    }

    private void Set(bool active)
    {
        if (active == true)
        {
            state = SwitchState.On;
            renderer.material = onMaterial;
            StopAllCoroutines();
        }
        else 
        {
            state = SwitchState.Off;
            renderer.material = offMaterial;
            StartCoroutine(BlinkTimerStart(5));
        }
    }

    private void Toggle()
        {
            if(state == SwitchState.On)
            {
                Set(false);
            }
            else 
            {
                Set(true);
            }
            //score add
            scoreManager.AddScore(score);
        }

    private IEnumerator Blink(int times)
    {
        state = SwitchState.Blink;

        for (int i = 0; i < times;  i++)
        {
            renderer.material = onMaterial;
            yield return new WaitForSeconds(0.5f);
            renderer.material = offMaterial;
            yield return new WaitForSeconds(0.5f);
        }

        state = SwitchState.Off;

        StartCoroutine(BlinkTimerStart(5));
    }

    private IEnumerator BlinkTimerStart (float time)
    {
        yield return new WaitForSeconds (time);
        StartCoroutine(Blink(2));
    }
}
