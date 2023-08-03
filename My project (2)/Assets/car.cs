using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 10f; // ความเร็วสูงสุดของรถ
    public float acceleration = 5f; // ความเร่งของรถ
    public float brakingPower = 10f; // แรงเบรกของรถ
    public float steeringSpeed = 5f; // ความเร็วในการเลี้ยวของรถ

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // รับคอมโพเนนต์ Rigidbody ของรถ
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // กำหนดให้รถไม่หมุนรอบแกน X และ Z เพื่อให้เคลื่อนที่เป็นระบบ 2D
    }

    private void FixedUpdate()
    {
        MoveCar();
        SteerCar();
        ApplyBrakes();
    }

    private void MoveCar()
    {
        float verticalInput = Input.GetAxis("Vertical"); // รับค่าการเคลื่อนที่ในแนวดิ่ง (W, S, Arrow Up, Arrow Down)

        // ตรวจสอบค่าความเร็วปัจจุบันของรถ
        float currentSpeed = rb.velocity.magnitude;

        // ให้รถเคลื่อนที่ไปข้างหน้าเมื่อมีการกดปุ่มเคลื่อนที่และยังไม่เกินความเร็วสูงสุด
        if (Mathf.Abs(verticalInput) > 0.1f && currentSpeed < maxSpeed)
        {
            rb.AddForce(transform.forward * verticalInput * acceleration, ForceMode.Acceleration);
        }
    }

    private void SteerCar()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // รับค่าการเลี้ยวของรถ (A, D, Arrow Left, Arrow Right)

        // คำนวณความเร็วในการหมุนของรถ
        Vector3 rotation = rb.rotation.eulerAngles;
        rotation.y += horizontalInput * steeringSpeed * Time.deltaTime;
        rb.MoveRotation(Quaternion.Euler(rotation));
    }

    private void ApplyBrakes()
    {
        float brakeInput = Input.GetAxis("Jump"); // รับค่าการเบรกของรถ (Space)

        // ให้รถเบรกเมื่อมีการกดปุ่มเบรก
        if (brakeInput > 0f)
        {
            rb.AddForce(rb.velocity.normalized * -brakingPower * brakeInput, ForceMode.Acceleration);
        }
    }
}
