using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cold : MonoBehaviour
{
    public GameObject burnnotice;
    public GameObject coolingnotice;
    public bool burning = false;
    private PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        coolingnotice.SetActive(false);
        burnnotice.SetActive(false);
        burning = false;
        
    }

    IEnumerator warmingtimer()
    {
        yield return new WaitForSeconds(10f);
        if(pm.oncooldownzone == false)
        {
            burning = true;
        }

    }
    IEnumerator burningtimer()
    {
        yield return new WaitForSeconds(5f);
        if(pm.oncooldownzone == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            burning = false;

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.oncooldownzone)
        {
            burnnotice.SetActive(false);
            burning = false;

        }
        else if(burning == false)
        {

            StartCoroutine(warmingtimer());
        }
        else if(burning)
        {

            burnnotice.SetActive(true);
            StartCoroutine(burningtimer());

            

        }
        
    }
}
