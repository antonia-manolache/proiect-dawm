using System.Diagnostics;
using System.Security.Claims;

namespace DataLayer.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }

    public List<Recipe> Recipes { get; set; }

    public string PasswordHash { get; set; }

}