using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    
    [SerializeField] private Transform _camera;
    [SerializeField] private float _cameraSensitivity = 2f;
    [SerializeField] private float _movementSpeed = 6f;
    [SerializeField] private float _jumpForce = 7.5f;


    [SerializeField] Item[] items;
    bool isGrouded = false;


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


        if (Input.GetButtonDown("Jump") && isGrouded)
        {
            TryJump();
        }
    
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
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void SetGroundState(bool _ground)
    {
        isGrouded = _ground;
    }

    private void EquipItem(int _index)
    {
        for(int i=0; i<items.Length; i++)
        {
            if (i == _index)
            {
                items[i].gameObject.SetActive(true);   
            }
            else { items[i].gameObject.SetActive(false); }
        }

     /*   if (_photonView.IsMine)
        {
            Hashtable has = new Hashtable();
            has.Add("items", items);
            PhotonNetwork.LocalPlayer.SetCustomProperties(has);
        }*/
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

        

    }
}
