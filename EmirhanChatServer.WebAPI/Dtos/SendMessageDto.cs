namespace EmirhanChatServer.WebAPI.Dtos
{
    public sealed record SendMessageDto(
        Guid userId,
        Guid ToUserId,
        string message);
    
    
}
