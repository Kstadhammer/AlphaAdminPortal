using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class EditProjectForm
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Project name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Client name is required")]
    [MaxLength(100)]
    public string ClientName { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public decimal Budget { get; set; }

    public List<string>? MemberIds { get; set; }

    public bool IsActive { get; set; }
}
