using Newtonsoft.Json;


namespace APIDEMO_.Models
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; } = Guid.NewGuid();
        public string Role { get; set; }
        //public string Description { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public bool Active { get; set; } = true;

        /// <summary>
        /// Se establece virtual ya que no se incluira en tdo momento
        /// </summary>
        public virtual User User { get; set; }
    }
}
