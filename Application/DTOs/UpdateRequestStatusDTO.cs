namespace Application.DTOs;

public class UpdateRequestStatusDTO
{
    public Guid RequestId { get; set; }
    public int Status { get; set; }
    public Guid UserId { get; set; }
}