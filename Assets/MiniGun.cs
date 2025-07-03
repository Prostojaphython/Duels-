using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        //Задержка между выстрелами(можно указать собственную)
        cooldown = 0.1f;
        auto = true;
        ammoCurrent = 300;
        ammoMax = 300;
        ammoBackPack = 600;
        //Стрельба автоматическая, значит при зажатой клавише мыши оружие будет стрелять непрерывно учитывая задержку
        
    }
      protected override void OnShoot()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 drift = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), Random.Range(-15, 15));       
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition + drift);
        RaycastHit hit;
        //Продолжаем писать после строки RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            //if(hit.collider.CompareTag("enemy"))
            {
                //Число 10 можешь поменять на своё. Это урон, который наносит одна пуля
                //hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(3);
            }
            //Destroy(gameBullet, 1);
        }

    }

}
