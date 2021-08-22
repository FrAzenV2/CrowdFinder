using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoving
{
    public void MoveAt(Vector2 point);
    public void Stop();
}