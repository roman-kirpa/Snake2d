using System;
using System.Collections;
using UnityEngine;

public class KeyboardScript : MonoBehaviour
{
    public int SnakeLenght;
    private Coordinate2d[] _snakeCoordinate;
    private GameObject[] _snake;
    public float Speed;
    public float RangeSpeed = 1.0f;
    private WayEnum _way;
    public GameObject cube;
    
    void Start()
    {
        _snakeCoordinate = new Coordinate2d[SnakeLenght];
        _snake = new GameObject[SnakeLenght];
        _way = GetRandomWay();
        _snake[0] = Instantiate(cube, new Vector2(0, 0), Quaternion.identity);
        _snakeCoordinate[0] = new Coordinate2d { CoordinateX = _snake[0].transform.position.x, CoordinateY = _snake[0].transform.position.y };
        StartCoroutine(UpdateSnakeCoordinate());
    }

    void Update()
    {
        if (_snake[0].GetComponent<ForCube>().Triggered)
        {
            if (_snake[_snake.Length - 1] == null)
            {
                for (var i = 0; i < _snake.Length; i++)
                {
                    if (_snake[i] == null)
                    {
                        _snakeCoordinate[i] = new Coordinate2d { CoordinateX = _snakeCoordinate[i - 1].CoordinateX, CoordinateY = _snakeCoordinate[i - 1].CoordinateY };
                        var duplicate = Instantiate(cube, new Vector2(_snakeCoordinate[i].CoordinateX, _snakeCoordinate[i].CoordinateY), Quaternion.identity);
                        _snake[i] = duplicate;
                        _snake[0].GetComponent<ForCube>().Triggered = false;
                        break;
                    }
                }
            }
        }
        ListenKey();
    }

    private IEnumerator UpdateSnakeCoordinate()
    {
        while (true)
        {
            Move(_way);
            for (var i = 0; i < _snake.Length; i++)
            {
                if (_snake[i] != null)
                {
                    _snake[i].transform.position = new Vector2(_snakeCoordinate[i].CoordinateX, _snakeCoordinate[i].CoordinateY);
                }
            }
            yield return new WaitForSeconds(Speed);
        }

    }

    private void Move(WayEnum way)
    {
        var oldX = _snakeCoordinate[0].CoordinateX;
        var oldY = _snakeCoordinate[0].CoordinateY;
        switch (way)
        {
            case WayEnum.Up:
                {
                    _snakeCoordinate[0].CoordinateY += RangeSpeed;
                    break;
                }
            case WayEnum.Down:
                {
                    _snakeCoordinate[0].CoordinateY -= RangeSpeed;
                    break;
                }
            case WayEnum.Right:
                {
                    _snakeCoordinate[0].CoordinateX += RangeSpeed;
                    break;
                }
            case WayEnum.Left:
                {
                    _snakeCoordinate[0].CoordinateX -= RangeSpeed;
                    break;
                }
        }
        for (var i = 1; i < _snake.Length; i++)
        {
            if (_snake[i] == null)
            {
                return;
            }
            var thisX = _snakeCoordinate[i].CoordinateX;
            var thisY = _snakeCoordinate[i].CoordinateY;
            _snakeCoordinate[i].CoordinateX = oldX;
            _snakeCoordinate[i].CoordinateY = oldY;
            oldX = thisX;
            oldY = thisY;
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
