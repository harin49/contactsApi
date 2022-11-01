namespace contactsApi.Models;

public class UpdateContactRequest
{
    public string FullName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public long PhoneNumber { get; set; }
    public string Address { get; set; } = string.Empty;
}