using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class FireworkController : MonoBehaviour
{
    public AudioSource fireworksLaunchAudioSource;
    public AudioSource fireworksExplodeAudioSource;

    public VisualEffect fireworkVFX1;
    public VisualEffect fireworkVFX2;
    public VisualEffect fireworkVFX3;

    public void ShowFireworks()
    {
        StartCoroutine(Fireworks());
        GameManager.instance.fwDetection.SetActive(false);
        UIManager.instance.HideMainPanel(UIManager.instance.fwPanel);
        UIManager.instance.audioSource.pitch = Random.Range(0.7f, 1.2f);
        UIManager.instance.audioSource.Play();
    }

    IEnumerator Fireworks()
    {
        fireworkVFX1.SendEvent("PlayFireworks");
        fireworkVFX2.SendEvent("PlayFireworks");
        fireworkVFX3.SendEvent("PlayFireworks");
        yield return new WaitForSeconds(1f);
        fireworksLaunchAudioSource.Play();
        yield return new WaitForSeconds(4f);
        fireworksExplodeAudioSource.Play();
        yield return new WaitForSeconds(18f);
        fireworkVFX1.SendEvent("StopFireworks");
        fireworkVFX2.SendEvent("StopFireworks");
        fireworkVFX3.SendEvent("StopFireworks");
        yield return new WaitForSeconds(7.5f);
        fireworksLaunchAudioSource.Stop();
        fireworksExplodeAudioSource.Stop();
        yield return new WaitForSeconds(5f);
        GameManager.instance.fwDetection.SetActive(true);
    }
}
