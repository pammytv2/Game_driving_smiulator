using UnityEngine;

public class KinematicMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPosition;
    private float movementSpeed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ตรวจสอบการคลิกเม้าส์
        if (Input.GetMouseButtonDown(0))
        {
            // รับตำแหน่งเป้าหมายที่คลิกเม้าส์
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
            }
        }
    }

    private void FixedUpdate()
    {
        // เคลื่อนที่ไปที่ตำแหน่งเป้าหมายโดยใช้ MovePosition()
        Vector3 direction = (targetPosition - rb.position).normalized;
        Vector3 velocity = direction * movementSpeed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
