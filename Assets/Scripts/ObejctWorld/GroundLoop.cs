//-----------------------------------------------------------------------
// <summary>
// 文件名: GroundLoop
// 描述: #DESCRIPTION#
// 作者: #AUTHOR#
// 创建日期: #CREATIONDATE#
// 修改记录: #MODIFICATIONHISTORY#
// </summary>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VampireDynasty
{
	public class GroundLoop : MonoBehaviour
	{
		[SerializeField] private Transform playerTransform;
		
		private readonly List<Transform> ground = new List<Transform>();
		private Transform currentGround;
		private bool isRight;
		private bool isTop;
		
		private void Awake()
		{
			for (int i = 0; i < 4; i++)
				ground.Add(transform.GetChild(i));
			
			currentGround = ground[0];
		}

		private void Update()
		{
			isRight = playerTransform.position.x >= currentGround.position.x;
			isTop = playerTransform.position.y >= currentGround.position.y;

			UpdateGroundPosition();
		}

		private void UpdateGroundPosition()
		{
			var movableGround = ground.ToList();
			movableGround.Remove(currentGround);
			
			movableGround[0].position = currentGround.position + new Vector3(isRight ? 14 : -14, 0f, 0f);
			movableGround[1].position = currentGround.position + new Vector3(0f, isTop ? 4 : -4, 0f);
			movableGround[2].position = currentGround.position + new Vector3(isRight ? 14 : -14, isTop ? 4 : -4, 0f);
			
			var offset = currentGround.position - playerTransform.position;
			if (Mathf.Abs(offset.x) > 7)
				currentGround = movableGround[0];
			else if (Mathf.Abs(offset.y) > 2)
				currentGround = movableGround[1];
		}
	}
}