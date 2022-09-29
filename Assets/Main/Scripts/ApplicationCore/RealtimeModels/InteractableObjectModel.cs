using Normal.Realtime;
using Normal.Realtime.Serialization;

namespace Main.Scripts.ApplicationCore.RealtimeModels
{
    [RealtimeModel]
    public partial class InteractableObjectModel
    {
        [RealtimeProperty(1, true )] private bool _isGrabbed;
        [RealtimeProperty(2, true )] private float _timeAfterRelease;
    }
}

/* ----- Begin Normal Autogenerated Code ----- */
namespace Main.Scripts.ApplicationCore.RealtimeModels {
    public partial class InteractableObjectModel : RealtimeModel {
        public bool isGrabbed {
            get {
                return _isGrabbedProperty.value;
            }
            set {
                if (_isGrabbedProperty.value == value) return;
                _isGrabbedProperty.value = value;
                InvalidateReliableLength();
            }
        }
        
        public float timeAfterRelease {
            get {
                return _timeAfterReleaseProperty.value;
            }
            set {
                if (_timeAfterReleaseProperty.value == value) return;
                _timeAfterReleaseProperty.value = value;
                InvalidateReliableLength();
            }
        }
        
        public enum PropertyID : uint {
            IsGrabbed = 1,
            TimeAfterRelease = 2,
        }
        
        #region Properties
        
        private ReliableProperty<bool> _isGrabbedProperty;
        
        private ReliableProperty<float> _timeAfterReleaseProperty;
        
        #endregion
        
        public InteractableObjectModel() : base(null) {
            _isGrabbedProperty = new ReliableProperty<bool>(1, _isGrabbed);
            _timeAfterReleaseProperty = new ReliableProperty<float>(2, _timeAfterRelease);
        }
        
        protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
            _isGrabbedProperty.UnsubscribeCallback();
            _timeAfterReleaseProperty.UnsubscribeCallback();
        }
        
        protected override int WriteLength(StreamContext context) {
            var length = 0;
            length += _isGrabbedProperty.WriteLength(context);
            length += _timeAfterReleaseProperty.WriteLength(context);
            return length;
        }
        
        protected override void Write(WriteStream stream, StreamContext context) {
            var writes = false;
            writes |= _isGrabbedProperty.Write(stream, context);
            writes |= _timeAfterReleaseProperty.Write(stream, context);
            if (writes) InvalidateContextLength(context);
        }
        
        protected override void Read(ReadStream stream, StreamContext context) {
            var anyPropertiesChanged = false;
            while (stream.ReadNextPropertyID(out uint propertyID)) {
                var changed = false;
                switch (propertyID) {
                    case (uint) PropertyID.IsGrabbed: {
                        changed = _isGrabbedProperty.Read(stream, context);
                        break;
                    }
                    case (uint) PropertyID.TimeAfterRelease: {
                        changed = _timeAfterReleaseProperty.Read(stream, context);
                        break;
                    }
                    default: {
                        stream.SkipProperty();
                        break;
                    }
                }
                anyPropertiesChanged |= changed;
            }
            if (anyPropertiesChanged) {
                UpdateBackingFields();
            }
        }
        
        private void UpdateBackingFields() {
            _isGrabbed = isGrabbed;
            _timeAfterRelease = timeAfterRelease;
        }
        
    }
}
/* ----- End Normal Autogenerated Code ----- */
