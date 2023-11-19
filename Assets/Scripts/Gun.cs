using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    
    AudioSource m_shootingSound;


    void Start()
    {
        m_shootingSound = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_shootingSound.Play();
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}