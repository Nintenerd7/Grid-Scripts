using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    public Vector2Int posIndex;
    
    public Board Grid;
    
    
    //first touch position 
    private Vector2 First_Touch_Pos;//starting position 
    private Vector2 Final_Touch_Pos;//final position

    private bool isClicked;//has the player clicked the Icon 
    private float Swipe_Angle;//calculates the direction of the swipe
    // Start is called before the first frame update
    private Interactable OtherIcon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if(isClicked || Input.GetMouseButtonDown(0))
        {
         isClicked = false;
          
          Final_Touch_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          Calculate_Angle();
        }
    }

 #region ICON ANGLE LOGIC
    public void Icon_Creation(Vector2Int pos, Board The_Board)
    {
      posIndex = pos;
      Grid = The_Board;
    }

    private void OnMouseDown()
    {
      First_Touch_Pos = Camera.main.ScreenToWorldPoint( Input.mousePosition);//click on the gems within the grid 
      isClicked = true;//click is true
    }

    private void Calculate_Angle()
    {
      Swipe_Angle = Mathf.Atan2(Final_Touch_Pos.y - First_Touch_Pos.y, Final_Touch_Pos.x - First_Touch_Pos.x);//creates x and y swipe angle
      Swipe_Angle = Swipe_Angle *180f/Mathf.PI;//calculates direction
      Debug.Log(Swipe_Angle);//shows co-ordinates in the console log 
      
      if(Vector3.Distance(First_Touch_Pos, Final_Touch_Pos) > .5f)
      {
        //if pressed and moving more than .5 call this method
        Move_Item();
      }
      
    }
#endregion


    void Move_Item()
    {
      Swipe();
      Grid.AllIcons[posIndex.x,posIndex.y] = this;//the Icon already clicked
      Grid.AllIcons[OtherIcon.posIndex.x,OtherIcon.posIndex.y] = OtherIcon;//the Icon that is being swapped
    }


#region SWAP AXIS
    void Swipe()
    {
      //SWIPE RIGHT
      if(Swipe_Angle < 45f && Swipe_Angle > -45f && posIndex.x < Grid.width - 1)
      {
        OtherIcon = Grid.AllIcons[posIndex.x + 1, posIndex.y];
        OtherIcon.posIndex.x--;
        OtherIcon.posIndex.x++;
      }
          else if( Swipe_Angle > 45 &&  Swipe_Angle <= 135 && posIndex.y < Grid.height-1)
        {
            OtherIcon = Grid.AllIcons[posIndex.x , posIndex.y + 1];
            OtherIcon.posIndex.y--;
           OtherIcon.posIndex.y++;
        } 
        //swipe down
        else if( Swipe_Angle < -45 &&  Swipe_Angle >= -135 && posIndex.y > 0)
        {
            OtherIcon = Grid.AllIcons[posIndex.x , posIndex.y - 1];
           OtherIcon.posIndex.y++;
            OtherIcon.posIndex.y--;
        } 
        // swipe left
        else if( Swipe_Angle > 135f &&  Swipe_Angle < -135 && posIndex.x > 0)
        {
        OtherIcon = Grid.AllIcons[posIndex.x - 1, posIndex.y];
        OtherIcon.posIndex.x++;
        OtherIcon.posIndex.x--;
        }
      
    }
    #endregion
}
