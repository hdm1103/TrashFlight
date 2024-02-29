using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private GameObject[] weapons;
        private int weaponIndex = 0;

        [SerializeField]
        private Transform shootTransform;

        [SerializeField]
        private float shootInterval = 0.05f;
        private float lastShotTime = 0f;


        // Update is called once per frame
        void Update()
        {
                //키보드로 제어1
                //     float horizontalInput = Input.GetAxisRaw("Horizontal");
                //     float verticalInput = Input.GetAxisRaw("Vertical");
                //     Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
                //     transform.position += moveTo * moveSpeed * Time.deltaTime;

                //키보드로 제어2
                // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                // if (Input.GetKey(KeyCode.LeftArrow)){
                //     transform.position -= moveTo;
                // } else if (Input.GetKey(KeyCode.RightArrow)){
                //     transform.position += moveTo;
                // }

                //마우스 제어

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
                transform.position = new Vector3(toX, transform.position.y, transform.position.z);

                if (GameManager.instance.isGameOver == false){
                        Shoot();
                }
               

        }

        void Shoot(){
                // 10 - 0 > 0.05
                // lastShottime = 10;

                // 10.001 - 10 > 0.05 ?
                //false

                // 10.02 - 0 > 0.05
                // 10.03 - 0 > 0.05
                // 10.06 - 0 > 0.05
                // lastShottime = 10.006;

                if (Time.time - lastShotTime > shootInterval){
                        Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
                        lastShotTime = Time.time;
                }
        }

        private void OnTriggerEnter2D(Collider2D other) {
                if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
                        GameManager.instance.SetGameOver();
                        Destroy(gameObject);

                } 
                else if (other.gameObject.tag == "Coin" ){
                        GameManager.instance.IncreaseCoin();
                        Destroy(other.gameObject);                                
                        
                }
        }

        public void Upgrade(){
                weaponIndex += 1;
                if (weaponIndex >= weapons.Length){
                        weaponIndex = weapons.Length - 1;
                }
        }
}
