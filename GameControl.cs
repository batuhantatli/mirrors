using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoSingleton<GameControl>
{
    public static GameControl DontDestroy { get; private set ;}
    public int level ;
    private void Start() 
    {
        level = 1;
    }
    
    private void Awake() {
        if(DontDestroy  == null) {
            DontDestroy = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
