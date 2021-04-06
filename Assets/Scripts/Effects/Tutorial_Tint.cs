using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Tint : MonoBehaviour
{
    private Material _mat;
    private Color _col;

    bool _gameStart = false;
    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<SpriteRenderer>().material;
        _col = _mat.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        //Fade out the tutorial prompt
        if (_gameStart)
        {
            _col.a = Mathf.Max(_col.a - (Time.deltaTime / 4), 0);

            _mat.SetColor("_Color", _col);

            gameObject.SetActive(_col.a > 0);
        }

        //Only fade out once the shark starts moving
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            _gameStart = true;
    }
}
