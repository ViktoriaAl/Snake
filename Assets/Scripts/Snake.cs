using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments;

    [SerializeField] private Transform _segmentPrefab;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int count = 0;
    public static int bestResult;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);

        //if (PlayerPrefs.HasKey("best"))
        //    bestResult = PlayerPrefs.GetInt("best");
        //else
        //    bestResult = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S))
            _direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A))
            _direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D))
            _direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this._segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
            Destroy(_segments[i].gameObject);

        _segments.Clear();
        _segments.Add(this.transform);

        count = 0;
        _scoreText.text = count.ToString();

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
            count += 1;
            _scoreText.text = count.ToString();
            if (count > bestResult)
            {
                bestResult = count;
                PlayerPrefs.SetInt("best", count);
            }
        }
        else if (collision.tag == "Obstacle")
            ResetState();
    }
}
