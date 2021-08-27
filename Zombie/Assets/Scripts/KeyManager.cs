using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    //[SerializeField] public Key[] allKeys;
    [HideInInspector] public List<Key> haveKeys = new List<Key>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Adds key to do have keys list
    /// </summary>
    /// <param name="key"></param>
    public void AddKey(Key key)
    {
        haveKeys.Add(key);
        if (key.keyImage!=null)
            key.keyImage.SetActive(true);
        //Instantiate(key.keyImage);
    }

    public bool DoIHaveThisKey(Key key)
    {
        foreach (var item in haveKeys)
        {
            if (item == key)
            {
                return true;
            }
        }
        return false;
    }

}
