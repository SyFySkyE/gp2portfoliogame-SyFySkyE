using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerID
{
    public static byte PlayerUserID = 0; // Each player gets its own unique ID. We could use hashcodes but let's use a byte  as we'll never have more than 255 (0 isn't used) players
}
