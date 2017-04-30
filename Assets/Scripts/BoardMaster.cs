using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaster : MonoBehaviour
{

    private GameObject[,] MassObj = new GameObject[10, 10];
    public int[,] MassNum = new int[10, 10];

    [SerializeField]
    private int MaxLength;
    [SerializeField]
    private int MaxSide;

    private int MaxNumber;
    //マスのオブジェクト
    [SerializeField]
    private GameObject[] ObjMass = new GameObject[2];

    //キャラクターのオブジェクト
    [SerializeField]
    private GameObject[] ObjCharcter = new GameObject[2];

    private Vector3 InstancePos = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        int count = 0;
        int masscolor = 0;
        for (int length = 0; length < MaxLength; length++)
        {


            for (int side = 0; side < MaxSide; side++)
            {
                InstancePos.x = side;
                InstancePos.z = length;
                if (length == 0 && side == 0)
                    masscolor = 1;
                GameObject Mass = Instantiate(ObjMass[masscolor], InstancePos, Quaternion.identity) as GameObject;
                Mass.name = count.ToString();
                Mass.GetComponent<NumberMass>().SetNumber(count);
                MassNum[length, side] = count;
                if (length == 2)
                {
                    Vector3 CharacterInstancePos = InstancePos;
                    CharacterInstancePos.y = 1;
                    Instantiate(ObjCharcter[0], CharacterInstancePos, Quaternion.identity);
                }
                count++;
                if (masscolor == 0)
                {
                    masscolor = 1;
                }
                else {
                    masscolor = 0;
                }

            }
            if (masscolor % 2 == 0)
            {
                masscolor = 1;
            }
            else
            {
                masscolor = 0;
            }
        }
        MaxNumber = count;
    }

    public int GetMaxNumber()
    {
        return MaxNumber;
    }

    
}
