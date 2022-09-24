using Main.Scripts.ApplicationCore.Views;
using Normal.Realtime;
using UnityEngine;

namespace Main.Scripts.ApplicationCore.Controllers
{
    public class TimelineController : BaseController
    {
        [SerializeField] private TimelineView timelineView;
        
        private TimelineView _timelineView;
        
        public void Init()
        {
            _timelineView = FindObjectOfType<TimelineView>();

            if (_timelineView == null)
            {
                var options = new Realtime.InstantiateOptions
                {
                    destroyWhenOwnerLeaves = false,
                    destroyWhenLastClientLeaves = false
                };

                _timelineView = Realtime
                    .Instantiate(timelineView.gameObject.name, options)
                    .GetComponent<TimelineView>();
                
                _timelineView.Play();
            }
            
        }
    }
}
