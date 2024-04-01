using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPickup : MonoBehaviour
{
    public string pickupableTag;

    public float holdDistance;

    public Transform holdPos;

    private bool hitObjectKnown;

    private bool holding = false;

    void Update()
    {
        RaycastHit hit;

        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out hit, holdDistance);

        if (hitSomething)
        {
            

            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.CompareTag(pickupableTag))
            {
                PickupableObject hitScript = hitObject.GetComponent<PickupableObject>();
                hitScript.beingHit = true;
                radioScript radioScript = hitObject.GetComponent<radioScript>();
                if (radioScript != null)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        radioScript.songIndex = radioScript.songIndex + 1;
                    }
                }
                //Debug.Log("Hitting object.");
            }

            else if (hitObject.CompareTag("Scarecrow"))
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                }
            }


            if (Input.GetButton("Fire1"))
            {

                if (hitSomething && !holding)
                {
                    holding = true;
                    hitObjectKnown = true;
                    if (hitObjectKnown)
                    {
                        if (hitObject.CompareTag(pickupableTag))
                        {
                            hitObject.transform.position = holdPos.position;
                            hitObject.transform.rotation = holdPos.rotation;
                            Rigidbody hitRb = hitObject.GetComponent<Rigidbody>();
                            hitObject.transform.parent = holdPos.transform;
                            hitRb.isKinematic = true;
                            hitRb.velocity = Vector3.zero;
                            hitRb.angularVelocity = Vector3.zero;
                        }
                    }
                    //Debug.Log(hitObject.transform.position.y);
                }
            }
            else if (!Input.GetButton("Fire1"))
            {
                holding = false;
                if (hitSomething)
                {
                    if (hitObject.CompareTag(pickupableTag))
                    {
                        PickupableObject hitScript = hitObject.GetComponent<PickupableObject>();
                        hitScript.beingHit = true;
                        hitObject.GetComponent<Rigidbody>().isKinematic = false;
                        hitObject.transform.parent = null;
                        
                    }
                }
            }
            //hitRenderer.material.color = Color.white;
        }
        
    }
}
