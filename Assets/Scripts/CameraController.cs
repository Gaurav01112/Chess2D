using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private Board board;
   private void Awake()
   {
      transform.position = new Vector3(board.transform.position.x, board.transform.position.y, -10);
   }
}
