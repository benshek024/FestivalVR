using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStart : MonoBehaviour
{
    // The purpose of this script is to activate corresponding UI Panels according to which area did player entered.
    // And deactivate it upon exit the area.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShootingGallery"))
        {
            UIManager.instance.ShowUIPanel(UIManager.instance.sgPanel);
            SGManager.instance.isEntered = true;
        }

        if (other.CompareTag("Wanage"))
        {
            UIManager.instance.ShowUIPanel(UIManager.instance.wPanel);
            WManager.instance.isEntered = true;
        }

        if (other.CompareTag("ElectricRush"))
        {
            UIManager.instance.ShowUIPanel(UIManager.instance.erPanel);
            ERManager.instance.isEntered = true;
        }

        if (other.CompareTag("Fireworks"))
        {
            UIManager.instance.ShowUIPanel(UIManager.instance.fwPanel);
        }

        if (other.CompareTag("WhackAMole"))
        {
            KManager.instance.isEntered = true;
            UIManager.instance.ShowUIPanel(UIManager.instance.kPanel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShootingGallery"))
        {
            UIManager.instance.HideUIPanel(UIManager.instance.sgPanel);
            SGManager.instance.isEntered = false;
        }

        if (other.CompareTag("Wanage"))
        {
            UIManager.instance.HideUIPanel(UIManager.instance.wPanel);
            WManager.instance.isEntered = false;
        }

        if (other.CompareTag("ElectricRush"))
        {
            UIManager.instance.HideUIPanel(UIManager.instance.erPanel);
            ERManager.instance.isEntered = false;
        }

        if (other.CompareTag("Fireworks"))
        {
            UIManager.instance.HideUIPanel(UIManager.instance.fwPanel);
        }

        if (other.CompareTag("WhackAMole"))
        {
            UIManager.instance.HideUIPanel(UIManager.instance.kPanel);
            KManager.instance.isEntered = false;
        }
    }
}
