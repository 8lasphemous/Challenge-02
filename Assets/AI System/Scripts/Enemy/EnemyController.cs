using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jcsilva.AISystem;

namespace jcsilva.CharacterController {

    public class EnemyController : MonoBehaviour {

        Animator animator;

        

        [Header("References")]
        [SerializeField] AIStateMachine stateMachine;

        [Header("Enemy Settings")]
        [SerializeField] float fireRate = 1f;
        [SerializeField] float elapsedTime;

        public Transform bulletSpawnPoint;
        public GameObject bulletPrefab;
        public float bulletSpeed = 10;

        private bool canShoot;

        private void Awake() {

            animator = this.GetComponent<Animator>();
            
            if (stateMachine == null) {
                stateMachine = GetComponent<AIStateMachine>();
            }
        }

        private void OnEnable() {
            stateMachine.EventAIEnableAttack += Shoot;
            stateMachine.EventAIDisableAttack += CantShoot;
        }

        private void OnDisable() {
            stateMachine.EventAIEnableAttack -= Shoot;
            stateMachine.EventAIDisableAttack -= CantShoot;
        }

        // Update is called once per fram
        void Update() {
            if (canShoot) {
                
                if (elapsedTime > fireRate) {
                    IsShooting();
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                    elapsedTime = 0f;
                    
                } else {
                    elapsedTime += Time.deltaTime;
                    animator.SetInteger("stateChange", 1);
                }
            } else if (!canShoot && elapsedTime > 0f) {
                if(elapsedTime > fireRate) {
                    elapsedTime = 0f;
                } else {
                    elapsedTime += Time.deltaTime;
                }
            }
            
        }

        private void Shoot() {
            canShoot = true;
        }

        private void CantShoot() {
            canShoot = false;
        }

        private void IsShooting() {
            Debug.Log("I'm Shooting");
        }
    }
}
