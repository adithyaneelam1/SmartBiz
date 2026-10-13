using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AdharshaHighSchool.Models
{
    // 1. ADMIN
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }
    }


    // 2. SCHOOL INFORMATION
    [Table("SchoolInfo")]
    
    public class SchoolInfo
    {
        [Key]
        public int SchoolId { get; set; }

        [Required]
        public string SchoolName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AboutUs { get; set; }

        public string Vision { get; set; }

        public string Mission { get; set; }

        public string PrincipalName { get; set; }

        public string PrincipalMessage { get; set; }
    }


    // 3. ACADEMICS
    [Table("Academics")]
    public class Academic
    {
        [Key]
        public int AcademicId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }


    // 4. FACILITIES
    [Table("Facilities")]
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        [Required]
        public string FacilityName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }


    // 5. GALLERY
    [Table("Gallery")]
    public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }

        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
    }


    // 6. CONTACT MESSAGE
    [Table("ContactMessages")]
    public class ContactMessage
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsRead { get; set; }
    }


    // 7. CONTACT VIEW MODEL
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter your message")]
        public string Message { get; set; }
    }


    // 8. DATABASE CONTEXT
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("AdharshaHighSchoolConnection")
        {
        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<SchoolInfo> SchoolInfos { get; set; }

        public DbSet<Academic> Academics { get; set; }

        public DbSet<Facility> Facilities { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }


     

    }


}