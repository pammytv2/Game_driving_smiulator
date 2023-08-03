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
        // ��Ǩ�ͺ��� frontLeftWheelCollider ����� null ����� Component WheelCollider ���ǡ�͹�������¡��ҹ
        if (frontLeftWheelCollider != null && frontLeftWheelCollider.GetComponent<WheelCollider>() != null)
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        }

        // ��Ǩ�ͺ��� frontRightWheelCollider ����� null ����� Component WheelCollider ���ǡ�͹�������¡��ҹ
        if (frontRightWheelCollider != null && frontRightWheelCollider.GetComponent<WheelCollider>() != null)
        {
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        }

        // ��Ǩ�ͺ��ҷء����ͧ�ѵ�ط������Ǣ�ͧ�Ѻ����á (WheelCollider) ����� null ����� Component WheelCollider ���ǡ�͹�������¡��ҹ
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
        // ������������ ������ WheelCollider ������躹 GameObject ��������㹵���� frontLeftWheelCollider, frontRightWheelCollider, rearLeftWheelCollider, ��� rearRightWheelCollider
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
