using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCrawlingEnemy : EnemyAI
{
    [SerializeField] private Transform[] wayPoint;
    int nextWayPoint = 1;
    float distanceToPoint;
   
    protected override bool DetectionPlayer()
    {
        return true; ;
    }
    protected override void EnemyMovement() 
    {
        Move();
    }
    protected override float GetDirection(bool isChasing)
    {
        return 0;
    }
    private void Move()
    {
        distanceToPoint = Vector3.Distance(transform.position , wayPoint[nextWayPoint].position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoint[nextWayPoint].position, config.moveSpeed*Time.fixedDeltaTime/10 );

        if (distanceToPoint < 0.2f)
        {
           TakeTurn();
        }
    }
    void TakeTurn()
    {
        transform.position = wayPoint[nextWayPoint].position;
        Vector3 currentAngle = transform.rotation.eulerAngles;
        currentAngle.z += wayPoint[nextWayPoint].rotation.eulerAngles.z;
        transform.eulerAngles = currentAngle;
        chooseNextPoint();

    }
    void chooseNextPoint()
    {
        nextWayPoint++;
        if(nextWayPoint == wayPoint.Length) { nextWayPoint = 0; }
    }
 
}
