using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator  {

	public static void ConstructDog(Transform prefab, Vector3 position,Vector3 rotation)
    {
        Transform dog = GameObject.Instantiate(prefab,position,Quaternion.Euler(rotation));
        dog.gameObject.SetActive(true);
    }

    public static void ConstructAcademy()
    {
        Info.Instance.academy = new Academy();
    }

    
}
