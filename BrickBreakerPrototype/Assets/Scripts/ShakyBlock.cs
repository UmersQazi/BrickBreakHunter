using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyBlock : MonoBehaviour
{
    [SerializeField] private bool shattered;
    public float shakeSpeed = 5f;
    public float shakeIntensity = 2f;

    public bool contact;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(contact)
        {
            gameObject.transform.right *= Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
        }


        if (shattered)
        {
            StartCoroutine(Reappear());
        }
        
    }

   


    IEnumerator destroyUnderPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            gameObject.SetActive(false);
            shattered = true;
        }
        
    }

    IEnumerator Reappear()
    {
        if(shattered)
        {
            yield return new WaitForSeconds(3);
            gameObject.SetActive(true);
            shattered = false;
            contact = false;
        }
        
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = true;
            
            StartCoroutine(destroyUnderPlayer());
        }
    }
}
