
namespace Prodcuts.Domain.Interfaces;


public interface ISoftDeletable
{
    public bool IsDeleted {get;}
    public DateTime? DeletedAt {get; }
    public void SoftDelete();
    public void  RevertDelete();
}