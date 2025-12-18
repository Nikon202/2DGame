using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
[SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform startPoint;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet = Instantiate(bulletPrefab, startPoint.position, transform.rotation);
                bullet.transform.right = startPoint.right;
            }
        }
}
