using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects.LevelControllers.POIs
{
    public class MusicSetter : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _clips;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            SetRandomTrack();
        }

        private void SetRandomTrack()
        {
            if(_clips.Length<=0) return;

            var clip = _clips[Random.Range(0, _clips.Length - 1)];
            StartCoroutine(WaitTillEndOfTrack(clip.length));
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private IEnumerator WaitTillEndOfTrack(float _currentClipDuration)
        {
            yield return new WaitForSeconds(_currentClipDuration);
            SetRandomTrack();
        }
    
    
    }
}
