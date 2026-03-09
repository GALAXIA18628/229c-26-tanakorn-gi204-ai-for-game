using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints;
    public float patrolSpeed = 2.0f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;
        //กำหนดเป้าหมายเป็น waypoint ปัจจุบัน
        Vector3 targetPos = waypoints[currentWaypointIndex].position;
        targetPos.y = transform.position.y; //ให้ npc เคลื่อนที่ในแนวนอนเท่านั้น

        //เคลื่อนที่ไปยังเป้าหมาย
        MoveTowards(targetPos, patrolSpeed);

        //ตรวจสอบว่าถึง waypoint หรือยัง
        if (Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            //ไปยัง waypoint ถัดไป
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        
    }

    void MoveTowards(Vector3 targetPos, float speed)
    {
        //คำนวณทิศทาง
        Vector3 direction = (targetPos - transform.position).normalized;
        
        //อัปเดตตำแหน่ง
        transform.position += direction * (speed * Time.deltaTime);

        //หมุนหน้าให้หันไปทางเป้าหมาย
        if (direction != Vector3.zero)
        {
            transform.up = -direction;
            Vector3 currentAngles = transform.eulerAngles;
            currentAngles.x = -90f; //ล็อกการหมุนในแกน x
            transform.eulerAngles = currentAngles;
        }
    }
}