using System;

namespace ConsoleAppDotnetCore.RepositoryPatternDemo
{
    // Simple User entity used by the repository demo
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Name} <{Email}> ({Id})";
        }
    }
}
