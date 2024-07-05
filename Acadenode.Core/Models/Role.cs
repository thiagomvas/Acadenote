namespace Acadenode.Core.Models
{
    [Flags]
    public enum Role
    {
        Guest = 0,
        User = 1,
        Writer = 1 << 1,
        Admin = 1 << 10,
    }

    public static class Roles
    {
        public static Role SuperAdmin = Role.Admin | Role.Writer | Role.User;
        public static Role Admin = Role.Admin | Role.User;
        public static Role Writer = Role.Writer | Role.User;
        public static Role User = Role.User;

        public static IEnumerable<Role> GetRoles(this Role role)
        {
            foreach (Role r in Enum.GetValues(typeof(Role)))
            {
                if (role.HasFlag(r))
                {
                    yield return r;
                }
            }
        }
        public static bool HasRoles(this Role role, params Role[] roles)
        {
            foreach (var r in roles)
            {
                if (!role.HasFlag(r))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool HasAnyRole(this Role role, params Role[] roles)
        {
            foreach (var r in roles)
            {
                if (role.HasFlag(r))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
