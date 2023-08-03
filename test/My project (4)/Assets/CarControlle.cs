using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        // ตรวจสอบว่า frontLeftWheelCollider ไม่เป็น null และมี Component WheelCollider แล้วก่อนที่จะเรียกใช้งาน
        if (frontLeftWheelCollider != null && frontLeftWheelCollider.GetComponent<WheelCollider>() != null)
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        }

        // ตรวจสอบว่า frontRightWheelCollider ไม่เป็น null และมี Component WheelCollider แล้วก่อนที่จะเรียกใช้งาน
        if (frontRightWheelCollider != null && frontRightWheelCollider.GetComponent<WheelCollider>() != null)
        {
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        }

        // ตรวจสอบว่าทุกเครื่องวัตถุที่เกี่ยวข้องกับการเบรก (WheelCollider) ไม่เป็น null และมี Component WheelCollider แล้วก่อนที่จะเรียกใช้งาน
        if (frontLeftWheelCollider != null && frontLeftWheelCollider.GetComponent<WheelCollider>() != null &&
            frontRightWheelCollider != null && frontRightWheelCollider.GetComponent<WheelCollider>() != null &&
            rearLeftWheelCollider != null && rearLeftWheelCollider.GetComponent<WheelCollider>() != null &&
            rearRightWheelCollider != null && rearRightWheelCollider.GetComponent<WheelCollider>() != null)
        {
            brakeForce = isBreaking ? 3000f : 0f;
            frontLeftWheelCollider.brakeTorque = brakeForce;
            frontRightWheelCollider.brakeTorque = brakeForce;
            rearLeftWheelCollider.brakeTorque = brakeForce;
            rearRightWheelCollider.brakeTorque = brakeForce;
        }
    }

    private void Start()
    {
        // เมื่อเริ่มเกม ให้ค้นหา WheelCollider ที่อยู่บน GameObject และเก็บไว้ในตัวแปร frontLeftWheelCollider, frontRightWheelCollider, rearLeftWheelCollider, และ rearRightWheelCollider
        frontLeftWheelCollider = frontLeftWheelTransform.GetComponent<WheelCollider>();
        frontRightWheelCollider = frontRightWheelTransform.GetComponent<WheelCollider>();
        rearLeftWheelCollider = rearLeftWheelTransform.GetComponent<WheelCollider>();
        rearRightWheelCollider = rearRightWheelTransform.GetComponent<WheelCollider>();
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }
}
