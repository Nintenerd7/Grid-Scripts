using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
//width and height variables are used to determine the size of the grid
    public int width;
    public int height;
    public GameObject Grid_Slot;
    public GameObject IconSlots;
    public Interactable[] Icon;//array of different pictures
    public Interactable[,] AllIcons;// for all icons

    public float Gem_Move_Speed;
    // Start is called before the first frame update
    void Start()
    {
      AllIcons = new Interactable[width,height];
        Setup();//generates grid
    }

    // Update is called once per frame
   private void Setup()
    {
        for(int x = 0; x <width; x++)
        {
         for(int y= 0; y <height; y++)
         {
          Vector2 pos = new Vector2(x,y);//spawns grid boarder
          GameObject Background = Instantiate(Grid_Slot,pos,Quaternion.identity);//spawns slots
          Background.transform.parent = transform;//sets spawned objects to parent
          Background.name = "Tile-"+ pos.x + ", "+ pos.y;
          ///interactable array
          int IconSpawn = Random.Range(0, Icon.Length);//randomizes Icon spawn position 
          Spawn_Icon(new Vector2Int(x,y), Icon[IconSpawn]);
        }
        }
    }

    private void Spawn_Icon(Vector2Int pos, Interactable Spawn)
    {
      //logic here
      Interactable Image = Instantiate(Spawn, new Vector3 (pos.x, pos.y, 0f), Quaternion.identity);
      Image.transform.parent = IconSlots.transform;
      Image.name = "ICON-"+ pos.x + ", "+ pos.y;
      AllIcons[pos.x, pos.y] = Image;//what we are currently looking at 
      
      Image.Icon_Creation(pos, this);
    }
}
