using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{   

    void Start()
    {
        //задержки между выстрелами нет
        
        cooldown = 0;
        auto = false;
        ammoCurrent = 80;
        ammoMax= 80;
        ammoBackPack = 150;
        //Стрельба не автоматическая. Нужно каждый раз нажимать на кнопку мыши для выстрела
        
    }

    protected override void OnShoot()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);        
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition);
        RaycastHit hit;
        //Продолжаем писать после строки RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            //if(hit.collider.CompareTag("enemy"))
            {
                //Число 10 можешь поменять на своё. Это урон, который наносит одна пуля
                //hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(10);
            }
            //Destroy(gameBullet, 1);
        }

    }
}
