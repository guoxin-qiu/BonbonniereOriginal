namespace Bonbonniere.Core.Models
{
    public interface IRegisterRepository
    {
        Register Get(int id);
        void Save(Register model);
    }
}
