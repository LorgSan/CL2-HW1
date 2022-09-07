using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CircleSpawn : MonoBehaviour
{
    [SerializeField] Sprite[] circles; //this is an array with all sprite variants
    //I must say I first tried to load it through the resources folder, but I didn't figure out how to
    //turn Object[] into Sprite[], cast wasn't working? 
    [SerializeField] Image hint; //this is the UI image that changes sprites randomly and wants you to click the same color 

    // Start is called before the first frame update
    void Start()
    {
        Spawn(); //we start right from spawning
    }

    void Update()
    {
        Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //saving the colider under the mouse if we get one
        if (col != null) //if it's not empty
        {
            SpriteRenderer colSR = col.GetComponent<SpriteRenderer>(); //we save it's sprite renderer
            if (Input.GetKeyDown(KeyCode.Mouse0) && colSR.sprite == hint.sprite) //and if we press the button and it's the right circle
            {
                Spawn(); //we restart it all
            }            
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //just a quick restart
        }
    }

    void Spawn()
    {   
        hint.sprite = circles[Random.Range(0,4)]; //we randomize the hint first
        for (int x = 0; x < circles.Length; x++) //for each element in the circles array (which is four times)
        {
            GameObject currentCircle = Instantiate(Resources.Load("Prefabs/Circle") as GameObject); //we instantiate a resourced prefab
            currentCircle.GetComponent<SpriteRenderer>().sprite = circles [x]; //setting the sprite to the element of the prefab by the index
            currentCircle.transform.position = RandomPos(); //randomizing the position
        }
    }

    Vector3 RandomPos()
    {
        Vector3 rPos = new Vector3 (Random.Range(-8.6f,8.6f), Random.Range(-4.5f, 4.5f), transform.position.z); //creating randomized vector3
        return rPos; //and returning it
    }
}
