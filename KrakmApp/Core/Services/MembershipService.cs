using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Services
{
    public class MembershipService : IMembershipService
    {
        IRoleRepository _roleRepository;
        IUserRepository _userRepository;
        IUserRoleRepository _userRoleRepository;
        IEncryptionService _encryptionService;

        public MembershipService(
            IRoleRepository role,
            IUserRepository user,
            IUserRoleRepository userRole,
            IEncryptionService encyption)
        {
            _roleRepository = role;
            _userRepository = user;
            _userRoleRepository = userRole;
            _encryptionService = encyption;
        }

        public User CreateUser(string username, string email, string password, int[] roles)
        {
            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();
            var user = new User()
            {
                Name = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now
            };

            _userRepository.Add(user);

            _userRepository.Commit();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    AddUserToRole(user, role);
                }
            }

            _userRepository.Commit();

            return user;
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public List<Role> GetUserRoles(string username)
        {
            List<Role> _result = new List<Role>();

            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    _result.Add(userRole.Role);
                }
            }

            return _result.Distinct().ToList();
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _userRepository.GetSingleByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Name);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Name);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }

        private bool IsUserValid(User user, string password)
        {
            if (IsPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(
                _encryptionService.EncryptPassword(password, user.Salt), 
                user.HashedPassword);
        }

        private void AddUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
                throw new Exception("Role doesn't exist.");

            var userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            _userRoleRepository.Add(userRole);

            _userRepository.Commit();
        }
    }
}
