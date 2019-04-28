using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TheMazeGame.Enums;

namespace TheMazeGame.Models.GameObjects
{
    [KnownType(typeof(GameObject))]
    [DataContract]
    public class Cell : GameObject
    {
        [DataMember]
        public FieldTypes FieldType { get; set; }
    }
}
