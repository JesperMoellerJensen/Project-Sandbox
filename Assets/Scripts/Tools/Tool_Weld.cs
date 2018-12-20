using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class Tool_Weld : Tool
    {
        public GameObject Entity1;
        public GameObject Entity2;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Weld();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SelectObject();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                UnWeld();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetObjects();
            }

        }

        void Weld()
        {
            Destroy(Entity1.GetComponent<Rigidbody>());
            Destroy(Entity2.GetComponent<Rigidbody>());

            if (Entity1.transform.parent == null && Entity2.transform.parent == null)
            {
                var parent = new GameObject();

                Entity1.transform.SetParent(parent.transform);
                Entity2.transform.SetParent(parent.transform);

                parent.AddComponent<Rigidbody>();
            }
            else
            {
                if (Entity1.transform.parent == null && Entity2.transform.parent != null)
                {
                    var parent = Entity2.transform.parent;

                    Entity1.transform.SetParent(parent.transform);
                }

                if (Entity1.transform.parent != null && Entity2.transform.parent == null)
                {
                    var parent = Entity1.transform.parent;

                    Entity2.transform.SetParent(parent.transform);
                }

                if (Entity1.transform.parent != null && Entity2.transform.parent != null)
                {
                    var tempParent1 = Entity1.transform.parent;
                    var tempParent2 = Entity2.transform.parent;

                    List<Transform> children = new List<Transform>();
                    GameObject parent = new GameObject();

                    foreach (Transform child in tempParent1)
                    {
                        children.Add(child);
                    }
                    foreach (Transform child in tempParent2)
                    {
                        children.Add(child);
                    }
                    foreach(Transform child in children)
                    {
                        child.SetParent(parent.transform);
                    }
                    parent.AddComponent<Rigidbody>();

                    Destroy(tempParent1.gameObject);
                    Destroy(tempParent2.gameObject);
                }
            } 

            ResetObjects();
        }

        void UnWeld()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.parent == null)
                {
                    return;
                }

                Transform parent = hit.collider.transform.parent;
                List<Transform> children = new List<Transform>();

                if (parent.childCount == 2)
                {
                    Debug.Log("only 2");
                    foreach (Transform child in parent)
                    {
                        children.Add(child);
                    }

                    foreach(Transform child in children)
                    {
                        child.SetParent(null);
                        child.gameObject.AddComponent<Rigidbody>();
                    }
                    Destroy(parent.gameObject);
                }
                else
                {
                    hit.collider.transform.SetParent(null);
                    hit.collider.gameObject.AddComponent<Rigidbody>();
                }
            }
        }

        void SelectObject()
        {
            GameObject target = GetLookAtObject(Camera.main);

            if (Entity1 == null)
            {
                Entity1 = target;
            }
            else if(target != Entity1)
            {
                Entity2 = target;
            }
        }

        void ResetObjects()
        {
            Entity1 = null;
            Entity2 = null;
        }
    }
}
