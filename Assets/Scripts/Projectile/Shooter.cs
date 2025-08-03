using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform target;
    [SerializeField] Transform attackPoint;

    [SerializeField] private float shootTime;
    [SerializeField]private float shootRate;
    [SerializeField] private float shootSpeed;

    private void Update()
    {
        shootTime -= Time.deltaTime;
        if(shootTime < 0)
        {
            
        }
    }
    //public void Fire()
    //{
    //    shootTime = shootRate;
    //    GameObject arrowGO = Instantiate(arrowPrefab, attackPoint.transform.position, Quaternion.identity);
    //    Arrow arrow = arrowGO.GetComponent<Arrow>();
    //    arrow.InitializeArrow(target, shootSpeed);
    //}
}
