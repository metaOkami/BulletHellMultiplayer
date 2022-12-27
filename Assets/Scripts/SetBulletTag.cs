using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBulletTag : MonoBehaviour
{
    public static int myTag;

    private void Start()
    {
        switch (this.name)
        {
            case "0":
                myTag = 1;
                break;
            case "1":
                myTag = 2;
                break;
            case "2":
                myTag = 3;
                break;
            case "3":
                myTag = 4;
                break;
            case "4":
                myTag = 5;
                break;
            case "5":
                myTag = 6;
                break;
            case "6":
                myTag = 7;
                break;
            case "7":
                myTag = 8;
                break;

        }
    }
}
