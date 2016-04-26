namespace Flavio.SSO.Core.Infra.Data.Interfaces
{
	public interface IUnitOfWork
	{
		void BeginTransaction();
		void Commit();
	}
}
