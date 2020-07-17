using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Threading;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    private static bool check;
    private Rigidbody rb;
    private int count;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
        count = 0;
        SetCountText();
        winText.text = "";
        check = false;
       

    }

    void FixedUpdate()
    {
       float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

       Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pick Up"))
        {
            Debug.Log(collision.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text.ToString());
            if (IsPalindrome(collision.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text.ToString()))
            {

                collision.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
                check = false;

            }

            else
            {
                Debug.Log("particle");
                collision.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            
             }

        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= Instantiation.PalindromeCount)
        {
            winText.text = "All  "+ Instantiation.PalindromeCount + " Palindrome Cubes are collected";
        }
    }

    public static bool IsPalindrome(string text)
    {
        if (text.Length <= 1)
            return true;
        else
        {
            if (text[0] != text[text.Length - 1])
                return false;
            else
                return IsPalindrome(text.Substring(1, text.Length - 2));
        }
    }
}
