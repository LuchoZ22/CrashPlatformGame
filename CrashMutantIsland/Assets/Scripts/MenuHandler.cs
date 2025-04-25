using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    public GameObject Crash;
   

    // Start is called before the first frame update
    void Start()
    {
       
        Crash.GetComponent<Animator>().SetBool("sprint", true);
    }

    // Update is called once per frame
    public void GoToLevel()
    {
        SceneManager.LoadScene(1);
    }

    
}
