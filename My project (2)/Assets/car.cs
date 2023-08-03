using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 10f; // ���������٧�ش�ͧö
    public float acceleration = 5f; // ������觢ͧö
    public float brakingPower = 10f; // �ç�á�ͧö
    public float steeringSpeed = 5f; // ��������㹡�������Ǣͧö

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // �Ѻ����๹�� Rigidbody �ͧö
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // ��˹����ö�����ع�ͺ᡹ X ��� Z �����������͹������к� 2D
    }

    private void FixedUpdate()
    {
        MoveCar();
        SteerCar();
        ApplyBrakes();
    }

    private void MoveCar()
    {
        float verticalInput = Input.GetAxis("Vertical"); // �Ѻ��ҡ������͹�����Ǵ�� (W, S, Arrow Up, Arrow Down)

        // ��Ǩ�ͺ��Ҥ������ǻѨ�غѹ�ͧö
        float currentSpeed = rb.velocity.magnitude;

        // ���ö����͹���仢�ҧ˹��������ա�á���������͹�������ѧ����Թ���������٧�ش
        if (Mathf.Abs(verticalInput) > 0.1f && currentSpeed < maxSpeed)
        {
            rb.AddForce(transform.forward * verticalInput * acceleration, ForceMode.Acceleration);
        }
    }

    private void SteerCar()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // �Ѻ��ҡ�������Ǣͧö (A, D, Arrow Left, Arrow Right)

        // �ӹǳ��������㹡����ع�ͧö
        Vector3 rotation = rb.rotation.eulerAngles;
        rotation.y += horizontalInput * steeringSpeed * Time.deltaTime;
        rb.MoveRotation(Quaternion.Euler(rotation));
    }

    private void ApplyBrakes()
    {
        float brakeInput = Input.GetAxis("Jump"); // �Ѻ��ҡ���á�ͧö (Space)

        // ���ö�á������ա�á������á
        if (brakeInput > 0f)
        {
            rb.AddForce(rb.velocity.normalized * -brakingPower * brakeInput, ForceMode.Acceleration);
        }
    }
}
