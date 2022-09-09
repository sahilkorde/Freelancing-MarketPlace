using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Trial3.Models;

namespace Trial3.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class

public enum Profession
{
    writer,
    disgital_artist,
    video_Editor,
    graphic_Designer,
    content_Writer,
    Web_Developer,
    FrontEnd_Developer,
    Android_Developer,
    UI_UX_Designer,
    Translator,
    WordPress_Developer
}
public enum Gender
{
    Male,
    Female,
    Other,
    Choose_not_Say
}

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; }
    [PersonalData]
    public string LastName { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }
    [PersonalData]
    public Gender Gender { get; set; }
    [PersonalData]
    public string? Description { get; set; }
    [PersonalData]
    public Profession? Profession { get; set; }
    public List<Project>? UserProjects { get; set; }
    public List<Project>? AcceptedProject { get; set; }
    public List<Bid>? Bids { get; set; }
    public List<Review>? UserReviews { get; set; }
    public List<MessageBox>? EmployerMessageBoxes { get; set; }
    public List<MessageBox>? FreelacerMessageBoxes { get; set; }

}

