using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Fish : MonoBehaviour
{
    [SerializeField] GameObject _npc_fish;
    [SerializeField] float _fish_spawn_delay;
    public static int _fish_max = 25;
    float _fish_spawn = 0, _spawn_height;
    

    private void Awake()
    {
        NPC_Fish.LoadFish();
    }
    // Start is called before the first frame update
    void Start()
    {
        _spawn_height = Camera.main.orthographicSize * 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn one fish every X seconds, up to a maximum
        if(_fish_max > 0)
            _fish_spawn -= Time.deltaTime;

        if(_fish_spawn <= 0)
        {
            //Fixed absolute X value, random Y value within range and constant Z value
            Vector3 startPoint = new Vector3(Camera.main.orthographicSize * (Random.Range(0, 2) % 2 == 1 ? 2 : -2),
                Random.Range(_spawn_height, -_spawn_height),
                0);

            Instantiate(_npc_fish,startPoint , Quaternion.identity);

            _fish_spawn += _fish_spawn_delay;
        }

    }
}
