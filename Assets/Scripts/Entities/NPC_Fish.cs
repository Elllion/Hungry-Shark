using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Fish : MonoBehaviour
{
    //Global texture references
    public static Sprite[] _fishSprites = new Sprite[4];

    //Movement variables
    [SerializeField] private float _speed = 1.5f;
    Vector3 _moveDirection = Vector2.zero;
    float _time = 0;

    //State variables
    private int _fishType;

    // Start is called before the first frame update
    void Start()
    {
        Spawner_Fish._fish_max--;

        //Assign a random type when created
        _fishType = Random.Range(0, _fishSprites.Length);
        GetComponent<SpriteRenderer>().sprite = _fishSprites[_fishType];

        //Set the fish's facing and movement direction
        if (transform.position.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f,0.66f, 0.66f);
            _moveDirection = Vector3.right * _speed;
        }
        else
            _moveDirection = Vector3.left * _speed;


    }

    // Update is called once per frame
    void Update()
    {
        //Move fish forwards with slight up/down waving
        _time += Time.deltaTime;
        transform.position += (_moveDirection + (Vector3.up * Mathf.Sin(_time))) * Time.deltaTime;

        //Remove the fish once it moves far enough offscreen
        if (Mathf.Abs(transform.position.x) > 20)
            ClearFish();
    }

    public int GetFishType()
    {
        return _fishType;
    }

    public void ClearFish()
    {
        Spawner_Fish._fish_max++;
        Destroy(this.gameObject);
    }

    //Called to load the fish sprites
    public static void LoadFish()
    {
        _fishSprites = Resources.LoadAll<Sprite>("Sprites/Fish");
    }
}
