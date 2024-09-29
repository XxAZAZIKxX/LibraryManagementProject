using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Data.Models;

[PrimaryKey(nameof(UserId))]
public class UserSalt
{
    [ForeignKey(nameof(User))] public Guid UserId { get; set; }

    public UserAccount User { get; set; }

    [Length(64, 64)] public byte[] SaltBytes { get; set; }
}