namespace user_management_api.Repositories
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
