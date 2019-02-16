using Microsoft.WindowsAzure.Storage.Table;


namespace RetroGamesGo.Service.Entities
{
    public class UserEntity : TableEntity
    {
        /// <summary>
        /// User Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// CI
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Cell Phone
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public UserEntity() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UserEntity(string email)
        {
            this.Email = email;
            this.RowKey = email;
            this.PartitionKey = "Users";
        }
    }
}
