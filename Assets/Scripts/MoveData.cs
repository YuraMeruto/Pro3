using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData : MonoBehaviour
{
    [SerializeField]
    GameObject MoveAreaObj;
    [SerializeField]
    GameObject Master;
    private int[,] ReadMoveData = new int[10, 10];//読み込まれた

    private int MyPositionX;    //読み込んだデータの自分のポジション
    private int MyPositionZ;    //読み込んだデータの自分のポジション
                                //CSVの自分のポジションとの差分を計算
    private int DifferenceX;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceZ;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceXCopy;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceZCopy;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int MassNumber;
    private int NowMyPosx = 0;
    private int NowMyPosz = 0;
    private int AbsPositionX;
    private int AbsPositionZ;
    private int ResultCalucationX;
    private int ResultCalucationZ;
    // Use this for initialization
    void Start()
    {
        Master = GameObject.Find("Master");
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                ReadMoveData[x, z] = Master.GetComponent<ReadCsv>().InputMoveData(x, z);
                if (ReadMoveData[x, z] == 2)
                {
                    MyPositionX = x;
                    MyPositionZ = z;
                }
            }
        }
    }

    /// <summary>
    /// 自分がどこのますいるかを検索
    /// </summary>
    /// <param name="num"></param>
    public void IsPossibleMove(int num)
    {
        MassNumber = num;
        int CopyMyPositionX = MyPositionX;
        int CopyMyPositionZ = MyPositionZ;
        //自分がどこのますにいるかをけんさく
        for (int length = 0; length < 10; length++)
        {
            for (int side = 0; side < 10; side++)
            {
                if (Master.GetComponent<BoardMaster>().MassNum[length, side] == num)
                {
                    GameObject ret;
                    ret = Master.GetComponent<BoardMaster>().GetCharObject(length, side);
                    if (ret == null)
                    {
                        break;
                    }
                    else
                    {
                        NowMyPosx = side;
                        NowMyPosz = length;
                        Debug.Log("aa");
                        InstanceIsPossibleMoveArea();
                    }
                    break;
                }
            }
        }
        //自分のいるポジションからMoveDataの自分のポジションとの差分を計算
        //        DifferencCalculation(NowMyPosx, NowMyPosz);
    }

    /*
    public void DifferencCalculation(int nowposx, int nowposz)
    {
        if (MyPositionX >= nowposx)
        {
            DifferenceX = MyPositionX - nowposx;
        }

        else if (MyPositionX <= nowposx)
        {
            DifferenceX = nowposx - MyPositionX;
        }

        if (MyPositionZ >= nowposz)
        {
            DifferenceZ = MyPositionZ - nowposz;
        }

        else if (MyPositionZ <= nowposz)
        {
            DifferenceX = nowposz - MyPositionZ;
        }
        DifferenceXCopy = DifferenceX;
        DifferenceZCopy = DifferenceZ;
    }
    */

    /// <summary>
    /// 移動範囲の表示及び生成
    /// </summary>
    public void InstanceIsPossibleMoveArea()
    {
        for (int length = 0; length < 10; length++)
        {
            for (int side = 0; side < 10; side++)
            {
              //  int AbsPositionX = Mathf.Abs(MyPositionX - side);
              //  int AbsPositionZ = Mathf.Abs(MyPositionZ - length);
              //Debug.Log(AbsPositionX);
                bool retif = true;
                retif = CutCaliculation(length,side);
                if (retif)
                {
                    if (ReadMoveData[ResultCalucationZ, ResultCalucationX] == 1)
                    {
                        bool ret = true;
                        ret = Master.GetComponent<BoardMaster>().GetMassStatus(length, side);
                        if (ret)
                        {
                            Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[length, side].transform.position;
                            InstancePos.y = 1.0f;
                            Instantiate(MoveAreaObj, InstancePos, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    bool CutCaliculation(int Masslength,int Massside)
    {
        bool  ret = true;

        AbsPositionX = Mathf.Abs(MyPositionX - Massside);
        AbsPositionZ = Mathf.Abs(MyPositionZ - Masslength);
        if(MyPositionZ<=Masslength)
        {
            ResultCalucationZ = MyPositionZ + AbsPositionZ;
        }
        else
        {
            ResultCalucationZ = MyPositionZ - AbsPositionZ;
        }

        if (MyPositionX <= Massside)
        {
            ResultCalucationX = MyPositionX + AbsPositionX;
        }
        else
        {
            ResultCalucationX = MyPositionX - AbsPositionX;
        }
        if (ResultCalucationZ<=0 || ResultCalucationX <= 0)
        {
            ret = false;
        }

        else if(ResultCalucationZ >= 10 || ResultCalucationX >= 10)
        {
            ret = false;
        }
        return ret;
    }
}
