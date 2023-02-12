using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("���������")]
    [SerializeField] private GameObject[] _engines;
    [Tooltip("����� ����������")]
    [SerializeField] private GameObject[] _screws;
    [Tooltip("�������� �������� ����������")]
    [SerializeField] private float _spinSpeed;
    [Tooltip("������ ���� ����")]
    [SerializeField] private float _floatForce = 0.1f;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _rotationSpeed = 1f;

    [Tooltip("���������� ���� ������")]
    [SerializeField] private Rigidbody containerRb;
    [Tooltip("���������� ���� ������")]
    [SerializeField] private Rigidbody playerRb;

    [Tooltip("������ ���������� ������ � ������ �����")]
    [SerializeField] private bool _lowEnough;

    [Tooltip("����������� � �������� ������")]
    [SerializeField] private bool _phisicsMove;

    private Vector2 _inputVector;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _inputVector = new Vector2(horizontalInput, verticalInput);
        Debug.Log("_inputVector " + _inputVector);

        if (transform.position.y >= 45f || transform.position.y < 0f)
        {
            _lowEnough = false;
        }
        else
        {
            _lowEnough = true;
            
        }
    }

    private void FixedUpdate()
    {
        RotationScrew();

        RotationEngine();

        Move();
    }

    /// <summary>
    /// �������� �������� ����������
    /// </summary>
    private void RotationScrew()
    {
        foreach (GameObject screw in _screws)
        {
            screw.transform.Rotate(Vector3.up, _spinSpeed * Time.deltaTime);
        }
    }
    /// <summary>
    /// ������� ����������
    /// </summary>
    private void RotationEngine()
    {
        //������ ����� ����������� � �������� ������
        if (_phisicsMove)
        {
            //�������� �� ������� �������
            if (Input.GetKey(KeyCode.E))
            {
                foreach (GameObject engine in _engines)
                {
                    ////�������� � ������� ������ #1
                    //Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
                    //engineRigidbody.angularVelocity = new Vector3(0f, 0f, Time.deltaTime * -_rotationSpeed);

                    //�������� � ������� ������ #2
                    Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
                    Vector3 resultEuler = engine.transform.eulerAngles + new Vector3(Time.deltaTime * _rotationSpeed, 0f, 0f);
                    engineRigidbody.MoveRotation(Quaternion.Euler(resultEuler));
                    
                    //containerRb.MoveRotation(Quaternion.Euler(resultEuler));
                    //playerRb.MoveRotation(Quaternion.Euler(-resultEuler));

                    ////�������� � ������� ������ #3
                    //Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
                    //Vector3 resultEuler = engine.transform.eulerAngles + new Vector3(Time.deltaTime * _rotationSpeed, 0f, 0f);
                    //Quaternion _targetRotation = Quaternion.RotateTowards(engine.transform.rotation, Quaternion.Euler(resultEuler), _rotationSpeed);
                    //engineRigidbody.MoveRotation(_targetRotation);

                    Debug.DrawLine(engine.transform.position, engine.transform.position - engine.transform.up * 5, Color.green);
                }
                
            }

            //�������� � ������� �������
            if (Input.GetKey(KeyCode.Q))
            {
                foreach (GameObject engine in _engines)
                {
                    ////�������� � ������� ������ #1
                    //Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
                    //engineRigidbody.angularVelocity = new Vector3(0f, 0f, Time.deltaTime * _rotationSpeed);

                    //�������� � ������� ������ #2
                    Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
                    Vector3 resultEuler = engine.transform.eulerAngles - new Vector3(Time.deltaTime * _rotationSpeed, 0f, 0f);
                    engineRigidbody.MoveRotation(Quaternion.Euler(resultEuler));

                    //containerRb.MoveRotation(Quaternion.Euler(resultEuler));
                    //playerRb.MoveRotation(Quaternion.Euler(-resultEuler));

                    ////�������� � ������� ������ #3
                    //Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
                    //Vector3 resultEuler = engine.transform.eulerAngles + new Vector3(Time.deltaTime * _rotationSpeed, 0f, 0f);
                    //Quaternion _targetRotation = Quaternion.RotateTowards(engine.transform.rotation, Quaternion.Euler(resultEuler), _rotationSpeed);
                    //engineRigidbody.MoveRotation(_targetRotation);

                    Debug.DrawLine(engine.transform.position, engine.transform.position - engine.transform.up * 5, Color.green);
                }
            }
        }
        else
        {
            //�������� �� ������� �������
            if (Input.GetKey(KeyCode.E))
            {
                foreach (GameObject engine in _engines)
                {
                    ////�������� ��� ������ #1
                    //engine.transform.eulerAngles += new Vector3(Time.deltaTime * _rotationSpeed, 0f, 0f);

                    //�������� ��� ������ #2
                    engine.transform.Rotate(Time.deltaTime * _rotationSpeed, 0f, 0f);

                    Debug.DrawLine(engine.transform.position, engine.transform.position - engine.transform.up * 5, Color.green);
                }
            }

            //�������� ����� ������� �������
            if (Input.GetKey(KeyCode.Q))
            {
                foreach (GameObject engine in _engines)
                {
                    ////�������� ��� ������ #1
                    //engine.transform.eulerAngles += new Vector3(Time.deltaTime * _rotationSpeed, 0f, 0f);

                    //�������� ��� ������ #2
                    engine.transform.Rotate(Time.deltaTime * -_rotationSpeed, 0f, 0f);

                    Debug.DrawLine(engine.transform.position, engine.transform.position - engine.transform.up * 5, Color.green);
                }
            }
        }
    }

    /// <summary>
    /// ���� �������� ����������
    /// </summary>
    /// <returns></returns>
    private void Move()
    {
        ////����������� ��� ������
        ////�������� �� �������������� ���
        //float h = Input.GetAxis("Horizontal");
        ////�������� �� ������������ ���
        //float v = Input.GetAxis("Vertical");
        ////����� ��������
        //Vector3 offset = new Vector3(0, v, h) * _floatForce;
        //transform.Translate(offset);


        if (_inputVector.y > 0)
        {
            //foreach (GameObject engine in _engines)
            //{
            //    Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
            //    engineRigidbody.AddRelativeForce(_floatForce * Vector3.forward);
            //}

            playerRb.AddRelativeForce(_floatForce * Vector3.forward);
            //containerRb.AddRelativeForce(_inputVector.y * _floatForce * 2 * Vector3.forward, ForceMode.Acceleration);
        }
        else if (_inputVector.y < 0)
        {
            playerRb.AddRelativeForce(_floatForce * Vector3.back);
        }

        foreach (GameObject engine in _engines)
        {
            Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
            engineRigidbody.AddRelativeTorque(_inputVector.x * _rotationSpeed * Vector3.right);
        }

        //playerRb.AddRelativeTorque(_inputVector.x * _rotationSpeed * Vector3.right);

        //transform.position = playerRb.position;

        

        //if (Input.GetKey(KeyCode.Space))
        //{

        //    Vector3 screwsTransformRotation = _engines[0].transform.up;

        //    foreach (GameObject engine in _engines)
        //    {
        //        Rigidbody engineRigidbody = engine.GetComponent<Rigidbody>();
        //        engineRigidbody.MovePosition(engine.transform.position + engine.transform.up * _floatForce * Time.deltaTime);
        //    }

        //    containerRb.MovePosition(transform.position + transform.up * _floatForce * Time.deltaTime);
        //    playerRb.MovePosition(transform.position + transform.up * _floatForce * Time.deltaTime);
        //}

        //playerRb.MoveRotation(Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed));
        ////playerRb.MovePosition(transform.position + transform.forward * _floatForce * Time.deltaTime);
        ////Vector3 resultEuler = transform.eulerAngles + new Vector3(1f, 0f, 0f);
        ////playerRb.MoveRotation(Quaternion.Euler(resultEuler));
    }
}
