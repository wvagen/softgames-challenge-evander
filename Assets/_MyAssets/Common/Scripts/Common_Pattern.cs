using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Softgames.Common
{

    public class Common_Pattern : MonoBehaviour
    {
        [SerializeField]
        private RawImage _imagePattern;
        [SerializeField] private float _x, _y;

        [SerializeField] private float _animDelay = 1f;

        private void Start()
        {
            StartCoroutine(Pattern_Animation(_animDelay));
        }

        private IEnumerator Pattern_Animation(float delay)
        {
            yield return new WaitForSeconds(delay);
            _imagePattern.uvRect = new Rect(_imagePattern.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _imagePattern.uvRect.size);
        }
    }
}