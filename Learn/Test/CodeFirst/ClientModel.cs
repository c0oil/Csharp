﻿using System;
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

        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }

        public bool IsPensioner { get; set; }
        public bool IsReservist { get; set; }

        public double? MonthlyIncome { get; set; }

        [Required, ForeignKey("Passport")]
        public int PassportId { get; set; }
        [Required, ForeignKey("Residense")]
        public int ResidenseId { get; set; }
        [Required, ForeignKey("Disability")]
        public int DisabilityId { get; set; }
        [Required, ForeignKey("Nationality")]
        public int NationalityId { get; set; }
        [Required, ForeignKey("FamilyStatus")]
        public int FamilyStatusId { get; set; }
        [Required, ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        
        public virtual Passport Passport { get; set; }
        public virtual Place Residense { get; set; }
        public virtual Disability Disability { get; set; }
        public virtual Nationality Nationality { get; set; }
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
        public string PassportNumber { get; set; }
        [Required]
        public string IdentNumber { get; set; }

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
        }
        
        public int PlaceId { get; set; }
        [Required]
        public string Adress { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        [Required]
        public virtual City City { get; set; }
        public virtual List<Client> Residenses { get; set; }
    }

    public class City : IEntityList
    {
        public City()
        {
            Places = new List<Place>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Place> Places { get; set; }
    }

    public class Currency : BaseEntity { }

    public class Disability : BaseEntity { }

    public class Nationality : BaseEntity { }

    public class FamilyStatus : BaseEntity { }

    public class BaseEntity : IEntityList
    {
        public BaseEntity()
        {
            Clients = new List<Client>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual List<Client> Clients { get; set; }
    }

    public interface IEntityList
    {
        int Id { get; }
        string Name { get; set; }
    }

    public enum Sex
    {
        Male = 1,
        Female = 2,
    }

    public static class ClientExtension
    {
        public static void CopyTo(this Client orig, Client copy)
        {
            copy.Surname = orig.Surname;
            copy.Name = orig.Name;
            copy.MiddleName = orig.MiddleName;
            copy.BirthDate = orig.BirthDate;
            copy.BirthPlace = orig.BirthPlace;
            copy.Sex = orig.Sex;

            copy.HomePhone = orig.HomePhone;
            copy.MobilePhone = orig.MobilePhone;
            copy.Email = orig.Email;

            copy.Passport = orig.Passport;
            copy.Residense = orig.Residense;

            copy.IsPensioner = orig.IsPensioner;
            copy.IsReservist = orig.IsReservist;
            copy.MonthlyIncome = orig.MonthlyIncome;

            copy.Disability = orig.Disability;
            copy.Currency = orig.Currency;
            copy.FamilyStatus = orig.FamilyStatus;
            copy.Nationality = orig.Nationality;
            copy.FamilyStatus = orig.FamilyStatus;
        }

        public static bool NotValid(this IEntityList entity)
        {
            return entity == null ||
                   string.IsNullOrWhiteSpace(entity.Name);
        }

        public static bool NotValid(this Place entity)
        {
            return entity == null ||
                   string.IsNullOrWhiteSpace(entity.Adress);
        }

        public static bool NotValid(this Passport entity)
        {
            return entity ==null ||
                   string.IsNullOrWhiteSpace(entity.IdentNumber) ||
                   string.IsNullOrWhiteSpace(entity.IssuedBy) ||
                   string.IsNullOrWhiteSpace(entity.PassportNumber) ||
                   string.IsNullOrWhiteSpace(entity.PassportSeries);
        }

        public static bool NotValid(this Client entity)
        {
            return string.IsNullOrWhiteSpace(entity.Surname) ||
                   string.IsNullOrWhiteSpace(entity.Name) ||
                   string.IsNullOrWhiteSpace(entity.MiddleName) ||
                   string.IsNullOrWhiteSpace(entity.BirthPlace) ||

                   entity.Residense.NotValid() ||

                   entity.Nationality.NotValid() ||
                   entity.FamilyStatus.NotValid() ||
                   entity.Disability.NotValid() ||

                   entity.Passport.NotValid();
        }
    }
}
