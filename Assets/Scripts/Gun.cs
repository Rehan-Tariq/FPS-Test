using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int maxAmmo = 30;
    private int currentAmmo;
    public float fireRate = 0.1f;
    public float damage = 10f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TMP_Text ammoText;
    public Transform firePoint;

    private float nextFireTime = 0f;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f))
        {
            Debug.Log("Hitting Something");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }
        else
        {
            Debug.Log("Hitting Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);

        }
    }
    void Shoot()
    {
        if (currentAmmo <= 0) return;

        muzzleFlash.Play();
        currentAmmo--;
        UpdateUI();

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Apply damage if enemy
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Spawn impact effect
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    void UpdateUI()
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
    }
}
