using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        //задержки между выстрелами нет
        cooldown = 0;
        //Стрельба не автоматическая. Нужно каждый раз нажимать на кнопку мыши для выстрела
        auto = false;
    }

    // Update is called once per frame
    protected override void OnShoot()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            Destroy(gameBullet, 1);
        }
    }
}
