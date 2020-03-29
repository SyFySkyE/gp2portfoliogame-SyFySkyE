using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout1  {

	[System.Serializable]
	public struct rowData{
		public bool[] row;
	}

    public Grid grid;
    public rowData[] rows = new rowData[16]; //Grid of 8x8 TODO Expose this
}
