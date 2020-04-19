﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Directions 
{
    public static Vector2[] AllDirections = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    public static Vector2[] RecursiveDirections = { Vector2.down, Vector2.left};
    public static Vector2[] ForwardDirections = { Vector2.up, Vector2.right };
    public static Vector2[] RightDirection = { Vector2.right };
    public static Vector2[] UpDirection = { Vector2.up };
}
