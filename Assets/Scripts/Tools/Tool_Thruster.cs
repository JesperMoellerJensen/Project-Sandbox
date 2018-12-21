using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_Thruster : Tool
{
	public GameObject ThrusterPrefab;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			CreateThruster();
		}
    }

	void CreateThruster()
	{
	    var hitInfo = GetLookAtObjectWithHitInfo(Camera.main);
	    if (hitInfo.collider == null)
	    {
	        return;
	    }

	    if (hitInfo.collider.transform.root.GetComponent<Rigidbody>() != null)
	    {
	        GameObject thruster = Instantiate(ThrusterPrefab);
	        thruster.transform.forward = -hitInfo.normal;
	        thruster.transform.position = hitInfo.point;
	        thruster.transform.SetParent(hitInfo.collider.gameObject.transform);
        }
	}
}
