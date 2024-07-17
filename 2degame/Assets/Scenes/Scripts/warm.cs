using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class warm : MonoBehaviour
{
    private PlayerMovement pm;

    public GameObject freezenotice;
    public GameObject warmupnotice;
    public bool freezing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        freezenotice.SetActive(false);
        warmupnotice.SetActive(false);
        freezing = false;
        
    }

    IEnumerator coldtimer()
    {
        yield return new WaitForSeconds(10f);
        if(pm.haswarmpowerup == false)
        {
            freezing = true;
        }

    }
    IEnumerator warmtimer()
    {
        yield return new WaitForSeconds(7f);
        warmupnotice.SetActive(false);

    }
    IEnumerator freezetimer()
    {
        yield return new WaitForSeconds(5f);
        if(pm.haswarmpowerup == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            freezing = false;

        }

        
    }


    // Update is called once per frame
    void Update()
    {
        if(pm.haswarmpowerup)
        {
            warmupnotice.SetActive(true);
            freezenotice.SetActive(false);
            freezing = false;
            StopCoroutine(freezetimer());
            StopCoroutine(coldtimer());
            StartCoroutine(warmtimer());

        }
        else if(freezing == false)
        {
            StartCoroutine(coldtimer());
        }
        else if(freezing)
        {
            freezenotice.SetActive(true);
            StartCoroutine(freezetimer());

            

        }
        
        
    }
}
