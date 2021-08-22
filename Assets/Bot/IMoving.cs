using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoving
{
    void MoveAt(Vector2 point);
    void Stop();
}