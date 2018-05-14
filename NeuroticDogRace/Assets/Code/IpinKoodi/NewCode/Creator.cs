using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class Creator  {

	public static Transform ConstructDog(Transform prefab, Vector3 position,Vector3 rotation)
    {
        Transform dog = GameObject.Instantiate(prefab,position,Quaternion.Euler(rotation));
        dog.gameObject.SetActive(true);
        return dog;
    }

    public static void ConstructAcademy()
    {
        Info.Instance.academy = new Academy();
    }

    public static Transform[] BuildUnitsInRow(Transform prefab, Vector3 center, float space)
    {
        Transform[] units = new Transform[Info.Instance.unitCount];
        Vector3 Left = new Vector3(space*Info.Instance.unitCount / 2, 0, 0);
        for (int i = 0; i < Info.Instance.unitCount; i++)
        {
            Transform dog = Creator.ConstructDog(prefab, center-Left+Vector3.right*i*space, Vector3.zero);
            dog.GetComponentInChildren<Doggy>().target = GameObject.FindGameObjectWithTag("Target");
            units[i] = dog;
        }
        return units;
    }

    public static Transform[] BuildUnitsInCircle(Transform prefab, Vector3 center, float radius)
    {
        Debug.LogWarning("From Creator");
        Debug.LogWarning("BUILD UNITS IN CIRCLE IMPLEMENTATION IS NOT WORKING CORRECT!");
        Transform[] units = new Transform[Info.Instance.unitCount];

        for (int i = 0; i < Info.Instance.unitCount; i++)
        {
            float angle =((float) i) * (1+units.Length)/15;
            Transform dog = Creator.ConstructDog(prefab, center + radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)), Vector3.up * units.Length * i);
            //Transform dog = Creator.ConstructDog(prefab, center + new Vector3(Mathf.Sin(angle) ,0, Mathf.Cos(angle)) , new Vector3(0,angle,0));
            dog.GetComponentInChildren<Doggy>().target = GameObject.FindGameObjectWithTag("Target");
            units[i] = dog;
        }
        return units;
    }

}
