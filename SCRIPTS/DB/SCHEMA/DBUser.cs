using System;

namespace DB.Schema.User
{
    [Serializable]

    public class DBUser
    {

        public string username;

        public string password;
        public DBUser()
        {
            username = "basic";
            password = "password";

        }

        public DBUser(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
