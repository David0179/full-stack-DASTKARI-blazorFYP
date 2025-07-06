using System.ComponentModel.DataAnnotations;
public class ArtistApplicationModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string ArtistName { get; set; }

    [Required]
    public string ProfileDescription { get; set; }

    [StringLength(100, MinimumLength = 3)]
    public string ProfileLink { get; set; }

    public int? YearsOfExperience { get; set; }

    [Required]
    public int StatusId { get; set; }

    [Required]
    public DateTime AppliedDate { get; set; } = DateTime.Now;

    public DateTime? ProcessedDate { get; set; }

    public int? ProcessedBy { get; set; }

    public string? RejectionReason { get; set; }
    public string? WorkImage { get; set; }

    [StringLength(255)]
    public string? ArtistImageUrl { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? ModifiedDate { get; set; }

}
