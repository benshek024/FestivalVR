using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class GunController : MonoBehaviour
{
    [Header("Booleans")]
    public bool isGrabbable = true;
    public bool isShootable = false;

    [Header("SteamVR Input")]
    public SteamVR_Action_Boolean fire = null;
    private SteamVR_Behaviour_Pose pose = null;
    //private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
    public Interactable interactable;

    [Header("Prefab Reference")]
    public GameObject bulletPrefab;

    [Header("Location Reference")]
    [SerializeField] private Animator anim;
    [SerializeField] private Transform firePos;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform gunOffset;

    [Header("Gun Settings")]
    [Tooltip("Specify time to destroy the bullet object")]
    [SerializeField] private float destroyTime = 2f;
    [Tooltip("Specify the velocity of the bullet object")]
    [SerializeField] private float bulletVelocity = 35f;
    [Tooltip("Fire rate of the gun")]
    [SerializeField] private float fireRate;
    private float timeStamp;
    public Rigidbody rb;

    private void Awake()
    {
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        interactable = GetComponentInParent<Interactable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Once the gun is attached to a hand, set isShootable to true for shooting.
        if (interactable.attachedToHand)
        {
            isShootable = true;
        }
        else
        {
            isShootable = false;
        }

        /*
        if (interactable != null && interactable.attachedToHand != null)
        {
            if (interactable.attachedToHand.handType == SteamVR_Input_Sources.RightHand)
            {
                float smooth = 0.5f;
                Quaternion rot = Quaternion.Euler(0f, 0f, -90f);

                gunOffset.rotation = Quaternion.Slerp(gunOffset.rotation, rot, Time.deltaTime * smooth);
            }

            if (interactable.attachedToHand.handType == SteamVR_Input_Sources.LeftHand)
            {
                float smooth = 0.5f;
                Quaternion rot = Quaternion.Euler(0f, 0f, 90f);

                gunOffset.rotation = Quaternion.Slerp(gunOffset.rotation, rot, Time.deltaTime * smooth);
            }
        }
        */

        if (isShootable)
        {
            if (Input.GetMouseButtonDown(1) && Time.time >= timeStamp)
            {
                Fire();
            }

            // Makes the gun can only be fired from the hand that is holding the gun.
            if (interactable.attachedToHand.handType == SteamVR_Input_Sources.RightHand)
            {
                if (SteamVR_Actions.default_Fire.GetStateDown(SteamVR_Input_Sources.RightHand) && Time.time >= timeStamp)
                {
                    Fire();
                }
            }

            if (interactable.attachedToHand.handType == SteamVR_Input_Sources.LeftHand)
            {
                if (SteamVR_Actions.default_Fire.GetStateDown(SteamVR_Input_Sources.LeftHand) && Time.time >= timeStamp)
                {
                    Fire();
                }
            }
        }

    }

    private void Fire()
    {
        anim.SetTrigger("Fire");
        timeStamp = Time.time + fireRate;
    }

    // Shoot a bullet
    void Shoot()
    {
        // return if there is no bullet prefab assigned.
        if (!bulletPrefab) { return; }

        // Instantiate a bullet
        GameObject bullet;
        audioSource.Play();
        bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePos.forward * bulletVelocity, ForceMode.Impulse);
        Destroy(bullet, destroyTime);
    }
}
