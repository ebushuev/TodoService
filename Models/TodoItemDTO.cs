using System.Runtime.Serialization;

namespace TodoApi.Models
{
    #region snippet
    [DataContract]
    public class TodoItemDTO
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsComplete { get; set; }
    }
    #endregion
}
