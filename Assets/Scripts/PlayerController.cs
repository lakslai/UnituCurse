using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    
    [SerializeField] private Transform _camera;
    [SerializeField] private float _cameraSensitivity = 2f;
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private float _checkJumpRadius = 0.2f;
    [SerializeField] private float _jumpForce = 5f;


    [SerializeField] Item[] items;
    [SerializeField] private Transform _handPivot;


    private float _rotationX;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (_photonView.IsMine)
        {
            EquipItem(0);
        }

        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(_rigidbody);
        }
       
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
            PlayerMovement();
    }

    private void Update()
    {
        if (!_photonView.IsMine) 
        {
            return;
        }
        RotatePlayerRightLeft();
        RotateCameraUpDown();


        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
    
      /*  for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;  
            }
           
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(1);
        }
    }

    private void TryJump()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position - Vector3.down * 0.5f, _checkJumpRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
                return;
        }

        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void EquipItem(int _index)
    {
        /*if (previousItemIndex == _index)
        {
            return;
        }

        itemIndex = _index;
        items[_index].gameObject.SetActive(true);
        
        
        if (previousItemIndex != -1)
        {
            items[previousItemIndex].gameObject.SetActive(true);
        }
        
        previousItemIndex = itemIndex;*/

        for(int i=0; i<items.Length; i++)
        {
            if (i == _index)
            {
                items[i].gameObject.SetActive(true);   
            }
            else { items[i].gameObject.SetActive(false); }
        }
    }
    
    private void PlayerMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movementDir = transform.forward * v + transform.right * h;
        movementDir = Vector3.ClampMagnitude(movementDir, 1f);

        _rigidbody.velocity = new Vector3(movementDir.x * _movementSpeed, _rigidbody.velocity.y,
            movementDir.z * _movementSpeed);
    }

    private void RotatePlayerRightLeft()
    {
        transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * _cameraSensitivity);
    }

    private void RotateCameraUpDown()
    {
        _rotationX -= _cameraSensitivity * Input.GetAxisRaw("Mouse Y");
        _rotationX = Mathf.Clamp(_rotationX, -65, 65);

        // Поворачиваем камеру
        _camera.eulerAngles = new Vector3(_rotationX, _camera.eulerAngles.y, _camera.eulerAngles.z);

        // Поворачиваем HandPivot вместе с телом
        _handPivot.eulerAngles = new Vector3(_rotationX, _handPivot.eulerAngles.y, _handPivot.eulerAngles.z);

    }
}
