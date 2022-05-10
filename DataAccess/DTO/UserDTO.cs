namespace DataAccess.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsBanned { get;set; }
    }
}
