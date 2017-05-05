using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    LayerMask MassLayer;
    private GameObject AtachMassObjct;
    private GameObject AtachCharObjct = null;
    private int AtachMassNumber;
    private GameObject MasterObject;
    private enum Status { None, Choose };
    private Status status = Status.None;
    // Use this for initialization
    void Start()
    {
        MasterObject = GameObject.Find("Master");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MassLayer))
            {
                switch (status)
                {
                    case Status.None:
                        AtachMassObjct = hit.collider.gameObject;
                        AtachMassNumber = AtachMassObjct.GetComponent<NumberMass>().GetNumber();
                        AtachCharObjct = MasterObject.GetComponent<BoardMaster>().GetCharObject(AtachMassNumber);                    
                        if (AtachCharObjct != null)
                        {
                            AtachCharObjct.GetComponent<MoveData>().IsPossibleMove(AtachMassNumber);
                            status = Status.Choose;
                        }
                        break;
                }
                //Debug.Log(AtachMassObjct);
                //Debug.Log(AtachMassNumber);
                //Debug.Log(AtachCharObjct);
                //Debug.Log(status);
            }
        }
    }
}
