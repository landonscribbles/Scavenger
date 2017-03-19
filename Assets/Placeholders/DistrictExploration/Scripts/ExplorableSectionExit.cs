using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorableSectionExit : MonoBehaviour {

        public enum ExitDirection { north, west, south, east};

        [SerializeField]
        private ExitDirection exitOrientation;
        public ExitDirection ExitOrientation {
            get {
                return exitOrientation;
            }
            set {
                exitOrientation = value;
            }
        }

        [SerializeField]
        private ExplorableSection.RoomType[] connectableRoomTypes;
        public ExplorableSection.RoomType[] ConnectableRoomTypes {
            get {
                return connectableRoomTypes;
            }
        }
}
