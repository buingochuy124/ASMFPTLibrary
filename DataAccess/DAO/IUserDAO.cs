using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IUserDAO
    {
        int User_Login(string UserAccount, string UserPassword);

        int User_Register(string UserAccount, string UserPassword, string UserFullName, string UserAddress, string UserPhoneNumber, int RoleID);

        List<UserDTO> Users_GetList();

        int User_Update(string UserAccount, string UserPassword, string UserFullName, string UserAddress, string UserPhoneNumber);

        bool User_CheckBan(int UserID);

        int User_Ban(int UserID);
        int User_UnBan(int UserID);

        int User_CheckExist(string UserAccount);

    }
}
