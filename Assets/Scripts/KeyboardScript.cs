using System;
using System.Collections;
using UnityEngine;

public class KeyboardScript : MonoBehaviour
{
    private BoxCollider2D cube;
    public float Speed = 0.5f;
    public float RangeSpeed = 1.0f;
    private WayEnum _way;

    void Start()
    {
        _way = GetRandomWay();
        cube = GetComponent<BoxCollider2D>();
        StartCoroutine(Move());
    }

    void Update()
    {
        ListenKey();
    }

    private IEnumerator Move()
    {
        while (true)
        {
            switch (_way)
            {
                case WayEnum.Up:
                    {
                        cube.transform.position = new Vector2(cube.transform.position.x, cube.transform.position.y + RangeSpeed);
                        break;
                    }
                case WayEnum.Down:
                    {
                        cube.transform.position = new Vector2(cube.transform.position.x, cube.transform.position.y - RangeSpeed);
                        break;
                    }
                case WayEnum.Left:
                    {
                        cube.transform.position = new Vector2(cube.transform.position.x - RangeSpeed, cube.transform.position.y);
                        break;
                    }
                case WayEnum.Right:
                    {
                        cube.transform.position = new Vector2(cube.transform.position.x + RangeSpeed, cube.transform.position.y);
                        break;
                    }
            }
            yield return new WaitForSeconds(Speed);
        }
        
    }

    private void ListenKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _way = _way != WayEnum.Down ? WayEnum.Up : _way;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _way = _way != WayEnum.Up ? WayEnum.Down : _way;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _way = _way != WayEnum.Right ? WayEnum.Left : _way;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _way = _way != WayEnum.Left ? WayEnum.Right : _way;
        }
    }

    private WayEnum GetRandomWay()
    {
        var ways = Enum.GetValues(typeof(WayEnum));
        var random = new System.Random();
        var randomWay = (WayEnum)ways.GetValue(random.Next(ways.Length));
        return randomWay;
    }
}
