using Main.Scripts.ApplicationCore.RealtimeModels;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Main.Scripts.ApplicationCore.Views
{
    public class TimelineView : MonoBehaviour
    {
        [SerializeField] private TimelineDataSender timelineDataSender;
        public bool repeat;

        private PlayableDirector[] _playableDirectors;
        private double _maxTime;
        private const float ErrorDelta = 0.5f;
        
   private void Start()
        {
            GetSceneData();
        }

        private void GetSceneData()
        {
            _playableDirectors = FindObjectsOfType<PlayableDirector>();
            
            foreach (var playableDirector in _playableDirectors)
            {
                if (playableDirector.duration > _maxTime) _maxTime = playableDirector.duration;
            }
        }

        private void Update()
        {
            UpdatePlayableDirectors();
        }

        private void UpdatePlayableDirectors()
        {
            foreach (var playableDirector in _playableDirectors)
            {
                if (playableDirector != null)
                {
                    if (repeat)
                    {
                        if (timelineDataSender.Time >= _maxTime && timelineDataSender.Time >= playableDirector.duration)
                        {
                            playableDirector.Pause();
                            playableDirector.time = 0;
                            playableDirector.Play();
                            timelineDataSender.Play();
                            return;
                        }
                    }

                    if (timelineDataSender.Time < playableDirector.duration)
                    {
                        if (Mathf.Abs((float)(playableDirector.time - timelineDataSender.Time)) > ErrorDelta)
                        {
                            playableDirector.Pause();
                            playableDirector.time = timelineDataSender.Time;
                            playableDirector.Play();
                        }
                    }
                    else
                    {
                        playableDirector.Pause();
                        playableDirector.time = 0;
                    }
                }
                else
                {
                    GetSceneData();
                }
            }
        }
        
        public void Play()
        {
            timelineDataSender.Play();
        }
    }
}
