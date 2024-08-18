using System.ComponentModel.DataAnnotations;
using NHibernate.Classic;

namespace TransactionService.Persistence.Settings;

public class StorageSettings : IValidatable
{
    [Required] public string DefaultConnection { get; set; }
    public void Validate()
    {
        Validator.ValidateObject(this, new ValidationContext(this), true);//this logic will check eza el docerator are applied lal properties
    }
}