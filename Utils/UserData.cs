public class UserData
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string StudentStartDateMonth { get; set; }
    public string StudentStartDateDay { get; set; }
    public List<string> SocialMediaOptions { get; set; }
}

public class StudentData : UserData
{
    public string PreferedName { get; set; }
    public string StudentDateOfBirth { get; set; }
    public string StudentGender { get; set; }
    public bool MiaPrepAccount { get; set; }
    public string SchoolingType { get; set; }
    public string Phone { get; set; }
    public string StartDate { get; set; }
    public List<string> MathOptions { get; set; }
    public List<string> EnglishOptions { get; set; }
    public List<string> ScienceOptions { get; set; }
    public string Electives { get; set; }
    public bool LearningChallenges { get; set; }
    public string Membership {  get; set; }
}