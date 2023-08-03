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
        // ��Ǩ�ͺ��ä�ԡ������
        if (Input.GetMouseButtonDown(0))
        {
            // �Ѻ���˹�������·���ԡ������
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
        // ����͹���价����˹������������ MovePosition()
        Vector3 direction = (targetPosition - rb.position).normalized;
        Vector3 velocity = direction * movementSpeed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
