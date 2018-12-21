using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_GravityGun : Tool
{
    private bool _isGrabbingObject;
    private GameObject _parent;
    private Rigidbody _parentBody;
    private float _distanceToTarget;
    private Quaternion _playerLocalRotationStart;
    private Quaternion _targetLocalRotationStart;
    private Vector3 _parentOffset;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            GrabObject();

        if (!Input.GetKey(KeyCode.Mouse0))
            ReleaseObject();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            FreezeObject();

        if (_isGrabbingObject)
            DragObject();

        if (Math.Abs(Input.GetAxisRaw("Mouse ScrollWheel")) != 0)
        {
            ChangeDistanceToTarget(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void GrabObject()
    {
        RaycastHit hit = GetLookAtObjectWithHitInfo(Camera.main);

        if (hit.collider == null)
        {
            return;
        }

        _parent = hit.collider.transform.root.gameObject;

        if (_parent.GetComponent<Rigidbody>())
        {
            _distanceToTarget = hit.distance;
            _parentBody = _parent.GetComponent<Rigidbody>();
            _parentBody.useGravity = false;
            _playerLocalRotationStart = Quaternion.Inverse(transform.root.localRotation);
            _targetLocalRotationStart = _parentBody.rotation;
            _parentOffset = _parentBody.position - hit.point;

            UnFreezeObjectInHand();
            _isGrabbingObject = true;
        }
    }

    void FreezeObject()
    {
        if (_isGrabbingObject)
        {
            Rigidbody temp = _parentBody;
            ReleaseObject();

            temp.isKinematic = true;
        }
    }

    void UnFreezeObjectInHand()
    {
        _parentBody.isKinematic = false;
    }

    void ReleaseObject()
    {
        if (_isGrabbingObject)
        {
            _isGrabbingObject = false;
            _parent.GetComponent<Rigidbody>().useGravity = true;
            _parent = null;
        }
    }

    void ChangeDistanceToTarget(float input)
    {
        if (_isGrabbingObject)
        {
            _distanceToTarget += input*5;
            _distanceToTarget = Mathf.Clamp(_distanceToTarget, 1, float.MaxValue);
        }
    }

    void DragObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        _parentBody.velocity = (ray.GetPoint(_distanceToTarget) - _parentBody.position + _parentOffset) * 500 * Time.deltaTime;

        Quaternion offsetRotation = transform.root.localRotation * _playerLocalRotationStart;

        //TODO: fix rotation
        Quaternion rotation = offsetRotation * _targetLocalRotationStart;
        //_parentBody.rotation = rotation;
    }
}
