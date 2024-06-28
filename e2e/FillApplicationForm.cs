using MiaplazaTest.Pages;
using Microsoft.Playwright;
using MiaplazaTest.Utils;
using Newtonsoft.Json;


namespace MiaplazaTest.e2e;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FillApplicationForm : TestBase
{
    private HomePage _homePage;
    private OnlineSchool _onlineSchool;
    private ParentInformation _parentInformation;
    private TestComponents _testComponents;
    private UserData _userData;
    private StudentInformation _studentInformation;
    private StudentData _studentData;

    [SetUp]
    public void SetUp()
    {
        _homePage = new HomePage(_page!);
        _onlineSchool = new OnlineSchool(_page!);
        _parentInformation = new ParentInformation(_page!);
        _testComponents = new TestComponents(_page!);
        _studentInformation = new StudentInformation(_page!);
    }

    [Test]
    public async Task GoToWebsiteAndFillTheApplicationForm()
    {
        var jsonFilePath = Path.Combine(projectDirectory, ".\\Utils\\parentData.json");
        string jsonString = File.ReadAllText(jsonFilePath);
        UserData parentInfo = JsonConvert.DeserializeObject<UserData>(jsonString);

        await _homePage.GoToHomePage(_baseUrl);
        await _homePage.ClickOnOnlineSchoolLink();
        await _onlineSchool.ClickOnApplyLink();
        await _parentInformation.AddName(parentInfo.Name);
        await _parentInformation.AddSurname(parentInfo.Surname);
        await _parentInformation.AddEmail(parentInfo.Email);
        await _parentInformation.AddPhone(parentInfo.Phone);
        await _parentInformation.SelectInformationAboutGuardian(false);
        await _parentInformation.SelectHowDidYouHearAboutUs(parentInfo.SocialMediaOptions);
        await _parentInformation.AddPreferredStartDate(parentInfo.StudentStartDateMonth, parentInfo.StudentStartDateDay);
        await _testComponents.ClickNextButton();

        jsonFilePath = Path.Combine(projectDirectory, ".\\Utils\\studentData.json");
        jsonString = File.ReadAllText(jsonFilePath);
        StudentData studentInfo = JsonConvert.DeserializeObject<StudentData>(jsonString);

        await _studentInformation.VerifyStudentInfromationPageIsVsible();
        await _studentInformation.SelectHowManyStudentsWantToEntroll(true);
        await _studentInformation.AddName(studentInfo.Name);
        await _studentInformation.AddSurname(studentInfo.Surname);
        await _studentInformation.AddPreferredName(studentInfo.PreferedName);
        await _studentInformation.AddEmail(studentInfo.Email);
        await _studentInformation.AddPhone(studentInfo.Phone);
        await _studentInformation.SelectConsentContactinChild(false);
        await _studentInformation.AddDateOfBirth(studentInfo.StudentDateOfBirth);
        await _studentInformation.SelectGender(studentInfo.StudentGender);
        await _studentInformation.SelectMiaPrepAccount(studentInfo.MiaPrepAccount);
        await _studentInformation.SelectTypeOfMembership(studentInfo.Membership);
        await _studentInformation.AddSchoolingType(studentInfo.SchoolingType);
        await _studentInformation.SelectMathSubjects(studentInfo.MathOptions);
        await _studentInformation.SelectEnglishLevels(studentInfo.EnglishOptions);
        await _studentInformation.SelectScienceSubjects(studentInfo.ScienceOptions);
        await _studentInformation.AddSchoolElectives(studentInfo.Electives);
        await _studentInformation.SelectStudentNeeds(studentInfo.LearningChallenges);
    }
}
