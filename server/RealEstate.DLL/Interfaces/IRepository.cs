namespace RealEstate.DLL.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(string name);
		Task<T> GetAsync(int id);
		Task CreateAsync(T item);
		void Update(T item);	
		Task DeleteAsync(int id);
	}
}
