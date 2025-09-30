namespace Rodnie.API.Enums {
    public enum RolesEnum {
        User = 0b_0000_0001,
        Admin = 0b_0000_0010 | User
    }
}
