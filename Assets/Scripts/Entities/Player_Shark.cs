using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Shark : MonoBehaviour
{
    //State variables
    [SerializeField] SpriteRenderer _fishThought;
    [SerializeField] GameObject[] _bubbles;
    bool _gameOver = false;
    int _fish_desired;

    //Score reference
    [SerializeField] Scoreboard _scoreboard;

    //Movement variables
    [SerializeField] float _acceleration;
    private Rigidbody2D _rgd_self;
    Vector3 _inputVelocity;

    //Animation variables
    [SerializeField] Sprite[] img_animationSprites = new Sprite[2];
    [SerializeField] float _animation_speed = 0;
    private bool _animation_firstFrame = false;
    private SpriteRenderer _spr_self;
    private float _animation_timer = 0;
    private float _angle;

    //Collider
    Collider2D _col_self;
    private void Awake()
    {
        _spr_self = GetComponent<SpriteRenderer>();
        _rgd_self = GetComponent<Rigidbody2D>();
        _col_self = GetComponent<BoxCollider2D>();

        _animation_speed = Mathf.Max(_animation_speed, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeDesiredFish();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        //Float upwards upside-down if the shark is sick
        if (_gameOver)
        {
            transform.position += Vector3.up * 0.75f * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(0,0,180), 0.25f);

            if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("SampleScene");
            return;
        }


        //Get input and add movement
        _inputVelocity.x = Input.GetAxis("Horizontal");
        _inputVelocity.y = Input.GetAxis("Vertical");
        _rgd_self.AddForce(_inputVelocity * _acceleration * Time.deltaTime);

        //Determine where the shark should be facing
        _angle = -Vector2.SignedAngle(_rgd_self.velocity, Vector2.left);
        transform.localScale = new Vector3(1,Mathf.Abs(_angle) > 90? -1: 1,1);
        transform.rotation = Quaternion.Euler(0,0,_angle);


        //Animation
        //Swap between two sprites at fixed intervals
        _animation_timer -= Time.deltaTime;
        if(_animation_timer <= 0)
        {
            _animation_firstFrame = !_animation_firstFrame;
            _animation_timer += 1 / _animation_speed;

            _spr_self.sprite = img_animationSprites[_animation_firstFrame ? 0: 1];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if shark collided with fish
        if (collision.tag == "Fish")
        {
            NPC_Fish fishRef = collision.GetComponent<NPC_Fish>();

            //Add point if correct fish was eaten
            if (fishRef.GetFishType() == _fish_desired)
            {
                _scoreboard.AddScore();
                ChangeDesiredFish();              
            }
            else//Otherwise end the game
            {
                _gameOver = true;

                transform.localScale = Vector3.one;

                foreach (GameObject g in _bubbles)
                    g.SetActive(false);

                _scoreboard.DisplayEndScreen();

                _col_self.enabled = false;
            }

            //Remove the fish
            fishRef.ClearFish();
        }
    }

    private void ChangeDesiredFish()
    {
        _fish_desired = Random.Range(0, NPC_Fish._fishSprites.Length);
        _fishThought.sprite = NPC_Fish._fishSprites[_fish_desired];
    }
}
