using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        public string BirthPlace { get; set; }
        public Sex Sex { get; set; }

        // can empty
        public string HomePhone { get; set; } //mask
        public string MobilePhone { get; set; } //mask
        public string Email { get; set; }

        public bool IsPensioner { get; set; }
        public bool IsReservist { get; set; }

        public double? MonthlyIncome { get; set; }

        [Required]
        public virtual Passport Passport { get; set; }
        [InverseProperty("Residenses")]
        public virtual Place Registration { get; set; }
        [InverseProperty("Registrations")]
        public virtual Place Residense { get; set; }
        [Required]
        public virtual Disability Disability { get; set; }
        [Required]
        public virtual Nationality Nationality { get; set; }
        [Required]
        public virtual FamilyStatus FamilyStatus { get; set; }
        public virtual Currency Currency { get; set; }
    }

    public class Passport
    {
        [Key, ForeignKey("Client")]
        public int PassportId { get; set; }
        [Required]
        public string PassportSeries { get; set; }
        [Required]
        public string PassportNumber { get; set; } //mask
        [Required]
        public string IdentNumber { get; set; } //mask

        [Required]
        public string IssuedBy { get; set; }
        public DateTime IssueDate { get; set; }

        public virtual Client Client { get; set; }
    }

    public class Place
    {
        public Place()
        {
            Residenses = new List<Client>();
            Registrations = new List<Client>();
        }

        public int PlaceId { get; set; }
        [Required]
        public string Adress { get; set; }

        [Required]
        public virtual City City { get; set; }
        public virtual List<Client> Residenses { get; set; }
        public virtual List<Client> Registrations { get; set; }
    }

    public class City
    {
        public City()
        {
            Places = new List<Place>();
        }

        public string CityId { get; set; }

        public virtual List<Place> Places { get; set; }
    }

    public class Currency : BaseEntity
    {
        public string CurrencyId { get; set; }
    }

    public class Disability : BaseEntity
    {
        public string DisabilityId { get; set; }
    }

    public class Nationality : BaseEntity
    {
        public string NationalityId { get; set; }
    }

    public class FamilyStatus : BaseEntity
    {
        public string FamilyStatusId { get; set; }
    }

    public class BaseEntity
    {
        public BaseEntity()
        {
            Clients = new List<Client>();
        }

        public virtual List<Client> Clients { get; set; }
    }

    public enum Sex
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2,
    }
}
