namespace Sattelite.EntityFramework.Security
{
    using System;
    using Sattelite.Entities;

    public class UserInfo
    {
        public int UserId { get; set; }

        /// <summary>
        /// Get or set UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get or set Roles Group
        /// </summary>
        public int GroupId { get; set; }

        public string DisplayName { get; set; }


        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string Token { get; set; }

        public UserInfo()
        {
            this.UserId = -1;
            this.UserName = string.Empty;
            this.DisplayName = string.Empty;
            this.Email = string.Empty;
            this.DateCreated = DateTime.Now;
            this.LastLoginTime = DateTime.Now;
            this.GroupId = -1;
        }

        public UserInfo(User user): this()
        {
            this.UserName = user.UserName;
            this.DisplayName = user.DisplayName;
            this.Email = user.Email;
        }
    }
}