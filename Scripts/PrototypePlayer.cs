using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypePlayer : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject hoopPrefab;
    [SerializeField] Transform firePos;

    [SerializeField] float mouseSens = 150f;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Look();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ShootHoop();
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.transform.position, firePos.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(firePos.up * 50f, ForceMode.Impulse);

        Destroy(bullet, 5f);
    }

    void ShootHoop()
    {
        GameObject hoop = Instantiate(hoopPrefab, firePos.transform.position, firePos.transform.rotation) as GameObject;
        hoop.GetComponent<Rigidbody>().AddForce(firePos.up * 35f, ForceMode.Impulse);
    }
}
