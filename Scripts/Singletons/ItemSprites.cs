using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites : MonoBehaviour
{
    public static ItemSprites Instance { get; private set; }

    private Sprite[] TestSprites;

    public Sprite Ether { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            TestSprites = Resources.LoadAll<Sprite>("TestSprites");
            foreach(var sprite in TestSprites)
            {
                switch (sprite.name)
                {
                    case "Ether":
                        Ether = sprite;
                        break;
                    default:
                        break;
                }
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}