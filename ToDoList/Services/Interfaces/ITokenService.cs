namespace ToDoList.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwtToken(string userId);
    }
}
